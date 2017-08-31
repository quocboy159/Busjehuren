using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Busjehuren.Core.Models;

namespace Busjehuren.FE.Models
{
    public class ContactVM
    {
        public ContentModel Content { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geen geldig e-mailadres.")]
        [EmailAddress(ErrorMessage = "Geen geldig e-mailadres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Telefoonnummer")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Uw bericht")]
        public string Message { get; set; }
    }
}