namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var departmentss = JsonConvert
                .DeserializeObject<IEnumerable<ImportDeparments>>(jsonString);
            foreach (var departmetnts in departmentss)
            {
                if (!IsValid(departmetnts) || !IsValid(departmetnts.Cells))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
               else if (!departmetnts.Cells.All(IsValid) || !IsValid(departmetnts.Cells.Any()))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var depart = new Department
                {
                    
                    Name = departmetnts.Name,
                   
                    Cells = departmetnts.Cells.Select(x => new Cell
                    {
                        CellNumber = x.CellNumber,
                        HasWindow = x.HasWindow,
                        
                    })
                    .ToList()
                };
                context.Departments.AddRange(depart);
                sb.AppendLine($"Imported {depart.Name} with {depart.Cells.Count()} cells");
            }
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var validPrisoner = JsonConvert
                .DeserializeObject <IEnumerable< ImportPrisonerMails >> (jsonString);
            foreach (var prisoner in validPrisoner)
            {
                if (!IsValid(prisoner) || !prisoner.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                //   bool incarcerDate = prisoner.IncarcerationDate.ParceExact

                var isValidReleaseDate = DateTime.TryParseExact(prisoner.ReleaseDate,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime releaseDate);
                var IncarcerationDate = DateTime.ParseExact(
                    prisoner.IncarcerationDate,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var newPrisoner = new Prisoner
                {
                    FullName = prisoner.FullName,
                    Nickname = prisoner.Nickname,
                    Age = prisoner.Age,
                    Bail = prisoner.Bail,
                    CellId = prisoner.CellId,
                    ReleaseDate = isValidReleaseDate? (DateTime?) releaseDate : null,
                    IncarcerationDate = IncarcerationDate,
                    Mails = prisoner.Mails.Select(m => new Mail
                    {
                        Sender = m.Sender,
                        Address = m.Address,
                        Description = m.Description

                    }).ToArray()

                };

                context.Prisoners.Add(newPrisoner);
               sb.AppendLine($"Imported {newPrisoner.FullName} {newPrisoner.Age} years old");

            }
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportPrisoners[]),
                new XmlRootAttribute("Officers"));

            var officer = (ImportPrisoners[])xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var validOfficer in officer)
            {
                if (!IsValid(validOfficer))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var newOfficer = new Officer
                {
                    FullName = validOfficer.Name,
                    Salary = validOfficer.Money,
                    DepartmentId = validOfficer.DepartmentId,
                    Position = Enum.Parse<Position>(validOfficer.Position),
                    Weapon = Enum.Parse<Weapon>(validOfficer.Weapon),
                    OfficerPrisoners = validOfficer.Prisoners.Select(p => new OfficerPrisoner
                    {
                        PrisonerId = p.Id
                    }).ToList()
                };
                context.Officers.Add(newOfficer);
                sb.AppendLine($"Imported {newOfficer.FullName} ({newOfficer.OfficerPrisoners.Count()} prisoners)");

            }
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}