using Applications.Core.Business;
using Applications.Core.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class CurrentUserController : Controller
    {
        private readonly ICurrentUserService currentUserService;

        public CurrentUserController(
            ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }

        [HttpGet("[action]")]
        public ApplicationPerson Get()
        {
            return currentUserService.GetCurrentUserInfo();
        }
    }
}