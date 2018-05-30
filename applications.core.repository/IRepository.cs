using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Applications.Core.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> criteria);

        void Save(T entity);

        void Remove(T entity);
    }
}