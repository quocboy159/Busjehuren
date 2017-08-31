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
    [XmlRoot("Camper")]
    public class CamperModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Naam")]
        public string Naam { get; set; }

        [XmlElement("UrlName")]
        public string UrlName { get; set; }

        [XmlElement("Lengte")]
        public decimal Lengte { get; set; }

        [XmlElement("Breedte")]
        public decimal Breedte { get; set; }

        [XmlElement("Hoogte")]
        public decimal Hoogte { get; set; }

        [XmlElement("InterieurHoogte")]
        public decimal InterieurHoogte { get; set; }

        [XmlElement("MaxAantalPersonen")]
        public int MaxAantalPersonen { get; set; }

        [XmlElement("MinAantalPersonen")]
        public int MinAantalPersonen { get; set; }

        [XmlElement("MaxAantalVolwassenen")]
        public int MaxAantalVolwassenen { get; set; }

        [XmlElement("MaxAantalKinderen")]
        public int MaxAantalKinderen { get; set; }

        [XmlElement("LaadHoogte")]
        public int LaadHoogte { get; set; }

        [XmlElement("LaadBreedte")]
        public int LaadBreedte { get; set; }

        [XmlElement("LaadLengte")]
        public int LaadLengte { get; set; }

        [XmlElement("LaadVermogen")]
        public int LaadVermogen { get; set; }

        [XmlElement("LaadRuimte")]
        public int LaadRuimte { get; set; }

        [XmlElement("BrandstofCapaciteit")]
        public int BrandstofCapaciteit { get; set; }

        [XmlElement("BrandstofVerbruik")]
        public decimal BrandstofVerbruik { get; set; }

        [XmlElement("WaterCapaciteit")]
        public int WaterCapacteit { get; set; }

        [XmlElement("Status")]
        public int Status { get; set; }

        [XmlElement("DagIndelingBestandId")]
        public int DagIndelingBestandId { get; set; }

        [XmlElement("NachtIndelingBestandId")]
        public int NachtIndelingBestandId { get; set; }

        [XmlElement("Video")]
        public string Video { get; set; }

        [XmlElement("Commissie")]
        public decimal? Commissie { get; set; }

        [XmlElement("CamperKorting")]
        public decimal? CamperKorting { get; set; }

        [XmlElement("Leverancier")]
        public LeverancierModel Leverancier { get; set; }

        [XmlElement("ExternalId")]
        public String ExternalId { get; set; }

        [XmlElement("Classification")]
        public int Classification { get; set; }
        public List<VestigingModel> Vestigingen { get; set; }

        [XmlArray("EigenschapWaarden")]
        [XmlArrayItem("EigenschapWaarde")]
        public List<PropertyDetailModel> EigenschapWaarden { get; set; }

        public List<CamperBedModel> CamperBedden { get; set; }

        public List<CamperBestandModel> CamperBestanden { get; set; }

        public List<CamperAanbiedingModel> CamperAanbiedingen { get; set; }

        public static List<CamperModel> Parse(IEnumerable<Camper> campersEntity, string currentLocation)
        {
            return campersEntity.Select(p => Parse(p, currentLocation)).ToList();
        }

        public static CamperModel Parse(Camper camperEntity,string currentLocation, bool incEigenschappen = true, bool incBestanden = true, bool incBedden = true, bool incAanbiedingen = false, bool showInActive = false)
        {
            List<PropertyDetailModel> eigenschapWaarden = null;

            if (incEigenschappen)
            {
                //In de backoffice moeten de waardes van de Inactieve eigenschappen wel worden opgehaalde.
                if (showInActive)
                {
                    eigenschapWaarden =
                        camperEntity.CamperEigenschappens.Distinct().Where(ce => ce.Eigenschap.Status != 3).Select(
                            e => PropertyDetailModel.Parse(e.EigenschapWaarde)).ToList();
                }
                else
                {
                    //En niet in de frontend.
                    eigenschapWaarden =
                        camperEntity.CamperEigenschappens.Distinct().Where(ce => ce.Eigenschap.Status == 1).Select(
                            e => PropertyDetailModel.Parse(e.EigenschapWaarde)).ToList();
                }

            }

            var camper = new CamperModel
            {
                Id = camperEntity.Id,
                Naam = camperEntity.Naam,
                UrlName = camperEntity.UrlName,
                Breedte = camperEntity.Breedte,
                Lengte = camperEntity.Lengte,
                Hoogte = camperEntity.Hoogte,
                InterieurHoogte = camperEntity.InterieurHoogte,
                EigenschapWaarden = eigenschapWaarden,
                //Leverancier = LeverancierModel.Parse(camperEntity.Leverancier, currentLocation, true),
                Vestigingen = VestigingModel.Parse(camperEntity.Leverancier.Vestigings, currentLocation),
                BrandstofCapaciteit = camperEntity.BrandstofCapaciteit,
                BrandstofVerbruik = camperEntity.BrandstofVerbruik,
                MaxAantalVolwassenen = camperEntity.MaxAantVolwassenen,
                MaxAantalKinderen = camperEntity.MaxAantKinderen,
                MaxAantalPersonen = camperEntity.MaxAantPersonen,
                MinAantalPersonen = camperEntity.MinAantPersonen,
                LaadHoogte = (int)camperEntity.LaadHoogte,
                LaadBreedte = (int)camperEntity.LaadBreedte,
                LaadLengte = (int)camperEntity.LaadLengte,
                LaadRuimte = (int)camperEntity.LaadRuimte,
                LaadVermogen = (int)camperEntity.LaadVermogen,
                Status = camperEntity.Status,
                WaterCapacteit = camperEntity.WaterCapacteit,
                Commissie = camperEntity.Commissie,
                CamperKorting = camperEntity.CamperKorting,
                CamperBedden = incBedden ? CamperBedModel.Parse(camperEntity.CamperBeds) : null,
                CamperBestanden = incBestanden ? CamperBestandModel.Parse(camperEntity.CamperBestandens.OrderBy(cb => cb.Volgorde)) : null,
                DagIndelingBestandId = camperEntity.DagIndelingBestandId,
                NachtIndelingBestandId = camperEntity.NachtIndelingBestandId,
                Video = camperEntity.Video,
                CamperAanbiedingen = incAanbiedingen ? CamperAanbiedingModel.Parse(camperEntity.CamperAanbiedings, currentLocation, incCamper: false) : null,
                ExternalId = camperEntity.ExternalId,
                Classification = (int)camperEntity.Classification
            };

            return camper;
        }
    }
}
