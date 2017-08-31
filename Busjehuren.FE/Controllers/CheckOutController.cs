using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using Busjehuren.FE.Models;
using Newtonsoft.Json;
using Busjehuren.Core.Services;
using Busjehuren.Common.Extensions;
using Busjehuren.Core.Enums;
using System.Web.UI;

namespace Busjehuren.FE.Controllers
{
    public class CheckOutController : BaseController
    {
        #region declare variables

        private readonly IProductService _productService;
        private readonly ICheckOutService _checkOutService;
        private readonly IPropertyService _propertyService;
        private readonly IContentService _contentService;
        private readonly IEmailService _emailService;
        private readonly ILocationService _locationService;
        private readonly IGenericService _genericService;

        #endregion

        #region Actions

        public CheckOutController(IGenericService genericService, IProductService productService, ICheckOutService checkOutService, IPropertyService propertyService, IContentService contentService, IEmailService emailService, ILocationService locationService)
        {
            this._genericService = genericService;
            this._productService = productService;
            this._checkOutService = checkOutService;
            this._propertyService = propertyService;
            this._contentService = contentService;
            this._emailService = emailService;
            this._locationService = locationService;
        }

        [Route("{urlNameParent}/{urlName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout1(FilterFormVM model)
        {
            if (model.PickUpCity.Id == 0 || model.DropOffCity.Id == 0)
                return RedirectToAction("Index", "Home");

            SetBookingDataByFilterFormVM(model);

            SearchResultVM searchResult = new SearchResultVM();
            searchResult.Products = _productService.Search(BookingData.OfferCriteria);
            searchResult.Properties = _propertyService.GetProperties();
            searchResult.Destination = _locationService.GetLocationByBestemmingId(BookingData.OfferCriteria.OphaalBestemmingId);
            searchResult.TotalSearchResult = searchResult.Products.Any() ? searchResult.Products.First().Total : 0;
            searchResult.IsSearch = true;
            searchResult.BookingData = BookingData;
            searchResult.SelectedPropertyDetails = BookingData.OfferCriteria.EigenschapWaarden;


            var pickUpCity = BookingData.Destinations.Find(f => f.Id == BookingData.OfferCriteria.OphaalBestemmingId);
            if (pickUpCity != null)
            {
                searchResult.PickUpCityName = pickUpCity.DisplayName;
            }

            return View(searchResult);
        }

        [Route("condition")]
        public ActionResult Condition(int id)
        {
            var model = _productService.GetCondition(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Route("specific")]
        public ActionResult Specific(int id)
        {
            var model = _productService.GetSpecific(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Route("{urlNameParent}/{urlName}")]
        [HttpGet]
        public ActionResult Checkout1(string urlNameParent, string urlName)
        {
            if (!IsNotSearch)
            {
                if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.OphaalBestemmingId == 0)
                    return RedirectToAction("Index", "Home");

                SearchResultVM searchResult = new SearchResultVM();
                searchResult.Products = _productService.Search(BookingData.OfferCriteria);
                searchResult.Properties = _propertyService.GetProperties();
                searchResult.Destination = _locationService.GetLocationByBestemmingId(BookingData.OfferCriteria.OphaalBestemmingId);
                searchResult.TotalSearchResult = searchResult.Products.Any() ? searchResult.Products.First().Total : 0;
                searchResult.IsSearch = true;
                searchResult.BookingData = BookingData;
                searchResult.SelectedPropertyDetails = BookingData.OfferCriteria.EigenschapWaarden;

                var pickUpCity = BookingData.Destinations.Find(f => f.Id == BookingData.OfferCriteria.OphaalBestemmingId);
                if (pickUpCity != null)
                {
                    searchResult.PickUpCityName = pickUpCity.DisplayName;
                }

                return View(searchResult);
            }
            else
            {
                var destination = _locationService.GetDestinationByUrl(urlName);
                if (destination == null)
                    return RedirectToAction("Index", "Home");

                FilterFormVM model = new FilterFormVM();
                model.CarType = 10;

                model.PickUpCity = destination;
                var availableDays = _locationService.GetAvailableDays(model.PickUpCity.Id);
                if (availableDays.Count == 0)
                    return RedirectToAction("Index", "Home");
                DateTime defaultDate = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0).AddDays(2)).DefaultValidDate(GlobalStatic.Holidays, availableDays);


                model.PickUpTime = defaultDate.ToString("HH:mm:ss");
                model.PickUpTimeString = defaultDate.ToString("HH:mm");
                model.StartDate = defaultDate.ToString("dd-MM-yyyy");
                model.StartDateString = defaultDate.ToString("dd-MM");

                model.DropOffCity = destination;

                availableDays = _locationService.GetAvailableDays(model.DropOffCity.Id);
                if (availableDays.Count == 0)
                    return RedirectToAction("Index", "Home");
                var availableEndDate = defaultDate.AddDays(1).DefaultValidDate(GlobalStatic.Holidays, availableDays);

                model.DropOffTime = defaultDate.ToString("HH:mm:ss");
                model.DropOffTimeString = defaultDate.ToString("HH:mm");
                model.EndDate = availableEndDate.ToString("dd-MM-yyyy");
                model.EndDateString = availableEndDate.ToString("dd-MM");


                SetBookingDataByFilterFormVM(model);

                SearchResultVM searchResult = new SearchResultVM();
                searchResult.Products = _productService.Search(BookingData.OfferCriteria);
                searchResult.Properties = _propertyService.GetProperties();
                searchResult.Destination = _locationService.GetLocationByBestemmingId(BookingData.OfferCriteria.OphaalBestemmingId);
                searchResult.TotalSearchResult = _productService.CountSearch(BookingData.OfferCriteria);
                searchResult.BookingData = BookingData;
                searchResult.SelectedPropertyDetails = BookingData.OfferCriteria.EigenschapWaarden;

                var pickUpCity = BookingData.Destinations.Find(f => f.Id == BookingData.OfferCriteria.OphaalBestemmingId);
                if (pickUpCity != null)
                {
                    searchResult.PickUpCityName = pickUpCity.DisplayName;
                }

                return View(searchResult);
            }
        }

        //Checkout 2
        [Route("extras-kiezen")]
        public ActionResult Checkout2(SearchCamperModel options)
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || (BookingData.CamperAanbiedingModel == null && IsGet))
                return RedirectToAction("Index", "Home");

            ViewBag.BookingData = BookingData;
            BookingData.Gegevens = BookingData.Gegevens ?? new Busjehuren.Core.Models.BookingData.MyGegevens();

            var model = new Checkout2VM();
            model.LandBestemming = BookingData.Gegevens.LandBestemming;

            if (IsPost)
            {
                if (options.vestigingId == 0 || options.aanbiedingId == 0 || options.vestigingTerugbrengId == 0)
                    return RedirectToAction("Index", "Home");

                BookingData.Step2 = true;
                _checkOutService.SetBookingData(options, BookingData);

                return View(model);
            }

            return View(model);
        }

        [Route("checkout2end")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Checkout2End(Checkout2VM model)
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || BookingData.CamperAanbiedingModel == null)
                return RedirectToAction("Index", "Home");

            if (!string.IsNullOrEmpty(model.SelectedPackages))
            {
                var packages = JsonConvert.DeserializeObject<List<SelectedPackage>>(model.SelectedPackages);

                BookingData.VestigingModel.Pakkets.ForEach(x =>
                {
                    var selectedPackage = packages.FirstOrDefault(c => c.Id == x.Id);
                    if (selectedPackage != null)
                    {
                        x.Number = selectedPackage.Number;
                        x.IsCheck = true;
                    }
                    else
                    {
                        x.Number = 1;
                        x.IsCheck = false;
                    }
                });
            }

            BookingData.Gegevens.LandBestemming = model.LandBestemming;
            BookingData.GeselecteerdePakketten = BookingData.VestigingModel.Pakkets.Where(x => x.IsCheck).ToList();
            BookingData.Step3 = true;

            return RedirectToAction("Checkout3");
        }

        //Checkout 3
        [Route("uw-gegevens")]
        [HttpGet]
        public ActionResult Checkout3()
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || BookingData.CamperAanbiedingModel == null)
                return RedirectToAction("Index", "Home");

            return View(BookingData.Gegevens);
        }

        [Route("uw-gegevens")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Checkout3(Busjehuren.Core.Models.BookingData.MyGegevens model)
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || BookingData.CamperAanbiedingModel == null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var birthDate = model.Hoofdboeker.BirthDate;

                var hoofdboeker = new Busjehuren.Core.Models.BookingData.Reisgenoot
                {
                    Hoofdboeker = true,
                    Geslacht = model.Hoofdboeker.Geslacht,
                    Voornaam = model.Hoofdboeker.Voornaam,
                    Familienaam = model.Hoofdboeker.Familienaam,
                    Geboortedag = int.Parse(birthDate.Split('-')[0]),
                    Geboortemaand = int.Parse(birthDate.Split('-')[1]),
                    Geboortejaar = int.Parse(birthDate.Split('-')[2]),
                    BirthDate = birthDate
                };

                model.Hoofdboeker = hoofdboeker;

                BookingData.Gegevens = model;
                BookingData.Step4 = true;

                return RedirectToAction("Checkout4");
            }

            return View(model);

        }

        //Checkout 4
        [Route("controleren")]
        public ActionResult Checkout4()
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || BookingData.CamperAanbiedingModel == null)
                return RedirectToAction("Index", "Home");

            return View(BookingData);
        }

        [Route("checkout4end")]
        [HttpPost]
        public ActionResult Checkout4End()
        {
            if (BookingData == null || BookingData.OfferCriteria == null || BookingData.OfferCriteria.Busjetype == 0 || BookingData.CamperAanbiedingModel == null)
                return RedirectToAction("Index", "Home");

            BookingData.Step5 = true;

            _checkOutService.CreateReservation(BookingData);

            TempData["ReserveringsNummer"] = BookingData.ReserveringsNummer;

            ResetBoekingsDataSession();

            return RedirectToAction("Checkout5");
        }

        //Checkout 5
        [Route("reservering")]
        public ActionResult Checkout5()
        {
            string ReserveringsNummer = TempData["ReserveringsNummer"] != null ? TempData["ReserveringsNummer"].ToString() : "";

            if (string.IsNullOrEmpty(ReserveringsNummer))
                return RedirectToAction("Index", "Home");

            return View(new Tuple<string>(ReserveringsNummer));
        }

        [Route("loadmore")]
        public ActionResult _LoadMore(int page = 1, List<int> properties = null)
        {
            SearchResultVM searchResult = new SearchResultVM();
            searchResult.Products = _productService.Search(BookingData.OfferCriteria, pageNr: page, propertyValues: properties ?? new List<int>());
            searchResult.TotalSearchResult = searchResult.Products.Any() ? searchResult.Products.First().Total : 0;
            searchResult.BookingData = BookingData;
            searchResult.SelectedPropertyDetails = BookingData.OfferCriteria.EigenschapWaarden;

            ViewData[GlobalStatic.Islast] = !_productService.Search(BookingData.OfferCriteria, pageNr: page + 1, propertyValues: properties ?? new List<int>()).Any();

            return PartialView("_LoadMore", searchResult);
        }

        public ActionResult _ReservationCart()
        {
            return PartialView(BookingData);
        }

        public ActionResult CheckOutSteps(int currentStep)
        {
            return PartialView("_CheckOutSteps", new Tuple<BookingData, int>(BookingData, currentStep));
        }

        //public ActionResult _LocationDetail()
        //{
        //    return PartialView(_locationService.GetLocationByBestemmingId(BookingData.OfferCriteria.OphaalBestemmingId));
        //}

        [HttpPost]
        [Route("searchbyproperties")]
        public ActionResult SearchByProperties(List<int> properties)
        {
            if (BookingData.OfferCriteria.OphaalBestemmingId == 0)
                return new EmptyResult();

            SearchResultVM searchResult = new SearchResultVM();
            searchResult.Products = _productService.Search(BookingData.OfferCriteria, propertyValues: properties ?? new List<int>());
            searchResult.TotalSearchResult = searchResult.Products.Any() ? searchResult.Products.First().Total : 0;
            searchResult.BookingData = BookingData;
            searchResult.SelectedPropertyDetails = BookingData.OfferCriteria.EigenschapWaarden;

            var pickUpCity = BookingData.Destinations.Find(f => f.Id == BookingData.OfferCriteria.OphaalBestemmingId);
            if (pickUpCity != null)
            {
                searchResult.PickUpCityName = pickUpCity.DisplayName;
            }

            return PartialView("_SearchResult", searchResult);
        }

        #endregion

        #region heplper

        private void ResetBoekingsDataSession()
        {
            var tempAanbiedingCriteria = BookingData.OfferCriteria;
            BookingData = new BookingData { OfferCriteria = tempAanbiedingCriteria };
        }

        private void SetBookingDataByFilterFormVM(FilterFormVM model)
        {
            if (model != null)
            {
                BookingData.CurrentUrl = Request.RawUrl;
                BookingData.OfferCriteria.OphaalBestemmingId = model.PickUpCity.Id;
                BookingData.Destinations = new List<DestinationModel>();

                if (BookingData.OfferCriteria.OphaalBestemmingId != 0)
                {
                    BookingData.Destinations.Add(model.PickUpCity);
                }

                BookingData.OfferCriteria.TerugbrengBestemmingId = model.DropOffCity.Id;
                if (BookingData.OfferCriteria.TerugbrengBestemmingId != 0)
                {
                    BookingData.Destinations.Add(model.DropOffCity);
                }

                BookingData.OfferCriteria.StartDate = DateTime.ParseExact(model.StartDate,
                    GlobalStatic.DateFormatString,
                    CultureInfo.InvariantCulture);

                BookingData.OfferCriteria.OphaalTijd = DateTime.ParseExact(model.PickUpTime,
                    GlobalStatic.TimeFormatString, CultureInfo.InvariantCulture);

                BookingData.OfferCriteria.EndDate = DateTime.ParseExact(model.EndDate, GlobalStatic.DateFormatString,
                    CultureInfo.InvariantCulture);

                BookingData.OfferCriteria.TerugbrengTijd = DateTime.ParseExact(model.DropOffTime,
                    GlobalStatic.TimeFormatString, CultureInfo.InvariantCulture);

                TimeSpan tsOphaalTijd = new TimeSpan(BookingData.OfferCriteria.OphaalTijd.Hour,
                    BookingData.OfferCriteria.OphaalTijd.Minute, BookingData.OfferCriteria.OphaalTijd.Second);

                TimeSpan tsTerugBrengTijd = new TimeSpan(BookingData.OfferCriteria.TerugbrengTijd.Hour,
                    BookingData.OfferCriteria.TerugbrengTijd.Minute, BookingData.OfferCriteria.TerugbrengTijd.Second);

                BookingData.OfferCriteria.StartDate += tsOphaalTijd;
                BookingData.OfferCriteria.EndDate += tsTerugBrengTijd;
                BookingData.OfferCriteria.MaxPrijs = 1000000;
                BookingData.OfferCriteria.NumberOfAdults = 2;
                BookingData.OfferCriteria.ZoekBestemmingId = model.PickUpCity.ParentId.HasValue
                    ? model.PickUpCity.ParentId.Value
                    : 0;
                BookingData.OfferCriteria.ZoekTerugbrengBestemmingId = model.DropOffCity.ParentId.HasValue
                    ? model.DropOffCity.ParentId.Value
                    : 0;

                BookingData.OfferCriteria.Busjetype = (BusjeType)model.CarType;
            }
        }

        protected internal class SelectedPackage
        {
            public int Id { get; set; }
            public int Number { get; set; }
        }

        #endregion
    }
}