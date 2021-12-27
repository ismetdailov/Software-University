using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")] // име на таг

    public class ImportMessage
    {
        [XmlAttribute("Project")] // атрибут
        [Required]
        [StringLength(40, MinimumLength = 2)]

        public string Name { get; set; }
        [Required]
        public string OpenDate { get; set; }

        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public TaskImport[] Tasks { get; set; }
    }

    public class TaskImport
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public string OpenDate { get; set; }

        [Required]
        public string DueDate { get; set; }

        [Required]
        [EnumDataType(typeof(ExecutionType))]
        public string ExecutionType { get; set; }

        [Required]
        [EnumDataType(typeof(LabelType))]

        public string LabelType { get; set; }
    }
}
//[XmlType("Project")]
//public class ProjectImportModel
//{
//    [Required]
//    [StringLength(40, MinimumLength = 2)]
//    public string Name { get; set; }

//    [Required]
//    public string OpenDate { get; set; }

//    public string DueDate { get; set; }

//    [XmlArray("Tasks")]
//    public TaskImport[] Tasks { get; set; }
//}

//[XmlType("Task")]
//public class TaskImport
//{
//    [Required]
//    [StringLength(40, MinimumLength = 2)]
//    public string Name { get; set; }

//    [Required]
//    public string OpenDate { get; set; }

//    [Required]
//    public string DueDate { get; set; }

//    [Required]
//    [EnumDataType(typeof(ExecutionType))]
//    public string ExecutionType { get; set; }

//    [Required]
//    [EnumDataType(typeof(LabelType))]
//    public string LabelType { get; set; }
