import { Component, Directive, Input, ElementRef, OnInit } from "@angular/core";

import { CommonService } from '../../services/common.service';

import 'bootstrap';
import * as $ from 'jquery';

@Directive({
    selector: '[bs-popover]',
})
export class PopoverDirective implements OnInit {
    @Input()
    dynamicTemplateUrl!: string;
    @Input()
    placement: string = "right";


    hovering!: boolean;

    constructor(private el: ElementRef, private commonService: CommonService) {
        $(this.el.nativeElement).hover(this.onHover.bind(this), this.onHoverOut.bind(this));
    }

    ngOnInit(): void {
    }

    onHover(ev: any) {
        var self = this;
        self.hovering = true;

        //ToDo: Add functionality to display "Loading....." if dynamic template is delayed.
        self.commonService
            .get(self.dynamicTemplateUrl)
            .then(self._open.bind(this));
    }

    _open(content: any) {
        if (!this.hovering) {
            return;
        }

        var popoverOptions: PopoverOptions = {
            animation: true,
            content: content.message,
            html: true,
            placement: this.placement,
            trigger: "manual",
            template: '<div class="popover profile-card" role="tooltip" data-content="Loading...."><div class="arrow"></div><div class="popover-content">Loading</div></div>'
        };
        $(this.el.nativeElement)
            .popover(popoverOptions)
            .popover('show');
    }

    onHoverOut() {
        this.hovering = false;
        $(this.el.nativeElement).popover('destroy');
    }
}