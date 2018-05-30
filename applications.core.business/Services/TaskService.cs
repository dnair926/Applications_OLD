using Applications.Core.Business.Data;
using Applications.Core.Business.Models;
using Applications.Core.Infrastructure;
using Applications.Core.Models;
using Applications.Core.Repository;
using System.Collections.Generic;

namespace Applications.Core.Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryService repositoryService;
        private readonly IObjectMapper objectMapper;
        private readonly IListService<TaskViewModel> listService;

        public TaskService (
            IRepositoryService repositoryService,
            IObjectMapper objectMapper,
            IListService<TaskViewModel> listService)
        {
            this.repositoryService = repositoryService;
            this.objectMapper = objectMapper;
            this.listService = listService;
        }

        public FilteredListInformation<TaskViewModel, TaskCriteria> Get(FilteredListInformation<TaskViewModel, TaskCriteria> listInformation)
        {
            var tasks = repositoryService.GetAll<Task>();
            var items = objectMapper.Map<IEnumerable<TaskViewModel>>(tasks);
            listService.UpdateListInformation(listInformation, items);

            return listInformation;
        }

        public void Delete(TaskViewModel item)
        {
            var task = objectMapper.Map<Task>(item);

            repositoryService.Delete(task);
        }

        public void Save(TaskViewModel item)
        {
            var taskId = item.ID;
            var task = new Task();

            if (taskId > 0)
            {
                task = repositoryService.Get<Task>(t => t.ID == taskId);
            }

            objectMapper.Map(item, task);

            repositoryService.Save(task);
        }

        public IEnumerable<string> ValidateTask(FormInformation<TaskViewModel> formInformation)
        {
            if (formInformation?.Model == null)
            {
                yield return AppConstants.DefaultErrorMessage;
                yield break;
            }

            if (string.IsNullOrWhiteSpace(formInformation.Model.Name))
            {
                yield return "Name is required.";
            }
        }
    }
}