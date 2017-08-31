using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IVestigingRepository : IRepository<Vestiging>
    {
    }

    public class VestigingRepository : BaseRepository<Vestiging, BusjehurenDbContext>, IVestigingRepository
    {
        public VestigingRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
