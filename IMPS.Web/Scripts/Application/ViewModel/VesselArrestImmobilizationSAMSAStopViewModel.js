(function (IPMSRoot) {

    var VesselArrestImmobilizationSAMSAStopViewModel = function () {

        var self = this;

        $("#VesselArrestImmobilizationSAMSAStopTitle").text("Vessel Arrests");

        self.viewModelHelper = new IPMSRoot.viewModelHelper();
        self.VesselArrestImmobilizationSAMSAStopList = ko.observableArray();
        self.vesselArrestImmobilizationSAMSAStopModel = ko.observable();
        self.viewMode = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsEditable = ko.observable(true);
        self.IsEditableA = ko.observable(true);
        self.IsEditableI = ko.observable(true);
        self.IsEditableS = ko.observable(true);
        self.GridChk = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.vcndetails = ko.observableArray();
        self.IschkVesselArrested = ko.observable(false);
        self.IschkVesselReleased = ko.observable(false);
        self.IschkImmobilization = ko.observable(false);
        self.IschkSAMSAStop = ko.observable(false);
        self.IschkSAMSACleared = ko.observable(false);
        self.isPeriodofImmobilization = ko.observable(false);
        self.IsVCN = ko.observable(false);
        self.IsVCNlable = ko.observable(false);
        self.isArrestedMsg = ko.observable(false);
        self.isArrestedRemarksMsg = ko.observable(false);
        self.isReleasedMsg = ko.observable(false);
        self.isReleasedRemarksMsg = ko.observable(false);
        self.isVesselArrestDetailsUploadFileMsg = ko.observable(false);
        self.isSAMSAStopUploadFileMsg = ko.observable(false);
        self.isIMBStartDateMsg = ko.observable(false);
        self.isIMBEndDateDMsg = ko.observable(false);
        self.isSAMSAStopMsg = ko.observable(false);
        self.isSAMSAStopRemarksMsg = ko.observable(false);
        self.isSMASAClearedMsg = ko.observable(false);
        self.isSAMSAClearedRemarksdMsg = ko.observable(false);
        self.isApprovedMsg = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.isArresstReleased_chk = ko.observable(false);
        self.isSAMSACleared_chk = ko.observable(false);
        self.arrivalDate = ko.observable();

        // Page Load
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.vesselArrestImmobilizationSAMSAStopModel(new IPMSROOT.VesselArrestImmobilizationSAMSAStopModel());
            self.LoadVesselArrestImmobilizationSAMSAStopList();
            self.GetFileSizeConfigValue();
            self.viewMode('List');
           // self.LoadVcnDetails();
        }
        VCNBackSpaceNumValid = function (evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) 
                    return false;
               
                    return true;
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue', null, function (result) {
                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
            }, null, null, false);
        }

        calOpen = function () {
            this.min(self.arrivalDate());
            this.max(new Date());
        };

        calOpenReleasedDate = function () {
            this.min($("#ArrestedDate").val());
            this.max(new Date());
        };

        calOpenImmobilizationEndDate = function () {
            this.min($("#ImmobilizationStartDate").val());
            this.max(new Date());
        };

        calOpenApprovedDate = function () {
            this.min($("#ImmobilizationEndDate").val());
            this.max(new Date());
        };

        calOpenSAMSAClearedDate = function () {
            this.min($("#SAMSAStopDate").val());
            this.max(new Date());
        };

        //Calculate Start Period Of Immobilization
        CalcStartPeriodofImmobilization = function () {
            $("#isIMBStartDateMsg").text('');
            self.isIMBStartDateMsg(false);
            var StartDateValue = $("#ImmobilizationStartDate").val();
            var EndDateValue = $("#ImmobilizationEndDate").val();
            $("#ImmobilizationEndDate").val('');
            $("#ImmobilizationEndDate").data('kendoDateTimePicker').min(StartDateValue);
            self.vesselArrestImmobilizationSAMSAStopModel().PeriodofImmobilization('');
        }

        //Calculate End Period Of Immobilization With Total Number of Hours
        CalcPeriodofImmobilization = function (data, event) {
            var startDateValue = self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate();
            var dtImmobilizationStartDate = new Date(Date.parse(moment(startDateValue)));
            var endDateValue = data.sender._oldText;
            var dtImmobilizationEndDate = new Date(Date.parse(moment(endDateValue)));
            if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() == "" || self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() == null) {
                self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate("");
                $("#ImmobilizationEndDate").val('');
                $("#ImmobilizationStartDate").focus();
                $("#isIMBStartDateMsg").text('Please select immobilization start Date/Time.');
                self.isIMBStartDateMsg(true);
            }
            else {
                if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != null)) {
                    self.isIMBStartDateMsg(false);
                    if (dtImmobilizationStartDate > dtImmobilizationEndDate) {
                        errors1 = errors1 + 1;
                        $("#isIMBEndDateDMsg").text('Immobilization end Date/Time should be greater than immobilization start Date/Time.');
                        self.isReleasedMsg(true);
                    }
                    else {
                        var currentDate = new Date();
                        var tImmobilizationStartDate = dtImmobilizationStartDate.getMilliseconds();
                        var tImmobilizationEndDate = dtImmobilizationEndDate.getMilliseconds();

                        // calculating differec time b/w start and end time of immobilazation
                        self.isReleasedMsg(false);
                        var diff = dtImmobilizationEndDate - dtImmobilizationStartDate;
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
                        period = hhh + ':' + mints + ':' + sss;
                        self.vesselArrestImmobilizationSAMSAStopModel().PeriodofImmobilization(period);
                    }
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isIMBStartDateMsg").text('Please select immobilization start Date/Time.');
                    self.isIMBStartDateMsg(true);
                }
            }
        }

        VesselArresstReleased = function () {
            self.isArresstReleased_chk(false);
            if ((self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != null)) {

                self.isArresstReleased_chk(true);
                self.IsEditableA(false);
                self.IschkVesselArrested(false);
                $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
            }
            else {
                self.isArresstReleased_chk(false);
                self.IsEditableA(true);
                self.IschkVesselArrested(true);
            }
        }

        VesselSAMSACleared = function () {

            self.isSAMSACleared_chk(false);
            if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != null)) {

                self.isSAMSACleared_chk(true);
                self.IsEditableS(false);
                self.IschkSAMSAStop(false)
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
            }
            else {
                self.isSAMSACleared_chk(false);
                self.IsEditableS(true);
                self.IschkSAMSAStop(true)
            }
        }

        CalcPeriodofImmobilization1 = function () {
            var startDateValue = self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate();
            var dtImmobilizationStartDate = new Date(Date.parse(moment(startDateValue)));
            var endDateValue = self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate();
            var dtImmobilizationEndDate = new Date(Date.parse(moment(endDateValue)));

            if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != null) && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != null)) {
                self.isIMBStartDateMsg(false);
                if (dtImmobilizationStartDate > dtImmobilizationEndDate) {
                    self.vesselArrestImmobilizationSAMSAStopModel().PeriodofImmobilization("");
                }
                else {
                    var currentDate = new Date();
                    var tImmobilizationStartDate = dtImmobilizationStartDate.getMilliseconds();
                    var tImmobilizationEndDate = dtImmobilizationEndDate.getMilliseconds();

                    // calculating differec time b/w start and end time of immobilazation
                    self.isReleasedMsg(false);
                    var diff = dtImmobilizationEndDate - dtImmobilizationStartDate;
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
                    self.vesselArrestImmobilizationSAMSAStopModel().PeriodofImmobilization(period);
                }
            }
        }

        self.clearallerrormsgs = function () {
            self.isArrestedMsg(false);
            self.isArrestedRemarksMsg(false);
            self.isReleasedMsg(false);
            self.isReleasedRemarksMsg(false);
            self.isIMBStartDateMsg(false);
            self.isIMBEndDateDMsg(false);
            self.isSAMSAStopMsg(false);
            self.isSAMSAStopRemarksMsg(false);
            self.isSMASAClearedMsg(false);
            self.isSAMSAClearedRemarksdMsg(false);
            self.isApprovedMsg(false);
        }

        //self.LoadVcnDetails = function () {
        //    self.viewModelHelper.apiGet('api/GetVcnDetails', null, function (result) {
        //        ko.mapping.fromJS(result, {}, self.vcndetails);
        //    }, null, null, false);
        //}

        self.LoadVesselArrestImmobilizationSAMSAStopList = function () {
            self.viewModelHelper.apiGet('api/GetVesselArrestImmobilizationSAMSAStopList', null,
            function (result) {
                self.VesselArrestImmobilizationSAMSAStopList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.VesselArrestImmobilizationSAMSAStopModel(item);
                }));
            }, null, null, false);
        }

        // Gets VCN details by VCN ( auto populate)
        self.VCNSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            var vcnlist = JSON.parse(ko.toJSON(self.VesselArrestImmobilizationSAMSAStopList));
            var flag = true;
            $('#spanvcnd').text('');
            $('#spanvcnd').css('display', '');
            $.each(vcnlist, function (index, value) {
                if (value.VCN == selecteddataItem.VCN) {
                    $('#spanvcnd').text('Already VCN number exists.! Please select another VCN.');
                    $('#spanvcnd').css('display', '');
                    flag = false;
                    return false;
                }
            });

            if (flag) {
                self.viewModelHelper.apiGet('api/GetVesselInfoByVCN', { vcn: selecteddataItem.VCN }, function (result) {
                    self.vesselArrestImmobilizationSAMSAStopModel().VCN(result.VCN);
                    self.vesselArrestImmobilizationSAMSAStopModel().VesselName(result.VesselName);
                    self.vesselArrestImmobilizationSAMSAStopModel().AgentName(result.AgentName);
                    self.vesselArrestImmobilizationSAMSAStopModel().LOA(result.LOA);
                    self.vesselArrestImmobilizationSAMSAStopModel().GRT(result.GRT);
                    self.vesselArrestImmobilizationSAMSAStopModel().PortOfRegistry(result.PortOfRegistry);
                    self.vesselArrestImmobilizationSAMSAStopModel().TelephoneNo1(result.TelephoneNo1);
                    self.vesselArrestImmobilizationSAMSAStopModel().ETA(result.ETA);
                    self.arrivalDate(moment(result.ETA).format('YYYY-MM-DD HH:mm'));
                }, null, null, false);
            }
        }

        //Add button click event
        self.addVesselArrestImmobilizationSAMSAStop = function (data) {
            self.vesselArrestImmobilizationSAMSAStopModel(new IPMSRoot.VesselArrestImmobilizationSAMSAStopModel);
            $("#VesselArrestImmobilizationSAMSAStopTitle").text("Add Vessel Arrests");
            self.IsVCN(true);
            self.IsVCNlable(false)
            self.IsSave(true);
            self.IsReset(true);
            self.viewMode('Form');
            self.IsEditable(true);
            self.IsEditableA(true);
            self.IsEditableI(true);
            self.IsEditableS(true);
            self.IsUpdate(false);
            self.IschkVesselArrested(false);
            self.IschkVesselReleased(false);           
            self.IschkImmobilization(false);
            self.IschkSAMSAStop(false);
            self.IschkSAMSACleared(false);
            $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
            $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
            $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(false);
            $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(false);
            $("#ApprovedDate").data('kendoDateTimePicker').enable(false);
            $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
            $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
            self.isArresstReleased_chk(false);
            self.isSAMSACleared_chk(false);
        }

        // Updates the VesselArrestImmobilizationSAMSAStop in Edit Mode
        self.ModifySaveVesselArrestImmobilizationSAMSAStop = function (model) {

            self.VesselArrestImmobilizationSAMSAStopValidation = ko.observable(model);
            self.VesselArrestImmobilizationSAMSAStopValidation().errors = ko.validation.group(self.VesselArrestImmobilizationSAMSAStopValidation);
            var errors = self.VesselArrestImmobilizationSAMSAStopValidation.errors().length;
            var errors1 = 0;
            if (errors == 0) {

                if ((!self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestedStatus()) && (!self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) && (!self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopStatus())) {
                    errors1 = errors1 + 1;
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please select atleast one type of arrest.");
                    return;
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != null)) {
                        self.isArrestedMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isArrestedMsg").text('Please select arrested Date/Time.');
                        self.isArrestedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ArrestedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ArrestedRemarks() != null)) {
                        self.isArrestedMsg(false);
                        self.isArrestedRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isArrestedRemarksMsg").text('Please enter arrested remarks.');
                        self.isArrestedRemarksMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().VesselReleasedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate() != null)) {
                        self.isReleasedMsg(false);
                        var dtArrestedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate()));
                        var dtReleasedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate()));
                        if (dtArrestedDate > dtReleasedDate) {
                            errors1 = errors1 + 1;
                            $("#isReleasedMsg").text('Released Date/Time should be greater than arrested Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isReleasedMsg").text('Please select released Date/Time.');
                        self.isReleasedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks() != null)) {
                        self.isReleasedRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isReleasedRemarksMsg").text('Please enter released remarks.');
                        self.isReleasedRemarksMsg(true);
                    }

                    // Bug # 4289
                    if (model.VesselArrestDocuments().length == 0) {
                        errors1 = errors1 + 1;
                        $("#isVesselArrestDetailsUploadFileMsg").text('Please upload files.');
                        self.isVesselArrestDetailsUploadFileMsg(true);

                        result = false;
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != null)) {
                        self.isIMBStartDateMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isIMBStartDateMsg").text('Please select immobilization start Date/Time.');
                        self.isIMBStartDateMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != null)) {
                        self.isIMBEndDateDMsg(false);
                        var dtImmobilizationStartDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate()));
                        var dtImmobilizationEndDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate()));
                        if (dtImmobilizationStartDate > dtImmobilizationEndDate) {
                            errors1 = errors1 + 1;
                            $("#isIMBEndDateDMsg").text('Immobilization end Date/Time should be greater than immobilization start Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isIMBEndDateDMsg").text('Please select immobilization end Date/Time.');
                        self.isIMBEndDateDMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != null)) {
                        self.isSAMSAStopMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopMsg").text('Please select SAMSA stop Date/Time.');
                        self.isSAMSAStopMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopRemarks() != null)) {
                        self.isSAMSAStopRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopRemarksMsg").text('Please enter SAMSA stop remarks.');
                        self.isSAMSAStopRemarksMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate() != null)) {
                        self.isSMASAClearedMsg(false);
                        var dtSAMSAStopDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate()));
                        var dtSAMSAClearedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate()));
                        if (dtSAMSAStopDate > dtSAMSAClearedDate) {
                            errors1 = errors1 + 1;
                            $("#isSMASAClearedMsg").text('SAMSA cleared Date/Time should be greater than SAMSA stop Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSMASAClearedMsg").text('Please select SAMSA cleared Date/Time.');
                        self.isSMASAClearedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks() != null)) {
                        self.isSAMSAClearedRemarksdMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAClearedRemarksdMsg").text('Please enter SAMSA cleared remarks.');
                        self.isSAMSAClearedRemarksdMsg(true);
                    }

                    // Bug # 4289
                    if (model.VesselSAMSAStopDocuments().length == 0) {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopUploadFileMsg").text('Please upload files.');
                        self.isSAMSAStopUploadFileMsg(true);

                        result = false;
                    }
                }
                if (errors1 == 0) {

                    if ($("#VesselArrestedStatus").is(":checked")) {
                        model.VesselArrestedStatus(true);
                    }
                    else {
                        model.VesselArrestedStatus(false);
                    }
                    if ($("#VesselReleasedStatus").is(":checked")) {
                        model.VesselReleasedStatus(true);
                    }
                    else {
                        model.VesselReleasedStatus(false);
                    }
                    if ($("#ImmobilizationStatus").is(":checked")) {
                        model.ImmobilizationStatus(true);
                    }
                    else {
                        model.ImmobilizationStatus(false);
                    }
                    if ($("#SAMSAStopStatus").is(":checked")) {
                        model.SAMSAStopStatus(true);
                    }
                    else {
                        model.SAMSAStopStatus(false);
                    }
                    if ($("#SAMSAClearedStatus").is(":checked")) {
                        model.SAMSAClearedStatus(true);
                    }
                    else {
                        model.SAMSAClearedStatus(false);
                    }

                    self.viewModelHelper.apiPut('api/VesselArrestImmobilizationSAMSAStop', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Vessel arrests immobilization SAMSAStop updated successfully.", "Vessel Arrests");
                            $("#VesselArrestImmobilizationSAMSAStopTitle").text("Vessel Arrests");
                            self.viewMode('List');
                            self.LoadVesselArrestImmobilizationSAMSAStopList();
                        });
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("You have some form errors. Please check below.");
                    self.VesselArrestImmobilizationSAMSAStopValidation().errors.showAllMessages();
                    $('#divValidationError').removeClass('display-none');
                    return;
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("You have some form errors. Please check below.");
                self.VesselArrestImmobilizationSAMSAStopValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        //Inserts / Adds the  VesselArrestImmobilizationSAMSAStop Data to Database in Add Mode
        self.SaveVesselArrestImmobilizationSAMSAStop = function (model) {



            self.VesselArrestImmobilizationSAMSAStopValidation = ko.observable(model);
            self.VesselArrestImmobilizationSAMSAStopValidation().errors = ko.validation.group(self.VesselArrestImmobilizationSAMSAStopValidation);
            var errors = self.VesselArrestImmobilizationSAMSAStopValidation.errors().length;
            var errors1 = 0;
            if (errors == 0) {
                if ((!self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestedStatus()) && (!self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) && (!self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopStatus())) {
                    errors1 = errors1 + 1;
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please select atleast one type of arrest.");
                    return;
                }

                if (self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate() != null)) {
                        self.isArrestedMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isArrestedMsg").text('Please select arrested Date/Time.');
                        self.isArrestedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ArrestedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ArrestedRemarks() != null)) {
                        self.isArrestedRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isArrestedRemarksMsg").text('Please enter arrested remarks.');
                        self.isArrestedRemarksMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().VesselReleasedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate() != null)) {
                        self.isReleasedMsg(false);
                        var dtArrestedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate()));
                        var dtReleasedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate()));
                        if (dtArrestedDate > dtReleasedDate) {
                            errors1 = errors1 + 1;
                            $("#isReleasedMsg").text('Released Date/Time should be greater than arrested Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isReleasedMsg").text('Please select released Date/Time.');
                        self.isReleasedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks() != null)) {
                        self.isReleasedRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isReleasedRemarksMsg").text('Please enter released remarks.');
                        self.isReleasedRemarksMsg(true);
                    }

                    // Bug # 4289
                    if (model.VesselArrestDocuments().length == 0) {
                        errors1 = errors1 + 1;
                        $("#isVesselArrestDetailsUploadFileMsg").text('Please upload files.');
                        self.isVesselArrestDetailsUploadFileMsg(true);

                        result = false;
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate() != null)) {
                        self.isIMBStartDateMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isIMBStartDateMsg").text('Please select immobilization start Date/Time.');
                        self.isIMBStartDateMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate() != null)) {
                        self.isIMBEndDateDMsg(false);
                        var dtImmobilizationStartDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate()));
                        var dtImmobilizationEndDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate()));
                        if (dtImmobilizationStartDate > dtImmobilizationEndDate) {
                            errors1 = errors1 + 1;
                            $("#isIMBEndDateDMsg").text('Immobilization end Date/Time should be greater than immobilization start Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isIMBEndDateDMsg").text('Please select immobilization end Date/Time.');
                        self.isIMBEndDateDMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate() != null)) {
                        self.isSAMSAStopMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopMsg").text('Please select SAMSA stop Date/Time.');
                        self.isSAMSAStopMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopRemarks() != null)) {
                        self.isSAMSAStopRemarksMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopRemarksMsg").text('Please enter SAMSA stop remarks.');
                        self.isSAMSAStopRemarksMsg(true);
                    }
                }
                if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedStatus()) {
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate() != null)) {
                        self.isSMASAClearedMsg(false);
                        var dtSAMSAStopDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate()));
                        var dtSAMSAClearedDate = new Date(Date.parse(self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate()));
                        if (dtSAMSAStopDate > dtSAMSAClearedDate) {
                            errors1 = errors1 + 1;
                            $("#isSMASAClearedMsg").text('SAMSA cleared Date/Time should be greater than SAMSA stop Date/Time.');
                            self.isReleasedMsg(true);
                        }
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSMASAClearedMsg").text('Please select SAMSA cleared Date/Time.');
                        self.isSMASAClearedMsg(true);
                    }
                    if ((self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks() != "") && (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks() != null)) {
                        self.isSAMSAClearedRemarksdMsg(false);
                    }
                    else {
                        errors1 = errors1 + 1;
                        $("#isSAMSAClearedRemarksdMsg").text('Please enter SAMSA cleared Remarks.');
                        self.isSAMSAClearedRemarksdMsg(true);
                    }

                    // Bug # 4289
                    if (model.VesselSAMSAStopDocuments().length == 0) {
                        errors1 = errors1 + 1;
                        $("#isSAMSAStopUploadFileMsg").text('Please upload files.');
                        self.isSAMSAStopUploadFileMsg(true);

                        result = false;
                    }
                }
                if (errors1 == 0) {
                    debugger;
                    if ($("#VesselArrestedStatus").is(":checked")) {
                        model.VesselArrestedStatus(true);
                    }
                    else {
                        model.VesselArrestedStatus(false);
                    }
                    if ($("#VesselReleasedStatus").is(":checked")) {
                        model.VesselReleasedStatus(true);
                    }
                    else {
                        model.VesselReleasedStatus(false);
                    }
                    if ($("#ImmobilizationStatus").is(":checked")) {
                        model.ImmobilizationStatus(true);
                    }
                    else {
                        model.ImmobilizationStatus(false);
                    }
                    if ($("#SAMSAStopStatus").is(":checked")) {
                        model.SAMSAStopStatus(true);
                    }
                    else {
                        model.SAMSAStopStatus(false);
                    }
                    if ($("#SAMSAClearedStatus").is(":checked")) {
                        model.SAMSAClearedStatus(true);
                    }
                    else {
                        model.SAMSAClearedStatus(false);
                    }

                    self.viewModelHelper.apiPost('api/VesselArrestImmobilizationSAMSAStop', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Vessel arrests immobilization SAMSAStop saved successfully.", "Vessel Arrests");
                            $("#VesselArrestImmobilizationSAMSAStopTitle").text("Vessel Arrests");
                            self.viewMode('List');
                            self.LoadVesselArrestImmobilizationSAMSAStopList();
                        });
                }
                else {
                    self.VesselArrestImmobilizationSAMSAStopValidation().errors.showAllMessages();
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("You have some form errors. Please check below.");
                    $('#divValidationError').removeClass('display-none');
                    return;
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("You have some form errors. Please check below.");
                self.VesselArrestImmobilizationSAMSAStopValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        //Cancel button click event in Add / Edit mode
        self.Cancel = function () {
            self.vesselArrestImmobilizationSAMSAStopModel().reset();
            self.viewMode("List");
            $("#VesselArrestImmobilizationSAMSAStopTitle").text("Vessel Arrests");
        }

        //Reset button click event in Add / Edit mode
        self.ResetVesselArrestImmobilizationSAMSAStop = function (model) {
            ko.validation.reset();

            self.vesselArrestImmobilizationSAMSAStopModel().reset();

            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            self.clearallerrormsgs();
            if ($("#VesselArrestedStatus").is(":checked")) {
                $("#ArrestedDate").data('kendoDateTimePicker').enable(true);
                self.IschkVesselArrested(true);
                self.isArresstReleased_chk(true);
            }
            else {
                $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
                self.IschkVesselArrested(false);
                self.isArresstReleased_chk(false);
            }

            if ($("#VesselReleasedStatus").is(":checked")) {
                $("#ReleasedDate").data('kendoDateTimePicker').enable(true);
                self.IschkVesselReleased(true);
            }
            else {
                $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
                self.IschkVesselReleased(false);
            }
            if ($("#SAMSAStopStatus").is(":checked")) {
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(true);
                self.IschkSAMSAStop(true);
                self.isSAMSACleared_chk(true);
            }
            else {
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
                self.IschkSAMSAStop(false);
                self.isSAMSACleared_chk(false);
            }
            if ($("#SAMSAClearedStatus").is(":checked")) {
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(true);
                self.IschkSAMSACleared(true);
            }
            else {
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
                self.IschkSAMSACleared(false);
            }

            if ($("#ImmobilizationStatus").is(":checked")) {

                self.IschkImmobilization(true);
                $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(true);
                $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(true);
                $("#ApprovedDate").data('kendoDateTimePicker').enable(true);
            }
            else {
                self.isIMBStartDateMsg(false);
                $("#isIMBEndDateDMsg").text('');
                self.isIMBEndDateDMsg(false);
                self.IschkImmobilization(false);
            }
        }

        //Edit button click event from the Grid 
        self.EditVesselArrestImmobilizationSAMSAStop = function (model) {
            $("#VesselArrestImmobilizationSAMSAStopTitle").text("Update Vessel Arrests");
            self.arrivalDate(moment(model.ETA()).format('YYYY-MM-DD HH:mm'));
            self.IsSave(false);
            self.IsReset(true);
            self.viewMode('Form');
            self.vesselArrestImmobilizationSAMSAStopModel(model);
            self.IsVCN(false);
            self.IsVCNlable(true)
            self.IsEditableA(true);
            self.IsEditableI(true);
            self.IsEditableS(true);
            self.IsUpdate(true);

            if (self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestedStatus()) {
                self.IschkVesselArrested(true);
            }
            else {
                self.IschkVesselArrested(false);
                $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
            }

            if (self.vesselArrestImmobilizationSAMSAStopModel().VesselReleasedStatus()) {
                self.IschkVesselReleased(true);
            }
            else {
                self.IschkVesselReleased(false);
                $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
            }
            if (self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStatus()) {
                self.IschkImmobilization(true);
            }
            else {
                self.IschkImmobilization(false);
                $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(false);
                $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(false);
                $("#ApprovedDate").data('kendoDateTimePicker').enable(false);
            }
            if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopStatus()) {
                self.IschkSAMSAStop(true);
            }
            else {
                self.IschkSAMSAStop(false);
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
            }
            if (self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedStatus()) {
                self.IschkSAMSACleared(true);
            }
            else {
                self.IschkSAMSACleared(false);
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
            }

            CalcPeriodofImmobilization1();
            VesselArresstReleased();
            VesselSAMSACleared();
            self.vesselArrestImmobilizationSAMSAStopModel().reset();
        }

        //View button click event from the grid
        self.ViewVesselArrestImmobilizationSAMSAStop = function (model) {
            $("#VesselArrestImmobilizationSAMSAStopTitle").text("View Vessel Arrests");
            self.IsSave(false);
            self.IsReset(false);
            self.IsEditable(false);
            self.IsEditableA(false);
            self.IsEditableI(false);
            self.IsEditableS(false);
            self.IsUpdate(false);
            self.IsVCN(false);
            self.IsVCNlable(true)
            self.viewMode('Form');
            self.vesselArrestImmobilizationSAMSAStopModel(model);
            self.IschkVesselArrested(false);
            self.IschkVesselReleased(false);
            self.IschkImmobilization(false);
            self.IschkSAMSAStop(false);
            self.IschkSAMSACleared(false);
            self.isSAMSACleared_chk(false);
            self.isArresstReleased_chk(false);
            $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
            $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
            $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(false);
            $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(false);
            $("#ApprovedDate").data('kendoDateTimePicker').enable(false);
            $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
            $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
            CalcPeriodofImmobilization1();
        }

        // VesselArrest Checkbox event changed
        //self.chkVesselArrested = function (event) {
        chkVesselArrested = function () {
            self.isArresstReleased_chk(false);
            //if (!event.VesselArrestedStatus()) {

            //}
            if ($("#VesselArrestedStatus").is(":checked")) {

                $("#ArrestedDate").data('kendoDateTimePicker').enable(true);
                self.IschkVesselArrested(true);
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks("");
                $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
                // self.IschkVesselReleased(false);
                self.IschkVesselReleased(true);
                self.isArresstReleased_chk(false);
            }
            else {
                $("#isArrestedMsg").text('');
                self.isArrestedMsg(false);
                $("#isReleasedRemarksMsg").text('');
                self.isReleasedRemarksMsg(false);
                $("#isReleasedMsg").text('');
                self.isReleasedMsg(false);
                $("#isArrestedRemarksMsg").text('');
                self.isArrestedRemarksMsg(false);
                self.isVesselArrestDetailsUploadFileMsg(false);
                $("#isVesselArrestDetailsUploadFileMsg").text("");
                self.vesselArrestImmobilizationSAMSAStopModel().VesselReleasedStatus(false);
                self.vesselArrestImmobilizationSAMSAStopModel().ArrestedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().ArrestedRemarks("");
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks("");
                $("#ArrestedDate").data('kendoDateTimePicker').enable(false);
                $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
                self.IschkVesselArrested(false);
                self.IschkVesselReleased(false);
                self.isArresstReleased_chk(false);
            }
        }

        // VesselReleased Checkbox event changed
        self.chkVesselReleased = function (event) {
            if ($("#VesselReleasedStatus").is(":checked")) {
                $("#ReleasedDate").data('kendoDateTimePicker').enable(true);
                self.IschkVesselReleased(true);
            }
            else {
                $("#isReleasedMsg").text('');
                self.isReleasedMsg(false);
                $("#isReleasedRemarksMsg").text('');
                self.isReleasedRemarksMsg(false);
                self.isVesselArrestDetailsUploadFileMsg(false);
                $("#isVesselArrestDetailsUploadFileMsg").text("");
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().ReleasedRemarks("");
                $("#ReleasedDate").data('kendoDateTimePicker').enable(false);
                self.IschkVesselReleased(false);
            }
        }

        // Immobilization Checkbox event changed
        self.chkImmobilization = function (event) {
            if ($("#ImmobilizationStatus").is(":checked")) {
                self.IschkImmobilization(true);
                $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(true);
                $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(true);
                $("#ApprovedDate").data('kendoDateTimePicker').enable(true);
            }
            else {
                $("#isIMBStartDateMsg").text('');
                self.isIMBStartDateMsg(false);
                $("#isIMBEndDateDMsg").text('');
                self.isIMBEndDateDMsg(false);
                self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationStartDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().ImmobilizationEndDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().PeriodofImmobilization("");
                self.vesselArrestImmobilizationSAMSAStopModel().ExactWorkProposed("");
                self.vesselArrestImmobilizationSAMSAStopModel().ApprovedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().PollutionPrecautionTakenStatus(false);
                $("#ImmobilizationStartDate").data('kendoDateTimePicker').enable(false);
                $("#ImmobilizationEndDate").data('kendoDateTimePicker').enable(false);
                $("#ApprovedDate").data('kendoDateTimePicker').enable(false);
                self.IschkImmobilization(false);
            }
        }

        // SAMSAStop Checkbox event changed
        self.chkSAMSAStop = function (event) {
            self.isSAMSACleared_chk(false);
            if ($("#SAMSAStopStatus").is(":checked")) {
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(true);
                self.IschkSAMSAStop(true);
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks("");
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
                self.IschkSAMSAStop(true);
                self.IschkSAMSACleared(false);
                self.isSAMSACleared_chk(false);
            }
            else {
                $("#isSAMSAStopMsg").text('');
                self.isSAMSAStopMsg(false);
                $("#isSAMSAStopRemarksMsg").text('');
                self.isSAMSAStopRemarksMsg(false);
                $("#isSMASAClearedMsg").text('');
                self.isSMASAClearedMsg(false);
                $("#isSAMSAClearedRemarksdMsg").text('');
                self.isSAMSAClearedRemarksdMsg(false);
                self.IschkSAMSAStop(false);
                self.isSAMSAStopUploadFileMsg(false);
                $("#isSAMSAStopUploadFileMsg").text("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedStatus(false);
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAStopRemarks("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks("");
                $("#SAMSAStopDate").data('kendoDateTimePicker').enable(false);
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
                self.IschkSAMSACleared(false);
                self.isSAMSACleared_chk(false);
            }
        }

        // SAMSACleared Checkbox event changed
        self.chkSAMSACleared = function (event) {
            if ($("#SAMSAClearedStatus").is(":checked")) {
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(true);
                self.IschkSAMSACleared(true);
            }
            else {
                self.isSAMSAStopUploadFileMsg(false);
                $("#isSAMSAStopUploadFileMsg").text("");
                $("#isSMASAClearedMsg").text('');
                self.isSMASAClearedMsg(false);
                $("#isSAMSAClearedRemarksdMsg").text('');
                self.isSAMSAClearedRemarksdMsg(false);
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedDate("");
                self.vesselArrestImmobilizationSAMSAStopModel().SAMSAClearedRemarks("");
                $("#SAMSAClearedDate").data('kendoDateTimePicker').enable(false);
                self.IschkSAMSACleared(false);
            }
        }

        self.uploadFile1 = function () {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            $("#isVesselArrestDetailsUploadFileMsg").text("");
            self.isVesselArrestDetailsUploadFileMsg(false);
            var documentType = $('#selUploadDocs option:selected').text();
            self.vesselArrestImmobilizationSAMSAStopModel().UploadedFiles([]);
            var uploadedFiles = [];
            uploadedFiles = self.vesselArrestImmobilizationSAMSAStopModel().UploadedFiles();
            var opmlFile = $('#fileToUpload1')[0];

            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {
                    var match = ko.utils.arrayFirst(self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestDocuments(), function (item) {
                        return item.FileName() === opmlFile.files[i].name;
                    });
                    if (match == null) {
                        //-- Checking For File Format
                        var elem = {};
                        elem.FileName = opmlFile.files[i].name;
                        var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                        var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                        if ($.inArray(extension, fileExtension) != -1) {

                            //-- Checking File Size
                            var fileSizeInBytes = opmlFile.files[i].size;
                            var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                            if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {

                                var elem = {};
                                elem.FileName = opmlFile.files[i].name;
                                //var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                //var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                                //if ($.inArray(extension, fileExtension) != -1) {
                                elem.CategoryName = $('#selUploadDocs option:selected').text();
                                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                //}
                                //else {
                                //    toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only.", "Error");
                                //    return;
                                //}
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only.", "Error");
                            return;
                        }
                    }
                    else {
                        toastr.error("The selected file already exists.! Please upload another file.", "Error");
                        return;
                    }
                }

                var formData = new FormData();
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });

                var CategoryCode = '';
                //if (opmlFile.files.length > 0) {
                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.Listdocuments = ko.observableArray();
                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.VesselSAMSAStopDocument();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
                        self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestDocuments.push(Adddoc);
                    }));
                });
                //}
            }

            else {
                $("#isVesselArrestDetailsUploadFileMsg").text('Please select file.');
                self.isVesselArrestDetailsUploadFileMsg(true);
            }

            return;
        }

        self.uploadFile2 = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            $("#isSAMSAStopUploadFileMsg").text("");
            self.isSAMSAStopUploadFileMsg(false);
            self.vesselArrestImmobilizationSAMSAStopModel().UploadedFiles([]);
            var documentType = $('#selUploadDocs option:selected').text();
            var uploadedFiles = [];
            uploadedFiles = self.vesselArrestImmobilizationSAMSAStopModel().UploadedFiles();
            var opmlFile = $('#fileToUpload2')[0];

            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {
                    var match = ko.utils.arrayFirst(self.vesselArrestImmobilizationSAMSAStopModel().VesselSAMSAStopDocuments(), function (item) {
                        return item.FileName() === opmlFile.files[i].name;
                    });
                    if (match == null) {
                        //-- Checking For File Format
                        var elem = {};
                        elem.FileName = opmlFile.files[i].name;
                        var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                        var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                        if ($.inArray(extension, fileExtension) != -1) {

                            //-- Checking File Size
                            var fileSizeInBytes = opmlFile.files[i].size;
                            var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                            if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {

                                var elem = {};
                                elem.FileName = opmlFile.files[i].name;
                                //var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                //var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                                //if ($.inArray(extension, fileExtension) != -1) {
                                elem.CategoryName = $('#selUploadDocs option:selected').text();
                                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                //}
                                //else {
                                //    toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only.", "Error");
                                //    return;
                                //}
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only.", "Error");
                            return;
                        }
                    }
                    else {
                        toastr.error("The selected file already exists.! Please upload another file.", "Error");
                        return;
                    }
                }
                var formData = new FormData();
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });

                var CategoryCode = '';
                //if (opmlFile.files.length > 0) {
                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.Listdocuments = ko.observableArray();
                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.VesselSAMSAStopDocument();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
                        self.vesselArrestImmobilizationSAMSAStopModel().VesselSAMSAStopDocuments.push(Adddoc);
                    }));
                });
                //}
            }
            else {
                $("#isSAMSAStopUploadFileMsg").text("Please select file.");
                self.isSAMSAStopUploadFileMsg(true);
            }
            return;
        }

        self.ArrestedDeleteDocument = function (Adddoc) {
            self.vesselArrestImmobilizationSAMSAStopModel().VesselArrestDocuments.remove(Adddoc);
        }

        self.ClearedDeleteDocument = function (Adddoc) {
            self.vesselArrestImmobilizationSAMSAStopModel().VesselSAMSAStopDocuments.remove(Adddoc);
        }

        //Vessel Search should be from Back end.
        //GetVCNDetails = function () {
        //    self.viewModelHelper.apiGet('api/GetVcnDetails', null, function (result) {
        //        ko.mapping.fromJS(result, {}, self.vcndetails);
        //    }, null, null, false);
        //}

        //Preventing BPreventBackSpaceackspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }

    IPMSRoot.VesselArrestImmobilizationSAMSAStopViewModel = VesselArrestImmobilizationSAMSAStopViewModel;
}(window.IPMSROOT));