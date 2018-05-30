namespace Applications.Core.Business
{
    using System;
    using AutoMapper;

    /// <summary>
    /// Date to String type converter. Implements <see cref="ITypeConverter{TSource, TDestination}"/>
    /// </summary>
    public class DateToStringConverter : ITypeConverter<DateTime, string>
    {
        /// <summary>
        /// Convert a date to string
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Converted value</returns>
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            if (source == DateTime.MinValue)
            {
                return string.Empty;
            }

            var hasTime = source != source.Date;
            var dateFormat = hasTime ? "MM/dd/yyyy hh:mm:ss tt" : "MM/dd/yyyy";
            return source.ToString(dateFormat);
        }
    }
}