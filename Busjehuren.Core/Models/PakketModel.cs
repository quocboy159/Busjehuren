using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("Pakket")]
    public class PakketModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Naam")]
        public string Naam { get; set; }

        [XmlElement("UitgebreideOmschrijving")]
        public string UitgebreideOmschrijving { get; set; }

        [XmlElement("IsPerPersoon")]
        public bool? IsPerPersoon { get; set; }

        [XmlElement("IsStandaardAan")]
        public bool? IsStandaardAan { get; set; }

        [XmlElement("Prijs")]
        public decimal Prijs { get; set; }

        [XmlElement("Valuta")]
        public ValutaModel Valuta { get; set; }

        [XmlElement("IsLokaalTeBetalen")]
        public bool IsLokaalTeBetalen { get; set; }

        [XmlElement("PakketType")]
        public int PakketType { get; set; }

        [XmlElement("IsGratis")]
        public bool IsGratis { get; set; }

        [XmlElement("GratisVan")]
        public DateTime? GratisVan { get; set; }

        [XmlElement("GratisTot")]
        public DateTime? GratisTot { get; set; }

        [XmlElement("MilePackageAmount")]
        public int? MilePackageAmount { get; set; }

        [XmlElement("MinAantalDagen")]
        public int MinAantalDagen { get; set; }

        [XmlElement("MaxAantalDagen")]
        public int MaxAantalDagen { get; set; }

        [XmlElement("IsAantal")]
        public bool IsAantal { get; set; }

        [XmlElement("MaxAantal")]
        public int? MaxAantal { get; set; }

        [XmlElement("Aantal")]
        public int? Aantal { get; set; }

        [XmlElement("Image")]
        public string Image { get; set; }

        [DefaultValue(1)]
        public int Number { get; set; }

        public bool IsCheck { get; set; }

        public static PakketModel Parse(Pakket pakketEntity)
        {
            var pakket = new PakketModel
            {
                Id = pakketEntity.Id,
                IsStandaardAan = pakketEntity.IsStandaardAan,
                Naam = pakketEntity.Naam,
                UitgebreideOmschrijving = pakketEntity.UitgebreideOmschrijving,
                IsPerPersoon = pakketEntity.IsPerPersoon,
                Prijs = pakketEntity.Prijs,
                Valuta = ValutaModel.Parse(pakketEntity.Valuta),
                IsLokaalTeBetalen = pakketEntity.IsLokaalTeBetalen,
                PakketType = pakketEntity.PakketType,
                IsGratis = (bool)pakketEntity.isGratis,
                GratisTot = pakketEntity.GratisTot,
                GratisVan = pakketEntity.GratisVan,
                MilePackageAmount = pakketEntity.MilePackageAmount,
                MinAantalDagen = pakketEntity.MinAantalDagen,
                MaxAantalDagen = pakketEntity.MaxAantalDagen,
                IsAantal = (bool)(pakketEntity.IsAantal ?? false),
                MaxAantal = pakketEntity.MaxAantal,
                Aantal = pakketEntity.Aantal,
                Image = pakketEntity.Image
            };
            return pakket;
        }

        public static List<PakketModel> Parse(IEnumerable<Pakket> pakketEntity)
        {
            return pakketEntity.Select(Parse).ToList();
        }
    }
}
