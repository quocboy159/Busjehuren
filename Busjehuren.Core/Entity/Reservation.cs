using Busjehuren.Core.Entity;

namespace Busjehuren.Core.EF
{
    public partial class Reservation : IEntity
    {
        public enum ReservationStatus
        {
            Quote = 10,
            ReservationRequest = 15,
            QuoteChanged = 20,
            QuoteRefused = 30,
            QuoteChecked = 35,
            Approved = 40,
            Confirmed = 50,
            Paid = 60,
            Cancelled = 70,
            NotAvailable = 80
        }
    }
}
