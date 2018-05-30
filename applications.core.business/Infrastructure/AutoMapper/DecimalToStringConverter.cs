namespace Applications.Core.Business
{
    using AutoMapper;

    /// <summary>
    /// Decimal to string type converter. Implements <see cref="ITypeConverter{TSource, TDestination}"/>
    /// </summary>
    public class DecimalToStringConverter : ITypeConverter<decimal, string>
    {
        public string Convert(decimal source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
}