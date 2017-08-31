using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Models
{
    public class CamperAanbiedingMetPrijzen
    {
        public CamperAanbieding Aanbieding { get; set; }
        public GetPrijsInformatie_Result Prijs { get; set; }

        public CamperAanbiedingMetPrijzen(CamperAanbieding aanbieding, GetPrijsInformatie_Result prijs)
        {
            Aanbieding = aanbieding;
            Prijs = prijs;
        }


    }
}
