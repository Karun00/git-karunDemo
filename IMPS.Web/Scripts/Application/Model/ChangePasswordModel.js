(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var ChangePasswordModel = function (data) {

        var self = this;
        self.UserName = ko.observable(data ? data.UserName : "");
       
        self.Password = ko.observable(data ? data.Password : "").extend({  required: { message: '* Please enter Old Password' }});
        self.NewPassword = ko.observable(data ? data.NewPassword : "");

        self.IsCredentialsRequired = ko.observable(true);

        //self.NewPassword = ko.observable(data ? data.NewPassword : "")
        //    .extend({
        //        required: {
        //            message: "* Please enter New Password",
                  
        //            onlyIf: self.IsCredentialsRequired
        //        },
                
        //        pattern: {
        //            params: patterns.UpperCase,
        //            message: "Password must have one upper case"
        //        },
        //        pattern: {
        //            params: patterns.LowerCase,
        //            message: "Password must have one lower case"
        //        }
        //    });

        self.ConfirmPassword = ko.observable(data ? data.ConfirmPassword : "").
        extend({
            required: {
                message: "* Please enter Confirm Password"
            },
           
            validation: {
                validator: function (val, params) {
                    var otherValue = params;
                    return val === ko.validation.utils.getValue(otherValue);
                },
                message: '* New Password and Confirm Password do not match.',
                params: self.NewPassword,
                onlyIf: self.IsCredentialsRequired
            }
        });

        self.PWD = ko.observable("");




        self.cache = function () { };
        self.set(data);
    }


    var app = angular.module('form-example', []);

    app.directive('passwordValidate', function () {
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {
                ctrl.$parsers.unshift(function (viewValue) {

                    scope.pwdValidLength = (viewValue && /(?!.*\s).{8,}/.test(viewValue)) ? 'valid' : undefined;
                    scope.pwdUpperLetter = (viewValue && /(?=.*[A-Z])/.test(viewValue)) ? 'valid' : undefined;
                    scope.pwdLowerLetter = (viewValue && /(?=.*[a-z])/.test(viewValue)) ? 'valid' : undefined;
                    scope.pwdHasNumber = (viewValue && /(?=.*\d)/.test(viewValue)) ? 'valid' : undefined;
                    scope.pwdSpecialChar = (viewValue && /\W/.test(viewValue)) ? 'valid' : undefined;
                    if (scope.pwdValidLength && scope.pwdUpperLetter && scope.pwdLowerLetter && scope.pwdHasNumber && scope.pwdSpecialChar ) {
                        ctrl.$setValidity('pwd', true);
                        return viewValue;
                    } else {
                        ctrl.$setValidity('pwd', false);
                        return undefined;
                    }

                });
            }
        };
    });


    var patterns = {
        UpperCase: /(?=.*[A-Z])/,
        LowerCase: /(?=.*[a-z])/,
        SpecialCharacter: /(?!.*\W)/,
        Digit: /(?=.*\d)/,         
        all: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
    };

    ipmsRoot.ChangePasswordModel = ChangePasswordModel;
   

}(window.IPMSROOT));

IPMSROOT.ChangePasswordModel.prototype.set = function (data) {
    var self = this;
    self.UserName(data ? (data.UserName || "") : "");
    self.Password(data ? (data.Password || "") : "");
    self.NewPassword(data ? (data.NewPassword || "") : "");
    self.ConfirmPassword(data ? (data.ConfirmPassword || "") : "");

    self.cache.latestData = data;
}
IPMSROOT.ChangePasswordModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}