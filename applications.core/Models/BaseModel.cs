namespace Applications.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Base model class
    /// </summary>
    public class BaseModel : IBaseModel
    {
        Hashtable entityProperties;

        /// <summary>
        /// <see cref="IBaseModel.GetProperties()"/>
        /// </summary>
        public Hashtable GetProperties()
        {
            if (entityProperties != null)
            {
                return entityProperties;
            }

            entityProperties = new Hashtable();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
            foreach (PropertyDescriptor info in properties)
            {
                if (info.IsReadOnly)
                {
                    continue;
                }

                entityProperties[info.Name.ToUpperInvariant()] = info;
            }

            return entityProperties;
        }

        /// <summary>
        /// <see cref="IBaseModel.GetPropertyNames()"/>
        /// </summary>
        public IEnumerable<string> GetPropertyNames()
        {
            var properties = GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            foreach (var key in properties.Keys)
            {
                yield return key.ToString();
            }
        }

        /// <summary>
        /// <see cref="IBaseModel.GetProperty(string)"/>
        /// </summary>
        public PropertyDescriptor GetProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return null;
            }

            var properties = GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                return null;
            }

            propertyName = propertyName.ToUpperInvariant();
            return properties[propertyName] as PropertyDescriptor;
        }

        /// <summary>
        /// <see cref="IBaseModel.GetValidValues()"/>
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> GetValidValues()
        {
            var propertyDescriptors = GetProperties();
            foreach (var key in propertyDescriptors.Keys)
            {
                var propertyName = key.ToString();
                var value = GetPropertyValue(propertyName);
                if (value == null)
                {
                    continue;
                }

                yield return new KeyValuePair<string, object>(propertyName, value);
            }
        }

        /// <summary>
        /// <see cref="GetPropertyValue(string)"/>
        /// </summary>
        public object GetPropertyValue(string propertyName)
        {
            PropertyDescriptor propertyInfo = GetProperty(propertyName);
            if (propertyInfo == null)
            {
                return null;
            }
            object fieldValue = new object();
            object propertyValue = null;
            Type propertyType = null;

            propertyValue = propertyInfo.GetValue(this);
            var validValue = true;
            propertyType = propertyInfo.PropertyType;
            if (propertyValue == null)
            {
                validValue = false;
            }
            else if (propertyType == typeof(string))
            {
                validValue &= !string.IsNullOrWhiteSpace(propertyValue.ToString());
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                validValue &= (int.TryParse(propertyValue.ToString(), out int valueParsed) && valueParsed != 0);
            }
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
            {
                validValue &= (decimal.TryParse(propertyValue.ToString(), out decimal valueParsed) && valueParsed != 0);
            }
            else if (propertyType == typeof(short) || propertyType == typeof(short?))
            {
                validValue &= (short.TryParse(propertyValue.ToString(), out short valueParsed) && valueParsed != 0);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                validValue &= (DateTime.TryParse(propertyValue.ToString(), out DateTime valueParsed) && valueParsed != DateTime.MinValue);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                validValue &= (bool.TryParse(propertyValue.ToString(), out bool valueParsed) && valueParsed);
            }

            return validValue ? propertyValue : null;
        }

        /// <summary>
        /// <see cref="IBaseModel.SetPropertyValue(string, object)"/>
        /// </summary>
        public void SetPropertyValue(string propertyName, object propertyValue)
        {
            PropertyDescriptor propertyInfo = GetProperty(propertyName);
            if (propertyInfo == null)
            {
                return;
            }

            propertyInfo.SetValue(this, propertyValue);
        }
    }
}