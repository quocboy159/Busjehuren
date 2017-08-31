using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IDestinationRepository : IRepository<Destination>
    {
    }

    public class DestinationRepository : BaseRepository<Destination, BusjehurenDbContext>, IDestinationRepository
    {
        public DestinationRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
