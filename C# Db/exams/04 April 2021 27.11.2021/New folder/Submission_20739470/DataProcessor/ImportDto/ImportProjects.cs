using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
   public class ImportProjects
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public ImportTask[] Tasks { get; set; }
    }
    [XmlType("Task")]
    public class ImportTask
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [Required]
        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [Range(0, 3)]
        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }

        [Range(0, 4)]
        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
  //< Project >
  //  < Name > S </ Name >
  //  < OpenDate > 25 / 01 / 2018 </ OpenDate >
  //  < DueDate > 16 / 08 / 2019 </ DueDate >
  //  < Tasks >
  //    < Task >
  //      < Name > Australian </ Name >
  //      < OpenDate > 19 / 08 / 2018 </ OpenDate >
  //      < DueDate > 13 / 07 / 2019 </ DueDate >
  //      < ExecutionType > 2 </ ExecutionType >
  //      < LabelType > 0 </ LabelType >
  //    </ Task >
  //    < Task >
  //      < Name > Upland Boneset </ Name >
   
  //         < OpenDate > 24 / 10 / 2018 </ OpenDate >
   
  //         < DueDate > 11 / 06 / 2019 </ DueDate >
   
  //         < ExecutionType > 2 </ ExecutionType >
   
  //         < LabelType > 3 </ LabelType >
   
  //       </ Task >
   
  //     </ Tasks >
   
  //   </ Project >
