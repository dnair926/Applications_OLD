namespace Applications.PersonnelTracker
{
    using Applications.Core.Business;
    using System;
    using System.Linq;
    using System.Reflection;

    public class AutoMapperStandardProfile : AutoMapper.Profile
    {
        private void LoadStandardMappings()
        {
            CreateMap<DateTime?, string>().ConvertUsing<NullableDateToStringConverter>();
            CreateMap<DateTime, string>().ConvertUsing<DateToStringConverter>();
            CreateMap<string, DateTime>().ConvertUsing<StringToDateConverter>();
            CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateConverter>();
            CreateMap<decimal?, string>().ConvertUsing<NullableDecimalToStringConverter>();
            CreateMap<decimal, string>().ConvertUsing<DecimalToStringConverter>();
            CreateMap<int?, string>().ConvertUsing<NullableIntToStringConverter>();
            CreateMap<int, string>().ConvertUsing<IntToStringConverter>();
            CreateMap<string, int?>().ConvertUsing<StringToNullableIntConverter>();
            CreateMap<string, int>().ConvertUsing<StringToIntConverter>();
            CreateMap<string, Decimal?>().ConvertUsing<StringToNullableDecimalConverter>();
            CreateMap<string, Decimal>().ConvertUsing<StringToDecimalConverter>();
            CreateMap<string, string>().ConvertUsing<StringToStringConverter>();

            var assemblies = new Assembly[]
            {
                typeof(Core.AssemblyHook).GetTypeInfo().Assembly,
                typeof(Core.Repository.AssemblyHook).GetTypeInfo().Assembly,
                typeof(AssemblyHook).GetTypeInfo().Assembly,
                typeof(AutoMapperStandardProfile).GetTypeInfo().Assembly,
            };

            foreach (var assembly in assemblies)
            {
                var maps =
                    (from t in assembly.GetTypes()
                     where typeof(IMap).IsAssignableFrom(t) &&
                           !t.GetTypeInfo().IsAbstract &&
                           !t.GetTypeInfo().IsInterface
                     select (IMap)Activator.CreateInstance(t)).ToArray();

                foreach (var map in maps)
                {
                    map.CreateMap(this);
                }
            }
        }

        public AutoMapperStandardProfile()
        {
            LoadStandardMappings();
        }
    }
}