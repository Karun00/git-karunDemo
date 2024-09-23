(function (IPMSRoot) {

    var ServiceTypeDesignationViewModel = function () {

        var self = this;
        $('#spnTitile').html("Service Type Designation");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.ServiceTypeDesignationModel = ko.observable(new IPMSROOT.ServiceTypeDesignationModel());
        self.ServiceTypeDesignationList = ko.observableArray();
        self.CraftTypeDetails = ko.observableArray();
        self.designationValues = ko.observableArray();
        self.isCraft = ko.observable();

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsAdd = ko.observable(false);
        self.selectedServiceTypeName = ko.observable(false);
        self.selectedServiceTypeCode = ko.observable(false);
        self.isSTDValEnable = ko.observable(true);
        self.IsValid = ko.observable(false);

        self.UniqueDesignationCodeVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadDesignations();
            self.LoadCraftTypes();
            self.LoadServiceTypeDesignations();
            self.viewMode('List');
        }

        //LoadServiceTypeDesignations fetches the Service Type Designations data from API Service
        self.LoadServiceTypeDesignations = function () {
            self.viewModelHelper.apiGet('api/ServiceTypeDesignationsList', null,
              function (result) {
                  self.ServiceTypeDesignationList(ko.utils.arrayMap(result, function (item) {

                      return new IPMSRoot.ServiceTypeDesignationModel(item);
                  }, null, null, false));
              });
        }

        //LoadDesignations fetches the Designation details from API Service
        self.LoadDesignations = function () {
            self.viewModelHelper.apiGet('api/GetDesignations', null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.designationValues);
              }, null, null, false);
        }

        // Load All Craft Types Data
        self.LoadCraftTypes = function () {
            self.viewModelHelper.apiGet('api/GetCraftTypeDetails', null, function (result) {
                ko.mapping.fromJS(result, {}, self.CraftTypeDetails);
            }, null, null, false);
        }

        //change Event for DesignationCode
        ChangeDesignationCode = function (data, event) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var duplicate = false;
            var selectedDesignationCode = data.DesignationCode();
            var obj = self.ServiceTypeDesignationModel().ServiceTypeDesignations();
            var iCount = 0;
            var rid = 0;
            self.thisServiceTypeDesignations = ko.observableArray();
            self.thisServiceTypeDesignations = obj;
            self.Valid = ko.observable();
            self.Valid = self.thisServiceTypeDesignations.filter(function (a) {
                rid = rid + 1;
                if (a.DesignationCode() == selectedDesignationCode) {
                    iCount++;
                    duplicate = true;
                    return true;
                }
                else {
                    duplicate = false;
                    return false;
                }
            });

            if (iCount > 1) {
                toastr.warning("The selected designation already has been added.", "Service Type Designation");
                self.IsSaveUpdateDisabled(true);
            }
            else {
                self.IsSaveUpdateDisabled(false);
            }
        }

        self.SaveServiceTypeDesignation = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.ServiceTypeDesignationValidation = ko.observable(model);
            self.ServiceTypeDesignationValidation().errors = ko.validation.group(self.ServiceTypeDesignationValidation());
            var errors = self.ServiceTypeDesignationValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsSave() == true) {
                    self.viewModelHelper.apiPost('api/ServiceTypeDesignationDetails', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.success("Service type designation details saved successfully.", "Service Type Designation");
                        self.LoadServiceTypeDesignations();
                        $('#spnTitile').html("Service Type Designation");
                        self.viewMode('List');
                    });
                }
            }
            else {
                self.ServiceTypeDesignationValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifyServiceTypeDesignation = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.ServiceTypeDesignationValidation = ko.observable(model.ServiceTypeDesignations);
            self.ServiceTypeDesignationValidation().errors = ko.validation.group(self.ServiceTypeDesignationValidation());
            var errors = self.ServiceTypeDesignationValidation().errors().length;
            var rid = 0;

            if (errors == 0) {
                var checkValid = false;
                var parentIsCraftStatus = model.IsCraft();
                var obj = model.ServiceTypeDesignations();
                var iCount = 0;
                var rid = 0;
                self.thisServiceTypeDesignations = ko.observableArray();
                self.thisServiceTypeDesignations = obj;
                self.Valid = ko.observable();
                self.Valid = self.thisServiceTypeDesignations.filter(function (a) {
                    rid = rid + 1;

                    if ((a.DesignationCode() == "" || a.DesignationCode() == undefined)) {
                        iCount++;
                        checkValid = true;
                        toastr.warning("Please select designation.", "Service Type Designation");
                        return true;
                    }
                    else if (model.IsCraft()) {
                        if ((a.CraftType() == "" || a.CraftType() == undefined) && parentIsCraftStatus == true) {
                            iCount++;
                            checkValid = true;
                            toastr.warning("Please select craft type.", "Service Type Designation");
                            return true;
                        }
                    }
                    else {
                        checkValid = false;
                        return false;
                    }
                });

                if (iCount > 0) {
                    self.IsModified(false);
                }
                else {
                    self.IsModified(true);
                }

                if (self.IsModified() == true) {
                    self.viewModelHelper.apiPut('api/ServiceTypeDesignationDetails', ko.mapping.toJSON(model), function Message(data) {
                        toastr.success("Service type designation details updated successfully.", "Service Type Designation");
                        self.LoadServiceTypeDesignations();
                        $('#spnTitile').html("Service Type Designation");
                        self.viewMode('List');
                    });
                }
            }
            else {
                return;
            }
        }

        self.ResetServiceTypeDesignation = function (model) {
            ko.validation.reset();
            model.validationEnabled(false);
            self.ServiceTypeDesignationModel().reset();
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.ServiceTypeDesignationModel().reset();
            self.isSTDValEnable(true);
            $('#spnTitile').html("Service Type Designation");
        }

        self.viewServiceTypeDesignation = function (ServiceTypeDesignation) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.ServiceTypeDesignationModel(ServiceTypeDesignation);
            self.IsAdd(false);
            self.isCraft("");
            self.isSTDValEnable(false);
            self.selectedServiceTypeName(ServiceTypeDesignation.ServiceTypeName())
            self.selectedServiceTypeCode(ServiceTypeDesignation.ServiceTypeCode())
            self.isCraft(ServiceTypeDesignation.IsCraft());
            self.editableView(false);

            $('#spnTitile').html("View Service Type Designation");
        }

        self.editServiceTypeDesignation = function (ServiceTypeDesignation) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.ServiceTypeDesignationModel(ServiceTypeDesignation);
            self.IsCodeEnable(false);
            self.IsAdd(true);
            self.isSTDValEnable(true);
            self.selectedServiceTypeName(ServiceTypeDesignation.ServiceTypeName())
            self.selectedServiceTypeCode(ServiceTypeDesignation.ServiceTypeCode())
            self.isCraft("");
            self.isCraft(ServiceTypeDesignation.IsCraft());

            $('#spnTitile').html("Update Service Type Designation");
        }

        self.addServiceTypeDesignation = function (Data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var databaseList = ko.toJSON(self.ServiceTypeDesignationModel().ServiceTypeDesignations);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var errors = 0;
            $.each(jsonObj, function (index, value) {
                var DesignationCode = value.DesignationCode;
                var strDesignationCode = "";
                if ((DesignationCode != "") && (DesignationCode != undefined)) {
                    strDesignationCode = "SSS";
                }

                rid = rid + 1;

                if (strDesignationCode == "") {
                    errors = errors + 1;
                    toastr.warning("Please select designation.", "Service Type Designation");
                    self.IsValid(false);
                    return;
                }

                if (Data.IsCraft()) {
                    var CraftType = value.CraftType;
                    var strCraftType = "";
                    if ((CraftType != "") && (CraftType != undefined)) {
                        strCraftType = "SSS";
                    }

                    if (strCraftType == "") {
                        errors = errors + 1;
                        toastr.warning("Please select crafttype.", "Service Type Designation");
                        self.IsValid(false);
                        return;
                    }
                }
                return;
            });
            if (errors == 0) {
                self.ServiceTypeDesignationModel().ServiceTypeDesignations.push(new IPMSROOT.ServiceTypeDesignationModel());
            }
        }

        self.removeDesignations = function (Data) {
            self.ServiceTypeDesignationModel().ServiceTypeDesignations.remove(Data);
        }

        HandleDesignationCodeKeyUp = function (data, event) {
            if (self.UniqueDesignationCodeVisible() == true) {
                self.UniqueDesignationCodeVisible(false);
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
    IPMSRoot.ServiceTypeDesignationViewModel = ServiceTypeDesignationViewModel;
}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.ServiceTypeDesignationModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.ServiceTypeDesignationModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}








