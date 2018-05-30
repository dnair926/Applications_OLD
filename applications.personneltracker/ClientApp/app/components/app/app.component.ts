import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';

import { ApplicationConfigurationService } from '../../services/application-configuration.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: [
        './_site.css',
        './input.css',
        './form.css',
        './autocomplete.css',
        './tab.css',
        './app.component.css'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit {
    loading!: boolean;

    constructor(private router: Router) {

    }

    ngOnInit(): void {
        this.router.events.subscribe((event: any): void => {
            this.navigationInterceptor(event);
        });
    }

    navigationInterceptor(event : any): void {
        if (event instanceof NavigationStart) {
            this.loading = true;
        }
        if (event instanceof NavigationEnd) {
            this.loading = false;
        }
    }
}
