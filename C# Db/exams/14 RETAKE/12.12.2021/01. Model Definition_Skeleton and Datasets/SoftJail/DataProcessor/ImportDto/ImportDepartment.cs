using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartment
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        public ImportCell[] Cells { get; set; }
    }

    public class ImportCell
    {
        [Range(1,1000)]
        public int CellNumber { get; set; }
        public bool HasWindow { get; set; }
    }

}
