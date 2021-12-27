using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
  public  class ExportProjectssTask
    {
        [XmlAttribute("TasksCount")]
        public string TaskCount { get; set; }
        public string ProjectName { get; set; }
        public string HasEndDate { get; set; }
        [XmlArray("Tasks")]
        public ExportPrTask[] Tasks { get; set; }

    }
}
