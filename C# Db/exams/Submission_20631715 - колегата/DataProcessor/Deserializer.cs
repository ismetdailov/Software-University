namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
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
            var projects = XmlConverter.Deserializer<ProjectImportModel>(xmlString, "Projects");
            StringBuilder sb = new StringBuilder();

            foreach (var project in projects)
            {
                if (!IsValid(project))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var projectOpenDate = DateTime
                    .ParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                bool isDueDateNotNull =
                    DateTime.TryParseExact(project.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime dueDateProj);

                var validTasks = new List<TaskInputModel>();
                foreach (var task in project.Tasks)
                {
                    var currTaskOpenDate = DateTime.ParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var currTaskDueDate = DateTime.ParseExact(task.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (IsValid(task) && 
                        !(currTaskOpenDate < projectOpenDate) && 
                        !(currTaskDueDate < projectOpenDate) && 
                        !isDueDateNotNull &&
                        dueDateProj > currTaskDueDate
                        )
                    {
                         validTasks.Add(task);
                    }
                    else
                    {
                       
                            sb.AppendLine(ErrorMessage);
                    }
                }
                project.Tasks = validTasks.ToArray();

                //DateTime.ParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),

                var curProject = new Project
                {
                    Name = project.Name,
                    DueDate = isDueDateNotNull ? (DateTime?)dueDateProj : null,
                    OpenDate = projectOpenDate,
                    Tasks = project.Tasks.Select(t => new Task
                    {
                        Name = t.Name,
                        DueDate = DateTime.ParseExact(t.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        OpenDate = DateTime.ParseExact(t.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ExecutionType = (ExecutionType)t.ExecutionType,
                        LabelType = (LabelType)t.LabelType
                    })
                    .ToList()
                };

                context.Projects.Add(curProject);
                context.SaveChanges();
                sb.AppendLine(String.Format(SuccessfullyImportedProject, curProject.Name, curProject.Tasks.Count()));
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeImportModel>>(jsonString);
            StringBuilder sb = new StringBuilder();
            var tasksIds = context.Tasks.Select(x => x.Id).ToList();

            foreach (var employee in employees)
            {
                if (!IsValid(employee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                

                var curEmployee = new Employee
                {
                    Username = employee.Username,
                    Email = employee.Email,
                    Phone = employee.Phone,
                };
                var validEmpTasks = new List<EmployeeTask>();
                foreach (var task in employee.Tasks.Distinct())
                {
                    var taskId = int.Parse(task);
                    if (!tasksIds.Contains(taskId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    validEmpTasks.Add(new EmployeeTask { TaskId = taskId });
                }

                curEmployee.EmployeesTasks = validEmpTasks;


                context.Employees.Add(curEmployee);
                context.SaveChanges();
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, curEmployee.Username, curEmployee.EmployeesTasks.Count()));
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