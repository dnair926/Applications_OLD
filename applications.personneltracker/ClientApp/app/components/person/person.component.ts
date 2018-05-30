import { Component, Input } from '@angular/core';

import { CommonService } from '../../services/common.service';
import { ModelService } from '../../services/model.service';

import { IFormInformation } from '../../.d.ts/IFormInformation';
import { IField } from '../../.d.ts/IField';
import { IPageModel } from '../../.d.ts/IPageModel';
import { IModelChangedEventArgs } from '../../.d.ts/IModelChangedEventArgs';

@Component({
    selector: 'person',
    templateUrl: 'person.component.html'
})
export class PersonComponent {
    pageModel!: IPageModel;

    @Input()
    apiUrl: string = 'api/person';

    constructor(
        private commonService: CommonService,
        private modelService: ModelService
    ) {
    }

    ngOnInit(): void {
        this.initialize();
    }

    initialize(): any {
        if (!this.apiUrl) {
            return;
        }
        var that = this;
        this.commonService
            .get<IPageModel>(this.apiUrl + '/forminformation')
            .then(response => {
                that.resetForm(response, '');
                that.pageModel = response;
            });
    }

    save() {
        var that = this;
        this.commonService
            .post(that.apiUrl, that.pageModel.formInformation)
            .then(response => {
                that.resetForm(response, '');
                that.pageModel = response;
            });
    }

    modelChanged(args: IModelChangedEventArgs) {
        //var formInformation = this.pageModel ? this.pageModel.formInformation : null;
        //if (!formInformation) {
        //    return;
        //}

        //var field = formInformation.fields && formInformation.fields.filter((value) => {
        //    return value.name == args.propertyName;
        //});

        //if (!field) {
        //    return;
        //}

        //field[0].disabled = true;
        
    }

    private resetForm(formInformation: IPageModel, changedPropertyName: string) {
        this.setFieldVisibility(formInformation, changedPropertyName);
    }

    private setFieldVisibility(formInformation: IPageModel, changedPropertyName: string) {
        let forms: Array<IFormInformation<any>> = [formInformation.formInformation],
            model: any,
            form: IFormInformation<any>,
            fields: IField[] | undefined,
            field: IField,
            hiddenBefore: boolean,
            hiddenAfter: boolean,
            stateChanged: boolean;

        for (let i = 0; i < forms.length; i++) {
            form = forms[i];
            fields = form.fields;
            if (!fields) {
                continue;
            }

            model = form.model;
            if (!form || !fields || !model) {
                continue;
            }
            for (let i = 0; i < fields.length; i++) {
                field = fields[i];
                hiddenBefore = field.hidden;

                hiddenAfter = field.hidden;
                stateChanged = hiddenAfter != hiddenBefore;

                if (stateChanged && hiddenAfter) {
                    model[field.accessor] = '';
                    this.setFieldVisibility(formInformation, field.accessor);
                }
            }
        }
    }
}

export interface IPageData {
    apiUrl: string;
}

export interface IPersonViewModel {
    [index: string]: any;
}