import { IField } from './IField';
import { FormOrientation } from './FormOrientation';

export interface IFormInformation<T> {
    name?: string;

    title?: string;

    //Information about elements to display
    fields?: Array<IField>;

    hidden?: boolean;

    //Messages to display at the top of the form
    messages?: Array<string>;

    //Warnings to display at the top of the form
    warnings?: Array<string>;

    //form orientation
    orientation?: FormOrientation;

    //Model to bind
    model?: T;
}
