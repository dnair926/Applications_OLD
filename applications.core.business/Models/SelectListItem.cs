using Applications.Core.Business.Data;
using AutoMapper;

namespace Applications.Core.Business.Models
{
    public class SelectListItem: IMap
    {
        public string Value { get; set; }

        public string Text { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<NamePrefix, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(m => m.Value, opt => opt.MapFrom(src => src.ID));

            profile.CreateMap<NameSuffix, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(m => m.Value, opt => opt.MapFrom(src => src.ID));
        }
    }
}
