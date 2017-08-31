using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Busjehuren.FE.Models
{
    public class FilterFormVM
    {
        public List<DestinationModel> Destinations { get; set; }

        public DestinationModel PickUpCity { get; set; }

        public DestinationModel DropOffCity { get; set; }

        public List<PropertyDetailModel> PropertyDetails { get; set; }

        public int CarType { get; set; }

        [Required(ErrorMessage = "Kies een datum")]
        public string StartDateString { get; set; }

        public string StartDate { get; set; }

        [Required(ErrorMessage = "Kies een datum")]
        public string EndDateString { get; set; }

        public string EndDate { get; set; }

        [Required(ErrorMessage = "Kies een tijd")]
        public string PickUpTimeString { get; set; }

        public string PickUpTime { get; set; }

        [Required(ErrorMessage = "Kies een tijd")]
        public string DropOffTimeString { get; set; }

        public string DropOffTime { get; set; }

        public DestinationModel GetCity(int cityId)
        {
            return (from country in Destinations
                    from city in country.Cities
                    where city.Id == cityId
                    select city).SingleOrDefault();
        }

        public List<SelectListItem> PickupOpeningHour { get; set; }
        public List<SelectListItem> DropOffOpeningHour { get; set; }
        public List<SelectListItem> DestinationSelectItem { get; set; }
    }
}