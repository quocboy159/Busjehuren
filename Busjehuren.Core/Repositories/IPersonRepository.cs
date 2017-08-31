using Busjehuren.Core.EF;

namespace Busjehuren.Core.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
    }

    public class PersonRepository : BaseRepository<Person, BusjehurenDbContext>, IPersonRepository
    {
        public PersonRepository(BusjehurenDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
