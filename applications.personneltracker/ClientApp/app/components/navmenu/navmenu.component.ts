import { Component, Input } from '@angular/core';

import { CurrentUserService } from '../../services/currentuser.service';
import { ApplicationConfigurationService } from '../../services/application-configuration.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    
    constructor(
        private currentUserService: CurrentUserService,
        private applicationConfigurationService: ApplicationConfigurationService 
    ) {
    }

    get configuration(): any {
        return this.applicationConfigurationService.configuration;
    }

    get currentUser(): any {
        return this.currentUserService.currentUser;
    }

}
