using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Purchase")]
    public class ExportUserPurchase
    {
        public string Card { get; set; }
        public string Cvc { get; set; }
        public string Date { get; set; }
        public GameExport Game { get; set; }
    }
    
}
