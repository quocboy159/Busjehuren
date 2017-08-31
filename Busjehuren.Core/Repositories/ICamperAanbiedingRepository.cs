using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface ICamperAanbiedingRepository : IRepository<CamperAanbieding>
    {
    }

    public class CamperAanbiedingRepository : BaseRepository<CamperAanbieding, BusjehurenDbContext>, ICamperAanbiedingRepository
    {
        public CamperAanbiedingRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
