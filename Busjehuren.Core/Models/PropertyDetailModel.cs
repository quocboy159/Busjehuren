using System;
using System.Collections.Generic;
using Busjehuren.Core.EF;
using System.Linq;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("EigenschapWaarde")]
    public class PropertyDetailModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("EigenschapId")]
        public int PropertyId { get; set; }
        [XmlElement("Waarde")]
        public string Value { get; set; }
        [XmlElement("Bijzonderheden")]
        public string Description { get; set; }
        [XmlElement("EigenschapNaam")]
        public string EigenschapNaam { get; set; }
        [XmlElement("IsZoekResultaatEigenschap")]
        public bool IsZoekResultaatEigenschap { get; set; }

        [XmlElement("IsUitrusting")]
        public bool IsUitrusting { get; set; }
        public static List<PropertyDetailModel> Parse(IEnumerable<EigenschapWaarde> eigenschapWaardeEntity)
        {
            return eigenschapWaardeEntity.Select(e => Parse(e)).ToList();
        }

        public static PropertyDetailModel Parse(EigenschapWaarde eigenschapWaardeEntity)
        {
            var eigenschapWaarde = new PropertyDetailModel
            {
                Id = eigenschapWaardeEntity.Id,
                Value = eigenschapWaardeEntity.Waarde,
                Description = eigenschapWaardeEntity.Bijzonderheden,
                EigenschapNaam = eigenschapWaardeEntity.Eigenschap.Naam,
                PropertyId = eigenschapWaardeEntity.Eigenschap.Id,
                IsUitrusting = eigenschapWaardeEntity.Eigenschap.IsUitrusting,
                IsZoekResultaatEigenschap = eigenschapWaardeEntity.Eigenschap.IsZoekResultaatEigenschap
            };
            return eigenschapWaarde;
        }
    }
}
