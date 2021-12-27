using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")] // име на таг

    public class PurchaseModelInputModel
    {

        [XmlAttribute("title")] // атрибут
        [Required]
        public string GameName { get; set; }

        [Required]
        //СЛАГАМЕ PurchaseType ДА ГО ИЗИГРАЕМ И ГО ПРАВИМ НЪЛЪБЪЛ
        public PurchaseType? Type { get; set; }

        [Required]
        [RegularExpression("[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}")]
        public string Key { get; set; }

        [Required]
        [RegularExpression("[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}")]
        public string Card { get; set; }

        [Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public string Date { get; set; }
    }
}
