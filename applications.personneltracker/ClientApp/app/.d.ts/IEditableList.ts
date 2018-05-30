import { IFormInformation } from './IFormInformation';

export interface IEditableListInformation<T> {
    listInformation: any;
    formInformation: IFormInformation<T>;
}
