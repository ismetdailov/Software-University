using BookShop.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ImportDto
{
    [XmlType("Book")]
  public  class ImportBook
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        [Range(0,3)]
        public int Genre { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(50,5000)]
        public int Pages { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        //public DateTime PublishedOn { get; set; }
        public string PublishedOn { get; set; }

    }
}
