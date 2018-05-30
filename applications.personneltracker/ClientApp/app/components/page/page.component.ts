import { Component, Input, Output, OnInit } from '@angular/core';

import { CommonService } from '../../services/common.service';
import { ModelService } from '../../services/model.service';

import { IFormInformation } from '../../.d.ts/IFormInformation';
import { IField } from '../../.d.ts/IField';
import { IPageModel } from '../../.d.ts/IPageModel';

@Component({
    selector: 'page',
    templateUrl: 'page.component.html'
})
export class PageComponent implements OnInit {
    @Output()
    public pageModel: IPageModel | undefined;

    @Input()
    apiUrl!: string;

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
        this.commonService.get<IPageModel>(this.apiUrl + '/forminformation')
            .then(response => {
                that.resetForm(response, '');
                that.pageModel = response;
            });
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