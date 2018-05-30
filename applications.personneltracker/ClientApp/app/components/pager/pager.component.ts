import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    templateUrl: './pager.component.html',
    selector: 'pager'
})
export class PagerComponent {
    @Input()
    pagerInfo: any;

    @Output()
    paging: EventEmitter<any> = new EventEmitter();

    onPaging(page: number) {
        this.paging.emit(page);
    }
}