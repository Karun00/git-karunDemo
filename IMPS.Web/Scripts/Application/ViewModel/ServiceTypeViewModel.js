(function (IPMSRoot) {

    var ServiceTypeViewModel = function () {

        var self = this;
        $('#spnTitile').html("Service Type");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.serviceTypeModel = ko.observable();
        self.ServiceTypeList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.serviceTypeModel(new IPMSROOT.ServiceTypeModel());
            self.LoadServiceTypes();
            self.viewMode('List');
        }

        self.LoadServiceTypes = function () {
            self.viewModelHelper.apiGet('api/ServiceTypesList', null,
              function (result) {
                  self.ServiceTypeList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.ServiceTypeModel(item);
                  }));
              }, null, null, false);
        }

        self.addServiceType = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.IsReset(true);
            self.serviceTypeModel(new IPMSRoot.ServiceTypeModel());

            $('#spnTitile').html("Add Service Type");
        }

        self.SaveServiceType = function (model) {
            model.validationEnabled(true);
            self.ServiceTypeValidation = ko.observable(model);
            self.ServiceTypeValidation().errors = ko.validation.group(self.ServiceTypeValidation());
            var errors = self.ServiceTypeValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsSave()) {
                    $.each(self.ServiceTypeList(), function (index, sertype) {
                        if ((sertype.ServiceTypeCode()).toLowerCase() == (model.ServiceTypeCode()).toLowerCase()) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }
                        if ((sertype.ServiceTypeName()).toLowerCase() == (model.ServiceTypeName()).toLowerCase()) {
                            self.UniqueNameVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }
                        return;
                    });
                }

                if (!duplicate) {
                    self.IsUnique(true);
                }
                if (self.IsUnique() == true) {
                    self.viewModelHelper.apiPost('api/ServiceTypeDetails', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Service type details saved successfully.", "Service Type");
                        self.LoadServiceTypes();
                        $('#spnTitile').html("Service Type");
                        self.viewMode('List');
                    });
                }
            }
            else {
                self.ServiceTypeValidation().errors.showAllMessages();
                //$('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyServiceType = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            self.UniqueNameVisible(false);
            self.UniqueCodeVisible(false);

            model.validationEnabled(true);

            self.ServiceTypeValidation = ko.observable(model);
            self.ServiceTypeValidation().errors = ko.validation.group(self.ServiceTypeValidation());
            var errors = self.ServiceTypeValidation().errors().length;
            var duplicate = false;
            self.IsModified(true);

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.ServiceTypeList(), function (index, sertype) {
                        if (self.IsCodeEnable() == true && ((sertype.ServiceTypeCode()).toLowerCase() == (model.ServiceTypeCode()).toLowerCase())) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                            self.IsModified(false);
                        }
                        if (self.IsCodeEnable() == true && ((sertype.ServiceTypeName()).toLowerCase() == (model.ServiceTypeName()).toLowerCase())) {
                            self.UniqueNameVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                            self.IsModified(false);
                        }
                        if (self.IsCodeEnable() == true && ((duplicate == true) && sertype.IsCraft() != model.IsCraft())) {
                            self.IsUnique(true);
                        }
                        return;
                    });
                }

                if (!duplicate) {
                    self.IsModified(true);
                }
                if (self.IsUnique() == true) {
                    if (self.IsModified() == true) {
                        self.viewModelHelper.apiPut('api/ServiceTypeDetails', ko.mapping.toJSON(model), function Message(data) {
                            toastr.success("Service type details updated successfully.", "Service Type");
                            $('#spnTitile').html("Service Type");
                            self.LoadServiceTypes();
                            self.UniqueNameVisible(false);
                            self.UniqueCodeVisible(false);
                            self.viewMode('List');
                        });
                    }
                }
                else {
                    toastr.error("Please enter name and code uniquely." + model.Slot(), "Service Type");
                }
            }
            else {
                self.ServiceTypeValidation().errors.showAllMessages();
                //$('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ResetServiceType = function (model) {

            self.serviceTypeModel().reset();
            model.validationEnabled(false);
            ko.validation.reset();
            ResetErrorMessages(self.serviceTypeModel());

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        //To reset error messages
        function ResetErrorMessages(model) {
            model.ServiceTypeCode.isModified(false);
            model.ServiceTypeName.isModified(false);
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.serviceTypeModel().reset();
            $('#spnTitile').html("Service Type");
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
        }

        self.viewServiceType = function (ServiceType) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.editableView(false);
            self.serviceTypeModel(ServiceType);
            $('#spnTitile').html("View Service Type");
        }

        self.editServiceType = function (ServiceType) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.serviceTypeModel(ServiceType);
            self.IsCodeEnable(false);
            self.editableView(true);
            $('#spnTitile').html("Update Service Type");
        }

        HandleServiceTypeCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleServiceTypeNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
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
    IPMSRoot.ServiceTypeViewModel = ServiceTypeViewModel;
}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.ServiceTypeModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.ServiceTypeModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}








