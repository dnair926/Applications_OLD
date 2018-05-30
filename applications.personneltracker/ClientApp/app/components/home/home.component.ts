import {
    Component
} from '@angular/core';

import { CommonService } from '../../services/common.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    assignmentListInformation: any;

    constructor(private commonService: CommonService) {
        this.initiate();
    }

    initiate() {
        return this.commonService.get('api/dashboard')
            .then(response => this.assignmentListInformation = response);
    }
}