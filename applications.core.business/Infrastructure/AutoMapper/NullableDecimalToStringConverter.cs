namespace Applications.Core.Business
{
    using AutoMapper;

    /// <summary>
    /// Nullable Decimal to String converter. Implements <see cref="ITypeConverter{TSource, TDestination}"/>
    /// </summary>    
    public class NullableDecimalToStringConverter : DecimalToStringConverter, ITypeConverter<decimal?, string>
    {
        /// <summary>
        /// <see cref=""/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(decimal? source, string destination, ResolutionContext context)
        {
            if (!source.HasValue)
            {
                return string.Empty;
            }
            return base.Convert(source.Value, destination, context);
        }
    }
}