using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("Bestemming")]
    public class BestemmingModel
    {
        [XmlElement("Id")]
        public int Id;

        [XmlElement("ParentId")]
        public int? ParentId;

        [XmlElement("UrlName")]
        public string UrlName;

        [XmlElement("SeoName")]
        public string SeoName;

        [XmlElement("BxName")]
        public string BxName;

        [XmlElement("DestinationType")]
        public int? DestinationType;

        [XmlElement("Latitude")]
        public double? Latitude;

        [XmlElement("Longitude")]
        public double? Longitude;

        [XmlElement("ZoomLevel")]
        public int? ZoomLevel;

        [XmlElement("IsActive")]
        public bool? IsActive;

        [XmlElement("TradeTrackerXml")]
        public bool? TradeTrackerXml;

        [XmlElement("TradeTrackerTopXml")]
        public bool? TradeTrackerTopXml;

        [XmlElement("WeerOnlineCountryID")]
        public int? WeerOnlineCountryID;

        [XmlElement("WeerOnlineRegionID")]
        public int? WeerOnlineRegionID;

        [XmlElement("WtcDesID")]
        public int? WtcDesID;

        [XmlElement("WtcHcID")]
        public int? WtcHcID;

        [XmlElement("WtcVtID")]
        public int? WtcVtID;

        [XmlElement("WtcHoID")]
        public int? WtcHoID;

        [XmlElement("ImageCounter")]
        public int? ImageCounter;

        [XmlElement("Fastcheck")]
        public bool? Fastcheck;

        [XmlElement("IsoCode")]
        public string IsoCode;

        //Camperbestemming properties.
        [XmlElement("Feestdagen")]
        public string Feestdagen;

        [XmlElement("Hotels")]
        public string Hotels;

        [XmlElement("EmailAdresReisburoId")]
        public string EmailAdresReisburoId;

        [XmlElement("CurrentDestination")]
        public bool CurrentDestination;

        public static List<BestemmingModel> Parse(IEnumerable<Destination> bestemmingEntity, string currentLocation, Language.Languages language = Language.Languages.Dutch)
        {
            return bestemmingEntity.Select(b => Parse(b, currentLocation, language)).ToList();
        }

        public static BestemmingModel Parse(Destination bestemmingEntity,string currentLocation, Language.Languages language = Language.Languages.Dutch)
        {
            var content = bestemmingEntity.Contents.Where(c => c.LanguageId == (int)language).SingleOrDefault()
                ?? bestemmingEntity.Contents.Where(c => c.LanguageId == (int)Language.Languages.Dutch).SingleOrDefault()
                ?? bestemmingEntity.Contents.FirstOrDefault();

            var bestemming = new BestemmingModel
            {
                BxName = "",
                DestinationType = bestemmingEntity.TypeId,
                Fastcheck = false,
                Id = bestemmingEntity.Id,
                ParentId = bestemmingEntity.ParentId,
                ImageCounter = 0,
                IsActive = bestemmingEntity.Status,
                IsoCode = "",
                Latitude = bestemmingEntity.Contents.First().Latitude,
                Longitude = bestemmingEntity.Contents.First().Longitude,
                SeoName = content != null ? content.DisplayName : "",
                TradeTrackerTopXml = false,
                TradeTrackerXml = false,
                UrlName = content != null ? content.UrlName : "",
                WeerOnlineCountryID = 0,
                WeerOnlineRegionID = 0,
                WtcDesID = 0,
                WtcHcID = 0,
                WtcHoID = 0,
                WtcVtID = 0,
                ZoomLevel = bestemmingEntity.Contents.First().ZoomLevel,
                Feestdagen = "",
                Hotels = "",
                EmailAdresReisburoId = "",
                CurrentDestination = content.UrlName == currentLocation
            };
            return bestemming;
        }
    }
}
