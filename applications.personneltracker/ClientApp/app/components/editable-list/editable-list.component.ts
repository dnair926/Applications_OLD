import {
    Component,
    ViewChild,
    Input,
    OnInit
} from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';

import { ListComponent } from '../list/list.component';
import { ModalFormComponent } from '../modal-form/modal-form.component';

import { CommonService } from '../../services/common.service';

import { IFormInformation } from '../../.d.ts/IFormInformation';
import { IEditableListInformation } from '../../.d.ts/IEditableList';

@Component({
    selector: "editable-list",
    templateUrl: './editable-list.component.html'
})
export class EditableListComponent implements OnInit {
    @ViewChild(ListComponent)
    private listComponent!: ListComponent;
    @ViewChild(ModalFormComponent)
    private formComponent!: ModalFormComponent;
    @Input()
    apiUrl: string = "";

    listInformation: any;
    formInformation: any;
    dialogRef!: MatDialogRef<ModalFormComponent>;

    constructor(
        private commonService: CommonService,
        private dialog: MatDialog
    ) {
    }

    ngOnInit(): void {
        this.initiate();
    }

    onAddItem() {
        this.openForm(null);
    }

    onEditItem(event: any) {
        this.openForm(event);
    }

    private openForm(editItem: any) {
        var that = this;
        this.commonService.get<IFormInformation<Model>>(this.apiUrl + "/forminformation")
            .then(response => {
                if (editItem) {
                    response.model = editItem;
                }
                that.dialogRef = that.dialog.open(ModalFormComponent, {
                    width: '500px',
                    data: { apiUrl: that.apiUrl, formInformation: response }
                })

                that.dialogRef.componentInstance.close().subscribe(() => {
                    that.close();
                });

                that.dialogRef.componentInstance.saveclose().subscribe((formInformation) => {
                    that.save(formInformation, true);
                });

                that.dialogRef.componentInstance.saveadd().subscribe((formInformation) => {
                    that.save(formInformation);
                });
            });
    }

    save(formInformation:any, closeModal: boolean = false) {
        this.listComponent.loadList();

        if (closeModal) {
            this.close();
        }
    }

    close() {
        this.dialogRef.close();
    }

    onItemAdded(event: any) {
        this.listComponent.loadList();
    }

    initiate() {
        if (!this.apiUrl) {
            return;
        }

        return this.commonService
            .get<IEditableListInformation<any>>(this.apiUrl + "/listInformation")
            .then(response => {
                this.listInformation = response.listInformation;
                this.formInformation = response.formInformation;
            });
    }
}

export class Model {

}