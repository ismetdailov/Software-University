namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var data = context.Prisoners.ToArray().Where(x => ids.Any(i=>i==x.Id))
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName,
                    CellNumber = x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.ToArray().Select(x => new
                    {
                        OfficerName = x.Officer.FullName,
                        Department = x.Officer.Department.Name,
                    }).OrderBy(x=>x.OfficerName).ToArray(),
                    TotalOfficerSalary = x.PrisonerOfficers.Sum(x=>x.Officer.Salary),
                })
                .OrderBy(x=>x.Name).ThenBy(x=>x.Id).ToArray();


            return JsonConvert.SerializeObject(data,Formatting.Indented);

        }
      
        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var data = context.Prisoners.ToArray().Where(x => prisonersNames.Contains(x.FullName))
                .Select(x => new ExportPrisoners()
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = x.Mails.ToArray().Select(x => new EncryptedMessages()
                    {
                        Description = ReverseString(x.Description)
                    }).ToArray()
                }).OrderBy(x=>x.Name).ThenBy(x=>x.Id).ToArray();
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportPrisoners[]),
                    new XmlRootAttribute("Prisoners"));
            var sw = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(sw, data, ns);
            return sw.ToString();

        }
        private static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}