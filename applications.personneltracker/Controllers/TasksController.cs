using Applications.Core.Business;
using Applications.Core.Business.Models;
using Applications.Core.Business.Services;
using Applications.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Applications.PersonnelTracker.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IFormService formService;

        public TasksController(ITaskService taskService, IFormService formService)
        {
            this.taskService = taskService;
            this.formService = formService;
        }

        [Route("listInformation")]
        public IActionResult TaskListInformation()
        {
            var result = new
            {
                ListInformation = new FilteredListInformation<TaskViewModel, TaskCriteria>()
                {
                    AllowManualRefresh = true,
                    DefaultSortExpression = nameof(TaskViewModel.Description),
                    EmptyDataMessage = "No tasks found.",
                    ShowAddButton = true,
                    AddButtonText = "Add Task",
                    ShowCount = true,
                    ShowEdit = true,
                    ShowRemove = true,
                    Title = "Tasks",
                },
            };
            return base.Json(result);
        }

        [HttpGet]
        public IActionResult Get(FilteredListInformation<TaskViewModel, TaskCriteria> listInformation)
        {
            return Json(taskService.Get(listInformation));
        }

        [Route("forminformation")]
        public IActionResult GetEditForm(FormInformation<TaskViewModel> formInformation)
        {
            formInformation = formInformation ?? new FormInformation<TaskViewModel>();
            formInformation.Model = formInformation.Model ?? new TaskViewModel();
            formInformation.Title = "Add Task";

            formService.UpdateFormInformation(formInformation);

            return Json(formInformation);
        }

        [HttpPost]
        public IActionResult Post([FromBody]FormInformation<TaskViewModel> formInformation)
        {
            if (formInformation == null)
            {
                return Json(new ResponseObject()
                {
                    Result = ResultTypes.Error,
                    ValidationMessages = new string[]
                    {
                        AppConstants.DefaultErrorMessage,
                    },
                });
            }

            var validations = taskService.ValidateTask(formInformation)?.ToList();
            if ((validations?.Count ?? 0) > 0)
            {
                return Json(new ResponseObject()
                {
                    Result = ResultTypes.Invalid,
                    ValidationMessages = validations
                });
            }

            taskService.Save(formInformation.Model);

            var addedTaskName = formInformation.Model.Name;
            formInformation.Model = new TaskViewModel();

            return Json(new ResponseObject()
            {
                Result = ResultTypes.Success,
                Alert = new Alert()
                {
                    AlertType = AlertType.Success,
                    Message = $"Task \"{addedTaskName}\" has been saved.",
                },
                ReturnObject = formInformation,
            });
        }

        [HttpDelete]
        public IActionResult Delete(TaskViewModel item)
        {
            taskService.Delete(item);

            return Json(new ResponseObject()
            {
                Result = ResultTypes.Success,
                Alert = new Alert()
                {
                    AlertType = AlertType.Success,
                    Message = $"Task \"{item.Name}\" has been removed.",
                },
            });
        }
    }
}
