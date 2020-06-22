$(document).ready(function () {
    $("#cityautocomplete").autocomplete({
        source: function (request, response) {
            $.getJSON("/City/Autocomplete", request, function (data) {
                response($.map(data, function (item) {
                    return {
                        label: item,
                        value: item + ""
                    }
                }))
            })
        }
    });
});