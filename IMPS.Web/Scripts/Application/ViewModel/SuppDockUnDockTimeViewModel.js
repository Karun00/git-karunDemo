(function (IPMSRoot) {

    var SuppDockUnDockTimeViewModel = function () {

        var self = this;
        $('#spnTitile').html("Docking Undocking Times Capturing List");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.suppDockUnDockTimeModel = ko.observable();
        self.SuppDockUnDockTimeList = ko.observableArray();
        self.AllSuppDockUnDockTimeList = ko.observableArray();
        self.validationHelper = new IPMSROOT.validationHelper();

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.editableView = ko.observable();
        self.IsPrintPemit = ko.observable(false);
        self.UserName = ko.observable();
        self.Designation = ko.observable();
        self.IsDocking = ko.observable(true);
        self.IsUnDocking = ko.observable(true);
        self.IsSaveUpdateDisabled = ko.observable(false);
        // self.DivingLogDateTime = ko.observable();
        // self.startDate = ko.observable();

        var validationErrorMessage = "* This field is required.";

        // Intilaize the model view
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.suppDockUnDockTimeModel(new IPMSROOT.SuppDockUnDockTimeModel());
            self.LoadGrid();
            self.UserDetailsByID();
            self.viewMode('List');
            self.suppDockUnDockTimeModel().startDate(new Date());
            self.suppDockUnDockTimeModel().endDate(new Date());
        }

        // Grid / list
        self.LoadGrid = function () {

            self.viewModelHelper.apiGet('api/SuppDockUnDockTimes',
        null,
        function (result) {
            self.SuppDockUnDockTimeList(ko.utils.arrayMap(result, function (item) {
                return new IPMSRoot.SuppDockUnDockTimeModel(item);
            }));
        });
            //prompt("Result", ko.toJSON(self.SuppDockUnDockTimeList));
        }
        // User Details
        self.UserDetailsByID = function () {

            self.viewModelHelper.apiGet('api/UserDetailsByID',
        null,
        function (result) {
            self.UserName(result.FirstName + " " + result.LastName);
            self.Designation(result.Designation);
        });
        }

        //  Updates the Docking Undocking Times Capturing details
        self.ModifySuppDockUnDockTime = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            // prompt("Result");
            model.validationEnabled(true);
            self.SuppDockUnDockTimeValidation = ko.observable(model);
            self.SuppDockUnDockTimeValidation().errors = ko.validation.group(self.SuppDockUnDockTimeValidation());
            var errors = self.SuppDockUnDockTimeValidation().errors().length;
            var validate = true;

            var EDockDate = new Date(Date.parse(model.EnteredDockDateTime()));
            var OnDockDate = new Date(Date.parse(model.OnBlocksDateTime()));
            var DryDockDate = new Date(Date.parse(model.DryDockDateTime()));

            var FDockDate = new Date(Date.parse(model.FinishedDockDateTime()));
            var OffDockDate = new Date(Date.parse(model.OffBlocksDateTime()));
            var LDockDate = new Date(Date.parse(model.LeftDockDateTime()));

            if (model.EnteredDockDateTime() == "" || model.EnteredDockDateTime() == "Invalid date") {
                $("#spanEnteredDockDateTime").text(validationErrorMessage);
                errors = 1;
            }
            if (model.OnBlocksDateTime() == "" || model.OnBlocksDateTime() == "Invalid date") {
                $("#spanOnBlocksDateTime").text(validationErrorMessage);
                errors = 1;
            }
            if (model.DryDockDateTime() == "" || model.DryDockDateTime() == "Invalid date") {
                $("#spanDryDockDateTime").text(validationErrorMessage);
                errors = 1;
            }

            if (self.IsUnDocking() == true) {

                if (model.FinishedDockDateTime() == "" || model.FinishedDockDateTime() == "Invalid date") {
                    $("#spanFinishedDockDateTime").text(validationErrorMessage);
                    errors = 1;
                }
                if (model.OffBlocksDateTime() == "" || model.OffBlocksDateTime() == "Invalid date") {
                    $("#spanOffBlocksDateTime").text(validationErrorMessage);
                    errors = 1;
                }
                if (model.LeftDockDateTime() == "" || model.LeftDockDateTime() == "Invalid date") {
                    $("#spanLeftDockDateTime").text(validationErrorMessage);
                    errors = 1;
                }
            }

            if (errors == 0) {
                if (EDockDate > OnDockDate) {
                    toastr.warning("On Blocks should be greater than vessel entered.");
                    validate = false;
                }
                else if (OnDockDate > DryDockDate) {
                    toastr.warning("Dry Dock at should be greater than On Blocks.");
                    validate = false;
                }

                else if (DryDockDate > FDockDate) {
                    toastr.warning("Finished with dock should be greater than Dry Dock at.");
                    validate = false;
                }
                else if (FDockDate > OffDockDate) {
                    toastr.warning("Off Blocks should be greater than Finished with dock at.");
                    validate = false;
                }
                else if (OffDockDate > LDockDate) {
                    toastr.warning("Vessel left dock at should be greater than Off Blocks.");
                    validate = false;
                }

            }

            if (model.FinishedDockDateTime() == "Invalid date") {
                model.FinishedDockDateTime(null);
            }
            if (model.OffBlocksDateTime() == "Invalid date") {
                model.OffBlocksDateTime(null);
            }
            if (model.LeftDockDateTime() == "Invalid date") {
                model.LeftDockDateTime(null);
            }

            if (errors == 0) {
                if (validate == true) {
                    self.viewModelHelper.apiPut('api/SuppDockUnDockTimes', ko.mapping.toJSON(model), function Message(data) {
                        toastr.success("Docking Undocking Times Capturing Details Updated Successfully.", "Docking/UnDocking Times Capturing");
                        $('#spnTitile').html("Docking Undocking Times Capturing List");
                        self.LoadGrid();
                        self.viewMode('List');

                    });
                }

                //self.cancel();

            }
            else {
                self.SuppDockUnDockTimeValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // Deletes the Docking Undocking Times Capturing
        self.DeleteSuppDockUnDockTime = function (model) {

            // confirmation box - start
            $.confirm({
                'title': 'Delete Docking Undocking Times Capturing',
                'message': "Are you sure you want to delete Docking Undocking Times Capturing(" + model.SubCatCode() + ")",
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.viewModelHelper.apiPut('api/SuppDockUnDockTime/PostDeleteSuppDockUnDockTimeData/' + ko.mapping.toJSON(model.SubCatCode), null, function (result) {
                                self.SuppDockUnDockTimeList.remove(model);
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
        self.addSuppDockUnDockTime = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.suppDockUnDockTimeModel(new IPMSRoot.SuppDockUnDockTimeModel());
            $('#spnTitile').html("Add Docking Undocking Times Capturing");

        }

        // View mode
        self.viewSuppDockUnDockTime = function (SuppDockUnDockTime) {

            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.editableView(false);
            self.IsDocking(false);
            self.IsUnDocking(false);

            self.suppDockUnDockTimeModel(SuppDockUnDockTime);
            $('#spnTitile').html("View Docking Undocking Times Capturing");

            if (SuppDockUnDockTime.ScheduleFromDate() == "Invalid date" || SuppDockUnDockTime.DryDockDateTime() == undefined) {
                SuppDockUnDockTime.ScheduleFromDate(null);
            }
            if (SuppDockUnDockTime.ScheduleToDate() == "Invalid date" || SuppDockUnDockTime.DryDockDateTime() == undefined) {
                SuppDockUnDockTime.ScheduleToDate(null);
            }

            $("#UserName").text(self.UserName);
            $("#Designation").text(self.Designation);
            $("#EnteredDockDateTime").data('kendoDateTimePicker').enable(false);
            $("#OnBlocksDateTime").data('kendoDateTimePicker').enable(false);
            $("#DryDockDateTime").data('kendoDateTimePicker').enable(false);
            $("#FinishedDockDateTime").data('kendoDateTimePicker').enable(false);
            $("#OffBlocksDateTime").data('kendoDateTimePicker').enable(false);
            $("#LeftDockDateTime").data('kendoDateTimePicker').enable(false);

        }

        // Update / Edit Mode
        self.editSuppDockUnDockTime = function (SuppDockUnDockTime) {

            // console.log(SuppDockUnDockTime);          

            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsSaveUpdateDisabled(false);
            self.suppDockUnDockTimeModel(SuppDockUnDockTime);
            $('#spnTitile').html("Update Docking Undocking Times Capturing");
            //var valuserName = ;
            $("#UserName").text(self.UserName());
            $("#Designation").text(self.Designation());
            if (SuppDockUnDockTime.ScheduleFromDate() == "Invalid date" || SuppDockUnDockTime.DryDockDateTime() == undefined) {
                SuppDockUnDockTime.ScheduleFromDate(null);
            }
            if (SuppDockUnDockTime.ScheduleToDate() == "Invalid date" || SuppDockUnDockTime.DryDockDateTime() == undefined) {
                SuppDockUnDockTime.ScheduleToDate(null);
            }
            //SuppDockUnDockTime.EnteredDockDateTime();
            //SuppDockUnDockTime.OnBlocksDateTime();
            //SuppDockUnDockTime.DryDockDateTime();
            var isEnteredDockDateTime = true;
            var isOnBlocksDateTime = true;
            var isDryDockDateTime = true;
            if (SuppDockUnDockTime.EnteredDockDateTime() == "Invalid date" || SuppDockUnDockTime.EnteredDockDateTime() == undefined) {
                isEnteredDockDateTime = false;
            }

            if (SuppDockUnDockTime.OnBlocksDateTime() == "Invalid date" || SuppDockUnDockTime.OnBlocksDateTime() == undefined) {
                isOnBlocksDateTime = false;
            }

            if (SuppDockUnDockTime.DryDockDateTime() == "Invalid date" || SuppDockUnDockTime.DryDockDateTime() == undefined) {
                isDryDockDateTime = false;
            }


            if ((isEnteredDockDateTime == true) && (isOnBlocksDateTime == true) && (isDryDockDateTime == true)) {
                self.IsUnDocking(true);
                self.IsDocking(false);
                $("#EnteredDockDateTime").data('kendoDateTimePicker').enable(false);
                $("#OnBlocksDateTime").data('kendoDateTimePicker').enable(false);
                $("#DryDockDateTime").data('kendoDateTimePicker').enable(false);
                $("#FinishedDockDateTime").data('kendoDateTimePicker').enable(true);
                $("#OffBlocksDateTime").data('kendoDateTimePicker').enable(true);
                $("#LeftDockDateTime").data('kendoDateTimePicker').enable(true);
            }
            else {
                self.IsUnDocking(false);
                self.IsDocking(true);
                $("#FinishedDockDateTime").data('kendoDateTimePicker').enable(false);
                $("#OffBlocksDateTime").data('kendoDateTimePicker').enable(false);
                $("#LeftDockDateTime").data('kendoDateTimePicker').enable(false);
                $("#EnteredDockDateTime").data('kendoDateTimePicker').enable(true);
                $("#OnBlocksDateTime").data('kendoDateTimePicker').enable(true);
                $("#DryDockDateTime").data('kendoDateTimePicker').enable(true);
            }


            //SuppDockUnDockTime.FinishedDockDateTime();
            //SuppDockUnDockTime.OffBlocksDateTime();
            //SuppDockUnDockTime.LeftDockDateTime();

            //if (SuppDockUnDockTime.EnteredDockDateTime() == undefined || SuppDockUnDockTime.EnteredDockDateTime() == "Invalid date") {
            //    self.IsDocking(true);
            //    self.IsUnDocking(false);
            //}
            //else if (SuppDockUnDockTime.FinishedDockDateTime() == undefined || SuppDockUnDockTime.FinishedDockDateTime() == "Invalid date") {
            //    self.IsDocking(true);
            //    self.IsUnDocking(true);
            //}


            //if (SuppDockUnDockTime.FinishedDockDateTime() == "Invalid date") {
            //    SuppDockUnDockTime.FinishedDockDateTime(null);
            //}
            //if (SuppDockUnDockTime.OffBlocksDateTime() == "Invalid date") {
            //    SuppDockUnDockTime.OffBlocksDateTime(null);
            //}
            //if (SuppDockUnDockTime.LeftDockDateTime() == "Invalid date") {
            //    SuppDockUnDockTime.LeftDockDateTime(null);
            //}
            //if (SuppDockUnDockTime.EnteredDockDateTime() != undefined && SuppDockUnDockTime.FinishedDockDateTime() != undefined) {
            //    self.IsDocking(true);
            //    self.IsUnDocking(true);
            //}


            var StartDateValue = SuppDockUnDockTime.ScheduleFromDate();
            var myDatePicker = new Date(StartDateValue);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = '00';
            var Mnt = '00';

            var EndDateValue = SuppDockUnDockTime.ScheduleToDate();
            var EmyDatePicker = new Date(EndDateValue);
            var Eday = EmyDatePicker.getDate();
            var Emonth = EmyDatePicker.getMonth();
            var Eyear = EmyDatePicker.getFullYear();
            var EHour = '23';
            var EMnt = '30';

            var CurrentDate = new Date();
            var Cday = CurrentDate.getDate();
            var Cmonth = CurrentDate.getMonth();
            var Cyear = CurrentDate.getFullYear();
            var CHour = CurrentDate.getHours();
            var CMnt = CurrentDate.getMinutes();
            

                if (CurrentDate <= myDatePicker) {                    
                    $("#EnteredDockDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#OnBlocksDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#DryDockDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#FinishedDockDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#OffBlocksDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#LeftDockDateTime").data('kendoDateTimePicker').min(new Date(Cyear, Cmonth, Cday, CHour, CMnt));

                }
                else {
                    $("#EnteredDockDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    $("#OnBlocksDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    $("#DryDockDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    $("#FinishedDockDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    $("#OffBlocksDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    $("#LeftDockDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                }


                if (CurrentDate <= EmyDatePicker) {                    
                    $("#EnteredDockDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#OnBlocksDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#DryDockDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#FinishedDockDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));                                
                    $("#OffBlocksDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                    $("#LeftDockDateTime").data('kendoDateTimePicker').max(new Date(Cyear, Cmonth, Cday, CHour, CMnt));
                }
                else {

                    $("#EnteredDockDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                    $("#OnBlocksDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                    $("#DryDockDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                    $("#FinishedDockDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                    $("#OffBlocksDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                    $("#LeftDockDateTime").data('kendoDateTimePicker').max(new Date(Eyear, Emonth, Eday, EHour, EMnt));
                }


            if (myDatePicker >= CurrentDate && EmyDatePicker >= CurrentDate) {                
                self.IsDocking(false);
                self.IsSaveUpdateDisabled(true);
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Docking Undocking Time Capturing Should be between Scheduled Dates.");              

            }
        }
        
        // Reset button
        self.ResetSuppDockUnDockTime = function (model) {
            ko.validation.reset();
            self.suppDockUnDockTimeModel().reset();
            self.SuppDockUnDockTimeValidation().errors.showAllMessages(false);
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            $("#spanEnteredDockDateTime").text('');
            $("#spanOnBlocksDateTime").text('');
            $("#spanDryDockDateTime").text('');
            $("#spanFinishedDockDateTime").text('');
            $("#spanOffBlocksDateTime").text('');
            $("#spanLeftDockDateTime").text('');
        }

        // Cancel Button
        self.cancel = function () {
            self.suppDockUnDockTimeModel().reset();
            self.LoadGrid();

            self.viewMode('List');
            $('#spnTitile').html("Docking Undocking Times Capturing List");

        }


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }
    IPMSRoot.SuppDockUnDockTimeViewModel = SuppDockUnDockTimeViewModel;


}(window.IPMSROOT));



