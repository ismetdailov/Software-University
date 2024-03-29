﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
  public class ExportUser
    {
        [XmlAttribute("username")]
        public string Username { get; set; }
        public decimal TotalSpend { get; set; }
        [XmlArray]
        public ExportUserPurchases[] Purchases { get; set; }
    }
}
