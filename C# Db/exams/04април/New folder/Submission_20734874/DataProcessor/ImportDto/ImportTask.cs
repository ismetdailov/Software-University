using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]

    public class ImportTask
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public ExecutionType ExecutionType { get; set; }
        [Required]
        public LabelType LabelType { get; set; }
    }
}
