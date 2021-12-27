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
            return "to do";

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            // var employees = context.Employees
            //.Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
            //.ToArray()
            //.Select(e => new
            //{
            //    Username = e.Username,
            //    Tasks = e.EmployeesTasks
            //        .ToArray()
            //        .Where(et => et.Task.OpenDate >= date)
            //        .OrderByDescending(et => et.Task.DueDate)
            //        .ThenBy(et => et.Task.Name)
            //        .Select(et => new
            //        {
            //            TaskName = et.Task.Name,
            //            OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
            //            DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
            //            LabelType = et.Task.LabelType.ToString(),
            //            ExecutionType = et.Task.ExecutionType.ToString()
            //        })
            //        .ToArray()

            //})
            //.OrderByDescending(e => e.Tasks.Length)
            //.ThenBy(e => e.Username)
            //.Take(10)
            //.ToArray();

            // string json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            // return json;
            var data = context.Employees
               .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .ToArray()
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks.ToArray()
                    .Where(x => x.Task.OpenDate >= date)
                    .OrderByDescending(x => x.Task.DueDate)
                    .ThenBy(x => x.Task.Name)
                    .Select(x => new
                    {
                        TaskName = x.Task.Name,
                        OpenDate = x.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = x.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = x.Task.LabelType.ToString(),
                        ExecutionType = x.Task.ExecutionType.ToString()
                    }).ToArray()
                })
                .OrderByDescending(x => x.Tasks.Length)
                .ThenBy(x => x.Username)
                .Take(10)
                .ToArray();
            return JsonConvert.SerializeObject(data, Formatting.Indented);

            //   return "to do";

        }
    }
}