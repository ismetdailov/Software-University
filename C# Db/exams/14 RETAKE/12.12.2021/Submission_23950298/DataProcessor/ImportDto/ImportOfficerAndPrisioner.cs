using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerAndPrisioner
    {
       
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(0.0, double.MaxValue)]

        public decimal Money { get; set; }
        [Required]

        public string Position { get; set; }
        [Required]

        public string Weapon { get; set; }
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public ImportPrissoner[] Prissoners { get; set; }
    }

}
