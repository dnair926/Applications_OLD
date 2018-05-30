import { Injectable } from '@angular/core';

import { IFormInformation } from '../.d.ts/IFormInformation';
import { IPageModel } from '../.d.ts/IPageModel';
import { IField } from '../.d.ts/IField';

@Injectable()
export class ModelService {

    resetForm(forms: Array<IFormInformation<any>>, changedPropertyName: string, fieldLogic: (formInformation: Array<IFormInformation<any>>, changedPropertyName: string, field: IField) => void ) {
        this.setFieldVisibility(forms, changedPropertyName, fieldLogic);
    }

    private setFieldVisibility(forms: Array<IFormInformation<any>>, changedPropertyName: string, fieldLogic: (forms: Array<IFormInformation<any>>, changedPropertyName: string, field: IField) => void) {
        let model: any,
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

                fieldLogic(forms, changedPropertyName, field);

                hiddenAfter = field.hidden;
                stateChanged = hiddenAfter != hiddenBefore;

                if (stateChanged && hiddenAfter) {
                    model[field.accessor] = '';
                    this.setFieldVisibility(forms, field.accessor, fieldLogic);
                }
            }
        }
    }


}
