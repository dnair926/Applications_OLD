import { IFormInformation } from './IFormInformation';
import { IListItem } from './IListItem';
import { IListColumn } from './IListColumn';
import { SortDirection } from './SortDirection';

export interface IList {
    listId: string;
    autocollapseWhenEmpty: boolean;
    collapsible: boolean;
    title: string;
    message: string;
    allowManualRefresh: boolean;
    showCount: boolean;
    loadTime: string;
    showFilterForm: boolean;
    sortExpression: string;
    newSortExpression: string;
    sortColumnName: string;
    sortDirection: SortDirection;
    showEdit: boolean;
    showRemove: boolean;
    showFooter: boolean;
    removeConfirmation: string;
    removedMessage: string;
    footerItem: IListItem
    items: Array<IListItem>;
    showHeader: boolean;
    columns: Array<IListColumn>;
    filterFormInformation: IFormInformation<any>;
    pager: {
        totalItems: number;
        currentPage: number;
    }
}
