import {
    Injectable
} from "@angular/core";

import { IAlert } from './alert.d';

@Injectable()
export class AlertService {
    _alerts!: IAlert[];

    add(alert : IAlert) : void {
        this._alerts = this._alerts || [];
        this._alerts.push(alert);
    }

    get alerts() {
        return this._alerts;
    }
}