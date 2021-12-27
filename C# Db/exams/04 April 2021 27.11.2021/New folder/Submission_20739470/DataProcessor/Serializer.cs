namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            return "TODO";

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