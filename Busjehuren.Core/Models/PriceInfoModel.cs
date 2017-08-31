using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Models
{
    public class PriceInfoModel
    {
        public int CamperAanbiedingId { get; set; }

        public int VestigingId { get; set; }

        public int BestemmingId { get; set; }

        public int? CamperVluchtId { get; set; }

        public decimal CamperPrijsPerPersoon { get; set; }

        public decimal? VluchtPrijsPerVolwassene { get; set; }

        public decimal? VluchtPrijsPerKind { get; set; }

        public decimal TotaalPakketPrijs { get; set; }

        public decimal TotaalPakketPrijsPerPersoon { get; set; }

        public decimal? PrijsVanaf { get; set; }

        public int ParentBestemmingId { get; set; }

        public decimal LongHireDiscount { get; set; }
        public decimal VroegboekKorting { get; set; }
        public static List<PriceInfoModel> Parse(IEnumerable<GetPrijsInformatie_Result> prijsinformatieEntity)
        {
            return prijsinformatieEntity.Select(Parse).ToList();
        }


        public static PriceInfoModel Parse(GetPrijsInformatie_Result prijsinformatieEntity)
        {
            //Exclude the flight price
            var prijsinformatie = new PriceInfoModel
            {
                CamperAanbiedingId = prijsinformatieEntity.CamperAanbiedingId,
                VestigingId = prijsinformatieEntity.VestigingId,
                BestemmingId = prijsinformatieEntity.BestemmingId,
                ParentBestemmingId = prijsinformatieEntity.ParentBestemmingId,
                //CamperVluchtId = prijsinformatieEntity.CamperVluchtId,
                CamperPrijsPerPersoon = prijsinformatieEntity.CamperPrijsPerPersoon.Value,
                VluchtPrijsPerVolwassene = 0,
                //prijsinformatieEntity.VluchtPrijsPerVolwassene,
                VluchtPrijsPerKind = 0,
                //prijsinformatieEntity.VluchtPrijsPerKind,
                TotaalPakketPrijs = prijsinformatieEntity.TotaalPakketPrijs.Value,
                TotaalPakketPrijsPerPersoon = prijsinformatieEntity.TotaalPakketPrijsPerPersoon.Value,
                PrijsVanaf = prijsinformatieEntity.PrijsVanaf,
                LongHireDiscount = 0,//prijsinformatieEntity.LongHireDiscount ?? 0,
                VroegboekKorting = 0 //prijsinformatieEntity.VroegboekKorting ?? 0
            };
            return prijsinformatie;
        }
    }
}
