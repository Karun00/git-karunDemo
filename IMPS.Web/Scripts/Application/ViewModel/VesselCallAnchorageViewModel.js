(function (IPMSRoot) {
    var VesselCallAnchorageViewModel = function (vcn, viewDetail) {
        //declare variables for knockout to maintain for MVVM pattern
        var self = this;
        $("#VesselCallAnchorageTitle").text("Capture Arrival/Departure");
        self.viewModelHelper = new IPMSRoot.viewModelHelper();
        self.VesselCallAnchorageList = ko.observableArray();
        self.vesselCallAnchorageModel = ko.observable(new IPMSROOT.VesselCallAnchorageModel());
        self.vesselCallReason = ko.observable();
        self.viewMode = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsEditable = ko.observable(true);
        self.IsView = ko.observable(true);
        self.IsAnchorDropTimeValid = ko.observable(false);
        self.IsATAValid = ko.observable(false);
        self.IsATDValid = ko.observable(false);
        self.IsPortInValid = ko.observable(false);
        self.IsPortOutValid = ko.observable(false);
        self.IsBreakInValid = ko.observable(false);
        self.IsBreakOutValid = ko.observable(false);
        self.IsRemarks = ko.observable(false);
        self.istablenable = ko.observable(true);
        self.IsValid = ko.observable(false);
        self.validationHelper = new IPMSROOT.validationHelper();
        self.vcnlocal  = ko.observable();
        self.Config = ko.observable();
        self.IsGridEditable = ko.observable(true);
        
        self.isspanVCNSearchValid = ko.observable(false);
        self.isspanVesselSearchValid = ko.observable(false);
        self.isPortDateValid = ko.observable(false);
        self.isVcnCloseView = ko.observable(false);
        self.vcnNameOnClose = ko.observable();
        self.ImoNoOnVcnClose = ko.observable();
        self.CallsignOnVcnClose = ko.observable();
        self.vcnCloseMsg = ko.observable(false);
        //Initial Value at load time
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadReasons();
            self.LoadVesselCallAnchorageList();
            self.viewMode('List');
            self.GetConfigurationValue();
        }

        //Onload Get Anchorage Recording
        self.LoadVesselCallAnchorageList = function () {
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/VesselCallAnchorages/{vcn?}',
                 { vcn: vcn },
                  function (result) {
                      self.VesselCallAnchorageList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.VesselCallAnchorageModel(item);
                      }));
                      self.ViewVesselCallAnchorage(self.VesselCallAnchorageList()[0]);
                  });
            }
            else {

                var etaFrom = moment(self.vesselCallAnchorageModel().ETAFrom()).format('YYYY-MM-DD');
                var etaTo = moment(self.vesselCallAnchorageModel().ETATo()).format('YYYY-MM-DD');
                var vcn = self.vesselCallAnchorageModel().VCNSearch() != undefined && self.vesselCallAnchorageModel().VCNSearch() != '' ? self.vesselCallAnchorageModel().VCNSearch() : 'ALL';
                var vesselName = self.vesselCallAnchorageModel().VesselNameSearch() != undefined && self.vesselCallAnchorageModel().VesselNameSearch() != '' ? self.vesselCallAnchorageModel().VesselNameSearch() : 'ALL';

                self.viewModelHelper.apiGet('api/VesselCallAnchorages/' + vcn + '/' + vesselName + '/' + etaFrom + '/' + etaTo, null,
                function (result) {
                    self.VesselCallAnchorageList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.VesselCallAnchorageModel(item);
                    }));
                });
            }
        }


        self.GetConfigurationValue = function()
        {

            self.viewModelHelper.apiGet('api/VesselCallAnchorage/GetGeneralConfigs', null,
              function (result) {
                  self.Config(result);
              }, null, null, false);

        }

        

        //Dynamically filling the Reason's Dropdown in Anchorage details list
        self.LoadReasons = function () {
            self.viewModelHelper.apiGet('api/VesselCallAnchorage/GetReasons', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.vesselCallReason(new IPMSRoot.VesselCallReason(result1));
                    }, null, null, false);
        }

        ////---- Button Click Start -------////
        // Button Add New +
        //Dynamically Creating  New Row for Capture Arrival/Departure List
        self.AddNewAnchorage = function (anchoragedata) {
            var databaseList = ko.toJSON(self.vesselCallAnchorageModel().VesselCallAnchorages);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var errors = 0;
            $.each(jsonObj, function (index, value) {
                var andt = value.AnchorDropTime;
                var strAnchorDropTime = "";
                var strAnchorAweighTime = "";
                if ((value.AnchorDropTime != "") && (value.AnchorDropTime != undefined)) {
                    strAnchorDropTime = "SSS";
                }
                if ((value.AnchorAweighTime != "") && (value.AnchorAweighTime != undefined)) {
                    strAnchorAweighTime = "SSS";
                }
                rid = rid + 1;
                if (strAnchorDropTime == "") {
                    errors = errors + 1;
                    $('#divValidationError1').removeClass('display-none');
                    $('#spanValidationError1').text('Anchorage Details Grid Error : Please select Anchor Drop Time at Row Number : ' + rid);
                    self.IsValid(false);
                    return;
                }
                if (strAnchorDropTime == "" && strAnchorAweighTime != "") {
                    errors = errors + 1;
                    $('#divValidationError1').removeClass('display-none');
                    $('#spanValidationError1').text('Anchorage Details Grid Error : Please select Anchor Drop Time at Row Number : ' + rid);
                    self.IsValid(false);
                    return;
                }
                if (strAnchorDropTime != "" && strAnchorAweighTime != "") {
                    var newAnchorDropTime = new Date(Date.parse(value.AnchorDropTime));
                    var newAnchorAweighTime = new Date(Date.parse(value.AnchorAweighTime));
                    var currentDate = new Date();
                    if (newAnchorAweighTime < newAnchorDropTime) {
                        errors = errors + 1;
                        $('#divValidationError1').removeClass('display-none');
                        $('#spanValidationError1').text('Anchorage Details Grid Error : Anchor Drop Time should be less than Anchor Aweigh Time at Row Number : ' + rid);
                        self.IsValid(false);
                        return;
                    }
                    else {

                        self.IsValid(true);
                    }
                }
                return;
            });
            if (errors == 0) {
                self.vesselCallAnchorageModel().VesselCallAnchorages.push(new IPMSROOT.VesselCallAnchorage());
            }
        }




        //Button Update
        //Modifying Capture Arrival/Departure Data to Database
        self.ModifyVesselCallAnchorage = function (model) {
            self.VesselCallAnchorageValidation = ko.observable(model);
            self.VesselCallAnchorageValidation().errors = ko.validation.group(self.VesselCallAnchorageValidation);
            var errors = self.VesselCallAnchorageValidation.errors().length;
            var errors1 = 0;
            var strATA = "";
            var strATD = "";
            self.IsATAValid(false);
            if ((model.ATD() != "") && (model.ATD() != null)) {
                strATD = "SSS";
            }
            if ((model.ATA() != "") && (model.ATA() != null)) {
                strATA = "SSS";
            }
            if (strATA == "" && strATD != "") {
                self.IsATAValid(true);
                self.IsValid(false);
                errors1 = errors1 + 1;
                $("#spanATA").text('Please select ATA');
                return;
            }
            else {
                self.IsATAValid(false);
                self.IsValid(true);
            }
            if (strATA != "" && strATD != "") {
                var newata = new Date(Date.parse(model.ATA()));
                var newatd = new Date(Date.parse(model.ATD()));
                var currentDate = new Date();

                if (newatd < newata) {
                    errors1 = errors1 + 1;
                    $("#spanATA").text('ATA should be less than ATD');
                    self.IsATAValid(true);
                    self.IsValid(false);
                    return;
                }
                else {
                    self.IsATAValid(false);
                    self.IsATDValid(false);
                    self.IsValid(true);
                }
            }
            if (self.isPortDateValid() == true)
                errors1 = errors1 + 1;
            var databaseList = ko.toJSON(self.vesselCallAnchorageModel().VesselCallAnchorages);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            $.each(jsonObj, function (index, value) {

                if (model.PortLimitIn() == "Invalid date") {
                    model.PortLimitIn("");
                }
                if (model.PortLimitOut() == "Invalid date") {
                    model.PortLimitOut("");
                }
                if (model.BreakWaterIn() == "Invalid date") {
                    model.BreakWaterIn("");
                }
                if (model.BreakWaterOut() == "Invalid date") {
                    model.BreakWaterOut("");
                }
                var andt = value.AnchorDropTime;
                var strAnchorDropTime = "";
                var strAnchorAweighTime = "";
                if ((value.AnchorDropTime != "") && (value.AnchorDropTime != undefined)) {
                    strAnchorDropTime = "SSS";
                }
                if ((value.AnchorAweighTime != "") && (value.AnchorAweighTime != undefined)) {
                    strAnchorAweighTime = "SSS";
                }
                rid = rid + 1;
                if (strAnchorDropTime == "") {
                    errors1 = errors1 + 1;
                    $('#divValidationError1').removeClass('display-none');
                    $('#spanValidationError1').text('Anchorage Details Grid Error : Please select Anchor Drop Time at Row Number : ' + rid);
                    self.IsValid(false);
                    return;
                }
                if (strAnchorDropTime == "" && strAnchorAweighTime != "") {
                    errors1 = errors1 + 1;
                    $('#divValidationError1').removeClass('display-none');
                    $('#spanValidationError1').text('Anchorage Details Grid Error : Please select Anchor Drop Time at Row Number : ' + rid);
                    self.IsValid(false);
                    return;
                }
                if (strAnchorDropTime != "" && strAnchorAweighTime != "") {
                    var newAnchorDropTime = new Date(Date.parse(value.AnchorDropTime));
                    var newAnchorAweighTime = new Date(Date.parse(value.AnchorAweighTime));
                    var currentDate = new Date();
                    if (newAnchorAweighTime < newAnchorDropTime) {
                        errors1 = errors1 + 1;
                        $('#divValidationError1').removeClass('display-none');
                        $('#spanValidationError1').text('Anchorage Details Grid Error : Anchor Drop Time should be less than Anchor Aweigh Time at Row Number : ' + rid);
                        self.IsValid(false);
                        return;
                    }
                    else {
                        self.IsValid(true);
                    }
                }
                return;
            });
            if (errors1 > 0) {
                return;
            }
            else {
                if (errors == 0) {
                    self.viewModelHelper.apiPut('api/VesselCallAnchorages', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            if (self.vcnCloseMsg() == true)
                                toastr.success("VCN Closed successfully");
                            else
                                toastr.success("Capture Arrival/Departure updated successfully", "Capture Arrival/Departure")
                            self.vcnCloseMsg(false);
                            self.viewMode('List');
                            $("#VesselCallAnchorageTitle").text("Capture Arrival/Departure");
                            self.vesselCallAnchorageModel(new IPMSROOT.VesselCallAnchorageModel());
                            self.LoadReasons();
                            self.LoadVesselCallAnchorageList();
                        });
                }
                else {
                    self.VesselCallAnchorageValidation().errors.showAllMessages();
                    $('#divValidationError').removeClass('display-none');
                    return;
                }
            }
        }

        //Button Reset
        //Reset Capture Arrival/Departure saved data
        self.ResetVesselCallAnchorage = function (model) {
            ko.validation.reset();
            self.vesselCallAnchorageModel().reset();
            if (self.VesselCallAnchorageValidation != undefined && self.VesselCallAnchorageValidation() != undefined && self.VesselCallAnchorageValidation() != "" && self.VesselCallAnchorageValidation().errors.showAllMessages(false) != undefined) {
                self.VesselCallAnchorageValidation().errors.showAllMessages(false) ? self.VesselCallAnchorageValidation().errors.showAllMessages(false) : "";
            }
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            if ($('#divValidationError1').is(':visible')) {
                $('#divValidationError1').css('display', 'none');
            }
            self.IsATAValid(false);
            self.IsATDValid(false);
            self.IsPortInValid(false);
            self.IsPortOutValid(false);
            self.IsBreakInValid(false);
            self.IsBreakOutValid(false);

        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }



        //Button Cancel
        //Cancel Capture Arrival/Departure  details and send back to summary of Capture Arrival/Departure
        self.Cancel = function () {
            self.vcnCloseMsg(false);
            if (viewDetail == true) {
                window.location.href = '/VoyageMonitoring/ManageVoyageMonitoring/' + self.vcnlocal;
            }
            else {
                self.vesselCallAnchorageModel().reset();
                self.viewMode("List");
                $("#VesselCallAnchorageTitle").text("Capture Arrival/Departure");
            }

        }
        ////---- Button Click Ends -------////

        //Dynamically Removing the Capture Arrival/Departure details from the list
        self.removeAnchor = function (anchoragedata) {
            self.vesselCallAnchorageModel().VesselCallAnchorages.remove(anchoragedata);
        }

        //Editing Saved details of Capture Arrival/Departure Data
        self.EditVesselCallAnchorage = function (model) {
            $("#VesselCallAnchorageTitle").text("Edit Capture Arrival/Departure");
            self.IsUpdate(true);
            self.IsReset(true);
            self.viewMode('Form');
            self.vesselCallAnchorageModel(model);
            self.IsEditable(true);
            self.IsGridEditable(true);
            self.istablenable(true);
            if (self.isVcnCloseView() == true) {
                $("#VesselCallAnchorageTitle").text("VCN Closure");
                $("#VcnClose").hide();
                $("#VesselName").val(self.vcnNameOnClose);
                $("#IMONumber").val(self.ImoNoOnVcnClose);
                $("#CallSign").val(self.CallsignOnVcnClose);
                $("#Update").text("VCN Close");
                self.vcnCloseMsg(true);
            }
            self.isVcnCloseView(false);
            // Ancorage details grid enabled
            $("#BearingDistanceFromBreakWater").attr('disabled', false);
            $('#ddlAnchorReason').removeAttr('disabled');
            $("#Remarks").attr('disabled', false);
            $("#AnchorPosition").attr('disabled', false);
            $('#ancDelete').removeClass('display-none');

            var PortIn = $("#PortIn").val();
            var BreakIn = $("#BreakIn").val();
            var BreakOut = $("#BreakOut").val();
            var PortOut = $("#PortOut").val();

            if (PortIn != "" && BreakIn == "" && BreakOut == "" && PortOut == "") {
                $("#BreakIn").data('kendoDateTimePicker').min(PortIn);
                $("#PortOut").data('kendoDateTimePicker').min(PortIn);
                $("#BreakOut").data('kendoDateTimePicker').min(PortIn);
            }
            else if (PortIn != "" && BreakIn != "" && BreakOut == "" && PortOut == "") {
                $("#BreakIn").data('kendoDateTimePicker').min(PortIn);
                $("#BreakOut").data('kendoDateTimePicker').min(BreakIn);
                $("#PortOut").data('kendoDateTimePicker').min(BreakIn);
            }
            else {
                $("#BreakIn").data('kendoDateTimePicker').min(PortIn);
                $("#BreakOut").data('kendoDateTimePicker').min(BreakIn);
                $("#PortOut").data('kendoDateTimePicker').min(BreakOut);
            }
        }


        //Set MaxDate as Today's date or Current date to avoid past date of post request
        calmaxtoday = function () {
            this.max(new Date());
        }
        //View  Capture Arrival/Departure Data 
        self.ViewVesselCallAnchorage = function (model) {
            $("#VesselCallAnchorageTitle").text("View Capture Arrival/Departure");
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsEditable(false);
            self.IsGridEditable(false);
            self.viewMode('Form');
            self.vesselCallAnchorageModel(model);
            self.vcnlocal = model.VCNSort;
            $("#PortIn").data('kendoDateTimePicker').enable(false);
            $("#PortOut").data('kendoDateTimePicker').enable(false);
            $("#BreakIn").data('kendoDateTimePicker').enable(false);
            $("#BreakOut").data('kendoDateTimePicker').enable(false);


        }

        //ATA Data modification
        ATAChangeDate = function () {
            $("#spanATA").text('');
            self.IsATAValid(false);
            var StartDateValue = $("#ATA").val();
            var EndDateValue = $("#ATD").val();
            $("#ATD").val('');
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '')
                $("#ATD").data('kendoDateTimePicker').min(StartDateValue);
        }
        //PortLimit Data modification
        PortInChangeDate = function () {
            $("#spanPortIn").text('');
            self.IsPortInValid(false);
            var StartDateValue = $("#PortIn").val() != "" ? moment($("#PortIn").val()).format('YYYY-MM-DD HH:mm') : "" || "";
            var EndDateValue = $("#PortOut").val();
            $("#PortOut").val('');
            $("#BreakIn").val('');
            $("#BreakOut").val('');
            self.vesselCallAnchorageModel().PortLimitOut('');
            self.vesselCallAnchorageModel().BreakWaterIn('');
            self.vesselCallAnchorageModel().ATA('')
            self.vesselCallAnchorageModel().BreakWaterOut('');
            self.vesselCallAnchorageModel().ATD('');
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '' &&  StartDateValue !="Invalid date") {
                $("#PortOut").data('kendoDateTimePicker').min(StartDateValue);
                $("#BreakIn").data('kendoDateTimePicker').min(StartDateValue);
                $("#BreakOut").data('kendoDateTimePicker').min(StartDateValue);

                if (self.Config() == 'PortLimit')
                    self.vesselCallAnchorageModel().ATA(StartDateValue);
            }
            if (StartDateValue == "Invalid date")
                self.isPortDateValid(true);
            else
                self.isPortDateValid(false);
        }
        PortOutChangeDate = function () {
            var StartDateValue = $("#PortOut").val() != "" ? moment($("#PortOut").val()).format('YYYY-MM-DD HH:mm') : "" || "";
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '' && StartDateValue != "Invalid date" && self.Config() == 'PortLimit')
                self.vesselCallAnchorageModel().ATD(StartDateValue);
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '' && StartDateValue != "Invalid date" && StartDateValue < $("#BreakOut").val()) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Port Limit Out should be greater than Break Water Out", "Capture Arrival/Departure");
                self.vesselCallAnchorageModel().PortLimitOut('');
                $("#PortOut").val('');
                $("#PortOut").focus();
            }
            if (StartDateValue == "Invalid date")
                self.isPortDateValid(true);
            else
                self.isPortDateValid(false);
        }
        //BreakWater Data modification
        BreakChangeDate = function () {
            $("#spanBreakIn").text('');
            self.IsBreakInValid(false);
            var StartDateValue = $("#BreakIn").val() != "" ? moment($("#BreakIn").val()).format('YYYY-MM-DD HH:mm') : "" || "";
            var EndDateValue = $("#BreakOut").val();
            $("#BreakOut").val('');
            $("#PortOut").val('');
            self.vesselCallAnchorageModel().PortLimitOut('');
            self.vesselCallAnchorageModel().BreakWaterOut('');
            self.vesselCallAnchorageModel().ATD('');
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '' && StartDateValue != "Invalid date") {
                $("#BreakOut").data('kendoDateTimePicker').min(StartDateValue);
                $("#PortOut").data('kendoDateTimePicker').min(StartDateValue);

                if (self.Config()=='BreakWater')
                    self.vesselCallAnchorageModel().ATA(StartDateValue);

                if (StartDateValue < $("#PortIn").val()) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Break Water In should be greater than Port Limit In", "Capture Arrival/Departure");
                    self.vesselCallAnchorageModel().BreakWaterIn('');
                    $("#BreakIn").val('');
                    $("#BreakIn").focus();
                    self.vesselCallAnchorageModel().ATA('');
                }
            }
            if (StartDateValue == "Invalid date")
                self.isPortDateValid(true);
            else
                self.isPortDateValid(false);

        }
        BreakOutChangeDate = function () {
            var StartDateValue = $("#BreakOut").val() != "" ? moment($("#BreakOut").val()).format('YYYY-MM-DD HH:mm') : "" || "";
            $("#PortOut").val('');
            self.vesselCallAnchorageModel().PortLimitOut('');
            self.vesselCallAnchorageModel().ATD('');
            if (StartDateValue != null && StartDateValue != "" && StartDateValue != '' && StartDateValue != "Invalid date") {
                $("#PortOut").data('kendoDateTimePicker').min(StartDateValue);
                if (self.Config() == 'BreakWater')
                    self.vesselCallAnchorageModel().ATD(StartDateValue);

                if (StartDateValue < $("#BreakIn").val()) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Break Water Out should be greater than Break Water In", "Capture Arrival/Departure");
                    self.vesselCallAnchorageModel().BreakWaterOut('');
                    $("#BreakOut").val('');
                    self.vesselCallAnchorageModel().ATD('');
                    $("#BreakOut").focus();
                }
            }
            if (StartDateValue == "Invalid date")
                self.isPortDateValid(true);
            else
                self.isPortDateValid(false);

        }

        ValidDate = function () {
            self.vesselCallAnchorageModel().ETATo(self.vesselCallAnchorageModel().ETAFrom());
        }

        // Get Search Data
        self.GetSearchData = function () {
            var isnoError = true;
            
            var vcnSearch = self.vesselCallAnchorageModel().VCNSearch();
            var vesselName = self.vesselCallAnchorageModel().VesselNameSearch();
            var vcnSearchSelected = self.vesselCallAnchorageModel().VCNSelected();

            if (vcnSearch == "") {
                vcnSearch = "All";
                $("#spanVCNSearchValid").text('');
                self.isspanVCNSearchValid(false);
            }
            else {

                if (vcnSearchSelected != vcnSearch) {
                    isnoError = false;
                    $("#spanVCNSearchValid").text('Please select valid VCN');
                    self.isspanVCNSearchValid(true);
                }

             
            }
          
            if (vesselName == "") {
                vesselName = "All";
                $("#spanVesselSearchValid").text('');
                self.isspanVesselSearchValid(false);
            }
            else {

                if (self.vesselCallAnchorageModel().VesselNameSelected() != vesselName) {
                    isnoError = false; 
                    $("#spanVesselSearchValid").text('Please select valid Vessel Name/IMO No.');
                    self.isspanVesselSearchValid(true);
                }


            }
            if (isnoError) {
                self.LoadVesselCallAnchorageList();
            }
        }

        //Reset Search Data
        self.ResetData = function () {           

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);

            self.vesselCallAnchorageModel().ETAFrom(moment(fromdate).format('YYYY-MM-DD'));
            self.vesselCallAnchorageModel().ETATo(moment(todate).format('YYYY-MM-DD'));
            self.vesselCallAnchorageModel().VCNSearch('');
            self.vesselCallAnchorageModel().VesselNameSearch('');
            self.isspanVCNSearchValid(false);
            self.isspanVesselSearchValid(false);
            $("#spanVesselSearchValid").text('');
            $("#spanVCNSearchValid").text('');
            self.LoadVesselCallAnchorageList();
        }
        //-------------------------------------------------------
        SerchVesselBackSpace = function (e) {
           
            self.vesselCallAnchorageModel().VesselNameSearch('');
        }
        SerchVCNBackSpace = function (e) {
           
            self.vesselCallAnchorageModel().VCNSearch('');
        }

        SerchVCNBackSpaceNumValid = function (evt) {


            self.vesselCallAnchorageModel().VCNSearch('');

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
     }

        self.VCNSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.vesselCallAnchorageModel().VCNSelected(selecteddataItem.vcn);
            self.isspanVCNSearchValid(false);
            $("#spanVCNSearchValid").text('');
        }

        self.VesselSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.vesselCallAnchorageModel().VesselNameSelected(selecteddataItem.VesselName);
            self.isspanVesselSearchValid(false);
            $("#spanVesselSearchValid").text('');

        }
        VCNonblur = function (e) {
           var vcnblur = $("#VCNName").val();
            self.vesselCallAnchorageModel().VCNSearch(vcnblur);



        }
        Vesselonblur = function (e) {
            var vesselblur = $("#VesselName1").val();
            self.vesselCallAnchorageModel().VesselNameSearch(vesselblur);

        }



        self.VcnClose = function (model) {
            var vcnName = model.VCN()!= undefined ? model.VCN() : "";
            self.vcnNameOnClose = model.VesselName() != undefined ? model.VesselName(): "";
            self.ImoNoOnVcnClose = model.IMONo() != undefined ? model.IMONo() : "";
            self.CallsignOnVcnClose = model.CallSign() != undefined ? model.CallSign() : "";
            self.viewModelHelper.apiGet('api/VcnClose/' + vcnName, null,
               function (result) {
                   if (result != null) {
                       self.isVcnCloseView(true);
                       self.EditVesselCallAnchorage(new IPMSRoot.VesselCallAnchorageModel(result));
                   }
               });
        }


        //-------------------------------------------------------

        self.Initialize();
    }

    IPMSRoot.VesselCallAnchorageViewModel = VesselCallAnchorageViewModel;

}(window.IPMSROOT));