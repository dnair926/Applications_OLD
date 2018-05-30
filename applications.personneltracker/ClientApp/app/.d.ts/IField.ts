import { ISelectListItem } from './ISelectListItem';
import { FormFieldType } from './FormFieldType';

export interface IField {
    //field caption
    caption: string;

    //Type of element to display
    fieldType: FormFieldType;

    //Maximum character allowed in free-form elements
    maxCharLength: number;

    //Element order of display
    displayOrder: number;

    // Element name
    name: string;

    //field show as required or optional
    required: boolean;

    //element disabled or enabled
    disabled: boolean;

    //Angular modal property
    accessor: string;

    //Description property name for list elements
    descriptionAccessor: string;

    //Help message Angular modal property
    helfInfoAccessor: string;

    //List items property name for list elements
    listItemsAccessor: string;

    //element visible or hidden
    hidden: boolean;

}