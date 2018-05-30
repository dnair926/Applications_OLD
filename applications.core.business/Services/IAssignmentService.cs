using Applications.Core.Business.Models;
using Applications.Core.Models;

namespace Applications.Core.Business.Services
{
    public interface IAssignmentService
    {
        FilteredListInformation<AssignmentViewModel, AssignmentCriteria> GetWorklistItems(FilteredListInformation<AssignmentViewModel, AssignmentCriteria> listInformation);
    }
}
