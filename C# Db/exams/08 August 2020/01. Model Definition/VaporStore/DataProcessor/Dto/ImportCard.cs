using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto
{
   public class ImportCard
    {
        [Required]
        [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        public string Number { get; set; }
        [Required]
        [RegularExpression("^[0-9]{3}$")]
        public string CVC { get; set; }
        public CardType Type { get; set; }
    }
}
