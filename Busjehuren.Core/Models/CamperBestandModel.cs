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
    [XmlRoot("CamperBestand")]
    public class CamperBestandModel : FileModel
    {
        [XmlElement("IsCamperDoorsnee")]
        public bool IsIndelingDoorsnee { get; set; }

        [XmlElement("Volgorde")]
        public int Volgorde { get; set; }

        [XmlElement("BestandId")]
        public int BestandId { get; set; }



        public static List<CamperBestandModel> Parse(IEnumerable<CamperBestanden> camperBestandenEntity)
        {
            return camperBestandenEntity.Select(Parse).ToList();
        }

        public static CamperBestandModel Parse(CamperBestanden camperBestandenEntity)
        {
            var camperbestand = new CamperBestandModel
            {
                LocationOnDisc = camperBestandenEntity.Bestand.LokatieOpSchijf,
                Id = camperBestandenEntity.Id,
                Name = camperBestandenEntity.Bestand.Naam,
                Volgorde = camperBestandenEntity.Volgorde,
                IsIndelingDoorsnee = camperBestandenEntity.IsIndelingDoorsnee,
                LocationOnWeb = camperBestandenEntity.Bestand.LokatieOpWeb,
                MediaType = camperBestandenEntity.Bestand.MediaType,
                Description = camperBestandenEntity.Bestand.Omschrijving,
                BestandId = camperBestandenEntity.Bestand.Id
            };
            return camperbestand;
        }
    }
}
