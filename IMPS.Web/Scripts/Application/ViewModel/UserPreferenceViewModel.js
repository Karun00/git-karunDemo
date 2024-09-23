(function (IPMSRoot) {

    var UserPreferenceViewModel = function () {
        var self = this;
        $('#spnTitle').html("Dashboard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.userpreferenceModel = ko.observable();
        self.userPreferenceDetailsByRole = ko.observableArray([]);
        self.userPreferenceDetailsByUser = ko.observableArray([]);
        self.items = ko.observableArray([]);

        

        self.Initialize = function () {
            self.userpreferenceModel(new IPMSROOT.UserPreferenceModel());
            self.LoadUserPreferenceDetails();
            self.LoadUserPreferenceDetailsByUser();
               
            self.viewMode('Form');


        }

        self.LoadUserPreferenceDetails = function () {
            self.viewModelHelper.apiGet('api/UserPreference/GetUserPreferenceDetails', null,
               function (result) {
                   self.userPreferenceDetailsByRole(ko.utils.arrayMap(result, function (item) {

                       return new IPMSRoot.UserPreferenceModel(item);
                   }));
               });
        }

        self.LoadUserPreferenceDetailsByUser = function () {
            self.viewModelHelper.apiGet('api/UserPreference/GetUserPreferenceDetailsByUser', null,
               function (result) {
                   self.userPreferenceDetailsByUser(ko.utils.arrayMap(result, function (item) {

                       return new IPMSRoot.UserPreferenceModel(item);
                   }));
               });
        }


        self.AddList = function () {
           
            var requested = self.userPreferenceDetailsByUser;
            var status = true;
            $.each(self.userpreferenceModel().allSelected(), function (n, item) {
                if (self.userPreferenceDetailsByUser() != null) {
                    $.each(self.userPreferenceDetailsByUser(), function (k, root) {

                        if (root.EntityCode() == item.EntityCode()) {
                            alert('Selected Item Already Existed');
                            status = false;
                        }

                    });
                }
                if (status) {
                    requested.push(item);
                }
             //   requested.push(item);

            });
            if (status) {
                self.userPreferenceDetailsByRole.removeAll(self.userpreferenceModel().allSelected());
                self.userpreferenceModel().allSelected().splice(0, self.userpreferenceModel().allSelected().length);
            }
        }

        self.RemoveList = function (model) {

            if (self.userpreferenceModel().itemSelected() != null) {
                self.userPreferenceDetailsByUser.removeAll(self.userpreferenceModel().itemSelected());
                self.userpreferenceModel().itemSelected().splice(0, self.userpreferenceModel().itemSelected().length);
                var listValue = "";
                ko.utils.arrayForEach(self.userPreferenceDetailsByUser(), function (reference) {

                    if (listValue == "")
                        listValue = reference.EntityCode();
                    else
                        listValue = listValue + "," + reference.EntityCode();
                });

                model.DashBoardConfig = listValue;

                self.viewModelHelper.apiPost('api/UserPreference/PostUserPreference', ko.mapping.toJSON(model), function Message(data) {
                });

            }
        }


        self.Save = function (model) {

                var listValue = "";

                ko.utils.arrayForEach(self.userPreferenceDetailsByUser(), function (reference) {

                    if (listValue == "")
                        listValue = reference.EntityCode();
                    else
                        listValue = listValue + "," + reference.EntityCode();
                });

                model.DashBoardConfig = listValue;

                self.viewModelHelper.apiPost('api/UserPreference/PostUserPreference', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Dashboard Configuration saved successfully", "UserPreference");

                });

        }

        self.Initialize();
    }
    IPMSRoot.UserPreferenceViewModel = UserPreferenceViewModel;

}(window.IPMSROOT));











