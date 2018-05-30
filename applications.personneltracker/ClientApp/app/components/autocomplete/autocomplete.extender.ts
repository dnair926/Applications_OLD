import * as $ from 'jquery';
import 'jqueryui';
import { AutocompleteExtender } from './autocomplete'

var Autocomplete: AutocompleteExtender = {
    popupOpen: false,
    tooltipOpen: false,
    triggeredEvent: '',
    tooltipMessage: '',
    selectedValue: '',

    options: {
        minLength: 3,
        selectionEnabled: true,
        tooltipInfoText: 'Enter at least {0} characters to start search.',
        tooltipSearchingText: 'Searching....',
        tooltipLocation: 'top',
        selectionRequired: true,
    },

    _create: function () {
        var self = this;

        self.options.search = self.onSearch.bind(this);
        self.options.open = self.onPopupOpen.bind(this);
        self.options.select = self.onItemSelected.bind(this);
        self.options.close = self.onPopupClosed.bind(this);
        self.selectedValue = this.element ? this.element.val() : "";

        if (!this.element || !this._on) {
            return;
        }

        this._on(this.element, {
            change: function (event) {
                self._hideTooltip();

                if (self.popupOpen) {
                    // validation of value will happen when popup closed.
                    return;
                }
            },

            blur: function (event) {
                if (self.popupOpen) {
                    return;
                }

                self._validateValue();
            },

            focus: function (event) {
                self.triggeredEvent = 'focus';
                self._setInfoTooltip();
                self.selectedValue = self.element ? self.element.val() : "";
            },

            keyup: function (event) {
                //this.xhr = null;
                self.triggeredEvent = 'keyup';
                var value = self.element ? self.element.val() : "";
                var keyCode = event.keyCode ? event.keyCode : event.rawEvent.keyCode;
                var enterKeyEvent = keyCode === 13;
                var arrowKeyEvent = keyCode >= 37 && keyCode <= 40;
                var minLength = self.options.minLength || 0;
                var valueLengthBelowMinPrefixLength = value ? value.toString().trim().length < minLength : 0;

                if (!enterKeyEvent && !arrowKeyEvent && valueLengthBelowMinPrefixLength) {
                    var infoText = self.options.tooltipInfoText || "";
                    self.tooltipMessage = infoText.replace('{0}', minLength.toString())
                    self._showTooltip();
                }
            }
        });

        return self._super ? self._super() : self;
    },

    onSearch: function (event, ui) {
        this._hideTooltip();
        this.tooltipMessage = this.options.tooltipSearchingText;
        this._showTooltip();

        return this;
    },

    onPopupOpen: function (event, ui) {
        this._hideTooltip();
        this.popupOpen = true;

        return this;
    },

    onPopupClosed: function (event, ui) {
        this.popupOpen = false;
        this._validateValue();
        return this;
    },

    onItemSelected: function (event, ui) {
        var item = ui.item,
            value = (item ? (item.displayText || item.selectedText) : "");
        this.selectedValue = value;
        if (this.options.setValue) {
            this.options.setValue(ui.item);
        }

        event.preventDefault();
    },

    _setInfoTooltip: function () {
        if (this.popupOpen) {
            if (this.tooltipOpen) {
                this._hideTooltip();
            }
            return;
        }

        var value = this.element ? (this.element.val() || "") : "",
            minLength = this.options.minLength || 0;
        var valueLengthPassesMinPrefixLength = value.toString().trim().length >= minLength;
        if (valueLengthPassesMinPrefixLength) {
            this._hideTooltip();
        } else {
            var infoText = this.options.tooltipInfoText || "";
            this.tooltipMessage = infoText.replace('{0}', minLength.toString())
            this._showTooltip();
        }
    },

    _showTooltip: function () {
        if (!this.tooltipMessage || !this.element) {
            return this;
        }

        this.element.initializeTooltip(this.options.tooltipLocation, this.tooltipMessage, this.triggeredEvent ? this.triggeredEvent : 'focus');
        this.element.showTooltip();
        this.tooltipOpen = true;

        return this;
    },

    _hideTooltip: function () {
        if (this.tooltipOpen && this.element) {
            this.element.removeTooltip();
            this.tooltipOpen = false;
        }

        return this;
    },

    _validateValue: function () {
        if (!this.element) {
            return this;
        }

        if (this.element.is(":focus")) {
            return this;
        }

        var currentvalue = this.element.val();
        if (this.options.selectionRequired && currentvalue !== this.selectedValue && this.options.setValue) {
            this.options.setValue(null);
        }

        return this;
    },

    _renderItem: function (ul, item) {
        item = item || { value: "", displayText: "", selectedText: "" };
        var value = item.value,
            listText = item.displayText,
            selectedText = item.selectedText,
            selectionDisabled = !value || value === "0" || !this.options.selectionEnabled;

        return $('<li ' + (selectionDisabled ? 'class="ui-state-disabled"' : '') + '></li>')
            .data("item.autocomplete", item)
            .append('<span>' + listText + '</span>')
            .appendTo(ul);
    },

    disable: function () {
        if (this.element) {
            this.element.removeTooltip();
        }

        return this._super ? this._super() : this;
    },

    enable: function () {
        return this._super ? this._super() : this;
    },

    destroy: function () {
        return this._super ? this._super() : this;
    },
};
$.widget("webextenders.autocompleteextender", $.ui.autocomplete, Autocomplete);


$.fn.extend({

    initializeTooltip: function (placement: string, title: string, trigger: string) {
        $(this)
            .attr('title', title)
            .tooltip({
                placement: placement,
                title: title,
                trigger: trigger,
                animation: false
            });
    },

    showTooltip: function () {
        $(this).tooltip('open');
    },

    hideTooltip: function () {
        $(this).tooltip('close');
    },

    removeTooltip: function () {
        $(this)
            .removeAttr('title')
            .tooltip('destroy');
    }

});