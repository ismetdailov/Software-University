using SoftJail.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
  [XmlType("Officer")]
   public class ImportPrisoners
    {
        [XmlElement("Name")]

        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [XmlElement("Money")]
        [Range(0, double.MaxValue)]
        public decimal Money { get; set; }


        [EnumDataType(typeof(Position))]
        [XmlElement("Position")]

        public string Position { get; set; }

       [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]

        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]

        public int DepartmentId { get; set; }

        public  PrisonerInport[] Prisoners { get; set; }

    }
    [XmlType("Prisoner")]
    public class PrisonerInport
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
//<? xml version = '1.0' encoding = 'UTF-8' ?>
//   < Officers >
   
//     < Officer >
   
//       < Name > Minerva Kitchingman </ Name >
   
//       < Money > 2582 </ Money >
   
//       < Position > Invalid </ Position >
   
//       < Weapon > ChainRifle </ Weapon >
   
//       < DepartmentId > 2 </ DepartmentId >
   
//       < Prisoners >
   
//         < Prisoner id = "15" />
    
//        </ Prisoners >
    
//      </ Officer >
