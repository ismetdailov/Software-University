namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {




            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ProjectExportDTO[]), new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (StringWriter stringWriter = new StringWriter(sb))
            {
                ProjectExportDTO[] projects = context.Projects
                    .Where(p => p.Tasks.Count > 0)
                    .Select(p => new ProjectExportDTO()
                    {
                        HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                        ProjectName = p.Name,
                        TasksCount = p.Tasks.Count,
                        Tasks = p.Tasks.Select(t => new TaskExportDTO()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                            .OrderBy(t => t.Name)
                            .ToArray()
                    })
                    .OrderByDescending(p => p.TasksCount)
                    .ThenBy(p => p.ProjectName)
                    .ToArray();

                xmlSerializer.Serialize(stringWriter, projects, namespaces);
            }

            return sb.ToString().TrimEnd();
            //var project = context.Projects.Include(x => x.Tasks)
            //    .ToList().Where(x => x.Tasks.Count() > 0)
            //    .Select(x => new ProjectExportDTO
            //    {
            //        ProjectName = x.Name,
            //        HasEndDate = x.DueDate == null ? "No" : "Yes",
            //        Tasks = x.Tasks.OrderBy(x => x.Name)
            //        .Select(t => new TaskExportDTO
            //        {
            //            Name = t.Name,
            //            Label = t.LabelType.ToString()
            //        })
            //        .ToArray(),
            //    })
            //    .OrderByDescending(x => x.TasksCount)
            //    .ThenBy(x => x.ProjectName)
            //    .ToList();

            //return XmlConverter.Serialize(project, "Projects");


        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {

            var topEmployees = context.Employees
                .Include(x => x.EmployeesTasks)
                .ThenInclude(x => x.Task)
                .ToList()
                .Where(x => x.EmployeesTasks.Where(et => et.Task.OpenDate >= date).Count() > 0)
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks
                    .Where(et => et.Task.OpenDate >= date)
                    .OrderByDescending(x => x.Task.DueDate)
                    .ThenBy(x => x.Task.Name)
                    .Select(et => new
                    {
                        TaskName = et.Task.Name,
                        OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = et.Task.LabelType.ToString(),
                        ExecutionType = et.Task.ExecutionType.ToString()
                    })
                    .ToList()

                })
                .OrderByDescending(x => x.Tasks.Count())
                .ThenBy(x =>x.Username)
                .Take(10)
                .ToList();
            return JsonConvert.SerializeObject(topEmployees, Formatting.Indented);

        }
    }
}