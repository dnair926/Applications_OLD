import { Component } from '@angular/core';

import { AlertService } from './alert.service';

import { IAlert } from './alert.d';

@Component({
    selector: "alerts",
    templateUrl: './alerts-container.component.html',
    styleUrls:['./alerts-container.component.css']
})
export class AlertsContainerComponent {

    //_alerts: IAlert[];

    constructor(private alertService: AlertService) {
        //this._alerts = alertService.alerts;
    }

    get alerts(): Array<IAlert> {
        return this.alertService.alerts;
    }
}