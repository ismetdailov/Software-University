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
            var xmlSerializer = new XmlSerializer(typeof(ImportProjects[]), new XmlRootAttribute("Projects"));
            var data = (ImportProjects[])xmlSerializer.Deserialize(new StringReader(xmlString));
            foreach (var project in data)
            {
                var openDateBool = DateTime.TryParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDate);
                var dueDateBool = DateTime.TryParseExact(project.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate);
                if (!IsValid(project) || !openDateBool )
                {
                    output.AppendLine("Invalid data!");
                    continue;
                }
                var projectForAdd = new Project
                {
                    Name = project.Name,
                    OpenDate = openDate,
                    DueDate = dueDate
                };
                foreach (var task in project.Tasks)
                {
                    var taskOpenBool = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskOpenDate);
                    var taskDueBool = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskdueDate);
                    if (!IsValid(task) || !taskOpenBool || !taskDueBool || taskOpenDate<openDate || taskdueDate > dueDate && dueDateBool)
                    {
                        output.AppendLine("Invalid data!");
                        continue;
                    }
                    
                    var taskForAdd = new Task
                    {
                        Name = task.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskdueDate,
                        ExecutionType = (ExecutionType)task.ExecutionType,
                        LabelType = (LabelType)task.LabelType
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
            var data = JsonConvert.DeserializeObject<IEnumerable<ImportEmployees>>(jsonString);
            foreach (var emplo in data)
            {
                if (!IsValid(emplo))
                {
                    output.AppendLine("Invalid data!");
                    continue;
                }
                var employeeForAdd = new Employee
                {
                    Username = emplo.Username,
                    Email = emplo.Email,
                    Phone = emplo.Phone
                };
                var uniqeTasks = context.Tasks.Select(x => x.Id).ToList();
                foreach (var task in emplo.Tasks.Distinct())
                {
                    if (!uniqeTasks.Contains(task))
                    {
                        output.AppendLine("Invalid data!");
                        continue;
                    }
                    var taskForAdd = new EmployeeTask
                    {
                        TaskId = task
                    };
                    employeeForAdd.EmployeesTasks.Add(taskForAdd);
                };
                context.Employees.Add(employeeForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully imported employee - {employeeForAdd.Username} with {employeeForAdd.EmployeesTasks.Count} tasks.");
            };
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