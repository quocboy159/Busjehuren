using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;

namespace Busjehuren.Core.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
    }

    public class ReservationRepository : BaseRepository<Reservation, BusjehurenDbContext>, IReservationRepository
    {
        public ReservationRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
