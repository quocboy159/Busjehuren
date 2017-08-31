using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using System.Collections.Generic;

namespace Busjehuren.Core.Services.Contract
{
    public interface IProductService
    {
        List<ProductItem> Search(OfferCriteria crit, int? aanbiedingId = null, int? vestigingId = null, Vestiging vestiging = null, int pageNr = 1, int pageSize = 5, List<int> propertyValues = null);
        LeverancierModel GetCondition(int Id);
        CamperModel GetSpecific(int Id);
        int CountSearch(OfferCriteria crit, int? aanbiedingId = null, int? vestigingId = null,
            Vestiging vestiging = null);
    }
}
