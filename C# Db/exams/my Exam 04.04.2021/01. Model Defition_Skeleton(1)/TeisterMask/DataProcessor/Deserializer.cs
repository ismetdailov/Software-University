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
            var xmlSerializer = new XmlSerializer(typeof(XmlImportProjects[]), new XmlRootAttribute("Projects"));
            var importProject = (XmlImportProjects[])xmlSerializer.Deserialize(new StringReader(xmlString));
                

            foreach (var xmlProjects in importProject)
            {

                var validOpenDate =
                   DateTime.TryParseExact(xmlProjects.OpenDate, "dd/MM/yyyy",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDate);
                bool validDueDate =
                   DateTime.TryParseExact(xmlProjects.DueDate, "dd/MM/yyyy",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateDueDate);
                if (!IsValid(xmlProjects))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }
                //if (!validOpenDate)
                //{
                //    output.AppendLine("Invalid Data");
                //    continue;   
                //}
                var validTask = new List<TaskInport>();
                foreach (var tasks in xmlProjects.Tasks)
                {
                    var taskOpenDate =
                        DateTime.ParseExact(tasks.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                    var taskDueDate =
                        DateTime.ParseExact(tasks.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                    if ( (IsValid(tasks) &&
                         taskOpenDate > openDate &&
                         taskDueDate > openDate &&
                        !validDueDate || dateDueDate > taskDueDate))
                    {
                        validTask.Add(tasks);
                    }
                    else
                    {
                        output.AppendLine(ErrorMessage);
                    }
                   
                    };
                var project = new Project
                {

                    Name = xmlProjects.Name,
                    OpenDate = openDate,
                    DueDate = validDueDate ? (DateTime?)dateDueDate : null,
                    Tasks = xmlProjects.Tasks.Select(t => new Task
                    {
                        Name = t.Name,
                        OpenDate = DateTime.ParseExact(t.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DueDate = DateTime.ParseExact(t.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ExecutionType = (ExecutionType)t.ExecutionType,
                        LabelType = (LabelType)t.LabelType


                    })
                    .ToArray()

                };
                xmlProjects.Tasks = validTask.ToArray();
                context.Projects.Add(project);

                context.SaveChanges();
                output.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count()));
            }
            return output.ToString().TrimEnd();


        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employe = JsonConvert
                .DeserializeObject<IEnumerable<EmployeeImport>>(jsonString);
            var output = new StringBuilder();

            var validId = context.Tasks.Select(x => x.Id).ToList();
            foreach (var employeForeach in employe)
            {
                if (!IsValid (employeForeach))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                var validTask = new Employee
                {
                    Username = employeForeach.Username,
                    Email = employeForeach.Email,
                    Phone = employeForeach.Phone
                };
                var validEmployeeTask = new List<EmployeeTask>();
                foreach (var task in employeForeach.Tasks.Distinct())
                {
                    var taskId = int.Parse(task);
                    if (!validId.Contains(taskId))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }
                    validEmployeeTask.Add(new EmployeeTask { TaskId = taskId });
                }
                validTask.EmployeesTasks = validEmployeeTask;
                context.Employees.Add(validTask);
                context.SaveChanges();
                output.AppendLine($"Successfully imported employee - {validTask.Username} with {validTask.EmployeesTasks.Count()} tasks.");
            }


            return output.ToString().TrimEnd(); ;
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}