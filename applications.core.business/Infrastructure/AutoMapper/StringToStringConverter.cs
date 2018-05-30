namespace Applications.Core.Business
{
    using AutoMapper;

    public class StringToStringConverter : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source) && string.IsNullOrWhiteSpace(destination))
            {
                return destination;
            }

            return source;
        }
    }
}