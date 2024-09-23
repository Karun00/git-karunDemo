
ko.validation.rules['remote'] = {
    async: true,
    validator: function (val, parms, callback) {

        if (!ko.validation.utils.isEmptyVal(val)) {
            $.get(parms.url, { value: val })
                .done(function (data) {
                    callback(data);
                })
                .fail(function (result) {
                    this.message = 'Unable to validate due to internal error';
                    callback(false);
                });
        }
    },
    message: 'Default Invalid Message'
};
