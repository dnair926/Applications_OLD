export interface AutocompleteElement extends JQuery<HTMLElement> {
    initializeTooltip: (tooltipLocation: string, tooltipMessage: string, triggerEvent: any) => void;
    removeTooltip: () => void;
    showTooltip: () => void;
}
export interface KeyEvent extends JQueryEventObject {
    rawEvent: JQueryEventObject;
}

export interface AutocompleteSelectedItem {
    value: string;
    selectedText: string;
    displayText: string;
}

export interface AutocompleteExtenderOptions extends JQueryUI.AutocompleteOptions {
    tooltipInfoText?: string;
    tooltipSearchingText?: string;
    tooltipLocation: string;
    selectionRequired: boolean;
    selectionEnabled: boolean;
    setValue?: (ui: JQueryUI.AutocompleteUIParams | null) => void;
}

export interface AutocompleteExtenderEvents extends JQueryUI.AutocompleteEvents {
    blur: (ev: JQueryUI.AutocompleteEvent) => void;
    keyup: (ev: KeyEvent) => void;
}

export interface AutocompleteExtender {
    popupOpen?: boolean;
    tooltipOpen: boolean;
    triggeredEvent: string;
    tooltipMessage: string | undefined;
    selectedValue: string | number | string[] | undefined;

    element?: AutocompleteElement;

    options: AutocompleteExtenderOptions;

    _create: () => AutocompleteExtender | undefined;
    _on?: (el: JQuery<HTMLElement>, events: AutocompleteExtenderEvents) => AutocompleteExtender;

    onSearch: (event: JQueryUI.AutocompleteEvent, ui: any) => AutocompleteExtender;
    onPopupOpen: (event: JQueryUI.AutocompleteEvent, ui: any) => AutocompleteExtender;
    onItemSelected: (event: Event, ui: any) => void;
    onPopupClosed: (event: JQueryUI.AutocompleteEvent, ui: any) => AutocompleteExtender;

    _hideTooltip: () => AutocompleteExtender;
    _showTooltip: () => AutocompleteExtender;
    _validateValue: () => AutocompleteExtender;
    _setInfoTooltip: () => void;
    _renderItem: (ul: JQuery<HTMLElement>, item: AutocompleteSelectedItem) => JQuery<HTMLElement>;

    disable: () => AutocompleteExtender;
    enable: () => AutocompleteExtender;
    destroy: () => AutocompleteExtender;

    _super?: () => AutocompleteExtender;
}
