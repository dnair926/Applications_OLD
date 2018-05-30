import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { CommonService } from './common.service';
import { IConfiguration } from './configuration.d';

@Injectable()
export class ApplicationConfigurationService {

    constructor(private commonService: CommonService, private titleService: Title) {
    }

    initiate() {
        return this.commonService.get<IConfiguration>('api/applicationconfiguration')
            .then(response => {
                this._configuration = response as IConfiguration;
                this.titleService.setTitle(this._configuration.siteName);
            });
    }

    private _configuration = {
        siteName: ""
    };
    get configuration() {
        return this._configuration;
    }
}