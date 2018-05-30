namespace Applications.Core.Business.Services
{
    using System.Collections.Generic;
    using Applications.Core;

    public interface IModelService
    {
        void ProcessModel<TModel>(TModel model) where TModel : class, IBaseModel;

        void ProcessModel<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel;

        IEnumerable<KeyValuePair<string, object>> GetCurrentValues<TModel>(TModel model) where TModel : class, IBaseModel;
    }
}