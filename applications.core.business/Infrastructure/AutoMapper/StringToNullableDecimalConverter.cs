namespace Applications.Core.Business
{
    using AutoMapper;

    public class StringToNullableDecimalConverter : ITypeConverter<string, decimal?>
    {
        public decimal? Convert(string source, decimal? destination, ResolutionContext context)
        {
            return !string.IsNullOrWhiteSpace(source) && decimal.TryParse(source, out decimal parsedValue) ? parsedValue : default(decimal?);
        }
    }
}