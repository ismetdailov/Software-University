using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUsers
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression("^[A-z]* [A-z]*$")]
        public string FullName { get; set; }
        [Required]
      //  [RegularExpression("^[A-Z][a-z]{2,} [A-Z][a-z]{2,}$")]

        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Range(3,103)]
        public int Age { get; set; }
        public ImportCards[] Cards { get; set; }
    }
}


