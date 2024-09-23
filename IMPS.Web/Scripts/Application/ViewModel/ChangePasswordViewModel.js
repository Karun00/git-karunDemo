(function (IPMSRoot) {
    var ChangePasswordViewModel = function (IsFirstTimeLogin) {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.changepasswordModel = ko.observable(new IPMSROOT.ChangePasswordModel());




        self.Initialize = function () {

        }

        self.SubmitPassword = function (model) {
            self.CPValidation = ko.observable(model);
            self.CPValidation().errors = ko.validation.group(self.CPValidation());
            var errors = self.CPValidation().errors().length;
            if (errors == 0) {
                self.viewModelHelper.apiPost('api/ChangePassword', ko.mapping.toJSON(model),
                               function Message(data) {
                                   toastr.options.closeButton = true;
                                   toastr.options.positionClass = "toast-top-right";
                                   if (data != '') {
                                       toastr.warning(data, "Change Password");
                                       self.CPValidation().errors.showAllMessages();
                                       return;
                                   } else {
                                       toastr.success("Password changed successfully", "Change Password");
                                       window.location.href = "/Welcome";
                                   }
                                   // if (IsFirstTimeLogin == "Y") {


                                   //  }
                                   //else {
                                   //    $.ajax({
                                   //        url: 'Account/Logout',
                                   //        type: 'POST',
                                   //        contentType: 'application/json; charset=utf-8',
                                   //        success: function () {

                                   //            window.location.href = "/Account/Login";
                                   //        }
                                   //    });
                                   //}
                               });
            }
            else {
                self.CPValidation().errors.showAllMessages();
                return;
            }

        }

        self.Cancel = function (model) {
            if (IsFirstTimeLogin == "Y")
                window.location.href = "/Account/Logout";
            else
                window.location.href = "/Welcome";
        }
        self.Initialize();
    }

    IPMSRoot.ChangePasswordViewModel = ChangePasswordViewModel;
}(window.IPMSROOT));

