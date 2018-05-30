namespace Applications.Core.Models
{
    /// <summary>
    /// Solution container for Inversion of Control containers
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// Get instance of from the Ioc Container
        /// </summary>
        /// <typeparam name="T">Type of the object to get intsance of</typeparam>
        /// <returns>Object of type <typeparamref name="T"/></returns>
        T GetInstance<T>();
    }
}