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
    [XmlRoot("Vestiging")]
    public class VestigingModel
    {
        public VestigingModel()
        {
            Pakkets = new List<PakketModel>();
            VestigingOpeningHours = new List<VestigingOpeningHourModel>();
        }
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Naam")]
        public string Naam { get; set; }

        [XmlElement("Status")]
        public int Status { get; set; }

        [XmlElement("Bestemming")]
        public BestemmingModel Destination { get; set; }

        [XmlElement("ZoomLevel")]
        public int ZoomLevel { get; set; }

        [XmlElement("Latitude")]
        public double Latitude { get; set; }

        [XmlElement("Longitude")]
        public double Longtitude { get; set; }

        [XmlElement("Korting")]
        public decimal? Korting { get; set; }

        [XmlElement("Tax")]
        public decimal? Tax { get; set; }

        [XmlElement("Adres")]
        public AdresModel Adres { get; set; }

        [XmlElement("Leverancier")]
        public LeverancierModel Leverancier { get; set; }

        [XmlElement("Valuta")]
        public ValutaModel Valuta { get; set; }

        [XmlElement("ExternalId")]
        public string ExternalId { get; set; }
        [XmlElement("Pakketten")]
        public List<PakketModel> Pakkets { get; set; }

        public List<VestigingOpeningHourModel> VestigingOpeningHours { get; set; }

        public string Content { get; set; }

        public string UrlNameParent { get; set; }

        public string UrlName { get; set; }

        public static List<VestigingModel> Parse(IEnumerable<Vestiging> vestigingEntity, string currentLocation, List<DestinationModel> destinations = null)
        {
            return vestigingEntity.Select(p => Parse(new OptionParseVestigingModels() { vestigingEntity = p, currentLocation = currentLocation, destinations = destinations ?? new List<DestinationModel>() })).ToList();
        }

        public static VestigingModel Parse(OptionParseVestigingModels option)
        {
            var vestiging = new VestigingModel
            {
                Id = option.vestigingEntity.Id,
                Adres = option.incAdres ? AdresModel.Parse(option.vestigingEntity.Adre, option.currentLocation) : null,
                Destination = BestemmingModel.Parse(option.vestigingEntity.Destination, option.currentLocation),
                Latitude = option.vestigingEntity.Latitude,
                Longtitude = option.vestigingEntity.Longtitude,
                Naam = option.vestigingEntity.Naam,
                Pakkets = option.incPakketten ? PakketModel.Parse(option.vestigingEntity.Pakkets) : null,
                ZoomLevel = option.vestigingEntity.ZoomLevel,
                Korting = option.vestigingEntity.Korting,
                Tax = option.vestigingEntity.Tax,
                Status = option.vestigingEntity.Status,
                Valuta = ValutaModel.Parse(option.vestigingEntity.Valuta),
                Leverancier = option.incLeverancier ? LeverancierModel.Parse(option.vestigingEntity.Leverancier, option.currentLocation, false) : null,
                ExternalId = option.vestigingEntity.ExternalId,
                VestigingOpeningHours = option.incVestigingOpeningHour ? VestigingOpeningHourModel.Parse(option.vestigingEntity.VestigingOpeningHours) : null,
                UrlNameParent = option.destinations.FirstOrDefault(l => l.Id == option.vestigingEntity.Destination.ParentId) != null
                                ? option.destinations.FirstOrDefault(l => l.Id == option.vestigingEntity.Destination.ParentId).DisplayName
                                : string.Empty,
                UrlName = option.destinations.FirstOrDefault(l => l.Cities.Any(c => c.Id == option.vestigingEntity.Destination.Id)) != null
                                ? option.destinations.FirstOrDefault(l => l.Cities.Any(c => c.Id == option.vestigingEntity.Destination.Id)).Cities.First(x => x.Id == option.vestigingEntity.Destination.Id).DisplayName
                                : string.Empty,
            };

            if (option.useLocalCurrency)
            {
                UpdateLocalPackagesUsingLocalCurrency(vestiging);
            }

            return vestiging;
        }

        private static void UpdateLocalPackagesUsingLocalCurrency(VestigingModel vestiging)
        {
            foreach (var pakket in vestiging.Pakkets)
            {
                if (pakket.IsLokaalTeBetalen)
                {
                    if (pakket.Valuta.Code != vestiging.Valuta.Code)
                    {
                        pakket.Prijs = pakket.Prijs / vestiging.Valuta.Koers;
                        pakket.Valuta = vestiging.Valuta;
                    }
                }
            }

        }
    }

    public class OptionParseVestigingModels
    {
        public OptionParseVestigingModels()
        {
            currentLocation = "";
            incAdres = true;
            incPakketten = true;
            incVestigingOpeningHour = true;
            destinations = new List<DestinationModel>();
        }

        public Vestiging vestigingEntity { get; set; }
        public string currentLocation { get; set; }
        public bool incAdres { get; set; }
        public bool incPakketten { get; set; }
        public bool incLeverancier { get; set; }
        public bool useLocalCurrency { get; set; }
        public bool incVestigingOpeningHour { get; set; }
        public List<DestinationModel> destinations { get; set; }
    }
}
