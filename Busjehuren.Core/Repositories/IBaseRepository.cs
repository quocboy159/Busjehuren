using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using System.Reflection;
using Busjehuren.Core.Entity;

namespace Busjehuren.Core.Repositories
{
    public abstract class BaseRepository<T, DbContextT> : BaseRepository<T, int, DbContextT>, IRepository<T>
        where T : class, IEntity
        where DbContextT : DbContext
    {
        public BaseRepository(DbContextT context)
            : base(context)
        {
        }

        public override T Find(int id)
        {
            return Collection.FirstOrDefault(e => id == e.Id);
            //return Collection.AsEnumerable().FirstOrDefault(e => e.GetType().GetProperty("Id").GetValue(e).ToString().Equals(id.ToString()));
        }

        public override Task<T> FindAsync(int id)
        {
            return Collection.FirstOrDefaultAsync(e => id == e.Id);
        }
    }

    public abstract class BaseRepository<T, IdT, DbContextT> : IRepository<T, IdT>
        where T : class, IEntity<IdT>
        where IdT : IEquatable<IdT>
        where DbContextT : DbContext
    {
        public BaseRepository(DbContextT context)
        {
            this.Context = context;
            this.Collection = context.Set<T>();
        }

        protected DbContextT Context { get; private set; }
        protected IDbSet<T> Collection { get; private set; }

        public virtual void Add(T e)
        {
            Collection.Add(e);
        }

        public virtual void Remove(T e)
        {
            Collection.Remove(e);
        }

        public virtual T Find(IdT id)
        {
            //return Collection.AsEnumerable().FirstOrDefault(e => e.GetType().GetProperty("Id").GetValue(e).ToString().Equals(id.ToString()));
            return Collection.FirstOrDefault(e => e.Id.Equals(id));
        }

        public virtual Task<T> FindAsync(IdT id)
        {
            //return Collection.FirstOrDefaultAsync(e => e.GetType().GetProperty("Id").GetValue(e).ToString().Equals(id.ToString()));
            return Collection.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null) return Collection;

            return Collection.AsExpandable().Where(predicate);
        }

        public virtual IQueryable<U> FindAll<U>(Expression<Func<U, bool>> predicate = null) where U : T
        {
            if (predicate == null) return Collection.OfType<U>();

            return Collection.AsExpandable().OfType<U>().Where(predicate);
        }
    }
}
