namespace Applications.Core.Business
{
    using AutoMapper;

    public class StringToNullableIntConverter : ITypeConverter<string, int?>
    {
        public int? Convert(string source, int? destination, ResolutionContext context)
        {
            return !string.IsNullOrWhiteSpace(source) && int.TryParse(source, out int parsedValue) ? parsedValue : default(int?);
        }
    }
}