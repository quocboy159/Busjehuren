using Busjehuren.Core.EF;
using Busjehuren.Core.Enums;
using Busjehuren.Core.Hepler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("BoekingsData")]
    public class BookingData
    {
        public string CurrentUrl { get; set; } 

        private CamperLocation _LastSelectedCamper;
        public CamperLocation LastSelectedCamper
        {
            get
            {
                _LastSelectedCamper = _LastSelectedCamper ?? new CamperLocation();
                return _LastSelectedCamper;
            }
            set { _LastSelectedCamper = value; }
        }

        //Search criteria
        private OfferCriteria _offerCriteria;
        [XmlElement("AanbiedingCriteria")]
        public OfferCriteria OfferCriteria
        {
            get
            {
                _offerCriteria = _offerCriteria ?? new OfferCriteria();
                return _offerCriteria;
            }
            set { _offerCriteria = value; }
        }

        private bool _step1;
        [XmlElement("Stap1")]
        public bool Step1
        {
            get { return _step1; }
            set
            {
                _step1 = value;
                if (_step1)
                {
                    _step2 = false;
                    _step3 = false;
                    _step4 = false;
                    _step5 = false;
                }
            }
        }

        private bool _step2;
        [XmlElement("Stap2")]
        public bool Step2
        {
            get { return _step2; }
            set
            {
                _step2 = value;
                if (_step2)
                {
                    _step1 = true;
                    _step3 = false;
                    _step4 = false;
                    _step5 = false;
                }
            }
        }

        private bool _step3;
        [XmlElement("Stap3")]
        public bool Step3
        {
            get { return _step3; }
            set
            {
                _step3 = value;
                if (_step3)
                {
                    _step1 = true;
                    _step2 = true;
                    _step4 = false;
                    _step5 = false;
                }
            }
        }

        private bool _step4;
        [XmlElement("Stap4")]
        public bool Step4
        {
            get { return _step4; }
            set
            {
                _step4 = value;
                if (_step4)
                {
                    _step1 = true;
                    _step2 = true;
                    _step3 = true;
                    _step5 = false;
                }
            }
        }

        private bool _step5;
        [XmlElement("Stap5")]
        public bool Step5 { get { return _step5; } set { _step5 = value; } }

        [XmlElement("BoekingsType")]
        public BookingType BookingType { get; set; }

        [XmlElement("ReserveringsNummer")]
        public string ReserveringsNummer { get; set; }

        /// <summary>
        /// Number of adults
        /// </summary>
        [XmlElement("AantalVolwassenen")]
        public int NumberOfAdults
        {
            get { return OfferCriteria.NumberOfAdults; }
            set { OfferCriteria.NumberOfAdults = value; }
        }

        /// <summary>
        /// Number of children
        /// </summary>
        [XmlElement("AantalKinderen")]
        public int NumberOfChildrens
        {
            get { return OfferCriteria.NumberOfChildrens; }
            set { OfferCriteria.NumberOfChildrens = value; }
        }

        /// <summary>
        /// Start date
        /// </summary>
        [XmlElement("Ophaaldatum")]
        public DateTime StartDate
        {
            get { return OfferCriteria.StartDate; }
            set { OfferCriteria.StartDate = value; }
        }

        /// <summary>
        /// End date
        /// </summary>
        [XmlElement("Inleverdatum")]
        public DateTime EndDate
        {
            get { return OfferCriteria.EndDate; }
            set { OfferCriteria.EndDate = value; }
        }

        /// <summary>
        /// Number of travel days
        /// </summary>
        [XmlElement("DuurInDagen")]
        public int NumberOfTravelDays
        {
            get
            {
                var aantalDagen = (int)Math.Ceiling((EndDate - StartDate).TotalDays);
                if (CamperAanbiedingModel != null && CamperAanbiedingModel.Camper != null && CamperAanbiedingModel.Camper.Leverancier != null && CamperAanbiedingModel.Camper.Leverancier.AddOneExtraDay == true)
                {
                    aantalDagen++;
                }

                return aantalDagen;
            }
            set { }
        }

        /// <summary>
        /// Destination list
        /// </summary>
        public List<DestinationModel> Destinations { get; set; }
        [XmlElement("PakketTotaalPrijsPerPersoon")]
        public decimal PakketTotaalPrijsPerPersoon
        {
            get;
            set;
        }

        /// <summary>
        /// Total price
        /// </summary>
        [XmlElement("TotaalPrijs")]
        public decimal TotaalPrijs
        {
            get
            {
                return TotalPrepaidPrice + (VestigingModel == null ? 0 : TotalLocalPackagesPrice / VestigingModel.Valuta.Koers);
            }
            set { }
        }

        /// <summary>
        /// Total camper price
        /// </summary>
        [XmlElement("TotalCamperPrice")]
        public decimal TotalCamperPrice { get; set; }

        /// <summary>
        /// Total prepaid packages price (without camper price)
        /// </summary>
        [XmlElement("TotalPrepaidPackagesPrice")]
        public decimal TotalPrepaidPackagesPrice
        {
            get
            {
                if (GeselecteerdePakketten == null)
                    return 0;

                var result = (from pakket in GeselecteerdePakketten
                              where !pakket.IsLokaalTeBetalen
                              let personCount = pakket.IsPerPersoon.Value ? OfferCriteria.NumberOfAdults + OfferCriteria.NumberOfChildrens : 1
                              select CamperCalculation.CalculatePackagePrice(NumberOfTravelDays, personCount, pakket)).Sum();
                return Math.Round(result, 2);
            }
            set { }
        }

        /// <summary>
        /// Total prepaid price (= total prepaid packages price + camper price)
        /// </summary>
        [XmlElement("TotalPrepaidPrice")]
        public decimal TotalPrepaidPrice
        {
            get { return TotalCamperPrice + TotalPrepaidPackagesPrice; }
            set { }
        }

        /// <summary>
        /// Total local packages price
        /// </summary>
        [XmlElement("TotalLocalPackagesPrice")]
        public decimal TotalLocalPackagesPrice
        {
            get
            {
                if (GeselecteerdePakketten == null)
                    return 0;

                var result = (from pakket in GeselecteerdePakketten
                              where pakket.IsLokaalTeBetalen
                              let personCount = pakket.IsPerPersoon.Value ? OfferCriteria.NumberOfAdults + OfferCriteria.NumberOfChildrens : 1
                              select CamperCalculation.CalculateLocalPackagePrice(NumberOfTravelDays, personCount, pakket)).Sum();
                return Math.Round(result, 2);
            }
            set { }
        }
        [XmlElement("DiscountAmount")]
        public decimal DiscountAmount { get; set; }
        [XmlElement("TotalAfterDiscount")]
        public decimal TotalAfterDiscount
        {
            get { return TotaalPrijs - DiscountAmount; }
            set { }
        }

        /// <summary>
        /// Selected packages
        /// </summary>
        [XmlArray("GeselecteerdePakketten")]
        [XmlArrayItem("Pakket")]
        public List<PakketModel> GeselecteerdePakketten {get;set;}

        /// <summary>
        /// Main booker information
        /// </summary>
        public MyGegevens Gegevens { get; set; }

        private CamperAanbiedingModel _CamperAanbiedingModel;

        /// <summary>
        /// Camper details
        /// </summary>
        [XmlElement("CamperAanbieding")]
        public CamperAanbiedingModel CamperAanbiedingModel
        {
            get { return _CamperAanbiedingModel; }
            set
            {
                if (_CamperAanbiedingModel != null && _CamperAanbiedingModel.Id != value.Id)
                {
                    ClearAanbiedingRelatedSettings();
                }

                _CamperAanbiedingModel = value;

            }
        }

        private void ClearAanbiedingRelatedSettings()
        {
            this.GeselecteerdePakketten = null;
        }

        private VestigingModel _VestigingModel;

        /// <summary>
        /// What is this ?
        /// </summary>
        [XmlElement("Vestiging")]
        public VestigingModel VestigingModel
        {
            get { return _VestigingModel; }
            set
            {
                if (_VestigingModel != null && _VestigingModel.Id != value.Id)
                {
                    ClearAanbiedingRelatedSettings();
                }
                _VestigingModel = value;
            }
        }

        private VestigingModel _vestigingTerugbreng;

        /// <summary>
        /// zone(longtitue, lattitude) ?
        /// </summary>
        [XmlElement("VestigingTerugbreng")]
        public VestigingModel VestigingTerugbrengModel
        {
            get
            {
                if (_vestigingTerugbreng == null)
                {
                    return _VestigingModel;
                }
                return _vestigingTerugbreng;
            }
            set
            {
                if (_vestigingTerugbreng != null && _vestigingTerugbreng.Id != value.Id)
                {
                    ClearAanbiedingRelatedSettings();
                }
                _vestigingTerugbreng = value;
            }
        }

        [Serializable]
        [XmlRoot("UwGegevens")]
        public class MyGegevens
        {
            [XmlElement("Hoofdboeker")]
            public Reisgenoot Hoofdboeker { get; set; }

            public string Adres { get { return Straat + " " + Huisnummer; } }

            [XmlElement("LandBestemming")]
            public string LandBestemming { get; set; }

            [XmlElement("Straat")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Straat { get; set; }

            [XmlElement("Huisnummer")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Huisnummer { get; set; }

            [XmlElement("Postcode")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Postcode { get; set; }

            [XmlElement("Stad")]
            [Display(Name = "Plaats")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Stad { get; set; }

            [XmlElement("Telefoon")]
            [Display(Name = "Telefoonnummer")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Telefoon { get; set; }

            [XmlElement("Email")]
            [Display(Name = "E-mailadres")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Geen geldig e-mailadres.")]
            [EmailAddress(ErrorMessage = "Geen geldig e-mailadres.")]
            public string Email { get; set; }

            [XmlElement("AfwijkendFactuuradres")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public bool AfwijkendFactuuradres { get; set; }

            [XmlElement("FactuurBedrijfsnaam")]
            [Display(Name = "Bedrijfsnaam")]
            public string FactuurBedrijfsnaam { get; set; }

            [XmlElement("FactuurEmail")]
            [Display(Name = "E-mailadres")]
            public string FactuurEmail { get; set; }

            [XmlElement("FactuurTelefoon")]
            [Display(Name = "Telefoonnummer")]
            public string FactuurTelefoon { get; set; }

            [XmlElement("FactuurStraat")]
            [Display(Name = "Straat")]
            public string FactuurStraat { get; set; }

            [XmlElement("FactuurHuisnummer")]
            [Display(Name = "Huisnummer")]
            public string FactuurHuisnummer { get; set; }

            [XmlElement("FactuurPostcode")]
            [Display(Name = "Postcode")]
            public string FactuurPostcode { get; set; }

            [XmlElement("FactuurStad")]
            [Display(Name = "Plaats")]
            public string FactuurStad { get; set; }
        }

        [Serializable]
        [XmlRoot("Reisgenoot")]
        public class Reisgenoot
        {
            [XmlElement("Hoofdboeker")]
            public bool Hoofdboeker { get; set; }

            [XmlElement("Geslacht")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            [Display(Name = "Aanhef")]
            public string Geslacht { get; set; }

            [XmlElement("Voornaam")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            [Display(Name = "Voorletters")]
            public string Voornaam { get; set; }

            [XmlElement("Familienaam")]
            [Display(Name = "Achternaam")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string Familienaam { get; set; }

            [Display(Name = "Geboortedatum")]
            [Required(ErrorMessage = "Dit veld is verplicht.")]
            public string BirthDate { get; set; }

            [XmlElement("Geboortedag")]
            public int Geboortedag { get; set; }

            [XmlElement("Geboortemaand")]
            public int Geboortemaand { get; set; }

            [XmlElement("Geboortejaar")]
            public int Geboortejaar { get; set; }
        }

        [Serializable]
        [XmlRoot("Verzekering")]
        public class MyAnnuleringsVerzekering
        {
            [XmlElement("Naam")]
            public string Name { get; set; }

            [XmlElement("Type")]
            public InsuranceType Type { get; set; }

            [XmlElement("Prijs")]
            public decimal Price { get; set; }
        }

        [Serializable]
        [XmlRoot("Verzekering")]
        public class MyReisVerzekering
        {
            [XmlElement("Naam")]
            public string Name { get; set; }

            [XmlElement("Type")]
            public InsuranceType Type { get; set; }

            [XmlElement("Prijs")]
            public decimal Price { get; set; }
        }

        [Serializable]
        [XmlRoot("Verzekering")]
        public class MyNoRiskVerzekering
        {
            [XmlElement("Naam")]
            public string Name { get; set; }

            [XmlElement("Type")]
            public InsuranceType Type { get; set; }

            [XmlElement("Prijs")]
            public decimal Price { get; set; }
        }
    }
}
