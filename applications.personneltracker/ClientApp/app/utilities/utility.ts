import * as $ from 'jquery';

let methodize = (scope: any, fn: any, ...args: Array<any>): any => {
    return function () {
        return fn.apply(scope, arguments);
    };
}

export { methodize }