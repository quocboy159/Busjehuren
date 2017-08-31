using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IEigenschapWaardeRepository : IRepository<EigenschapWaarde>
    {
    }

    public class EigenschapWaardeRepository : BaseRepository<EigenschapWaarde, BusjehurenDbContext>, IEigenschapWaardeRepository
    {
        public EigenschapWaardeRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
