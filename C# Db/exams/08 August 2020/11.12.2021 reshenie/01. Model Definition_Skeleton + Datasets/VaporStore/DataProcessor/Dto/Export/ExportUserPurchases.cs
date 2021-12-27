﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
   public class ExportUserPurchases
    {
        [XmlAttribute("username")]
        public string Username { get; set; }
        public ExportPurchases[] Purchases { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
