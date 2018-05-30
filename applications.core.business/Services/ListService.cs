namespace Applications.Core.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Applications.Core.Models;
    using Humanizer;
    using Applications.Core.Attributes;
    using System.ComponentModel.DataAnnotations;
    using Applications.Core.Business.Models;

    public class ListService<T> : IListService<T>
        where T : BaseListModel, new()
    {
        private readonly IModelService modelService;
        private readonly IFormService formService;

        public ListService(
            IModelService modelService,
            IFormService formService)
        {
            this.modelService = modelService;
            this.formService = formService;
        }

        /// <summary>
        /// Process list information
        /// </summary>
        /// <param name="listInformation">ListInformation[of T]<seealso cref="ListInformation{T}"/></param>
        /// <param name="items">List items to be processed</param>
        public void UpdateListInformation<TCriteria>(FilteredListInformation<T, TCriteria> listInformation, IEnumerable<T> items) where TCriteria: class, IBaseModel
        {
            if (listInformation == null)
            {
                return;
            }

            UpdateListInformation((ListInformation<T>)listInformation, items);

            formService.UpdateFormInformation(listInformation.FilterFormInformation);
        }

        /// <summary>
        /// Process list information
        /// </summary>
        /// <param name="listInformation">ListInformation[of T]<seealso cref="ListInformation{T}"/></param>
        /// <param name="items">List items to be processed</param>
        public void UpdateListInformation(ListInformation<T> listInformation, IEnumerable<T> items)
        {
            if (listInformation == null)
            {
                return;
            }

            this.modelService.ProcessModel(items);

            items = this.SortList(listInformation, items);

            items = this.SliceList(listInformation, items);

            this.SetListProperties(listInformation, items);

            listInformation.Items = items;
        }

        private void SetListProperties(ListInformation<T> listInformation, IEnumerable<T> items)
        {
            if ((items?.Count() ?? 0) == 0 || listInformation == null)
            {
                return;
            }

            listInformation.ShowEdit = items.Where(item => item.ShowEdit)?.Count() > 0;
            listInformation.ShowRemove = items.Where(item => item.ShowRemove)?.Count() > 0;
            listInformation.Columns = GetColumnInformation(items)?.OrderBy(c => c.DisplayOrder)?.ToList();
        }

        private IEnumerable<ColumnInformation> GetColumnInformation(IEnumerable<T> items)
        {
            if ((items?.Count() ?? 0) == 0)
            {
                yield break;
            }

            var properties = new T().GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            foreach (var key in properties.Keys)
            {
                var propertyDescriptor = properties[key] as PropertyDescriptor;
                if (propertyDescriptor == null)
                {
                    continue;
                }

                var columnInfo = propertyDescriptor.Attributes[typeof(ListColumnAttribute)] as ListColumnAttribute;
                if (columnInfo == null)
                {
                    continue;
                }

                if (columnInfo.HideEmptyColumn)
                {
                    var allRowsHaveValue = items.Where(i => i.GetPropertyValue(propertyDescriptor.Name) != null).Count() > 0;
                    if (!allRowsHaveValue)
                    {
                        continue;
                    }
                }

                var profileIdentifierColumn = "";
                if (propertyDescriptor.Attributes[typeof(ProfileDescriptorAttribute)] is ProfileDescriptorAttribute profileDescriptor)
                {
                    profileIdentifierColumn = profileDescriptor.IdentifierPropertyName;
                }
                else
                {
                    var profileIdentifier = propertyDescriptor.Attributes[typeof(ProfileIdentifierAttribute)] as ProfileIdentifierAttribute;
                    profileIdentifierColumn = profileIdentifier != null ? propertyDescriptor.Name : "";
                }

                var displayAttribute = propertyDescriptor.Attributes[typeof(DisplayAttribute)] as DisplayAttribute;
                yield return new ColumnInformation()
                {
                    ColumnName = propertyDescriptor.Name.Camelize(),
                    HeaderText = !string.IsNullOrWhiteSpace(columnInfo?.HeaderText) ? columnInfo.HeaderText : !string.IsNullOrWhiteSpace(displayAttribute?.Name) ? displayAttribute.Name : propertyDescriptor.Name,
                    Sortable = columnInfo?.EnableSorting ?? false,
                    SortColumnName = !string.IsNullOrWhiteSpace(columnInfo?.SortColumnName) ? columnInfo.SortColumnName : propertyDescriptor.Name,
                    DisplayOrder = columnInfo.DisplayOrder,
                    ProfileIdentifierColumn = !string.IsNullOrWhiteSpace(profileIdentifierColumn) ? profileIdentifierColumn.Camelize() : "",
                };
            }
        }

        private IEnumerable<T> SliceList(ListInformation<T> listInformation, IEnumerable<T> items)
        {
            if (listInformation == null || (items?.Count() ?? 0) == 0)
            {
                return items;
            }

            listInformation.Pager = listInformation.Pager ?? new PagerInformation();
            listInformation.Pager.TotalItems = items.Count();
            this.SetPagerInformation(listInformation);

            var pageSize = listInformation?.Pager?.PageSize ?? 0;
            return pageSize > 0 ? items?
                .Skip(listInformation.Pager.StartItemIndex - 1)
                .Take(listInformation.Pager.PageSize)
                .ToList() : items;
        }

        private void SetPagerInformation(ListInformation<T> listInformation)
        {
            if (listInformation == null)
            {
                return;
            }

            var pager = listInformation.Pager ?? new PagerInformation();

            var pageSize = pager.PageSize > 0 ? pager.PageSize :
                pager.TotalItems > 0 ? pager.TotalItems : 1;

            var totalItems = pager.TotalItems;
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var currentPage = pager.CurrentPage > 0 ? pager.CurrentPage : 1;
            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            pager.StartItemIndex = ((currentPage - 1) * pageSize) + 1;
            pager.EndItemIndex = currentPage == totalPages ? totalItems : ((currentPage - 1) * pageSize) + pageSize;
            pager.TotalItems = totalItems;
            pager.CurrentPage = currentPage;
            pager.PageSize = pageSize;
            pager.TotalPages = totalPages;
            pager.StartPage = startPage;
            pager.EndPage = endPage;
            pager.Pages = totalPages > 1 ? Enumerable.Range(start: startPage, count: endPage - startPage + 1).ToList() : null;
            listInformation.Pager = pager;
        }

        private IEnumerable<T> SortList(ListInformation<T> listInformation, IEnumerable<T> items)
        {
            if ((items?.Count() ?? 0) == 0)
            {
                return items;
            }

            this.SetSortExpression(listInformation);

            var sortField = listInformation?.SortExpression;
            if (string.IsNullOrWhiteSpace(sortField))
            {
                return items;
            }

            PropertyDescriptor propertyDescriptor = new T().GetProperty(sortField);
            if (propertyDescriptor == null)
            {
                return items;
            }

            var sortDescending = listInformation.SortDirection == Models.ListSortDirection.Descending;

            return sortDescending ?
                items.OrderByDescending(_ => propertyDescriptor.GetValue(_)) :
                items.OrderBy(_ => propertyDescriptor.GetValue(_));
        }

        private void SetSortExpression(ListInformation<T> listInformation)
        {
            if (listInformation == null)
            {
                return;
            }

            var currentSortExpression = listInformation.SortExpression ?? "";
            var newSortExpression = listInformation.NewSortExpression ?? "";
            listInformation.NewSortExpression = "";
            var sortingInfoMissing = string.IsNullOrWhiteSpace(currentSortExpression) &&
                string.IsNullOrWhiteSpace(newSortExpression);
            if (sortingInfoMissing)
            {
                listInformation.SortExpression = listInformation.DefaultSortExpression;
                listInformation.SortDirection = listInformation.DefaultSortDirection;
                return;
            }

            var sorting = !string.IsNullOrWhiteSpace(newSortExpression);
            if (!sorting)
            {
                return;
            }

            var sortFieldChanged = !string.IsNullOrWhiteSpace(newSortExpression) &&
                !string.Equals(newSortExpression, currentSortExpression, StringComparison.OrdinalIgnoreCase);
            if (sortFieldChanged)
            {
                listInformation.SortExpression = newSortExpression;
                listInformation.SortDirection = Models.ListSortDirection.Ascending;
                return;
            }

            listInformation.SortDirection = listInformation.SortDirection == Models.ListSortDirection.Ascending ? Models.ListSortDirection.Descending : Models.ListSortDirection.Ascending;
        }
    }
}