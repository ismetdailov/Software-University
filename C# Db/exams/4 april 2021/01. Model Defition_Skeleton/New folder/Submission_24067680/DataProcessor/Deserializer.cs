namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var output = new StringBuilder();
            var serialize = new XmlSerializer(typeof(ImportProjects[]),new XmlRootAttribute("Projects"));
            var data = (ImportProjects[])serialize.Deserialize(new StringReader(xmlString));

            foreach (var project in data)
            {
                var openBool = DateTime.TryParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDate);
                var dueBool = DateTime.TryParseExact(project.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate);
                if (!IsValid(project)|| !openBool)
                {
                    output.AppendLine("Invalid data!");
                    continue;
                }
                var projectForAdd = new Project
                {
                    Name = project.Name,
                    OpenDate = openDate,
                    DueDate = dueDate,
                };
                foreach (var task in project.Tasks)
                {
                    var opBool = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var opdate);
                    var duBool = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dudate);
                    if (!IsValid(task)|| !duBool || !opBool)
                    {
                        output.AppendLine("Invalid data!");
                        continue;
                    }
                    if (dudate>dueDate&& dueBool || opdate<openDate)
                    {
                        output.AppendLine("Invalid data!");
                        continue;
                    }
                    var taskForAdd = new Task
                    {
                        Name = task.Name,
                        OpenDate = opdate,
                        DueDate = dudate,
                        ExecutionType = (ExecutionType) task.ExecutionType,
                        LabelType =(LabelType) task.LabelType
                    };
                    projectForAdd.Tasks.Add(taskForAdd);
                }
                context.Projects.Add(projectForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully imported project - {projectForAdd.Name} with {projectForAdd.Tasks.Count} tasks.");
            }
            return output.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var output = new StringBuilder();
            var employyes = JsonConvert.DeserializeObject<IEnumerable<ImportEmployee>>(jsonString);

            foreach (var emplo in employyes)
            {
                if (!IsValid(emplo))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                var employee = new Employee
                {
                    Username = emplo.Username,
                    Email = emplo.Email,
                    Phone = emplo.Phone,
                };
                var id = context.Tasks.Select(x => x.Id).ToList();
                foreach (var taskDi in emplo.Tasks.Distinct())
                {
                    if (!id.Contains(taskDi))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }
                    var taskIdID = new EmployeeTask
                    {
                        TaskId = taskDi
                    };
                    employee.EmployeesTasks.Add(taskIdID);
                }
                context.Employees.Add(employee);
                context.SaveChanges();
                output.AppendLine($"Successfully imported employee - {employee.Username} with {employee.EmployeesTasks.Count} tasks.");
            }

            return output.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}