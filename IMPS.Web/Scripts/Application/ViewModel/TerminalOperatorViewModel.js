ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});
toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot) {
    var isView = 0;

    var TerminalOperatorViewModel = function () {

        var self = this;

        $('#spnTitle').html("Terminal Operator");
        self.validationHelperDate = new IPMSROOT.validationHelper();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = ko.observable();
        self.terminaloperatorList = ko.observableArray([]);
        self.terminaloperatorModel = ko.observable();
        self.masterQuays = ko.observableArray([]);
        self.masterCargoTypes = ko.observableArray([]);

        self.PortReferenceData = ko.observable();

        self.viewMode = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.checkedValue = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsRegistrationNoEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);

        //Validation Visible
        self.isNumStreetMsg = ko.observable(false);
        self.isSuburbMsg = ko.observable(false);
        self.isTownCityMsg = ko.observable(false);
        self.isPostalCodeMsg = ko.observable(false);
        self.isNumStreet2Msg = ko.observable(false);
        self.isSuburb2Msg = ko.observable(false);
        self.isTownCity2Msg = ko.observable(false);
        self.isPostalCode2Msg = ko.observable(false);
        self.isRegistrationDateMsg = ko.observable(false);
        self.isValidityDateMsg = ko.observable(false);
        self.ischkmsg = ko.observable(false);
        self.isSameAsBuisinessAddress = ko.observable(false);
        self.PortTempArray = ko.observableArray();
        self.PortLoopArray = ko.observableArray();
        self.PortNameLoopArray = ko.observableArray();
        self.TerminalOperatorPortsNameArr = ko.observableArray();

        self.isResidentialAddressCountry = ko.observable(false);
        self.isPostalAddressCountry = ko.observable(false);
        self.CountriesList = ko.observableArray();

        self.validationTextMessage = ko.observable('* This field is required.');

        self.PortLoop = ko.observableArray();

        self.Initialize = function () {
            self.validationHelper(new IPMSRoot.validationHelper());
            self.LoadAllPorts();
            self.LoadCargos();
            self.LoadCountries();
            self.viewMode('List');
        }

        // this is for grid binding
        // LoadTerminalOparators Used For fetch the data for List from backend
        self.LoadTerminalOparators = function () {
            self.viewModelHelper.apiGet('api/TerminalOperatorMaster', null, function (result) {
                ko.mapping.fromJS(result, {}, self.terminaloperatorList);
                self.terminaloperatorList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.TerminalOperatorModel(item, self.masterQuays(), self.masterCargoTypes());
                }));
            }, null, null, false);
        }

        self.LoadAllPorts = function () {
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
                self.PortReferenceData(new IPMSRoot.PortReferenceData(result));
                self.PortTempArray(result);
            }, null, null, false);
        }

        //Countries list from Vessel Nationality
        self.LoadCountries = function () {
            self.viewModelHelper.apiGet('api/SubCategory/GetCountriesList', null, function (result) {

                self.CountriesList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Countries(item);
                }));
            });
        }

        // LoadQuays Used For fetch the Queydata for Dropdown and berth detailsgrid from backend
        self.LoadQuays = function (model) {
            if (model.PortValue() == undefined) {
                self.masterQuays(undefined);
                return;
            }

            self.viewModelHelper.apiGet('api/GetQuays', { portCode: model.PortValue() }, function (result) {
                self.masterQuays(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Quay(item);
                    self.LoadTerminalOparators(item);
                }));
            });

            self.terminaloperatorModel().selectedQuayKey.subscribe(function (key) {
                if (key !== undefined) {
                    var quaySelected = self.masterQuays().filter(function (item) {
                        return item.QuayKey() === key;
                    })[0];
                    self.terminaloperatorModel().selectedQuay(ko.toJS(quaySelected));
                }
            });
        }

        self.load = function () {
            self.terminaloperatorModel().selectedQuayKey.subscribe(function (key) {
                if (key !== undefined) {
                    var quaySelected = self.masterQuays().filter(function (item) {
                        return item.QuayKey() === key;
                    })[0];
                    self.terminaloperatorModel().selectedQuay(ko.toJS(quaySelected));
                }
                else {
                    self.terminaloperatorModel().selectedQuay(undefined);
                }
            });
        }

        // LoadCargos Used For fetch the cargodata for  CargoDetailsGrid from backend
        self.LoadCargos = function () {
            self.viewModelHelper.apiGet('api/CargoTypes', null, function (result) {
                self.LoadTerminalOparators();
                self.masterCargoTypes(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Cargo(item);
                }));
            });
        }

        // viewTerminalOperator method is fires when View button press in View mode
        self.viewTerminalOperator = function (terminalOperator) {

            self.PortLoopArray.removeAll();
            self.val1 = ko.observableArray();
            self.temp1 = ko.observableArray();
            self.val1 = self.PortTempArray();
            temp1 = terminalOperator.TerminalOperatorPortsArr();

            for (var i = 0, n = temp1.length ; i < n; i++) {
                var users1 = ko.utils.arrayFilter(self.PortReferenceData().PortRequesttypes(), function (item) {
                    if (item.PortCode() == temp1[i]) {
                        self.PortLoopArray.push(item);
                    }
                });
            }
            terminalOperator.TerminalOperatorPortsNameArr = self.PortLoopArray();

            self.terminaloperatorModel(terminalOperator);
            self.viewMode("Form");
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsSave(false);
            self.IsActivityEnable(false);
            self.IsRegistrationNoEnable(false);
            self.IsCodeEnable(false);
            $("#portdropdown").prop("disabled", false);
            $('#chkBuisinessAddress').prop("disabled", true);
            self.isSameAsBuisinessAddress(true);
            $('#spnTitle').html("View Terminal Operator");

            self.isNumStreetMsg(false);
            self.isSuburbMsg(false);
            self.isTownCityMsg(false);
            self.isResidentialAddressCountry(false);
            self.isPostalAddressCountry(false);
            self.isPostalCodeMsg(false);
            self.isNumStreet2Msg(false);
            self.isSuburb2Msg(false);
            self.isTownCity2Msg(false);
            self.isPostalCode2Msg(false);
            self.isRegistrationDateMsg(false);
            self.isValidityDateMsg(false);
            $('#allchks').attr('disabled', 'disabled');
            if (terminalOperator.TerminalOperatorServiceTypesArray().length > 1) {
                $("#chk1").prop('checked', true);
                $("#berthiddiv").show();
                $("#chk2").prop('checked', true);
                $("#divcargo").show();
            }
            else {
                $.each(terminalOperator.TerminalOperatorServiceTypesArray(), function (key, val) {
                    if (val == "TOB") {
                        $("#chk1").prop('checked', true);
                        $("#berthiddiv").show();
                        $("#divcargo").hide();
                    } else {
                        $("#chk2").prop('checked', true);
                        $("#divcargo").show();
                        $("#berthiddiv").hide();
                    }
                });
            }

            var cnt = 0;
            $('#divPortList input[name="port"]').each(function () {
                if (this.checked)
                    cnt += 1;
            });

            if ($('#divPortList input[name="port"]').length == cnt)
                $('#allchks').attr('checked', true);

            if (terminalOperator.IsSameBusinessAdd()) {
                $("#chkBuisinessAddress").attr('checked', true);
                self.isSameAsBuisinessAddress(true);
            }
            else {
                $('#chkBuisinessAddress').attr('checked', false);
                self.isSameAsBuisinessAddress(false);
            }
        }

        // EditTerminalOperator method is fires when Edit button press in Edit mode
        self.EditTerminalOperator = function (terminalOperator) {

            $('#allchks').prop('disabled', true);
            $('#allchks').attr('disabled', true);

            self.PortLoopArray.removeAll();
            self.val1 = ko.observableArray();
            self.temp1 = ko.observableArray();
            self.val1 = self.PortTempArray();
            temp1 = terminalOperator.TerminalOperatorPortsArr();

            for (var i = 0, n = temp1.length ; i < n; i++) {
                var users1 = ko.utils.arrayFilter(self.PortReferenceData().PortRequesttypes(), function (item) {
                    if (item.PortCode() == temp1[i]) {
                        self.PortLoopArray.push(item);
                        $('#port' + temp1[i]).prop('disabled', true);
                    }
                });
            }
            terminalOperator.TerminalOperatorPortsNameArr = self.PortLoopArray();

            self.terminaloperatorModel(terminalOperator);
            self.viewMode("Form");
            self.IsUpdate(true);
            self.IsReset(true);
            self.IsSave(false);
            self.IsActivityEnable(true);
            self.IsRegistrationNoEnable(false);
            self.IsCodeEnable(true);
            $('#spnTitle').html("Update Terminal Operator");

            self.isNumStreetMsg(false);
            self.isSuburbMsg(false);
            self.isTownCityMsg(false);
            self.isResidentialAddressCountry(false);
            self.isPostalAddressCountry(false);
            self.isPostalCodeMsg(false);
            self.isNumStreet2Msg(false);
            self.isSuburb2Msg(false);
            self.isTownCity2Msg(false);
            self.isPostalCode2Msg(false);
            self.isRegistrationDateMsg(false);
            self.isValidityDateMsg(false);

            if (terminalOperator.TerminalOperatorServiceTypesArray().length > 1) {

                $("#chk1").prop('checked', true);
                $("#berthiddiv").show();
                $("#chk2").prop('checked', true);
                $("#divcargo").show();

            } else {
                $.each(terminalOperator.TerminalOperatorServiceTypesArray(), function (key, val) {
                    if (val == "TOB") {
                        $("#chk1").prop('checked', true);
                        $("#berthiddiv").show();
                        $("#divcargo").hide();
                    } else {
                        $("#chk2").prop('checked', true);
                        $("#divcargo").show();
                        $("#berthiddiv").hide();
                    }
                });
            }

            var cnt = 0;
            $('#divPortList input[name="port"]').each(function () {
                if (this.checked)
                    cnt += 1;
            });

            if ($('#divPortList input[name="port"]').length == cnt)
                $('#allchks').attr('checked', true);

            $("#telephoneNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#faxNum").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            if (terminalOperator.IsSameBusinessAdd()) {
                $("#chkBuisinessAddress").attr('checked', true);
                self.isSameAsBuisinessAddress(true);
            }
            else {
                $('#chkBuisinessAddress').attr('checked', false);
                self.isSameAsBuisinessAddress(false);
            }
        }

        // addTerminalOperator method is fires when Addnew button press in Add mode
        self.addTerminalOperator = function () {

            self.terminaloperatorModel(new IPMSROOT.TerminalOperatorModel(undefined, self.masterQuays(), self.masterCargoTypes()));
            self.viewMode("Form");
            $("#berthiddiv").hide();
            $("#divcargo").hide();

            self.IsSave(true);
            self.IsReset(true);
            self.IsUpdate(false);
            self.selectedChoices();
            self.IsRegistrationNoEnable(true);
            self.IsActivityEnable(false);
            self.IsCodeEnable(true);
            $('#spnTitle').html("Add Terminal Operator");
            self.isNumStreetMsg(false);
            self.isSuburbMsg(false);
            self.isTownCityMsg(false);
            self.isResidentialAddressCountry(false);
            self.isPostalAddressCountry(false);
            self.isPostalCodeMsg(false);
            self.isNumStreet2Msg(false);
            self.isSuburb2Msg(false);
            self.isTownCity2Msg(false);
            self.isPostalCode2Msg(false);
            self.isRegistrationDateMsg(false);
            self.isValidityDateMsg(false);

            $("#telephoneNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#faxNum").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        // ResetTerminalOperators method is fires when press the Reset button and set the all fields data in the stage that in page load time
        self.ResetTerminalOperators = function (data) {
            if (self.IsSave()) {
                self.terminaloperatorModel(new IPMSROOT.TerminalOperatorModel(undefined, self.masterQuays(), self.masterCargoTypes()));
                $("#chk1").prop('checked', false);
                $("#berthiddiv").hide();
                $("#chk2").prop('checked', false);
                $("#divcargo").hide();
                $("#ResidentialAddressCountryMsg").text('');
                $("#PostalAddressCountryMsg").text('');
            }
            else {
                if (confirm('Are you want to reset the changes?')) {
                    self.terminaloperatorModel().reset();
                    //$('#spnTitle').html("Terminal Operator");

                    if (self.isSameAsBuisinessAddress() == true) {
                        $("#chkBuisinessAddress").attr('checked', true);
                    }

                    if (data.TerminalOperatorServiceTypesArray().length > 1) {
                        $("#chk1").prop('checked', true);
                        $("#berthiddiv").show();
                        $("#chk2").prop('checked', true);
                        $("#divcargo").show();
                    }
                    else {
                        $.each(data.TerminalOperatorServiceTypesArray(), function (key, val) {
                            if (val == "TOB") {
                                $("#chk1").prop('checked', true);
                                $("#berthiddiv").show();
                                $("#divcargo").hide();
                            }
                            else {
                                $("#chk2").prop('checked', true);
                                $("#divcargo").show();
                                $("#berthiddiv").hide();
                            }
                        });
                    }
                }
                else {
                    $(this).dialog("close");
                }
            }
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber, Type) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            if (Type == 'Telephone') {
                var validPhoneNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Telephone number.");
                    validPhoneNumber++;
                    return validPhoneNumber;
                }
                else {
                    return validPhoneNumber;
                }
            }

            if (Type == 'Fax') {
                var validFaxNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Fax number.");
                    validFaxNumber++;
                    return validFaxNumber;
                }
                else {
                    return validFaxNumber;
                }
            }
        }

        // UpdateTerminalOperator Used For Save the data from all controls into backend
        self.UpdateTerminalOperator = function (TerminalOperator) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.TerminalOperatorValidation = ko.observable(TerminalOperator);
            self.TerminalOperatorValidation().errors = ko.validation.group(self.TerminalOperatorValidation());
            var errors = self.TerminalOperatorValidation().errors().length;

            self.PostalValidation = ko.observable(TerminalOperator.PostalAddress);
            self.PostalValidation().errors = ko.validation.group(self.PostalValidation());
            var Postalerrors = self.PostalValidation().errors().length;

            self.BusinessValidation = ko.observable(TerminalOperator.BusinessAddress);
            self.BusinessValidation().errors = ko.validation.group(self.BusinessValidation());
            var Businesserrors = self.BusinessValidation().errors().length;

            var errorCount = 0;
            if (TerminalOperator.TerminalOperatorServiceTypesArray().length <= 0) {
                self.ischkmsg(true);
                errorCount = errorCount + 1;
                $("#ischkmsgischkmsg1").text('Please select at least one service type.');
            }

            if ($("#NumStreet").val() == null || $("#NumStreet").val() == '' || $("#NumStreet").val() == undefined) {
                $("#errNumStreetMsg").text(self.validationTextMessage());
                self.isNumStreetMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errNumStreetMsg").text('');
                self.isNumStreetMsg(false);
            }

            if ($("#Suburb").val() == null || $("#Suburb").val() == '' || $("#Suburb").val() == undefined) {
                $("#errSuburbMsg").text(self.validationTextMessage());
                self.isSuburbMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errSuburbMsg").text('');
                self.isSuburbMsg(false);
            }

            if ($("#TownCity").val() == null || $("#TownCity").val() == '' || $("#TownCity").val() == undefined) {
                $("#errTownCityMsg").text(self.validationTextMessage());
                self.isTownCityMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errTownCityMsg").text('');
                self.isTownCityMsg(false);
            }

            if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                $("#ResidentialAddressCountryMsg").text(self.validationTextMessage());
                self.isResidentialAddressCountry(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#ResidentialAddressCountryMsg").text('');
                self.isResidentialAddressCountry(false);
            }

            if ($("#PostalCode").val() == null || $("#PostalCode").val() == '' || $("#PostalCode").val() == undefined) {
                $("#errPostalCodeMsg").text(self.validationTextMessage());
                self.isPostalCodeMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errPostalCodeMsg").text('');
                self.isPostalCodeMsg(false);
            }

            if ($("#NumStreet2").val() == null || $("#NumStreet2").val() == '' || $("#NumStreet2").val() == undefined) {
                $("#errNumStreet2Msg").text(self.validationTextMessage());
                self.isNumStreet2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
            }

            if ($("#Suburb2").val() == null || $("#Suburb2").val() == '' || $("#Suburb2").val() == undefined) {
                $("#errSuburb2Msg").text(self.validationTextMessage());
                self.isSuburb2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
            }

            if ($("#TownCity2").val() == null || $("#TownCity2").val() == '' || $("#TownCity2").val() == undefined) {
                $("#errTownCity2Msg").text(self.validationTextMessage());
                self.isTownCity2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
            }

            if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                $("#PostalAddressCountryMsg").text(self.validationTextMessage());
                self.isPostalAddressCountry(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#PostalAddressCountryMsg").text('');
                self.isPostalAddressCountry(false);
            }

            if ($("#PostalCode2").val() == null || $("#PostalCode2").val() == '' || $("#PostalCode2").val() == undefined) {
                $("#errPostalCode2Msg").text(self.validationTextMessage());
                self.isPostalCode2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);
            }

            if ($("#RegistrationDate").val() == "" || $("#RegistrationDate").val() == null || $("#RegistrationDate").val() == undefined) {
                $("#RegistrationDateMsg").text(self.validationTextMessage());
                self.isRegistrationDateMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#RegistrationDateMsg").text('');
                self.isRegistrationDateMsg(false);
            }

            if ($("#ValidityDate").val() == "" || $("#ValidityDate").val() == null || $("#ValidityDate").val() == undefined) {
                $("#ValidityDateMsg").text(self.validationTextMessage());
                self.isValidityDateMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#ValidityDateMsg").text('');
                self.isValidityDateMsg(false);
            }

            var filterTelephoneNumber = TerminalOperator.TelephoneNo1();
            if (filterTelephoneNumber != null || filterTelephoneNumber != '' || filterTelephoneNumber != undefined) {
                var validTelephoneNumber = 0;

                if (filterTelephoneNumber.length != 0) {
                    validTelephoneNumber = CheckPhoneValidation(filterTelephoneNumber, 'Telephone');
                    if (validTelephoneNumber > 0) {
                        errorCount = errorCount + 1;
                    }
                }
            }

            var filterFaxNumber = TerminalOperator.FaxNo();
            if (filterFaxNumber != null || filterFaxNumber != '' || filterFaxNumber != undefined) {
                var validFaxNumber = 0;

                if (filterFaxNumber.length != 0) {
                    validFaxNumber = CheckPhoneValidation(filterFaxNumber, 'Fax');
                    if (validFaxNumber > 0) {
                        errorCount = errorCount + 1;
                    }
                }
            }

            if (TerminalOperator.TerminalOperatorServiceTypesArray().indexOf('TOCH') >= 0) {
                if (TerminalOperator.operationalCargotypes().length <= 0) {
                    errorCount = errorCount + 1;
                    toastr.warning("You have to select Cargo Types. Please check below.");
                }
            }

            if (TerminalOperator.TerminalOperatorServiceTypesArray().indexOf('TOB') >= 0) {
                if (TerminalOperator.operationalBerths().length <= 0) {
                    errorCount = errorCount + 1;
                    toastr.warning("You have to select Berths. Please check below.");
                }
            }

            if (errors == 0 && Postalerrors == 0 && Businesserrors == 0) {
                if (errorCount > 0) {
                    toastr.warning("Please fill all the required fields.");
                    return;
                }
                checkboxesAll = document.getElementsByName('Allport');
                if (checkboxesAll[0].checked == true) {
                    self.terminaloperatorModel().TerminalOperatorPortsArr([]);
                    checkboxes = document.getElementsByName('port');
                    for (var i = 0, n = checkboxes.length; i < n; i++) {
                        if (!checkboxes[i].checked) {
                            self.terminaloperatorModel().TerminalOperatorPortsArr.push(checkboxes[i].value);
                        }
                    }
                }

                self.viewModelHelper.apiPut('api/TerminalOperatorMaster', ko.mapping.toJSON(TerminalOperator), function Message(data) {
                    toastr.success("Terminal Operator details updated successfully.", "Terminal Operator");
                    self.LoadTerminalOparators();
                    self.viewMode('List');
                    $('#spnTitle').html("Terminal Operator");
                });
            }
            else {
                self.TerminalOperatorValidation().errors.showAllMessages();
                self.PostalValidation().errors.showAllMessages();
                self.BusinessValidation().errors.showAllMessages();
                toastr.warning("Please fill all the required fields.");
                return;
            }
        }

        // SaveTerminalOperator Used For Save the data from all controls into backend
        self.SaveTerminalOperator = function (TerminalOperator) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.TerminalOperatorValidation = ko.observable(TerminalOperator);
            self.TerminalOperatorValidation().errors = ko.validation.group(self.TerminalOperatorValidation());
            var errors = self.TerminalOperatorValidation().errors().length;

            self.PostalValidation = ko.observable(TerminalOperator.PostalAddress);
            self.PostalValidation().errors = ko.validation.group(self.PostalValidation());
            var Postalerrors = self.PostalValidation().errors().length;

            self.BusinessValidation = ko.observable(TerminalOperator.BusinessAddress);
            self.BusinessValidation().errors = ko.validation.group(self.BusinessValidation());
            var Businesserrors = self.BusinessValidation().errors().length;

            var errorCount = 0;
            //////Validation for the input fields where .extend is not working 

            if ($("#NumStreet").val() == null || $("#NumStreet").val() == '' || $("#NumStreet").val() == undefined) {
                $("#errNumStreetMsg").text(self.validationTextMessage());
                self.isNumStreetMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errNumStreetMsg").text('');
                self.isNumStreetMsg(false);
            }

            if ($("#Suburb").val() == null || $("#Suburb").val() == '' || $("#Suburb").val() == undefined) {
                $("#errSuburbMsg").text(self.validationTextMessage());
                self.isSuburbMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errSuburbMsg").text('');
                self.isSuburbMsg(false);
            }

            if ($("#TownCity").val() == null || $("#TownCity").val() == '' || $("#TownCity").val() == undefined) {
                $("#errTownCityMsg").text(self.validationTextMessage());
                self.isTownCityMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errTownCityMsg").text('');
                self.isTownCityMsg(false);
            }

            if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                $("#ResidentialAddressCountryMsg").text(self.validationTextMessage());
                self.isResidentialAddressCountry(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#ResidentialAddressCountryMsg").text('');
                self.isResidentialAddressCountry(false);
            }

            if ($("#PostalCode").val() == null || $("#PostalCode").val() == '' || $("#PostalCode").val() == undefined) {
                $("#errPostalCodeMsg").text(self.validationTextMessage());
                self.isPostalCodeMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errPostalCodeMsg").text('');
                self.isPostalCodeMsg(false);
            }

            if ($("#NumStreet2").val() == null || $("#NumStreet2").val() == '' || $("#NumStreet2").val() == undefined) {
                $("#errNumStreet2Msg").text(self.validationTextMessage());
                self.isNumStreet2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
            }

            if ($("#Suburb2").val() == null || $("#Suburb2").val() == '' || $("#Suburb2").val() == undefined) {
                $("#errSuburb2Msg").text(self.validationTextMessage());
                self.isSuburb2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
            }

            if ($("#TownCity2").val() == null || $("#TownCity2").val() == '' || $("#TownCity2").val() == undefined) {
                $("#errTownCity2Msg").text(self.validationTextMessage());
                self.isTownCity2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
            }

            if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                $("#PostalAddressCountryMsg").text(self.validationTextMessage());
                self.isPostalAddressCountry(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#PostalAddressCountryMsg").text('');
                self.isPostalAddressCountry(false);
            }

            if ($("#PostalCode2").val() == null || $("#PostalCode2").val() == '' || $("#PostalCode2").val() == undefined) {
                $("#errPostalCode2Msg").text(self.validationTextMessage());
                self.isPostalCode2Msg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);
            }

            if ($("#RegistrationDate").val() == "" || $("#RegistrationDate").val() == null || $("#RegistrationDate").val() == undefined) {
                $("#RegistrationDateMsg").text(self.validationTextMessage());
                self.isRegistrationDateMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#RegistrationDateMsg").text('');
                self.isRegistrationDateMsg(false);
            }

            if ($("#ValidityDate").val() == "" || $("#ValidityDate").val() == null || $("#ValidityDate").val() == undefined) {
                $("#ValidityDateMsg").text(self.validationTextMessage());
                self.isValidityDateMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#ValidityDateMsg").text('');
                self.isValidityDateMsg(false);
            }

            if (TerminalOperator.TerminalOperatorServiceTypesArray().length <= 0) {
                self.ischkmsg(true);
                errorCount = errorCount + 1;
                $("#ischkmsgischkmsg1").text('You should select at least one');
            }

            var filterTelephoneNumber = TerminalOperator.TelephoneNo1();
            if (filterTelephoneNumber != null || filterTelephoneNumber != '' || filterTelephoneNumber != undefined) {
                var validTelephoneNumber = 0;

                if (filterTelephoneNumber.length != 0) {
                    validTelephoneNumber = CheckPhoneValidation(filterTelephoneNumber, 'Telephone');
                    if (validTelephoneNumber > 0) {
                        errorCount = errorCount + 1;
                    }
                }
            }

            var filterFaxNumber = TerminalOperator.FaxNo();
            if (filterFaxNumber != null || filterFaxNumber != '' || filterFaxNumber != undefined) {
                var validFaxNumber = 0;

                if (filterFaxNumber.length != 0) {
                    validFaxNumber = CheckPhoneValidation(filterFaxNumber, 'Fax');
                    if (validFaxNumber > 0) {
                        errorCount = errorCount + 1;
                    }
                }
            }

            if (TerminalOperator.TerminalOperatorServiceTypesArray().indexOf('TOCH') >= 0) {
                if (TerminalOperator.operationalCargotypes().length <= 0) {
                    errorCount = errorCount + 1;
                    toastr.warning("You have to select Cargo Types. Please check below.");
                }
            }

            if (TerminalOperator.TerminalOperatorServiceTypesArray().indexOf('TOB') >= 0) {
                if (TerminalOperator.operationalBerths().length <= 0) {
                    errorCount = errorCount + 1;
                    toastr.warning("You have to select Berths. Please check below.");
                }
            }
            ///////////////////////////////////////////////////////////////////////////

            if (errors == 0 && Postalerrors == 0 && Businesserrors == 0) {
                if (errorCount > 0) {
                    toastr.warning("Please fill all the required fields.");
                    return;
                }
                checkboxesAll = document.getElementsByName('Allport');
                if (checkboxesAll[0].checked == true) {
                    self.terminaloperatorModel().TerminalOperatorPortsArr([]);
                    checkboxes = document.getElementsByName('port');
                    for (var i = 0, n = checkboxes.length; i < n; i++) {
                        if (!checkboxes[i].checked) {
                            self.terminaloperatorModel().TerminalOperatorPortsArr.push(checkboxes[i].value);
                        }
                    }
                }

                if ($('#allchks').is(':Unchecked')) {
                    self.terminaloperatorModel().TerminalOperatorPortsArr(self.PortLoopArray());
                }

                self.viewModelHelper.apiPost('api/TerminalOperatorMaster', ko.mapping.toJSON(TerminalOperator), function Message(data) {
                    toastr.success("Terminal Operator details saved successfully.", "Terminal Operator");
                    self.LoadTerminalOparators();
                    self.viewMode('List');
                    $('#spnTitle').html("Terminal Operator");
                });
            }
            else {
                self.TerminalOperatorValidation().errors.showAllMessages();
                self.PostalValidation().errors.showAllMessages();
                self.BusinessValidation().errors.showAllMessages();
                toastr.warning("Please fill all the required fields.");
                return;
            }
        }

        //CancelTerminalOperator method is Cancel the TerminalOperator screen data and navigate to List screen
        self.CancelTerminalOperator = function (data) {
            self.LoadTerminalOparators();
            self.viewMode("List");
            $('#spnTitle').html("Terminal Operator");
        }

        CheckingAll = function (ctrl) {
            self.terminaloperatorModel().TerminalOperatorPortsArr([]);
            checkboxes = document.getElementsByName('port');

            if (ctrl.checked) {
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    checkboxes[i].checked = ctrl.checked;
                    self.terminaloperatorModel().TerminalOperatorPortsArr.push(checkboxes[i].value);
                }
            }
            else {
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    checkboxes[i].checked = ctrl.checked;
                }
            }
        }

        // ValidEvent method is fires validate the data
        ValidEvent = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.terminaloperatorList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.RegistrationNumber.toLowerCase() == entry.RegistrationNumber.toLowerCase()) {
                    $('#spanRegNo').text('Already exists.! Please enter another registration number.');
                    $('#spanRegNo').css('display', '');
                }
                if (value.RegistrationNumber == entry.RegistrationNumber) {
                    $('#spanRegNo').text('Already exists.! Please enter another registration number.');
                    $('#spanRegNo').css('display', '');
                }
                return;
            });
        }

        HandleKeyUp = function (data, event) {
            if ($('#spanRegNo').is(':visible')) {
                $('#spanRegNo').css('display', 'none');
            }
            else {
                $('#spanRegNo').css('display', '');
            }
        }

        // Initialize method is fires in  page Initializetion mode
        self.Initialize();

        // Bhoji
        self.eventallport = function (event) {

            if ($('#allchks').is(':checked')) {
                self.LoadAllPorts();
            }
            else {
                self.PortTempArray.removeAll();
            }

            self.terminaloperatorModel().TerminalOperatorPortsNameArr.removeAll();
            self.masterQuays.removeAll();
            self.terminaloperatorModel().TerminalOperatorPortsNameArr(self.PortTempArray());
        }

        function toggleSingle(source) {

            checkboxes = document.getElementsByName('port');

            var chk = true;
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (!checkboxes[i].checked) {
                    chk = false;
                }
            }
            checkboxesAll = document.getElementsByName('Allport');
            checkboxesAll[0].checked = chk;
        }

        self.CategoryChange = function (event) {

            self.PortLoopArray.removeAll();
            self.masterQuays.removeAll();
            self.checkUserList = ko.observableArray();
            if (self.terminaloperatorModel().TerminalOperatorPortsNameArr.length > 0)
                self.terminaloperatorModel().TerminalOperatorPortsNameArr.removeAll();
            checkboxes = document.getElementsByName('port');
            var chk = true;
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (!checkboxes[i].checked) {
                    chk = false;
                }
                else {
                    var users1 = ko.utils.arrayFilter(self.PortReferenceData().PortRequesttypes(), function (item) {
                        if (item.PortCode() == checkboxes[i].defaultValue) {
                            self.checkUserList.push(item);
                            self.PortLoopArray.push(item.PortCode());
                            // self.terminaloperatorModel().TerminalOperatorPortsArr(item.PortCode);
                        }
                    });
                    if (self.terminaloperatorModel().TerminalOperatorPortsNameArr.length > 0)
                        self.terminaloperatorModel().TerminalOperatorPortsNameArr.push(self.checkUserList());
                    else
                        self.terminaloperatorModel().TerminalOperatorPortsNameArr(self.checkUserList());
                }
            }
            self.checkUserList.removeAll();
            checkboxesAll = document.getElementsByName('Allport');
            checkboxesAll[0].checked = chk;
            //self.TerminalOperatorPortsNameArr().subscribe();
        }

        self.eventberth = function (event) {

            if ($('#chk1').is(':checked')) {
                self.terminaloperatorModel().TerminalOperatorServiceTypesArray.subscribe(function () {
                    ko.utils.arrayForEach(self.terminaloperatorModel().TerminalOperatorServiceTypesArray(), function (ref) {
                        if (ref == "TOB") {
                            $("#ischkmsgischkmsg1").text('');
                            $("#berthiddiv").show();
                        }
                        if ($('#chk2').is(':checked')) {
                            $("#divcargo").show();
                        }
                    });
                });
            }
            else {
                $("#berthiddiv").hide();
            }
        }

        self.eventcargo = function (event) {

            if ($('#chk2').is(':checked')) {
                self.terminaloperatorModel().TerminalOperatorServiceTypesArray.subscribe(function () {
                    ko.utils.arrayForEach(self.terminaloperatorModel().TerminalOperatorServiceTypesArray(), function (ref) {
                        if (ref == "TOCH") {
                            $("#divcargo").show();
                            $("#ischkmsgischkmsg1").text('');
                        } else {
                            $("#ischkmsgischkmsg1").text('');
                            $("#divcargo").hide();
                        }
                        if ($('#chk1').is(':checked')) {
                            $("#berthiddiv").show();
                        }
                    });
                });
            }
            else {
                $("#divcargo").hide();
            }
        }

        //selectedChoices method is set the all details in BuisinessAddress put postal address fields the pilotexemption request screen 
        self.selectedChoices = function () {
            if ($('#chkBuisinessAddress').is(':checked')) {
                self.terminaloperatorModel().PostalAddress().NumberStreet((self.terminaloperatorModel().BusinessAddress().NumberStreet()));
                self.terminaloperatorModel().PostalAddress().Suburb((self.terminaloperatorModel().BusinessAddress().Suburb()));
                self.terminaloperatorModel().PostalAddress().TownCity((self.terminaloperatorModel().BusinessAddress().TownCity()));
                self.terminaloperatorModel().PostalAddress().CountryCode((self.terminaloperatorModel().BusinessAddress().CountryCode()));
                self.terminaloperatorModel().PostalAddress().PostalCode((self.terminaloperatorModel().BusinessAddress().PostalCode()));
                //////Validation for the input fields where .extend is not working 

                var errorCount = 0;

                if ($("#NumStreet").val() == null || $("#NumStreet").val() == '' || $("#NumStreet").val() == undefined) {
                    $("#errNumStreetMsg").text(self.validationTextMessage());
                    self.isNumStreetMsg(true);
                }
                else {
                    $("#errNumStreetMsg").text('');
                    self.isNumStreetMsg(false);
                }

                if ($("#Suburb").val() == null || $("#Suburb").val() == '' || $("#Suburb").val() == undefined) {
                    $("#errSuburbMsg").text(self.validationTextMessage());
                    self.isSuburbMsg(true);
                }
                else {
                    $("#errSuburbMsg").text('');
                    self.isSuburbMsg(false);
                }

                if ($("#TownCity").val() == null || $("#TownCity").val() == '' || $("#TownCity").val() == undefined) {
                    $("#errTownCityMsg").text(self.validationTextMessage());
                    self.isTownCityMsg(true);
                }
                else {
                    $("#errTownCityMsg").text('');
                    self.isTownCityMsg(false);
                }

                if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                    $("#ResidentialAddressCountryMsg").text(self.validationTextMessage());
                    self.isResidentialAddressCountry(true);
                }
                else {
                    $("#ResidentialAddressCountryMsg").text('');
                    self.isResidentialAddressCountry(false);
                }

                if ($("#PostalCode").val() == null || $("#PostalCode").val() == '' || $("#PostalCode").val() == undefined) {
                    $("#errPostalCodeMsg").text(self.validationTextMessage());
                    self.isPostalCodeMsg(true);
                }
                else {
                    $("#errPostalCodeMsg").text('');
                    self.isPostalCodeMsg(false);
                }

                if ($("#NumStreet2").val() == null || $("#NumStreet2").val() == '' || $("#NumStreet2").val() == undefined) {
                    $("#errNumStreet2Msg").text(self.validationTextMessage());
                    self.isNumStreet2Msg(true);
                }
                else {
                    $("#errNumStreet2Msg").text('');
                    self.isNumStreet2Msg(false);
                }

                if ($("#Suburb2").val() == null || $("#Suburb2").val() == '' || $("#Suburb2").val() == undefined) {
                    $("#errSuburb2Msg").text(self.validationTextMessage());
                    self.isSuburb2Msg(true);
                }
                else {
                    $("#errSuburb2Msg").text('');
                    self.isSuburb2Msg(false);
                }

                if ($("#TownCity2").val() == null || $("#TownCity2").val() == '' || $("#TownCity2").val() == undefined) {
                    $("#errTownCity2Msg").text(self.validationTextMessage());
                    self.isTownCity2Msg(true);
                }
                else {
                    $("#errTownCity2Msg").text('');
                    self.isTownCity2Msg(false);
                }

                if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                    $("#PostalAddressCountryMsg").text(self.validationTextMessage());
                    self.isPostalAddressCountry(true);
                }
                else {
                    $("#PostalAddressCountryMsg").text('');
                    self.isPostalAddressCountry(false);
                }

                if ($("#PostalCode2").val() == null || $("#PostalCode2").val() == '' || $("#PostalCode2").val() == undefined) {
                    $("#errPostalCode2Msg").text(self.validationTextMessage());
                    self.isPostalCode2Msg(true);
                }
                else {
                    $("#errPostalCode2Msg").text('');
                    self.isPostalCode2Msg(false);
                }
            }
            else {
                self.terminaloperatorModel().PostalAddress().NumberStreet('');
                self.terminaloperatorModel().PostalAddress().Suburb('');
                self.terminaloperatorModel().PostalAddress().TownCity('');
                self.terminaloperatorModel().PostalAddress().CountryCode('');
                self.terminaloperatorModel().PostalAddress().PostalCode('');
                $("#errNumStreet2Msg").text(self.validationTextMessage());
                self.isNumStreet2Msg(true);
                $("#errSuburb2Msg").text(self.validationTextMessage());
                self.isSuburb2Msg(true);
                $("#errTownCity2Msg").text(self.validationTextMessage());
                self.isTownCity2Msg(true);
                $("#PostalAddressCountryMsg").text(self.validationTextMessage());
                self.isPostalAddressCountry(true);
                $("#errPostalCode2Msg").text(self.validationTextMessage());
                self.isPostalCode2Msg(true);
                $("#ValidityDateMsg").text('');
                self.isValidityDateMsg(false);
                $("#RegistrationDateMsg").text('');
                self.isValidityDateMsg(false);
            }
            return true;
        };

        //AddessKeyupEvents method is set the all details in BuisinessAddress put postal address fields  on key up event the pilotexemption request screen 
        self.AddessKeyupEvents = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                $('#NumStreet').keyup(function () { $('#NumStreet2').val($(this).val()); });
                $('#Suburb').keyup(function () { $('#Suburb2').val($(this).val()); });
                $('#TownCity').keyup(function () { $('#TownCity2').val($(this).val()); });
                $('#ResidentialAddressCountry').keyup(function () { $('#PostalAddressCountry').val($(this).val()); });
                $('#PostalCode').keyup(function () { $('#PostalCode2').val($(this).val()); });
            }
            else {
                $('#NumStreet').keyup();
                $('#Suburb').keyup();
                $('#TownCity').keyup();
                $('#ResidentialAddressCountry').keyup();
                $('#PostalCode').keyup();
                return false;
            }
        };

        PreventBackSpace = function () {
            return self.validationHelperDate.PreventBackspaces_keydownEvent(event);
        }

        NumStreetChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errNumStreetMsg").text(self.validationTextMessage());
                self.isNumStreetMsg(true);
            }
            else {
                $("#errNumStreetMsg").text('');
                self.isNumStreetMsg(false);
            }
        }

        SuburbChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errSuburbMsg").text(self.validationTextMessage());
                self.isSuburbMsg(true);
            }
            else {
                $("#errSuburbMsg").text('');
                self.isSuburbMsg(false);
            }
        }

        TownCityChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errTownCityMsg").text(self.validationTextMessage());
                self.isTownCityMsg(true);
            }
            else {
                $("#errTownCityMsg").text('');
                self.isTownCityMsg(false);
            }
        }

        ResidentialAddressCountryChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#ResidentialAddressCountryMsg").text(self.validationTextMessage());
                self.isResidentialAddressCountry(true);
            }
            else {
                $("#ResidentialAddressCountryMsg").text('');
                self.isResidentialAddressCountry(false);
                if ($("#PostalAddressCountry").val().trim() != $("#ResidentialAddressCountry").val().trim()) {
                    $('#chkBuisinessAddress').attr('checked', false);
                }
            }
        }

        PostalCodeChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errPostalCodeMsg").text(self.validationTextMessage());
                self.isPostalCodeMsg(true);
            }
            else {
                $("#errPostalCodeMsg").text('');
                self.isPostalCodeMsg(false);
            }
        }

        NumStreet2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errNumStreet2Msg").text(self.validationTextMessage());
                self.isNumStreet2Msg(true);
            }
            else {
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
            }
        }

        Suburb2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errSuburb2Msg").text(self.validationTextMessage());
                self.isSuburb2Msg(true);
            }
            else {
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
            }
        }

        TownCity2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errTownCity2Msg").text(self.validationTextMessage());
                self.isTownCity2Msg(true);
            }
            else {
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
            }
        }

        PostalAddressCountryChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#PostalAddressCountryMsg").text(self.validationTextMessage());
                self.isPostalAddressCountry(true);
            }
            else {
                $("#PostalAddressCountryMsg").text('');
                self.isPostalAddressCountry(false);
                if ($("#PostalAddressCountry").val().trim() != $("#ResidentialAddressCountry").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        PostalCode2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errPostalCode2Msg").text(self.validationTextMessage());
                self.isPostalCode2Msg(true);
            }
            else {
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);
            }
        }

        // AlphaNumeric with Spaces
        self.ValidateAlphaNumericWithSpaces = function (data, event) {
            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[A-Za-z\d\s\b]*$/;
            return charcheck.test(keychar);
        }

        //To validate alphabet with spaces
        self.ValidateAlphabetsWithSpaces = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z\b ]*$/;
            return charcheck.test(keychar);
        }
        
        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }

        ///////////////////////////////////////////////////////////////////////////
    }

    IPMSRoot.TerminalOperatorViewModel = TerminalOperatorViewModel;

}(window.IPMSROOT));

$('input').bind("paste", function (e) {
    e.preventDefault();
});

ChangeEvent = function (data, event) {
    if (data.RegisteredName().length == 0 || data.TradingName().length == 0 || data.RegistrationDate().length == 0
        || data.PremiseLocation().length == 0 || data.TelephoneNo1().length == 0 || data.FaxNo().length == 0 || data.NumStreet().length == 0
        || data.Suburb().length == 0 || data.TownCity().length == 0 || data.CountryCode().length == 0 || data.PostalCode().length == 0 || data.NumStreet2().length == 0
        || data.Suburb2().length == 0 || data.TownCity2().length == 0 || data.PostalCode2().length == 0) {
        self.IsClicks(false);
    }
    else {
        self.IsClicks(true);
    }
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

