using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Users")]
   public class XmlExportModel
    {
        internal string Username;
        internal decimal TotalSpent;
        internal object Purchases;
        internal string Card;
        internal string Cvc;
        internal string Date;
        internal XmlExportModel Game;
        internal string Title;
        internal decimal Price;
        internal string Genre;

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Purchase
        {

            private string cardField;

            private ushort cvcField;

            private string dateField;

            private PurchaseGame gameField;

            /// <remarks/>
            public string Card
            {
                get
                {
                    return this.cardField;
                }
                set
                {
                    this.cardField = value;
                }
            }

            /// <remarks/>
            public ushort Cvc
            {
                get
                {
                    return this.cvcField;
                }
                set
                {
                    this.cvcField = value;
                }
            }

            /// <remarks/>
            public string Date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
            public PurchaseGame Game
            {
                get
                {
                    return this.gameField;
                }
                set
                {
                    this.gameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class PurchaseGame
        {

            private string genreField;

            private decimal priceField;

            private string titleField;

            /// <remarks/>
            public string Genre
            {
                get
                {
                    return this.genreField;
                }
                set
                {
                    this.genreField = value;
                }
            }

            /// <remarks/>
            public decimal Price
            {
                get
                {
                    return this.priceField;
                }
                set
                {
                    this.priceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }
        }


    }
}
