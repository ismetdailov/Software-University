using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Data
{
  public  class UserCard
    {
      //  public int Id { get; set; }
      [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
