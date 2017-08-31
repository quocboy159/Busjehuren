using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IEigenschapRepository : IRepository<Eigenschap>
    {
    }

    public class EigenschapRepository : BaseRepository<Eigenschap, BusjehurenDbContext>, IEigenschapRepository
    {
        public EigenschapRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
