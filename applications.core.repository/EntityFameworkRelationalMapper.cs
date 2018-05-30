using Applications.Core.Infrastructure;
using Applications.Core.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Applications.Core.Repository
{
    public class EntityFameworkRelationalMapper<T> : IObjectRelationalMapper<T> where T: class, IEntity, new()
    {
        private readonly DbContext context;
        private readonly IObjectMapper objectMapper;

        public EntityFameworkRelationalMapper (DbContext context, IObjectMapper objectMapper)
        {
            this.context = context;
            this.objectMapper = objectMapper;
        }

        public void Add(T entity) 
        {
            context.Set<T>().Add(entity);
        }

        public IEnumerable<T> LoadAll() 
        {
            return LoadByCriteria(null);
        }

        public IEnumerable<T> LoadByCriteria(Expression<Func<T, bool>> criteria) 
        {
            return Load(criteria)?.ToList();
        }

        public void Remove(T entity) 
        {
            var entityToDelete = new T() { ID = entity.ID };
            //var dbSet = context.Set<T>();
            //if (context.Entry(entity).State == EntityState.Detached)
            //{
            //    dbSet.Attach(entity);
            //}

            context.Entry(entityToDelete).State = EntityState.Deleted;
            //dbSet.Remove(entityToDelete);
        }

        public void Update(T entity) 
        {
            if (entity.ID > 0)
            {
                context.Update(entity);
            }
            else
            {
                context.Add(entity);
            }
        }

        private IQueryable<T> Load(Expression<Func<T, bool>> criteria) 
        {
            var query = context.Set<T>().AsNoTracking();
            if (criteria != null)
            {
                return query.AsNoTracking().Where(criteria);
            }

            return query;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }
        }


    }
}