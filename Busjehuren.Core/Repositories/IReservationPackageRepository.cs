using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;

namespace Busjehuren.Core.Repositories
{
    public interface IReservationPackageRepository : IRepository<ReservationPackage>
    {
    }

    public class ReservationPackageRepository : BaseRepository<ReservationPackage, BusjehurenDbContext>, IReservationPackageRepository
    {
        public ReservationPackageRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
