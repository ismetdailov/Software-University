using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{


    public class UserJsonInputModel
        {
        [Required]
            public string FullName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
            public int Age { get; set; }
            public IEnumerable<CardJsonInputModel> Cards { get; set; }
        }

    
}
