import { Injectable} from '@angular/core';
import { CommonService } from './common.service';

@Injectable()
export class CurrentUserService {

    constructor(private commonService: CommonService) {
        //this.initiate();
    }

    initiate() {
        return this.commonService.get('api/currentuser/get')
            .then(response => this._currentUser = response);
    }

    private _currentUser: any;
    get currentUser() {
        return this._currentUser;
    }
}