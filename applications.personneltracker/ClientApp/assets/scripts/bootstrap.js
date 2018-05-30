$.fn.extend({
    initializeTooltip : function(placement: any, title: any, trigger: any) {
        $(this)
            .attr('title', title)
            .tooltip({
                placement: placement,
                title: title,
                trigger: trigger,
                animation: false
            });
    },
    showTooltip : function() {
        $(this).tooltip('show');
    },
    hideTooltip : function() {
        $(this).tooltip('hide');
    },
    removeTooltip : function() {
        $(this)
            .attr('title', '')
            .tooltip('destroy');
    }

})
