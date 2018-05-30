using Applications.Core.Business.Models;

namespace Applications.Core.Business.Services
{
    public interface IFormService
    {
        void UpdateFormInformation<T>(FormInformation<T> formInformation) where T : class, IBaseModel;
    }
}