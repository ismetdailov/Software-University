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
            var result = context.Prisoners.ToArray().Where(x => ids.Any(i=>i==x.Id)).ToArray().Select(p => new
            {
                Id = p.Id,
                Name = p.FullName,
                CellNumber = p.Cell.CellNumber,
                Officers = p.PrisonerOfficers.Select(o => new
                {
                    OfficerName = o.Officer.FullName,
                    Department = o.Officer.Department.Name
                }).OrderBy(z => z.OfficerName).ToArray(),
                TotalOfficerSalary =decimal.Parse(p.PrisonerOfficers.Sum(s => s.Officer.Salary).ToString("f2"))

            }).OrderBy(z => z.Name).ThenBy(c => c.Id).ToArray();
            return JsonConvert.SerializeObject(result,Formatting.Indented);

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisoners = prisonersNames.Split(",").ToArray();
            var result = context.Prisoners.ToArray().Where(x => prisoners.Contains(x.FullName)).ToArray().Select(p => new ExportPrisoners
            {
                Id = p.Id,
                Name = p.FullName,
                IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Message = p.Mails.Select(m => new ExportMessages
                {
                    Description = ReversStr(m.Description)
                }).ToArray()
            }).OrderBy(x => x.Name).ThenBy(z => z.Id).ToArray();
            var xml =new XmlSerializer(typeof(ExportPrisoners[]), new XmlRootAttribute("Prisoners"));
            var sw = new StringWriter();
            var ns =new XmlSerializerNamespaces();
            ns.Add("", "");

            xml.Serialize(sw, result,ns);
            return sw.ToString().TrimEnd() ;

        }
        public static string ReversStr(string s)
        {
            var arr= s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}