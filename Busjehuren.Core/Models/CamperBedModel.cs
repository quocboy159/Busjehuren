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
    [XmlRoot("CamperBed")]
    public class CamperBedModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Omschrijving")]
        public string Omschrijving { get; set; }




        public static List<CamperBedModel> Parse(IEnumerable<CamperBed> camperBeddenEntity)
        {
            return camperBeddenEntity.Select(Parse).ToList();
        }

        public static CamperBedModel Parse(CamperBed camperBedEntity)
        {
            var camperBed = new CamperBedModel
            {
                Id = camperBedEntity.Id,
                Omschrijving = camperBedEntity.Omschrijving
            };

            return camperBed;
        }

    }
}
