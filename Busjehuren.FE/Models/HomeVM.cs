using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Busjehuren.FE.Models
{
    public class HomeVM
    {
        public ContentModel ContentModel { get; set; }
        public ContentModel PromoModel { get; set; }
        public List<DestinationModel> Destinations { get; set; }
        public List<FaqModel> Faqs { get; set; }
    }
}