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
    [XmlRoot("CamperAanbieding")]
    public class CamperAanbiedingModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        public int CamperId { get; set; }
        [XmlElement("PeriodeVan")]
        public System.DateTime PeriodeVan { get; set; }
        [XmlElement("PeriodeTot")]
        public System.DateTime PeriodeTot { get; set; }
        [XmlElement("BasisPrijs")]
        public Nullable<decimal> BasisPrijs { get; set; }
        [XmlElement("BasisPrijs35")]
        public Nullable<decimal> BasisPrijs35 { get; set; }
        [XmlElement("BasisPrijsWeek")]
        public Nullable<decimal> BasisPrijsWeek { get; set; }
        [XmlElement("BasisPrijsWeekend")]
        public Nullable<decimal> BasisPrijsWeekend { get; set; }
        [XmlElement("BasisPrijs829")]
        public Nullable<decimal> BasisPrijs829 { get; set; }
        [XmlElement("BasisPrijs30")]
        public Nullable<decimal> BasisPrijs30 { get; set; }
        [XmlElement("InventarisPrijsPerPersoon")]
        public Nullable<decimal> InventarisPrijsPerPersoon { get; set; }
        [XmlElement("InventarisPrijs")]
        public Nullable<decimal> InventarisPrijs { get; set; }
        [XmlElement("IsActive")]
        public bool IsActief { get; set; }
        public Nullable<decimal> VluchtPrijsEuro { get; set; }
        public Nullable<decimal> VluchtTaxEuro { get; set; }
        [XmlElement("KortingPercOpCamper")]
        public Nullable<decimal> KortingPercOpCamper { get; set; }
        [XmlElement("KortingPercOpTotaal")]
        public Nullable<decimal> KortingPercOpTotaal { get; set; }
        public Nullable<decimal> KortingOpVluchtEuro { get; set; }
        [XmlElement("VastePrijsEuro")]
        public Nullable<decimal> VastePrijsEuro { get; set; }
        [XmlElement("IsBestGeboekt")]
        public bool IsBestGeboekt { get; set; }
        [XmlElement("BestGeboektPrijsEuro")]
        public Nullable<decimal> BestGeboektPrijsEuro { get; set; }
        [XmlElement("BestGeboektDuur")]
        public Nullable<int> BestGeboektDuur { get; set; }
        [XmlElement("VanafPrijs")]
        public Nullable<decimal> VanafPrijs { get; set; }
        public int PrijsLijstId { get; set; }
        [XmlElement("DuurInDagen")]
        public int DuurInDagen { get; set; }
        [XmlElement("Camper")]
        public CamperModel Camper { get; set; }
        public List<VestigingModel> Vestigings { get; set; }
        [XmlElement("PrijsInformatie")]
        public PriceInfoModel PrijsInformatie
        {
            get;
            set;
        }
        public static List<CamperAanbiedingModel> Parse(IEnumerable<CamperAanbieding> aanbiedingEntity, string currentLocation, bool incEigenschappen = true, bool incBestanden = true, bool incBedden = true, bool incCamper = true)
        {
            return aanbiedingEntity.Select(p => Parse(p, currentLocation, incCamper: incCamper, incBedden: incBedden, incBestanden: incBestanden, incEigenschappen: incEigenschappen)).ToList();
        }

        public static CamperAanbiedingModel Parse(CamperAanbieding aanbiedingEntity,string currentLocation, bool incEigenschappen = true, bool incBestanden = true, bool incBedden = true, bool incVestigingen = false, bool incCamper = true)
        {
            var timespan = aanbiedingEntity.PeriodeTot - aanbiedingEntity.PeriodeVan;

            var aanbieding = new CamperAanbiedingModel
            {
                Id = aanbiedingEntity.Id,
                BasisPrijs = aanbiedingEntity.BasisPrijs,
                BestGeboektPrijsEuro = aanbiedingEntity.BestGeboektPrijsEuro,
                Camper = incCamper ? CamperModel.Parse(aanbiedingEntity.Camper, currentLocation, incEigenschappen, incBestanden, incBedden) : null,
                InventarisPrijs = aanbiedingEntity.InventarisPrijs,
                InventarisPrijsPerPersoon = aanbiedingEntity.InventarisPrijsPerPersoon,
                IsActief = aanbiedingEntity.IsActief,
                IsBestGeboekt = aanbiedingEntity.IsBestGeboekt,
                BestGeboektDuur = aanbiedingEntity.BestGeboektDuur,
                KortingPercOpCamper = aanbiedingEntity.KortingPercOpCamper,
                KortingPercOpTotaal = aanbiedingEntity.KortingPercOpTotaal,
                PeriodeTot = aanbiedingEntity.PeriodeTot,
                PeriodeVan = aanbiedingEntity.PeriodeVan,
                DuurInDagen = (int)Math.Ceiling(timespan.TotalDays),
                VastePrijsEuro = aanbiedingEntity.VastePrijsEuro,
                BasisPrijs35 = aanbiedingEntity.BasisPrijs35,
                BasisPrijsWeek = aanbiedingEntity.BasisPrijsWeek,
                BasisPrijsWeekend = aanbiedingEntity.BasisPrijsWeekend,
                BasisPrijs829 = aanbiedingEntity.BasisPrijs829,
                BasisPrijs30 = aanbiedingEntity.BasisPrijs30,
                VanafPrijs = aanbiedingEntity.VanafPrijs,
                Vestigings = incVestigingen ? VestigingModel.Parse(aanbiedingEntity.Vestigings, currentLocation, new List<DestinationModel>()) : null
            };

            return aanbieding;
        }
        public static List<CamperAanbiedingModel> Parse(IEnumerable<CamperAanbiedingMetPrijzen> aanbiedingEntity,string currentLocation, bool incEigenschappen = true, bool incBestanden = true, bool incBedden = true)
        {
            return aanbiedingEntity.Select(p => Parse(p, currentLocation, incBedden: incBedden, incBestanden: incBestanden, incEigenschappen: incEigenschappen)).ToList();
        }

        public static CamperAanbiedingModel Parse(CamperAanbiedingMetPrijzen aanbiedingMetPrijzenEntity, string currentLocation, bool incEigenschappen = true, bool incBestanden = true, bool incBedden = true)
        {

            var aanbieding = Parse(aanbiedingMetPrijzenEntity.Aanbieding, currentLocation, incBedden: incBedden, incEigenschappen: incEigenschappen,
                  incBestanden: incBedden);


            aanbieding.PrijsInformatie = PriceInfoModel.Parse(aanbiedingMetPrijzenEntity.Prijs);
            return aanbieding;

        }
    }
}
