using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportGames
    {
        [Required]
        public string Name { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public IEnumerable<string> Tags { get; set; }
    }


}
