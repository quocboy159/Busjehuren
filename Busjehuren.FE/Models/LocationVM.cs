using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Busjehuren.Core.Models;

namespace Busjehuren.FE.Models
{
    public class LocationVM
    {
        public List<VestigingModel> Locations { get; set; }
        public ContentModel Content { get; set; }
        public List<DestinationModel> Destinations { get; set; }
    }
}