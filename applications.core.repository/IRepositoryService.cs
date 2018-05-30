using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Applications.Core.Repository
{
    public interface IRepositoryService
    {
        IEnumerable<T> GetAll<T>() where T : class;

        IEnumerable<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria) where T : class;

        T Get<T>(Expression<Func<T, bool>> criteria) where T : class;

        T Save<T>(T entity) where T : class;

        bool Delete<T>(T entity) where T : class;
    }
}