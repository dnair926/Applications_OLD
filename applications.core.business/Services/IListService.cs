namespace Applications.Core.Business.Services
{
    using Applications.Core.Business.Models;
    using Applications.Core.Models;
    using System.Collections.Generic;

    public interface IListService<T> where T : BaseListModel, new()
    {
        void UpdateListInformation<TCriteria>(FilteredListInformation<T, TCriteria> listInformation, IEnumerable<T> items) where TCriteria: class, IBaseModel;

        void UpdateListInformation(ListInformation<T> listInformation, IEnumerable<T> items);
    }
}