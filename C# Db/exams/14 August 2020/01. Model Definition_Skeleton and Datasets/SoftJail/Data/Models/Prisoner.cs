﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Prisoner
    {
        public Prisoner()
        {
            this.Mails = new HashSet<Mail>();
            this.PrisonerOfficers = new HashSet<OfficerPrisoner>();
        }
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FullName { get; set; }
        [Required]

        public string Nickname { get; set; }
        public int Age { get; set; }

        public DateTime IncarcerationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Range(0,double.MaxValue)]
        public decimal? Bail { get; set; }
        public int? CellId { get; set; }
        public Cell Cell { get; set; }
        public ICollection<Mail>Mails { get; set; }
        public ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
    }
}