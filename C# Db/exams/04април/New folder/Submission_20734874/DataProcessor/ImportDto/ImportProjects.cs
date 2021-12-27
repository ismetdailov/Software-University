using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
   public class ImportProjects
    {
       [Required]
       [MinLength(3)]
       [MaxLength(40)]
      // [RegularExpression("^[A-z0-9]+$")]
        public string Name { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        public ImportTask[] Tasks { get; set; }
    }

}
