using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
  public  class ImportPurchases
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        
        public PurchaseType  Type { get; set; }
        public string Key { get; set; }
        public string Card { get; set; }
        public string Date { get; set; }
    }
}
