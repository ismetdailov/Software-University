using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
  public  class ImportOfficers
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Range(0.0,double.MaxValue)]
        public decimal Money { get; set; }
        [Required]
        [EnumDataType(typeof(Position))]

        public string  Position { get; set; }
        [Required]
        [EnumDataType(typeof(Weapon))]
        public string Weapon { get; set; }
        public int DepartmentId { get; set; }
        [XmlArray]
        public ImporOfficertPrisoner[] Prisoners { get; set; }

    }
    [XmlType("Prisoner")]
    public class ImporOfficertPrisoner
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
