using System;
using System.Collections.Generic;
using Busjehuren.Core.EF;
using System.Linq;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("Eigenschap")]
    public class PropertyModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Naam")]
        public string Name { get; set; }
        [XmlElement("Categorie")]
        public int Categorie { get; set; }

        [XmlElement("IsFilterAttribuut")]
        public bool IsFilterAttribuut { get; set; }

        [XmlElement("IsFastCheckAttribuut")]
        public bool IsFastCheckAttribuut { get; set; }

        [XmlElement("Status")]
        public int Status { get; set; }

        [XmlElement("Type")]
        public int Type { get; set; }

        [XmlElement("IsZoekResultaatEigenschap")]
        public bool IsZoekResultaatEigenschap { get; set; }

        [XmlElement("IsUitrusting")]
        public bool IsUitrusting { get; set; }
        public List<PropertyDetailModel> PropertyDetails { get; set; }

        public static PropertyModel Parse(Eigenschap eigenschapEntity)
        {
            var eigenschap = new PropertyModel
            {
                Id = eigenschapEntity.Id,
                Name = eigenschapEntity.Naam,
                Categorie = eigenschapEntity.Categorie,
                IsFastCheckAttribuut = eigenschapEntity.IsFastCheckFilterAttribuut,
                IsFilterAttribuut = eigenschapEntity.IsFilterAttribuut,
                Status = eigenschapEntity.Status,
                IsUitrusting = eigenschapEntity.IsUitrusting,
                IsZoekResultaatEigenschap = eigenschapEntity.IsZoekResultaatEigenschap,
                Type = eigenschapEntity.Type,
                PropertyDetails = PropertyDetailModel.Parse(eigenschapEntity.EigenschapWaardes)
            };

            return eigenschap;
        }

        public static PropertyModel Parse(Eigenschap eigenschapEntity, bool isForCamper)
        {
            var eigenschap = Parse(eigenschapEntity);
            eigenschap.PropertyDetails = isForCamper ? eigenschapEntity.CamperEigenschappens.Select(c => PropertyDetailModel.Parse(c.EigenschapWaarde)).ToList() : PropertyDetailModel.Parse(eigenschapEntity.EigenschapWaardes);
            return eigenschap;
        }



        public static List<PropertyModel> Parse(IEnumerable<Eigenschap> eigenschappenEntity)
        {
            return eigenschappenEntity.Where(e => e.Status != 2).Select(Parse).ToList();
        }
        
    }
}
