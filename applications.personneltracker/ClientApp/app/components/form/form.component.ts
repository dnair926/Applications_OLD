import { Component, Input, EventEmitter, Output } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { ModalMessageComponent } from '../modal-message/modal-message.component';
import { IFormInformation } from '../../.d.ts/IFormInformation';
import { IModelChangedEventArgs } from '../../.d.ts/IModelChangedEventArgs';

@Component({
    selector: 'app-form',
    templateUrl: './form.component.html'
})
export class FormComponent {
    @Input()
    formInformation: IFormInformation<any> | undefined;
    @Output()
    modelChanged: EventEmitter<IModelChangedEventArgs> = new EventEmitter<IModelChangedEventArgs>();
    dialogRef!: MatDialogRef<ModalMessageComponent>;

    constructor(
        private dialog: MatDialog
    ) {
    }

    showInfo(message: string) {
        var that = this;
        that.dialogRef = that.dialog.open(ModalMessageComponent, {
            width: '500px',
            data: { title: 'Info', message: message }
        })

        that.dialogRef.componentInstance.close().subscribe(() => {
            that.close();
        });
    }

    multiselectValueChanged(event: any, propertyName: string) {
        if (!this.formInformation) {
            return;
        }

        if (event.srcElement.checked) {
            this.formInformation.model[propertyName].push(event.srcElement.value);
        } else {
            this.formInformation.model[propertyName].splice(this.formInformation.model[propertyName].indexOf(event.srcElement.value), 1);
        }
    }

    modelChange(event: any, propertyName: string) {
        var args: IModelChangedEventArgs = {
            propertyName: propertyName,
            value: this.formInformation ? this.formInformation.model[propertyName] : null
        };
        this.modelChanged.emit(args);
    }

    close() {
        this.dialogRef.close();
    }

}