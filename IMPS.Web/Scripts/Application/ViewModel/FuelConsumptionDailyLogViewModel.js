(function (IPMSRoot) {

    var FuelConsumptionDailyLogViewModel = function () {

        var self = this;
        $('#spnTitle').html("Fuel Consumption Daily Log");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.fuelConsumptionLogReferenceData = ko.observable();
        self.fuelConsumptionDailyLoglist = ko.observableArray();
        self.FuelConsumptionDailyLogModel = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);
        self.CraftList = ko.observableArray();
        self.previousFuelLogList = ko.observableArray();
        self.validationHelper = new IPMSRoot.validationHelper();


        // This is used to Initialize method
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.FuelConsumptionDailyLogModel(new IPMSROOT.FuelConsumptionDailyLogModel());

            self.LoadFuelLogConsumptionDtls();
            self.viewMode('List');
            //  self.LoadCrafts();
        }

        // This is used to Add New Fuel Consumption Daily Log
        self.addFuelConsumptionDailyLog = function () {
            self.FuelConsumptionDailyLogModel(new IPMSRoot.FuelConsumptionDailyLogModel());
            //self.LoadCrafts();
            $('#spnTitle').html("Add Fuel Consumption Daily Log");
            self.viewMode('Form');
            self.IsSave(true);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            self.IsActivityEnable(false);
            $("#CraftName").data('kendoAutoComplete').enable(true);
            $("#StartDateTime").data('kendoDateTimePicker').enable(false);
            $("#EndDateTime").data('kendoDateTimePicker').enable(false);
            $("#StartDateTime").data('kendoDateTimePicker').max(new Date());
            $("#EndDateTime").data('kendoDateTimePicker').max(new Date());


        }


        // This is used to Edit Fuel Consumption Daily Log
        self.editFuelConsumptionDailyLog = function (data) {
            $('#spnTitle').html("Update Fuel Consumption Daily Log");

            self.FuelConsumptionDailyLogModel(data);
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsActivityEnable(true);
            //self.CraftSelect();
            $("#CraftName").data('kendoAutoComplete').enable(false);

            if (data.Crafts().CraftID != null) {

                self.viewModelHelper.apiGet('api/GetFUELLogGriddetails', { craftId: data.Crafts().CraftID },
                 function (result) {

                     self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(result, function (item) {
                         //self.FuelConsumptionDailyLogModel().PresentROB(item.PresentROB);
                         //self.FuelConsumptionDailyLogModel().PreviousROB(item.PreviousROB);
                         return new IPMSRoot.FuelConsumptionLogDtl(item);
                     }));
                 });
            }


            $("#StartDateTime").data('kendoDateTimePicker').min(data.StartDateTime());
            var myDatePicker = new Date(data.StartDateTime());
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#EndDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
            $("#StartDateTime").data('kendoDateTimePicker').max(new Date());
            $("#EndDateTime").data('kendoDateTimePicker').max(new Date());



        }


        // This is used to View Fuel Consumption Daily Log
        self.viewFuelConsumptionDailyLog = function (data) {
            $('#spnTitle').html("View Fuel Consumption Daily Log");
            self.FuelConsumptionDailyLogModel(data);
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            self.IsActivityEnable(false);

            $("#StartDateTime").data('kendoDateTimePicker').enable(false);
            $("#EndDateTime").data('kendoDateTimePicker').enable(false);
            $("#CraftName").data('kendoAutoComplete').enable(false);

            if (data.Crafts().CraftID != null) {

                self.viewModelHelper.apiGet('api/GetFUELLogGriddetails', { craftId: data.Crafts().CraftID },
                 function (result) {

                     self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(result, function (item) {
                         //self.FuelConsumptionDailyLogModel().PresentROB(item.PresentROB);
                         //self.FuelConsumptionDailyLogModel().PreviousROB(item.PreviousROB);
                         return new IPMSRoot.FuelConsumptionLogDtl(item);
                     }));
                 });
            }
        }


        // This is used to Cancel Fuel Consumption Daily Log
        self.cancelFuelConsumptionDailyLog = function () {

            self.FuelConsumptionDailyLogModel().reset();
            $('#spnTitle').html("Fuel Consumption Daily Log");
            self.viewMode('List');

        }

        //This is used to Validate numeric
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        };

        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function () {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }
        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        // Load All Crafts Data
        self.LoadCrafts = function () {
            self.viewModelHelper.apiGet('api/GetCraftDetails', null,
          function (result) {
              self.fuelConsumptionLogReferenceData(new IPMSRoot.FuelConsumptionLogReferenceData(result));
          }, null, null, false);
        }

        //This is used to Get All Fuel Consumption Daily Log Records
        self.LoadFuelLogConsumptionDtls = function () {

            self.viewModelHelper.apiGet('api/GetAllFuelConsumptionDailyLogDetails',
            null,
               function (result) {

                   self.fuelConsumptionDailyLoglist(ko.utils.arrayMap(result, function (item) {
                       return new IPMSRoot.FuelConsumptionDailyLogModel(item);
                   }));
               });
        }


        // This is used to fill Previous Fuel Log Grid based on Craft selection
        self.CraftSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.FuelConsumptionDailyLogModel().Crafts(selecteddataItem);
            self.FuelConsumptionDailyLogModel().CraftID(selecteddataItem.CraftID);
            self.FuelConsumptionDailyLogModel().Crafts().CraftID;

            if (self.FuelConsumptionDailyLogModel().Crafts().CraftID != null) {
                self.IsCodeEnable(true);


                self.viewModelHelper.apiGet('api/GetFUELLogGriddetails', { craftId: self.FuelConsumptionDailyLogModel().Crafts().CraftID },
                 function (result) {
                     if (result.length == 0) {
                         //$('#PreviousROB').text(0);
                         //$('#PresentROB').text(0);
                         self.FuelConsumptionDailyLogModel().PresentROB("0");
                         self.FuelConsumptionDailyLogModel().PreviousROB("0");
                         self.FuelConsumptionDailyLogModel().FuelReceived("0");
                     }
                     self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(result, function (item) {
                         self.FuelConsumptionDailyLogModel().PreviousROB(item.PresentROB);
                         self.FuelConsumptionDailyLogModel().PresentROB("0");
                         self.FuelConsumptionDailyLogModel().FuelReceived("0");
                         return new IPMSRoot.FuelConsumptionLogDtl(item);
                     }));
                 });

                $("#StartDateTime").data('kendoDateTimePicker').enable(true);
                $("#EndDateTime").data('kendoDateTimePicker').enable(true);
            }
            else {
                self.IsCodeEnable(false);
                $("#StartDateTime").data('kendoDateTimePicker').enable(false);
                $("#EndDateTime").data('kendoDateTimePicker').enable(false);

            }
        }

        CraftNameBlur = function () {

            if (self.FuelConsumptionDailyLogModel().CraftID() == "") {
                self.FuelConsumptionDailyLogModel().CraftName('');
                $('#CraftNCode').text('');
                $('#CraftNTyp').text('');
                $('#CraftNIMO').text('');
                $('#CraftNoil').text('');
                self.FuelConsumptionDailyLogModel().PresentROB('');
                self.FuelConsumptionDailyLogModel().PreviousROB('');




                self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(undefined, function (item) {
                    return new IPMSRoot.FuelConsumptionLogDtl(item);
                }));

            }
        }

        CraftNameKeypress = function () {
            self.FuelConsumptionDailyLogModel().CraftID('');
            self.FuelConsumptionDailyLogModel().CraftTypeName('');
            $('#CraftNCode').text('');
            $('#CraftNTyp').text('');
            $('#CraftNIMO').text('');
            $('#CraftNoil').text('');
            self.FuelConsumptionDailyLogModel().PresentROB('');
            self.FuelConsumptionDailyLogModel().PreviousROB('');

            self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(undefined, function (item) {
                return new IPMSRoot.FuelConsumptionLogDtl(item);
            }));

        }

        //Change of VCN need to reset all the values - Omprakash kotha on 11th November 2014
        $('#CraftName').live('keydown', function (e) {



            var charCode = e.which || e.keyCode;
            if (charCode == 8 || charCode == 46) {
                self.FuelConsumptionDailyLogModel().CraftID('');
                $('#CraftNCode').text('');
                $('#CraftNTyp').text('');
                $('#CraftNIMO').text('');
                $('#CraftNoil').text('');
                self.FuelConsumptionDailyLogModel().PresentROB('');
                self.FuelConsumptionDailyLogModel().PreviousROB('');

                self.FuelConsumptionDailyLogModel().CraftTypeName('');
                self.FuelConsumptionDailyLogModel().IMONo('');
                self.FuelConsumptionDailyLogModel().PreviousFuelLogDetails(ko.utils.arrayMap(undefined, function (item) {
                    return new IPMSRoot.FuelConsumptionLogDtl(item);
                }));
            }
        });


        // This is used to Save Fuel Consumption Daily Log
        self.SaveFuelConsumptionDailyLog = function (FuelConsumptionDailyLog) {
            var valid = true;
            self.FuelConsumptionDailyLogValidation = ko.observable(FuelConsumptionDailyLog);
            self.FuelConsumptionDailyLogValidation().errors = ko.validation.group(self.FuelConsumptionDailyLogValidation());
            var errors = self.FuelConsumptionDailyLogValidation().errors().length;

            //var match = ko.utils.arrayFirst(self.CraftList(), function (item) {
            //    return item.CraftID() === model.CraftID();
            //});

            //if (match == null) {
            //    toastr.options.closeButton = true;
            //    toastr.options.positionClass = "toast-top-right";
            //    toastr.warning("Please Select Valid Craft Name", "Fuel Consumption Daily Log");            
            //    return;
            //}



            if (errors == 0) {


                //if (FuelConsumptionDailyLog.PreviousROB() == 0) {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Please Enter Previous ROB greater than zero", "Fuel Consumption Daily Log");
                //    valid = false;
                //    return;
                //}
                //else if (FuelConsumptionDailyLog.PresentROB() == 0) {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Please Enter Present ROB greater than zero", "Fuel Consumption Daily Log");
                //    valid = false;
                //    return;
                //}
                //else if (FuelConsumptionDailyLog.RunningHours() == 0) {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Please Enter Running Hrs greater than zero", "Fuel Consumption Daily Log");
                //    valid = false;
                //    return;
                //}
                if (FuelConsumptionDailyLog.FuelConsumed() < 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Fuel Consumed cannot be negative.Please enter valid details", "Fuel Consumption Daily Log");
                    valid = false;
                    return;
                }


                if (valid = true) {
                    self.viewModelHelper.apiPost('api/AddFuelConsumptionDailyLog', ko.mapping.toJSON(FuelConsumptionDailyLog), function Message(data) {

                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Fuel Consumption Daily Log details saved successfully", "Fuel Consumption Daily Log");
                        self.LoadFuelLogConsumptionDtls();
                        self.viewMode('List');
                        $('#spnTitle').html("Fuel Consumption Daily Log");

                    });
                }
            }

            else {
                self.FuelConsumptionDailyLogValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                // $('#divValidationError').removeClass('display-none');
                return;
            }

        }


        // This is used to Update Fuel Consumption Daily Log
        self.UpdateFuelConsumptionDailyLog = function (FuelConsumptionDailyLog) {
            self.FuelConsumptionDailyLogValidation = ko.observable(FuelConsumptionDailyLog);
            self.FuelConsumptionDailyLogValidation().errors = ko.validation.group(self.FuelConsumptionDailyLogValidation());
            var errors = self.FuelConsumptionDailyLogValidation().errors().length;

            if (errors == 0) {

                if (FuelConsumptionDailyLog.FuelConsumed() < 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Fuel Consumed cannot be negative.Please enter valid details", "Fuel Consumption Daily Log");
                    valid = false;
                    return;
                }

                if (valid = true) {

                    self.viewModelHelper.apiPut('api/ModifyFuelConsumptionDailyLog', ko.mapping.toJSON(FuelConsumptionDailyLog), function Message(data) {

                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Fuel Consumption Daily Log details Updated successfully", "Fuel Consumption Daily Log");
                        self.LoadFuelLogConsumptionDtls();
                        self.viewMode('List');
                        $('#spnTitle').html("Fuel Consumption Daily Log");

                    });
                }
            }
            else {
                self.FuelConsumptionDailyLogValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                //$('#divValidationError').removeClass('display-none');
                return;
            }

        }


        //Only Future Dates
        //calOpen = function () {
        //    this.min(new Date());
        //};

        //Start Running Hours Date Change
        //debugger;
        StartRunningHrs = function () {
            if ($("#StartDateTime").val() == "" || $("#StartDateTime").val() == null) {
                $("#spanStartDateTime").text(' This field is required.');
                // $("#EndRunning").val('');               
                self.FuelConsumptionDailyLogModel().EndDateTime("");
            }
            else {
                $("#spanStartDateTime").text('');
                var StartDateValue = $("#StartDateTime").val();
                var EndDateValue = $("#EndDateTime").val();
                //$("#EndRunning").val('');            
                self.FuelConsumptionDailyLogModel().EndDateTime("");
                // self.FuelConsumptionDailyLogModel().RunningHours("");

                var myDatePicker = new Date(StartDateValue);
                var day = myDatePicker.getDate();
                var month = myDatePicker.getMonth();
                var year = myDatePicker.getFullYear();
                var Hour = myDatePicker.getHours();
                var Mnt = myDatePicker.getMinutes();
                //$("#EndRunning").data('kendoDateTimePicker').min(StartDateValue);
                $("#EndDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                //  $("#EndRunning").data('kendoDateTimePicker').max(new Date());

            }
        }

        //End Running Hours Date Change
        //debugger;
        EndRunningHrs = function () {
            if (!(($("#StartDateTime").val() == "" || $("#StartDateTime").val() == null))) {
                if ($("#EndDateTime").val() == "" || $("#EndDateTime").val() == null) {
                    $("#spanEndDateTime").text(' This field is required.');
                }
                else {
                    $("#spanEndDateTime").text('');
                }
            }
            else {
                $("#StartDateTime").focus();
                $("#spanStartDateTime").text(' This field is required.');
                // $("#EndRunning").val('');           
                //  self.FuelConsumptionDailyLogModel().EndRunningHrs("");
                $("#spanEndDateTime").text('');
            }
        }


        self.PresentROBChange = function (event) {
            if (event.FuelReceived() != "") {
                var PresentROB = parseFloat(event.PreviousROB()) + parseFloat(event.FuelReceived());
                self.FuelConsumptionDailyLogModel().PresentROB(PresentROB);
            }
        }

        self.RunningHrsChange = function (event) {
            if ($("#StartRunning").val() != "" && $("#EndRunning").val() != "") {
                var StartHrs = parseFloat($("#StartRunning").val());
                var EndHrs = parseFloat($("#EndRunning").val());
                if (StartHrs >= EndHrs) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("End Running Hrs should be greater than Start Running Hrs");
                    //$("#EndRunning").val("");
                    self.FuelConsumptionDailyLogModel().EndRunningHrs("");
                }
                else {

                    var RunningHr = parseFloat(EndHrs - StartHrs).toFixed(2);
                    self.FuelConsumptionDailyLogModel().RunningHours(RunningHr);
                    Math.round(RunningHr);


                }

            }
            else if ($("#StartRunning").val() == "" || $("#EndRunning").val() == "") {
                self.FuelConsumptionDailyLogModel().RunningHours("");
            }
        }

        self.Initialize();

    }
    IPMSRoot.FuelConsumptionDailyLogViewModel = FuelConsumptionDailyLogViewModel;
}(window.IPMSROOT));
