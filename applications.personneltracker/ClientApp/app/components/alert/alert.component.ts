import {
    Component,
    Input,
    AfterViewInit
} from "@angular/core";

import {
    AlertService
} from "./alert.service";

import { IAlert } from './alert.d';

@Component({
    selector: "alert-view",
    templateUrl: "./alert.component.html" 
})
export class AlertComponent implements AfterViewInit {

    @Input()
    alert!: IAlert | null;

    constructor() {
    }

    ngAfterViewInit() {
        window.setTimeout(() => { this.alert = null; }, 3000);
    }

}