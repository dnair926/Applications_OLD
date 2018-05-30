namespace Applications.Core.Business
{
    using AutoMapper;
    using System;

    /// <summary>
    /// Nullable Date to String converter. Implements <see cref="ITypeConverter{TSource, TDestination}"/>
    /// </summary>
    public class NullableDateToStringConverter : DateToStringConverter, ITypeConverter<DateTime?, string>
    {
        /// <summary>
        /// <see cref="ITypeConverter{TSource, TDestination}.Convert(TSource, TDestination, ResolutionContext)"/>
        /// </summary>
        /// <returns>
        /// If source object has value, returns converted value <see cref="DateToStringConverter.Convert(DateTime, string, ResolutionContext)"/>.
        /// Otherwise, empty string
        /// </returns>
        public string Convert(DateTime? source, string destination, ResolutionContext context)
        {
            if (!source.HasValue)
            {
                return string.Empty;
            }

            return base.Convert(source.Value, destination, context);
        }
    }
}