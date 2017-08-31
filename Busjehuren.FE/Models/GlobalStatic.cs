using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml;

namespace Busjehuren.FE.Models
{
    public class GlobalStatic
    {
        public const string DefaultLanguage = "nl-NL";

        public const string FormatDateStringPickup = "ddd dd-M-\\'yy HH:mmu";

        public static string UrlHowDoesItWork { get { return ConfigurationManager.AppSettings["UrlHowDoesItWork"]; } }

        public static string ToEmailContact { get { return ConfigurationManager.AppSettings["ToEmailContact"]; } }

        public static string SubjectContact { get { return ConfigurationManager.AppSettings["SubjectContact"]; } }

        public const string EmailTemplatesContact = "App_Data/EmailTemplates/Contact.cshtml";

        public static string BaseUrl { get { return ConfigurationManager.AppSettings["BaseUrl"]; } }

        public const string NetherLandPositionText = "Ik blijf in Nederland";

        public static string Islast { get { return "Islast"; } }

        public static string SelectedPropertyDetails { get { return "SelectedPropertyDetails"; } }

        public static string PreviousStep { get { return "PreviousStep"; } }

        public static string BookingData { get { return "BoekingsData"; } }

        public static string DateFormatString
        {
            get { return "d-M-yyyy"; }
        }

        public static string TimeFormatString
        {
            get { return "HH:mm:ss"; }
        }

        public static string UrlNameFaq { get { return ConfigurationManager.AppSettings["UrlNameFaq"]; } }

        public static string UrlNameHome { get { return ConfigurationManager.AppSettings["UrlNameHome"]; } }

        public static string UrlNameTermsConditions { get { return ConfigurationManager.AppSettings["UrlNameTermsConditions"]; } }

        public static string optionLabelGender { get { return "Maak een keuze"; } }

        public static string determineTagOfFaq { get { return ConfigurationManager.AppSettings["DetermineTagOfFaq"]; } }

        public static string DropdownsLandBestemming { get { return ConfigurationManager.AppSettings["DropdownsLandBestemming"]; } }

        public static string From { get { return ConfigurationManager.AppSettings["From"]; } }

        public static string To { get { return ConfigurationManager.AppSettings["To"]; } }

        public static string AliasAboutUs { get { return ConfigurationManager.AppSettings["AliasAboutUs"]; } }

        public static string IgnoreRoutes { get { return ConfigurationManager.AppSettings["IgnoreRoutes"]; } }

        public static string UrlNameProMoHomePage { get { return ConfigurationManager.AppSettings["UrlNameProMoHomePage"]; } }

        public static string GetImageFromNews(string text)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode parentNode = xmlDoc.CreateNode(XmlNodeType.Element, "content", "");
            parentNode.InnerXml = text;
            xmlDoc.AppendChild(parentNode);

            XmlNodeList imgs = xmlDoc.GetElementsByTagName("img");
            string imagePath = "";
            if (imgs.Count > 0 && imgs[0].Attributes != null && imgs[0].Attributes.Count > 0)
            {
                imagePath = imgs[0].Attributes["src"].Value;
            }
            return string.IsNullOrWhiteSpace(imagePath) ? "http://placehold.it/371x174" : imagePath;
        }

        public static List<string> Holidays
        {
            get
            {
                var list = new List<string>();
                list.Add("04-17");
                list.Add("04-27");
                list.Add("05-25");
                list.Add("06-05");
                list.Add("12-25");
                list.Add("12-26");

                return list;
            }
        }

    }
}