using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Busjehuren.FE.Models
{
    public class SearchResultVM
    {
        public List<PropertyDetailModel> SelectedPropertyDetails { get; set; }

        public BookingData BookingData { get; set; }

        public List<ProductItem> Products { get; set; }

        public List<PropertyModel> Properties { get; set; }

        public Destination Destination { get; set; }

        public int TotalSearchResult { get; set; }

        public string PickUpCityName { get; set; }

        public bool IsSearch { get; set; }

        public string Title
        {
            get
            {
                if (Destination != null && Destination.Contents != null)
                {
                    var Content = Destination.Contents.FirstOrDefault();
                    return string.Format("BUSJE HUREN IN {0}", Content.DisplayName).ToUpper();
                }

                return string.Empty;
            }
        }

        public string Text
        {
            get
            {
                if (Destination != null && Destination.Contents != null)
                {
                    var Content = Destination.Contents.FirstOrDefault();
                    return Content.Text;
                }

                return string.Empty;
            }
        }

        public string HeadText
        {
            get
            {
                if (Destination != null && Destination.Contents != null)
                {
                    var Content = Destination.Contents.FirstOrDefault();
                    return Content.HeadText;
                }

                return string.Empty;
            }
        }

        public string FromMobile
        {
            get 
            {
                return BookingData.Destinations.FirstOrDefault(x => x.Id == BookingData.OfferCriteria.OphaalBestemmingId).DisplayName;
            }
        }

        public string ToMobile
        {
            get
            {
                return BookingData.Destinations.FirstOrDefault(x => x.Id == BookingData.OfferCriteria.TerugbrengBestemmingId).DisplayName;
            }
        }

        public string PickupLocationMobile
        {
            get
            {
                return BookingData.StartDate.ToString(GlobalStatic.FormatDateStringPickup, new CultureInfo(GlobalStatic.DefaultLanguage));
            }
        }

        public string ReturnLocationMobile
        {
            get
            {
                return BookingData.EndDate.ToString(GlobalStatic.FormatDateStringPickup, new CultureInfo(GlobalStatic.DefaultLanguage));
            }
        }
    }
}