import {
    DefaultUrlSerializer,
    UrlTree
} from "@angular/router";

/// source: http://stackoverflow.com/a/39560520
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
    parse(url: string): UrlTree {
        return super.parse(url.toLowerCase());
    }
}