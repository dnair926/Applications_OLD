using Applications.Core.Business;
using Applications.Core.Business.Data;
using Applications.Core.Infrastructure;
using Applications.Core.Models;
using Applications.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class ProfilesController : Controller
    {
        private readonly IRepositoryService repositoryService;
        private readonly IObjectMapper mapper;

        public ProfilesController(
            IRepositoryService repositoryService,
            IObjectMapper mapper
            )
        {
            this.repositoryService = repositoryService;
            this.mapper = mapper;
        }

        public IActionResult Get(int id)
        {
            if (id < 1)
            {
                return Json(new ResponseObject()
                {
                    Result = ResultTypes.Error,
                    Message = "Could not retrieve information.",
                });
            }

            var profile = this.repositoryService.Get<Person>(p => p.ID == id);
            var user = this.mapper.Map<ApplicationPerson>(profile);

            if (user == null)
            {
                return Json(new ResponseObject()
                {
                    Result = ResultTypes.Error,
                    Message = AppConstants.DefaultErrorMessage,
                });
            }

            return Json(new ResponseObject()
            {
                Result = ResultTypes.Success,
                ReturnObject = user,
            });
        }
    }
}
