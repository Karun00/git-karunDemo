(function (IPMSRoot) {
    var isView = 0;
    var CraftMasterViewModel = function () {
        var self = this;
        $('#spnTitle').html("Crafts");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.craftModel = ko.observable(new IPMSROOT.CraftModel());
        self.viewMode = ko.observable();
        self.craftmasterList = ko.observableArray([]);
        self.craftmasterModel = ko.observable();
        self.craftReferenceData = ko.observable();
        self.viewMode = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.checkedValue = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);
        self.PhoneNumber = ko.observable();

        // Initialize method is fires in  pageload Initializetion mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.craftmasterModel(new IPMSROOT.CraftModel());
            self.LoadCraftMaster();
            self.LoaddropdownsData();
            self.viewMode('List');
            $('#spnTitle').html("Crafts");

           
        }

        // this is for grid binding
        // LoaddropdownsData Used For fetch the data for all controls from backend
        self.LoadCraftMaster = function () {
            self.viewModelHelper.apiGet('api/Crafts', null,
                        function (result) {
                            self.craftmasterList(ko.utils.arrayMap(result, function (item) {
                                return new IPMSRoot.CraftModel(item);
                                //self.craftmasterModel(new IPMSROOT.CraftModel(item));
                            }, null, null, false));
                        });
        }

        // LoaddropdownsData Used For fetch the data for allDropdown from backend
        self.LoaddropdownsData = function () {
            self.viewModelHelper.apiGet('api/Craftreferencedata', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData(). And do it only once. not every time.
                        self.craftReferenceData(new IPMSRoot.CraftReferenceData(result1));
                    }, null, null, false);
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();

        // ResetCraft method is fires when press the Reset button and set the all fields data is in stage that in page load time
        self.ResetCraft = function (model) {
            self.craftmasterModel().reset();
            self.craftmasterModel(model);
            ko.validation.group(self.craftmasterModel()).showAllMessages(false);
        }

        // CancelCraft method is fires when press the cancel button and set the all fields data is clear  move to list
        self.CancelCraft = function () {
            self.viewMode('List');
            self.craftmasterModel().reset();
            $('#spnTitle').html("Crafts");
        }

        // addCraftMaster method is fires when Addnew button press in Add mode
        self.addCraftMaster = function () {
            self.craftmasterModel(new IPMSROOT.CraftModel(undefined));
            $('#spnTitle').html("Add Craft");
            self.viewMode('Form');
            self.IsSave(true);
            self.IsReset(true);
            self.IsUpdate(false);
            self.IsCodeEnable(true);
            self.IsActivityEnable(false);
            ko.validation.group(self.craftmasterModel()).showAllMessages(false);
            //Masking Of Personal MobileNo.
            $("#PhoneNumber").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
            self.PhoneNumber = ContactNomaskedtextbox.value();
        }

        // editCraftMaster method is fires when Edit button press in Edit mode
        self.editCraftMaster = function (data) {
            self.craftmasterModel(data);

            //For Set Color to Dredger Type
            var flag = false;
            if (data.CraftType() == 'CRTD') {
                flag = true;
            }

            if (flag == true) {
                $('#DredgerColor').show();
            }
            else {
                $('#DredgerColor').hide();
            }

            self.viewMode("Form");
            $('#spnTitle').html("Update Craft");
            self.IsReset(true);
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsActivityEnable(true);
            self.IsCodeEnable(true);
            
            $("#PhoneNumber").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
            data.PhoneNumber(ContactNomaskedtextbox.value());
        }

        // viewCraftMaster method is fires when View button press in View mode
        self.viewCraftMaster = function (data) {
            self.craftmasterModel(data);

            //For Set Color to Dredger Type
            var flag = false;
            if (data.CraftType() == 'CRTD') {
                flag = true;
            }

            if (flag == true) {
                $('#DredgerColor').show();
            }
            else {
                $('#DredgerColor').hide();
            }

            self.viewMode("Form");
            $('#spnTitle').html("View Craft");
            self.IsReset(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsActivityEnable(false);
            self.IsCodeEnable(false);
            $('#chkBuisinessAddress').prop("disabled", true);
            $("#CraftBuildDate").data('kendoDatePicker').enable(false);
            $("#CommissionDate").data('kendoDatePicker').enable(false);
            $("#DateOfDelivery").data('kendoDatePicker').enable(false);
        };

        // ValidEvent method for check duplicate of CraftCode
        ValidEventCraftCode = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.craftmasterList));
            var entry = JSON.parse(ko.toJSON(data));
            var check = 0;

            $.each(items, function (index, value) {
                if (value.CraftCode.toLowerCase() === entry.CraftCode.toLowerCase()) {
                    check = check + 1;
                }
                return;
            });

            if (check > 0) {
                $('#spanCraftcode').text('Already exists.! Please enter another Code.');
                $('#spanCraftcode').css('display', '');
            }
            else {
                $('#spanCraftcode').text('');
            }
        }

        HandleKeyUpCraftCode = function (data, event) {
            if ($('#spanCraftcode').is(':visible')) {
                $('#spanCraftcode').css('display', 'none');
            }
            else {
                $('#spanCraftcode').css('display', '');
            }
        }

        // ValidEvent method for check duplicate of CraftName
        ValidEventCraftName = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.craftmasterList));
            var entry = JSON.parse(ko.toJSON(data));
            var check = 0;

            $.each(items, function (index, value) {
                if (value.CraftName.toLowerCase() === entry.CraftName.toLowerCase()) {
                    check = check + 1;
                }
                return;
            });

            if (check > 0) {
                $('#spanCraftName').text('Already exists.! Please enter another Name.');
                $('#spanCraftName').css('display', '');
            }
            else {
                $('#spanCraftName').text('');
            }
        }

        HandleKeyUpCraftName = function (data, event) {
            if ($('#spanCraftName').is(':visible')) {
                $('#spanCraftName').css('display', 'none');
            }
            else {
                $('#spanCraftName').css('display', '');
            }
        }

        // ValidEventIMO method is fires when already enter IMO number is in data then it validate 
        self.ValidEventIMO = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.craftmasterList));
            var entry = JSON.parse(ko.toJSON(data));
            var check = 0;

            $.each(items, function (index, value) {
                if (value.IMONo.toLowerCase() === entry.IMONo.toLowerCase()) {
                    check = check + 1;
                }
                return;
            });

            if (check > 0) {
                $('#spanIMONo').text('Already exists ! enter other IMO No');
                $('#spanIMONo').css('display', '');
            }
            else {
                $('#spanIMONo').text('');
            }
        }

        // HandleKeyUpIMO method is fires  entering IMO number is in data  
        HandleKeyUpIMO = function (data, event) {
            var keyCode = event.charCode || event.keyCode;
            if (keyCode === 8 || keyCode === 46) {
                event.returnValue = true;
            }
            else {
                var keyChar = String.fromCharCode(keyCode);
                return /[a-zA-Z0-9]/.test(keyChar);
            }

            var items = JSON.parse(ko.toJSON(self.craftmasterList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.IMONo == entry.IMONo) {
                    $('#spanIMONo').css('display', '');
                }
                else {
                    $('#spanIMONo').css('display', 'none');
                }
            });
        }

        //For Set Color to Dredger Type
        DredgerColorEvent = function (data) {
            var flag = false;
            if (data.value == 'CRTD') {
                flag = true;
                $('#spanDredgerColorCode').text('This is field is required.');
                $('#spanDredgerColorCode').css('display', '');
            }
            else {
                $('#spanDredgerColorCode').text('');
            }

            if (flag == true) {
                $('#DredgerColor').show();
            }
            else {
                $('#DredgerColor').hide();
            }

            var colorcode = $("#chosen-color").val();
            if (colorcode.length > 0) {
                $('#spanDredgerColorCode').text('');
            }
        }

        //To Set Colorcode to Model Variable
        ColorCodeChange = function () {
            //onchange="javascript:document.getElementById('chosen-color').value = document.getElementById('background-color').value;"
            $("#chosen-color").val($("#background-color").val());
            var colorcode = $("#chosen-color").val();
            if (colorcode.length > 0) {
                $('#spanDredgerColorCode').text('');
            }
        }

        // SaveCraftMaster Used For Save the data from all controls into backend
        self.SaveCraftMaster = function (CraftModel) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
           
            var filterPhoneNumber = CraftModel.PhoneNumber();
            filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');

            CraftModel.PhoneNumber(filterPhoneNumber);

            self.craftValidation = ko.observable(CraftModel);
            self.craftValidation().errors = ko.validation.group(self.craftValidation());
            var errors = self.craftValidation().errors().length;

            //To set Color code for Craft Type -> Dredger
            var colorcode = $("#chosen-color").val();
            CraftModel.DredgerColorCode = colorcode;

            if (CraftModel.CraftType() == 'CRTD' && (CraftModel.DredgerColorCode == null || CraftModel.DredgerColorCode == '' || CraftModel.DredgerColorCode == undefined)) {
                errors = 1;
                $('#spanDredgerColorCode').text('This is field is required.');
                $('#spanDredgerColorCode').css('display', '');
            }
            else {
                $('#spanDredgerColorCode').text('');
            }

            if (errors == 0) {
                var validPhoneNumber = 0;
                
                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid phone number");
                        validPhoneNumber++;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid phone number");
                            validPhoneNumber++;
                        }

                    }
                }
                if (validPhoneNumber != 0) {
                    $("#PhoneNumber").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
                    CraftModel.PhoneNumber(ContactNomaskedtextbox.value());
                    return;
                }
            }
            if (errors == 0) {

                $('#spanDredgerColorCode').text('');
                //***************************
                var dateobj1 = kendo.parseDate(CraftModel.CraftBuildDate(), "yyyy-MM-dd HH:mm:ss");
                var datestring1 = kendo.toString(dateobj1, "yyyy-MM-dd HH:mm:ss");
                CraftModel.CraftBuildDate(datestring1);

                var dateobj3 = kendo.parseDate(CraftModel.DateOfDelivery(), "yyyy-MM-dd HH:mm:ss");
                var datestring3 = kendo.toString(dateobj3, "yyyy-MM-dd HH:mm:ss");
                CraftModel.DateOfDelivery(datestring3);

                var dateobj2 = kendo.parseDate(CraftModel.CommissionDate(), "yyyy-MM-dd HH:mm:ss");
                var datestring2 = kendo.toString(dateobj2, "yyyy-MM-dd HH:mm:ss");
                CraftModel.CommissionDate(datestring2);

                //**************
                self.viewModelHelper.apiPost('api/Crafts', ko.mapping.toJSON(CraftModel), function Message(data) {
                    toastr.success("Craft details saved successfully.", "Craft");
                    self.LoadCraftMaster();
                    $('#spnTitle').html("Crafts");
                    self.viewMode('List');
                });
            }
            else {

                self.craftValidation().errors.showAllMessages();
                toastr.warning("Please fill the mandatory fields.", "Craft");
                $("#PhoneNumber").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
                CraftModel.PhoneNumber(ContactNomaskedtextbox.value());
                return;
            }
        }

        // UpdateCraftMaster Used For Update the data from all controls into backend
        self.UpdateCraftMaster = function (CraftModel) {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

           
            var filterPhoneNumber = CraftModel.PhoneNumber();

            
            filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
            CraftModel.PhoneNumber(filterPhoneNumber);
            


            //To set Color code for Craft Type -> Dredger
            var colorcode = $("#chosen-color").val();
            CraftModel.DredgerColorCode = colorcode;

            self.craftValidation = ko.observable(CraftModel);
            self.craftValidation().errors = ko.validation.group(self.craftValidation());
            var errors = self.craftValidation().errors().length;

            //To set Color code for Craft Type -> Dredger
            var colorcode = $("#chosen-color").val();
            CraftModel.DredgerColorCode = colorcode;

            if (CraftModel.CraftType() == 'CRTD' && (CraftModel.DredgerColorCode == null || CraftModel.DredgerColorCode == '' || CraftModel.DredgerColorCode == undefined)) {
                errors = 1;
                $('#spanDredgerColorCode').text('This is field is required.');
                $('#spanDredgerColorCode').css('display', '');
            }
            else {
                $('#spanDredgerColorCode').text('');
            }

            if (errors == 0) {
                var validPhoneNumber = 0;
                
                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid phone number");
                        validPhoneNumber++;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid phone number");
                            validPhoneNumber++;
                        }
                    }
                }
                if (validPhoneNumber != 0) {
                    $("#PhoneNumber").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
                    CraftModel.PhoneNumber(ContactNomaskedtextbox.value());
                    return;
                }
            }

            if (errors == 0) {
                $('#spanDredgerColorCode').text('');
                var dateobj1 = kendo.parseDate(CraftModel.CraftBuildDate(), "yyyy-MM-dd HH:mm:ss");
                var datestring1 = kendo.toString(dateobj1, "yyyy-MM-dd HH:mm:ss");
                CraftModel.CraftBuildDate(datestring1);

                var dateobj3 = kendo.parseDate(CraftModel.DateOfDelivery(), "yyyy-MM-dd HH:mm:ss");
                var datestring3 = kendo.toString(dateobj3, "yyyy-MM-dd HH:mm:ss");
                CraftModel.DateOfDelivery(datestring3);

                var dateobj2 = kendo.parseDate(CraftModel.CommissionDate(), "yyyy-MM-dd HH:mm:ss");
                var datestring2 = kendo.toString(dateobj2, "yyyy-MM-dd HH:mm:ss");
                CraftModel.CommissionDate(datestring2);

                self.viewModelHelper.apiPut('api/Crafts', ko.mapping.toJSON(CraftModel), function Message(data) {
                    toastr.success("Craft details updated successfully.", "Craft");
                    self.LoadCraftMaster();
                    $('#spnTitle').html("Crafts");
                    self.viewMode('List');
                });
            }
            else {
                self.craftValidation().errors.showAllMessages();
                $("#PhoneNumber").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var ContactNomaskedtextbox = $("#PhoneNumber").data("kendoMaskedTextBox");
                CraftModel.PhoneNumber(ContactNomaskedtextbox.value());
                toastr.warning("Please fill the mandatory fields.", "Craft");
                return;
            }
        }
    }
    IPMSRoot.CraftMasterViewModel = CraftMasterViewModel;

}(window.IPMSROOT));