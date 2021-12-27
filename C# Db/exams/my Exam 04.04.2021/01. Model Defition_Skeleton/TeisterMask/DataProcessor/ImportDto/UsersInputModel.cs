using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.DataProcessor.ImportDto
{
   public class UsersInputModel
    {
        public UsersInputModel()
        {
            Tasks = new List<int>();
        }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        [RegularExpression("^[A-z0-9]+$")]
        //  [RegularExpression("[A-z0-9]{1,}")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}-[0-9]{3}-[0-9]{4}")]
        public string Phone { get; set; }
        public IEnumerable<int> Tasks { get; set; }



    }
}
//public EmplImport()
//{
//    Tasks = new List<int>();
//}

//[Required]
//[StringLength(40, MinimumLength = 3)]
//[RegularExpression("^[A-z0-9]+$")]
//public string Username { get; set; }

//[Required]
//[EmailAddress]
//public string Email { get; set; }

//[Required]
//[RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
//public string Phone { get; set; }

//public ICollection<int> Tasks { get; set; }