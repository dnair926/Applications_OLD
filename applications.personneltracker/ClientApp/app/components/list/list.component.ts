import { Component, Input, Output, OnInit, EventEmitter } from "@angular/core";

import * as Immutable from 'immutable';

import { CommonService } from '../../services/common.service';
import { IResponseObject } from '../../.d.ts/IResponseObject';
import { CommonUtilities } from "../../utilities/common.utility";
import { AlertService } from '../alert/alert.service';

import { IAlert, AlertType } from '../alert/alert.d';
import { ResponseObject } from '../../services/response-object.d';

import { IList } from '../../.d.ts/IList';
import { Observable } from "rxjs/internal/Observable";

export enum ListStates {
    Unspecified,
    Loading,
    Loaded
}

@Component({
    selector: "list",
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
    @Input()
    url: string = "";
    @Input()
    listInfo: IList | undefined;

    @Output()
    editItem: EventEmitter<any> = new EventEmitter<any>();

    @Output()
    addItem: EventEmitter<any> = new EventEmitter<any>();

    loading: boolean = false;
    loaded: boolean = false;
    listState!: ListStates;

    constructor(private listService: CommonService, private alertService: AlertService) {
    }

    ngOnInit(): void {
        if (this.listInfo && !this.listInfo.items) {
            this.loadList();
        }
    }

    refreshList() {
        this.loadList();
    }

    loadList() {
        this.loading = true;
        this.loaded = false;

        var url = CommonUtilities.convertJsonToQuery(this.url, this.listInfo);
        this.listService.get(url)
            .then(this.listLoaded.bind(this))
            .catch(this.listLoadFailed.bind(this));
    }

    onPaging(page: number) {
        if (!this.listInfo) {
            return;
        }

        this.listInfo.pager.currentPage = page;
        this.loadList();
    }

    sort(sortExpression: string) {
        if (!this.listInfo) {
            return;
        }

        this.listInfo.newSortExpression = sortExpression;
        this.loadList();
    }

    getValue(item:any, propertyName: string) {
        return item[propertyName];
    }

    listLoaded(list: any) {
        this.listLoadCompleted(list);
    }

    listLoadFailed(error: any) {
        this.listLoadCompleted(null);
    }

    listLoadCompleted(list: any) {
        this.loading = false;
        this.loaded = true;
        this.listState = ListStates.Loaded;
        this.listInfo = list;
        if (this.listInfo) {
            var currentDate = new Date(),
                dateTimeFormatted = currentDate.toLocaleDateString() + ' ' + currentDate.toLocaleTimeString();
            this.listInfo.loadTime = dateTimeFormatted;
        }
    }

    remove(item: any) {
        var confirmation = confirm(item.removeConfirmation || (this.listInfo && this.listInfo.removeConfirmation) || "Are you sure you want to remove this item?");
        if (!confirmation) {
            return;
        }

        var self = this;
        self.listService
            .delete(this.url, item)
            .then(this.removed);
    }

    private removed(data: IResponseObject) {
        
        let self = this,
            alert: IAlert = {
                alertType: data && data.alert && data.alert.alertType ? data.alert.alertType :
                    AlertType.Success,
                message: data && data.alert && data.alert.message ? data.alert.message :
                    self.listInfo && self.listInfo.removedMessage ? self.listInfo.removedMessage :
                        "Item has been removed"
            };
        self.alertService.add(alert);
        self.loadList();
    }

    edit(item: any) {
        var itemImmuted = Immutable.Map(item);
        this.editItem.emit(itemImmuted.toObject());
    }

    add() {
        this.addItem.emit();
    }

}