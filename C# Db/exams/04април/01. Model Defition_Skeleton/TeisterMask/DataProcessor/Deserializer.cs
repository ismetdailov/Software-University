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
    using SoftJail.DataProcessor;
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

            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportProjects[]), new XmlRootAttribute("Projects"));

            List<Project> projectsToAdd = new List<Project>();

            using (StringReader reader = new StringReader(xmlString))
            {
                ImportProjects[] projectDtOs = (ImportProjects[])xmlSerializer.Deserialize(reader);


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

                // var sb = new StringBuilder();
                //// var proj = new List<Project>();

                // var result = XmlConverter.Deserializer<ImportProjects>(xmlString, "Projects");

                // foreach (var project in result)
                // {
                //     if (!IsValid(project))
                //     {
                //         sb.AppendLine("Invalid Data");
                //         continue;
                //     }
                //     var isValidOpenDate = DateTime.ParseExact(project.OpenDate,
                //         "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //     bool isVaLidDueDate = DateTime.TryParseExact(project.DueDate,
                //        "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                //                                           out DateTime dueDate);
                //     if (!IsValid(project.DueDate))
                //     {
                //         sb.AppendLine("Invalid Data");
                //         continue;
                //     }
                //     var validTasks = new List<ImportTask>();
                //     foreach (var task in project.Tasks)
                //     {
                //         var currTaskOpenDate = DateTime.ParseExact(task.OpenDate, "dd/MM/yyyy",
                //             CultureInfo.InvariantCulture);
                //         var currTaskDueDate = DateTime.ParseExact(task.DueDate, "dd/MM/yyyy",
                //             CultureInfo.InvariantCulture);

                //         if ((IsValid(task) &&  // error is probably here
                //             currTaskOpenDate > isValidOpenDate &&
                //             currTaskDueDate > isValidOpenDate) &&
                //             (!isVaLidDueDate ||
                //             dueDate > currTaskDueDate)
                //             )
                //         {
                //             validTasks.Add(task);
                //         }
                //         else
                //         {
                //             sb.AppendLine(ErrorMessage);
                //         }
                //     }
                //     project.Tasks = validTasks.ToArray();
                //     var newProject = new Project
                //     {
                //         Name = project.Name,
                //         DueDate = isVaLidDueDate ? (DateTime?)dueDate : null,
                //         OpenDate = isValidOpenDate,
                //         Tasks = project.Tasks.Select(p => new Task
                //         {
                //             Name = p.Name,
                //             OpenDate = DateTime.ParseExact(p.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                //             DueDate = DateTime.ParseExact(p.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                //             ExecutionType = (ExecutionType)p.ExecutionType, //Enum.Parse<ExecutionType>(p.ExecutionType),
                //             LabelType = (LabelType)p.LabelType//Enum.Parse<LabelType>(p.LabelType),
                //         })
                //         .ToList()
                //     };
                //     // proj.Add(newProject);
                //     context.Projects.Add(newProject);
                //     context.SaveChanges();
                //     sb.AppendLine($"Successfully imported project - {newProject.Name} with {newProject.Tasks.Count} tasks.");

                // }

                // //context.Projects.AddRange(proj);
                // return sb.ToString().TrimEnd();

            }
        }
        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employees = JsonConvert.DeserializeObject<IEnumerable<ImportEmployee>>(jsonString);
            StringBuilder sb = new StringBuilder();
             var tasksIds = context.Tasks.Select(x => x.Id).ToList();
            foreach (var emp in employees)
            {
                if (!IsValid(emp) || !IsValid( emp.Email.Any()) || !IsValid(emp.Phone.Any()))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var newEmp = new Employee
                {
                    Username = emp.Username,
                    Email = emp.Email,
                    Phone = emp.Phone,
                };
                var validTask = new List<EmployeeTask>();
                foreach (var task in emp.Tasks.Distinct())
                {
                    var taskId = task;
                    if (!tasksIds.Contains(taskId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    validTask.Add(new EmployeeTask { TaskId = taskId });
                }
                newEmp.EmployeesTasks = validTask;
                context.Employees.Add(newEmp);
                context.SaveChanges();
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, newEmp.Username, newEmp.EmployeesTasks.Count()));
            }
            return sb.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}