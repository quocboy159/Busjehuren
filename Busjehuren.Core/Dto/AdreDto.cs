using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class AdreDto
    {
         public int Id { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public Nullable<int> LandId { get; set; }
        public string Telefoonnummer { get; set; }
        public string Faxnummer { get; set; }
        public string Mobielnummer { get; set; }
        
    }
}
