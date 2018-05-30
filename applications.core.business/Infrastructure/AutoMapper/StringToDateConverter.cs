namespace Applications.Core.Business
{
    using AutoMapper;
    using System;

    public class StringToDateConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return DateTime.MinValue;
            }

            return DateTime.TryParse(source, out DateTime parsedValue) ? parsedValue : DateTime.MinValue;
        }
    }
}