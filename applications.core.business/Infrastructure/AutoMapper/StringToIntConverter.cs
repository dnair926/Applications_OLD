namespace Applications.Core.Business
{
    using AutoMapper;

    public class StringToIntConverter : ITypeConverter<string, int>
    {
        public int Convert(string source, int destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default(int);
            }
            return int.TryParse(source, out int parsedValue) ? parsedValue : default(int);
        }
    }
}