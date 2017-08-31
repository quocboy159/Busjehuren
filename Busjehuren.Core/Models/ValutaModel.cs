using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("Valuta")]
    public class ValutaModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Code")]
        public string Code { get; set; }

        [XmlElement("Omschrijving")]
        public string Omschrijving { get; set; }

        [XmlElement("Koers")]
        public decimal Koers { get; set; }



        public static List<ValutaModel> Parse(IEnumerable<Valuta> valutaEntity)
        {
            return valutaEntity.Select(Parse).ToList();
        }

        public static ValutaModel Parse(Valuta valutaEntity)
        {
            var valuta = new ValutaModel
            {
                Code = valutaEntity.Code,
                Id = valutaEntity.Id,
                Koers = valutaEntity.Koers,
                Omschrijving = valutaEntity.Omschrijving
            };

            return valuta;
        }
    }
}
