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
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var result = context.Projects.Where(x => x.Tasks.Any()).ToArray()
                  .Select(p => new ExportProjectsTasks()
                  {
                      ProjectName = p.Name,
                      HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                      TasksCount = p.Tasks.Count,
                      Tasks = p.Tasks.Select(t => new ExportTasks()
                      {
                          Name = t.Name,
                          Label = t.LabelType.ToString()
                      }).OrderBy(x => x.Name).ToArray()
                  }).OrderByDescending(x => x.TasksCount).ThenBy(x => x.ProjectName).ToArray();
            var serializer = new XmlSerializer(typeof(ExportProjectsTasks[]),
                new XmlRootAttribute("Projects"));
            var sw = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(sw, result, ns);
            return sw.ToString();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {

            var employee = context.Employees
                .Where(x => x.EmployeesTasks.Any(e => e.Task.OpenDate >= date))
                .ToArray()
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks.ToArray()
                .Where(x => x.Task.OpenDate >= date).OrderByDescending(x => x.Task.DueDate)
                .ThenBy(x => x.Task.Name)
                .Select(t => new
                {
                    TaskName = t.Task.Name,
                    OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                    DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                    LabelType = t.Task.LabelType.ToString(),
                    ExecutionType = t.Task.ExecutionType.ToString()
                })

                .ToArray()
                })
                .OrderByDescending(x => x.Tasks.Length)
                .ThenBy(x => x.Username).Take(10)
                .ToArray();
            return JsonConvert.SerializeObject(employee, Formatting.Indented);
        }
    }
}