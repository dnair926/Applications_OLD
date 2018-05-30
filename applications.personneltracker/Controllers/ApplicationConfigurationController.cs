using Applications.PersonnelTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationConfigurationController : Controller
    {
        private readonly ApplicationSetting applicationSetting;

        public ApplicationConfigurationController(IOptions<ApplicationSetting> applicationSettings)
        {
            this.applicationSetting = applicationSettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(applicationSetting);
        }
    }
}
