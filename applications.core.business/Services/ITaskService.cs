using Applications.Core.Business.Models;
using Applications.Core.Models;
using System.Collections.Generic;

namespace Applications.Core.Business.Services
{
    public interface ITaskService
    {
        FilteredListInformation<TaskViewModel, TaskCriteria> Get(FilteredListInformation<TaskViewModel, TaskCriteria> listInformation);

        void Delete(TaskViewModel item);

        void Save(TaskViewModel editItem);

        IEnumerable<string> ValidateTask(FormInformation<TaskViewModel> formInformation);
    }
}
