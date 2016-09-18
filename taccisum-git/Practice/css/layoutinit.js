//layout init

$(function () {
    function showIfHasChild(selectorArray) {
        $.each(selectorArray, function(index, item) {
            var $target = $(item);
            if ($target.children().length == 0) {
                $target.remove();
            } else {
                $target.show();
            }
        });
    }

    var targets = [
        ".area-operation .area-param",
        ".area-operation .area-info",
        ".area-operation .area-button"
    ];

    showIfHasChild(targets);


});
 