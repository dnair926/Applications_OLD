import { Injectable } from '@angular/core';
import * as $ from 'jquery';


@Injectable()
export class PluginService {
    init_hover(el: any, onMouseOver: any, onMouseOut: any) {
        $(el).hover(onMouseOver, onMouseOut);
    }

    init_autocomplete(el: any, options: any) {
        var element: any = $(el);
        element["autocompleteextender"](options)
    }

//    showModal(containerId: string) {
//        $(containerId).modal('show');
 //   }

//    hideModal(containerId: string) {
//        $(containerId).modal('hide');
//    }

//    init_datepicker(el: any, options: any) {
 //       $(el).datepicker(options);
//    }
}