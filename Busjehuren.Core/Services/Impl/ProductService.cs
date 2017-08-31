using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Enums;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Services.Impl
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }
        /// <summary>
        /// Create a query search
        /// </summary>
        /// <param name="crit">all criterial to search</param>
        /// <param name="aanbiedingId">Service id</param>
        /// <param name="vestigingId">location id</param>
        /// <param name="vestiging">loction</param>
        /// <param name="propertyValues">list of property value</param>
        /// <returns></returns>
        private IQueryable<ProductItemQueryable> QuerySearch(OfferCriteria crit, int? aanbiedingId = null,
            int? vestigingId = null, Vestiging vestiging = null, List<int> propertyValues = null)
        {
            if (crit.StartDate == DateTime.MinValue)
            {
                crit.StartDate = DateTime.Now;
                crit.EndDate = crit.StartDate.AddDays(7);
            }

            var aanbiedingen = _unitOfWork.CamperAanbiedingRepository.FindAll(aanbieding => aanbieding.IsActief && aanbieding.Camper.Leverancier.Status == 1);

            if (aanbiedingId != null)
            {
                aanbiedingen = aanbiedingen.Where(a => a.Id == (int)aanbiedingId);
            }

            var ophaalBestemming = crit.OphaalBestemmingId;
            if (vestiging != null)
            {
                vestigingId = vestiging.Id;
                ophaalBestemming = vestiging.BestemmingId;
            }

            if (vestigingId != null)
            {
                aanbiedingen = aanbiedingen.Where(a => a.Vestigings.Select(v => v.Id).Contains((int)vestigingId));
            }

            if (aanbiedingId == null && vestigingId == null)
            {
                //if (crit.EigenschapWaarden != null && crit.EigenschapWaarden.Count > 0)
                //{

                //    var offerEigenschapWaardens = crit.EigenschapWaarden.Select(e => e.Id);
                //    var eigenschapwaardeIds = _unitOfWork.EigenschapWaardeRepository
                //                                            .FindAll(ew => offerEigenschapWaardens.Contains(ew.Id)).Select(x => x.Id);

                //    // filter op aanbiedingen waarvan alle crit.Eigenschapwaarden voorkomen in de campereigenschappen.
                //    aanbiedingen = aanbiedingen.Where(
                //    aanbieding => eigenschapwaardeIds.All(eigenschapwaardeId =>
                //                                          aanbieding.Camper.CamperEigenschappens.Select(
                //                                              camperEigenschapwaardeId =>
                //                                              camperEigenschapwaardeId.EigenschapWaardeId).
                //                                              Contains(eigenschapwaardeId))
                //    );
                //}

                if (propertyValues != null)
                {
                    var eigenschapwaardeIds = _unitOfWork.EigenschapWaardeRepository
                                                            .FindAll(ew => propertyValues.Contains(ew.Id)).Select(x => x.Id);

                    // filter op aanbiedingen waarvan alle crit.Eigenschapwaarden voorkomen in de campereigenschappen.
                    aanbiedingen = aanbiedingen.Where(
                    aanbieding => eigenschapwaardeIds.All(eigenschapwaardeId =>
                                                          aanbieding.Camper.CamperEigenschappens.Select(
                                                              camperEigenschapwaardeId =>
                                                              camperEigenschapwaardeId.EigenschapWaardeId).
                                                              Contains(eigenschapwaardeId))
                    );
                }
            }

            // Join result met prijs
            //IQueryable<CamperAanbiedingMetPrijzen> result;
            if (ophaalBestemming == 0)
            {
                ophaalBestemming = crit.ZoekBestemmingId;
            }


            int returnDestinationId = (crit.TerugbrengBestemmingId > 0 && crit.TerugbrengBestemmingId != crit.OphaalBestemmingId) ? crit.TerugbrengBestemmingId : ophaalBestemming;

            var resultaat = aanbiedingen.Join(_unitOfWork.context.GetPrijsInformatie(ophaalBestemming, crit.NumberOfAdults, crit.NumberOfChildrens, crit.StartDate, crit.EndDate, crit.ReserveringsDatum, returnDestinationId),
                                aanbieding => aanbieding.Id, prijs => prijs.CamperAanbiedingId,
                                (aanbieding, prijs) => new { aanbieding, prijs });



            if (aanbiedingId == null && vestigingId == null)
            {
                resultaat = resultaat.Where(a => a.prijs.TotaalPakketPrijsPerPersoon >= crit.MinPrijs && a.prijs.TotaalPakketPrijsPerPersoon <= crit.MaxPrijs);
            }

            // 12972-25610 - Camper classification
            resultaat = crit.Busjetype == BusjeType.Personenbus ? resultaat.Where(c => c.aanbieding.Camper.Classification == (int)BusjeType.Personenbus)
                                                                : resultaat.Where(c => c.aanbieding.Camper.Classification == (int)BusjeType.BestelBusje
                                                                                       || c.aanbieding.Camper.Classification == (int)BusjeType.VerhuisWagen);

            // 12972-24136 - Check locations in case of one way rental
            if (crit.TerugbrengBestemmingId > 0 && crit.TerugbrengBestemmingId != crit.OphaalBestemmingId)
            {
                var suppliers = _unitOfWork.CamperAanbiedingRepository
                    .FindAll(ca => ca.Vestigings
                        .Where(v => v.Status == 1)
                        .Select(v => v.BestemmingId)
                        .Contains(crit.TerugbrengBestemmingId))
                    .Select(c => c.Camper.LeverancierId).Distinct();

                resultaat = resultaat.Where(a => suppliers.Contains(a.aanbieding.Camper.LeverancierId));
            }

            // Sort
            switch (crit.Sortering.ToLower())
            {
                case "bestgeboekt":
                    resultaat = resultaat.OrderBy(a => a.aanbieding.IsBestGeboekt).ThenBy(a => a.prijs.TotaalPakketPrijsPerPersoon);
                    break;
                default:
                    resultaat = resultaat.OrderBy(a => a.prijs.TotaalPakketPrijsPerPersoon);
                    break;

            }

            return resultaat.Select(p => new ProductItemQueryable() { CamperAanbiedingModel = p.aanbieding, PriceInfoModel = p.prijs });
        }

        public List<ProductItem> Search(OfferCriteria crit, int? aanbiedingId = null, int? vestigingId = null, Vestiging vestiging = null, int pageNr = 1, int pageSize = 5, List<int> propertyValues = null)
        {
            var list = QuerySearch(crit, aanbiedingId, vestigingId, vestiging, propertyValues);
            int total = list.Count();

            return list.ToList().Select(s => new ProductItem()
            {
                CamperAanbiedingModel = CamperAanbiedingModel.Parse(s.CamperAanbiedingModel, ""),
                PriceInfoModel = PriceInfoModel.Parse(s.PriceInfoModel),
                Total = total
            }).ToList();
        }

        public int CountSearch(OfferCriteria crit, int? aanbiedingId = null, int? vestigingId = null, Vestiging vestiging = null)
        {
            return QuerySearch(crit, aanbiedingId, vestigingId, vestiging).Count();
        }

        public LeverancierModel GetCondition(int Id)
        {
            Leverancier result = _unitOfWork.CamperAanbiedingRepository.FindAll(t => t.Id == Id && t.Camper.Leverancier.Status == 1).Select(t => t.Camper.Leverancier).FirstOrDefault();
            return LeverancierModel.Parse(result, "", true);
        }

        public CamperModel GetSpecific(int Id)
        {
            Camper result = _unitOfWork.CamperAanbiedingRepository.FindAll(t => t.Id == Id && t.Camper.Leverancier.Status == 1).Select(t => t.Camper).FirstOrDefault();
            return CamperModel.Parse(result, "");
        }
    }
}
