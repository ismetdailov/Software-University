using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto
{
    public class ImportUsers
    {
       [Required]
       [RegularExpression("^[A-z]+ [A-z]+$")]
        public string FullName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(3,103)]
        public int Age { get; set; }
        public ImportCard[] Cards { get; set; }
    }

}
