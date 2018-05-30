using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Applications.Core.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly IObjectRelationalMapper<T> objectRelationalMapper;

        public Repository(IObjectRelationalMapper<T> objectRelationalMapper)
        {
            this.objectRelationalMapper = objectRelationalMapper;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria)
        {
            return objectRelationalMapper.LoadByCriteria(criteria);
        }

        public IEnumerable<T> FindAll()
        {
            return objectRelationalMapper.LoadAll();
        }

        public void Remove(T entity)
        {
            objectRelationalMapper.Remove(entity);
            objectRelationalMapper.Save();
        }

        public void Save(T entity)
        {
            
            objectRelationalMapper.Update(entity);
            objectRelationalMapper.Save();
        }
    }
}