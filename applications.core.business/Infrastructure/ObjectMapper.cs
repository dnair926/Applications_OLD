using AutoMapper;

namespace Applications.Core.Business
{
    public class ObjectMapper : Infrastructure.IObjectMapper
    {
        readonly IMapper mapper;

        public T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }

        public void Map(object source, object destination)
        {
            mapper.Map(source, destination);
        }

        public ObjectMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}