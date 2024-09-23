(function (IPMSRoot) {

    var QuayViewModel = function () {

        var self = this;
        $('#spnTitile').html("Quay");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.quayModel = ko.observable();
        self.QuayList = ko.observableArray();
        self.PortList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsQuayCodeEnable = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.UniqueShortNameVisible = ko.observable(false);
        self.IsModified = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.quayModel(new IPMSROOT.QuayModel());
            self.LoadQuays();
            self.viewMode('List');
            self.viewModelHelper.apiGet('api/Ports/GetLoginPort',
            null,
              function (result) {
                  var ports = $.map(result, function (item) {
                      return new self.Port(item);
                  });
                  self.PortList(ports);
              });
        }

        self.Port = function (data) {
            this.PortCode = ko.observable(data.PortCode);
            this.PortName = ko.observable(data.PortCode + '-' + data.PortName);
        };

        self.LoadQuays = function () {
            self.viewModelHelper.apiGet('api/Quays',
            null,
              function (result) {

                  self.QuayList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.QuayModel(item);
                  }));


              });

        }

        self.addquay = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.quayModel(new IPMSRoot.QuayModel());
            $('#spnTitile').html("Add Quay");
        }

        self.SaveQuay = function (model) {
            model.validationEnabled(true);
            self.QuayValidation = ko.observable(model);
            self.QuayValidation().errors = ko.validation.group(self.QuayValidation());
            var errors = self.QuayValidation().errors().length;
            var duplicate = false;
            if (errors == 0) {
                if (self.IsSave()) {
                    $.each(self.QuayList(), function (index, quay) {
                        if ((quay.PortCode()).toLowerCase() == (model.PortCode()).toLowerCase()) {
                            if ((quay.QuayCode()).toLowerCase() == (model.QuayCode()).toLowerCase()) {
                                self.UniqueCodeVisible(true);
                                duplicate = true;
                                self.IsUnique(false);
                            }

                            if ((quay.QuayName()).toLowerCase() == (model.QuayName()).toLowerCase()) {
                                self.UniqueNameVisible(true);
                                duplicate = true;
                                self.IsUnique(false);
                            }
                            if ((quay.ShortName()).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                self.UniqueShortNameVisible(true);
                                duplicate = true;
                                self.IsUnique(false);
                            }
                        }
                        else {
                            if ((quay.ShortName()).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                self.UniqueShortNameVisible(true);
                                duplicate = true;
                                self.IsUnique(false);
                            }
                        }
                        return;
                    });
                }
                if (!duplicate) {
                    self.IsUnique(true);
                }

                if (self.IsUnique() == true) {
                    self.viewModelHelper.apiPost('api/Quays', ko.mapping.toJSON(model), function Message(data) {
                        model.PortName(data.PortName);
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Quay details saved successfully.", "Quay");
                        self.LoadQuays();
                        $('#spnTitile').html("Quay");
                        self.viewMode('List');
                    });
                }
            }
            else {
                self.QuayValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifyQuay = function (model) {
            model.validationEnabled(true);
            self.QuayValidation = ko.observable(model);
            self.QuayValidation().errors = ko.validation.group(self.QuayValidation());
            var errors = self.QuayValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.QuayList(), function (index, quay) {
                        if ((quay.PortCode()).toLowerCase() == (model.PortCode()).toLowerCase()) {
                            if (!((quay.QuayCode()).toLowerCase() == (model.QuayCode()).toLowerCase())) {
                                if ((quay.QuayName()).toLowerCase() == (model.QuayName()).toLowerCase()) {
                                    self.UniqueNameVisible(true);
                                    duplicate = true;
                                    self.IsModified(false);
                                }
                                if ((quay.ShortName()).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                    self.UniqueShortNameVisible(true);
                                    duplicate = true;
                                    self.IsModified(false);
                                }
                            }
                        }
                        else {
                            if ((quay.ShortName()).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                self.UniqueShortNameVisible(true);
                                duplicate = true;
                                self.IsModified(false);
                            }
                            return;
                        }

                    });
                }
                if (!duplicate) {
                    self.IsModified(true);
                }
                if (self.IsModified() == true) {
                    self.viewModelHelper.apiPut('api/Quays', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Quay details updated successfully.", "Quay");
                        $('#spnTitile').html("Quay");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.QuayValidation().errors.showAllMessages();
                return;
            }
        }

        self.viewquay = function (quay) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.quayModel(quay);
            $('#spnTitile').html("View Quay");
        }

        self.editquay = function (quay) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.quayModel(quay);
            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Quay");

        }

        self.ResetQuay = function (model) {

            model.validationEnabled(false);
            ko.validation.reset();
            self.quayModel().reset();
            self.QuayValidation().errors.showAllMessages(false);
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitile').html("Quay");
            self.quayModel(new IPMSRoot.QuayModel());

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
        }

        HandleQuayCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
        }

        HandleQuayNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
        }

        HandleQuayShortNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
        }

        self.Initialize();
    }
    IPMSRoot.QuayViewModel = QuayViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.quayModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.quayModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}

