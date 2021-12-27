namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects.Include(x => x.Tasks)
                .ToList().Where(x => x.Tasks.Count() > 0)
                .Select(x => new ProjectExportDTO
                {
                    ProjectName = x.Name,
                    HasEndDate = x.DueDate == null ? "No" : "Yes",
                    TasksCount = x.Tasks.Count(),
                    Tasks = x.Tasks.OrderBy(x => x.Name)
                    .Select(t => new TaskExportDTO
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString(),
                    })
                    .ToArray(),
                })
                .OrderByDescending(x => x.TasksCount)
                .ThenBy(x => x.ProjectName)
                .ToList();

            return XmlConverter.Serialize(projects, "Projects");
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //date = DateTime.ParseExact(date.ToString("d"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var employees2 = context.Employees
                .Include(x => x.EmployeesTasks)
                .ThenInclude(x => x.Task)
                .ToList()
                .Where(x => x.EmployeesTasks.Where(et => et.Task.OpenDate >= date).Count() > 0)
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks
                    .Where(et => et.Task.OpenDate >= date)
                    .OrderByDescending(t => t.Task.DueDate)
                    .ThenBy(t => t.Task.Name)
                    .Select(et => new
                    {
                        TaskName = et.Task.Name,
                        OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = et.Task.LabelType.ToString(),
                        ExecutionType = et.Task.ExecutionType.ToString(),
                    })
                    
                    .ToList()
                })
                .OrderByDescending(x => x.Tasks.Count())
                .ThenBy(x => x.Username)
                .Take(10)
                .ToList();
            //var employees = context.Employees
            //    .Include(x => x.EmployeesTasks)
            //    .ThenInclude(x => x.Task)
            //    .ToList()
            //    .Where(x => x.EmployeesTasks.Where(et => et.Task.OpenDate >= date).Count() > 0)
            //    .Select(x => new
            //    {
            //        Username = x.Username,
            //        Tasks = x.EmployeesTasks
            //        .Where(et => et.Task.OpenDate >= date)
            //        .Select(et => new
            //        {
            //            TaskName = et.Task.Name,
            //            OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
            //            DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
            //            LabelType = et.Task.LabelType.ToString(),
            //            ExecutionType = et.Task.ExecutionType.ToString(),
            //        })
            //        .OrderByDescending(t => t.DueDate)
            //        .ThenBy(t => t.TaskName)
            //        .ToList()
            //    })
            //    .OrderByDescending(x => x.Tasks.Count())
            //    .ThenBy(x => x.Username)
            //    .Take(10)
            //    .ToList();

            return JsonConvert.SerializeObject(employees2, Formatting.Indented);
        }
    }
}