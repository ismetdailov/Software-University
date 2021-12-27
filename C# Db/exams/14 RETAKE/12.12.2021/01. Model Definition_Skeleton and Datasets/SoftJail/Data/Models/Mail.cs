using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.Data.Models
{
   public class Mail
    {
        public int Id { get; set; }

        [Rerquired]
        public string Description { get; set; }

        [Rerquired]
        public string Sender { get; set; }

        [Rerquired]
        public string Address { get; set; }
        public int PrisonerId { get; set; }
        public Prisoner Prisoner { get; set; }
    }
}
