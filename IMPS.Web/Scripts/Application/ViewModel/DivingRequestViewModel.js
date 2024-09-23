(function (IPMSRoot) {

    var DivingRequestViewModel = function (DivingRequestID, viewDetail) {

        var self = this;
        $('#spnTitle').html("Diving Request");
        $('#spnTitleOcp').html("Diving Request Occupation");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        //For Common Validation
        self.validationHelper = new IPMSRoot.validationHelper();

        self.viewMode = ko.observable();
        self.divingmodel = ko.observable(new IPMSROOT.DivingRequestModel());
        self.divingRequestModel = ko.observable();
        self.DivingRequestList = ko.observableArray();
        self.DivingRequestListOccupation = ko.observableArray();
        self.LocationList = ko.observableArray();
        self.OtherLocation = ko.observableArray();
        self.QuayLocation = ko.observableArray();
        self.LocationType = ko.observableArray();
        self.QuayList = ko.observableArray();
        self.BerthList = ko.observableArray();
        self.BollardList = ko.observableArray();

        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCancel = ko.observable(true);
        self.editableView = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.isQuayChanged = ko.observable(true);
        self.isBerthChanged = ko.observable(true);
        self.isOcupationFromDateMsg = ko.observable(false);
        self.isOcupationToDateMsg = ko.observable(false);
        self.IsOtherLocation = ko.observable(false);
        self.IsValid = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.IsAmend = ko.observable(false);

        self.radLocation = ko.observable();
        self.radQuayLocation = ko.observable();
        self.occupationFromDate = ko.observable();

        self.LoadRadioButtons = ko.observable();
        self.validationHelper = new IPMSROOT.validationHelper();

        self.divingRequestReasonsList = ko.observableArray();

        // To Intialize
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadDivingRequestOccupations();
            self.LoadDivingRequests();
            self.LoadDivingRequestReasons();

            if (viewDetail == true) {
                $('#spnTitle').html("View Diving Request");
                //$('#spnTitleOcp').html("Raise Diving Request Occupation");
                $('#spnTitleOcp').html("Diving Request Occupation Details");


                self.viewMode('Form');

                //  $("#divLocation").hide();
                //if (divingrequest.FromQuayCode() == "") {
                //    $("#divQuay").css('display', 'none');
                //}
                //else {

                //}
            }
            else {
                self.viewMode('List');
            }

            self.LoadRadioButtons();
            //self.divingRequestModel(new IPMSROOT.DivingRequestModel());
        }


        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.divingRequestModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        //Load Radio buttons
        self.LoadRadioButtons = function () {
            $("#OL").prop('checked', false);
            $("#QL").prop('checked', true);
            $('#divQuayLocation').css('display', 'block');
            $("#Otherlocation").prop('disabled', true);
            $("#divLocationddl").css('display', 'none');
        }

        // On click of quay location radio button
        self.radQuayLocation = function (event) {
            if ($("#QL").is(":checked")) {
                self.LoadQuays();
                $("#OL").prop('checked', false);
                $('#divQuayLocation').css('display', 'block');
                $("#Otherlocation").prop('disabled', false);

                //Added by Chandrima
                $("#divLocationddl").css('display', 'none');
                self.IsOtherLocation(false);
            }
        }

        // On click of other location radio button
        self.radLocation = function (event) {
            if ($("#OL").is(":checked")) {
                self.LoadOtherLocations();
                $("#QL").prop('checked', false);
                $('#divQuayLocation').css('display', 'none');
                self.isQuayChanged(false);
                self.isBerthChanged(false);
                $("#Otherlocation").prop('disabled', true);
                $("#divLocationddl").css('display', 'block');
                self.IsOtherLocation(true);
            }
        }

        self.LoadDivingRequestOccupations = function () {
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/GetDivingRequestOccupationById', { id: DivingRequestID }, function (result) {
                    //self.viewDivingRequestOccupation(new IPMSRoot.DivingRequestModel(result));
                    self.divingRequestModel(new IPMSRoot.DivingRequestModel(result));
                    self.raiseDivingRequestOccupation(self.divingRequestModel());

                }, null, null, false);
            }
            else {
                self.viewModelHelper.apiGet('api/GetAllDivingRequestOccupation', null, function (result) {
                    self.DivingRequestListOccupation(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.DivingRequestModel(item);
                    }));
                }, null, null, false);
            }
        }

        // Load All Diving Requests Data
        self.LoadDivingRequests = function () {
            self.viewModelHelper.apiGet('api/DivingRequestDetails', null, function (result) {
                self.DivingRequestList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.DivingRequestModel(item);
                }));
            }, null, null, false);
        }

        // Get Other Locations Data
        self.LoadOtherLocations = function () {
            self.viewModelHelper.apiGet('api/GetOtherLocations', null, function (result) {
                ko.mapping.fromJS(result, {}, self.LocationList);
            }, null, null, false);
        }

        // Get Quays Data
        self.LoadQuays = function () {
            self.LocationList("");
            self.viewModelHelper.apiGet('api/GetQuaysForDivingRequest', null, function (result) {
                ko.mapping.fromJS(result, {}, self.QuayList);
            }, null, null, false);
        }

        // Get Berths Data
        self.LoadBerths = function (event) {
            self.BerthList("");
            if (event.FromQuayCode() == undefined) {
                self.BerthList({ BerthCode: 0, BerthName: '' });
                self.isQuayChanged(false);
            }
            else {
                self.isQuayChanged(true);
            }

            self.viewModelHelper.apiGet('api/GetBerthsForDivingRequest/' + event.FromQuayCode(), { QuayCode: event.FromQuayCode() }, function (result) {
                ko.mapping.fromJS(result, {}, self.BerthList);
            });

            if ($("#location").val() == "" || $("#location").val() == null) {
                $('#Qloc').text('Please select quay location.');
            }
            else {
                $("#Qloc").text('');
            }
        }

        // Get Bollards Data
        self.LoadBollards = function (event) {
            self.BollardList("");

            if (event == undefined) {
                self.BollardList({ BollardCode: 0, BollardName: '' });
                self.isBerthChanged(false);
            }
            else {
                self.isBerthChanged(true);
            }

            self.viewModelHelper.apiGet('api/GetBollardsForDivingRequest/' + event.FromQuayCode() + event.FromBerthCode(),
               { QuayCode: event.FromQuayCode(), BerthCode: event.FromBerthCode() }, function (result) {
                   ko.mapping.fromJS(result, {}, self.BollardList);
               });

            if ($("#berthno").val() == "" || $("#berthno").val() == null) {
                $('#QlocBerth').text('Please select berth name.');
            }
            else {
                $("#QlocBerth").text('');
            }
        }

        ValidationReset = function () {
            $('#Qloc').text('');
            $('#QlocBerth').text('');
            $('#QlocFromBol').text('');
            $('#QlocToBol').text('');
            $('#Oloc').text('');
        }

        //OtherLocationCode Change
        ChangeOtherLocationCode = function () {
            if ($("#Otherlocation").val() == "" || $("#Otherlocation").val() == null) {
                $('#Oloc').text('Please select other location.');
            }
            else {
                $("#Oloc").text('');
            }
        }

        //BollardFromCode Change
        ChangeBollardfromCode = function () {
            if ($("#frombollard").val() == "" || $("#frombollard").val() == null) {
                $('#QlocFromBol').text('Please select from bollard.');
            }
            else {
                $("#QlocFromBol").text('');
            }
        }

        //BollardtoCode Change
        ChangeBollardtoCode = function () {
            if ($("#tobollard").val() == "" || $("#tobollard").val() == null) {
                $('#QlocToBol').text('Please select To Bollard');
            }
            else {
                $("#QlocToBol").text('');
            }
        }

        Validation = function () {
            $('#Qloc').text('');
            $('#QlocBerth').text('');
            $('#QlocFromBol').text('');
            $('#QlocToBol').text('');
            $('#Oloc').text('');
            if (divingrequest.LocationType() == "Q") {
                if ($("#location").val() == "" || $("#location").val() == null) {
                    $('#Qloc').text('Please select quay location.');
                    errors++;
                }
                if ($("#berthno").val() == "" || $("#berthno").val() == null) {
                    $('#QlocBerth').text('Please select berth name.');
                    errors++;
                }
                if ($("#frombollard").val() == "" || $("#frombollard").val() == null) {
                    $('#QlocFromBol').text('Please select From Bollard.');
                    errors++;
                }
                if ($("#tobollard").val() == "" || $("#tobollard").val() == null) {
                    $('#QlocToBol').text('Please select To Bollard.');
                    errors++;
                }
            }
            else {
                if ($("#Otherlocation").val() == "" || $("#Otherlocation").val() == null) {
                    $('#Oloc').text('Please select other location.');
                    errors++;
                }
            }
        }

        // Save Diving Request 
        self.SaveDivingRequest = function (divingrequest) {
            $('#Qloc').text('');
            $('#QlocBerth').text('');
            $('#QlocFromBol').text('');
            $('#QlocToBol').text('');
            $('#Oloc').text('');
            divingrequest.validationEnabled(true);

            self.DivingRequestValidation = ko.observable(divingrequest);

            self.DivingRequestValidation().errors = ko.validation.group(self.DivingRequestValidation());
            var errors = self.DivingRequestValidation().errors().length;

            if ($("#OccupationReason").val() == "" || $("#OccupationReason").val() == null) {
                $('#QlReason').text('Please select Reason.');
                errors++;
            }

            if (divingrequest.LocationType() == "Q") {
                if ($("#location").val() == "" || $("#location").val() == null) {
                    $('#Qloc').text('Please select Quay location.');
                    errors++;
                }
                if ($("#berthno").val() == "" || $("#berthno").val() == null) {
                    $('#QlocBerth').text('Please select Berth Name.');
                    errors++;
                }
                if ($("#frombollard").val() == "" || $("#frombollard").val() == null) {
                    $('#QlocFromBol').text('Please select From Bollard.');
                    errors++;
                }
                if ($("#tobollard").val() == "" || $("#tobollard").val() == null) {
                    $('#QlocToBol').text('Please select To Bollard.');
                    errors++;
                }
            }
            else {
                if ($("#Otherlocation").val() == "" || $("#Otherlocation").val() == null) {
                    $('#Oloc').text('Please select Other location.');
                    errors++;
                }
            }
            if (errors == 0) {
                self.viewModelHelper.apiPost('api/DivingRequest', ko.mapping.toJSON(divingrequest), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Diving request details saved successfully.", "Diving Request");
                    self.LoadDivingRequests();
                    self.viewMode('List');
                    $('#spnTitle').html("Diving Request");
                });
            }
            else {
                self.DivingRequestValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // To Add New Diving Request
        self.addDivingRequest = function () {
            self.divingmodel(new IPMSROOT.DivingRequestModel());
            self.viewMode('Form');

            self.IsSave(true);
            self.IsReset(true);
            self.IsCancel(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.isQuayChanged(false);
            self.isBerthChanged(false);
            self.LoadRadioButtons();
            self.LoadQuays();
            $('#spnTitle').html("Add Diving Request");
        }

        // Adds diving Request Occupation
        self.addDivingRequestOccupation = function () {
            self.divingmodel(new IPMSROOT.DivingRequestModel());
            self.viewMode('Form');
            self.LoadQuays();

            self.IsSave(true);
            self.IsReset(true);
            self.IsCancel(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.isQuayChanged(false);
            self.isBerthChanged(false);
            self.IsAmend(false);
            self.LoadRadioButtons();
            $('#spnTitleOcp').html("Raise Diving Request Occupation");
        }

        // Raise Diving Request Occupation Link
        self.raiseDivingRequestOccupation = function (divingrequest) {
          
            var date = new Date();
            var requiredbydate = new Date(divingrequest.RequiredByDate());
            requiredbydate.setHours(date.getHours());
            requiredbydate.setMinutes(date.getMinutes());           
            self.occupationFromDate(moment(requiredbydate).format('YYYY-MM-DD HH:mm tt'));
            //self.occupationFromDate(moment(divingrequest.RequiredByDate()).format('YYYY-MM-DD HH:mm'));
            //var data = { FromQuayCode: ko.observable(divingrequest.FromQuayCode()), FromBerthCode: ko.observable(divingrequest.FromBerthCode()) };
            self.viewMode('Form');

            self.IsReset(true);
            self.IsCodeEnable(true);
            //self.editableView(true);
            //self.isQuayChanged(false);
            //self.isBerthChanged(false);
            //self.QuayList([{ QuayCode: divingrequest.FromQuayCode(), QuayName: divingrequest.FromQuayName() }]);
            //self.BerthList([{ BerthCode: divingrequest.FromBerthCode(), BerthName: divingrequest.FromBerthName() }]);

            //self.LoadBollards(data);
            self.divingmodel(divingrequest);
            self.divingRequestModel(divingrequest);

            //if (divingrequest.FromQuayCode() == "") {
            //    $("#divQuay").css('display', 'none');
            //}
            //else {
            //    $("#divLocation").css('display', 'none');
            //}

            //$("#frombollard").prop('disabled', true);
            //$("#OL").prop('checked', false);
            //$("#QL").prop('checked', true);
            //$("#OL").prop('disabled', true);
            //$("#QL").prop('disabled', true);
            //$('#divQuayLocation').css('display', 'block');
            $('#spnTitleOcp').html("Raise Diving Request Occupation");

            if (viewDetail == true) {
                //viewDetail = false;
                //self.LoadDivingRequestOccupations();
               
                self.IsAmend(true);
                self.IsSave(false);
                var ReferenceID = divingrequest.DivingRequestID();
                var WorkflowInstanceID = divingrequest.WorkflowInstanceID();//11424; 
                self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                       null,
                             function (result) {

                                 ko.utils.arrayForEach(result, function (val) {
                                     var pendingtaskaction = new IPMSROOT.pendingTask();
                                     pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                     pendingtaskaction.ReferenceID(val.ReferenceID);
                                     pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                     pendingtaskaction.APIUrl(val.APIUrl);
                                     pendingtaskaction.TaskName(val.TaskName);
                                     pendingtaskaction.TaskDescription(val.TaskDescription);
                                     pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                     pendingtaskaction.HasRemarks(val.HasRemarks);
                                     divingrequest.pendingTasks.push(pendingtaskaction);
                                 });
                             });
            }
            else {
                self.IsSave(true);
                self.IsAmend(false);
            }
        }

        // View Diving Request Occupation 
        self.viewDivingRequestOccupation = function (divingrequest) {
            //var data = { FromQuayCode: ko.observable(divingrequest.FromQuayCode()), FromBerthCode: ko.observable(divingrequest.FromBerthCode()) };
            self.viewMode('Form');
            self.IsSave(false);
            self.IsAmend(false);
            self.IsReset(false);
            self.IsCodeEnable(false);
            //self.editableView(false);
            //self.isQuayChanged(false);
            //self.isBerthChanged(false);
            //self.QuayList([{ QuayCode: divingrequest.FromQuayCode(), QuayName: divingrequest.FromQuayName() }]);
            //self.BerthList([{ BerthCode: divingrequest.FromBerthCode(), BerthName: divingrequest.FromBerthName() }]);

            // Loads Bollards
            //self.LoadBollards(data);
            self.divingmodel(divingrequest);
            //$("#frombollard").prop('disabled', true);

            //if (divingrequest.FromQuayCode() == "") {
            //    $("#divQuay").css('display', 'none');
            //}
            //else {
            //    $("#divLocation").css('display', 'none');
            //}

            $('#spnTitleOcp').html("View Diving Request Occupation");
            $("#OcupationToDate").data('kendoDateTimePicker').enable(false);
            $("#OcupationFromDate").data('kendoDateTimePicker').enable(false);

            if (viewDetail == true) {
                viewDetail = false;
                self.LoadDivingRequestOccupations();
               
                var ReferenceID = divingrequest.DivingRequestID();
                var WorkflowInstanceID = divingrequest.WorkflowInstanceID();//11424; 
                self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                       null,
                             function (result) {

                                 ko.utils.arrayForEach(result, function (val) {
                                     var pendingtaskaction = new IPMSROOT.pendingTask();
                                     pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                     pendingtaskaction.ReferenceID(val.ReferenceID);
                                     pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                     pendingtaskaction.APIUrl(val.APIUrl);
                                     pendingtaskaction.TaskName(val.TaskName);
                                     pendingtaskaction.HasRemarks(val.HasRemarks);
                                     divingrequest.pendingTasks.push(pendingtaskaction);
                                 });
                             });
            }
        }

        // Reset the Diving Request Occupation form
        self.ResetOccupation = function (divingrequest) {

            ko.validation.reset();
            self.divingmodel().reset();
            self.DivingRequestValidation = ko.observable(divingrequest);
            self.DivingRequestValidation().errors = ko.validation.group(self.DivingRequestValidation());
            self.DivingRequestValidation().errors.showAllMessages(false);
            //self.IsValid(true);
            //self.IsUnique(true);

            //if (divingrequest.FromQuayCode() == "") {
            //    $("#divQuay").css('display', 'none');
            //}
            //else {
            //    $("#divLocation").css('display', 'none');
            //}

        }

        // save diving request occupation
        self.SaveDivingRequestOccupation = function (divingrequest) {
            self.DivingRequestValidation = ko.observable(divingrequest);
            self.DivingRequestValidation().errors = ko.validation.group(self.DivingRequestValidation());

            var errors = self.DivingRequestValidation().errors().length;
            var errors1 = 0;

            if ((self.divingRequestModel().OcupationFromDate() != "") && (self.divingRequestModel().OcupationFromDate() != null)) {
                if ((self.divingRequestModel().OcupationToDate() != "") && (self.divingRequestModel().OcupationToDate() != null)) {
                    self.isOcupationToDateMsg(false);
                    var dtOcupationFromDate = new Date(Date.parse(self.divingRequestModel().OcupationFromDate()));
                    var dtOcupationToDate = new Date(Date.parse(self.divingRequestModel().OcupationToDate()));
                    if (dtOcupationFromDate >= dtOcupationToDate) {
                        errors1 = errors1 + 1;
                        $("#isOcupationToDateMsg").text('Occupation To Date/Time should be greater than Occupation From Date/Time.');
                        self.isOcupationToDateMsg(true);
                    }
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isOcupationToDateMsg").text('Please select Occupation To Date/Time.');
                    self.isOcupationToDateMsg(true);
                }
            }
            else {
                errors1 = errors1 + 1;
                $("#isOcupationFromDateMsg").text('Please select Occupation From Date/Time.');
                self.isOcupationFromDateMsg(true);
            }

            if (errors1 == 0) {                
                self.viewModelHelper.apiPost('api/ModifyDivingRequestOccupation', ko.mapping.toJSON(divingrequest), function Message(data) {
                    if (data.DivingRequestID > 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";

                        self.LoadDivingRequestOccupations();
                        if (viewDetail == false) {
                            self.viewMode('List');
                            $('#spnTitleOcp').html("Diving Request Occupation");
                            toastr.success("Diving request occupation details saved successfully.", "Diving Request Occupation");
                            //Html.partial("~/Views/Shared/_IPMSFooter.cshtml");
                            //TODO:need to check the alternative to reload the footer once saved or updated
                            var chat = $.connection.chatHub;

                            // Declare a function on the job hub so the server can invoke it
                            chat.client.BrodcastNews = function () {
                                getData();
                                getDivingData();
                            };

                            // Start the connection
                            //  $.connection.hub.start();
                            getData();
                            getDivingData();
                           
                        }
                        else {
                            toastr.success("Diving request occupation details amended successfully.", "Diving Request Occupation");
                        }
                    }
                });
            }
            else {
                self.VesselArrestImmobilizationSAMSAStopValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        function getData() {
            $('#divNews').hide();
            var $tbl = $('#tblJobInfo');
            $.ajax({
                url: '../api/NewsScroll',
                type: 'GET',
                datatype: 'json',
                success: function (data) {
                    if (data.length > 0) {
                        $("#divNews").show();
                        $tbl.empty();
                        var rows = [];
                        for (var i = 0; i < data.length; i++) {
                            rows.push(' <tr><td><a href="http://' + data[i].NewsUrl + '" target="_blank" style="color:#00ff00;">' + $('<div/>').text(data[i].NewsContent).html() + '</a></td></tr>');
                        }
                        $tbl.append('<marquee scrolldelay="150" onmouseover="this.stop();" onmouseout="this.start();">' + rows.join('&nbsp;&nbsp;&nbsp;&nbsp;') + '</marquee>');

                    }
                }
            });
        }
        function getDivingData() {

            $('#divDivingRequest').hide();

            var $tbl = $('#tblDivReq');
            $.ajax({

                url: '../api/DivingRequestsForScroll',
                type: 'GET',
                datatype: 'json',
                success: function (data) {
                    if (data.length > 0) {
                        $("#divDivingRequest").show();
                        $tbl.empty();
                        var rows = [];
                        for (var i = 0; i < data.length; i++) {
                            rows.push(' <tr><td> Diving Activity At ' + data[i].LocationorQuay + ' Will Be Stop By  ' + data[i].OcupationToDate + '</td></tr> ');
                        }
                        $tbl.append('<marquee scrolldelay="150" onmouseover="this.stop();" onmouseout="this.start();">' + rows.join('&nbsp;&nbsp;&nbsp;&nbsp;') + '</marquee>');

                    }
                }
            });
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

        //Calculating PeriodofOccupation based on start and end date of immobilazation
        CalcPeriodofOccupation = function (data, event) {
            var startDateValue = self.divingRequestModel().OcupationFromDate();
            var dtOcupationFromDate = new Date(Date.parse(moment(startDateValue)));
            var endDateValue = data.sender._oldText;
            var dtOcupationToDate = new Date(Date.parse(moment(endDateValue)));

            if ((self.divingRequestModel().OcupationFromDate() != "") && (self.divingRequestModel().OcupationFromDate() != null)) {
                self.isOcupationToDateMsg(false);
                self.isOcupationFromDateMsg(false);

                if (dtOcupationFromDate >= dtOcupationToDate) {
                    $("#isOcupationToDateMsg").text('Occupation To Date/Time should be greater than Occupation From Date/Time.');
                    self.divingRequestModel().OcupationToDate('');
                    self.divingRequestModel().HoursOfOccupation1('');
                    self.isOcupationToDateMsg(true);
                }
                else {
                    var currentDate = new Date();
                    var tOcupationFromDate = dtOcupationFromDate.getMilliseconds();

                    var tOcupationToDate = dtOcupationToDate.getMilliseconds();

                    // calculating differec time b/w start and end time of immobilazation
                    self.isOcupationToDateMsg(false);
                    var diff = dtOcupationToDate - dtOcupationFromDate;
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

                    period = hhh + '.' + mints;
                    if (period == '00.00') {
                        $("#isOcupationToDateMsg").text('Occupation To Date/Time should be greater than Occupation From Date/Time.');                        
                        self.isOcupationToDateMsg(true);
                    }
                    else {
                        self.divingRequestModel().HoursOfOccupation1(period);
                    }
                }
            }
            else {
                $("#isOcupationFromDateMsg").text('Please select Occupation from Date/Time.');
                self.isOcupationFromDateMsg(true);
            }
        }

        self.CancelOccupation = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.divingmodel().reset();
                $('#spnTitleOcp').html("Diving Request Occupation");
                self.divingmodel().pendingTasks.removeAll();
            }
        }

        // view Diving Request
        self.viewDivingRequest = function (divingrequest) {
            var data = { FromQuayCode: ko.observable(divingrequest.FromQuayCode()), FromBerthCode: ko.observable(divingrequest.FromBerthCode()), FromBollardCode: ko.observable(divingrequest.FromBollardCode()) };
            self.viewMode('Form');
            self.IsSave(false);
            self.IsReset(false);
            self.IsCodeEnable(false);
            self.editableView(false);

            self.QuayList([{ QuayCode: divingrequest.FromQuayCode(), QuayName: divingrequest.FromQuayName() }]);
            self.BerthList([{ BerthCode: divingrequest.FromBerthCode(), BerthName: divingrequest.FromBerthName() }]);
            self.LoadBollards(data);
            self.divingmodel(divingrequest);
            var ff = divingrequest.RequiredByDate();
            $("#RequiredByDate").val(ff);

            if (divingrequest.LocationType() == "Quay Location") {
                self.LoadQuays();
                $("#OL").prop('checked', false);
                $("#QL").prop('checked', true);
                $("#OL").prop('disabled', true);
                $("#QL").prop('disabled', true);
                $('#divQuayLocation').css('display', 'block');

                //chandrima
                $('#divLocationddl').css('display', 'none');
                $("#Otherlocation").prop('disabled', false);
                self.isQuayChanged(false);
                self.isBerthChanged(false);
                self.IsOtherLocation(false);
                Validation();
            }
            else {
                self.LoadOtherLocations();
                $("#QL").prop('checked', false);
                $("#OL").prop('checked', true);
                $("#OL").prop('disabled', true);
                $("#QL").prop('disabled', true);
                $('#divQuayLocation').css('display', 'none');
                $("#Otherlocation").prop('disabled', true);
                self.IsOtherLocation(false);
                Validation();
            }

            $('#spnTitle').html("View Diving Request");
        }

        // Reset of Diving Request
        self.Reset = function (divingrequest) {
            ko.validation.reset();
            self.divingmodel().reset();
            ko.validation.reset();
            self.divingmodel().reset();
            ValidationReset();
            self.DivingRequestValidation = ko.observable(divingrequest);
            self.DivingRequestValidation().errors = ko.validation.group(self.DivingRequestValidation());
            self.DivingRequestValidation().errors.showAllMessages(false);
            self.IsValid(true);
            self.IsUnique(true);

            //chandrima
            $('#divLocationddl').css('display', 'none');
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            if ($("#QL").is(":checked")) {
                $('#divQuayLocation').css('display', 'block');
                self.IsOtherLocation(false);
            }

            $("#QlReason").text('');
            self.isQuayChanged(false);
            self.isBerthChanged(false);
        }

        // Cancel Diving Request
        self.Cancel = function () {
            self.viewMode('List');
            self.divingmodel().reset();
            $('#spnTitle').html("Diving Request");
        }

        //Author : Omprakash
        //Reason : Preventing Backspace 
        //Dated  : 2nd September 2014
        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.LoadDivingRequestReasons = function () {
           
            self.viewModelHelper.apiGet('api/GetDivingRequestReasons', null,
                function (result) {
                    //self.divingRequestReasonsList(ko.utils.arrayMap(result, function (item) {
                    //    return new IPMSRoot.Reasons(item);
                    //}));
                    ko.mapping.fromJS(result, {}, self.divingRequestReasonsList);
                }, null, null, false);
        }

        //OccupationReason Change
        ChangeReason = function () {
          
            if ($("#OccupationReason").val() == "" || $("#OccupationReason").val() == null) {
                $('#QlReason').text('Please select Reason.');
            }
            else {
                $("#QlReason").text('');
            }
        }

        self.OccupationMaxDate = function () {
            this.min(self.occupationFromDate());
        }



        self.viewWorkFlow = function (divingmodel) {
            var workflowinstanceId = divingmodel.WorkflowInstanceID();
            if (workflowinstanceId == "") {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack1').modal('show');
            }
            else {
                self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {
                      self.divingmodel(new IPMSROOT.DivingRequestModel());
                      self.divingmodel().WorkFlowRemarks(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack1').modal('show');
                  });
            }
        }

        self.Initialize();


    }
    IPMSRoot.DivingRequestViewModel = DivingRequestViewModel;

}(window.IPMSROOT));
