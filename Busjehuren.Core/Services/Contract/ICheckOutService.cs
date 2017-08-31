using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;
using System.Collections.Generic;

namespace Busjehuren.Core.Services.Contract
{
    public interface ICheckOutService
    {
        void SetBookingData(SearchCamperModel options, BookingData bookingData);
        void CreateReservation(BookingData bookingData);
    }
}
