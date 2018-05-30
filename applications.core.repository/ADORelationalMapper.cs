using Applications.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Applications.Core.Repository
{
    public class ADORelationalMapper<T> : IObjectRelationalMapper<T> where T : IDataEntity, new()
    {
        public ADORelationalMapper(
            IDataMapper dataMapper,
            ISqlCommandBuilder commandBuilder)
        {
            this.dataMapper = dataMapper;
            this.commandBuilder = commandBuilder;
        }

        public T Add(T entity)
        {
            var results = ExecuteReader(entity, CrudAction.Insert);

            var newEntity = results == null ? entity : results.FirstOrDefault();

            var propertyDescriptors = newEntity.GetProperties();
            var hashTable = propertyDescriptors as Hashtable;

            foreach (var key in hashTable.Keys)
            {
                var descriptor = hashTable[key] as PropertyDescriptor;
                if (descriptor == null)
                {
                    continue;
                }

                descriptor.SetValue(entity, descriptor.GetValue(newEntity));
            }

            entity.SetAsPersisted();

            return entity;
        }

        public IQueryable<T> LoadAll() => LoadByCriteria(null);

        public IQueryable<T> LoadByCriteria(Func<T, bool> criteria) => ExecuteReader(criteria, CrudAction.Select);

        public int Remove(T entity) => ExecuteNonQuery(CrudAction.Delete, entity);

        public int Update(T entity) => ExecuteNonQuery(CrudAction.Update, entity);

        private IQueryable<T> ExecuteReader(Func<T, bool> criteria, CrudAction crudAction)
        {
            using (SqlCommand command = new SqlCommand())
            {
#if DEBUG
                Stopwatch timer = new Stopwatch();
                timer.Start();
#endif
                try
                {
                    commandBuilder.BuildSqlCommand(command, criteria, crudAction);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        IEnumerable<T> entities = this.dataMapper.MapDataToEntities<T>(reader).ToList();
                        return (entities?.Count() ?? 0) > 0 ? entities.ToList().AsQueryable() : null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("Exception: " + ex.Message);
                }
                finally
                {
                    command.Connection.Close();
#if DEBUG
                    timer.Stop();
                    SqlCommandBuilder.PrintCommand(command);
                    Debug.Write($"took {timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}");
#endif
                }
            }
            return null;
        }

        private int ExecuteNonQuery(CrudAction crudAction, T entity)
        {
            if (entity == null)
            {
                return 0;
            }

            using (SqlCommand command = new SqlCommand())
            {
#if DEBUG
                Stopwatch timer = new Stopwatch();
                timer.Start();
#endif
                try
                {
                    commandBuilder.BuildSqlCommand(command, entity, crudAction);
                    return command.ExecuteNonQuery();
                }
                catch
                {
                }
                finally
                {
                    command.Connection.Close();
#if DEBUG
                    timer.Stop();
                    SqlCommandBuilder.PrintCommand(command);
                    Debug.Write($"took {timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}");
#endif
                }
            }

            return 0;
        }

        private readonly IDataMapper dataMapper;
        private readonly ISqlCommandBuilder commandBuilder;
    }
}