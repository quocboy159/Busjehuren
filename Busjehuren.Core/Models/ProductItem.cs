using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Models
{
   public class ProductItem
    {
       public CamperAanbiedingModel CamperAanbiedingModel { get; set; }
       public PriceInfoModel PriceInfoModel { get; set; }
       public int Total { get; set; }
    }
}
