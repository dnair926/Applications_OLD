namespace Applications.Core.Infrastructure
{
    /// <summary>
    /// Object mapper
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Create a new object from the given object
        /// </summary>
        /// <typeparam name="T">Type of new object to create</typeparam>
        /// <param name="source">Source object to create the new object</param>
        /// <returns>New object of type <typeparamref name="T"/></returns>
        T Map<T>(object source);

        /// <summary>
        /// Map from one object to another
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        void Map(object source, object destination);
    }
}