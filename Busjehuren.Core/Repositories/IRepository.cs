using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Repositories
{
    public interface IRepository<T> : IRepository<T, int> where T : class
    {
    }

    public interface IRepository<T, IdT> where T : class
    {
        void Add(T e);
        void Remove(T e);
        T Find(IdT id);
        Task<T> FindAsync(IdT id);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null);
        IQueryable<U> FindAll<U>(Expression<Func<U, bool>> predicate = null) where U : T;
    }
}
