namespace Applications.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Base model interface
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Get class properties
        /// </summary>
        /// <returns>Properties as <see cref="Hashtable"/></returns>
        Hashtable GetProperties();

        /// <summary>
        /// Get class property names
        /// </summary>
        /// <returns>Property names as <see cref="IEnumerable{String}"/></returns>
        IEnumerable<string> GetPropertyNames();

        /// <summary>
        /// Get a class property
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Property as <see cref="PropertyDescriptor"/></returns>
        PropertyDescriptor GetProperty(string propertyName);

        /// <summary>
        /// Get a property value
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Property value as <see cref="object"/></returns>
        object GetPropertyValue(string propertyName);

        /// <summary>
        /// Set a property value
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="propertyValue">Value to set</param>
        void SetPropertyValue(string propertyName, object propertyValue);

        /// <summary>
        /// Get valid values for all properties
        /// </summary>
        /// <returns>Property name and value as <see cref="System.Collections.Generic.IEnumerable{KeyValuePair{String, object}}"/></returns>
        IEnumerable<KeyValuePair<string, object>> GetValidValues();
    }
}