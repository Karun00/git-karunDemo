(function (IPMSRoot) {

    var ShiftViewModel = function () {

        var self = this;
        $('#spnTitle').html("Shift");

        var flag = true;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.Shiftlist = ko.observableArray();
        self.getActiveShiftlist = ko.observableArray();
        self.getRollOverList = ko.observableArray();
        self.ShiftModel = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);
        self.IsShiftOff = ko.observable();
        self.IsEnableStartTime = ko.observable(false);
        self.isContinuousShift = ko.observable(false);
        self.isShiftOff = ko.observable(false);
        self.isContinuousShiftEnabled = ko.observable(true);

        // This is used to Initialize method
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.ShiftModel(new IPMSROOT.ShiftModel());
            self.LoadShift();
            self.LoadActiveShiftList();
            self.viewMode('List');
        }

        // This is used to Add New Shift
        self.addShift = function () {
            self.ShiftModel(new IPMSRoot.ShiftModel(undefined));
            self.ShiftModel().validationEnabled(false);

            $('#spnTitle').html("Add Shift");
            self.viewMode('Form');
            self.IsSave(true);
            self.IsReset(true);
            self.IsUpdate(false);
            self.IsCodeEnable(true);
            self.IsActivityEnable(false);
            self.isContinuousShift(false);
            self.isShiftOff(true);
            self.isContinuousShiftEnabled(true);
        }

        // This is used to View Shift
        self.viewShift = function (data) {

            if (data.IsContinuousShift()) {
                data.StartTime("");
                data.EndTime("");
            }
            else {
                data.FirstShiftID("");
                data.SecondShiftID("");
                data.RollOverOn("");


            }

            self.ShiftModel(data);
            $('#spnTitle').html("View Shift");
            self.viewMode('Form');
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            self.IsActivityEnable(false);
            self.isContinuousShift(false);
            self.isShiftOff(false);
            self.isContinuousShiftEnabled(false);
            $("#startTimepicker").data('kendoTimePicker').enable(false);
            $("#endTimepicker").data('kendoTimePicker').enable(false);
        }

        // This is used to Edit Shift
        self.editShift = function (data) {
            var strt = new Date("November 13, 2013 " + data.StartTime());
            data.StartTime(strt);
            var endt = new Date("November 13, 2013 " + data.EndTime());
            data.EndTime(endt);

            self.ShiftModel(data);

            $('#spnTitle').html("Update Shift");
            self.viewMode('Form');
            self.IsSave(false);
            self.IsReset(true);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsActivityEnable(true);
            self.isContinuousShiftEnabled(false);
            //$('#ShiftName').prop('disabled', true);

            if (data.IsContinuousShift() == false) {

                if (data.IsShiftOff() == false) {
                    $("#startTimepicker").removeAttr("disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(true);
                    $("#endTimepicker").removeAttr("disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(true);

                    data.IsShiftOff(false);
                    data.IsContinuousShift(false);
                    self.isShiftOff(true);
                    self.isContinuousShift(false);
                }
                else {
                    $("#startTimepicker").attr("disabled", "disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(false);
                    $("#endTimepicker").attr("disabled", "disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(false);

                    data.StartTime('00:00');
                    data.EndTime('23:59');

                    data.IsShiftOff(true);
                    data.IsContinuousShift(false);
                    self.isShiftOff(false);
                    self.isContinuousShift(false);
                }
            }
            else {
                $("#startTimepicker").attr("disabled", "disabled");
                $("#startTimepicker").data('kendoTimePicker').enable(false);
                $("#endTimepicker").attr("disabled", "disabled");
                $("#endTimepicker").data('kendoTimePicker').enable(false);
                data.StartTime('');
                data.EndTime('');
                self.isShiftOff(false);

                if (data.IsShiftOff() == false) {
                    data.IsShiftOff(false);
                    data.IsContinuousShift(true);
                    self.isContinuousShift(true);

                }
                else {
                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.RollOverOn("");


                    data.IsShiftOff(true);
                    data.IsContinuousShift(false);
                    self.isContinuousShift(false);
                }
            }
        }

        // This is used to Reset Shift
        self.ResetShift = function (data) {

            ko.validation.reset();
            self.ShiftModel().reset();
            $("#divValidationError").text('');
            $("#divValidationError").removeClass("alert alert-danger");

            if (!($('#IsContinuousShift').is(':checked'))) {

                if (!($('#IsShiftOffid').is(':checked'))) {
                    $("#startTimepicker").removeAttr("disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(true);
                    $("#endTimepicker").removeAttr("disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(true);

                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.IsShiftOff(false);
                    data.IsContinuousShift(false);
                    self.isShiftOff(true);
                    self.isContinuousShift(false);
                }
                else {
                    $("#startTimepicker").attr("disabled", "disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(false);
                    $("#endTimepicker").attr("disabled", "disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(false);

                    data.StartTime('00:00');
                    data.EndTime('23:59');
                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.IsShiftOff(true);
                    data.IsContinuousShift(false);
                    self.isShiftOff(false);
                    self.isContinuousShift(false);
                }
            }
            else {
                $("#startTimepicker").attr("disabled", "disabled");
                $("#startTimepicker").data('kendoTimePicker').enable(false);
                $("#endTimepicker").attr("disabled", "disabled");
                $("#endTimepicker").data('kendoTimePicker').enable(false);
                data.StartTime('');
                data.EndTime('');
                self.isShiftOff(false);

                if (!($('#IsShiftOffid').is(':checked'))) {
                    data.IsShiftOff(false);
                    data.IsContinuousShift(true);
                    self.isContinuousShift(true);
                    $("#RollOverOn").attr("disabled", false);
                }
                else {
                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.IsShiftOff(true);
                    data.IsContinuousShift(true);
                    self.isContinuousShift(false);
                }
            }

            $("#valstartTimepicker").text("");
            $("#valendTimepicker").text("");
            $("#endTime").text("");
            $("#spanFirstShiftID").text("");
            $("#spanSecondShiftID").text("");
            self.ShiftValidation().errors.showAllMessages(false);
        }

        // This is used to Cancel Shift
        self.CancelShift = function () {
            self.ShiftModel().reset();
            self.viewMode("List");
            $('#spnTitle').html("Shift");
        }

        // ValidEvent  method is fires validate the data
        ValidEvent = function (data, event) {

            var items = JSON.parse(ko.toJSON(self.Shiftlist));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.ShiftName.toLowerCase() === entry.ShiftName.toLowerCase()) {
                    if (entry.ShiftID === "") {
                        data.ShiftName("");
                        $('#spanShiftName').text('This shift name already exists.');
                        $('#spanShiftName').css('display', '');
                        $('#ShiftName').val("");
                    }
                    else if (parseInt(entry.ShiftID) != parseInt(value.ShiftID)) {
                        data.ShiftName("");
                        $('#spanShiftName').text('This shift name already exists.');
                        $('#spanShiftName').css('display', '');
                        $('#ShiftName').val("");
                    }
                }
            });
        }

        HandleKeyUp = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.Shiftlist));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.ShiftName == entry.ShiftName) {
                    $('#spanShiftName').css('display', '');
                }
                else {
                    $('#spanShiftName').css('display', 'none');
                }
            });
        }

        // This is used to Save Shift
        self.SaveShift = function (Shift) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            Shift.validationEnabled(true);

            self.ShiftValidation = ko.observable(Shift);
            self.ShiftValidation().errors = ko.validation.group(self.ShiftValidation());
            var errors = self.ShiftValidation().errors().length;

            if (!Shift.IsContinuousShift()) {

                if ($("#startTimepicker").val() == "") {
                    $("#valstartTimepicker").text("* This field is required.");
                    errors++;
                }
                else {
                    $("#valstartTimepicker").text("");
                }

                if ($("#endTimepicker").val() == "") {
                    $("#valendTimepicker").text("* This field is required.");
                    errors++;
                }
                else {
                    $("#valendTimepicker").text("");
                }
            }
            else {
                if (!Shift.IsShiftOff()) {
                    if (Shift.FirstShiftID() == "" || Shift.FirstShiftID() == undefined) {
                        $("#spanFirstShiftID").text("This field is required.");
                        errors++;
                    }
                    else {
                        $("#spanFirstShiftID").text('');
                    }

                    if (Shift.SecondShiftID() == "" || Shift.SecondShiftID() == undefined) {
                        $("#spanSecondShiftID").text("This field is required.");
                        errors++;
                    }
                    else {
                        $("#spanSecondShiftID").text('');
                    }
                }
            }

            if (errors == 0) {
                if (Shift.IsShiftOff() == true) {
                    Shift.IsShiftOff("Y");
                }
                else {
                    Shift.IsShiftOff("N");
                }

                if (Shift.IsContinuousShift() == true) {
                    Shift.IsContinuousShift("Y");
                }
                else {
                    Shift.IsContinuousShift("N");
                }

                if (flag == true) {
                    self.viewModelHelper.apiPost('api/PostShift', ko.mapping.toJSON(Shift), function Message(data) {

                        toastr.success("Shift details saved successfully.", "Shift");
                        $('#spnTitle').html("Shift");
                        self.LoadShift();
                        self.LoadActiveShiftList();
                        self.viewMode('List');

                    });
                }
                else {
                    toastr.error("Start Time and End Time are not valid.");
                    return;
                }
            }

            else {
                $("#divValidationError").text('You have some form errors. Please check below.');
                $("#divValidationError").addClass("alert alert-danger");
                self.ShiftValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // This is used to Update Shift
        self.UpdateShift = function (Shift) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            Shift.validationEnabled(true);
            self.ShiftValidation = ko.observable(Shift);
            self.ShiftValidation().errors = ko.validation.group(self.ShiftValidation());
            var errors = self.ShiftValidation().errors().length;

            if (!Shift.IsContinuousShift()) {

                if ($("#startTimepicker").val() == "") {
                    $("#valstartTimepicker").text("* Please select the Start Time.");
                    errors++;
                }
                else {
                    $("#valstartTimepicker").text("");
                }

                if ($("#endTimepicker").val() == "") {
                    $("#valendTimepicker").text("* Please select the End Time.");
                    errors++;
                }
                else {
                    $("#valendTimepicker").text("");
                }
            }
            else {
                if (!Shift.IsShiftOff()) {

                    if (Shift.FirstShiftID() == "" || Shift.FirstShiftID() == undefined) {
                        $("#spanFirstShiftID").text("Please select First Shift.");
                        errors++;
                    }
                    else {
                        $("#spanFirstShiftID").text('');
                    }

                    if (Shift.SecondShiftID() == "" || Shift.SecondShiftID() == undefined) {
                        $("#spanSecondShiftID").text("Please select Second Shift.");
                        errors++;
                    }
                    else {
                        $("#spanSecondShiftID").text('');
                    }
                }
            }

            if (errors == 0) {

                if (Shift.IsShiftOff() == true) {
                    Shift.IsShiftOff("Y");
                }
                else {
                    Shift.IsShiftOff("N");
                }

                if (Shift.IsContinuousShift() == true) {
                    Shift.IsContinuousShift("Y");
                }
                else {
                    Shift.IsContinuousShift("N");
                }

                self.viewModelHelper.apiPut('api/PutShift', ko.mapping.toJSON(Shift), function Message(data) {
                    toastr.success("Shift details updated successfully.", "Shift");
                    $('#spnTitle').html("Shift");
                    self.LoadShift();
                    self.viewMode('List');

                });
            }
            else {
                self.ShiftValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // This is used to Get All Sift Records
        self.LoadShift = function () {

            self.viewModelHelper.apiGet('api/ShiftDetails', null, function (result) {

                self.Shiftlist(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.ShiftModel(item);
                }));
            }, null, null, false);
        }

        // This is used to Validate start time and end time.
        ValidEndTime = function (data, event) {

            var today = new Date();
            var dd = Number(today.getDate());
            var mm = Number(today.getMonth() + 1);
            var yyyy = Number(today.getFullYear());

            var SDate = self.ShiftModel().StartTime();
            var EDate = this.value();
            if ($('#startTimepicker').val() != "" && $("#startTimepicker").val() != "") {
                var Sdat = Number(SDate.getDate());
                var Edat = Number(EDate.getDate());
                var Shours = Number(SDate.getHours());
                var Ehours = Number(EDate.getHours());
                var Sminutes = Number(SDate.getMinutes());
                var Eminutes = Number(EDate.getMinutes());

                if (Edat <= Sdat) {
                    if (Ehours <= Shours) {

                        if (Ehours == Shours) {
                            if (Eminutes <= Sminutes) {
                                $('#endTime').text("End time should not be less than start time.");
                                $('#endTimepicker').val("");
                                return;
                            }
                            else {
                                $('#endTime').css('display', 'none');
                                $('#endTime').text('');
                            }
                        }
                        else {
                            $('#endTime').text("End time should not be less than start time.");
                            $('#endTimepicker').val("");
                            return;
                        }
                    }
                }
                else {
                    $('#startTime').text("End date should not be less than start date.");
                }
            }
            else {
                toastr.error("please select start time first.");
            }
            $('#endTime').text('');
        }

        // This is used to Validate end time.
        ValidStartTime = function (data, event) {

            var today = new Date();
            var dd = Number(today.getDate());
            var mm = Number(today.getMonth() + 1);
            var yyyy = Number(today.getFullYear());

            var EDate = self.ShiftModel().EndTime();
            var SDate = this.value();
            if ($('#endTimepicker').val() != "" && $("#startTimepicker").val() != "") {
                var Sdat = Number(SDate.getDate());
                var Edat = Number(EDate.getDate());
                var Shours = Number(SDate.getHours());
                var Ehours = Number(EDate.getHours());
                var Sminutes = Number(SDate.getMinutes());
                var Eminutes = Number(EDate.getMinutes());

                if (Edat <= Sdat) {
                    if (Ehours <= Shours) {

                        if (Ehours == Shours) {
                            if (Eminutes <= Sminutes) {
                                $('#startTime').text("End time should not be less than start time.");
                                $('#startTimepicker').val("");
                                return;
                            }
                            else {
                                $('#startTime').css('display', 'none');
                                $('#startTime').text('');
                            }
                        }
                        else {
                            $('#startTime').text("End time should not be less than start time.");
                            $('#startTimepicker').val("");
                            return;
                        }
                    }
                }
                else {
                    $('#startTime').text("End date should not be less than start date.");
                }
            }
            $('#startTime').text('');
        }

        // This is used to Get All Active Shits
        self.LoadActiveShiftList = function () {
            self.getActiveShiftlist.removeAll();
            ko.utils.arrayMap(self.Shiftlist(), function (item) {
                if (item.IsShiftOff() == false && item.IsContinuousShift() == false) {
                    self.getActiveShiftlist.push(item);
                }
            });
            // item2 = [{ name: 'Select..', val: '' }, { name: 'First Shift', val: '1' }, { name: 'Second Shift', val: '2' }];
            self.getRollOverList.removeAll();
            item2 = { name: 'First Shift', val: '1' };
            item3 = { name: 'Second Shift', val: '2' };
            self.getRollOverList.push(item2);
            self.getRollOverList.push(item3);
        }

        ChangeIsShiftOffid = function (data) {
            if (!($('#IsContinuousShift').is(':checked'))) {

                if (!($('#IsShiftOffid').is(':checked'))) {
                    $("#startTimepicker").removeAttr("disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(true);
                    $("#endTimepicker").removeAttr("disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(true);

                    data.StartTime('');
                    data.EndTime('');
                    data.FirstShiftID('');
                    data.SecondShiftID('');
                    data.RollOverOn('');


                    data.IsShiftOff(false);
                    data.IsContinuousShift(false);
                    self.isShiftOff(true);
                    self.isContinuousShift(false);
                }
                else {
                    $("#startTimepicker").attr("disabled", "disabled");
                    $("#startTimepicker").data('kendoTimePicker').enable(false);
                    $("#endTimepicker").attr("disabled", "disabled");
                    $("#endTimepicker").data('kendoTimePicker').enable(false);

                    data.StartTime('00:00');
                    data.EndTime('23:59');
                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.RollOverOn('');
                    data.IsShiftOff(true);
                    data.IsContinuousShift(false);
                    self.isShiftOff(false);
                    self.isContinuousShift(false);
                }
            }
            else {
                $("#startTimepicker").attr("disabled", "disabled");
                $("#startTimepicker").data('kendoTimePicker').enable(false);
                $("#endTimepicker").attr("disabled", "disabled");
                $("#endTimepicker").data('kendoTimePicker').enable(false);
                data.StartTime('');
                data.EndTime('');
                self.isShiftOff(false);

                if (!($('#IsShiftOffid').is(':checked'))) {

                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.RollOverOn('');
                    data.IsShiftOff(false);
                    data.IsContinuousShift(true);
                    self.isContinuousShift(true);
                }
                else {

                    data.FirstShiftID("");
                    data.SecondShiftID("");
                    data.RollOverOn('');
                    data.IsShiftOff(true);
                    data.IsContinuousShift(true);
                    self.isContinuousShift(false);
                }
            }

            $("#valstartTimepicker").text("");
            $("#valendTimepicker").text("");
            $("#endTime").text("");
            $("#spanFirstShiftID").text("");
            $("#spanSecondShiftID").text("");
        }

        ChangeIsContinuousShift = function (data) {

            data.StartTime('');
            data.EndTime('');

            if (!($('#IsContinuousShift').is(':checked'))) {
                $("#startTimepicker").removeAttr("disabled");
                $("#startTimepicker").data('kendoTimePicker').enable(true);
                $("#endTimepicker").removeAttr("disabled");
                $("#endTimepicker").data('kendoTimePicker').enable(true);

                data.FirstShiftID("");
                data.SecondShiftID("");
                data.RollOverOn('');
                self.isContinuousShift(false);
                data.IsShiftOff(false);
                data.IsContinuousShift(false);
                self.isShiftOff(true);
            }
            else {
                $("#startTimepicker").attr("disabled", "disabled");
                $("#startTimepicker").data('kendoTimePicker').enable(false);
                $("#endTimepicker").attr("disabled", "disabled");
                $("#endTimepicker").data('kendoTimePicker').enable(false);
                data.FirstShiftID("");
                data.SecondShiftID("");
                data.RollOverOn('');
                data.IsShiftOff(false);
                data.IsContinuousShift(true);
                self.isShiftOff(false);
                self.isContinuousShift(true);
            }

            $("#valstartTimepicker").text("");
            $("#valendTimepicker").text("");
            $("#endTime").text("");
            $("#spanFirstShiftID").text("");
            $("#spanSecondShiftID").text("");
        }

        ChangeFirstShift = function (data) {
            $("#spanFirstShiftID").text("");

            if (!data.IsShiftOff()) {
                if ((data.FirstShiftID() == undefined || data.FirstShiftID() == "")) {
                    $("#spanFirstShiftID").text("Please select first shift");
                }
                else if ((data.FirstShiftID() != undefined || data.FirstShiftID() != "") && (data.SecondShiftID() != undefined || data.SecondShiftID() != "")) {
                    if (data.FirstShiftID() == data.SecondShiftID()) {
                        data.FirstShiftID("");
                        $("#spanFirstShiftID").text("Shift already selected, select another shift.");
                    }
                }
            }
        }

        ChangeSecondShift = function (data) {
            $("#spanSecondShiftID").text("");
            if (!data.IsShiftOff()) {
                if ((data.FirstShiftID() == undefined || data.FirstShiftID() == "")) {
                    $("#spanFirstShiftID").text("Please select first shift");
                }
                else if (data.SecondShiftID() == undefined || data.SecondShiftID() == "") {
                    $("#spanSecondShiftID").text("Please select second shift.");
                }
                else if ((data.FirstShiftID() != undefined || data.FirstShiftID() != "") && (data.SecondShiftID() != undefined || data.SecondShiftID() != "")) {
                    if (data.FirstShiftID() == data.SecondShiftID()) {
                        data.SecondShiftID("");
                        $("#spanSecondShiftID").text("Shift already selected, select another shift.");
                    }
                }
            }
        }

        self.Initialize();
    }

    IPMSRoot.ShiftViewModel = ShiftViewModel;
}(window.IPMSROOT));