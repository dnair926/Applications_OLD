namespace Applications.Core.Business
{

    using AutoMapper;

    public class NullableIntToStringConverter : IntToStringConverter, ITypeConverter<int?, string>
    {
        public string Convert(int? source, string destination, ResolutionContext context)
        {
            if (!source.HasValue)
            {
                return string.Empty;
            }

            return base.Convert(source.Value, destination, context);
        }
    }
}