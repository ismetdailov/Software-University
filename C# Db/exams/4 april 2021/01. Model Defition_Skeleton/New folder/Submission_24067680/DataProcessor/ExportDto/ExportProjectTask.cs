using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class ExportPrTask
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
}
