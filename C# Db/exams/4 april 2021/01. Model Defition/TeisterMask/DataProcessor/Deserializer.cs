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
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportProject[]), new XmlRootAttribute("Projects"));

            List<Project> projectsToAdd = new List<Project>();

            using (StringReader reader = new StringReader(xmlString))
            {
                ImportProject[] projectDtOs = (ImportProject[])xmlSerializer.Deserialize(reader);


                foreach (var projectDtO in projectDtOs)
                {
                    if (!IsValid(projectDtO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime openDate;
                    bool isValidOpenDate = DateTime.TryParseExact(projectDtO.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out openDate);

                    if (!isValidOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime? dueDate;
                    if (!String.IsNullOrEmpty(projectDtO.DueDate))
                    {
                        DateTime projectDueDate;
                        bool isvalidDueDate = DateTime
                            .TryParseExact(projectDtO.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out projectDueDate);

                        if (!isvalidDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        dueDate = projectDueDate;
                    }
                    else
                    {
                        dueDate = null;
                    }

                    Project project = new Project()
                    {
                        Name = projectDtO.Name,
                        OpenDate = openDate,
                        DueDate = dueDate
                    };

                    foreach (var taskDto in projectDtO.Tasks)
                    {

                        if (!IsValid(taskDto))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        DateTime taskOpenDate;
                        bool isValidTaskOpdenDate = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out taskOpenDate);

                        if (!isValidTaskOpdenDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        if (taskOpenDate < project.OpenDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        DateTime taskDueDate;
                        bool isValidTaskDueDate = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out taskDueDate);

                        if (!isValidTaskDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        if (project.DueDate.HasValue)
                        {
                            if (taskDueDate > project.DueDate)
                            {
                                sb.AppendLine(ErrorMessage);
                                continue;
                            }
                        }


                        Task task = new Task()
                        {
                            Name = taskDto.Name,
                            OpenDate = taskOpenDate,
                            DueDate = taskDueDate,
                            ExecutionType = (ExecutionType)taskDto.ExecutionType,
                            LabelType = (LabelType)taskDto.LabelType,

                        };
                        project.Tasks.Add(task);

                    }

                    projectsToAdd.Add(project);
                    sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                }

                context.Projects.AddRange(projectsToAdd);
                context.SaveChanges();

                return sb.ToString().TrimEnd();
                //var output = new StringBuilder();
                //var xmlSerialize = new XmlSerializer(typeof(ImportProject[]), new XmlRootAttribute("Projects"));
                //var xmlProject = (ImportProject[])xmlSerialize.Deserialize(new StringReader(xmlString));
                //foreach (var project in xmlProject)
                //{
                //    if (!IsValid(project))
                //    {
                //        output.AppendLine( "Invalid Data!");
                //        continue;
                //    }

                //    DateTime openDate;
                //    bool isValidOpenDate = DateTime.TryParseExact(project.OpenDate, "dd/MM/yyyy",
                //        CultureInfo.InvariantCulture, DateTimeStyles.None, out openDate);
                //    DateTime projectDueDate;
                //    bool isvalidDueDate = DateTime
                //        .TryParseExact(project.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                //            DateTimeStyles.None, out projectDueDate);
                //    if (!isValidOpenDate || !isvalidDueDate)
                //    {
                //        output.AppendLine("Invalid Data!");
                //        continue;

                //    }
                //    var projectNew = new Project
                //    {
                //        Name = project.Name,
                //        OpenDate = openDate,
                //        DueDate = projectDueDate,

                //    };
                //    foreach (var task in project.Tasks)
                //    {
                //        if (!IsValid(task))
                //        {
                //            output.AppendLine("Invalid Data!");
                //            continue;
                //        }
                //        DateTime taskOpenDate;
                //        bool isValidTaskOpdenDate = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy",
                //            CultureInfo.InvariantCulture, DateTimeStyles.None,
                //            out taskOpenDate);
                //        DateTime taskDueDate;
                //        bool isValidTaskDueDate = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy",
                //            CultureInfo.InvariantCulture, DateTimeStyles.None,
                //            out taskDueDate);
                //        if (!isValidTaskOpdenDate || !isValidTaskDueDate)
                //        {
                //            output.AppendLine("Invalid Data!");
                //            continue;


                //        }
                //        var taskNew = new Task
                //        {

                //            Name = task.Name,
                //            OpenDate= taskOpenDate,
                //            DueDate = taskDueDate,
                //            ExecutionType = (ExecutionType) task.ExecutionType,
                //            LabelType =(LabelType) task.LabelType,
                //        };
                //        projectNew.Tasks.Add(taskNew);
                //    }
                //    context.Projects.Add(projectNew);
                //    output.AppendLine($"Successfully imported project - {projectNew.Name} with {projectNew.Tasks.Count} tasks.");
                //}
                //    context.SaveChanges();
                //    return output.ToString().TrimEnd();
            }
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var output = new StringBuilder();
            var data = JsonConvert.DeserializeObject<IEnumerable<ImportEmployee>>(jsonString);
            var taskId = context.Tasks.Select(x => x.Id).ToList();

         

            var employee = AutoMapper.Mapper.Map<ImportEmployee>(jsonString);
            //foreach (var employ in data)
            //{
            //    var employee = new Employee
            //    {
            //        Username = employ.Username,
            //        Email = employ.Email,
            //        Phone = employ.Phone,

            //    };
            //    var ids = new List<EmployeeTask>();
            //    foreach (var id in employ.Tasks.Distinct())
            //    {
            //        if (!taskId.Contains(id))
            //        {
            //            output.AppendLine(ErrorMessage);
            //            continue;
            //        }
            //        ids.Add(new EmployeeTask { TaskId = id});
            //    }

            // employee.EmployeesTasks=ids;
            //  context.Employees.Add(employee);
            context.SaveChanges();
                output.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.Tasks.Count()));

          //  }
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