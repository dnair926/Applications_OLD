import {
    Component,
    Input,
    OnInit,
    ElementRef
} from '@angular/core';

import * as $ from 'jquery';

@Component({
    selector: 'profile-card',
    templateUrl: './profilecard.component.html'
})
export class ProfileCardComponent implements OnInit {
    @Input()
    profileId!: string;
    @Input()
    showLarge!: boolean;

    templateUrl!: string;
    hovering!: boolean;

    ngOnInit() {
        this.templateUrl = "/profiles/card?id=" + this.profileId;
    }

    onHover(ev: any) {
        this.hovering = true;
    }
}
