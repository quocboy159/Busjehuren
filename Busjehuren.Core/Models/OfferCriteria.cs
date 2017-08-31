using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Busjehuren.Core.Enums;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("AanbiedingCriteria")]
    public class OfferCriteria
    {
        public OfferCriteria()
        {
            _NumberOfAdults = 2;
            _NumberOfChildrens = 0;
        }

        public BusjeType Busjetype
        {
            get;
            set;
        }

        public string Sortering = "Prijs";
        private int _pageNr = 1;
        public int PageNr
        {
            get { return _pageNr > 0 ? _pageNr : 1; }
            set { _pageNr = value; }
        }

        private int _pageSize = 1000000;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        private int _vestigingId;
        public int VestigingId
        {
            get { return _vestigingId; }
            set { _vestigingId = value; }
        }

        private int _aanbiedingId;
        public int AanbiedingId
        {
            get { return _aanbiedingId; }
            set { _aanbiedingId = value; }
        }

        private int _NumberOfChildrens;
        [XmlElement("AantalKinderen")]
        public int NumberOfChildrens
        {
            get { return _NumberOfChildrens; }
            set { _NumberOfChildrens = value; }
        }

        private int _NumberOfAdults;
        [XmlElement("AantalVolwassenen")]
        public int NumberOfAdults
        {
            get { return _NumberOfAdults; }
            set { _NumberOfAdults = value; }
        }

        private int _zoekBestemmingId;
        public int ZoekBestemmingId
        {
            get { return _zoekBestemmingId; }
            set { _zoekBestemmingId = value; }
        }

        private int _ophaalBestemmingId;
        public int OphaalBestemmingId
        {
            get { return _ophaalBestemmingId; }
            set { _ophaalBestemmingId = value; }
        }

        private int _zoekTerugbrengBestemmingId;
        public int ZoekTerugbrengBestemmingId
        {
            get { return _zoekTerugbrengBestemmingId; }
            set { _zoekTerugbrengBestemmingId = value; }
        }

        private int _terugbrengBestemmingId;
        public int TerugbrengBestemmingId
        {
            get { return _terugbrengBestemmingId; }
            set { _terugbrengBestemmingId = value; }
        }

        private int _minPrijs;
        public int MinPrijs
        {
            get { return _minPrijs; }
            set { _minPrijs = value; }
        }

        private int _maxPrijs;
        public int MaxPrijs
        {
            get { return _maxPrijs; }
            set { _maxPrijs = value; }
        }

        private DateTime _StartDate;
        [XmlElement("OphaalDatum")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        private DateTime _EndDate;
        [XmlElement("TerugbrengDatum")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private DateTime _ophaalTijd;
        public DateTime OphaalTijd
        {
            get { return _ophaalTijd; }
            set { _ophaalTijd = value; }
        }

        private DateTime _terugbrengTijd;
        public DateTime TerugbrengTijd
        {
            get { return _terugbrengTijd; }
            set { _terugbrengTijd = value; }
        }

        private DateTime _reserveringsDatum = DateTime.Now.Date;
        public DateTime ReserveringsDatum
        {
            get { return _reserveringsDatum; }
            set { _reserveringsDatum = value; }
        }

        private List<PropertyDetailModel> _eigenschapWaarden = new List<PropertyDetailModel>();
        public List<PropertyDetailModel> EigenschapWaarden
        {
            get
            {
                return _eigenschapWaarden;
            }
            set
            {
                _eigenschapWaarden = value;
            }
        }

    }
}
