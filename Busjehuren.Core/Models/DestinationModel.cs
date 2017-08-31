using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Busjehuren.Core.Models
{
    public class DestinationModel
    {
        [Required(ErrorMessage = "Kies een locatie")]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required(ErrorMessage = "Kies een locatie")]
        public string DisplayName { get; set; }

        public string UrlName { get; set; }

        public List<DestinationModel> Cities { get; set; }
    }
}
