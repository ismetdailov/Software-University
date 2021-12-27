namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var output = new StringBuilder();
            var data =  JsonConvert.DeserializeObject<IEnumerable<ImportDepartment>>(jsonString);
            foreach (var dep in data)
            {
                if (!IsValid(dep) || dep.Cells.Length==0)
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                var depForAdd = new Department
                {
                    Name = dep.Name,
                };
                var flag = true;
                foreach (var cell in dep.Cells)
                {
                    if (!IsValid(cell))
                    {
                        output.AppendLine("Invalid Data");
                        flag = false;
                        continue;
                    }
                    var cellForAdd = new Cell
                    {
                        CellNumber = cell.CellNumber,
                        HasWindow = cell.HasWindow
                    };
                    depForAdd.Cells.Add(cellForAdd);
                }
                if (flag == true)
                {
                    context.Departments.Add(depForAdd);
                    context.SaveChanges();
                    output.AppendLine($"Imported {depForAdd.Name} with {depForAdd.Cells.Count} cells");
                }
            }
            return output.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var output = new StringBuilder();
            var data = JsonConvert.DeserializeObject <IEnumerable<ImportPrisoner>>(jsonString);
            foreach (var prison in data)
            {
                if (!IsValid(prison))
                {
                output.AppendLine("Invalid Data");
                continue;
                }
                var incrDateBool = DateTime.TryParseExact(prison.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var incDate);
                var realDateBool = DateTime.TryParseExact(prison.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var realDate);
                var bail = prison.Bail;
                var prisonForAdd = new Prisoner
                {
                    FullName = prison.FullName,
                    Nickname = prison.Nickname,
                    Age = prison.Age,
                    IncarcerationDate =incDate,
                    ReleaseDate =realDate,
                    Bail = prison.Bail,
                    CellId = prison.CellId,
                };
                var flag = true;
                foreach (var mail in prison.Mails)
                {
                    if (!IsValid(mail))
                    {
                        output.AppendLine("Invalid Data");
                        flag = false;
                        continue;
                    }
                    var mailForAdd = new Mail
                    {
                        Description = mail.Description,
                        Sender = mail.Sender,
                        Address = mail.Address
                    };
                    prisonForAdd.Mails.Add(mailForAdd);
                  
                }
                if (flag == true)
                {
                    context.Prisoners.Add(prisonForAdd);
                    context.SaveChanges();
                    output.AppendLine($"Imported {prisonForAdd.FullName} {prisonForAdd.Age} years old");
                }
            }

            return output.ToString().TrimEnd();

        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(typeof(ImportOfficers[]), new XmlRootAttribute("Officers"));
            var data = (ImportOfficers[])xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var officer in data)
            {
                
                if (!IsValid(officer))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                var officerForAdd = new Officer
                {
                    FullName = officer.Name,
                    Salary = officer.Money,
                    Position = (Position)Enum.Parse(typeof(Position),officer.Position),
                    Weapon = (Weapon)Enum.Parse(typeof(Weapon), officer.Weapon),
                    DepartmentId = officer.DepartmentId,
                   
                };
                foreach (var pris in officer.Prisoners)
                {
                    officerForAdd.OfficerPrisoners.Add(new OfficerPrisoner { PrisonerId = pris.Id });
                }
                context.Officers.Add(officerForAdd);
                context.SaveChanges();
                output.AppendLine($"Imported {officerForAdd.FullName} ({officerForAdd.OfficerPrisoners.Count} prisoners)");
            }
            return output.ToString().TrimEnd();

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