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
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {

            var output = new StringBuilder();
            var departmentData = JsonConvert.DeserializeObject<IEnumerable<ImportDepartmentsCells>>(jsonString);

            foreach (var department in departmentData)
            {
                if (!IsValid(department))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                if (!department.Cells.Any())
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                var depart = new Department
                {
                    Name = department.Name
                };
                var flag = false;
                foreach (var cell in department.Cells)
                {
                    if (!IsValid(cell))
                    {
                        flag = true;
                        output.AppendLine("Invalid Data");
                        continue;
                    }
                    var cellJson = new Cell
                    {
                        CellNumber = cell.CellNumber,
                        HasWindow = cell.HasWindow
                    };
                    depart.Cells.Add(cellJson);
                }

                if (flag == false)
                {
                    context.Departments.Add(depart);
                    output.AppendLine($"Imported {depart.Name} with {depart.Cells.Count} cells");
                    context.SaveChanges();
                }
            }
            return output.ToString();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var output = new StringBuilder();
            var prisonerJson = JsonConvert.DeserializeObject<IEnumerable<ImportPrisonerAndMail>>(jsonString);

            foreach (var prisoner in prisonerJson)
            {
                if (!IsValid(prisoner))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                var incarreDate = DateTime.TryParseExact(prisoner.IncarcerationDate, "d",CultureInfo.InvariantCulture, DateTimeStyles.None, out var IncrDate);
                var releaseBoolDate = DateTime.TryParseExact(prisoner.IncarcerationDate, "d",CultureInfo.InvariantCulture, DateTimeStyles.None, out var releaseDate);
                var prison = new Prisoner
                {
                    FullName = prisoner.FullName,
                    Nickname = prisoner.Nickname,
                    Age = prisoner.Age,
                    IncarcerationDate = IncrDate,
                    ReleaseDate = releaseDate,
                    Bail = prisoner.Bail,
                    CellId = prisoner.CellId,

                };
                var flag = true;
                foreach (var mail in prisoner.Mails)
                {
                    if (!IsValid(mail))
                    {
                        flag = false;
                        output.AppendLine("Invalid Data");
                        continue;
                    }
                    var mailForAdd = new Mail
                    {
                        Description = mail.Description,
                        Sender = mail.Sender,
                        Address = mail.Address,
                    };
                    prison.Mails.Add(mailForAdd);
                }
                if (flag == true)
                {
                    context.Prisoners.Add(prison);
                    context.SaveChanges();
                    output.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }

            }
            return output.ToString().Trim();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var output = new StringBuilder();
            var data = new XmlSerializer(typeof(ImportOfficerAndPrisioner[]), new XmlRootAttribute("Officers"));
            var OfficerData = (ImportOfficerAndPrisioner[])data.Deserialize(new StringReader(xmlString));

            foreach (var officer in OfficerData)
            {
                if (!IsValid(officer))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                object positionObj;
                bool isPositionValid = Enum.TryParse(typeof(Position), officer.Position, out positionObj);

                if (!isPositionValid)
                {
                    output.AppendLine("Invalid Data");

                    continue;
                }

                object weaponObj;
                bool isWeaponValid = Enum.TryParse(typeof(Weapon), officer.Weapon, out weaponObj);

                if (!isWeaponValid)
                {
                    output.AppendLine("Invalid Data");

                    continue;
                }
                var officerForAdd = new Officer
                {
                    FullName = officer.Name,
                    Salary = officer.Money,
                    Position = (Position)positionObj,
                    Weapon = (Weapon)weaponObj,
                    DepartmentId = officer.DepartmentId
                };
                foreach (var prisoners in officer.Prissoners)
                {
                    
                    var prisonerForAdd = new Prisoner
                    {
                        Id = prisoners.Id
                    };
                    var prison = context.OfficersPrisoners.FirstOrDefault(x => x.PrisonerId == prisonerForAdd.Id);
                    if (prison!= null)
                    {
                        officerForAdd.OfficerPrisoners.Add(prison);
                    }
                }
                context.Officers.Add(officerForAdd);
                context.SaveChanges();
                output.AppendLine($"Imported {officerForAdd.FullName} ({officerForAdd.OfficerPrisoners.Count} prisoners)");
            }
            return output.ToString().Trim();

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