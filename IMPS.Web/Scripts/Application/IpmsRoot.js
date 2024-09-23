
window.IPMSROOT = {};
//debugger;
(function (cr) {
    var initialId;
    cr.initialId = initialId;
    CutPaste();
}(window.IPMSROOT));

/*
jQuery.browser.mobile (http://detectmobilebrowser.com/)
 *
 * jQuery.browser.mobile will be true if the browser is a mobile device
 *
 **/
(function (a) {
    (jQuery.browser = jQuery.browser || {}).mobile = /(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))
    // alert(navigator.userAgent);
    //   alert(jQuery.browser.mobile);
})(navigator.userAgent || navigator.vendor || window.opera);



(function (cr) {
    var initialState;
    cr.initialState = initialState;
    CutPaste();
}(window.IPMSROOT));

(function (cr) {
    var rootPath;
    cr.rootPath = rootPath;
    CutPaste();
}(window.IPMSROOT));

(function (cr) {

    var mustEqual = function (val, other) {
        return val == other();
    }
    cr.mustEqual = mustEqual;
    CutPaste();
}(window.IPMSROOT));

(function (cr) {

    var viewModelHelper = function () {
        //debugger;
        var self = this;

        self.modelIsValid = ko.observable(true);
        self.modelErrors = ko.observableArray();
        self.isLoading = ko.observable(false);

        self.statePopped = false;
        self.stateInfo = {};

        self.apiGet = function (uri, data, success, failure, always, isAsync) {
            self.isLoading(true);
            self.modelIsValid(true);
            if (isAsync === undefined) {
                isAsync = true;
            }
            $.ajax({
                type: 'Get', url: IPMSROOT.rootPath + uri, data: data, contentType: 'application/json;charset=utf-8',
                dataType: 'json', async: isAsync
            })
                .done(success)
                .fail(function (result) {
                    if (failure == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ' - ' + result.responseText]);
                        else
                            self.modelErrors(JSON.parse(result.responseText));
                        self.modelIsValid(false);
                    }
                    else
                        failure(result);
                })
                .always(function () {
                    if (always == null)
                        self.isLoading(false);
                    else
                        always();
                });
        };


        self.apiPost = function (uri, data, sucessCallback, failureCallback, alwaysCallback) {
            self.isLoading(true);
            self.modelIsValid(true);

            $.ajax({
                type: 'Post', url: IPMSROOT.rootPath + uri, data: data, contentType: 'application/json;charset=utf-8',
                dataType: 'json'
            })
                .done(sucessCallback)
                .fail(function (result) {
                 
                    if (failureCallback == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ':' + result.responseText]);
                        else {
                            self.modelErrors(JSON.parse(result.responseText));

                        }
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        if (uri == "api/Account/CheckCredentials")
                        {
                            toastr.error('"Invalid Login Credentials"');
                        }
                        else {
                            toastr.error(result.responseText);
                        }
                       
                        self.modelIsValid(false);
                    } else {
                        failureCallback(result);
                    }
                })
                .always(function () {
                    if (alwaysCallback == null) {

                        self.isLoading(false);
                    } else {
                        alwaysCallback();
                    }
                });
        };

        self.apiUpload = function (uri, data, sucessCallback, failureCallback, alwaysCallback) {
            self.isLoading(true);
            self.modelIsValid(true);

            $.ajax({
                type: 'Post', url: IPMSROOT.rootPath + uri, data: data, cache: false, contentType: false, processData: false, dataType: 'json'
            })
                .done(sucessCallback)
                .fail(function (result) {
                    if (failureCallback == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ':' + result.responseText]);
                        else {
                            self.modelErrors(JSON.parse(result.responseText));

                        }
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.error(result.responseText);
                        self.modelIsValid(false);
                    } else {
                        failureCallback(result);
                    }
                })
                .always(function () {
                    if (alwaysCallback == null) {

                        self.isLoading(false);
                    } else {
                        alwaysCallback();
                    }
                });
        };

        self.apiPut = function (uri, data, sucessCallback, failureCallback, alwaysCallback) {
            self.isLoading(true);
            self.modelIsValid(true);

            $.ajax({
                type: 'Put', url: IPMSROOT.rootPath + uri, data: data, contentType: 'application/json;charset=utf-8',
                dataType: 'json'
            })
                .done(sucessCallback)
                .fail(function (result) {
                    if (failureCallback == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ':' + result.responseText]);
                        else {
                            self.modelErrors(JSON.parse(result.responseText));
                        }
                        // Srinivas
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.error(result.responseText);
                        self.modelIsValid(false);
                    } else {
                        failureCallback(result);
                    }
                })
                .always(function () {
                    if (alwaysCallback == null) {

                        self.isLoading(false);
                    } else {
                        alwaysCallback();
                    }
                });
        };

        self.apiDelete = function (uri, data, sucessCallback, failureCallback, alwaysCallback) {
            self.isLoading(true);
            self.modelIsValid(true);

            $.ajax({
                type: 'Delete', url: IPMSROOT.rootPath + uri, data: data, contentType: 'application/json;charset=utf-8',
                dataType: 'json'
            })
                .done(sucessCallback)
                .fail(function (result) {
                    if (failureCallback == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ':' + result.responseText]);
                        else {
                            self.modelErrors(JSON.parse(result.responseText));
                        }
                        self.modelIsValid(false);
                    } else {
                        failureCallback(result);
                    }
                })
                .always(function () {
                    if (alwaysCallback == null) {

                        self.isLoading(false);
                    } else {
                        alwaysCallback();
                    }
                });
        };

        self.pushUrlState = function (code, title, id, url) {
            self.stateInfo = { State: { Code: code, Id: id }, Title: title, Url: IPMSROOT.rootPath + url };
        }

        self.handleUrlState = function (initialState) {
            if (!self.statePopped) {
                if (initialState != '') {
                    history.replaceState(self.stateInfo.State, self.stateInfo.Title, self.stateInfo.Url);
                    // we're past the initial nav state so from here on everything should push
                    initialState = '';
                }
                else {
                    history.pushState(self.stateInfo.State, self.stateInfo.Title, self.stateInfo.Url);
                }
            }
            else
                self.statePopped = false; // only actual popping of state should set this to true

            return initialState;
        }
    }
    CutPaste();
    cr.viewModelHelper = viewModelHelper;
}(window.IPMSROOT));

// Validation Helper
(function (cr) {
    CutPaste();
    var validationHelper = function () {
        var self = this;

        // Alphanumeric
        self.ValidateAlphaNumeric = function (data, event) {

            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z0-9]/;
            return charcheck.test(keychar);
        }
        self.ValidateAlphaNumericWithStarChracter = function (data, event) {

            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z0-9*]/;
            return charcheck.test(keychar);
        }

        // Numeric 
        self.ValidateNumeric = function (data, event) {
            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            //charcheck = /[0-9]/;
            charcheck = /[0-9.\b]/;
            return charcheck.test(keychar);
        }

        // Numeric 
        //Author : Omprakash Kotha
        //Dated  : 25th August 2014
        //Reason : Added on key press event to check valid or not
        self.ValidateNumeric_keypressEvent = function (data, event) {

            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /[0-9\b]/g;
            // var result = keychar.match(charcheck)
            return ((keychar.match(charcheck) == null) ? false : true);

        }
        //Author : Omprakash Kotha
        //Dated  : 25th August 2014
        //Reason : Added on key press event to check valid or not
        // Accept only Alphabets and spaces
        self.ValidateAlphabetsWithSpaces_keypressEvent = function (data, event) {

            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z \b]*$/;
            // var result = keychar.match(charcheck)
            return ((keychar.match(charcheck) == null) ? false : true);

        }
        //Author : Omprakash Kotha
        //Dated  : 2nd September 2014
        //Reason : Added on key down event to prevent backspace
        // Prevent Backspace Button in text field
        self.PreventBackspaces_keydownEvent = function (event) {
            CutPaste();
            var evt = event || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8 || keyCode === 46) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }


        // Accept only Alphabets and spaces
        function ValidateAlphabetsWithSpaces(data, event) {
            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z \b]*$/;
            return charcheck.test(keychar);
        }
        // AlphaNumeric with Spaces
        self.ValidateAlphaNumericWithSpaces = function (data, event) {
            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[A-Za-z\d\s\b]*$/;
            return charcheck.test(keychar);
        }

        //Author : Suresh
        //Dated  : 12 NOV 2014
        //Reason : Added on key down event to prevent dop
        // Prevent prevent drop into text box
        self.PreventDrop = function (event) {
            event.preventDefault();
        }


    }
    cr.validationHelper = validationHelper;

}(window.IPMSROOT));

function CutPaste() {
    var controls = $(".form-control");
    controls.bind("paste", function () {
        return false;
    });
    controls.bind("drop", function () {
        return false;
    });
    controls.bind("cut", function () {
        return false;
    });
    controls.bind("copy", function () {
        return false;
    });
}



(function (ipmsRoot) {
    //  alert('Check');
    CutPaste();

}(window.IPMSROOT));

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            var newDate = $(element).datepicker("getDate");
            // newDate format is 2013-01-11T06:11:00.000Z
            observable(moment(newDate).format('MM/DD/YYYY'));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

    },
    // get the value from the viewmodel and format it for display
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        var date = moment(value);
        current = $(element).datepicker("getDate");

        if (value != null) {
            if (value - current !== 0) {
                var date = moment(value);
                $(element).val(date.format("L"));
            }
        }
    }
}

ko.bindingHandlers.loadingWhen = {
    // any ViewModel using this extension needs a property called isLoading
    // the div tag to contain the loaded content uses a data-bind="loadingWhen: isLoading"
    init: function (element) {
        var
            $element = $(element),
            currentPosition = $element.css("position")
        $loader = $("<div>").addClass("loading-loader").hide();

        //add the loader
        $element.append($loader);

        //make sure that we can absolutely position the loader against the original element
        if (currentPosition == "auto" || currentPosition == "static")
            $element.css("position", "static");

        //center the loader
        $loader.css({
            position: "absolute",
            top: "50%",
            left: "50%",
            "margin-left": -($loader.width() / 2) + "px",
            "margin-top": -($loader.height() / 2) + "px"
        });
    },
    update: function (element, valueAccessor) {
        var isLoading = ko.utils.unwrapObservable(valueAccessor()),
            $element = $(element),
            $childrenToHide = $element.children(":not(div.loading-loader)"),
            $loader = $element.find("div.loading-loader");

        if (isLoading) {
            $childrenToHide.css("visibility", "hidden").attr("disabled", "disabled");
            $loader.show();
        }
        else {
            $loader.fadeOut("fast");
            $childrenToHide.css("visibility", "visible").removeAttr("disabled");
        }
    }
};

ko.extenders.numeric = function (target, precision) {
    //create a writeable computed observable to intercept writes to our observable
    var result = ko.computed({
        read: target,  //always return the original observables value
        write: function (newValue) {
            var current = target(),
                roundingMultiplier = Math.pow(10, precision),
                newValueAsNum = isNaN(newValue) ? 0 : parseFloat(+newValue),
                valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

            //only write if it changed
            if (valueToWrite !== current) {
                target(valueToWrite);
            } else {
                //if the rounded value is the same, but a different value was written, force a notification for the current field
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    });

    //initialize with current value to make sure it is rounded appropriately
    result(target());

    //return the new computed observable
    return result;
};

ko.bindingHandlers.date = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // TODO: this should be able to support foreign date formats
        var jsonDate = valueAccessor(); // 2013-02-19T00:00:00
        var ret = $.datepicker.formatDate('mm-dd-yy', new Date(jsonDate()));
        element.innerHTML = ret;
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
    }
};

ko.bindingHandlers.enterkey = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var inputSelector = 'input,textarea,select,checkbox,form';
        $(document).on('keypress', inputSelector, function (e) {
            var allBindings = allBindingsAccessor();
            //  $(element).on('keypress', 'input, textarea, select', function (e) {
            var keyCode = e.which || e.keyCode;
            if (keyCode !== 13) {
                return true;
            }

            var target = e.target;
            target.blur();

            allBindings.enterkey.call(viewModel, viewModel, target, element);

            return false;
            // });
        });
    }
};
