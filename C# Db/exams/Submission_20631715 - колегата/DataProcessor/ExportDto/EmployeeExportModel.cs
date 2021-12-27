using System;
using System.Collections.Generic;
using System.Text;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class EmployeeExportModel
    {
        public string Username  { get; set; }

        public List<TaskExportModel> Tasks { get; set; }
    }

    public class TaskExportModel
    {
        public string TaskName { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime DueDate { get; set; }

        public LabelType LabelType { get; set; }

        public ExecutionType ExecutionType { get; set; }

    }
}