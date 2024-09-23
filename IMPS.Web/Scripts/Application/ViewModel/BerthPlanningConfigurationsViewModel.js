(function (IPMSRoot) {

    var BerthPlanningConfigurationsViewModel = function () {

        var self = this;
        $('#spnTitle').html("Berth Planning Configurations");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.berthplanningconfigurationsModel = ko.observable();
        self.BerthPlanningConfigsList = ko.observableArray();
        self.PortList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.UniqueDaysandSlotVisible = ko.observable(false);
        self.UniquePortVisible = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode('List');
            self.berthplanningconfigurationsModel(new IPMSROOT.BerthPlanningConfigurationsModel());
            self.IsUpdate(false);
            self.LoadBerthPlanningConfigurations();
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null,
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

        self.LoadBerthPlanningConfigurations = function () {
            self.viewModelHelper.apiGet('api/BerthPlanningConfigurations', null,
              function (result) {
                  self.BerthPlanningConfigsList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.BerthPlanningConfigurationsModel(item);
                  }));
              }, null, null, false);
        }

        self.addberthplanconfig = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.berthplanningconfigurationsModel(new IPMSRoot.BerthPlanningConfigurationsModel());
            $('#spnTitle').html("Add Berth Planning Configurations");
        }
      
        self.editberthplanconfig = function (berthplanconfig) {
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsReset(true);
            self.editableView(true);
            $('#spnTitle').html("Update Berth Planning Configurations");
            self.berthplanningconfigurationsModel(berthplanconfig);
        }

        self.viewberthplanconfig = function (berthplanconfig) {
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.editableView(false);
            $('#spnTitle').html("View Berth Planning Configurations");
            self.berthplanningconfigurationsModel(berthplanconfig);
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("Berth Planning Configurations");
            self.berthplanningconfigurationsModel(new IPMSRoot.BerthPlanningConfigurationsModel());
        }

        self.Resetberthplanconfig = function (model) {
               ko.validation.reset();
                model.validationEnabled(false);
                self.berthplanningconfigurationsModel().reset();
                self.BerthPlanningConfigurationsValidation = ko.observable(model);
                self.BerthPlanningConfigurationsValidation().errors = ko.validation.group(self.BerthPlanningConfigurationsValidation());
                self.BerthPlanningConfigurationsValidation().errors.showAllMessages(false);
        }

        self.Saveberthplanconfig = function (model) {
            model.validationEnabled(true);
            self.BerthPlanningConfigurationsValidation = ko.observable(model);
            self.BerthPlanningConfigurationsValidation().errors = ko.validation.group(self.BerthPlanningConfigurationsValidation());
            var errors = self.BerthPlanningConfigurationsValidation().errors().length;
            var Duplicate = false;
            $.each(self.BerthPlanningConfigsList(), function (index, berthplanconfig) {
                if ((berthplanconfig.PortCode()).toLowerCase() == (model.PortCode()).toLowerCase()) {
                    self.UniquePortVisible(true);
                    Duplicate = true;
                    self.IsUnique(false);
                }
                return;
            });
            if (!Duplicate) {
                self.IsUnique(true);
            }
            if (self.IsUnique() == true) {

                var Duplicate1 = false;
                $.each(self.BerthPlanningConfigsList(), function (index, berthplanconfig) {
                    if (berthplanconfig.Days() == model.Days() && berthplanconfig.Slot() == model.Slot()) {
                        Duplicate1 = true;
                        self.IsUnique(false);
                    } return;
                });
                if (!Duplicate1) {
                    self.IsUnique(true);
                }
                if (self.IsUnique() == true) {
                    if (errors == 0) {
                        self.viewModelHelper.apiPost('api/BerthPlanningConfigurations', ko.mapping.toJSON(model), function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Berth Planning Configurations details added successfully", "Berth Planning Configurations");
                            $('#spnTitle').html("Berth Planning Configurations");
                            self.viewMode('List');
                        });
                    }
                    else {
                        self.BerthPlanningConfigurationsValidation().errors.showAllMessages();
                        $('#divValidationError').removeClass('display-none');
                        return;
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.error("Please select Days and slots other than " + model.Days() + " and " + model.Slot(), "Berth Planning Configurations");
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("Please select Port other than " + model.PortCode() , "Berth Planning Configurations");
            }
        }

        self.Modifyberthplanconfig = function (model) {
            model.validationEnabled(true);
            self.BerthPlanningConfigurationsValidation = ko.observable(model);
            self.BerthPlanningConfigurationsValidation().errors = ko.validation.group(self.BerthPlanningConfigurationsValidation());
            var errors = self.BerthPlanningConfigurationsValidation().errors().length;
            var Duplicate = false;
            $.each(self.BerthPlanningConfigsList(), function (index, berthplanconfig) {
                if (!(berthplanconfig.BerthPlanConfigid() == model.BerthPlanConfigid())) {
                    if ((berthplanconfig.PortCode()).toLowerCase() == (model.PortCode()).toLowerCase()) {
                        self.UniquePortVisible(true);
                        Duplicate = true;
                        self.IsUnique(false);
                    }
                }
                return;
            });
            if (!Duplicate) {
                self.IsUnique(true);
            }
            if (self.IsUnique() == true) {
                var Duplicate1 = false;
                $.each(self.BerthPlanningConfigsList(), function (index, berthplanconfig) {
                    if (!(berthplanconfig.BerthPlanConfigid() == model.BerthPlanConfigid())) {
                        if (berthplanconfig.Days() == model.Days() && berthplanconfig.Slot() == model.Slot()) {
                            Duplicate1 = true;
                            self.IsUnique(false);
                        }
                    }
                    return;
                });
                if (!Duplicate1) {
                    self.IsUnique(true);
                }
                if (self.IsUnique() == true) {
                    if (errors == 0) {
                        self.viewModelHelper.apiPut('api/BerthPlanningConfigurations', ko.mapping.toJSON(model), function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Berth Planning Configurations details updated successfully", "Berth Planning Configurations");
                            $('#spnTitle').html("Berth Planning Configurations");
                            self.viewMode('List');
                        });
                    }
                    else {
                        self.BerthPlanningConfigurationsValidation().errors.showAllMessages();
                        $('#divValidationError').removeClass('display-none');
                        return;
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.error("Please select Days and slots other than " + model.Days() + " and " + model.Slot(), "Berth Planning Configurations");
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("Please select Port other than " + model.PortCode(), "Berth Planning Configurations");
            }
        }

        self.ValidEvent = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.BerthPlanningConfigsList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.PortCode == entry.PortCode) {
                    $('#spanportid').text('Already exists ! enter other Code');
                    $('#spanportid').css('display', '');
                }
                return;
            });
        }

        HandleKeyUp = function (data, event) {
            if ($('#spanportid').is(':visible')) {
                $('#spanportid').css('display', 'none');
            }
            else {
                $('#spanportid').css('display', '');
            }
        }

        self.Initialize();
    }
    IPMSRoot.BerthPlanningConfigurationsViewModel = BerthPlanningConfigurationsViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.berthplanningconfigurationsModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.berthplanningconfigurationsModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}