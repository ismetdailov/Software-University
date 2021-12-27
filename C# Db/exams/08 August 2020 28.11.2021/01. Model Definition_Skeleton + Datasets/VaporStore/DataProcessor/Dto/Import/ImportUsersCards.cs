using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUsersCards
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [RegularExpression("^[A-z]+ [A-z]+$")]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Range(3,103)]
        public int Age { get; set; }
        public ImportCard[] Cards { get; set; }
    }


}
