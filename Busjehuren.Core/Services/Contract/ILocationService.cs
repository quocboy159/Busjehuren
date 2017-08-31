using Busjehuren.Core.Dto;
using System.Collections.Generic;
using Busjehuren.Core.Models;
using Busjehuren.Core.EF;
using System;

namespace Busjehuren.Core.Services.Contract
{
    public interface ILocationService
    {
        List<DestinationModel> GetAllDestination(int currentLanguage = 1);
        List<VestigingModel> GetLocations();
        object[] GetVestigingOpeningHourByLocationId(int Id, DateTime date);
        List<int> GetAvailableDays(int Id);
        DestinationModel GetDestination(int id);
        Destination GetLocationByBestemmingId(int bestemmingId);
        DestinationModel GetDestinationByUrl(string url);
    }
}
