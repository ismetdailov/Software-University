﻿using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType]
    public class GameExport
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}