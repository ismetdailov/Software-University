using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]

  public  class ImportProjections
    {
        [XmlAttribute ("MovieId")]
        [Required]
        public string MovieId { get; set; }

        [XmlAttribute("DateTime")]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public string DateTime { get; set; }


        [XmlAttribute("Movie")]
        public string Movie { get; set; }
    }
}
//< Projection >
//    < MovieId > 38 </ MovieId >
//    < DateTime > 2019 - 04 - 27 13:33:20 </ DateTime >
       
//         </ Projection >
