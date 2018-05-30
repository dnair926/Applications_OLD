using Applications.Core.Business.Models;
using Applications.Core.Business.Services;
using Applications.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IFormService formService;

        public DashboardController(
            ICurrentUserService currentUserService,
            IFormService formService
            )
        {
            this.currentUserService = currentUserService;
            this.formService = formService;
        }

        public IActionResult Get()
        {
            var formInformation = new FormInformation<AssignmentCriteria>()
            {
                Model = new AssignmentCriteria() { PersonID = currentUserService.GetCurrentUserInfo().ID },
            };

            formService.UpdateFormInformation(formInformation);

            return Json(new FilteredListInformation<AssignmentViewModel, AssignmentCriteria>()
            {
                SortDirection = ListSortDirection.Ascending,
                SortExpression = nameof(AssignmentViewModel.AssignedOn),
                EmptyDataMessage = "No Assignments found.",
                AllowManualRefresh = true,
                ShowCount = true,
                Title = "To-Do items",
                ShowFilterForm = true,
                FilterFormInformation = formInformation,
            });
        }
    }
}
