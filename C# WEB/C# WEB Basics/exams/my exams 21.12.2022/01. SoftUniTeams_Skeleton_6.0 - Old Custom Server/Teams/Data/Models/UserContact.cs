using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams.Data.Models
{
    public class UserContact
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
