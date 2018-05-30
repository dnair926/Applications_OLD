namespace Applications.Core.Business
{
    using Applications.Core.Business.Data;
    using AutoMapper;

    public class ApplicationPerson : IMap
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Person, ApplicationPerson>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => $"{src.FirstName}{(!string.IsNullOrWhiteSpace(src.MiddleName) ? $" {src.MiddleName}" : "")} {src.LastName}".Trim()));

            profile.CreateMap<Models.Assignment, ApplicationPerson>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => $"{src.AssignedToFirstName}{(!string.IsNullOrWhiteSpace(src.AssignedToMiddleName) ? $" {src.AssignedToMiddleName}" : "")} {src.AssignedToLastName}".Trim()));
        }
    }
}