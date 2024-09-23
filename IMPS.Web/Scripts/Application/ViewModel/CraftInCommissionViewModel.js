(function (IPMSRoot) {

    var CraftInCommissionViewModel = function () {

        var self = this;
        $('#spnTitle').html("Craft Back to Commission");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.craftincommissionModel = ko.observable();
        self.CraftInCommsList = ko.observableArray();
        self.CraftList = ko.observable();
        self.ReasonsList = ko.observable();
        self.CommStatusList = ko.observable();
        self.CraftInCommReferenceData = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.editableRemarks = ko.observable(true);
        self.Dataeditable = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode('List');
            self.craftincommissionModel(new IPMSROOT.CraftInCommissionModel());
            self.LoadCraftInCommissions();
            self.LoadCrafts();
            self.LoadReasons();
            self.LoadCommStatus();
        }

        self.LoadCraftInCommissions = function () {
            self.viewModelHelper.apiGet('api/CraftInCommissionDetails', null,
              function (result) {
                  self.CraftInCommsList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.CraftInCommissionModel(item);
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

        self.editCraftIncomm = function (CraftIncomm) {
            self.viewMode('Form');
            self.craftincommissionModel().Reason(CraftIncomm.Reason());
            self.editableView(false);
            self.editableRemarks(true);
            self.Dataeditable(false);
            self.LoadCraftsbasedonCraftID(CraftIncomm.CraftID());
            self.IsReset(true);
            self.IsUpdate(true);
            $('#spnTitle').html("Update Craft Back to Commission");
            self.craftincommissionModel(CraftIncomm);
        }

        self.viewCraftIncomm = function (CraftIncomm) {
            self.viewMode('Form');
            self.IsReset(false);
            self.editableView(false);
            self.editableRemarks(false);
            self.IsUpdate(false);
            self.Dataeditable(false);
            self.LoadCraftsbasedonCraftID(CraftIncomm.CraftID());
            $('#spnTitle').html("View Craft Back to Commission");
            self.craftincommissionModel(CraftIncomm);
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("Craft Back to Commission");
            self.LoadCraftInCommissions();
            self.craftincommissionModel(new IPMSRoot.CraftInCommissionModel());
        }

        self.ResetCraftIncomm = function (model) {
            ko.validation.reset();
            model.validationEnabled(false);
            self.craftincommissionModel().reset();
            self.CraftInCommissionValidation = ko.observable(model);
            self.CraftInCommissionValidation().errors = ko.validation.group(self.CraftInCommissionValidation());
            self.CraftInCommissionValidation().errors.showAllMessages(false);
        }

        self.Modifycraftincomm = function (model) {
            toastr.options.closeButton = true;
            model.CraftCommissionStatus('IC');
            model.validationEnabled(true);
            self.CraftInCommissionValidation = ko.observable(model);
            self.CraftInCommissionValidation().errors = ko.validation.group(self.CraftInCommissionValidation());
            var errors = self.CraftInCommissionValidation().errors().length;
            if (errors == 0) {
                self.viewModelHelper.apiPut('api/CraftInCommission', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Craft back to commission details updated successfully.", "Craft Back to Commission");
                    $('#spnTitle').html("Craft Back to Commission");
                    self.LoadCraftInCommissions();
                    self.viewMode('List');
                });
            }
            else {
                self.CraftInCommissionValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }

        self.Initialize();
    }
    IPMSRoot.CraftInCommissionViewModel = CraftInCommissionViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.craftincommissionModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.craftincommissionModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}