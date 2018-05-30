namespace Applications.PersonnelTracker
{
    using AutoMapper;
    using StructureMap;

    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperStandardProfile());
                
            });

            For<MapperConfiguration>()
                .Use(config);

            For<IMapper>()
                .Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));

        }
    }
}