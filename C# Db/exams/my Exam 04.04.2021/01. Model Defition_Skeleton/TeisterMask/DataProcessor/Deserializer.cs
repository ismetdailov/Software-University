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

            XmlSerializer serializer = new XmlSerializer(typeof(ImportMessage[]), new XmlRootAttribute("Projects"));
            TextReader reader = new StringReader(xmlString);
            var projectsDtos = (IEnumerable<ImportMessage>)serializer.Deserialize(reader);

            StringBuilder sb = new StringBuilder();
            List<Project> projects = new List<Project>();

            foreach (var currProject in projectsDtos)
            {
                if (!IsValid(currProject))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isValidDate = DateTime.TryParseExact(currProject.DueDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime myDate);

                Project project = new Project()
                {
                    Name = currProject.Name,
                    OpenDate = DateTime.ParseExact(currProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DueDate = isValidDate ? (DateTime?)myDate : null,
                };

                foreach (var currTast in currProject.Tasks)
                {
                    if (!IsValid(currTast))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var taskOpenDate = DateTime.ParseExact(currTast.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var taskDueDate = DateTime.ParseExact(currTast.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (taskOpenDate <= project.OpenDate
                        || taskDueDate >= project.DueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task task = new Task()
                    {
                        Name = currTast.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = Enum.Parse<ExecutionType>(currTast.ExecutionType),
                        LabelType = Enum.Parse<LabelType>(currTast.LabelType),
                        Project = project
                    };

                    project.Tasks.Add(task);
                }

                projects.Add(project);
                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
            //  var output = new StringBuilder();
            //  var xmlSerializer = new XmlSerializer(
            //      typeof(ImportMessage[]), // ТУК НЕМОЖЕМ ДА ИЗПОЛЗВАМЕ ieNUMERUBLE И ЗАТОВА И ЗАТОВА ИЗПОЛЗВАМЕ МАСИВ
            //      new XmlRootAttribute("Projects"));

            //  var message = // ТУК ПОЛУЧАВАМЕ ДАННИТЕ
            //      (ImportMessage[])xmlSerializer.Deserialize(
            //          new StringReader(xmlString));
            //  foreach (var massage in message)
            //  {
            //      if (!IsValid(massage))
            //      {
            //          output.AppendLine("Invalid Data");
            //          continue;
            //      }

            //      bool parsedDate = DateTime.TryParseExact(
            //          massage.OpenDate, "dd/MM/yyyy HH",
            //          CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            //      if (!parsedDate)
            //      {
            //          output.AppendLine("Invalid Data");
            //          continue;
            //      }
            //      //purchase Е МАСИВ ИЛИ КОЛЕКЦИЯ ОТ ТЕЗИ НЕЩА ДАТЕ, ТИПЕ И ТН.
            //      var purchase = new Project
            //      {
            //          Name = massage.Name,

            //      };

            //    //  context.EmployeesTasks.Add();
            //      //user не е в дадената таблица тя е свързана с card И ПО ТОЗИ НАЧИН
            //      //БЪРКАМЕ В ТАБЛИЦИТЕ И ВЗИМАМЕ ИМЕТО НА ЮЗЕРА ЧРЕЗ НЕГОВАТА КАРТА
            //    //  var username = context.Users.Where(x => x.Id == purchase.Card.UserId)
            //        //  .Select(x => x.Username).FirstOrDefault();
            //      output.AppendLine($"Successfully imported project - {purchase.Name} with {purchase} tasks.");
            //  }

            //  context.SaveChanges();
            //  return output.ToString();
            ////  return "TODO";
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeesDtos = JsonConvert.DeserializeObject<ICollection<UsersInputModel>>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Employee> employees = new List<Employee>();

            foreach (var currEmployee in employeesDtos)
            {
                if (!IsValid(currEmployee))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }

                Employee employee = new Employee()
                {
                    Username = currEmployee.Username,
                    Email = currEmployee.Email,
                    Phone = currEmployee.Phone
                };

                var tasksIds = context.Tasks.Select(t => t.Id).ToList();

                foreach (var currTask in currEmployee.Tasks.Distinct())
                {
                    if (!tasksIds.Contains(currTask))
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }

                    employee.EmployeesTasks.Add(new EmployeeTask
                    {
                        TaskId = currTask
                    });
                }

                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
            //var output = new StringBuilder();
            //var employeeeee = JsonConvert
            //    .DeserializeObject<IEnumerable<UsersInputModel>>(jsonString);

            //foreach (var employe in employeeeee)
            //{
            //    if (!IsValid(employe) || !IsValid(employe.Username.Any()) || !IsValid(employe.Phone.Any()))
            //    {



            //        // Invalid data
            //        output.AppendLine("Invalid Data");
            //        continue;
            //    }
            //    var emplo = new Employee
            //    {
            //        Username = employe.Username,
            //        Email = employe.Email,
            //        Phone = employe.Phone,

            //        //EmployeesTasks = employe.Tasks.Select(x => new Task
            //        // {
            //        //  Id = x.
            //        // }).ToList(),
            //    };
            //        context.Employees.Add(emplo);
            //    output.AppendLine($"Successfully imported employee - {employe.Username} with {employe.Tasks.Count()} tasks.");
            //};

            //context.SaveChanges();
            //return output.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}