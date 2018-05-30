using Applications.Core.Business.Models;
using Applications.Core.Models;

namespace Applications.Core.Business.Services
{
    public interface IPersonService
    {
        void SetLookupValues(PersonViewModel viewModel);
        void Save(FormInformation<PersonViewModel> formInformation);
    }
}
