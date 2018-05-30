using Applications.Core.Business.Models;
using Applications.Core.Business.Services;
using Applications.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/person")]
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        private readonly IFormService formService;

        public PersonController(
            IPersonService personService,
            IFormService formService)
        {
            this.personService = personService;
            this.formService = formService;
        }

        [HttpGet]
        [Route("forminformation")]
        public IActionResult GetFormInformation()
        {
            var viewModel = new PersonViewModel();
            personService.SetLookupValues(viewModel);
            var formInformation = new FormInformation<PersonViewModel>()
            {
                Model = viewModel
            };
            formService.UpdateFormInformation(formInformation);

            return Json(new
            {
                FormInformation = formInformation,
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody]FormInformation<PersonViewModel> formInformation)
        {
            personService.Save(formInformation);

            return Json(new
            {
                FormInformation = formInformation,
            });
        }
    }
}
