using Applications.Core.Models;
using Applications.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Applications.Core.Business.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IIocContainer container;

        public RepositoryService(
            IIocContainer container)
        {
            this.container = container;
        }

        public T Get<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            var entities = GetByCriteria(criteria);
            return entities != null ? entities.FirstOrDefault() : default(T);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            var repositoryInstance = container.GetInstance<IRepository<T>>();
            if (repositoryInstance == null)
            {
                return null;
            }

            return repositoryInstance.FindAll();
        }

        public IEnumerable<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            var repositoryInstance = container.GetInstance<IRepository<T>>();
            if (repositoryInstance == null)
            {
                return null;
            }

            return repositoryInstance.Find(criteria);
        }

        public T Save<T>(T entity) where T : class
        {
            var repositoryInstance = container.GetInstance<IRepository<T>>();
            if (repositoryInstance == null)
            {
                return default(T);
            }
            
            repositoryInstance.Save(entity);
            return entity;
        }

        public bool Delete<T>(T entity) where T : class
        {
            var repositoryInstance = container.GetInstance<IRepository<T>>();
            if (repositoryInstance == null)
            {
                return false;
            }

            repositoryInstance.Remove(entity);
            return true;
        }
    }
}