using Busjehuren.Core.EF;
using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IContentRepository : IRepository<Content>
    {
    }

    public class ContentRepository : BaseRepository<Content, BusjehurenDbContext>, IContentRepository
    {
        public ContentRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
