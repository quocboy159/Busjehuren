using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Enums;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using Busjehuren.Common.Utils;
using System.Xml.Serialization;
using System.IO;
using Busjehuren.Core.Models;

namespace Busjehuren.Core.Services.Impl
{
    public class CheckOutService : BaseService, ICheckOutService
    {

        private readonly IProductService _productService;
        private readonly IEmailService _emailService;

        public CheckOutService(IUnitOfWork unitOfWork, IMapper mapper, ILog log, IProductService productService, IEmailService emailService)
            : base(unitOfWork, mapper, log)
        {
            this._productService = productService;
            this._emailService = emailService;
        }

        public void SetBookingData(SearchCamperModel options, BookingData bookingData)
        {
            int camperAanbiedingId = options.aanbiedingId;
            int vestigingsId = options.vestigingId;
            int vestigingTerugbrengId = options.vestigingTerugbrengId;
            var typeId = options.typeId;

            bookingData.BookingType = (BookingType)Enum.ToObject(typeof(BookingType), typeId);

            if (camperAanbiedingId == 0 || vestigingsId == 0)
            {
                if (bookingData.LastSelectedCamper.CamperID != 0)
                {
                    camperAanbiedingId = bookingData.LastSelectedCamper.CamperID;
                    vestigingsId = bookingData.LastSelectedCamper.LocationID;
                }
            }
            else
            {
                bookingData.LastSelectedCamper = new CamperLocation { CamperID = camperAanbiedingId, LocationID = vestigingsId };
            }

            var camperAanbieding = _unitOfWork.CamperAanbiedingRepository.Find(camperAanbiedingId);
            var vestiging = _unitOfWork.VestigingRepository.Find(vestigingsId);
            var vestigingTerugbreng = _unitOfWork.VestigingRepository.Find(vestigingTerugbrengId);

            if (camperAanbieding != null && vestiging != null && bookingData.OfferCriteria != null)
            {
                bookingData.CamperAanbiedingModel = CamperAanbiedingModel.Parse(camperAanbieding, "");
                bookingData.VestigingModel = VestigingModel.Parse(new OptionParseVestigingModels() { vestigingEntity = vestiging });//_mapper.Map<VestigingModel>(vestiging);
                bookingData.VestigingModel.Pakkets = bookingData.VestigingModel.Pakkets
                                                                                .GroupBy(x => x.Naam)
                                                                                .Select(x => x.FirstOrDefault(p => bookingData.NumberOfTravelDays >= p.MinAantalDagen
                                                                                                                && bookingData.NumberOfTravelDays <= p.MaxAantalDagen) ?? x.First())
                                                                                .ToList();
                bookingData.VestigingTerugbrengModel = VestigingModel.Parse(new OptionParseVestigingModels() { vestigingEntity = vestigingTerugbreng }); //_mapper.Map<VestigingModel>(vestigingTerugbreng);
                bookingData.OfferCriteria.ZoekBestemmingId = vestiging.Destination.ParentId.GetValueOrDefault(vestiging.Destination.Id);

                var aanbiedingen = _productService.Search(bookingData.OfferCriteria, camperAanbiedingId, vestigingsId, vestiging, bookingData.OfferCriteria.PageNr, bookingData.OfferCriteria.PageSize);

                if (aanbiedingen.Count != 0)
                    bookingData.TotalCamperPrice = aanbiedingen.First().PriceInfoModel.TotaalPakketPrijs;

                var ophaalParentId = bookingData.VestigingModel.Destination.ParentId;
                if (ophaalParentId != null)
                {
                    var bestemmingen = _unitOfWork.DestinationRepository.FindAll(x => x.ParentId == ophaalParentId).ToList();
                    bookingData.Destinations = ConvertToDestinationModels(bestemmingen);
                }

                var terugbrengParentId = bookingData.VestigingTerugbrengModel.Destination.ParentId;
                if (terugbrengParentId != null && terugbrengParentId != ophaalParentId)
                {
                    var bestemmingen = _unitOfWork.DestinationRepository.FindAll(x => x.ParentId == terugbrengParentId).ToList();
                    bookingData.Destinations.AddRange(ConvertToDestinationModels(bestemmingen));
                    bookingData.Destinations = bookingData.Destinations.Distinct().ToList();
                }

                foreach (var p in bookingData.VestigingModel.Pakkets)
                {
                    p.Number = 1;

                    if (p.IsGratis && (DateTime)p.GratisTot >= bookingData.EndDate && (DateTime)p.GratisVan <= bookingData.StartDate)
                    {
                        p.Prijs = 0;
                    }
                }
            }

        }

        public void CreateReservation(BookingData bookingData)
        {
            //The BoekinsData.BoekingsType is set in STEP 2 of the proces, based on which button is clicked.
            var reservationStatus = bookingData.BookingType == BookingType.Reservation ? (int)Reservation.ReservationStatus.ReservationRequest : (int)Reservation.ReservationStatus.Quote;

            //reservation
            var reservation = new Reservation
            {
                ReservationNumber = "0",
                Status = reservationStatus,
                ReservationDate = DateTime.Now,
                GrossAmount = bookingData.TotaalPrijs,
                Discount = 0,
                NetAmount = bookingData.TotaalPrijs,
                PickupDate = bookingData.StartDate,
                ReturnDate = bookingData.EndDate,
                DestinationId = bookingData.OfferCriteria.OphaalBestemmingId,
                SnapShot = XElement.Parse("<root />").ToString(),
                ReservationLanguageId = 1,
                TotalAdults = bookingData.NumberOfAdults,
                TotalChildren = bookingData.NumberOfChildrens,
                CompanyName = "",
                DestinationCountry = !string.IsNullOrEmpty(bookingData.Gegevens.LandBestemming) ? bookingData.Gegevens.LandBestemming : "",
                AfwijkendFactuuradres = bookingData.Gegevens.AfwijkendFactuuradres,
                FactuurBedrijfsnaam = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurBedrijfsnaam) ? bookingData.Gegevens.FactuurBedrijfsnaam : "",
                FactuurEmail = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurEmail) ? bookingData.Gegevens.FactuurEmail : "",
                FactuurTelefoon = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurTelefoon) ? bookingData.Gegevens.FactuurTelefoon : "",
                FactuurStraat = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurStraat) ? bookingData.Gegevens.FactuurStraat : "",
                FactuurHuisnummer = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurHuisnummer) ? bookingData.Gegevens.FactuurHuisnummer : "",
                FactuurPostcode = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurPostcode) ? bookingData.Gegevens.FactuurPostcode : "",
                FactuurPlaats = !string.IsNullOrEmpty(bookingData.Gegevens.FactuurStad) ? bookingData.Gegevens.FactuurStad : "",
            };

            _unitOfWork.ReservationRepository.Add(reservation);
            _unitOfWork.Commit();

            reservation.ReservationNumber = GenerateReserveringsNummer(reservation.Id);
            bookingData.ReserveringsNummer = reservation.ReservationNumber;

            // To write the file, a TextWriter is required.
            XmlSerializer serializer = new XmlSerializer(typeof(BookingData));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, bookingData);
            reservation.SnapShot = XElement.Parse(writer.ToString()).ToString();

            _unitOfWork.Commit();

            //Saving address
            var address = new Adre
            {
                Straat = bookingData.Gegevens.Straat,
                Huisnummer = bookingData.Gegevens.Huisnummer,
                Postcode = bookingData.Gegevens.Postcode,
                Plaats = bookingData.Gegevens.Stad,
                Telefoonnummer = bookingData.Gegevens.Telefoon,
            };

            _unitOfWork.AdreRepository.Add(address);
            _unitOfWork.Commit();

            // Saving person info
            var person = new Person
            {
                AddressId = address.Id,
                ReservationId = reservation.Id,
                FirstName = bookingData.Gegevens.Hoofdboeker.Voornaam,
                LastName = bookingData.Gegevens.Hoofdboeker.Familienaam,
                EmailAddress = bookingData.Gegevens.Email,
                Gender = bookingData.Gegevens.Hoofdboeker.Geslacht,
                DateOfBirth = new DateTime(bookingData.Gegevens.Hoofdboeker.Geboortejaar, bookingData.Gegevens.Hoofdboeker.Geboortemaand, bookingData.Gegevens.Hoofdboeker.Geboortedag)
            };

            _unitOfWork.PersonRepository.Add(person);
            _unitOfWork.Commit();

            foreach (var item in bookingData.VestigingModel.Pakkets.Where(x => x.IsCheck))
            {
                var package = new ReservationPackage
                {
                    ReservationId = reservation.Id,
                    Name = item.Naam,
                    Description = item.UitgebreideOmschrijving,
                    Price = getPackagePrice(bookingData, item)
                };

                _unitOfWork.ReservationPackageRepository.Add(package);
            }

            _unitOfWork.Commit();

            SendEmail(bookingData);
        }

        #region hepler

        private List<DestinationModel> ConvertToDestinationModels(List<Destination> Destinations)
        {
            var contents = _unitOfWork.ContentRepository.FindAll().ToList();
            var list = new List<DestinationModel>();

            Destinations.ForEach(x =>
            {
                list.Add(new DestinationModel()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    DisplayName = contents.FirstOrDefault(c => c.DestinationId == x.Id).DisplayName
                });
            });

            return list;
        }

        private string GenerateReserveringsNummer(int id)
        {
            var code = string.Empty;
            var n = 5;
            var t = n - id.ToString().Length;

            for (var i = 0; i < t; i++)
            {
                code += "0";
            }

            code += id.ToString();

            var reserveringsnummer = DateTime.Now.Year.ToString() + code;

            return reserveringsnummer;
        }

        private decimal getPackagePrice(BookingData bookingData, PakketModel pakket)
        {
            decimal total = 0;
            var aantalDagen = bookingData.NumberOfTravelDays;
            var aantalVolwassenen = bookingData.NumberOfAdults;
            var aantalKinderen = bookingData.NumberOfChildrens;
            var aantalPersonen = (aantalVolwassenen + aantalKinderen);
            var tempPrijs = pakket.IsLokaalTeBetalen ? pakket.Prijs : (pakket.Prijs * pakket.Valuta.Koers);

            //Controleer pakkettype. 10 = Pakketprijs per dag.
            if (pakket.PakketType == 10)
                tempPrijs = tempPrijs * aantalDagen;

            if ((bool)pakket.IsPerPersoon)
            {
                total = tempPrijs * aantalPersonen;
            }
            else
            {
                total = tempPrijs;// total += tempPrijs/ aantalPersonen;
            }

            return Math.Round(total, 2);
        }

        private void SendEmail(BookingData bookingData)
        {
            string from = ConfigurationManager.AppSettings["From"];
            string to = bookingData.Gegevens.Email;
            string subject = string.Empty;
            string bodyHtml = string.Empty;
            List<string> emailTos = new List<string>();
            emailTos.Add(to);

            if (bookingData.BookingType == BookingType.Reservation)
            {
                subject = string.Format("{0} {1}", ConfigurationManager.AppSettings["CamperSubject"], bookingData.ReserveringsNummer);
                bodyHtml = RazorHelper.Parse("App_Data/EmailTemplates/Reservation.cshtml", bookingData);
            }
            else
            {
                subject = string.Format("{0} {1}", ConfigurationManager.AppSettings["CamperOfferSubject"], bookingData.ReserveringsNummer);
                bodyHtml = RazorHelper.Parse("App_Data/EmailTemplates/Offer.cshtml", bookingData);
            }

            EmailModel email = new EmailModel();
            email.Body = bodyHtml;
            email.From = from;
            email.To = emailTos.ToArray();
            email.Subject = subject;

            _emailService.Send(email);
        }

        #endregion
    }
}
