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

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjects[]), new XmlRootAttribute("Projects"));

            using StringReader stringReader = new StringReader(xmlString);

            ImportProjects[] projectDtos = (ImportProjects[])xmlSerializer.Deserialize(stringReader);

            List<Project> projects = new List<Project>();

            foreach (var projectDto in projectDtos)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime openDate;
                bool isOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out openDate);

                if (!isOpenDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? dueDate = null;

                if (!String.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    DateTime dueDateDt;
                    bool isDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDateDt);

                    if (!isDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    dueDate = dueDateDt;
                }

                Project p = new Project()
                {
                    Name = projectDto.Name,
                    OpenDate = openDate,
                    DueDate = dueDate
                };

                foreach (var taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskOpenDate;
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out taskOpenDate);

                    if (!isTaskOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskDueDate;
                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDate);

                    if (!isTaskDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (taskOpenDate < openDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (dueDate.HasValue && taskDueDate > dueDate.Value)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task t = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType
                    };

                    p.Tasks.Add(t);
                }

                projects.Add(p);
                sb.AppendLine(String.Format(SuccessfullyImportedProject, p.Name, p.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
            //var output = new StringBuilder();
            //var xmlDeserialize = new XmlSerializer(typeof(ImportProjects[]),
            //    new XmlRootAttribute("Projects"));
            //var xmlProject = (ImportProjects[])xmlDeserialize.Deserialize(new StringReader(xmlString));

            //foreach (var project in xmlProject)
            //{
            //    var openDate = DateTime.TryParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDateTime);
            //    var dueDate = DateTime.TryParseExact(project.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDateTime);
            //    if (!openDate )
            //    {
            //        output.AppendLine("Invalid data!");
            //        continue;
            //    }
            //    if (!IsValid(project))
            //    {
            //        output.AppendLine("Invalid data!");
            //        continue;
            //    }
            //    var projectXml = new Project
            //    {
            //        Name = project.Name,
            //        OpenDate = openDateTime,
            //        DueDate = dueDateTime,

            //    };
            //    foreach (var task in project.Tasks)
            //    {
            //        if (!IsValid(task))
            //        {
            //            output.AppendLine("Invalid data!");
            //            continue;
            //        }

            //        var taskOpenDate = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out var taskOpen);
            //        var taskDueDate = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out var taskDue);
            //        if (!taskOpenDate || !taskDueDate)
            //        {
            //            output.AppendLine("Invalid data!");
            //            continue;
            //        }

            //        if (!taskDueDate)
            //        {
            //            output.AppendLine(ErrorMessage);
            //            continue;
            //        }

            //        if (taskOpen < openDateTime)
            //        {
            //            output.AppendLine(ErrorMessage);
            //            continue;
            //        }

            //        if (dueDateTime!=null && taskDue > dueDateTime)
            //        {
            //            output.AppendLine(ErrorMessage);
            //            continue;
            //        }
            //        var taskXml = new Task
            //        {
            //            Name = task.Name,
            //            OpenDate = taskOpen,
            //            DueDate = taskDue,
            //            ExecutionType = (ExecutionType)task.ExecutionType,
            //            LabelType = (LabelType)task.LabelType,
            //        };
            //        projectXml.Tasks.Add(taskXml);
            //    }
            //    context.Projects.Add(projectXml);
            //    context.SaveChanges();
            //    output.AppendLine($"Successfully imported project - {projectXml.Name} with {projectXml.Tasks.Count} tasks.");
            //}
            //return output.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportEmployee[] employeeDtos = JsonConvert.DeserializeObject<ImportEmployee[]>(jsonString);

            List<Employee> employees = new List<Employee>();

            foreach (var employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee e = new Employee()
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone
                };

                foreach (int taskId in employeeDto.Tasks.Distinct())
                {
                    Task t = context.Tasks.Find(taskId);

                    if (t == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    e.EmployeesTasks.Add(new EmployeeTask()
                    {
                        Task = t
                    });
                }

                employees.Add(e);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, e.Username, e.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
            //var output = new StringBuilder();
            //var data = JsonConvert.DeserializeObject<IEnumerable<ImportEmployee>>(jsonString);
            //var tasks = context.Tasks.Select(x => x.Id).ToList();

            //foreach (var employee in data)
            //{
            //    if (!IsValid(employee))
            //    {
            //       output.AppendLine("Invalid data!");
            //        continue;
            //    }
            //    var emplo = new Employee
            //    {
            //        Username = employee.Username,
            //        Email = employee.Email,
            //        Phone = employee.Phone,


            //    };
            //    foreach (var task in employee.Tasks.Distinct())
            //    {
            //        Task taska = context.Tasks
            //         .Find(task);

            //        if (taska == null)
            //        {
            //            output.AppendLine(ErrorMessage);
            //            continue;
            //        }

            //        emplo.EmployeesTasks.Add(new EmployeeTask()
            //        {
            //            Task = taska
            //        });
            //        //// var tas = context.Tasks.FirstOrDefault(x => x.Id == task);

            //        // if (!tasks.Contains(task))
            //        // {
            //        //     output.AppendLine("Invalid data!");
            //        //     continue;
            //        // }
            //        // emplo.EmployeesTasks.Add(new EmployeeTask { TaskId = task });

            //    }

            //    context.Employees.Add(emplo);
            //    context.SaveChanges();
            //    output.AppendLine($"Successfully imported project - {emplo.Username} with {emplo.EmployeesTasks.Count()} tasks.");
            //}
            //return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}