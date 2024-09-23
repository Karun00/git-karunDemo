(function (IPMSRoot) {

    var ExternalDivingRegisterViewModel = function () {

        var self = this;
        $('#spnTitile').html("External Diving Register");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.ExternalDivingRegisterModel = ko.observable();
        self.ExternalDivingRegisterList = ko.observableArray();
        self.AllExternalDivingRegisterList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.editableView = ko.observable();
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.isStartTimeMsg = ko.observable(false);
        self.isEndTtmeMsg = ko.observable(false);
        self.IsModified = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();        
        self.divingReferenceData = ko.observableArray();
        self.VesselList = ko.observableArray();
        self.CompanyList = ko.observableArray();
        self.OffsiteSupervisorContNo = ko.observable();
        self.OnsiteSupervisorContNo = ko.observable();

        // Intilaize the model view
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.ExternalDivingRegisterModel(new IPMSROOT.ExternalDivingRegisterModel());
            self.LoadGrid();            
            self.LoadReferenceData();
            self.LoadVessels();
            self.LoadCompanies();
            self.viewMode('List');
            var errors1 = 0;
        }

        // Loads the Vessels
        self.LoadVessels = function () {
            self.viewModelHelper.apiGet('api/GetAllVessels', null, function (result) {
                ko.mapping.fromJS(result, {}, self.VesselList);
            });
        }

        //Loads the Company 
        self.LoadCompanies = function () {
            self.viewModelHelper.apiGet('api/GetAllCompanies', null, function (result) {
                ko.mapping.fromJS(result, {}, self.CompanyList);
            });
        }        

        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/DivingReferenceData', null, function (refdata) {
                self.divingReferenceData(new IPMSRoot.ReferenceData(refdata));
            }, null, null, false);
        }



        // Grid / list
        self.LoadGrid = function () {
            self.viewModelHelper.apiGet('api/ExternalDivingRegisters', null, function (result) {
                self.ExternalDivingRegisterList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.ExternalDivingRegisterModel(item);
                }));
            });
        }

        // Saves the External Diving Register Details
        self.SaveExternalDivingRegister = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($("#StartTime").val() == "" || $("#StartTime").val() == null) {
                $("#isStartTimeMsg").text('Please select Start Time.');
                self.isStartTimeMsg(true);
            }





            var errors1 = 0;
            model.validationEnabled(true);
            self.ExternalDivingRegisterValidation = ko.observable(model);
            self.ExternalDivingRegisterValidation().errors = ko.validation.group(self.ExternalDivingRegisterValidation());
            var errors = self.ExternalDivingRegisterValidation().errors().length;

            if (model.StartTime() == "Invalid date")
                model.StartTime("");
            if (model.StopTime() == "Invalid date")
                model.StopTime("");

            if (errors == 0 && errors1 == 0) {
                var filterPhoneNumber1 = model.OnsiteSupervisorContNo();
                filterPhoneNumber1 = filterPhoneNumber1.replace(/(\)|\()|_|-+/g, '');
                model.OnsiteSupervisorContNo(filterPhoneNumber1);
                var filterPhoneNumber2 = model.OffsiteSupervisorContNo();
                filterPhoneNumber2 = filterPhoneNumber2.replace(/(\)|\()|_|-+/g, '');
                model.OffsiteSupervisorContNo(filterPhoneNumber2);

                var validPhoneNumber = 0;
                if (filterPhoneNumber2.length != 13) {
                    toastr.warning("Invalid offsite supervisor contact no.");
                    validPhoneNumber++;
                }
                else if (filterPhoneNumber2.length == 13) {
                    var validNo1 = parseInt(filterPhoneNumber2);
                    if (validNo1 == 0) {
                        toastr.warning("Invalid offsite supervisor contact no.");
                        validPhoneNumber++;
                    }

                }


                if (filterPhoneNumber1.length != 13) {
                    toastr.warning("Invalid onsite supervisor contact no.");
                    validPhoneNumber++;
                }
                else if (filterPhoneNumber1.length == 13) {
                    var validNo = parseInt(filterPhoneNumber1);
                    if (validNo == 0) {
                        toastr.warning("Invalid onsite supervisor contact no.");
                        validPhoneNumber++;
                    }

                }
                if (validPhoneNumber != 0) {
                    $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
                    model.OnsiteSupervisorContNo(OnsiteSupervisorContNomaskedtextbox.value());
                    $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
                    model.OffsiteSupervisorContNo(OffsiteSupervisorContNomaskedtextbox.value());

                    return;
                }

            }

            if (errors == 0 && errors1 == 0) {


                self.viewModelHelper.apiPost('api/ExternalDivingRegisters', ko.mapping.toJSON(model), function Message(data) {
                    toastr.success("External diving register details saved successfully.", "External Diving Register");
                    self.LoadGrid();
                    self.viewMode('List');
                });
                self.cancel();
            }
            else {
                self.ExternalDivingRegisterValidation().errors.showAllMessages();
                toastr.warning("* Please fill mandatory fields.");
                return;
            }
        }

        //  Updates the external diving register details
        self.ModifyExternalDivingRegister = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";


            if ($("#StartTime").val() == "" || $("#StartTime").val() == null) {
                $("#isStartTimeMsg").text('Please select Start Time.');
                self.isStartTimeMsg(true);
            }

            var filterPhoneNumber1 = model.OnsiteSupervisorContNo();






            var errors1 = 0;
            model.validationEnabled(true);
            self.ExternalDivingRegisterValidation = ko.observable(model);
            self.ExternalDivingRegisterValidation().errors = ko.validation.group(self.ExternalDivingRegisterValidation());
            var errors = self.ExternalDivingRegisterValidation().errors().length;
            if (model.StartTime() == "Invalid date")
                model.StartTime("");
            if (model.StopTime() == "Invalid date")
                model.StopTime("");

            if (errors == 0 && errors1 == 0) {

                filterPhoneNumber1 = filterPhoneNumber1.replace(/(\)|\()|_|-+/g, '');


                model.OnsiteSupervisorContNo(filterPhoneNumber1);


                var filterPhoneNumber2 = model.OffsiteSupervisorContNo();
                filterPhoneNumber2 = filterPhoneNumber2.replace(/(\)|\()|_|-+/g, '');


                model.OffsiteSupervisorContNo(filterPhoneNumber2);


                var validPhoneNumber = 0;
                if (filterPhoneNumber1.length != 13) {
                    toastr.warning("Invalid onsite supervisor contact no.");
                    validPhoneNumber++;
                }
                else if (filterPhoneNumber1.length == 13) {
                    var validNo = parseInt(filterPhoneNumber1);
                    if (validNo == 0) {
                        toastr.warning("Invalid onsite supervisor contact no.");
                        validPhoneNumber++;
                    }

                }
                if (filterPhoneNumber2.length != 13) {
                    toastr.warning("Invalid offsite supervisor contact no.");
                    validPhoneNumber++;
                }
                else if (filterPhoneNumber2.length == 13) {
                    var validNo1 = parseInt(filterPhoneNumber2);
                    if (validNo1 == 0) {
                        toastr.warning("Invalid offsite supervisor contact no.");
                        validPhoneNumber++;
                    }

                }


                if (validPhoneNumber != 0) {
                    $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
                    model.OnsiteSupervisorContNo(OnsiteSupervisorContNomaskedtextbox.value());
                    $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
                    model.OffsiteSupervisorContNo(OffsiteSupervisorContNomaskedtextbox.value());

                    return;
                }

            }
            if (errors == 0 && errors1 == 0) {
                self.viewModelHelper.apiPut('api/ExternalDivingRegisters', ko.mapping.toJSON(model), function Message(data) {
                    toastr.success("External diving register details updated successfully.", "External Diving Register");
                    self.LoadGrid();
                    self.viewMode('List');
                });

                self.cancel();
            }
            else {
                self.ExternalDivingRegisterValidation().errors.showAllMessages();
                toastr.warning("* Please fill all mandatory fields.");
                $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
                model.OnsiteSupervisorContNo(OnsiteSupervisorContNomaskedtextbox.value());
                $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
                model.OffsiteSupervisorContNo(OffsiteSupervisorContNomaskedtextbox.value());

                return;
            }
        }

        // Deletes the external diving register
        self.DeleteExternalDivingRegister = function (model) {
            // confirmation box - start
            $.confirm({
                'title': 'External Diving Register',
                'message': "Are you sure you want To delete external diving register(" + model.SubCatCode() + ")",
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.viewModelHelper.apiPut('api/ExternalDivingRegister/PostDeleteExternalDivingRegisterData/' + ko.mapping.toJSON(model.SubCatCode), null, function (result) {
                                self.ExternalDivingRegisterList.remove(model);
                            });
                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {

                        }
                    }
                }
            });
            //confirmation box - end
        }

        // Add new mode
        self.addExternalDivingRegister = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.editableView(true);
            self.ExternalDivingRegisterModel(new IPMSRoot.ExternalDivingRegisterModel());


            $('#spnTitile').html("Add External Diving Register");
            $("#DivingLogDateTime").data('kendoDateTimePicker').enable(false);
            $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OnsiteSupervisorContNo = OnsiteSupervisorContNomaskedtextbox.value();

            $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OffsiteSupervisorContNo = OffsiteSupervisorContNomaskedtextbox.value();
        }

        // View mode
        self.viewExternalDivingRegister = function (ExternalDivingRegister) {


            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.editableView(false);

            self.ExternalDivingRegisterModel(ExternalDivingRegister);
            $('#spnTitile').html("View External Diving Register");
            $("#DivingLogDateTime").data('kendoDateTimePicker').enable(false);
            $("#StartTime").data('kendoDateTimePicker').enable(false);
            $("#StopTime").data('kendoDateTimePicker').enable(false);

            $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OnsiteSupervisorContNo = OnsiteSupervisorContNomaskedtextbox.value();

            $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OffsiteSupervisorContNo = OffsiteSupervisorContNomaskedtextbox.value();
        }

        // Update / Edit Mode
        self.editExternalDivingRegister = function (ExternalDivingRegister) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);

            self.ExternalDivingRegisterModel(ExternalDivingRegister);
            CalcStartTime1();

            $('#spnTitile').html("Update External Diving Register");

            $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OnsiteSupervisorContNo = OnsiteSupervisorContNomaskedtextbox.value();

            $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OffsiteSupervisorContNomaskedtextbox = $("#OffsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OffsiteSupervisorContNo = OffsiteSupervisorContNomaskedtextbox.value();
            $("#DivingLogDateTime").data('kendoDateTimePicker').enable(false);
        }

        // Reset button
        self.ResetExternalDivingRegister = function (model) {


            ko.validation.reset();
            self.ExternalDivingRegisterModel().reset();
            self.ExternalDivingRegisterValidation = ko.observable(model);
            self.ExternalDivingRegisterValidation().errors = ko.validation.group(self.ExternalDivingRegisterValidation());
            self.ExternalDivingRegisterValidation().errors.showAllMessages(false);

            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }

            $("#OnsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OnsiteSupervisorContNomaskedtextbox = $("#OnsiteSupervisorContNo").data("kendoMaskedTextBox");
            self.OnsiteSupervisorContNo = OnsiteSupervisorContNomaskedtextbox.value();

            $("#OffsiteSupervisorContNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            $("#DivingLogDateTime").data('kendoDateTimePicker').enable(false);
            $("#isStartTimeMsg").text('');
            self.isStartTimeMsg(false);
        }

        // Cancel Button
        self.cancel = function () {
            self.ExternalDivingRegisterModel().reset();
            self.LoadGrid();
            self.viewMode('List');
            $('#spnTitile').html("External Diving Register");
        }

        // Calender / DAte time picker sets upto today
        calOpen = function () {
            this.max(new Date());
        };

        //Start Time validations when change, clears the stop time
        CalcStartTime = function () {
            $("#isStartTimeMsg").text('');
            self.isStartTimeMsg(false);
            var StartDateValue = $("#StartTime").val();
            var EndDateValue = $("#StopTime").val();

            if (EndDateValue != "") {
                $("#StopTime").val("");
                self.ExternalDivingRegisterModel().StopTime("");
            }
            $("#StopTime").data('kendoDateTimePicker').min(StartDateValue);
        }

        CalcStartTime1 = function () {
            var StartDateValue = $("#StartTime").val();
            $("#StopTime").data('kendoDateTimePicker').min(StartDateValue);
        }

        //Calculate Start Period Of Occupation
        CalcStartPeriodofOccupation = function () {
            var endDate = new Date();
            $("#OcupationToDate").data('kendoDateTimePicker').min(endDate);

            $("#OcupationToDate")
            $("#isOcupationFromDateMsg").text('');
            self.isOcupationFromDateMsg(false);
            var StartDateValue = $("#OcupationFromDate").val();
            var EndDateValue = $("#OcupationToDate").val();
            $("#OcupationToDate").attr("value", "");
            $("#OcupationToDate").data('kendoDateTimePicker').min(StartDateValue);
            self.divingRequestModel().OcupationToDate('');
            self.divingRequestModel().HoursOfOccupation1('');
        }

        //End Time Validation, Start time is there or not, greater than start time or not
        CalcEndTime = function (data, event) {
            var errors1 = 0;
            if ($("#StartTime").val() == "" || $("#StartTime").val() == null) {
                self.ExternalDivingRegisterModel().StopTime('');
                $("#StopTime").val('');
                $("#StartTime").focus();
                $("#isStartTimeMsg").text('Please select Start Time.');
                self.isStartTimeMsg(true);
                return;
            }
        }

        //Added By Omprakash Kotha on 2nd September 2014
        // Reason : Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }
    IPMSRoot.ExternalDivingRegisterViewModel = ExternalDivingRegisterViewModel;

}(window.IPMSROOT));
