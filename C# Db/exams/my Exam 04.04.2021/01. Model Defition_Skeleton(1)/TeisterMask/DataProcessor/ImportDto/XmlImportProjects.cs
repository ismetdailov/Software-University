using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class XmlImportProjects
    {

        [XmlElement("Name")]
        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        [Required]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]

        public string DueDate { get; set; }
        [XmlArray("Tasks")]
        public TaskInport[] Tasks { get; set; }

   
    }
}
[XmlType("Task")]

public class TaskInport
{
    [XmlElement("Name")]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Name { get; set; }

    [XmlElement("OpenDate")]
    [Required]
    public string OpenDate { get; set; }
    
    
    [Required]
    [XmlElement("DueDate")]
    public string DueDate { get; set; }

    [XmlElement("ExecutionType")]
    public int ExecutionType { get; set; }

    [XmlElement("LabelType")]
    public int LabelType { get; set; }
}

//<Project>
//    <Name>S</Name>
//    <OpenDate>25/01/2018</OpenDate>
//    <DueDate>16/08/2019</DueDate>
//    <Tasks>
//      <Task>
//        <Name>Australian</Name>
//        <OpenDate>19/08/2018</OpenDate>
//        <DueDate>13/07/2019</DueDate>
//        <ExecutionType>2</ExecutionType>
//        <LabelType>0</LabelType>
//      </Task>
//      <Task>
//        <Name>Upland Boneset</Name>
//        <OpenDate>24/10/2018</OpenDate>
//        <DueDate>11/06/2019</DueDate>
//        <ExecutionType>2</ExecutionType>
//        <LabelType>3</LabelType>
//      </Task>
//    </Tasks>
//  </Project>
