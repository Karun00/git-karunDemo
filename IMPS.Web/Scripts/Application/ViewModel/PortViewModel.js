(function (IPMSRoot) {

    var PortViewModel = function () {

        var self = this;
        $('#spnTitile').html("Port");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.portModel = ko.observable();
        self.portList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);

        self.LoadPorts = function () {
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null,
              function (result) {
                  self.portList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.PortModel(item);
                  }));
              });
        }

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.portModel(new IPMSROOT.PortModel());
            self.LoadPorts();
            self.viewMode('List');
            self.IsCodeEnable(true);
        }

        self.SavePort = function (model) {
            model.validationEnabled(true);
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($('#PortCode').val() == '') {
                $('#spanportcode').text('* This field is required.');
            }

            if ($('#PortName').val() == '') {
                $('#spanportname').text('* This field is required.');
            }

            self.PortValidation = ko.observable(model);
            self.PortValidation().errors = ko.validation.group(self.PortValidation());
            var errors = self.PortValidation().errors().length;

            var filterContanctNumber1 = model.ContactNo();
            filterContanctNumber1 = filterContanctNumber1.replace(/[-@()]/g, "");
            if (filterContanctNumber1 != null || filterContanctNumber1 != '' || filterContanctNumber1 != undefined) {
                var validPhoneNumber1 = 0;

                if (filterContanctNumber1.length != 0) {
                    validPhoneNumber1 = CheckPhoneValidation(filterContanctNumber1, "ContactNo");
                    if (validPhoneNumber1 > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterFax = model.Fax();
            filterFax = filterFax.replace(/[-@()]/g, "");
            if (filterFax != null || filterFax != '' || filterFax != undefined) {
                var validPhoneNumber2 = 0;

                if (filterFax.length != 0) {
                    validPhoneNumber2 = CheckPhoneValidation(filterFax, "Fax");
                    if (validPhoneNumber2 > 0) {
                        errors = errors + 1;
                    }
                }
            }

            if (errors == 0) {

                model.ContactNo(filterContanctNumber1);
                model.Fax(filterFax);

                self.viewModelHelper.apiPost('api/Ports/PostPortData', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.success("Port details saved successfully.", "Port");
                        self.LoadPorts();
                        self.viewMode('List');
                        $('#spnTitile').html("Port");
                    });
            }
            else {
                self.PortValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifyPort = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.IsUpdate(true);
            self.IsSave(false);
            self.PortValidation = ko.observable(model);
            self.PortValidation().errors = ko.validation.group(self.PortValidation());

            var errors = ko.validation.group(model, { deep: true });
            self.viewModelHelper.modelIsValid(model.isValid());

            var filterContanctNumber1 = model.ContactNo();
            filterContanctNumber1 = filterContanctNumber1.replace(/[-@()]/g, "");
            if (filterContanctNumber1 != null || filterContanctNumber1 != '' || filterContanctNumber1 != undefined) {
                var validPhoneNumber1 = 0;

                if (filterContanctNumber1.length != 0) {
                    validPhoneNumber1 = CheckPhoneValidation(filterContanctNumber1, "ContactNo");
                    if (validPhoneNumber1 > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterFax = model.Fax();
            filterFax = filterFax.replace(/[-@()]/g, "");
            if (filterFax != null || filterFax != '' || filterFax != undefined) {
                var validPhoneNumber2 = 0;

                if (filterFax.length != 0) {
                    validPhoneNumber2 = CheckPhoneValidation(filterFax, "Fax");
                    if (validPhoneNumber2 > 0) {
                        errors = errors + 1;
                    }
                }
            }

            if (errors.length == 0) {

                model.ContactNo(filterContanctNumber1);
                model.Fax(filterFax);

                self.viewModelHelper.apiPost('api/Ports/ModifyPortData', ko.mapping.toJSON(model), function Message(data) {
                    if (data != null) {
                        $('#divValidationSuccess').removeClass('display-none');
                        toastr.success("Port details updated successfully.", "Port");
                        self.LoadPorts();
                        self.viewMode('List');
                        $('#spnTitile').html("Port");
                        //self.portModel().set(data);
                    }
                },
                function (error) {
                    toastr.error(error.statusText + ' - ' + error.responseText);
                });
            }
            else {
                self.PortValidation().errors.showAllMessages();
                return;
            }
        }

        self.DeletePort = function (model) {
            self.viewModelHelper.apiDelete('api/Ports/PutDeletePort', ko.mapping.toJS(model), alert("success"));
        }

        self.viewport = function (port) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.portModel(port);
            $('#spnTitile').html("View Port");

            //Masking Of ContactNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNo = $("#ContactNo").data("kendoMaskedTextBox");
            self.portModel().ContactNo(ContactNo.value());

            //Masking Of Fax.
            $("#Fax").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var Fax = $("#Fax").data("kendoMaskedTextBox");
            self.portModel().Fax(Fax.value());
        }

        self.editport = function (port) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.IsReset(true);
            self.portModel(port);
            $('#spnTitile').html("Update Port");
            self.IsCodeEnable(false);

            //Masking Of ContactNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNo = $("#ContactNo").data("kendoMaskedTextBox");
            self.portModel().ContactNo(ContactNo.value());

            //Masking Of Fax.
            $("#Fax").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var Fax = $("#Fax").data("kendoMaskedTextBox");
            self.portModel().Fax(Fax.value());
        }

        self.addPort = function () {
            $('#spnTitile').html("Add Port");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.portModel(new IPMSRoot.PortModel());

            //Masking Of ContactNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNo = $("#ContactNo").data("kendoMaskedTextBox");
            self.portModel().ContactNo(ContactNo.value());

            //Masking Of Fax.
            $("#Fax").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var Fax = $("#Fax").data("kendoMaskedTextBox");
            self.portModel().Fax(Fax.value());
        }

        self.ResetPort = function (model) {
            model.validationEnabled(false);
            $('#spanportcode').text('');
            self.portModel().reset();
            $("#ContactNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Fax").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        self.cancel = function () {
            self.viewMode('List');
            self.portModel().reset();
            $("#ContactNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Fax").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $('#spnTitile').html("Port");
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber, Type) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            if (Type == 'ContactNo') {
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

        ValidPortName = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.portList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.PortName.toLowerCase() == entry.PortName.toLowerCase()) {

                    $('#spanportname').text('Already exists.! Please enter another name.');
                    $('#spanportname').css('display', '');
                    $('#PortName').val('');
                    return false;
                }
                else {
                    $('#spanportname').css('display', 'none');
                    return true;
                }
            });
        }

        ValidEvent = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.portList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.PortCode.toLowerCase() == entry.PortCode.toLowerCase()) {
                    $('#spanportcode').text('Already exists.! Please enter another code.');
                    $('#spanportcode').css('display', '');
                    $('#PortCode').val('');
                    return false;
                }
                else {
                    $('#spanportcode').css('display', 'none');
                    return true;
                }
            });
        }

        self.Initialize();
    }
    IPMSRoot.PortViewModel = PortViewModel;

}(window.IPMSROOT));

//To validate numeric of the text boxes.
function Validatealphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[a-zA-Z0-9\b ]/;
    return charcheck.test(keychar);
}

//To get validate alphabet with spaces
function ValidateAlphabets(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z\b ]*$/;
    return charcheck.test(keychar);
}