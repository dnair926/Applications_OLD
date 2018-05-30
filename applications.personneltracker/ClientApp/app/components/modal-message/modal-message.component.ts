import { Component, Input, Output, EventEmitter, Inject } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Subject ,  Observable } from 'rxjs';

@Component({
    selector: "modal-form",
    templateUrl: './modal-message.component.html',
    styleUrls: ['./modal-message.component.css']
})
export class ModalMessageComponent {

    private _close = new Subject<any>();
    validations: string[] = [];

    constructor(
        private dialogRef: MatDialogRef<ModalMessageComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any) {
    }

    private closeModal() {
        this._close.next();
        this._close.complete();
    }

    close(): Observable<any> {
        return this._close.asObservable();
    }
}