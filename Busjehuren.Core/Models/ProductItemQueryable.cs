using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Models
{
    public class ProductItemQueryable
    {
       public CamperAanbieding CamperAanbiedingModel { get; set; }
       public GetPrijsInformatie_Result PriceInfoModel { get; set; }
    }
}
