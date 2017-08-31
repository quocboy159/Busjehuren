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
    [XmlRoot("Adres")]
    public class AdresModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Straat")]
        public string Straat { get; set; }

        [XmlElement("Huisnummer")]
        public string Huisnummer { get; set; }

        [XmlElement("Postcode")]
        public string Postcode { get; set; }

        [XmlElement("Plaats")]
        public string Plaats { get; set; }

        [XmlElement("Bestemming")]
        public BestemmingModel Land { get; set; }

        [XmlElement("Telefoonnummer")]
        public string Telefoonnummer { get; set; }

        [XmlElement("Faxnummer")]
        public string Faxnummer { get; set; }

        [XmlElement("Mobielnummer")]
        public string Mobielnummer { get; set; }
        public static AdresModel Parse(Adre adresEntity,string currentLocation)
        {
            var adres = new AdresModel
            {
                Id = adresEntity.Id,
                Faxnummer = adresEntity.Faxnummer,
                Land = adresEntity.Destination != null ? BestemmingModel.Parse(adresEntity.Destination, currentLocation) : new BestemmingModel(),
                Mobielnummer = adresEntity.Mobielnummer,
                Plaats = adresEntity.Plaats,
                Postcode = adresEntity.Postcode,
                Straat = adresEntity.Straat,
                Huisnummer = adresEntity.Huisnummer,
                Telefoonnummer = adresEntity.Telefoonnummer,
            };
            return adres;
        }
    }
}