using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Services.Impl
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public List<DestinationModel> GetAllDestination(int currentLanguage = 1)
        {
            var list = new List<DestinationModel>();

            var provinces = _unitOfWork.DestinationRepository
                        .FindAll(x => x.Status && x.Contents.Count > 0 && x.TypeId == (int)Busjehuren.Core.Enums.DestinationType.Country)
                        .OrderBy(x => x.Contents.FirstOrDefault(c => c.LanguageId == currentLanguage).DisplayName)
                        .ToList();

            var cityList = _unitOfWork.DestinationRepository
                            .FindAll(x => x.Status && x.Contents.Count > 0 && x.TypeId == (int)Busjehuren.Core.Enums.DestinationType.City)
                            .ToList();

            //find the cities for the province
            provinces.ForEach(x =>
            {
                var content = x.Contents.FirstOrDefault(c => c.LanguageId == currentLanguage)
                              ?? x.Contents.FirstOrDefault();

                var citiesDto = new List<DestinationModel>();

                cityList.Where(c => c.ParentId == x.Id).ToList().ForEach(ci =>
                {
                    citiesDto.Add(Parse(ci));
                });

                if (citiesDto.Count > 0)
                {
                    var item = Parse(x, currentLanguage);
                    item.Cities = citiesDto;
                    list.Add(item);
                }
            });

            return list.OrderBy(x => x.DisplayName).ToList();
        }

        public object[] GetVestigingOpeningHourByLocationId(int Id, DateTime date)
        {
            List<Vestiging> vestiging = _unitOfWork.VestigingRepository
                .FindAll(v => v.BestemmingId == Id && v.Status == 1 && v.VestigingOpeningHours.Any()).ToList();

            if (vestiging.Count != 0)
            {
                var openingHour = vestiging.Select(x => new
                {
                    MinHour = x.VestigingOpeningHours.Where(vh => vh.WeekdayName == date.DayOfWeek.ToString() && vh.FromHour.HasValue).Min(h => h.FromHour),
                    MaxHour = x.VestigingOpeningHours.Where(vh => vh.WeekdayName == date.DayOfWeek.ToString() && vh.ToHour.HasValue).Max(h => h.ToHour),
                })
                .Where(vh => vh.MinHour.HasValue && vh.MaxHour.HasValue).ToList();

                decimal? startHour = openingHour.OrderBy(h => h.MinHour).FirstOrDefault() != null ? openingHour.OrderBy(h => h.MinHour).FirstOrDefault().MinHour : null;
                decimal? endHour = openingHour.OrderBy(h => h.MinHour).FirstOrDefault() != null ? openingHour.OrderByDescending(h => h.MaxHour).FirstOrDefault().MaxHour : null;

                IEnumerable<int> availableDays = vestiging.Select(v => v.VestigingOpeningHours.Select(h => h.Weekday).Distinct()).OrderByDescending(h => h.Count()).FirstOrDefault();

                return new object[]
                {
                    startHour.HasValue ? startHour.Value.ToString() : null,
                    endHour.HasValue ? endHour.Value.ToString() : null,
                    availableDays != null ? availableDays.ToList() : null
                };
            }

            return null;
        }

        public List<int> GetAvailableDays(int Id)
        {
            List<Vestiging> vestiging = _unitOfWork.VestigingRepository
                .FindAll(v => v.BestemmingId == Id && v.Status == 1 && v.VestigingOpeningHours.Any()).ToList();
            if (vestiging.Count != 0)
            {
                IEnumerable<int> availableDays = vestiging.Select(v => v.VestigingOpeningHours.Select(h => h.Weekday).Distinct()).OrderByDescending(h => h.Count()).FirstOrDefault();
                return availableDays.ToList();
            }

            return new List<int>();
        }

        public DestinationModel GetDestination(int id)
        {
            var destination = _unitOfWork.DestinationRepository.Find(id);

            return Parse(destination);
        }

        public List<VestigingModel> GetLocations()
        {
            var destinations = _unitOfWork.DestinationRepository
                                            .FindAll(d => d.DestinationType.Id == 3)
                                            .Select(d => d.Id)
                                            .ToList();

            var locations = _unitOfWork.VestigingRepository
                            .FindAll(f => (f.Status == 1)
                                          && destinations.Contains(f.BestemmingId))
                                                         .OrderBy(o => o.Naam)
                                                         .ToList();

            return locations.Any() ? VestigingModel.Parse(locations, currentLocation: "", destinations: GetAllDestination()) : new List<VestigingModel>();
        }

        public Destination GetLocationByBestemmingId(int bestemmingId)
        {
            var model = _unitOfWork.DestinationRepository.Find(bestemmingId);

            return model;
        }

        public DestinationModel GetDestinationByUrl(string url)
        {
            var rs = new DestinationModel();
            var contents = _unitOfWork.ContentRepository.FindAll(x => x.UrlName.Equals(url));

            if (contents.Any())
            {
                int DestinationId = contents.FirstOrDefault().DestinationId.Value;
                rs = Parse(_unitOfWork.DestinationRepository.Find(DestinationId));
            }

            return rs;
        }

        #region helper

        private DestinationModel Parse(Destination source, int currentLanguage = 1)
        {
            var item = new DestinationModel();
            var content = source.Contents.FirstOrDefault(c => c.LanguageId == currentLanguage)
                              ?? source.Contents.FirstOrDefault();

            item.Id = source.Id;
            item.ParentId = source.ParentId;
            item.DisplayName = content.DisplayName;
            item.UrlName = content.UrlName;

            return item;
        }

        #endregion
    }
}
