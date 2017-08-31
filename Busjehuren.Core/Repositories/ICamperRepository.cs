using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface ICamperRepository : IRepository<Camper>
    {
    }

    public class CamperRepository : BaseRepository<Camper, BusjehurenDbContext>, ICamperRepository
    {
        public CamperRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
