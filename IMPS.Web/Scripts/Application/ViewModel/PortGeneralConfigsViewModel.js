(function (IPMSRoot) {

    var PortGeneralConfigsViewModel = function () {
        var self = this;
        $('#spnTitle').html("Port General Configuration");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.PortGeneralConfigsList = ko.observableArray();
        self.PortGeneralConfigsModel = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);

        // This is used to Initialize method
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.PortGeneralConfigsModel(new IPMSROOT.PortGeneralConfigsModel());
            self.LoadPortGeneralConfigs();
            self.viewMode('List');
        }

        // This is used to Edit Port General Configs
        self.editPortGeneralConfigs = function (data) {
            $('#spnTitle').html("Update Port General Configuration");
            self.viewMode('Form');
            self.IsReset(true);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsActivityEnable(true);
            self.LoadGroupNames(data.GroupName);
            self.PortGeneralConfigsModel(data);
        }

        // This is used to View Port General Configs
        self.viewPortGeneralConfigs = function (data) {
            $('#spnTitle').html("View Port General Configuration");
            self.viewMode('Form');
            self.IsReset(false);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            self.IsActivityEnable(false);
            self.LoadGroupNames(data.GroupName);
            self.PortGeneralConfigsModel(data);
        }

        self.LoadPortGeneralConfigs = function () {
            self.viewModelHelper.apiGet('api/GetAllPortGeneralConfigsDetails', null,
                function (result) {
                    self.PortGeneralConfigsList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.PortGeneralConfigsModel(item);
                    }));
                });
        }

        self.LoadGroupNames = function (GroupName) {
            self.viewModelHelper.apiGet('api/GroupNamesDetails', { GroupName: GroupName },
             function (result) {
                 self.PortGeneralConfigsModel().GroupNames(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.PortGeneralConfigsModel(item);
                 }));
             });
        }

        // This is used to Update  GroupName
        self.ModifyPortGeneralConfigs = function (GroupName) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.PortGeneralConfigsValidation = ko.observable(GroupName);
            self.PortGeneralConfigsValidation().errors = ko.validation.group(self.PortGeneralConfigsValidation());
            var errors = self.PortGeneralConfigsValidation().errors().length;

            if (errors == 0) {
                self.viewModelHelper.apiPut('api/UpdatePortGeneralConfigsDetails', ko.mapping.toJSON(GroupName),
                    function Message(data) {
                        toastr.success("Port General Configuration details updated successfully.", "Port General Configuration");
                        self.LoadPortGeneralConfigs();
                        self.viewMode('List');
                    });
            }
            else {
                self.PortGeneralConfigsValidation().errors.showAllMessages();
                return;
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("Port General Configuration");
            self.PortGeneralConfigsModel(new IPMSRoot.PortGeneralConfigsModel());
        }

        //To Set Colorcode to Model Variable
        ColorCodeChange = function () {
            $("#chosen-color").val($("#background-color").val());
        }

        self.Initialize();
    }
    IPMSRoot.PortGeneralConfigsViewModel = PortGeneralConfigsViewModel;
}(window.IPMSROOT));