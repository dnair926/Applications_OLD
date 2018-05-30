namespace Applications.Core.Infrastructure
{
    using AutoMapper;

    public class ObjectMapper : IObjectMapper
    {
        private readonly IMapper mapper;

        public T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }

        public ObjectMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}