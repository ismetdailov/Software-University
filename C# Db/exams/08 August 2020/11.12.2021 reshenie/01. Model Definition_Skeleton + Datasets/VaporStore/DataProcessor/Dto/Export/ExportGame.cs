﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Game")]
   public class ExportGame
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Price { get; set; }
    }
}
