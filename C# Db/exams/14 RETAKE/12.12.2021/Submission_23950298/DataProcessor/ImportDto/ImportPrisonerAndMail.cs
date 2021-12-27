﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerAndMail
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("[T][h][e] [A-Z]{1}[a-z]+")]
        public string Nickname { get; set; }
        [Range(18,65)]
        public int Age { get; set; }
        [Required]
        public string IncarcerationDate { get; set; }
        public string ReleaseDate { get; set; }
       
        [Range(0,double.MaxValue)]
        public decimal? Bail { get; set; }
        public int CellId { get; set; }
        public ImportMail[] Mails { get; set; }
    }

    

  

}
