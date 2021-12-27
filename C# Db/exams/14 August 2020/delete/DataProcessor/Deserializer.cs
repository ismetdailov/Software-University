namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

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
            return output.ToString().TrimEnd();






            //var output = new StringBuilder();
            //var data = JsonConvert.DeserializeObject<IEnumerable<ImportDepartmentsCells>>(jsonString);
            //foreach (var departJson in data)
            //{
            //    if (!IsValid(departJson))
            //    {
            //        output.AppendLine("Invalid Data");
            //        continue;
            //    }
            //    var depart = new Department
            //    {
            //        Name = departJson.Name,
            //    };

            //    var flag = true;
            //    foreach (var cel in departJson.Cells)
            //    {
                   
            //        if (!IsValid(cel))
            //        {
            //            flag = false;
            //            break;
            //        }
                    

            //        depart.Cells.Add(  new Cell()
            //        {
            //            CellNumber = cel.CellNumber,
            //            HasWindow = cel.HasWindow,
            //        });
            //    }
            //    if (flag == false)
            //    {
            //        output.AppendLine("Invalid Data");
            //        continue;

            //    }
            //    if (depart.Cells.Count == 0)
            //    {
            //        output.AppendLine("Invalid Data");
            //        continue;
            //    }
            //    context.Departments.Add(depart);
            //    context.SaveChanges();
            //    output.AppendLine($"Imported {depart.Name} with {depart.Cells.Count} cells");

            //}
            //return output.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            return "Hei ismet";
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            return "Hei ismet";

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