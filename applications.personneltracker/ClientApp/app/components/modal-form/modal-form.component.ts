import { Component, Input, Output, EventEmitter, Inject } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Subject ,  Observable } from 'rxjs';

import { CommonService } from '../../services/common.service';

@Component({
    selector: "modal-form",
    templateUrl: './modal-form.component.html',
    styleUrls: ['./modal-form.component.css']
})
export class ModalFormComponent {

    private _saveClose = new Subject<any>();
    private _saveAdd = new Subject<any>();
    private _close = new Subject<any>();
    validations: string[] = [];

    constructor(
        private commonService: CommonService,
        private dialogRef: MatDialogRef<ModalFormComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any) {
    }

    private saveDone() {
        this.save(this._saveClose);
    }

    saveclose(): Observable<any> {
        return this._saveClose.asObservable();
    }

    private saveAddMore() {
        this.save(this._saveAdd);
    }

    private save(subject: Subject<any>): Promise<any> {
        var that = this,
            invalidRequestMessage = "An error occurred and cannot complete this request at this time.";
        that.validations = [];

        var apiUrl = that.data.apiUrl;
        if (!apiUrl) {
            that.validations = [invalidRequestMessage];
        }

        return that.commonService
            .post(apiUrl, that.data.formInformation)
            .then(response => {
                if (response.result !== 1 || response.validationMessages) {
                    that.validations = response.validationMessages || [invalidRequestMessage];
                    return;
                }

                that.data.formInformation = response.returnObject;
                subject.next(that.data);
                subject.complete();
            });
    }

    saveadd(): Observable<any> {
        return this._saveAdd.asObservable();
    }

    private closeForm() {
        this._close.next();
        this._close.complete();
    }

    close(): Observable<any> {
        return this._close.asObservable();
    }
}