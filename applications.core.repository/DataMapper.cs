using Applications.Core;
using Applications.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Applications.Core.Repository
{
    public class DataMapper : IDataMapper
    {
        public DataMapper()
        {
        }

        public IEnumerable<T> MapDataToEntities<T>(IDataReader dataReader) where T : IDataEntity, new()
        {
            Hashtable hashtable = new T().GetProperties();

            while (dataReader.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    PropertyDescriptor info = GetProperty<T>(dataReader.GetName(index).ToString(), hashtable);
                    if (info == null)
                    {
                        continue;
                    }

                    var value = dataReader.GetValue(index);
                    if (value == DBNull.Value)
                    {
                        continue;
                    }

                    try
                    {
                        info.SetValue(newObject, value);
                    }
                    catch (Exception ex)
                    {
                        Debug.Write($"Exception: {ex.Message}: Name = {info?.Name}, Value: {value}");
                    }
                    finally
                    {
                    }
                }
                newObject.SetAsPersisted();
                yield return newObject;
            }
            dataReader.Close();

            yield break;
        }

        private PropertyDescriptor GetProperty<T>(string name, Hashtable properties)
        {
            if (string.IsNullOrWhiteSpace(name) || properties == null)
            {
                return null;
            }

            if (properties[name.ToUpperInvariant()] is PropertyDescriptor descriptor)
            {
                return descriptor;
            }

            DataFieldAttribute dataFieldAttribute = null;
            foreach (var key in properties.Keys)
            {
                descriptor = properties[key] as PropertyDescriptor;
                if (descriptor == null)
                {
                    continue;
                }

                dataFieldAttribute = descriptor.Attributes[typeof(DataFieldAttribute)] as DataFieldAttribute;
                if (!string.Equals((dataFieldAttribute?.RepositoryName?? ""), name, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                return descriptor;
            }

            return null;
        }
    }
}