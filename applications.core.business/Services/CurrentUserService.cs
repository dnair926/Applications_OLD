namespace Applications.Core.Business.Services
{
    using AutoMapper;
    using Applications.Core;
    using System.Linq;
    using Applications.Core.Repository;
    using Applications.Core.Business.Data;

    public class CurrentUserService : ICurrentUserService
    {

        private IAuthenticationService authenticationService;
        private readonly IRepository<Person> repository;
        private readonly IMapper mapper;

        public CurrentUserService(
            IMapper mapper,
            IAuthenticationService authenticationService,
            IRepository<Person> repository
            )
        {
            this.mapper = mapper;
            this.authenticationService = authenticationService;
            this.repository = repository;
        }

        public ApplicationPerson GetCurrentUserInfo()
        {
            var currentUserId = authenticationService.GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return null;
            }

            var userInfo = repository.Find(p => p.UserID == currentUserId)?.FirstOrDefault();

            return this.mapper.Map<ApplicationPerson>(userInfo);
        }
    }
}