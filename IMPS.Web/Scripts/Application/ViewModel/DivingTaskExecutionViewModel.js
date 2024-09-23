(function (IPMSRoot) {

    var DivingTaskExecutionViewModel = function () {

        var self = this;

        $("#DivingTaskExecutionTitle").text('Diving Task Execution');
        self.viewModelHelper = new IPMSRoot.viewModelHelper();
        self.viewMode = ko.observable();
        self.divingTaskExecutionModel = ko.observable();
        self.DivingTaskExecutionList = ko.observableArray();
        self.viewModeForTabs = ko.observable();
        self.IsEditable = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsSaveVisible = ko.observable(false);
        self.LocationList = ko.observableArray();
        self.IsEPQStatusEnabled = ko.observable(false);
        self.IsPPEStatusEnabled = ko.observable(false);
        self.IsWBPStatusEnabled = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.userName = ko.observable();
        self.minDate = ko.observable();
        self.maxDate = ko.observable();
        self.isClearanceNoMsg = ko.observable(false);
        self.IsView = ko.observable(false);

        //Diving Task Execution Initialization(pageload) mode
        self.Initialize = function () {
            self.LoggedInUserName();
            self.divingTaskExecutionModel(new IPMSROOT.DivingTaskExecutionModel());
            self.LoadDivingTaskExecutionList();
            self.LoadLocations();
            self.viewModeForTabs('taskexecution1');
            self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
            self.viewMode('List');
        }

        //LoadDivingTaskExecution fetches the data from API Service
        self.LoadDivingTaskExecutionList = function () {
            self.viewModelHelper.apiGet('api/DivingTaskExecutions', null, function (result) {
                self.DivingTaskExecutionList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.DivingTaskExecutionModel(item);
                }));
                //console.log(ko.toJSON(self.DivingTaskExecutionList()));
            }, null, null, false);
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //LoadLocation fetches the data from API Service
        self.LoadLocations = function () {
            self.viewModelHelper.apiGet('api/GetOtherLocations', null, function (result) {
                ko.mapping.fromJS(result, {}, self.LocationList);
            }, null, null, false);
        }

        self.LoggedInUserName = function () {
            self.viewModelHelper.apiGet('api/GetLoggedInUserName', null,
                function (result) {
                    self.userName(result);
                }, null, null, false);
        }

        //Grid Action
        //View Details of selected Row in the Grid List of Supplementary Service Request List
        //Author  : Sandeep A
        //Date    : 5th September 2014
        self.ViewDivingTaskExecution = function (divingtaskexecution) {
           
            self.viewMode('Form');
            self.IsEPQStatusEnabled(false);
            self.IsPPEStatusEnabled(false);
            self.IsWBPStatusEnabled(false);
            self.divingTaskExecutionModel(divingtaskexecution);

            var index = 0;
            HandleProgressBarAndActiveTab(index);

            self.IsUpdate(false);
            self.IsEditable(false);
            self.IsReset(false);
            self.IsView(true);

            self.viewModeForTabs('taskexecution1');
            self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
            $("#DivingTaskExecutionTitle").text('Diving Task Execution');
            $("#Date").data('kendoDateTimePicker').enable(false);
            $("#StartTime").data('kendoDateTimePicker').enable(false);
            $("#StopTime").data('kendoDateTimePicker').enable(false);
            $("#LoggedDiveTimeFrom").data('kendoDateTimePicker').enable(false);
            $("#LoggedDiveTimeTo").data('kendoDateTimePicker').enable(false);
            $("#TimeDiveOperationCancelled").data('kendoDateTimePicker').enable(false);
            $("#TimeLeftWorkshop").data('kendoDateTimePicker').enable(false);
            $("#TimeArrivedSite").data('kendoDateTimePicker').enable(false);
            $("#TimeLeftSite").data('kendoDateTimePicker').enable(false);
            $("#TimeArrivedWorkshop").data('kendoDateTimePicker').enable(false);
            $("#TimeArrivedSite").data('kendoDateTimePicker').enable(false);
            $("#LostDiveTime").data('kendoDateTimePicker').enable(false);
        }

        //Grid Action        
        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Purpose : Edit Details of selected Row in the Grid List of Supplementary Service Request List
        self.EditDivingTaskExecution = function (divingtaskexecution) {

            self.minDate("");
            self.maxDate("");
            self.minDate(moment(divingtaskexecution.OcupationFromDate()).format('YYYY-MM-DD HH:mm'));
            self.maxDate(moment(divingtaskexecution.OcupationToDate()).format('YYYY-MM-DD HH:mm'));

            $("#DivingTaskExecutionTitle").text('Diving Task Execution');
            self.viewModeForTabs('taskexecution1');

            divingtaskexecution.DivingCheckList().DivingSupervisorName(self.userName());
            //divingtaskexecution.DivingCheckList().DiveReferenceNo(divingtaskexecution.DRN());

            if (divingtaskexecution.DivingCheckList().EQPOtherStatus() == true) {
                self.IsEPQStatusEnabled(true);
            }

            if (divingtaskexecution.DivingCheckList().PPEOtherStatus() == true) {
                self.IsPPEStatusEnabled(true);
            }

            if (divingtaskexecution.DivingCheckList().WBPOtherStatus() == true) {
                self.IsWBPStatusEnabled(true);
            }

            self.divingTaskExecutionModel(divingtaskexecution);
            self.IsEditable(true);
            self.IsUpdate(true);
            self.IsReset(true);
            self.viewMode('Form');
            self.IsView(false);
            self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
            var index = 0;
            HandleProgressBarAndActiveTab(index);
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Purpose : Other Check Box change Under PPE
        self.IschkPPEStatus = function (event) {
            if (!event.PPEOtherStatus()) {
                self.IsPPEStatusEnabled(true);
            }
            else {
                self.IsPPEStatusEnabled(false);
                $("#PPEOtherDescription").val("");
                self.divingTaskExecutionModel().PPEOtherDescription("");
            }
        }

        self.IschkWBPStatus = function (event) {
            if (!event.WBPOtherStatus()) {
                self.IsWBPStatusEnabled(true);
            }
            else {
                self.IsWBPStatusEnabled(false);
                $("#WBPOtherDescription").val("");
                self.divingTaskExecutionModel().WBPOtherDescription("");
            }
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Purpose : Other Check Box change Under Equipment
        self.IschkEQPStatus = function (event) {
            if (!event.EQPOtherStatus()) {
                self.IsEPQStatusEnabled(true);
            }
            else {
                self.IsEPQStatusEnabled(false);
                $("#EQPOtherDescription").val("");
                self.divingTaskExecutionModel().EQPOtherDescription("");
            }
        }

        //Action : Button Add (Pre Diving Safety CheckList Form)
        //Author : Sandeep A
        //Date   : 5th September 2014
        //Purpose : Dynamically adding rows to "Onsite Hazard Identification Risk Assessment"
        self.AddNewHazard = function (hazarddata) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            //$('#divValidationErrors').removeClass('alert');
            //$('#spanValidationErrors').text('');
            var databaseList = ko.toJSON(self.divingTaskExecutionModel().DivingCheckList().DivingCheckListHazard);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var status = true;

            $.each(jsonObj, function (index, value) {

                rid = rid + 1;
                if (value.Hazard == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid, "Pre Diving Safety Checklist");
                    status = false;
                    return;
                }
                if (value.Cause == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid, "Pre Diving Safety Checklist");
                    status = false;
                    return;
                }

                if (value.Action == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please enter Hazard data at row number : ' + rid, "Pre Diving Safety Checklist");
                    status = false;
                    return;
                }
            });

            if (status) {
                self.divingTaskExecutionModel().DivingCheckList().DivingCheckListHazard.push(new IPMSROOT.DivingCheckListHazards());
            }
        }

        //Action : Button Add (Diving Task Execution Form)
        //Author : Sandeep A
        //Date   : 5th September 2014
        //Purpose : Dynamically adding rows to "Name of Drivers"
        self.AddDiverName = function (divername) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            //$('#divValidationErrors').removeClass('alert');
            //$('#spanValidationErrors').text('');
            var databaseList = ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers1);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var status = true;

            $.each(jsonObj, function (index, value) {
                rid = rid + 1;
                if (value.DiverName == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Name of drivers details grid error : Please enter Driver name at row number : ' + rid);
                    toastr.warning('Name of drivers details grid error : Please enter Driver name at row number : ' + rid, "Diving Task Execution");
                    status = false;
                    return false;
                }
            });

            if (status) {
                self.divingTaskExecutionModel().DivingRequestDivers1.push(new IPMSROOT.DivingRequestDrivers());
            }
        }

        //Action : Button Add (Diving Task Execution Form)
        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Purpose : Dynamically adding rows to "Name of Standby Drivers"
        self.AddStandByDiver = function (divername) {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            //$('#divValidationErrors').removeClass('alert');
            //$('#spanValidationErrors').text('');

            var databaseList = ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers2);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var status = true;

            $.each(jsonObj, function (index, value) {
                rid = rid + 1;
                if (value.DiverName == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Name of standby drivers details grid error : Please enter Driver name at row number : ' + rid);
                    toastr.warning('Name of standby drivers details grid error : Please enter Driver name at row number : ' + rid, "Diving Task Execution");
                    status = false;
                    return;
                }
            });

            if (status) {
                self.divingTaskExecutionModel().DivingRequestDivers2.push(new IPMSROOT.DivingRequestDrivers());
            }
        }

        //Action : Button Add (Diving Task Execution Form)
        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Purpose : Dynamically adding rows to "Onsite Hazard Identification Risk Assessment"
        self.AddDiver = function (divername) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            //$('#divValidationErrors').removeClass('alert');
            var databaseList = ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers3);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var status = true;
            var date = null;
            $.each(jsonObj, function (index, value) {
                rid = rid + 1;
                if (value.DiverName == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter Driver name at row number : ' + rid);
                    toastr.warning('Add diver details grid error : Please enter Driver name at row number : ' + rid, "Diving Task Execution");
                    status = false;
                    return false;
                }
                if (value.TimeLeftSurface == "" || value.TimeLeftSurface == undefined) {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeLeftSurface datetime at row number : ' + rid);
                    toastr.warning('Add diver details grid error : Please enter TimeLeftSurface datetime at row number : ' + rid, "Diving Task Execution");
                    status = false;
                    return false;
                }

                if (value.TimeArrivedSurface == "" || value.TimeArrivedSurface == undefined) {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeArrivedSurface datetime at row number : ' + rid);
                    toastr.warning('Add diver details grid error : Please enter TimeArrivedSurface datetime at row number : ' + rid, "Diving Task Execution");
                    status = false;
                    return false;
                }



                if (date == null) {
                    date = moment(value.TimeLeftSurface).format('YYYY-MM-DD HH:mm');
                }

                var TimeArrivedSurface = moment(value.TimeArrivedSurface).format('YYYY-MM-DD HH:mm');
                var TimeLeftSurface = moment(value.TimeLeftSurface).format('YYYY-MM-DD HH:mm');
                //alert(TimeLeftSurface + " "+ date);
                //alert(TimeArrivedSurface + " " + TimeLeftSurface);


                if (TimeLeftSurface >= date && TimeArrivedSurface >= TimeLeftSurface) {
                    date = TimeArrivedSurface;
                }
                else {
                    status = false;
                    //alert(TimeArrivedSurface + " " + TimeLeftSurface);
                    if (TimeLeftSurface < date) {
                        if (rid == 1) {
                            toastr.warning("Add diver details grid error : Please enter TimeLeftSurface datetime should be greater than TimeArrivedSurface at row number : " + rid, "Diving Task Execution");
                            return false;
                        }
                        else {
                            toastr.warning("Add diver details grid error : Please enter TimeLeftSurface datetime at row number: " + rid + " should be greater than TimeArrivedSurface at row number : " + (rid - 1), "Diving Task Execution");
                            return false;
                        }
                    }
                    else if (TimeArrivedSurface < TimeLeftSurface) {
                        toastr.warning("Add diver details grid error : Please enter TimeArrivedSurface datetime should be greater than TimeLeftSurface at row number : " + rid, "Diving Task Execution");
                        return false;
                    }
                    //DivingTaskExecutionData.DivingRequestDivers3()[index].TimeArrivedSurface("");
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeArrivedSurface value should be greater than TimeLeftSurface datetime at row number : ' + rid2);
                    //toastr.success("Add diver details grid error : Please enter TimeArrivedSurface value should be greater than TimeLeftSurface datetime at row number : " + rid, "Diving Task Execution");
                    //return;
                }
            });

            if (status) {
                self.divingTaskExecutionModel().DivingRequestDivers3.push(new IPMSROOT.DivingRequestDrivers());
            }
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Save
        //Purpose : Executing the selected Diving Task Execution
        self.ModifyPreDivingCheckList = function (DivingTaskExecutionData) {
            debugger;

            DivingTaskExecutionData.SupervisorName(self.userName());
            DivingTaskExecutionData.DivingCheckList().DivingSupervisorName(self.userName());

            if (DivingTaskExecutionData.DivingCheckList().Date() == null) {
                DivingTaskExecutionData.DivingCheckList().Date("");
            }

            //if (DivingTaskExecutionData().StartTime() == null) {
            //    DivingTaskExecutionData().StartTime("");
            //}
            //if (DivingTaskExecutionData().StopTime() == null) {
            //    DivingTaskExecutionData().StopTime("");
            //}
            //if (DivingTaskExecutionData().LoggedDiveTimeFrom() == null) {
            //    DivingTaskExecutionData().LoggedDiveTimeFrom("");
            //}
            //if (DivingTaskExecutionData().LoggedDiveTimeTo() == null) {
            //    DivingTaskExecutionData().LoggedDiveTimeTo("");
            //}
            //if (DivingTaskExecutionData().TimeDiveOperationCancelled() == null) {
            //    DivingTaskExecutionData().TimeDiveOperationCancelled("");
            //}
            //if (DivingTaskExecutionData().TimeLeftWorkshop() == null) {
            //    DivingTaskExecutionData().TimeLeftWorkshop("");
            //}
            //if (DivingTaskExecutionData().TimeArrivedSite() == null) {
            //    DivingTaskExecutionData().TimeArrivedSite("");
            //}
            //if (DivingTaskExecutionData().TimeLeftSite() == null) {
            //    DivingTaskExecutionData().TimeLeftSite("");
            //}
            //if (DivingTaskExecutionData().TimeArrivedWorkshop() == null) {
            //    DivingTaskExecutionData().TimeArrivedWorkshop("");
            //}
            //if (DivingTaskExecutionData().LostDiveTime() == null) {
            //    DivingTaskExecutionData().LostDiveTime("");
            //}

            $("#spanDiveReferenceNo").text('');
            $("#spanDate").text('');

            var errors = 0;

            if ($("#DiveReferenceNo").val() == "") {
                $("#spanDiveReferenceNo").text('Please select dive reference number.');
                errors = errors + 1;
            }

            if ($("#Date").val() == "") {
                $("#spanDate").text('Please select date.');
                errors = errors + 1;
            }

            var length = $("input[name='WBP']:checked").length;

            if (length == 0) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please check atleast one checkbox under work being performed.", "PreDiving Safety Check List");
                errors = errors + 1;
            }

            if ($("#WBPOtherStatus").is(':checked')) {
                if ($("#WBPOtherDescription").val() == "") {
                    toastr.warning("Please enter others textbox under Work Being Performed.", "PreDiving Safety Check List");
                    errors = errors + 1;
                }
            }

            if ($("#PPEOther").is(':checked')) {
                if ($("#PPEOtherDescription").val() == "") {
                    toastr.warning("Please enter others textbox under PPE.", "PreDiving Safety Check List");
                    errors = errors + 1;
                }
            }

            if ($("#EQPOther").is(':checked')) {
                if ($("#EQPOtherDescription").val() == "") {
                    toastr.warning("Please enter others textbox under Equipment.", "PreDiving Safety Check List");
                    errors = errors + 1;
                }
            }

            var row = 0;
            var errors1 = 0;
            var jsonobj = JSON.parse(ko.toJSON(self.divingTaskExecutionModel().DivingCheckList().DivingCheckListHazard));

            $.each(jsonobj, function (key, value) {

                row = row + 1;

                if (value.Hazard == "") {

                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Hazard at row number : ' + row);
                    toastr.warning("Onsite hazard identification risk assessment details grid error : Please select Hazard at row number : " + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }

                if (value.Cause == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Cause at row number : ' + row);
                    toastr.warning("Onsite hazard identification risk assessment details grid error : Please select Cause at row number : " + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }

                if (value.Action == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Action at row number : ' + row);
                    toastr.warning("Onsite hazard identification risk assessment details grid error : Please select Action at row number : " + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }
            });

            if (errors1 > 0 || errors > 0) {
                return;
            }
            else {
                //$('#divValidationErrors').removeClass('alert');
                //$('#spanValidationErrors').text('');
                if (errors == 0) {
                    self.viewModelHelper.apiPost('api/PreDivingCheckList', ko.mapping.toJSON(DivingTaskExecutionData), function Message(data) {
                        self.LoadDivingTaskExecutionList();
                        self.viewMode('Form');
                        if (self.viewModeForTabs() == "taskexecution1") {
                            GoToTab2(self, DivingTaskExecutionData);
                        }
                    });
                }
                else {
                    return;
                }
            }
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Save
        //Purpose : Executing the selected Pre Diving CheckList
        self.ModifyDivingTaskExecution = function (DivingTaskExecutionData) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            //if (DivingTaskExecutionData().DivingCheckList().Date() == null) {
            //    DivingTaskExecutionData().DivingCheckList().Date("");
            //}

            if (DivingTaskExecutionData.StartTime() == null) {
                DivingTaskExecutionData.StartTime("");
            }
            if (DivingTaskExecutionData.StopTime() == null) {
                DivingTaskExecutionData.StopTime("");
            }
            if (DivingTaskExecutionData.LoggedDiveTimeFrom() == null) {
                DivingTaskExecutionData.LoggedDiveTimeFrom("");
            }
            if (DivingTaskExecutionData.LoggedDiveTimeTo() == null) {
                DivingTaskExecutionData.LoggedDiveTimeTo("");
            }           
            if (DivingTaskExecutionData.TimeDiveOperationCancelled() == null) {
                DivingTaskExecutionData.TimeDiveOperationCancelled("");
            }
            if (DivingTaskExecutionData.TimeLeftWorkshop() == null) {
                DivingTaskExecutionData.TimeLeftWorkshop("");
            }
            if (DivingTaskExecutionData.TimeArrivedSite() == null) {
                DivingTaskExecutionData.TimeArrivedSite("");
            }
            if (DivingTaskExecutionData.TimeLeftSite() == null) {
                DivingTaskExecutionData.TimeLeftSite("");
            }
            if (DivingTaskExecutionData.TimeArrivedWorkshop() == null) {
                DivingTaskExecutionData.TimeArrivedWorkshop("");
            }
            if (DivingTaskExecutionData.LostDiveTime() == null) {
                DivingTaskExecutionData.LostDiveTime("");
            }            

            self.DivingTaskExecutionValidation = ko.observable(DivingTaskExecutionData);
            self.DivingTaskExecutionValidation().errors = ko.validation.group(self.DivingTaskExecutionValidation());

            var errors = self.DivingTaskExecutionValidation().errors().length;

            var errorCount = 0;
            if ($("#ClearanceNo").val() == null || $("#ClearanceNo").val() == '' || $("#ClearanceNo").val() == undefined) {
                $("#ClearanceNoMsg").text('* Please enter Clearance No.');
                self.isClearanceNoMsg(true);
                errorCount = errorCount + 1;
            }
            else {
                $("#ClearanceNoMsg").text('');
                self.isClearanceNoMsg(false);
            }

            var DivingRequestDivers1 = JSON.parse(ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers1));
            var rid = 0;
            var rid1 = 0;
            var rid2 = 0;
            var errors1 = 0;

            $.each(DivingRequestDivers1, function (index, value) {
                rid = rid + 1;
                if (value.DiverName == "") {

                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Name of drivers details grid error : Please enter Driver Name at row number : ' + rid);
                    toastr.warning('Name of drivers details grid error : Please enter Driver Name at row number : ' + rid, "Diving Task Execution");
                    errors1 = errors1 + 1;
                    return false;
                }
            });

            var DivingRequestDivers2 = JSON.parse(ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers2));

            $.each(DivingRequestDivers2, function (index, value) {
                rid1 = rid1 + 1;
                if (value.DiverName == "") {

                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Name of standby drivers details grid error : Please enter Driver Name at row number : ' + rid1);
                    toastr.warning('Name of standby drivers details grid error : Please enter Driver Name at row number : ' + rid, "Diving Task Execution");
                    errors1 = errors1 + 1;
                    return;
                }
            });

            var DivingRequestDivers3 = JSON.parse(ko.toJSON(self.divingTaskExecutionModel().DivingRequestDivers3));
            var date = null;

            $.each(DivingRequestDivers3, function (index, value) {
                rid2 = rid2 + 1;
                if (value.DiverName == "") {

                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter Driver Name at row number : ' + rid2);
                    toastr.warning('Add diver details grid error : Please enter Driver Name at row number : ' + rid2, "Diving Task Execution");
                    errors1 = errors1 + 1;
                    return false;
                }
                if (value.TimeLeftSurface == "" || value.TimeLeftSurface == undefined) {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeLeftSurface datetime at row number : ' + rid2);
                    toastr.warning('Add diver details grid error : Please enter TimeLeftSurface datetime at row number : ' + rid2, "Diving Task Execution");
                    errors1 = errors1 + 1;
                    return false;
                }
                if (value.TimeArrivedSurface == "" || value.TimeArrivedSurface == undefined) {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeArrivedSurface datetime at row number : ' + rid2);
                    toastr.warning('Add diver details grid error : Please enter TimeArrivedSurface datetime at row number : ' + rid2, "Diving Task Execution");
                    errors1 = errors1 + 1;
                    return false;
                }

                if (date == null) {
                    date = moment(value.TimeLeftSurface).format('YYYY-MM-DD HH:mm');
                }

                var TimeArrivedSurface = moment(value.TimeArrivedSurface).format('YYYY-MM-DD HH:mm');
                var TimeLeftSurface = moment(value.TimeLeftSurface).format('YYYY-MM-DD HH:mm');


                if (TimeLeftSurface >= date && TimeArrivedSurface >= TimeLeftSurface) {
                    date = TimeArrivedSurface;
                }

                else {
                    errors1 = errors1 + 1;
                    //alert(TimeArrivedSurface + " " + TimeLeftSurface);
                    if (TimeLeftSurface < date) {
                        if (rid2 == 1) {
                            toastr.warning("Add diver details grid error : Please enter TimeLeftSurface datetime should be greater than TimeArrivedSurface at row number : " + rid2, "Diving Task Execution");
                            return false;
                        }
                        else {
                            toastr.warning("Add diver details grid error : Please enter TimeLeftSurface datetime at row number: " + rid2 + " should be greater than TimeArrivedSurface at row number : " + (rid2 - 1), "Diving Task Execution");
                            return false;
                        }
                    }
                    else if (TimeArrivedSurface < TimeLeftSurface) {
                        toastr.warning("Add diver details grid error : Please enter TimeArrivedSurface datetime should be greater than TimeLeftSurface at row number : " + rid2, "Diving Task Execution");
                        return false;
                    }

                    //DivingTaskExecutionData.DivingRequestDivers3()[index].TimeArrivedSurface("");
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanvalidationErrors').text('Add diver details grid error : Please enter TimeArrivedSurface value should be greater than TimeLeftSurface datetime at row number : ' + rid2);
                    //toastr.success("Add diver details grid error : Please enter TimeArrivedSurface value should be greater than TimeLeftSurface datetime at row number : " + rid2, "Diving Task Execution");
                }
            });
            if (errors1 > 0 || errorCount > 0) {
                return;
            }
            else {
                //$('#divValidationErrors').removeClass('alert');
                //$('#spanValidationErrors').text('');
                if (errors == 0) {
                    self.viewModelHelper.apiPost('api/DivingTaskExecutions', ko.mapping.toJSON(DivingTaskExecutionData), function Message(data) {
                        //toastr.options.closeButton = true;
                        //toastr.options.positionClass = "toast-top-right";
                        toastr.success("Diving task execution details updated successfully.", "Diving Task Execution");
                        self.LoadDivingTaskExecutionList();
                        self.viewMode('List');
                    });
                }
                else {
                    return;
                }
            }
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Reset
        //Purpose : Reset Diving Task Execution saved data
        self.ResetDivingTaskExecution = function (divingTaskExecution) {

            var notify = self.viewModeForTabs();

            self.divingTaskExecutionModel().reset();            
            self.viewModeForTabs(notify);
            self.divingTaskExecutionModel().ViewModeForTabs(notify);
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Cancel
        //Purpose : Cancel Diving Task Execution and redirect from Form to List
        self.CancelDivingtaskExecution = function (divingTaskExecution) {

            self.divingTaskExecutionModel().reset();
            self.viewModeForTabs('taskexecution1');

            self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
            self.viewMode("List");
        }

        self.GotoTab1 = function (divingtaskexecutionData) {
            self.viewModeForTabs('taskexecution1');
            self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
            GoToTab1(self, divingtaskexecutionData);
        }

        self.GotoTab2 = function (divingtaskexecutionData, e) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            divingtaskexecutionData.SupervisorName(self.userName());

            $("#spanDiveReferenceNo").text('');
            $("#spanDate").text('');

            var errors = 0;

            if (self.IsEditable()) {


                if ($("#DiveReferenceNo").val() == "") {
                    $("#spanDiveReferenceNo").text('Please select dive reference number.');
                    errors = errors + 1;
                }

                if ($("#Date").val() == "") {
                    $("#spanDate").text('Please select date.');
                    errors = errors + 1;
                }

                var length = $("input[name='WBP']:checked").length;

                if (length == 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please check atleast one checkbox under work being performed.", "PreDiving Safety Check List");
                    errors = errors + 1;
                }

                if ($("#WBPOtherStatus").is(':checked')) {
                    if ($("#WBPOtherDescription").val() == "") {
                        toastr.warning("Please enter others textbox under Work Being Performed.", "PreDiving Safety Check List");
                        errors = errors + 1;
                    }
                }

                if ($("#PPEOther").is(':checked')) {
                    if ($("#PPEOtherDescription").val() == "") {
                        toastr.warning("Please enter others textbox under PPE.", "PreDiving Safety Check List");
                        errors = errors + 1;
                    }
                }

                if ($("#EQPOther").is(':checked')) {
                    if ($("#EQPOtherDescription").val() == "") {
                        toastr.warning("Please enter others textbox under Equipment.", "PreDiving Safety Check List");
                        errors = errors + 1;
                    }
                }
            }

            var row = 0;
            var errors1 = 0;
            var jsonobj = JSON.parse(ko.toJSON(self.divingTaskExecutionModel().DivingCheckList().DivingCheckListHazard));

            $.each(jsonobj, function (key, value) {

                row = row + 1;

                if (value.Hazard == "") {

                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Hazard at row number : ' + row);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please select Hazard at row number : ' + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }

                if (value.Cause == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Cause at row number : ' + row);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please select Cause at row number : ' + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }

                if (value.Action == "") {
                    //$('#divValidationErrors').removeClass('display-none').addClass('alert');
                    //$('#spanValidationErrors').text('Onsite hazard identification risk assessment details grid error : Please select Action at row number : ' + row);
                    toastr.warning('Onsite hazard identification risk assessment details grid error : Please select Action at row number : ' + row, "PreDiving Safety Check List");
                    errors1 = errors1 + 1;
                    return;
                }
            });

            if (errors1 > 0 || errors > 0) {
                var index = 0;
                $("#ulTabs2").removeClass('active');
                $("#ulTabs1").addClass('active');
                e.stopPropagation();
                return false;
            }
            else {
                var status = true;
                if (self.IsEditable()) {
                    if (divingtaskexecutionData.DivingCheckList().DivingCheckListID() == 0) {
                        status = false;
                    }
                }
                if (status) {
                    self.viewModeForTabs('taskexecution2');
                    self.divingTaskExecutionModel().ViewModeForTabs('taskexecution2');
                    GoToTab2(self, divingtaskexecutionData);
                    //}
                }
                else {
                    self.viewModeForTabs('taskexecution1');
                    self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
                    GoToTab1(self, divingtaskexecutionData);
                }
            }
        }

        // Display Maximum date in DateTime picker as today date
        calmaxtoday = function () {
            $("#Date").data('kendoDateTimePicker').min(self.minDate());
            $("#Date").data('kendoDateTimePicker').max(self.maxDate());

            $("#TimeDiveOperationCancelled").data('kendoDateTimePicker').min(self.minDate());
            $("#TimeDiveOperationCancelled").data('kendoDateTimePicker').max(self.maxDate());

            $("#LostDiveTime").data('kendoDateTimePicker').min(self.minDate());
            $("#LostDiveTime").data('kendoDateTimePicker').max(self.maxDate());
        };

        calStartTime = function () {
            $("#HoursOfOccupation2").val('');
            self.divingTaskExecutionModel().HoursOfOccupation2('');

            $("#StopTime").val('');

            $("#StartTime").data('kendoDateTimePicker').min(self.minDate());
            $("#StartTime").data('kendoDateTimePicker').max(self.maxDate());
        }

        clickStartTime = function () {
            $("#StopTime").val('');
            $("#HoursOfOccupation2").val('');
            self.divingTaskExecutionModel().HoursOfOccupation2('');
        }

        // Display Maximum date in DateTime picker as today date and minimum date in DateTime picker as Start Time
        calEndDatetoday = function () {
            var startTime = $("#StartTime").val();
            if (startTime != null && startTime != "") {
                $("#StopTime").data('kendoDateTimePicker').min(startTime);
            }
            else {
                $("#StopTime").data('kendoDateTimePicker').min(self.minDate());
            }
            $("#StopTime").data('kendoDateTimePicker').max(self.maxDate());

        };

        calLoggedDiveTimeFrom = function () {
            $("#LoggedDiveTimeFrom").data('kendoDateTimePicker').min(self.minDate());
            $("#LoggedDiveTimeFrom").data('kendoDateTimePicker').max(self.maxDate());
            $("#LoggedDiveTimeTo").val('');
        };

        calLoggedDiveTimeTo = function () {
            var startTime = $("#LoggedDiveTimeFrom").val();
            if (startTime != null && startTime != "") {
                $("#LoggedDiveTimeTo").data('kendoDateTimePicker').min(startTime);
            }
            else {
                $("#LoggedDiveTimeTo").data('kendoDateTimePicker').min(self.minDate());

            }
            $("#LoggedDiveTimeTo").data('kendoDateTimePicker').max(self.maxDate());
        };

        calTimeLeftWorkshop = function () {
            $("#TimeLeftWorkshop").data('kendoDateTimePicker').min(self.minDate());
            $("#TimeLeftWorkshop").data('kendoDateTimePicker').max(self.maxDate());
            $("#TimeArrivedSite").val('');
        }

        calTimeArrivedSite = function () {
            var startTime = $("#TimeLeftWorkshop").val();
            if (startTime != null && startTime != "") {
                $("#TimeArrivedSite").data('kendoDateTimePicker').min(startTime);
            }
            else {
                $("#TimeArrivedSite").data('kendoDateTimePicker').min(self.minDate());
            }
            $("#TimeArrivedSite").data('kendoDateTimePicker').max(self.maxDate());
        };

        calTimeLeftSite = function () {
            $("#TimeLeftSite").data('kendoDateTimePicker').min(self.minDate());
            $("#TimeLeftSite").data('kendoDateTimePicker').max(self.maxDate());
            $("#TimeArrivedWorkshop").val('');
        };

        calTimeArrivedWorkshop = function () {
            var startTime = $("#TimeLeftSite").val();
            if (startTime != null && startTime != "") {
                $("#TimeArrivedWorkshop").data('kendoDateTimePicker').min(startTime);
            }
            else {
                $("#TimeArrivedWorkshop").data('kendoDateTimePicker').min(self.minDate());
            }

            $("#TimeArrivedWorkshop").data('kendoDateTimePicker').max(self.maxDate());
        };

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Delete
        //Purpose : Dynamically Remove the row from the Name of Drivers list
        self.RemoveAddDiver = function (divername) {
            self.divingTaskExecutionModel().DivingRequestDivers3.remove(divername);
        };

        //Action  : Button Delete 
        //Purpose : Dynamically Remove the row from the Name of Standby Drivers list
        self.RemoveStandByDiver = function (divername) {
            self.divingTaskExecutionModel().DivingRequestDivers2.remove(divername);
        };

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Delete for single row under Add Diver
        //Purpose : Dynamically Remove the row from the Add Diver
        self.RemoveAddDiverName = function (divername) {
            self.divingTaskExecutionModel().DivingRequestDivers1.remove(divername);
        };

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : Button Delete
        //Purpose : Dynamically Remove the row from the Onsite Hazard Identification Risk Assessment list
        self.RemoveHazard = function (hazard) {
            self.divingTaskExecutionModel().DivingCheckList().DivingCheckListHazard.remove(hazard);
        };

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Validate Only Numberic
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        }

        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function () {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Validate Alphabets and numerics With Spaces
        ValidateAlphaNumericWithSpaces = function () {
            return self.validationHelper.ValidateAlphaNumericWithSpaces(this, event);
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        // OnChange for Start Time
        ChangeStartTime = function () {
            $("#HoursOfOccupation2").val('');
            self.divingTaskExecutionModel().HoursOfOccupation2('');
            $("#spanStartTime").text('');

            if ($("#StartTime").val() != "") {

                if ($("#StopTime").val() != "") {
                    $("#StopTime").val('');
                }
                $("#StopTime").data('kendoDateTimePicker').min($("#StartTime").val());
                self.divingTaskExecutionModel().HoursOfOccupation2('');
            }
        }

        //Author  : Sandeep A
        //Date    : 5th September 2014
        //Action  : OnChange for Stop Time
        //Purpose : Calculate Total Number of Hours between Start Time and End Time
        ChangeStopTime = function (data, event) {
            $("#HoursOfOccupation2").val('');
            self.divingTaskExecutionModel().HoursOfOccupation2('');
            var startDateValue = self.divingTaskExecutionModel().StartTime();
            var dtStartDate = new Date(Date.parse(startDateValue));
            var endDateValue = data.sender._oldText;
            var dtEndDate = new Date(Date.parse(endDateValue));
            if (self.divingTaskExecutionModel().StartTime() == "" || self.divingTaskExecutionModel().StartTime() == null) {
                self.divingTaskExecutionModel().StopTime("");
                $("#StopTime").val('');
                $("#StartTime").focus();
                $("#spanStartTime").text('Please Select Start Time.');
            }
            else {
                if ((self.divingTaskExecutionModel().StartTime() != "") && (self.divingTaskExecutionModel().StartTime() != null)) {
                    if (dtStartDate > dtEndDate) {
                        $("#spanStartTime").text('End Time should be greater than Start Time.');
                    }
                    else {
                        var currentDate = new Date();
                        var tStartDate = dtStartDate.getMilliseconds();
                        var tEndDate = dtEndDate.getMilliseconds();

                        // calculating difference between start time and end time
                        var diff = dtEndDate - dtStartDate;
                        var msec = diff;

                        // converting milli sec to hours
                        var hh = Math.floor(msec / 1000 / 60 / 60);

                        //converting milli seconds to mints 
                        var mint = Math.floor(msec / 1000 / 60) - hh * 60;

                        // milli secs to secs
                        var ss = Math.floor(msec / 1000) - ((hh * 60 * 60) + (mint * 60));
                        var period;

                        // formting the time in HH:MM:SS
                        var hhh = "";
                        hhh = hh;
                        if (hh < 10) {
                            hhh = '0' + hh;
                        }
                        var mints = "";
                        mints = mint;
                        if (mint < 10) {
                            mints = '0' + mint;
                        }
                        var sss = "";
                        sss = ss;
                        if (ss < 10) {
                            sss = '0' + ss;
                        }
                        period = hhh + ':' + mints;
                        self.divingTaskExecutionModel().HoursOfOccupation2(period);
                    }
                }
                else {
                    self.divingTaskExecutionModel().HoursOfOccupation2('');
                    $("#spanStartTime").text('Please select Start Time.');
                }
            }
        }

        Validation = function () {
        };

        changeDiveReferenceNo = function () {
            if ($("#DiveReferenceNo").val() != "" && $("#DiveReferenceNo").val() != null) {
                $("#spanDiveReferenceNo").text('');
            }
            else {
                $("#spanDiveReferenceNo").text('Please enter Dive reference number.');
            }
        }

        changeDate = function () {
            if ($("#Date").val() != "") {
                $("#spanDate").text('');
            }
            else {
                $("#spanDate").text('Please select Date.');
            }
        }

        //Preventing Backspace
        PreventBackSpace = function (event) {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }

    IPMSRoot.DivingTaskExecutionViewModel = DivingTaskExecutionViewModel;

}(window.IPMSROOT));

function HandleProgressBarAndActiveTab(index) {
    var total = $('#ulTabs').find('li').length;
    var current = index + 1;
    $('li', $('#divFormWizardTabNavigation')).removeClass("done");
    var li_list = $('#ulTabs').find('li');
    for (var i = 0; i < index; i++) {
        $(li_list[i]).addClass("done");
    }
    for (var i = current; i < total; i++) {
        $(li_list[i]).removeClass("done");
        $(li_list[i]).removeClass("active");
    }
    $(li_list[index]).addClass("active");
    var $percent = (current / total) * 100;
    $('#divFormWizardTabNavigation').find('.progress-bar').css({
        width: $percent + '%'
    });
}

function GoToTab1(self, divingtaskexecutionData) {
    self.viewModeForTabs('taskexecution1');
    self.divingTaskExecutionModel().ViewModeForTabs('taskexecution1');
    var index = 0;
    HandleProgressBarAndActiveTab(index);
}

function GoToTab2(self, divingtaskexecutionData) {
    self.viewModeForTabs('taskexecution2');
    self.divingTaskExecutionModel().ViewModeForTabs('taskexecution');
    var index = 1;
    HandleProgressBarAndActiveTab(index);
}