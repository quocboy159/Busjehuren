namespace Busjehuren.FE.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Busjehuren.Core.Models;
    using Busjehuren.FE.Models;
    using Busjehuren.Core.Services.Contract;
    using System.Globalization;
    using Newtonsoft.Json;

    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        private readonly IContentService _contentService;

        public LocationController(ILocationService locationService, IContentService contentService)
        {
            this._locationService = locationService;
            this._contentService = contentService;
        }

        [Route("vestigingen")]
        public ActionResult Index()
        {
            var model = new LocationVM();
            model.Locations = _locationService.GetLocations();
            model.Content = _contentService.GetByAlias("rental-companies");
            model.Destinations = _locationService.GetAllDestination();

            return View(model);
        }

        [Route("GetLocationById")]
        public ActionResult GetVestigingOpeningHourByLocationId(int Id, string date)
        {
            object[] item = _locationService.GetVestigingOpeningHourByLocationId(Id, DateTime.ParseExact(date, GlobalStatic.DateFormatString, CultureInfo.InvariantCulture));

            if (item != null)
            {
                var minTime = new TimeVM();
                var maxTime = new TimeVM();

                int tempTryResult = 0;

                if (item[0] != null)
                {
                    minTime.Hour = int.TryParse(item[0].ToString().Split('.')[0], out tempTryResult) ? tempTryResult : 0;
                    minTime.Minute = int.TryParse(item[0].ToString().Split('.')[1], out tempTryResult) ? tempTryResult : 0;
                }
                else
                {
                    minTime = null;
                }

                if (item[1] != null)
                {
                    maxTime.Hour = int.TryParse(item[1].ToString().Split('.')[0], out tempTryResult) ? tempTryResult : 0;
                    maxTime.Minute = int.TryParse(item[1].ToString().Split('.')[1], out tempTryResult) ? tempTryResult : 0;
                }
                else
                {
                    maxTime = null;
                }

                return Json(new { minTime, maxTime, availableDates = item[2] }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _FilterLocationForm()
        {
            var model = new FilterFormVM();

            model.Destinations = _locationService.GetAllDestination();
            var cities = new List<SelectListItem>();
            foreach (var destination in model.Destinations)
            {
                cities.AddRange(destination.Cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.DisplayName }).ToList());
            }
            model.DestinationSelectItem = cities.OrderBy(c => c.Text).ToList();

            model.CarType = (int)BookingData.OfferCriteria.Busjetype == 0 ? 10 : (int)BookingData.OfferCriteria.Busjetype;
            model.PickUpCity = model.GetCity(BookingData.OfferCriteria.OphaalBestemmingId);
            model.DropOffCity = model.GetCity(BookingData.OfferCriteria.TerugbrengBestemmingId);

            DateTime startDate = BookingData.OfferCriteria.StartDate;
            if (startDate.Ticks != 0)
            {
                model.StartDate = startDate.ToString(GlobalStatic.DateFormatString);
                model.StartDateString = startDate.ToString(GlobalStatic.DateFormatString);
                model.PickUpTime = startDate.ToString(GlobalStatic.TimeFormatString);
                model.PickUpTimeString = startDate.ToString(GlobalStatic.TimeFormatString);
            }

            DateTime endDate = BookingData.OfferCriteria.EndDate; //BookingData.OfferCriteria.EndDate.Ticks == 0 ? DateTime.Now.AddDays(7) : BookingData.OfferCriteria.EndDate;
            if (endDate.Ticks != 0)
            {
                model.EndDate = endDate.ToString(GlobalStatic.DateFormatString);
                model.EndDateString = endDate.ToString(GlobalStatic.DateFormatString);
                model.DropOffTime = endDate.ToString(GlobalStatic.TimeFormatString);
                model.DropOffTimeString = endDate.ToString(GlobalStatic.TimeFormatString);
            }

            model.PickupOpeningHour =
                GetOpeningHour(BookingData.OfferCriteria.OphaalBestemmingId,
                    string.IsNullOrEmpty(model.StartDateString) ? string.Empty : model.StartDateString);
            model.DropOffOpeningHour =
                GetOpeningHour(BookingData.OfferCriteria.TerugbrengBestemmingId,
                    string.IsNullOrEmpty(model.EndDateString) ? string.Empty : model.EndDateString);

            return PartialView(model);
        }

        private List<SelectListItem> GetOpeningHour(int Id, string date)
        {
            var itemList = new List<SelectListItem>();
            var start = DateTime.Now;
            TimeSpan tsStart;
            var end = DateTime.Now;
            TimeSpan tsEnd;

            if (Id != 0 && !string.IsNullOrEmpty(date))
            {
                object[] openingHour =
                    _locationService.GetVestigingOpeningHourByLocationId(Id, DateTime.ParseExact(date, GlobalStatic.DateFormatString, CultureInfo.InvariantCulture));
                if (openingHour != null)
                {
                    tsStart = new TimeSpan(int.Parse(openingHour[0].ToString().Split('.')[0]), int.Parse(openingHour[0].ToString().Split('.')[1]), 0);
                    start = start.Date + tsStart;


                    tsEnd = new TimeSpan(int.Parse(openingHour[1].ToString().Split('.')[0]), int.Parse(openingHour[1].ToString().Split('.')[1]), 0);
                    end = end.Date + tsEnd;

                    for (var i = start; i.TimeOfDay <= end.TimeOfDay; i = i.AddMinutes(30))
                    {
                        var item = new SelectListItem { Text = i.ToString("HH:mm"), Value = i.ToString(GlobalStatic.TimeFormatString) };
                        itemList.Add(item);
                    }
                    return itemList;
                }
            }
            tsStart = new TimeSpan(8, 0, 0);
            start = start.Date + tsStart;

            tsEnd = new TimeSpan(17, 30, 0);
            end = end.Date + tsEnd;

            for (var i = start; i.TimeOfDay <= end.TimeOfDay; i = i.AddMinutes(30))
            {
                var item = new SelectListItem { Text = i.ToString("HH:mm"), Value = i.ToString(GlobalStatic.TimeFormatString) };
                itemList.Add(item);
            }

            return itemList;
        }
    }
}