import { IAlert } from '../components/alert/alert/alert.d';

export class ResponseObject {
    validationMessage: Array<string>;
    alert: IAlert;
    message: string;
}