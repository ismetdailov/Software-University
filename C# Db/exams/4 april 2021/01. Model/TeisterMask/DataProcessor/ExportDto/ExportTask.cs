using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Task")]

    public class ExportTask
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
}
