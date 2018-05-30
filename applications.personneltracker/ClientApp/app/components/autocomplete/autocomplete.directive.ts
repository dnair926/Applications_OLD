import {
    Directive,
    ElementRef,
    Input,
    Output,
    OnInit,
    AfterViewInit,
    EventEmitter
} from "@angular/core"

import {
    CommonUtilities
} from "../../utilities/common.utility"

import {
    CommonService
} from "../../services/common.service";

import * as $ from 'jquery';
import './autocomplete.extender';

interface Autocomplete<HtmlElement extends Node = HtmlElement> extends JQuery<HtmlElement> {
    autocompleteextender?(options: any): this;
}

@Directive({
    selector: '[auto-complete]',
    providers: [CommonService]
})
export class AutocompleteDirective implements AfterViewInit {
    @Input() autocompleteProperty: any;
    @Input() autocompleteCriteria: any;
    @Output() itemSelected: EventEmitter<any> = new EventEmitter();
    options: any;
    contextKey: any;

    ngAfterViewInit(): void {
        this.options = JSON.parse(this.autocompleteProperty);
        this.options.source = this.listSource.bind(this);
        this.options.setValue = this.setItem.bind(this);
        var element: Autocomplete<HTMLElement> = $(this.el.nativeElement);
        if (element.autocompleteextender) {
            element.autocompleteextender(this.options);
        }
    }

    setItem(item: any) {
        this.itemSelected.emit(item);
    }

    listSource(request: any, response: any) {
        var params = request.term;
        var data = (this.autocompleteCriteria ? JSON.parse(this.autocompleteCriteria) : {}) || {};
        data.keyword = params;
        data.contextKey = this.contextKey;

        var url = this.options.url;
        url = CommonUtilities.convertJsonToQuery(url, data);

        this.commonService.get(url)
            .then((data: any) =>
            { 
            response(
                $.map(data, function (item) {
                    return item;
                })
                )
            }
            );
    }

    constructor (
        private el: ElementRef,
        private commonService: CommonService) {
    }
}