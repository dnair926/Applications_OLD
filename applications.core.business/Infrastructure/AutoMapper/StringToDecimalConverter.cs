namespace Applications.Core.Business
{
    using AutoMapper;

    public class StringToDecimalConverter : ITypeConverter<string, decimal>
    {
        public decimal Convert(string source, decimal destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default(decimal);
            }

            return decimal.TryParse(source, out decimal parsedValue) ? parsedValue : default(decimal);
        }
    }
}