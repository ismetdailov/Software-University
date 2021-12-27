using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.DataProcessor.ImportDto
{
   public class ImportEmployee
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression("^[A-z0-9]+$")]

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^((\d){3}-(\d){3}-(\d){4})$")]
        public string Phone { get; set; }

        
        public int[] Tasks { get; set; }
    }
}
//[
//  {
//    "Username": "jstanett0",
//    "Email": "kknapper0@opera.com",
//    "Phone": "819-699-1096",
//    "Tasks": [
//      34,
//      32,
//      65,
//      30,
//      30,
//      45,
//      36,
//      67
//    ]
//  },
//  ...
//]
