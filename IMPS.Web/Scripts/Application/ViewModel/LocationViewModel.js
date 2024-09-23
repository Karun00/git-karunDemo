(function (IPMSRoot) {

    var LocationViewModel = function () {

        var self = this;
        $('#spnTitile').html("Location");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.locationModel = ko.observable();
        self.LocationList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.PortList = ko.observableArray();
        self.UniqueNameVisible = ko.observable(false);
        self.LocationPort = ko.observable();
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.locationModel(new IPMSROOT.LocationModel());
            self.Loadlocations();
            self.viewMode('List');

            self.viewModelHelper.apiGet('api/Ports/GetLoginPort', null, function (result) {
                var ports = $.map(result, function (item) {
                    return new self.Port(item);
                });
                self.PortList(ports);
            });
        }

        self.Port = function (data) {
            this.PortCode = ko.observable(data.PortCode);
            this.PortName = ko.observable(data.PortName);
        };

        self.Loadlocations = function () {
            self.viewModelHelper.apiGet('api/Locations',
            null,
              function (result) {

                  self.LocationList(ko.utils.arrayMap(result, function (item) {

                      self.LocationPort(item.LocationPortCode);
                      return new IPMSRoot.LocationModel(item);

                  }));


              }, null, null, false);

        }

        self.addlocation = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.IsReset(true);
            self.locationModel(new IPMSRoot.LocationModel());

            $('#spnTitile').html("Add Location");
        }

        self.SaveLocation = function (model) {

            model.validationEnabled(true);
            self.LocationValidation = ko.observable(model);
            self.LocationValidation().errors = ko.validation.group(self.LocationValidation());
            var errors = self.LocationValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {

                if (self.IsSave()) {
                    $.each(self.LocationList(), function (index, location) {

                        if ((location.PortCode()).toLowerCase() == (self.LocationPort()).toLowerCase()) {

                            if ((location.LocationName()).toLowerCase() == (model.LocationName()).toLowerCase()) {

                                self.UniqueNameVisible(true);
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
                    self.viewModelHelper.apiPost('api/Locations', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Location details saved successfully", "Location");
                        self.Loadlocations();
                        $('#spnTitile').html("Location");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.LocationValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyLocation = function (model) {
            model.validationEnabled(true);
            self.LocationValidation = ko.observable(model);
            self.LocationValidation().errors = ko.validation.group(self.LocationValidation());
            var errors = self.LocationValidation().errors().length;
            var duplicate = false;
            self.IsModified(true);
            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.LocationList(), function (index, location) {

                        if (!((location.LocationID()) == (model.LocationID()))) {

                            // alert(location.PortCode());
                            if ((location.LocationName()).toLowerCase() == (model.LocationName()).toLowerCase()) {
                                //alert(location.LocationName());
                                //alert(model.LocationName());
                                self.UniqueNameVisible(true);
                                duplicate = true;
                                self.IsModified(false);
                            }
                        }


                        return;
                    });
                }
                if (!duplicate) {
                    self.IsModified(true);
                }

                if (self.IsModified() == true) {
                    self.viewModelHelper.apiPut('api/Locations', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Location details updated successfully", "Location");
                        $('#spnTitile').html("Location");
                        self.Loadlocations();
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.LocationValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ResetLocation = function (model) {

            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.locationModel().reset();
            self.LocationValidation().errors.showAllMessages(false);
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.locationModel().reset();
            $('#spnTitile').html("Location");
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }


        }

        self.viewlocation = function (location) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.editableView(false);
            self.locationModel(location);
            $('#spnTitile').html("View location");
        }

        self.editlocation = function (location) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.locationModel(location);
            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Location");

        }

        HandleLocationNameKeyUp = function (data, event) {


            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        //To validate alphabet Numeric with spaces
        function ValidateAlphaNumericWithSpaces(data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[[a-zA-Z][0-9]\b ]*$/;
            return charcheck.test(keychar);
        }

        self.Initialize();
    }
    IPMSRoot.LocationViewModel = LocationViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.locationModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.locationModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}








