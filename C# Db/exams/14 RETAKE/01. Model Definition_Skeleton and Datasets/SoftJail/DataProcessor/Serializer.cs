namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var everyPrisoner = context.Prisoners
                .Where(x => ids.Contains(x.Id)).Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName,
                    CellNumber = x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.Select(p => new
                    {
                        OfficerName = p.Officer.FullName,
                        Department = p.Officer.Department.Name
                    })
                      .OrderBy(x => x.OfficerName)
                      .ToList(),
                    TotalOfficerSalary = decimal.Parse(x.PrisonerOfficers
                      .Sum(x => x.Officer.Salary)
                      .ToString("F2"))
                })
                .OrderBy(p => p.Name)
                .ThenBy(x => x.Id)
                .ToList();
            string json = JsonConvert.SerializeObject(everyPrisoner, Formatting.Indented);

            return json;

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var neshto = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var result = context.Prisoners
                .Where(x => neshto.Contains(x.FullName))
                .ToArray()
                .Select(x => new PrisonerViewModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages =x.Mails.Select(m => new EncryptedMessagesViewModel
                    { 
                    Description= string.Join("",m.Description.Reverse())
                    })
                    .ToArray()
                    
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToList();
            var xml = XmlConverter.Serialize(result, "Prisoners");
            return xml;




            //XmlSerializer xmlSerializer =
            //    new XmlSerializer(typeof(ExportInboxPrisoner[]),
            //        new XmlRootAttribute("Prisoners"));
            //var sw = new StringWriter();
            ////ТОВА ГО СЛАГАМЕ АКО ИМЕМЕ РАЗЛИКА ОТ НАШИЯ АУТПУТ И АУТПУТА КОЙТО НИ ДАВАТ
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("", "");
            //xmlSerializer.Serialize(sw, result, ns);
            //return sw.ToString();

        }
    }
}