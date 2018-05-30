import {
    URLSearchParams
} from "@angular/http";

export class CommonUtilities {
    public static convertJsonToQuery(url: string, obj: any): string {
        if (!url) {
            return url;
        }

        var params: URLSearchParams = new URLSearchParams();
        params = this.getParams(url, obj, "", params);
        if (params) {
            url = url + (url.indexOf("?") == -1 ? '?' : '&') + params.toString();
        }
        return url;
    }

    private static getParams(url: string, obj: any, prefix: string, params: URLSearchParams) {
        params = params || new URLSearchParams();
        var value;
        for (let key in obj) {
            value = obj[key];
            if (!value || value instanceof Array) {
                continue;
            }
            if (value instanceof Object) {
                this.getParams(url, value, (prefix ? (prefix + '.' + key) : key), params);
                continue;
            }
            params.set((prefix ? (prefix + '.' + key) : key), value);
        }

        return params;
    }
}