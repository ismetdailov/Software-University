using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisoner
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("^The [A-z]*$")]
        public string Nickname { get; set; }
        [Range(18,65)]
        public int Age { get; set; }
        [Required]
        public string IncarcerationDate { get; set; }
        public string ReleaseDate { get; set; }
        public decimal? Bail { get; set; }
        public int? CellId { get; set; }
        public ImportMails[] Mails { get; set; }
    }

  

    public class ImportMails
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        [RegularExpression("^[A-z0-9 ]*str.$")]
        public string Address { get; set; }
    }

}
