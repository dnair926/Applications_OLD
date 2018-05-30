namespace Applications.Core.Business
{
    using AutoMapper;

    /// <summary>
    /// Int to String converter
    /// </summary>
    public class IntToStringConverter : ITypeConverter<int, string>
    {
        /// <summary>
        /// Convert value from int to string.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns></returns>
        public string Convert(int source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
}