using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Busjehuren.FE.Models
{
    public class Checkout2VM
    {
        public string SelectedPackages { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public string LandBestemming { get; set; }
    }
}