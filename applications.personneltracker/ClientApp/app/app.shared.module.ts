import './polyfills';
import { NgModule, ClassProvider, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, UrlSerializer } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { PersonComponent } from './components/person/person.component';

import { ProfileCardComponent } from './components/profilecard/profilecard.component';

import { ListComponent } from './components/list/list.component';
import { EditableListComponent } from './components/editable-list/editable-list.component';
import { FormComponent } from './components/form/form.component';
import { PageComponent } from './components/page/page.component';
import { ModalFormComponent } from './components/modal-form/modal-form.component';
import { ModalMessageComponent } from './components/modal-message/modal-message.component';
import { PagerComponent } from './components/pager/pager.component';
import { PopoverDirective } from './components/popover/popover.directive';
import { AutocompleteDirective } from './components/autocomplete/autocomplete.directive';

import { CurrentUserService } from './services/currentuser.service';
import { CommonService } from './services/common.service';
import { ModelService } from './services/model.service';
import { ApplicationConfigurationService } from './services/application-configuration.service';
import { AlertService } from './components/alert/alert.service';
import { AlertsContainerComponent } from './components/alert/alerts-container.component';
import { AlertComponent } from './components/alert/alert.component';

import { LowerCaseUrlSerializer } from './utilities/lowercaseurlserializer';

var urlSerializerProvider: ClassProvider = {
    provide: UrlSerializer,
    useClass: LowerCaseUrlSerializer,
}

@NgModule({
    declarations: [
        ListComponent,
        EditableListComponent,
        FormComponent,
        PageComponent,
        ModalFormComponent,
        ModalMessageComponent,
        PagerComponent,
        PopoverDirective,
        AutocompleteDirective,
        ProfileCardComponent,
        AlertsContainerComponent,
        AlertComponent,
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TasksComponent,
        PersonComponent
    ],
    providers: [
        CommonService,
        ModelService,
        AlertService,
        urlSerializerProvider,
        CurrentUserService,
        {
            provide: APP_INITIALIZER,
            useFactory: getCurrentUser,
            deps: [CurrentUserService],
            multi: true
        },
        ApplicationConfigurationService,
        {
            provide: APP_INITIALIZER,
            useFactory: applicationConfiguration,
            deps: [ApplicationConfigurationService],
            multi: true
        }
    ],
    entryComponents: [ModalFormComponent, ModalMessageComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        CommonModule,
        HttpClientModule,
        FormsModule,
        MatDialogModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'person', component: PersonComponent },
            { path: 'tasks', component: TasksComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}

export function getCurrentUser(currentUserServie: CurrentUserService) {
    return () => currentUserServie.initiate();
}

export function applicationConfiguration(applicationConfigurationService: ApplicationConfigurationService) {
    return () => applicationConfigurationService.initiate();
}