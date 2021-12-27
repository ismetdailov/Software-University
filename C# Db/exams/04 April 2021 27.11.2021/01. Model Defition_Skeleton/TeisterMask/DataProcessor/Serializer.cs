namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using TeisterMask.DataProcessor.ImportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var data = context.Projects.Where(x => x.Tasks.Any()).ToArray().Select(p => new ExportProjectssTask()
            {
                TaskCount = p.Tasks.Count().ToString(),
                ProjectName = p.Name,
                HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                Tasks = p.Tasks.ToArray().Select(t => new ExportPrTask()
                {
                    Name = t.Name,
                    Label = t.LabelType.ToString()
                }).ToArray().OrderBy(x => x.Name).ToArray()

            }).ToArray().OrderByDescending(x => x.TaskCount).ThenBy(n => n.ProjectName).ToArray();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportProjectssTask[]), new XmlRootAttribute("Projects"));
            var sb = new StringWriter();
            xmlSerializer.Serialize(sb, data, namespaces);
            return sb.ToString().TrimEnd();

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //var employees = context
            //   .Employees
            //   .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
            //   .ToArray()
            //   .Select(e => new
            //   {
            //       e.Username,
            //       Tasks = e.EmployeesTasks
            //           .Where(et => et.Task.OpenDate >= date)
            //           .ToArray()
            //           .OrderByDescending(et => et.Task.DueDate)
            //           .ThenBy(et => et.Task.Name)
            //           .Select(et => new
            //           {
            //               TaskName = et.Task.Name,
            //               OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
            //               DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
            //               LabelType = et.Task.LabelType.ToString(),
            //               ExecutionType = et.Task.ExecutionType.ToString()
            //           })
            //           .ToArray()
            //   })
            //   .OrderByDescending(e => e.Tasks.Length)
            //   .ThenBy(e => e.Username)
            //   .Take(10)
            //   .ToArray();

            //return JsonConvert.SerializeObject(employees, Formatting.Indented);

            //var data = context.Employees
            //    .Include(x => x.EmployeesTasks)
            //    .ThenInclude(x => x.Task)
            //    .ToArray()
            //    .Where(x => x.EmployeesTasks.Where(x => x.Task.OpenDate >= date).Count() > 0)
            //    .Select(e => new
            //    {
            //        Username = e.Username,
            //        Tasks = e.EmployeesTasks
            //        .Where(x => x.Task.OpenDate >= date).OrderByDescending(x => x.Task.DueDate)
            //        .ThenBy(x => x.Task.Name)
            //        .Select(t => new
            //        {
            //            TaskName = t.Task.Name,
            //            OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
            //            DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
            //            LaybelType = t.Task.LabelType.ToString(),
            //            ExecutionType = t.Task.ExecutionType.ToString()
            //        }).ToArray()
            //    }).OrderByDescending(x => x.Tasks.Length)
            //    .ThenBy(x => x.Username)
            //    .Take(10)
            //    .ToArray();
            //return JsonConvert.SerializeObject(data, Formatting.Indented);
            var data = context.Employees.Where(x => x.EmployeesTasks.Any(r => r.Task.OpenDate >= date))
                .ToArray()
                .Select(e => new ExportMostBusiestEmployees()
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks.Where(t => t.Task.OpenDate >= date)
                    .ToArray()
                    .OrderByDescending(x => x.Task.DueDate)
                 .ThenBy(x => x.Task.Name).ToArray()
                    .Select(r => new ExporttttttttTask
                    {
                        TaskName = r.Task.Name,
                        OpenDate = r.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = r.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = r.Task.LabelType.ToString(),
                        ExecutionType = r.Task.ExecutionType.ToString()
                    }).ToArray()
                 
                }).OrderByDescending(x => x.Tasks.Length)
            .ThenBy(x => x.Username).Take(10);

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}