using Applications.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Applications.Core.Repository
{
    public interface IObjectRelationalMapper<T> 
    {
        IEnumerable<T> LoadAll();

        IEnumerable<T> LoadByCriteria(Expression<Func<T, bool>> criteria);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void Save();
    }
}