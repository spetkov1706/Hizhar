(function ($) {
    if (!$ || !$.validator || !$.validator.unobtrusive) {
        return;
    }

    $.validator.addMethod('notinpast', function (value, element) {
        if (!value) {
            return true;
        }

        var today = new Date();
        today.setHours(0, 0, 0, 0);

        var parts = value.split('-'); 
        var selected = new Date(parts[0], parts[1] - 1, parts[2]);

        return selected >= today;
    });
    $.validator.unobtrusive.adapters.addBool('notinpast');

    $.validator.addMethod('dategreaterthan', function (value, element, params) {
        if (!value) {
            return true;
        }

        var otherValue = $('[name="' + params.other + '"]').val();
        if (!otherValue) {
            return true;
        }

        return value > otherValue;
    });
    $.validator.unobtrusive.adapters.add('dategreaterthan', ['other'], function (options) {
        options.rules['dategreaterthan'] = { other: options.params.other };
        options.messages['dategreaterthan'] = options.message;
    });

    $.validator.addMethod('maxfilesize', function (value, element, params) {
        if (!element.files || element.files.length === 0) {
            return true;
        }

        return element.files[0].size <= parseInt(params.max, 10);
    });
    $.validator.unobtrusive.adapters.add('maxfilesize', ['max'], function (options) {
        options.rules['maxfilesize'] = { max: options.params.max };
        options.messages['maxfilesize'] = options.message;
    });
})(window.jQuery);