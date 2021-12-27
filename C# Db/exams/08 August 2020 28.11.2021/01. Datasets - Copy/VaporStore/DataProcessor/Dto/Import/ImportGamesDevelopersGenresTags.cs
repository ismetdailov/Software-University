using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportGamesDevelopersGenresTags
    {
        [Required]
        public string Name { get; set; }
        [Range(0.00,double.MaxValue)]
        public decimal Price { get; set; }

        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string[] Tags { get; set; }
    }

}
