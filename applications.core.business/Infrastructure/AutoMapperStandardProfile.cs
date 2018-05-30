namespace Applications.Fox.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Applications.Core.Infrastructure.Mapper;

    public class AutoMapperStandardProfile : AutoMapper.Profile
    {
        private void LoadStandardMappings()
        {
            var maps =
                (from t in typeof(AutoMapperStandardProfile).GetTypeInfo().Assembly.GetTypes()
                 where typeof(IMap).IsAssignableFrom(t) &&
                       !t.GetTypeInfo().IsAbstract &&
                       !t.GetTypeInfo().IsInterface
                 select (IMap)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMap(this);
            }
        }

        public AutoMapperStandardProfile()
        {
            LoadStandardMappings();
        }
    }
}