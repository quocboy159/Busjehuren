using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IAdreRepository : IRepository<Adre>
    {
    }

    public class AdreRepository : BaseRepository<Adre, BusjehurenDbContext>, IAdreRepository
    {
        public AdreRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
