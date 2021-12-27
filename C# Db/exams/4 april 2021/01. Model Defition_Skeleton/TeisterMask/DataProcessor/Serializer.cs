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
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var result = context.Projects.ToArray().Where(x => x.Tasks.Any()).Select(p => new ExportProjects
            {
                TasksCount = p.Tasks.Count,
                ProjectName = p.Name,
                HasEndDate = p.DueDate.HasValue ?  "Yes": "No",
                Tasks = p.Tasks.ToArray().Select(t => new ExportTasks()
                {
                    Name = t.Name,
                    Label = t.LabelType.ToString()
                }).OrderBy(x => x.Name).ToArray()
            }).OrderByDescending(x => x.TasksCount).ThenBy(y => y.ProjectName).ToArray();
            var xml = new XmlSerializer(typeof(ExportProjects[]), new XmlRootAttribute("Projects"));
            var sw = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xml.Serialize(sw, result, ns);
            return sw.ToString().TrimEnd();

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var result = context.Employees
                .Where(x=>x.EmployeesTasks.Any(x=>x.Task.OpenDate>=date))
                .ToArray()
                .Select(e => new
            {
                Username = e.Username,
                Tasks = e.EmployeesTasks
                .Where(x => x.Task.OpenDate >= date)
                .ToArray()
                .OrderByDescending(x => x.Task.DueDate)
                .ThenBy(x => x.Task.Name)
                .Select(t => new
                {
                    TaskName = t.Task.Name,
                    OpenDate = t.Task.OpenDate.ToString("d",CultureInfo.InvariantCulture),
                    DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                    LabelType = t.Task.LabelType.ToString(),
                    ExecutionType = t.Task.ExecutionType.ToString()
                }).ToArray()
            })
                .OrderByDescending(v => v.Tasks.Length)
                .ThenBy(x => x.Username)
                .Take(10)
                .ToArray();
                
            return JsonConvert.SerializeObject(result,Formatting.Indented);
        }
    }
}