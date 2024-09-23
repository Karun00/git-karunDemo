(function (IPMSRoot) {

    var CraftOutOfCommissionViewModel = function () {

        var self = this;
        $('#spnTitle').html("Craft Out Of Commission");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.Dropdowneditable = ko.observable(true);
        self.Dataeditable = ko.observable(true);
        self.craftoutofcommissionModel = ko.observable();
        self.CraftOutOfCommsList = ko.observableArray();
        self.CraftList = ko.observable();
        self.ReasonsList = ko.observable();
        self.CommStatusList = ko.observable();
        self.CraftOutOfCommReferenceData = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode('List');
            self.craftoutofcommissionModel(new IPMSROOT.CraftOutOfCommissionModel());
            self.LoadCraftOutOfCommissions();
            self.LoadCrafts();
            self.LoadReasons();
            self.LoadCommStatus();
            self.IsUpdate(false);
        }

        self.LoadCraftOutOfCommissions = function () {
            self.viewModelHelper.apiGet('api/CraftOutOfCommissionDetails', null,
              function (result) {
                  self.CraftOutOfCommsList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.CraftOutOfCommissionModel(item);
                  }));
              }, null, null, false);
        }

        self.LoadCrafts = function () {
            var craftid = 0;
            self.viewModelHelper.apiGet('api/CraftDetailsForOutofComm/' + craftid, null,
              function (result) {
                  self.CraftList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Crafts(item);
                  }));
              }, null, null, false);
        }

        self.LoadCraftsbasedonCraftID = function (CraftID) {
            self.viewModelHelper.apiGet('api/CraftDetailsForOutofComm/' + CraftID, null,
              function (result) {
                  self.CraftList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Crafts(item);
                  }));
              }, null, null, false);
        }

        self.LoadReasons = function (ReasonCode) {
            self.viewModelHelper.apiGet('api/GetReasonforOutofCommDetails/' + ReasonCode, null,
              function (result) {
                  self.ReasonsList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Reasons(item);
                  }));
              }, null, null, false);
        }

        self.LoadCommStatus = function (Status) {
            self.viewModelHelper.apiGet('api/GetCommStatusDetails/' + Status, null,
              function (result) {
                  self.CommStatusList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Reasons(item);
                  }));
              }, null, null, false);
        }

        self.addcraftoutofcomm = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.Dropdowneditable(true);
            self.Dataeditable(false);
            //self.Resetcraftoutofcomm();
            self.craftoutofcommissionModel(new IPMSROOT.CraftOutOfCommissionModel());
            self.LoadCrafts();
            $('#spnTitle').html("Add Craft Out Of Commission");
        }        

        self.editcraftoutofcomm = function (craftoutofcomm) {
            self.viewMode('Form');
            self.craftoutofcommissionModel().Reason(craftoutofcomm.Reason());
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsReset(true);
            self.editableView(true);
            self.Dropdowneditable(false);
            self.Dataeditable(false);
            self.LoadCraftsbasedonCraftID(craftoutofcomm.CraftID());
            $('#spnTitle').html("Update Craft Out Of Commission");
            self.craftoutofcommissionModel(craftoutofcomm);
        }

        self.viewcraftoutofcomm = function (craftoutofcomm) {
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.editableView(false);
            self.Dropdowneditable(false);
            self.Dataeditable(false);
            self.LoadCraftsbasedonCraftID(craftoutofcomm.CraftID());
            $('#spnTitle').html("View Craft Out Of Commission");
            self.craftoutofcommissionModel(craftoutofcomm);
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("Craft Out Of Commission");
            self.LoadCraftOutOfCommissions();
            self.LoadCrafts();
            self.craftoutofcommissionModel(new IPMSRoot.CraftOutOfCommissionModel());
        }

        self.Resetcraftoutofcomm = function (model) {
            ko.validation.reset();
            model.validationEnabled(false);
            self.craftoutofcommissionModel().reset();
            self.CraftOutOfCommissionValidation = ko.observable(model);
            self.CraftOutOfCommissionValidation().errors = ko.validation.group(self.CraftOutOfCommissionValidation());
            self.CraftOutOfCommissionValidation().errors.showAllMessages(false);
            ko.validation.reset();
        }

        self.CraftChanged = function (event) {
            if (event.CraftID() != "") {
                console.log(event.CraftID());
                self.viewModelHelper.apiGet('api/CraftDetailsForOutofComm/' + event.CraftID(), null,
              function (result) {
                  self.craftoutofcommissionModel().CraftID(result[0].CraftID);
                  self.craftoutofcommissionModel().CraftCode(result[0].CraftCode);
                  self.craftoutofcommissionModel().CraftName(result[0].CraftName);
                  self.craftoutofcommissionModel().CraftType(result[0].CraftType);
                  self.craftoutofcommissionModel().IMONo(result[0].IMONo);
              }, null, null, false);
            }
            else {
                console.log(event.CraftID());
                self.CraftList({ CraftID: event.CraftID, CraftName: event.CraftName, CraftCode: event.CraftCode, CraftType: event.CraftType });
            }
        }

        self.Savecraftoutofcomm = function (model) {
            model.validationEnabled(true);
            self.CraftOutOfCommissionValidation = ko.observable(model);
            self.CraftOutOfCommissionValidation().errors = ko.validation.group(self.CraftOutOfCommissionValidation());
            var errors = self.CraftOutOfCommissionValidation().errors().length;
            toastr.options.closeButton = true;

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/CraftOutOfCommission', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Craft Out Of Commission details added successfully.", "Craft Out Of Commission");
                    $('#spnTitle').html("Craft Out Of Commission");
                    self.LoadCraftOutOfCommissions();
                    self.viewMode('List');
                });
            }
            else {
                self.CraftOutOfCommissionValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }

        self.Modifycraftoutofcomm = function (model) {
            toastr.options.closeButton = true;

            model.validationEnabled(true);
            self.CraftOutOfCommissionValidation = ko.observable(model);
            self.CraftOutOfCommissionValidation().errors = ko.validation.group(self.CraftOutOfCommissionValidation());
            var errors = self.CraftOutOfCommissionValidation().errors().length;
            if (errors == 0) {
                self.viewModelHelper.apiPut('api/CraftOutOfCommission', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Craft Out Of Commission details updated successfully.", "Craft Out Of Commission");
                    $('#spnTitle').html("Craft Out Of Commission");
                    self.LoadCraftOutOfCommissions();
                    self.viewMode('List');
                });
            }
            else {
                self.CraftOutOfCommissionValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }

        self.Initialize();
    }
    IPMSRoot.CraftOutOfCommissionViewModel = CraftOutOfCommissionViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.craftoutofcommissionModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.craftoutofcommissionModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}