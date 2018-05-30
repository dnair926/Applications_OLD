namespace Applications.Core.Business
{
    using System;
    using AutoMapper;

    public class StringToNullableDateConverter : ITypeConverter<string, DateTime?>
    {
        public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
        {
            return !string.IsNullOrWhiteSpace(source) && DateTime.TryParse(source, out DateTime parsedValue) ? parsedValue : default(DateTime?);
        }
    }
}