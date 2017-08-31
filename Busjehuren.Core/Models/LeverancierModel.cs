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
    [XmlRoot("Leverancier")]
    public class LeverancierModel
    {
        public LeverancierModel()
        {
            Vestigingen = new List<VestigingModel>();
            Longhires = new List<LonghireModel>();
        }

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Naam")]
        public string Naam { get; set; }

        [XmlElement("Status")]
        public int Status { get; set; }

        [XmlElement("Commissie")]
        public decimal? Commissie { get; set; }

        [XmlElement("Korting")]
        public decimal? Korting { get; set; }
        
        [XmlElement("PrijsPerPeriode")]
        public bool? PrijsPerPeriode { get; set; }

        [XmlElement("AddOneExtraDay")]
        public bool? AddOneExtraDay { get; set; }
        
        [XmlElement("Huurvoorwaarden")]
        public string Huurvoorwaarden { get; set; }

        [XmlElement("Openingstijden")]
        public string Openingstijden { get; set; }

        [XmlElement("Ophaalinformatie")]
        public string OphaalInformatie { get; set; }

        [XmlElement("Inleverinformatie")]
        public string InleverInformatie { get; set; }

        [XmlElement("Transferinformatie")]
        public string TransferInformatie { get; set; }

        [XmlElement("Prijsvoorwaarden")]
        public string Prijsvoorwaarden { get; set; }

        [XmlElement("Bestand")]
        public FileModel Bestand { get; set; }


        [XmlElement("Vroegboekkorting")]
        public bool? Vroegboekkorting { get; set; }

        [XmlElement("Vbkpercentage")]
        public decimal? Vbkpercentage { get; set; }

        [XmlElement("Vbkdagen")]
        public int? Vbkdagen { get; set; }

        [XmlElement("VroegboekPeriodeStart")]
        public DateTime? VroegboekPeriodeStart { get; set; }

        [XmlElement("VroegboekPeriodeEind")]
        public DateTime? VroegboekPeriodeEind { get; set; }

        [XmlArray("Vestigingen")]
        [XmlArrayItem("Vestiging")]
        public List<VestigingModel> Vestigingen { get; set; }

        public List<LonghireModel> Longhires { get; set; }

        public static LeverancierModel Parse(Leverancier leverancierEntity,string currentLocation, bool incVestigingen, bool incLonghires = true)
        {
            var leverancier = new LeverancierModel
            {
                Id = leverancierEntity.Id,
                Naam = leverancierEntity.Naam,
                Status = leverancierEntity.Status,
                Commissie = leverancierEntity.Commissie,
                Bestand = FileModel.Parse(leverancierEntity.Bestand),
                Vestigingen = incVestigingen ? VestigingModel.Parse(leverancierEntity.Vestigings, currentLocation) : null,
                Longhires = incLonghires ? LonghireModel.Parse(leverancierEntity.Longhires) : null,
                Huurvoorwaarden = leverancierEntity.Huurvoorwaarden,
                InleverInformatie = leverancierEntity.Inleverinformatie,
                Openingstijden = leverancierEntity.Openingstijden,
                OphaalInformatie = leverancierEntity.Ophaalinformatie,
                Prijsvoorwaarden = leverancierEntity.Prijsvoorwaarden,
                TransferInformatie = leverancierEntity.Transferinformatie,
                Korting = leverancierEntity.Korting,
                PrijsPerPeriode = leverancierEntity.PrijsPerPeriode,
                AddOneExtraDay = leverancierEntity.AddOneExtraDay,
                Vroegboekkorting = leverancierEntity.Vroegboekkorting,
                Vbkpercentage = leverancierEntity.Vbkpercentage,
                Vbkdagen = leverancierEntity.Vbkdagen,
                VroegboekPeriodeStart = leverancierEntity.VroegboekPeriodeStart,
                VroegboekPeriodeEind = leverancierEntity.VroegboekPeriodeEind
            };

            return leverancier;
        }

        public static List<LeverancierModel> Parse(IEnumerable<Leverancier> leveranciersEntity,string currentLocation, bool incVestigingen)
        {
            return leveranciersEntity.Select(leverancier => Parse(leverancier,currentLocation, incVestigingen)).ToList();
        }
    }
}
