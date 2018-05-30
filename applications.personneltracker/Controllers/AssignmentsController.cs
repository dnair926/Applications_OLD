using Applications.Core.Business.Models;
using Applications.Core.Business.Services;
using Applications.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentsController: Controller
    {
        private readonly IAssignmentService todoService;

        public AssignmentsController(
            IAssignmentService todoService)
        {
            this.todoService = todoService;
        }

        public IActionResult Get(FilteredListInformation<AssignmentViewModel, AssignmentCriteria> listInformation)
        {
            return Json(todoService.GetWorklistItems(listInformation));
        }
    }
}
