import { ResultType } from "./ResultType";
import { IAlert } from "../components/alert/alert";

export interface IResponseObject {
    validationMessages?: Array<string>;

    result?: ResultType;

    alert?: IAlert;

    message?: string;

    returnObject?: {};
}