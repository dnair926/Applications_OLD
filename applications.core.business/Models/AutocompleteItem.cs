namespace Applications.Core.Business
{
    using Applications.Core.Business.Data;
    using AutoMapper;

    public class AutocompleteItem : IMap
    {
        public string DisplayText { get; set; }

        public string Value { get; set; }

        public string SelectedText { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Person, AutocompleteItem>()
                .ForMember(m => m.DisplayText, opt => opt.MapFrom(src => $"{src.FirstName}{(!string.IsNullOrWhiteSpace(src.MiddleName) ? $" {src.MiddleName}" : "")} {src.LastName}"))
                .ForMember(m => m.Value, opt => opt.MapFrom(src => src.ID))
                .ForMember(m => m.SelectedText, opt => opt.MapFrom(src => $"{src.FirstName}{(!string.IsNullOrWhiteSpace(src.MiddleName) ? $" {src.MiddleName}" : "")} {src.LastName}"));
        }
    }
}