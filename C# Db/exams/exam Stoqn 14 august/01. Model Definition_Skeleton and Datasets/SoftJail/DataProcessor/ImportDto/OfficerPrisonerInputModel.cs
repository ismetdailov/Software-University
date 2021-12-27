﻿using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
  public  class OfficerPrisonerInputModel
    {
        [Required]
        [StringLength (30, MinimumLength =3)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [XmlElement("Money")]

        public decimal Money { get; set; }
        [EnumDataType(typeof(Position))]
        [XmlElement("Position")]

        public string Position { get; set; }

        [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]

        public string Weapon { get; set; }
        [XmlElement("DepartmentId")]

        public int DepartmentId { get; set; }
        [XmlArray("Prisoners")]
        public PrisonerIdInputModel[] Prisoners { get; set; }
    }
    //•	Id – integer, Primary Key
    //•	FullName – text with min length 3 and max length 20 (required)
    //•	Nickname – text starting with "The " and a single word only of letters with an uppercase letter for beginning(example: The Prisoner) (required)
    //•	Age – integer in the range[18, 65] (required)
    //•	IncarcerationDate ¬– Date(required)
    //•	ReleaseDate– Date
    //•	Bail– decimal(non - negative, minimum value: 0)
    //•	CellId - integer, foreign key
    //•	Cell – the prisoner's cell
    //•	Mails - collection of type Mail
    //•	PrisonerOfficers - collection of type OfficerPrisoner
    [XmlType("Prisoner")]
    public class PrisonerIdInputModel
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
