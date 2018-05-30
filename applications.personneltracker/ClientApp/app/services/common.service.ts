
import { throwError as observableThrowError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {Injectable} from '@angular/core';
import {
    Response,
    Request,
    RequestOptions,
    RequestOptionsArgs,
    RequestMethod,
    URLSearchParams
} from "@angular/http";

import { HttpClient, HttpHeaders } from "@angular/common/http";

import { CommonUtilities } from "../utilities/common.utility";
import { IResponseObject } from '../.d.ts/IResponseObject';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class CommonService {

    constructor(private http: HttpClient) {
    }

    get<T>(url: string): Promise<T> {
        return this.http.get<T>(url)
            .toPromise();
    }

    post(url: string, data: any): Promise<any> {
        return this.http.post(url, data, httpOptions)
            .toPromise()
            .catch(this.handleError);
    }

    put(url: string, data?: any): Promise<any> {
        return this.http.put(url, data, httpOptions)
            .toPromise()
            .catch(this.handleError);
    }

    delete(url: string, data: any): Promise<IResponseObject> {
        let query = CommonUtilities.convertJsonToQuery(url, data);
        return this.http.delete<IResponseObject>(query, httpOptions)
            .toPromise<IResponseObject>()
            .catch(this.handleError);
    }

    private extractData<T>(res: Response) : T | any {
        if (!res) {
            return res;
        }

        try {
            let body = res.json() as T;
            return body || {};
        } catch (ex) {
            return res;
        }
    }

    private handleError(error: Response | any) : Promise<IResponseObject> {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        let responseObject: IResponseObject = {
            message: "An error occurred"
        };

        return Promise.resolve(responseObject);
    }
}

export enum ResultTypes {
    error,
    success,
    invalid
}