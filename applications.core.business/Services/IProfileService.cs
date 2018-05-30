namespace Applications.Core.Business.Services
{
    using System.Collections.Generic;
    using Applications.Core;
    using Applications.Core.Business.Data;

    public interface IProfileService
    {
        IEnumerable<Person> GetProfilesByIds(IEnumerable<int> ids);

        void SetProfileDescriptors<TModel>(TModel model) where TModel : class, IBaseModel;

        void SetProfileDescriptors<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel;
    }
}