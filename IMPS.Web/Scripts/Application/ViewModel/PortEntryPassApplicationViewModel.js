toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";



(function (IPMSRoot) {
    var isView = 0;
    var PortEntryPassApplicationViewModel = function (PermitRequestID, viewDetail) {
        var self = this;

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        //self.validationEnabled = ko.observable(false);
        self.portentrypassapplicationModel = ko.observable(new IPMSROOT.PortEntryPassApplicationModel());
        self.portentrypassapplicationreferencedata = ko.observable();
        self.subAreasForRB = ko.observable();
        self.permitrequestlist = ko.observableArray();
        self.permitrequestlistforapproval = ko.observableArray();
        self.permitrequestlistForSSA = ko.observableArray();
        self.permitrequestlistForSAPS = ko.observableArray();
        self.viewMode = ko.observable();

        //self.AdvnceSearchmodel = ko.observable();
        self.AdvnceSearchmodel = ko.observable(new IPMSROOT.AdvnceSearchmodel());

        self.ismultiplepfileToUpload = ko.observable(false);
        self.isAllportenable = ko.observable(false);
        self.isfileToUpload = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.viewMode = ko.observable();
        self.PortLoopArray = ko.observableArray();
        self.resubmitted = ko.observable(false);
        self.AppealOnRejected = ko.observable(false);
        self.isApplicationStaus = ko.observable();
        self.isSSAEnable = ko.observable(false);
        self.IsCodeEnable = ko.observable(false);
        self.isAppealenable = ko.observable(false);
        self.IsAppealCodeEnable = ko.observable(false);
        self.CaptachText = ko.observable();
        self.CaptachTextResubmit = ko.observable();
        self.CaptachTextRejected = ko.observable();
        self.isCompanyNameMsg = ko.observable(false);
        self.isContractNoMsg = ko.observable(false);
        self.isContractManagerNameMsg = ko.observable(false);
        self.isContractDurationMsg = ko.observable(false);
        self.isServiceCompanyNameMsg = ko.observable(false);
        self.isResponsibleManagerMsg = ko.observable(false);
        self.isContactNoMsg = ko.observable(false);
        self.isMobileNoMsg = ko.observable(false);
        self.isVehicleMakeNewDMsg = ko.observable(false);
        self.isVehicleModelNewDMsg = ko.observable(false);
        self.isVehicleRegnNoNewDMsg = ko.observable(false);
        self.isVehicleDescriptionNewDMsg = ko.observable(false);
        self.isVehiclePointedMsg = ko.observable(false);
        self.isVehicleRegisterdMsg = ko.observable(false);
        self.isVisitorpermitTelephoneNoNewGMsg = ko.observable(false);
        self.isVisitorpermitPositionHeldNewGMsg = ko.observable(false);
        self.isVisitorpermitAuthorizedPersonNameNewGMsg = ko.observable(false);
        self.isVisitorpermitDivisionNewGMsg = ko.observable(false);
        self.isVisitorpermitCompanynameNewGMsg = ko.observable(false);
        self.isVisitorpermitEscortNameNewGMsg = ko.observable(false);
        self.isVisitorpermitPermitNoNewGMsg = ko.observable(false);
        self.ispermitrequirementWharfvechiclenewDMsg = ko.observable(false);
        self.isCompanyNameResubmissionMsg = ko.observable(false);
        self.isContractNoResubmissionMsg = ko.observable(false);
        self.isContractManagerNameResubmissionMsg = ko.observable(false);
        self.isContractDurationResubmissionMsg = ko.observable(false);
        self.isServiceCompanyNameResubmissionMsg = ko.observable(false);
        self.isResponsibleManagerResubmissionMsg = ko.observable(false);
        self.isContactNoResubmissionMsg = ko.observable(false);
        self.isMobileNoResubmissionMsg = ko.observable(false);
        self.isVehicleMakewharfResubmissionMsg = ko.observable(false);
        self.isVehicleModelwharfResubmissionMsg = ko.observable(false);
        self.isVehicleRegnNowharfResubmissionMsg = ko.observable(false);
        self.isVehicleDescriptionwharfResubmissionMsg = ko.observable(false);
        self.isVehicleRegisterdwharfResubmissionMsg = ko.observable(false);
        self.isVehiclePointedwharfResubmissionMsg = ko.observable(false);
        self.isPermitRequeirementstypeswharfResubmissionMsg = ko.observable(false);
        self.isCompanyNamevisitorResubmissionMsg = ko.observable(false);
        self.isDivisionvisitorResubmissionMsg = ko.observable(false);
        self.isAuthorizedPersonNamevisitorResubmissionMsg = ko.observable(false);
        self.isPositionHeldvisitorResubmissionMsg = ko.observable(false);
        self.isTelephoneNovisitorResubmissionMsg = ko.observable(false);
        self.isEscortNamevisitorResubmissionMsg = ko.observable(false);
        self.isPermitNovisitorResubmissionMsg = ko.observable(false);
        self.IsEnableFalse = ko.observable(false);
        self.isPermittype = ko.observable();
        self.isPortSelected = ko.observable(false);
        self.RBportSelected = ko.observable(false);
        self.CTportSelected = ko.observable(false);
        self.DBportSelected = ko.observable(false);
        self.ELportSelected = ko.observable(false);
        self.NGportSelected = ko.observable(false);
        self.MBportSelected = ko.observable(false);
        self.SBportSelected = ko.observable(false);
        self.PEportSelected = ko.observable(false);

        self.CustomizedPort = ko.observable(false);
        self.CustomizedPort1 = ko.observable(false);
        self.vehiclepermit = ko.observable(new IPMSRoot.VehiclePermit());
        self.SubAccessAreasForRB = ko.observableArray();
        self.ViewSubAccessAreasForRB = ko.observableArray();
        self.isCmpnyTelephoneNoMsg = ko.observable(false);
        self.isAuthorisedTelephoneWorkMsg = ko.observable(false);
        self.isAuthorisedMobileMsg = ko.observable(false);
        self.isCntAuthorisedTelephoneWorkMsg = ko.observable(false);
        self.isCntAuthorisedMobileMsg = ko.observable(false);
        self.selectedAreaCode = ko.observableArray([]);
        self.permitsubrequesarea = ko.observable(new IPMSROOT.PermitRequestSubArea());
        self.IsIndiTemp = ko.observable(false);
        self.IsIndiPerm = ko.observable(false);
        self.IsContractorTemp = ko.observable(false);
        self.IsContractorPerm = ko.observable(false);
        self.isPermitNoMsg = ko.observable(false);
        self.isIndTempPermitNoMsg = ko.observable(false);
        self.isIndPermPermitNoMsg = ko.observable(false);
        self.istempToDateNoMsg = ko.observable(false);
        self.istempFromDateNoMsg = ko.observable(false);
        self.isIndPerFromDateNoMsg = ko.observable(false);
        self.isPerToDateNoMsg = ko.observable(false);
        self.isReappNoMsg = ko.observable(false);
        self.istrainingDateNoMsg = ko.observable(false);
        self.ispermitReasonNoMsg = ko.observable(false);
        self.ispermitAreaNoMsg = ko.observable(false);
        self.ispermitSubAreaNoMsg = ko.observable(false);
        self.isToolsNoMsg = ko.observable(false);
        self.isCameraNoMsg = ko.observable(false);
        self.isSpclEqupNoMsg = ko.observable(false);
        self.Choices = ko.observableArray([]);
        self.isTelephoneNumberNoMsg = ko.observable(false);
        self.isSubContractorTelephoneNumberNoMsg = ko.observable(false);
        self.isCntrTempPermitNoMsg = ko.observable(false);
        self.isCntrPermPermitNoMsg = ko.observable(false);
        self.isPermit1NoMsg = ko.observable(false);
        self.isCntrtempFromDateNoMsg = ko.observable(false);
        self.isCntrtempToDateNoMsg = ko.observable(false);
        self.isCntPerFromDateNoMsg = ko.observable(false);
        self.isCntPerToDateNoMsg = ko.observable(false);
        self.isCntpermitAreaNoMsg = ko.observable(false);
        self.isCntpermitSubAreaNoMsg = ko.observable(false);
        self.isCntpermitReasonNoMsg = ko.observable(false);
        self.isCntToolsNoMsg = ko.observable(false);
        self.isCntCameraNoMsg = ko.observable(false);
        self.isSACitizenMsg = ko.observable(false);
        self.isNoGenderMsg = ko.observable(false);
        self.isCurrPerMsg = ko.observable(false);
        self.isPrtIndMsg = ko.observable(false);
        self.isCrmBckMsg = ko.observable(false);
        self.isNoToolsMsg = ko.observable(false);
        self.isNoCameraMsg = ko.observable(false);
        self.isSpclEqpMsg = ko.observable(false);
        self.isCntNoCameraMsg = ko.observable(false);
        self.isCntNoToolsMsg = ko.observable(false);
        self.isCntSpclEqpMsg = ko.observable(false);
        self.PreAreaLength = ko.observable();
        self.CurrAreaLength = ko.observable();
        self.isAppliDateNoMsg = ko.observable(false);
        self.isSigDateNoMsg = ko.observable(false);
        self.isCntSigDateNoMsg = ko.observable(false);
        self.isCntSpclEqupNoMsg = ko.observable(false);
        self.isNoCountryOfOriginMsg = ko.observable(false);


        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadInitialData();
            self.GetFileSizeConfigValue();



            if (viewDetail == 1) {

            }
            else if (viewDetail == 2) {
                $('#spnTitle').html("Port Entry Pass Application");
                self.viewMode('List');
                self.LoadPermitRequestList();
                self.LoadInitialData();

            } else if (viewDetail == 3) {
                $('#spnTitle').html("SSA Security Check");
                self.viewMode('List');
                self.LoadPermitRequestListForSSA();
                self.LoadInitialData();
            }
            else if (viewDetail == 4) {
                $('#spnTitle').html("SAPS Security Check");
                self.viewMode('List');
                self.LoadPermitRequestListForSAPS();
                self.LoadInitialData();
            }
            else if (viewDetail == 5) {
                $('#spnTitle').html("Permit Office");
                self.viewMode('List');
                self.LoadApprovedPermitrequestlist();
                self.LoadInitialData();
            }
            var captachText = randString(6);
            self.CaptachText(captachText);
            captachText = randString(6);
            self.CaptachTextResubmit(captachText);
            captachText = randString(6);
            self.CaptachTextRejected(captachText);
            self.Choices = [
        { value: 'Y', text: "Yes" },
        { value: 'N', text: "No" }
            ];



        }
        //******************************************************************************************************************************************************************************************************************
        self.GetFileSizeConfigValue = function () {

            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {
                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
            });
        }
        self.LoadInitialData = function () {

            self.viewModelHelper.apiGet('api/PortEntryPassReferenceData', null,
                    function (result1) {
                        self.portentrypassapplicationreferencedata(new IPMSRoot.PortEntryPassApplicationReferenceData(result1));
                    }, null, null, false);
        }
        ChangeVesselNationality = function () {

            if ($("#ddlVesselNationality").val() == "") {
                $("#spanVesselNationality").text('This field is required.');

            }
            else {

                $("#spanVesselNationality").text('');

            }
        }

        self.LoadPortData = function () {

            self.viewModelHelper.apiGet('/api/Account/GetPortsForGatePass', null,
                    function (result1) {
                        //self.PortData(new IPMSRoot.PortEntryPassApplicationReferenceData(result1));
                        self.PortData(result1);
                    }, null, null, false);
        }


        self.GetDataClickPermitOffice = function (model) {

            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);
            self.LoadApprovedPermitrequestlist();

        }

        self.ClearFilterPermitOffice = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadApprovedPermitrequestlist();
        }


        self.GetDataClick = function (model) {

            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);
            self.LoadPermitRequestList();
        }
        self.GetDataClickSSA = function (model) {

            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);
            self.LoadPermitRequestListForSSA();


        }




        self.ClearFilterSSA = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadPermitRequestListForSSA();
        }

        self.GetDataClickSAPS = function (model) {

            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);

            self.LoadPermitRequestListForSAPS();


        }
        self.ClearFilterSAPS = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadPermitRequestListForSAPS();
        }

        self.ClearFilter = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadPermitRequestList();
        }

        self.LoadPermitRequestList = function () {

            // debugger;

            //self.viewModelHelper.apiPost('api/GetPortEntryPassRequestlistSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(data)
            //{
            //    toastr.options.closeButton = true;
            //    toastr.options.positionClass = "toast-top-right";
            //    toastr.success(" Permit Request Approve Details Submitted Successfully", "Port Entry Pass Request");
            //    self.LoadApprovedPermitrequestlist();
            //    self.viewMode('List');
            //    $('#spnTitle').html("Permit Office");

            //});



            //self.viewModelHelper.apiGet('api/GetPortEntryPassRequestlistSearch', null,
            //            function (result) {


            self.viewModelHelper.apiPost('api/GetPortEntryPassRequestlistSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
                if (result.length !== 0) {
                    if (result[0].PortCode == 'RB') {
                        self.RBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.CustomizedPort1(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                        //sri
                    else if (result[0].PortCode === 'CT') {
                        self.CTportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'DB') {
                        self.DBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'EL') {
                        self.ELportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.CTportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'NG') {
                        self.NGportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.CTportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'MB') {
                        self.MBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.CTportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'SB') {
                        self.SBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.CTportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else {
                        self.PEportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);


                    }
                }
                var supCatCode = null;
                self.permitrequestlistforapproval(ko.utils.arrayMap(result, function (item) {
                    //$.each(item.PermitRequestAreas, function (index, areadata) {
                    //    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                    //          function (result1) {
                    //              $.each(result1, function (index, resdata) {
                    //                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                    //              });

                    //          }, null, null, false);
                    //});
                    return new IPMSRoot.PortEntryPassApplicationModel(item);

                }));

            });
        }
        self.LoadPermitRequestListForSSA = function () {

            // nnnnnnnnnnn

            //self.viewModelHelper.apiGet('api/PermitRequestListForSSASearch', null,
            //              function (result) {    api/PermitRequestListForSSASearch
            self.viewModelHelper.apiPost('api/PermitRequestListForSSASearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
                if (result.length !== 0) {
                    if (result[0].PortCode == 'RB') {
                        self.RBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.CustomizedPort1(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                        //sri1
                    else if (result[0].PortCode === 'CT') {
                        self.CTportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'DB') {
                        self.DBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'EL') {
                        self.ELportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.CTportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'NG') {
                        self.NGportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.CTportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'MB') {
                        self.MBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.CTportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'SB') {
                        self.SBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.CTportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else {
                        self.PEportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);


                    }

                }
                var supCatCode = null;
                self.permitrequestlistForSSA(ko.utils.arrayMap(result, function (item) {
                    //$.each(item.PermitRequestAreas, function (index, areadata) {
                    //    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                    //          function (result1) {
                    //              $.each(result1, function (index, resdata) {
                    //                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                    //              });

                    //          }, null, null, false);
                    //});
                    return new IPMSRoot.PortEntryPassApplicationModel(item);
                }));
            });
        }
        self.LoadPermitRequestListForSAPS = function () {

            // self.viewModelHelper.apiGet('api/PermitRequestListForSAPS', null,
            //debugger;
            self.viewModelHelper.apiPost('api/PermitRequestListForSAPSSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
                // if (result.length !== 0) {
                // function (result) {
                if (result.length !== 0) {
                    if (result[0].PortCode == 'RB') {
                        self.RBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.CustomizedPort1(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                        //sri2
                    else if (result[0].PortCode === 'CT') {
                        self.CTportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'DB') {
                        self.DBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'EL') {
                        self.ELportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.CTportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'NG') {
                        self.NGportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.CTportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'MB') {
                        self.MBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.CTportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else if (result[0].PortCode === 'SB') {
                        self.SBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.CTportSelected(false);
                        self.NGportSelected(false);
                        self.PEportSelected(false);
                    }
                    else {
                        self.PEportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                        self.RBportSelected(false);
                        self.CTportSelected(false);
                        self.DBportSelected(false);
                        self.ELportSelected(false);
                        self.MBportSelected(false);
                        self.SBportSelected(false);
                        self.NGportSelected(false);


                    }

                }
                var supCatCode = null;

                self.permitrequestlistForSAPS(ko.utils.arrayMap(result, function (item) {
                    //$.each(item.PermitRequestAreas, function (index, areadata) {
                    //    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                    //          function (result1) {
                    //              $.each(result1, function (index, resdata) {
                    //                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                    //              });

                    //          }, null, null, false);
                    //});
                    return new IPMSRoot.PortEntryPassApplicationModel(item);
                }));
            });
        }
        //SRI3s
        self.LoadApprovedPermitrequestlist = function () {
            // BHOJI1
            //self.viewModelHelper.apiGet('api/ApprovedPermitrequestlist', null,
            //              function (result) {

            self.viewModelHelper.apiPost('api/ApprovedPermitrequestlistSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
                if (result.length !== 0) {


                    if (result[0].PortCode == 'RB') {
                        self.RBportSelected(true);
                        self.CustomizedPort(true);
                        self.CustomizedPort1(false);
                    } else {
                        self.RBportSelected(false);
                        self.CustomizedPort(false);
                        self.CustomizedPort1(true);
                    }
                }
                var supCatCode = null;

                self.permitrequestlist(ko.utils.arrayMap(result, function (item) {
                    //$.each(item.PermitRequestAreas, function (index, areadata) {
                    //    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                    //          function (result1) {
                    //              $.each(result1, function (index, resdata) {
                    //                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                    //              });

                    //          }, null, null, false);
                    //});
                    return new IPMSRoot.PortEntryPassApplicationModel(item);

                }));
            });
        }

        self.GetDetailsExistingPermitRequest = function (data) {

            //if ($('#portresubmit').get(0).selectedIndex != 0 && $('#portresubmit option:selected').text() != "") {
            if (data.PortCode() != "" && data.PortCode() != null && data.PortCode() != undefined) {
                if (data.ReferenceNo() != "" && data.ReferenceNo() != null && data.ReferenceNo() != undefined) {
                    if (self.isApplicationStaus() == 'Resub') {
                        self.portentrypassapplicationModel().Flag = 1;
                    }
                    else if (self.isApplicationStaus() == 'Appeal') {
                        self.portentrypassapplicationModel().Flag = 2;
                    }
                    var supCatCode = null;
                    self.viewModelHelper.apiGet('api/PermitRequestbyid', { refrenceNumber: data.ReferenceNo(), flag: data.Flag, portcode: data.PortCode() },
                                   function (result) {

                                       if (result.ReferenceNo != undefined || result.ReferenceNo != null) {
                                           if (self.isApplicationStaus() == 'Resub') {

                                               self.viewModelHelper.isLoading(false);
                                               self.resubmitted(true);

                                               //if (result.PersonalPermits.SpecificNPASites == null)
                                               //{ result.PersonalPermits.SpecifyArea = ""; }

                                               $.each(result.PermitRequestAreas, function (index, areadata) {
                                                   self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                                                         function (result1) {
                                                             $.each(result1, function (index, resdata) {
                                                                 self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                                                             });

                                                         }, null, null, false);
                                               });

                                               self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(result));


                                               $("#MobileNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyTelephoneNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyFaxNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#MobileNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyTelephoneNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#ContactTelephoneNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyFaxNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#ContactMobileNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyTelephoneNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyFaxNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#MobileNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyTelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyFaxNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#TelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#HometelephoneWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#MobileNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyMobileNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyFaxNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CompanyTelephoneNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#TelephoneNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#resubmissionreferenceno").attr("disabled", "disabled");
                                               $("#portresubmit").attr("disabled", "disabled");
                                               $("#resubmissionreferenceno").prop("disabled", true);
                                               $("#portresubmit").prop("disabled", true);

                                               //--------Indi Appli----------
                                               $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               // var IndiResubMobile = $("#IndividualApplicationMobileNoResub").data("kendoMaskedTextBox");
                                               //selfUser.ContactNo = ContactNomaskedtextbox.value();
                                               // self.portentrypassapplicationModel.MobileNo = IndiResubMobile.value();
                                               $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                                               //---------------Contractor appli
                                               $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                               $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                                               //$("#ContactNo").kendoMaskedTextBox({
                                               //    mask: "(000)000-000-0000"
                                               //});
                                               //var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");

                                               //self.portentrypassapplicationModel(data);

                                               $("#New").attr("disabled", "disabled");
                                               $("#New").prop("disabled", true);
                                               $("#Resub").attr("disabled", "disabled");
                                               $("#Resub").prop("disabled", true);
                                               $("#Appeal").attr("disabled", "disabled");
                                               $("#Appeal").prop("disabled", true);
                                           }
                                           else if (self.isApplicationStaus() == 'Appeal') {
                                               self.AppealOnRejected(true);
                                               var today = new Date();
                                               var createddate = moment(result.CreatedDate).format('YYYY-MM-DD ')
                                               var today = moment(new Date()).format('YYYY-MM-DD ');
                                               var daysdiff = moment.duration(moment(today).diff(createddate)).asDays();
                                               self.viewModelHelper.isLoading(false);
                                               if (daysdiff <= 7) {
                                                   $.each(result.PermitRequestAreas, function (index, areadata) {
                                                       self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                                                             function (result1) {
                                                                 $.each(result1, function (index, resdata) {
                                                                     self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                                                                 });

                                                             }, null, null, false);
                                                   });
                                                   self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(result));
                                                   $("#MobileNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyTelephoneNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyFaxNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#MobileNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyTelephoneNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#ContactTelephoneNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyFaxNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#ContactMobileNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyTelephoneNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyFaxNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#MobileNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyTelephoneNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyFaxNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#TelephoneNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#HometelephoneWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#MobileNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyMobileNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyFaxNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CompanyTelephoneNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#TelephoneNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#TelephoneNovisitorformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#referencenoappeal").attr("disabled", "disabled");
                                                   $("#portappeal").attr("disabled", "disabled");
                                                   $("#referencenoappeal").prop("disabled", true);
                                                   $("#portappeal").prop("disabled", true);

                                                   $("#New").attr("disabled", "disabled");
                                                   $("#New").prop("disabled", true);
                                                   $("#Resub").attr("disabled", "disabled");
                                                   $("#Resub").prop("disabled", true);
                                                   $("#Appeal").attr("disabled", "disabled");
                                                   $("#Appeal").prop("disabled", true);
                                                   //--------Indi Appli----------
                                                   $("#IndividualApplicationCmpTelephoneNoappel").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#IndividualApplicationMobileNoappel").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#AuthorisedTelephoneWorkappel").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#AuthorisedMobileappel").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                                                   //---------------Contractor appli
                                                   $("#TelephoneNumberappel1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#SubContractorTelephoneNumberaappel1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CntAuthorisedTelephoneWorkappel1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                                                   $("#CntAuthorisedMobileappel1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                                               }
                                               else {
                                                   self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(undefined));
                                                   self.viewModelHelper.isLoading(false);
                                                   toastr.options.closeButton = true;
                                                   toastr.options.positionClass = "toast-top-right";
                                                   toastr.warning("An Applicant Can Appeal On A Rejected Permit Within 7 Days of Rejection. You Have Exceeded The Maximum No. of Days For Appealing", "Port Entry Pass Request");
                                               }
                                           }


                                       }
                                       else {
                                           self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(undefined));
                                           self.viewModelHelper.isLoading(false);
                                           toastr.options.closeButton = true;
                                           toastr.options.positionClass = "toast-top-right";
                                           toastr.warning("No Records Found For This Reference Number", "Port Entry Pass Request");
                                       }
                                       self.viewModelHelper.isLoading(false);
                                   },
                                     function failure(result) {
                                         self.viewModelHelper.isLoading(false);
                                         toastr.error(result.responseText);
                                     },
                                                      function callbackloder(result) {

                                                      }
                                                     );
                    //self.portentrypassapplicationModel(new IPMSROOT.PortEntryPassApplicationModel(undefined)); 
                }
                else {
                    self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(undefined));
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Enter Your Port Entry Pass Application Number", "Port Entry Pass Request");
                }
            }
            else {
                self.portentrypassapplicationModel(new IPMSRoot.PortEntryPassApplicationModel(undefined));
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Select Your Port", "Port Entry Pass Request");

            }
        }

        //**************************************************************************************ChangeEndDateDefault********************************************************************************************************************

        ChangeEndDateDefault = function () {
            $("#datepickerE").val("");

            var StartDate = $("#datepickerS").val();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate() + 1;
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            $("#datepickerE").data('kendoDatePicker').min(new Date(year, month, day));
        }

        Checkdates = function (model) {
            var temppermit;
            if (model.permittype() == 'Inditemp') {
                if (model.IndividualTemporaryPermits() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select individual temporary permits", "Port Entry Pass Request");
                }
                else {
                    temppermit = model.IndividualTemporaryPermits();
                    self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                            function (result) {
                                if (result !== '') {
                                    if (result.indexOf('-') != -1) {
                                        var min = result.split('-')[0];
                                        var Compareparam = (result.split('-')[1]).split(" ");
                                        var max = Compareparam[0];
                                        var param = Compareparam[1];
                                        var date1 = new Date(model.TempFromDate());
                                        var date2 = new Date(model.TempToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.TempToDate('');
                                            }

                                        }
                                    } else {
                                        var min = 1;
                                        var str = result.split(" ");
                                        var max = str[1];
                                        var param = str[2];
                                        var date1 = new Date(model.TempFromDate());
                                        var date2 = new Date(model.TempToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.TempToDate('');
                                            }

                                        }


                                    }
                                }
                            });

                }
                if (model.TempFromDate() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select individual temporary from date", "Port Entry Pass Request");
                    model.TempToDate('');
                }
                else {

                }
            }

        }


        ChangeIndEndDateDefault = function () {
            $("#datepickerE1").val("");

            var StartDate = $("#datepickerS1").val();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate() + 1;
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            $("#datepickerE1").data('kendoDatePicker').min(new Date(year, month, day));
        }

        CheckInvPerdates = function (model) {
            var temppermit;
            if (model.permittype() == 'Indiper') {
                if (model.IndividualPermanentPermits() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select individual Permanent permits", "Port Entry Pass Request");
                }
                else {
                    temppermit = model.IndividualPermanentPermits();
                    self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                            function (result) {
                                if (result !== '') {
                                    if (result.indexOf('-') != -1) {
                                        var min = result.split('-')[0];
                                        var Compareparam = (result.split('-')[1]).split(" ");
                                        var max = Compareparam[0];
                                        var param = Compareparam[1];
                                        var date1 = new Date(model.PerFromDate());
                                        var date2 = new Date(model.PerToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }

                                        }
                                        else if (param === "Weeks") {
                                            var NewMin = min * 7;
                                            var NewMax = max * 7;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }
                                        }
                                        else if (param === "Years") {
                                            var NewMin = min * 365;
                                            var NewMax = max * 365;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }
                                        }
                                    } else {
                                        var min = 1;
                                        var str = result.split(" ");
                                        var max = str[1];
                                        var param = str[2];
                                        var date1 = new Date(model.PerFromDate());
                                        var date2 = new Date(model.PerToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }

                                        }
                                        else if (param === "Weeks") {
                                            var NewMin = min * 7;
                                            var NewMax = max * 7;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }
                                        }
                                        else if (param === "Years") {
                                            var NewMin = min * 365;
                                            var NewMax = max * 365;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.PerToDate('');
                                            }
                                        }


                                    }
                                }
                            });

                }
                if (model.PerFromDate() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select individual Permanent from date", "Port Entry Pass Request");
                    model.TempToDate('');
                }
                else {

                }
            }

        }



        ChangeCntTemEndDateDefault = function () {
            $("#datepickerE2").val("");

            var StartDate = $("#datepickerS2").val();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate() + 1;
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            $("#datepickerE2").data('kendoDatePicker').min(new Date(year, month, day));
        }

        CheckCntTempdates = function (model) {
            var temppermit;
            if (model.permittype() == 'contemp') {
                if (model.ContractorTemporaryPermits() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Contractor temporary permits", "Port Entry Pass Request");
                }
                else {
                    temppermit = model.ContractorTemporaryPermits();
                    self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                            function (result) {
                                if (result !== '') {
                                    if (result.indexOf('-') != -1) {
                                        var min = result.split('-')[0];
                                        var Compareparam = (result.split('-')[1]).split(" ");
                                        var max = Compareparam[0];
                                        var param = Compareparam[1];
                                        var date1 = new Date(model.ContractorTempFromDate());
                                        var date2 = new Date(model.ContractorTempToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.ContractorTempToDate('');
                                            }

                                        }
                                        else if (param === "Weeks") {
                                            var NewMin = min * 7;
                                            var NewMax = max * 7;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.ContractorTempToDate('');
                                            }
                                        }
                                    } else {
                                        var min = 1;
                                        var str = result.split(" ");
                                        if (str.length === 3) {
                                            var max = str[1];
                                            var param = str[2];
                                            var date1 = new Date(model.ContractorTempFromDate());
                                            var date2 = new Date(model.ContractorTempToDate());
                                            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                            //alert(diffDays);
                                            if (param === 'Days') {
                                                if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorTempToDate('');
                                                }

                                            }
                                            else if (param === "Weeks") {
                                                var NewMin = min * 7;
                                                var NewMax = max * 7;
                                                if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorTempToDate('');
                                                }
                                            }


                                        }
                                        else if (str.length === 2) {
                                            var max = str[0];
                                            var param = str[1];
                                            var date1 = new Date(model.ContractorTempFromDate());
                                            var date2 = new Date(model.ContractorTempToDate());
                                            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                            //alert(diffDays);
                                            if (param === 'Days') {
                                                if (diffDays !== parseInt(max)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorTempToDate('');
                                                }

                                            }
                                            else if (param === "Weeks") {
                                                var NewMin = min * 7;
                                                var NewMax = max * 7;
                                                if (diffDays !== parseInt(NewMax)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorTempToDate('');
                                                }
                                            }



                                        }
                                    }
                                }
                            });

                }
                if (model.ContractorTempFromDate() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Contractor temporary from date", "Port Entry Pass Request");
                    model.ContractorTempToDate('');
                }
                else {

                }
            }

        }


        ChangeCntPerEndDateDefault = function () {
            $("#datepickerE3").val("");

            var StartDate = $("#datepickerS3").val();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate() + 1;
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            $("#datepickerE3").data('kendoDatePicker').min(new Date(year, month, day));
        }

        CheckCntPerdates = function (model) {
            var temppermit;
            if (model.permittype() == 'conper') {
                if (model.ContractorPermanentPermits() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Contractor temporary permits", "Port Entry Pass Request");
                }
                else {
                    temppermit = model.ContractorPermanentPermits();
                    self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                            function (result) {
                                if (result !== '') {
                                    if (result.indexOf('-') != -1) {
                                        var min = result.split('-')[0];
                                        var Compareparam = (result.split('-')[1]).split(" ");
                                        var max = Compareparam[0];
                                        var param = Compareparam[1];
                                        var date1 = new Date(model.ContractorPerFromDate());
                                        var date2 = new Date(model.ContractorPerToDate());
                                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                        //alert(diffDays);
                                        if (param === 'Days') {
                                            if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.ContractorPerToDate('');
                                            }

                                        }
                                        else if (param === "Weeks") {
                                            var NewMin = min * 7;
                                            var NewMax = max * 7;
                                            if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                model.ContractorPerToDate('');
                                            }
                                        }
                                    } else {
                                        var min = 1;
                                        var str = result.split(" ");
                                        if (str.length === 3) {
                                            var max = str[1];
                                            var param = str[2];
                                            var date1 = new Date(model.ContractorPerFromDate());
                                            var date2 = new Date(model.ContractorPerToDate());
                                            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                            //alert(diffDays);
                                            if (param === 'Days') {
                                                if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorPerToDate('');
                                                }

                                            }
                                            else if (param === "Weeks") {
                                                var NewMin = min * 7;
                                                var NewMax = max * 7;
                                                if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorPerToDate('');
                                                }
                                            }


                                        }
                                        else if (str.length === 2) {
                                            var max = str[0];
                                            var param = str[1];
                                            var date1 = new Date(model.ContractorPerFromDate());
                                            var date2 = new Date(model.ContractorPerToDate());
                                            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                            //alert(diffDays);
                                            if (param === 'Days') {
                                                if (diffDays !== parseInt(max)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorPerToDate('');
                                                }

                                            }
                                            else if (param === "Weeks") {
                                                var NewMin = min * 7;
                                                var NewMax = max * 7;
                                                if (diffDays !== parseInt(NewMax)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorPerToDate('');
                                                }
                                            }
                                            else if (param === "Year") {
                                                var NewMin = min * 365;
                                                var NewMax = max * 365;
                                                if (diffDays !== parseInt(NewMax)) {
                                                    toastr.options.closeButton = true;
                                                    toastr.options.positionClass = "toast-top-right";
                                                    toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                    model.ContractorPerToDate('');
                                                }
                                            }



                                        }
                                    }
                                }
                            });

                }
                if (model.ContractorPerFromDate() === '') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Contractor Permanent from date", "Port Entry Pass Request");
                    model.ContractorPerToDate('');
                }
                else {

                }
            }

        }


        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.SelectedPort = function () {
            var portValue = $("#drpport").val();
            if (portValue != "") {
                self.isPortSelected(true);
                var myDatePicker = new Date();
                var day = myDatePicker.getDate();
                var month = myDatePicker.getMonth();
                var year = myDatePicker.getFullYear();

                $("#datepickerS").data('kendoDatePicker').min(new Date(year, month, day));
                $("#datepickerS1").data('kendoDatePicker').min(new Date(year, month, day));
                $("#datepickerS2").data('kendoDatePicker').min(new Date(year, month, day));
                $("#datepickerS3").data('kendoDatePicker').min(new Date(year, month, day));
                $('#Date').data('kendoDatePicker').min(new Date(year, month, day));
                //$('#Date_Signatory').data('kendoDatePicker').min(new Date(year, month, day));
                $('#Date_Signatory1').data('kendoDatePicker').min(new Date(year, month, day));
                if (portValue === 'RB') {
                    self.RBportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);

                }
                else if (portValue === 'CT') {
                    self.CTportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);

                }
                else if (portValue === 'DB') {
                    self.DBportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);

                }
                else if (portValue === 'EL') {
                    self.ELportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);
                }
                else if (portValue === 'MB') {
                    self.MBportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);
                }
                else if (portValue === 'SB') {
                    self.SBportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.NGportSelected(false);
                    self.PEportSelected(false);
                }
                else if (portValue === 'NG') {
                    self.NGportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.PEportSelected(false);

                }
                else {
                    self.PEportSelected(true);
                    self.CustomizedPort(true);
                    self.CustomizedPort1(false);
                    self.RBportSelected(false);
                    self.CTportSelected(false);
                    self.DBportSelected(false);
                    self.ELportSelected(false);
                    self.MBportSelected(false);
                    self.SBportSelected(false);
                    self.NGportSelected(false);
                }
                //if (portValue === 'RB') {
                //    self.CustomizedPort(true);
                //}
                //else {
                //    self.CustomizedPort(false);
                //}
            } else {
                self.isPortSelected(false);
            }
        }
        self.CancelPemitOffice = function () {

            $('#spnTitle').html("Permit Office");
            self.viewMode('List');
            self.IsCodeEnable(true);
            self.LoadApprovedPermitrequestlist();

        }
        self.viewApprovedportentrypassapplication = function (data) {

            self.viewMode('ApproveForm');
            self.IsCodeEnable(false);
            $('#spnTitle').html(" View Permit Office");
            self.portentrypassapplicationModel(data);
        }
        self.viewAppealApprovedportentrypassapplication = function (data) {

            //aaaaaaaaaaaaaaaaaaaaaaaaaabbb
            if (data.PermitRequestAreas().length > 0) {
                var supCatCode = null;
                $.each(data.PermitRequestAreas(), function (index, areadata) {
                    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                          function (result1) {
                              $.each(result1, function (index, resdata) {
                                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              });

                          }, null, null, false);
                });
            }

            self.IsCodeEnable(false);
            if (data.AppealBoardRemarks() != null && data.AppealBoardRemarks() != "" && data.AppealBoardRemarks() != undefined) {
                self.viewMode('AppealForm');
                $('#spnTitle').html(" View Permit Office");
                if (data.permitStatus() == "PAUP") {
                    data.Status = "N";
                }
                else if (data.permitStatus() == "PAAD" || data.permitStatus() == "PRIC") {
                    data.Status = "Y";
                }
                self.portentrypassapplicationModel(data);
                $("#MobileNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactMobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#HometelephoneWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyMobileNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNovisitorAppealForm").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            }
            else if (data.PermitRequestTypeCode() == "APCG") {
                self.viewMode('InternalempForm');
                $('#spnTitle').html(" View Permit Office");
                self.portentrypassapplicationModel(data);
                $("#mobilenoinpermitoffice").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#telephonenoinpermitoffice").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#faxnoinpermitoffice").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            }
            else {
                self.viewMode('ApproveForm');
                $('#spnTitle').html(" View Permit Office");
                self.portentrypassapplicationModel(data);
                $("#MobileNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactMobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#HometelephoneWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyMobileNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            }


        }
        self.viewpermitofficeInternalEmployeePermit = function (data) {

            self.viewMode('InternalempForm');
            self.IsCodeEnable(false);
            $('#spnTitle').html(" View Permit Office");
            self.portentrypassapplicationModel(data);
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.portentrypassapplicationModel().reset();
        }

        self.Cancelportentrypass = function () {
            self.viewMode('List');
            self.portentrypassapplicationModel().reset();
            $('#spnTitle').html("Port Entry Pass Application");
        }

        self.Resetportentrypass = function (data) {

            self.portentrypassapplicationModel().reset();

            $("#MobileNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        self.CancelPermitOffice = function () {
            self.viewMode('List');
            self.portentrypassapplicationModel().reset();
            $('#spnTitle').html("Permit Office");
        }
        self.SSACancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("SSA Security Check");
            self.portentrypassapplicationModel().reset();
        }
        self.SAPSCancel = function () {
            self.viewMode('List');
            $('#spnTitle').html("SAPS Security Check");
            self.portentrypassapplicationModel().reset();
        }
        self.cancelrequest = function () {
            window.location = '/Account/Login';

        }
        self.Reset = function (data) {
            self.portentrypassapplicationModel().reset();
        }
        self.SSAReset = function (data) {
            self.portentrypassapplicationModel().reset();
            $('#spnTitle').html("Edit SSA Security Check");
            $("#MobileNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }
        self.SAPSReset = function (data) {
            self.portentrypassapplicationModel().reset();
            $('#spnTitle').html("Edit SAPS Security Check");
            $("#MobileNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorSAPSSecurityCheck").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }
        self.editportentrypassapplication = function (data) {
            // EEEE
            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                          });

                      }, null, null, false);
            });


            self.viewMode('Form');
            $('#spnTitle').html("Port Entry Pass Application Approve");
            self.isSSAEnable(true);
            self.IsCodeEnable(false);

            self.portentrypassapplicationModel(data);
            self.isApplicationStaus() == 'Approval';

            $("#MobileNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }
        self.editSSAportentrypassapplication = function (data) {


            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                          });

                      }, null, null, false);
            });

            self.viewMode('Form');
            $('#spnTitle').html("Edit SSA Security Check");
            self.isSSAEnable(true);
            self.IsCodeEnable(false);
            self.viewModelHelper.apiGet('api/SSAandSAPSStatusbyid', { id: data.PermitRequestID, flag: 'PSSA' },
                             function (result) {

                                 if (result > 0) {
                                     $('#SSASave').prop('disabled', true);
                                     toastr.warning("This Record Is Already Verifyed ", "Port Entry Pass");

                                 }
                             });
            self.portentrypassapplicationModel(data);
            // self.isApplicationStaus() == 'Approval';            
            $("#MobileNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });



        }
        self.editSAPSportentrypassapplication = function (data) {
            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                          });

                      }, null, null, false);
            });

            self.viewMode('Form');
            $('#spnTitle').html("Edit SAPS Security Check");

            self.isSSAEnable(true);
            self.IsCodeEnable(false);
            self.viewModelHelper.apiGet('api/SSAandSAPSStatusbyid', { id: data.PermitRequestID, flag: 'SAPS' },
                          function (result) {

                              if (result > 0) {
                                  $('#SAPSSave').prop('disabled', true);
                                  toastr.warning("This Record Is Already Verifyed ", "Port Entry Pass");

                              }
                          });
            self.portentrypassapplicationModel(data);
            // self.isApplicationStaus() == 'Approval';

            $("#MobileNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorSAPSSecurityCheck").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


        }


        //var viewModel = {
        //    selectionChanged: function (event) {
        //        alert("the other selection changed");
        //    }
        //};
        self.viewSSAportentrypassapplication = function (data) {

            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                          });

                      }, null, null, false);
            });

            self.viewMode('Form');
            $('#spnTitle').html("View SSA Security Check");
            self.isSSAEnable(false);
            self.IsCodeEnable(false);

            self.portentrypassapplicationModel(data);

            $("#MobileNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSSASecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSSASecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSSASecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSSASecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }
        self.viewSAPSportentrypassapplication = function (data) {

            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                          });

                      }, null, null, false);
            });

            self.viewMode('Form');
            $('#spnTitle').html("View SAPS Security Check");
            self.isSSAEnable(false);
            self.IsCodeEnable(false);
            self.portentrypassapplicationModel(data);
            $("#MobileNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoSAPSSecurityCheckB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoSAPSSecurityCheckC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleSAPSSecurityCheckD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSAPSSecurityCheckE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorSAPSSecurityCheck").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }
        self.viewportentrypassapplication = function (data) {


            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              // data.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              //data.SubAccessAreasForRB.push(resdata);
                          });

                      }, null, null, false);
            });


            self.IsCodeEnable(false);
            if (data.AppealRemarks() != null && data.AppealRemarks() != "" && data.AppealRemarks() != undefined) {
                $('#spnTitle').html("View Appeal Board Discussion");
                self.viewMode('AppealForm');
                self.isSSAEnable(false);
                self.IsCodeEnable(false);
                self.isAppealenable(false);
                if (data.permitStatus() == "PAUP") {
                    data.Status = "N";
                }
                else if (data.permitStatus() == "PAAD" || data.permitStatus() == "PRIC") {
                    data.Status = "Y";
                }
                self.portentrypassapplicationModel(data);
                $("#MobileNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactMobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#HometelephoneWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyMobileNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNovisitorAppealForm").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                //--------Indi Appli----------
                $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                // var IndiResubMobile = $("#IndividualApplicationMobileNoResub").data("kendoMaskedTextBox");
                //selfUser.ContactNo = ContactNomaskedtextbox.value();
                // self.portentrypassapplicationModel.MobileNo = IndiResubMobile.value();
                $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                //---------------Contractor appli
                $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


            }
            else {
                self.viewMode('Form');
                self.isSSAEnable(false);
                self.IsCodeEnable(false);
                self.portentrypassapplicationModel(data);
                $('#spnTitle').html(" View Port Entry Pass Application");
                $("#MobileNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactTelephoneNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#ContactMobileNoApprovalB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoApprovalC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#HometelephoneWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#MobileNoWharfVehicleApprovalD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyMobileNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyFaxNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CompanyTelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNoApprovalE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


                //--------Indi Appli----------
                $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                // var IndiResubMobile = $("#IndividualApplicationMobileNoResub").data("kendoMaskedTextBox");
                //selfUser.ContactNo = ContactNomaskedtextbox.value();
                // self.portentrypassapplicationModel.MobileNo = IndiResubMobile.value();
                $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                //---------------Contractor appli
                $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            }
        }
        self.AppealBoardDiscuusion = function (data) {
            // DDDDD
            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              // data.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              //data.SubAccessAreasForRB.push(resdata);
                          });

                      }, null, null, false);
            });


            self.viewMode('AppealForm');
            $('#spnTitle').html("Port Entry Pass Application Appeal Approve");
            self.IsCodeEnable(false);
            self.isAppealenable(true);
            self.portentrypassapplicationModel(data);
            $("#MobileNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorAppealForm").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }
        self.viewAppealBoardDiscuusion = function (data) {
            self.viewMode('AppealForm');
            self.IsAppealCodeEnable(false);
            self.portentrypassapplicationModel(data);
            $('#spnTitle').html("Appeal Board Discussion");
            $("#MobileNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoAppealFormB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoAppealFormC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleAppealFormD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoAppealFormE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorAppealForm").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


            //--------Indi Appli----------
            $("#IndividualApplicationMobileNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationCmpTelephoneNoResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#AuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            //---------------Contractor appli
            $("#TelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#SubContractorTelephoneNumberResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWorkResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobileResub").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }
        self.PopUpEnable = function (data) {

            // cccc
            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              // data.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              //data.SubAccessAreasForRB.push(resdata);
                          });

                      }, null, null, false);
            });

            self.viewMode('PopUp');
            self.portentrypassapplicationModel(data);
        }


        self.Resetnewportentrypass = function (data) {
            var pcode = data.PortCode();
            self.portentrypassapplicationModel().reset();
            self.portentrypassapplicationModel().IndividualVehiclePermits.removeAll();
            self.portentrypassapplicationModel().ContractorPermitEmployeeDetails.removeAll();
            $('#drpport').val(pcode);
            if (pcode === "RB") {
                self.isPortSelected(true);
                self.RBportSelected(true);
                self.CustomizedPort(true);
                self.CustomizedPort1(false);
            }
            else {
                self.isPortSelected(true);
                self.RBportSelected(false);
                self.CustomizedPort(false);
                self.CustomizedPort1(true);
            }

            //self.isApplicationStaus('');
            $("#txtCaptachCode").val('');
            $("#CompanyTelephoneNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#IndividualApplicationMobileNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedTelephoneWork").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CntAuthorisedMobile").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("SubContractorTelephoneNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("TelephoneNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("AuthorisedMobile").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("AuthorisedTelephoneWork").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("IndividualApplicationCmpTelephoneNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("IndividualApplicationMobileNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


            $("#MobileNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#ConteractMobileNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#CompanyTelephoneNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#CompanyTelephoneNonewD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNonewD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#NewTelephoneNoD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneNEWD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoNEWD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#MobileNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            data.errors.showAllMessages(false);
            self.ispermitrequirementWharfvechiclenewDMsg(false);
            //self.isPortSelected(false);
            //self.RBportSelected(false);           
            self.isCmpnyTelephoneNoMsg(false);
            self.isAuthorisedTelephoneWorkMsg(false);
            self.isAuthorisedMobileMsg(false);
            self.isCntAuthorisedTelephoneWorkMsg(false);
            self.isCntAuthorisedMobileMsg(false);
            self.IsIndiTemp(false);
            self.IsIndiPerm(false);
            self.IsContractorTemp(false);
            self.IsContractorPerm(false);
            self.isPermitNoMsg(false);
            self.isIndTempPermitNoMsg(false);
            self.isIndPermPermitNoMsg(false);
            self.istempToDateNoMsg(false);
            self.istempFromDateNoMsg(false);
            self.isIndPerFromDateNoMsg(false);
            self.isPerToDateNoMsg(false);
            self.isReappNoMsg(false);
            self.istrainingDateNoMsg(false);
            self.ispermitReasonNoMsg(false);
            self.ispermitAreaNoMsg(false);
            self.ispermitSubAreaNoMsg(false);
            self.isToolsNoMsg(false);
            self.isCameraNoMsg(false);
            self.isSpclEqupNoMsg(false);
            self.isTelephoneNumberNoMsg(false);
            self.isSubContractorTelephoneNumberNoMsg(false);
            self.isCntrTempPermitNoMsg(false);
            self.isCntrPermPermitNoMsg(false);
            self.isPermit1NoMsg(false);
            self.isCntrtempFromDateNoMsg(false);
            self.isCntrtempToDateNoMsg(false);
            self.isCntPerFromDateNoMsg(false);
            self.isCntPerToDateNoMsg(false);
            self.isCntpermitAreaNoMsg(false);
            self.isCntpermitSubAreaNoMsg(false);
            self.isCntpermitReasonNoMsg(false);
            self.isCntToolsNoMsg(false);
            self.isCntCameraNoMsg(false);
            self.isSACitizenMsg(false);
            self.isNoGenderMsg(false);
            self.isCurrPerMsg(false);
            self.isPrtIndMsg(false);
            self.isCrmBckMsg(false);
            self.isNoToolsMsg(false);
            self.isNoCameraMsg(false);
            self.isSpclEqpMsg(false);
            self.isCntNoCameraMsg(false);
            self.isCntNoToolsMsg(false);
            self.isCntSpclEqpMsg(false);
        }

        self.Resetresubmissionnew = function (data) {
            //data.errors.showAllMessages(false);

            self.portentrypassapplicationModel().reset();
            self.portentrypassapplicationModel().IndividualVehiclePermits.removeAll();
            self.portentrypassapplicationModel().ContractorPermitEmployeeDetails.removeAll();
            $('#drpport').val(pcode);
            if (pcode === "RB") {
                self.isPortSelected(true);
                self.RBportSelected(true);
                self.CustomizedPort(true);
                self.CustomizedPort1(false);
            }
            else {
                self.isPortSelected(true);
                self.RBportSelected(false);
                self.CustomizedPort(false);
                self.CustomizedPort1(true);
            }
            //self.isApplicationStaus('');
            $("#txtCaptachCode").val('');
            $("#MobileNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoResubmissionA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoResubmissionB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoResubmissionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleResubmissionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoResubmissionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            self.isPermitRequeirementstypeswharfResubmissionMsg(false);
            self.isCmpnyTelephoneNoMsg(false);
            self.isAuthorisedTelephoneWorkMsg(false);
            self.isAuthorisedMobileMsg(false);
            self.isCntAuthorisedTelephoneWorkMsg(false);
            self.isCntAuthorisedMobileMsg(false);
            self.IsIndiTemp(false);
            self.IsIndiPerm(false);
            self.IsContractorTemp(false);
            self.IsContractorPerm(false);
            self.isPermitNoMsg(false);
            self.isIndTempPermitNoMsg(false);
            self.isIndPermPermitNoMsg(false);
            self.istempToDateNoMsg(false);
            self.istempFromDateNoMsg(false);
            self.isIndPerFromDateNoMsg(false);
            self.isPerToDateNoMsg(false);
            self.isReappNoMsg(false);
            self.istrainingDateNoMsg(false);
            self.ispermitReasonNoMsg(false);
            self.ispermitAreaNoMsg(false);
            self.ispermitSubAreaNoMsg(false);
            self.isToolsNoMsg(false);
            self.isCameraNoMsg(false);
            self.isSpclEqupNoMsg(false);
            self.isTelephoneNumberNoMsg(false);
            self.isSubContractorTelephoneNumberNoMsg(false);
            self.isCntrTempPermitNoMsg(false);
            self.isCntrPermPermitNoMsg(false);
            self.isPermit1NoMsg(false);
            self.isCntrtempFromDateNoMsg(false);
            self.isCntrtempToDateNoMsg(false);
            self.isCntPerFromDateNoMsg(false);
            self.isCntPerToDateNoMsg(false);
            self.isCntpermitAreaNoMsg(false);
            self.isCntpermitSubAreaNoMsg(false);
            self.isCntpermitReasonNoMsg(false);
            self.isCntToolsNoMsg(false);
            self.isCntCameraNoMsg(false);
            self.isSACitizenMsg(false);
            self.isNoGenderMsg(false);
            self.isCurrPerMsg(false);
            self.isPrtIndMsg(false);
            self.isCrmBckMsg(false);
            self.isNoToolsMsg(false);
            self.isNoCameraMsg(false);
            self.isSpclEqpMsg(false);
            self.isCntNoCameraMsg(false);
            self.isCntNoToolsMsg(false);
            self.isCntSpclEqpMsg(false);
        }

        self.ResetNewAppeal = function (data) {
            self.portentrypassapplicationModel().reset();
            $("#MobileNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoformaskAppealA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactTelephoneNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactMobileNoformaskAppealB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoformaskAppealC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#HometelephoneWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MobileNoWharfVehicleformaskAppealD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyMobileNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNovisitorformaskAppealE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            self.isPermitRequeirementstypeswharfResubmissionMsg(false);
        }
        //*********************file upload for new &resubmission& applead*********************************************************************************************************************************************************************************
        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFile = function () {

            if ($('#selUploadDocs1').get(0).selectedIndex == 0) {
                toastr.warning("Please Select Document Type.");
                self.ismultiplepfileToUpload(true);
                $('#spanmultiplepfileToUpload11').text("Please Select The Document Type");
                return;
            }
            else {
                self.ismultiplepfileToUpload(false);
                $("#spanmultiplepfileToUpload11").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs1 option:selected').text();
                // //alert(documentType);
                uploadedFiles = self.portentrypassapplicationModel().UploadedFiles();
                var opmlFile = $('#fileToUpload1')[0];

                if (opmlFile.files.length > 0) {
                    $("#spanNewFileToUpload").text('');
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.portentrypassapplicationModel().PermitRequestDocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            var fileSizeInBytes = opmlFile.files[i].size;
                            var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                            if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                                if ($.inArray(extension, fileExtension) != -1) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.FileSize = opmlFile.files[i].size;
                                    elem.CategoryName = $('#selUploadDocs1 option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs1 option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.portentrypassapplicationModel().UploadedFiles(uploadedFiles);
                                } else {
                                    toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("The " + opmlFile.files[i].name + " File Size is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "Warning");
                                return;
                            }
                        }
                        else {
                            toastr.warning("The Selected File Already Exist, Please Upload Another File", "Warning");
                            return;
                        }

                    }


                    var formData = new FormData();
                    $.each(self.portentrypassapplicationModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });


                    var CategoryCode = $('#selUploadDocs1 option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {

                                var Adddoc = new IPMSROOT.PermitRequestDocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                //Adddoc.deletevisable(true);
                                self.portentrypassapplicationModel().PermitRequestDocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    toastr.warning("Please Select File For Upload ", "Error");
                    $("#spanNewFileToUpload").text('Please Select File');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs1').val('');
                self.portentrypassapplicationModel().UploadedFiles([]);
                $('#fileToUpload1').val('');
                return;
                //}

            }
        }
        //-------------------------------  
        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFileforAppealeddocument = function () {

            if ($('#selUploadDocs3').get(0).selectedIndex == 0) {
                toastr.warning("Please Select Document Type.");
                self.ismultiplepfileToUpload(true);
                $('#spanmultiplepfileToUpload').text("Please Select The Document Type");
                return;
            }
            else {
                self.ismultiplepfileToUpload(false);
                $("#spanmultiplepfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs3 option:selected').text();
                // //alert(documentType);
                uploadedFiles = self.portentrypassapplicationModel().UploadedFiles();
                var opmlFile = $('#fileToUpload3')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.portentrypassapplicationModel().PermitRequestDocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.FileSize = opmlFile.files[i].size;
                                    elem.CategoryName = $('#selUploadDocs3 option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs3 option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.portentrypassapplicationModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " File Size is Exceeded the Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                return;
                            }
                        }
                        else {
                            toastr.error("The Selected File Already Exist, Please Upload Another File", "Error");
                            return;
                        }

                    }
                    var formData = new FormData();
                    $.each(self.portentrypassapplicationModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });


                    var CategoryCode = $('#selUploadDocs3 option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {

                                var Adddoc = new IPMSROOT.PermitRequestDocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                Adddoc.deletevisable(true);
                                self.portentrypassapplicationModel().PermitRequestDocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    $("#spanHWPSfileToUpload").text('Please Select File');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs3').val('');
                self.portentrypassapplicationModel().UploadedFiles([]);
                $('#fileToUpload3').val('');
                return;
                //}

            }
        }
        //-------------------------------   
        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFileforresubmission = function () {

            if ($('#selUploadDocs2').get(0).selectedIndex == 0) {
                toastr.warning("Please Select Document Type.");
                self.ismultiplepfileToUpload(true);
                $('#spanmultiplepfileToUpload21').text("Please Select The Document Type");
                return;
            }
            else {
                $("#spanmultiplepfileToUpload21").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs2 option:selected').text();
                // //alert(documentType);
                uploadedFiles = self.portentrypassapplicationModel().UploadedFiles();
                var opmlFile = $('#fileToUpload2')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.portentrypassapplicationModel().PermitRequestDocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.FileSize = opmlFile.files[i].size;
                                    elem.CategoryName = $('#selUploadDocs2 option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs2 option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.portentrypassapplicationModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.warning("The " + opmlFile.files[i].name + " File Size is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "Warning");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                return;
                            }
                        }
                        else {
                            toastr.error("The Selected File Already Exist, Please Upload Another File", "Error");
                            return;
                        }

                    }
                    var formData = new FormData();
                    $.each(self.portentrypassapplicationModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });


                    var CategoryCode = $('#selUploadDocs2 option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {

                                var Adddoc = new IPMSROOT.PermitRequestDocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                //Adddoc.deletevisable(true);
                                self.portentrypassapplicationModel().PermitRequestDocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    $("#spanmultiplepfileToUpload22").text('Please Select File');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs2').val('');
                self.portentrypassapplicationModel().UploadedFiles([]);
                $('#fileToUpload2').val('');
                return;
                //}

            }
        }
        //---------


        //******************************************************************************************************************************************************************************************************
        //*******************************************************************************************************************************************************************************************************
        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFileforssaverificationssa = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.warning("Please Select Document Type.");
                return;
            }
            else {
                $("#spanHWPSfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                // //alert(documentType);
                uploadedFiles = self.portentrypassapplicationModel().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.portentrypassapplicationModel().PermitRequestverifyedbySSADocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.FileSize = opmlFile.files[i].size;
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.portentrypassapplicationModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                return;
                            }
                        }
                        else {
                            toastr.error("The selected file already exist, Please upload another file", "Error");
                            return;
                        }

                    }
                    var formData = new FormData();
                    $.each(self.portentrypassapplicationModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });


                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {

                                var Adddoc = new IPMSROOT.PermitRequestverifyedbySSADocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                self.portentrypassapplicationModel().PermitRequestverifyedbySSADocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    $("#spanHWPSfileToUpload").text('Please select file');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs').val('');
                self.portentrypassapplicationModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
                //}

            }
        }

        self.multipleDeleteDocumentforssaverificationssa = function (Adddoc) {
            self.portentrypassapplicationModel().PermitRequestverifyedbySSADocuments.remove(Adddoc);
        }

        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFileforssaverificationSAPS = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.warning("Please Select Document Type.");
                return;
            }
            else {
                $("#spanHWPSfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                // //alert(documentType);
                uploadedFiles = self.portentrypassapplicationModel().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.portentrypassapplicationModel().PermitRequestverifyedbySAPSDocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.FileSize = opmlFile.files[i].size;
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.portentrypassapplicationModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }

                            }
                            else {
                                toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                return;
                            }

                        }
                        else {
                            toastr.error("The selected file already exist, Please upload another file", "Error");
                            return;
                        }
                    }

                    var formData = new FormData();
                    $.each(self.portentrypassapplicationModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });


                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {

                                var Adddoc = new IPMSROOT.PermitRequestverifyedbySAPSDocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                self.portentrypassapplicationModel().PermitRequestverifyedbySAPSDocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    $("#spanHWPSfileToUpload").text('Please select file');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs').val('');
                self.portentrypassapplicationModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
                //}

            }
        }

        self.multipleDeleteDocumentforssaverificationsapa = function (Adddoc) {
            self.portentrypassapplicationModel().PermitRequestverifyedbySAPSDocuments.remove(Adddoc);
        }

        //*******************************************************************************************************************************************************************************************************


        self.ApprovePermitRequest = function (data) {

            self.viewModelHelper.apiPost('api/IssuePortEntryPass', ko.mapping.toJSON(data), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success(" Permit Request Approve Details Submitted Successfully", "Port Entry Pass Request");
                self.LoadApprovedPermitrequestlist();
                self.viewMode('List');
                $('#spnTitle').html("Permit Office");

            });
        }
        self.Resubmitteddata = function (data) {

            if (data.PSOremarkes() != "" && data.PSOremarkes() != null && data.PSOremarkes() != undefined) {
                self.viewModelHelper.apiPost('api/PermitRequestupdate', ko.mapping.toJSON(data), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Port Entry Pass Request  Reverted Details Submitted Successfully", "Port Entry Pass Request");
                    self.LoadPermitRequestList();
                    self.viewMode('List');
                    $('#spnTitle').html("Port Entry Pass Application");
                });
            } else {

                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Enter The Remarks To Revert The Port Entry Pass Request ", "Port Entry Pass Request");
                self.LoadPermitRequestList();
                self.viewMode('List');
                $('#spnTitle').html("Port Entry Pass Application");
            }
        }
        self.ForwordToSecurityCheck = function (data) {
            //FFFFFF
            var supCatCode = null;
            $.each(data.PermitRequestAreas(), function (index, areadata) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                      function (result1) {
                          $.each(result1, function (index, resdata) {
                              // data.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                              //data.SubAccessAreasForRB.push(resdata);
                          });

                      }, null, null, false);
            });

            self.viewModelHelper.apiPost('api/PermitRequestforword', ko.mapping.toJSON(data), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Forwarded To Security Check. Details Submitted Successfully", "Port Entry Pass Request");
                self.LoadPermitRequestList();
                self.viewMode('List');
                //setTimeout(function () {
                //    window.location = '/Dashboard';
                //}, 4000);
            });
        }

        self.saveApprovaldetails = function (portentrypass) {


            if (portentrypass.PSOremarkes() != "" && portentrypass.PSOremarkes() != null && portentrypass.PSOremarkes() != undefined) {
                self.portentrypassapplicationModel().Flag = 1;
                portentrypass.Status = 'Y';
                self.viewModelHelper.apiPost('api/ApprovePermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Port Entry Pass Request Approval Details Submitted Successfully", "Port Entry Pass Request");
                    self.LoadPermitRequestList();
                    self.viewMode('List');
                    $('#spnTitle').html("Port Entry Pass Application");

                });
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Enter Your Remarks/Comments", "Port Entry Pass Request");
            }
        }
        self.saveAppealApprovedetails = function (portentrypass) {

            if (portentrypass.AppealBoardRemarks() != "" && portentrypass.AppealBoardRemarks() != null && portentrypass.AppealBoardRemarks() != undefined) {
                if (portentrypass.Status() != "" && portentrypass.Status() != null && portentrypass.Status() != undefined) {
                    self.viewModelHelper.apiPost('api/AppealApprovePermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Port Entry Pass Request Appeal Approval Details Submitted Successfully", "Port Entry Pass Request");
                        self.LoadPermitRequestList();
                        self.viewMode('List');
                        $('#spnTitle').html("Port Entry Pass Application");

                    });
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Check Approve Or UpHeld", "Port Entry Pass Request");
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Enter Your Remarks/Comments", "Port Entry Pass Request");
            }
        }
        self.saveRejecteddetails = function (portentrypass) {

            if (portentrypass.PSOremarkes() != "" && portentrypass.PSOremarkes() != null && portentrypass.PSOremarkes() != undefined) {
                if (portentrypass.Status() != null || portentrypass.Status() != "" || portentrypass.Status() != undefined) {
                    self.portentrypassapplicationModel().Flag = 2;
                    portentrypass.Status = 'N';
                    self.viewModelHelper.apiPost('api/ApprovePermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Port Entry Pass Request Rejected Details Submitted Successfully", "Port Entry Pass Request");
                        self.LoadPermitRequestList();
                        self.viewMode('List');
                        $('#spnTitle').html("Port Entry Pass Application");

                    });
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Check Your Remarks/Comments", "Port Entry Pass Request");
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Enter Your Remarks/Comments", "Port Entry Pass Request");
            }
        }
        self.saveSSAverificationdetails = function (portentrypass) {
            //if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().CreminalCheck() != "N") {

            //    if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() == "" || portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() == null || portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() == undefined) {

            //        if (portentrypass.PermitRequestverifyedbySSADocuments().length == 0) {

            //            self.portentrypassapplicationModel().Flag = 1;
            //            self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {

            //                toastr.options.closeButton = true;
            //                toastr.options.positionClass = "toast-top-right";
            //                toastr.success("SSA Verification Details Submitted Successfully", "Port Entry Pass Request");

            //                self.LoadPermitRequestListForSSA();
            //                $('#spnTitle').html("SSA Security Check");
            //                self.viewMode('List');
            //            });
            //        }
            //    }
            //}
            if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().CreminalCheck() != "N") {
                if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != "" || portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != null || portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != undefined) {
                    if (portentrypass.PermitRequestverifyedbySSADocuments().length >= 0) {

                        self.portentrypassapplicationModel().Flag = 1;
                        self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {

                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("SSA Verification Details Submitted Successfully", "Port Entry Pass Request");

                            self.LoadPermitRequestListForSSA();
                            $('#spnTitle').html("SSA Security Check");
                            self.viewMode('List');
                        });
                    }
                }
            }
                //else {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Please Upload Security Check Documents", "Port Entry Pass Request");
                //}

            else if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().CreminalCheck() == "N") {
                if (portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != "" && portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != null && portentrypass.PermitRequestVerifyedDetailsverifyedbySSA().Comments() != undefined) {
                    if (portentrypass.PermitRequestverifyedbySSADocuments().length >= 0) {

                        self.portentrypassapplicationModel().Flag = 1;
                        self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {

                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("SSA Verification Details Submitted Successfully", "Port Entry Pass Request");

                            self.LoadPermitRequestListForSSA();
                            $('#spnTitle').html("SSA Security Check");
                            self.viewMode('List');
                        });
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Enter Your Comments", "Port Entry Pass Request");
                }
            }


        }


        //selectionChanged = function () {
        // , event: { change:selectionChanged }
        // if ($("#ddlVesselNationality").val() == "") {
        //$("#spanVesselNationality").text('This field is required.');    
        //       //}
        //       //else {

        //       //    $("#spanVesselNationality").text('');

        //       //}
        // }
        //self.selectionChanged = function (data, event) {
        //}

        //self.selectionChanged = function (event) {

        //}
        self.saveSAPSverificationdetails = function (portentrypass) {

            //if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().CreminalCheck() != "N") {

            //    if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() == "" || portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() == null || portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() == undefined) {

            //        if (portentrypass.PermitRequestverifyedbySAPSDocuments().length == 0) {

            //            self.portentrypassapplicationModel().Flag = 2;
            //            self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
            //                toastr.options.closeButton = true;
            //                toastr.options.positionClass = "toast-top-right";
            //                toastr.success("SAPS Verification Details  Submitted Successfully", "Port Entry Pass Request");
            //                self.LoadPermitRequestListForSAPS();
            //                $('#spnTitle').html("SAPS Security Check");
            //                self.viewMode('List');
            //            });
            //        }
            //    }
            //}
            if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().CreminalCheck() != "N") {
                if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != "" || portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != null || portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != undefined) {
                    if (portentrypass.PermitRequestverifyedbySAPSDocuments().length >= 0) {

                        self.portentrypassapplicationModel().Flag = 2;
                        self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("SAPS Verification Details  Submitted Successfully", "Port Entry Pass Request");
                            self.LoadPermitRequestListForSAPS();
                            $('#spnTitle').html("SAPS Security Check");
                            self.viewMode('List');
                        });
                    }
                }
            }
                //else {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Please Upload Security Check Documents", "Port Entry Pass Request");
                //}

            else if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().CreminalCheck() == "N") {
                if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != "" && portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != null && portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != undefined) {
                    if (portentrypass.PermitRequestverifyedbySAPSDocuments().length >= 0) {

                        self.portentrypassapplicationModel().Flag = 2;
                        self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("SAPS Verification Details  Submitted Successfully", "Port Entry Pass Request");
                            self.LoadPermitRequestListForSAPS();
                            $('#spnTitle').html("SAPS Security Check");
                            self.viewMode('List');
                        });
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Enter Your Comments", "Port Entry Pass Request");
                }
            }

            //if (portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != "" && portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != null && portentrypass.PermitRequestVerifyedDetailsverifyedbySAPS().Comments() != undefined) {
            //    if (portentrypass.PermitRequestverifyedbySAPSDocuments().length > 0) {
            //        self.portentrypassapplicationModel().Flag = 2;
            //        self.viewModelHelper.apiPost('api/ADDPermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
            //            toastr.options.closeButton = true;
            //            toastr.options.positionClass = "toast-top-right";
            //            toastr.success("SAPS Verification Details  Submitted Successfully", "Port Entry Pass Request");
            //            self.LoadPermitRequestListForSAPS();
            //            $('#spnTitle').html("SAPS Security Check");
            //            self.viewMode('List');
            //        });
            //    }
            //    else {
            //        toastr.options.closeButton = true;
            //        toastr.options.positionClass = "toast-top-right";
            //        toastr.warning("Please Upload Security Check Documents", "Port Entry Pass Request");
            //    }
            //}
            //else {
            //    toastr.options.closeButton = true;
            //    toastr.options.positionClass = "toast-top-right";
            //    toastr.warning("Please Enter Your Comments", "Port Entry Pass Request");
            //}
        }

        self.saveportentrypassapplication = function (portentrypass) {


            if (portentrypass.PermitRequestID() == 0) {
                //if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCF' && self.RBportSelected) {
                //    var permitreqSubArealist = [];

                //    var length = portentrypass.PermitRequestSubAreas1().length;
                //    for (var i = 0; i < length; i++) {
                //        var permitreqSubArea = new IPMSRoot.PermitRequestSubArea();
                //        var SlipVar = portentrypass.PermitRequestSubAreas1()[i].split('_');
                //        permitreqSubArea.PermitRequestAreaCode(SlipVar[1]);
                //        permitreqSubArea.PermitRequestSubAreaCode(SlipVar[0]);
                //        permitreqSubArealist.push(permitreqSubArea);
                //    }
                //    //for (var j = 0; j < permitreqSubArealist.length; j++) {
                //    portentrypass.PermitRequestSubAreas(permitreqSubArealist);
                //    //}
                //}
                //if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCH' && self.RBportSelected) {
                //    var permitreqSubArealist = [];

                //    var length = portentrypass.PermitRequestSubAreas1().length;
                //    for (var i = 0; i < length; i++) {
                //        var permitreqSubArea = new IPMSRoot.PermitRequestSubArea();
                //        var SlipVar = portentrypass.PermitRequestSubAreas1()[i].split('_');
                //        permitreqSubArea.PermitRequestAreaCode(SlipVar[1]);
                //        permitreqSubArea.PermitRequestSubAreaCode(SlipVar[0]);
                //        permitreqSubArealist.push(permitreqSubArea);
                //    }
                //    portentrypass.PermitRequestSubAreas(permitreqSubArealist);
                // }
                if (formvalidation(portentrypass) <= true) {
                    if (portentrypass.PermitRequestDocuments().length <= 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Upload  Atleast One Document", "Port Entry Pass Request");
                        return;
                    }


                    if ($('input[name="portentry"]:checked').val() == "New") {
                        if ($("#txtCaptachCode").val() != "") {

                            if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                                $("#spanCaptachCode").text('');
                            }
                            else {
                                $("#spanCaptachCode").text('* Invalid security code');
                                var captachText = randString(6);
                                self.CaptachText(captachText);
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("You Have Some Form Errors. Please Check Below.");
                                return;
                            }
                        }
                        else {
                            $("#spanCaptachCode").text('* This field is required');
                            var captachText = randString(6);
                            self.CaptachText(captachText);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("You Have Some Form Errors. Please Check Below.");
                            return;
                        }


                    }
                    self.viewModelHelper.apiPost('api/PermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("New Port Entry Pass Request Details Submitted Successfully", "Port Entry Pass Request");
                        setTimeout(function () {
                            window.location = '/Account/Login';
                        }, 4000);
                    });
                }

            }
            else if (portentrypass.PermitRequestID() > 0) {
                if (Resubmssionformvalidation(portentrypass) == true) {
                    if (self.isApplicationStaus() == 'Resub') {
                        portentrypass.Flag = 1;
                    }
                    else if (self.isApplicationStaus() == 'Appeal') {
                        if (portentrypass.AppealRemarks() == "") {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("Please Fill Detailed Reason To Substantial Appeal Application In The Form", "Port Entry Pass Request");
                            return;
                        }
                        portentrypass.Flag = 2;
                    }
                    if ($('input[name="portentry"]:checked').val() == "Resub") {

                        if ($("#txtCaptachCodeResubmit").val() != "") {

                            if ($("#txtCaptachCodeResubmit").val() == $("#lblCaptachResubmit").text()) {
                                $("#spanCaptachCodeResubmit").text('');
                            }
                            else {
                                $("#spanCaptachCodeResubmit").text('* Invalid security code');
                                var captachText = randString(6);
                                self.CaptachTextResubmit(captachText);
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("You Have Some Form Errors. Please Check Below.");
                                return;
                            }
                        }
                        else {
                            $("#spanCaptachCodeResubmit").text('* This field is required');
                            var captachText = randString(6);
                            self.CaptachTextResubmit(captachText);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("You Have Some Form Errors. Please Check Below.");
                            return;
                        }
                    }
                    else if ($('input[name="portentry"]:checked').val() == "Appeal") {
                        if ($("#txtCaptachCodeRejected").val() != "") {

                            if ($("#txtCaptachCodeRejected").val() == $("#lblCaptachRejected").text()) {
                                $("#spanCaptachCodeRejected").text('');
                            }
                            else {
                                $("#spanCaptachCodeRejected").text('* Invalid security code');
                                var captachText = randString(6);
                                self.CaptachTextRejected(captachText);
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("You Have Some Form Errors. Please Check Below.");
                                return;
                            }
                        }
                        else {
                            $("#spanCaptachCodeRejected").text('* This field is required');
                            var captachText = randString(6);
                            self.CaptachTextRejected(captachText);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("You Have Some Form Errors. Please Check Below.");
                            return;
                        }
                    }
                    else {
                        return;
                    }
                    if (portentrypass.PermitRequestDocuments().length <= 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Upload  Atleast One Document", "Port Entry Pass Request");
                        return;
                    }
                    self.viewModelHelper.apiPut('api/PermitRequest', ko.mapping.toJSON(portentrypass), function Message(data) {
                        if ($('input[name="portentry"]:checked').val() == "Resub") {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("ReSubmitted Port Entry Pass Request Details Submitted Successfully", "Port Entry Pass Request");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        }
                        else if ($('input[name="portentry"]:checked').val() == "Appeal") {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Appealed Port Entry Pass Request Details Submitted Successfully", "Port Entry Pass Request");
                        }
                        setTimeout(function () {
                            window.location = '/Account/Login';
                        }, 4000);
                    });
                }

            }

        }
        self.CurrentPermitChnage = function () {
            self.portentrypassapplicationModel().IndividualPermitApplicationDetails().Reason_Reapplication('');
            return true;
        }
        self.CurrentIndTraining = function () {
            self.portentrypassapplicationModel().IndividualPermitApplicationDetails().Training_Date('');
            return true;
        }
        self.IsToolsChanged = function () {
            self.portentrypassapplicationModel().IndividualPersonalPermits().ToolsDetails('');
            return true;
        }
        self.IsCameraChanged = function () {
            self.portentrypassapplicationModel().IndividualPersonalPermits().CameraDetails('');
            return true;
        }
        self.IsSpclEquipmentChanged = function () {
            self.portentrypassapplicationModel().IndividualPersonalPermits().SpclEquipmentDetails('');
            return true;
        }

        self.unselectionrcheckbox = function () {
            if (self.portentrypassapplicationModel().PersonalPermits().PermitCategoryCode() != 'PERA') {
                $('#AllNPASites12').prop('checked', false);
                $('#AllNPASites12').attr('checked', false);
                $('#AllNPASitessecond2').prop('checked', false);
                $('#AllNPASitessecond2').attr('checked', false);
                self.portentrypassapplicationModel().PersonalPermits().AllNPASites(false);
            }
            return true;
        }
        self.selectedAPermitChoices = function () {

            if (self.portentrypassapplicationModel().PersonalPermits().PermitCategoryCode() == 'PERA') {
                if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCA') {
                    $('#AllNPASites').prop('checked', true);
                    //toastr.warning("this is only applicable on an A Category Permit", "Prot Entry Pass");
                    return true;
                }
                else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCB') {
                    $('#AllNPASitessecond2').prop('checked', true);
                    //toastr.warning("this is only applicable on an A Category Permit", "Prot Entry Pass");
                    return true;
                }
            }

            else if (self.portentrypassapplicationModel().PersonalPermits().PermitCategoryCode() != 'PERA') {
                if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCA') {
                    $('#AllNPASites').prop('checked', "false");
                    toastr.warning("This Is Only Applicable On An 'A' Category Permit", "Port Entry Pass");
                    self.portentrypassapplicationModel().PersonalPermits().AllNPASites(false);
                    return false;
                }
                else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCB') {
                    $('#AllNPASitessecond2').prop('checked', "false");
                    toastr.warning("This Is Only Applicable On An 'A' Category Permit", "Port Entry Pass");
                    self.portentrypassapplicationModel().PersonalPermits().AllNPASites(false);
                    return false;
                }

            }

        };
        self.AddVehicles = function () {
            self.PortEntryPassApplicationModel().VehiclePermitDtos.unshift(new eGateRoot.VehiclePermitDto());
            //$('#DriverMobileNo0').kendoMaskedMobile();
        }

        function formvalidation(portentrypass) {

            var result = true;
            portentrypass.validationEnabled(true);
            //  portentrypass.VehiclePermits.validationEnabled(true);
            //vehiclepermit.validationEnabled(true);
            if ($("#txtCaptachCode").val() != "") {

                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    result = false;
                }
            }

            if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCA') {
                var errors = 0;
                var parent = 0; var Child = 0; var Child1 = 0;
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })

                self.PersonalPermitValidation = ko.observable(portentrypass.PersonalPermits);
                self.PersonalPermitValidation().errors = ko.validation.group(self.PersonalPermitValidation());
                Child = ko.utils.arrayFilter(self.PersonalPermitValidation().errors(), function (error) {

                    return error != null;
                })
                PermitRequestContractorserrors = Child.length;
                if (parent.length != 0 || Child.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.PersonalPermitValidation().errors.showAllMessages();
                    result = false;
                }
                if (portentrypass.PersonalPermits().PermitCategoryCode() != 'PERA')
                { portentrypass.PersonalPermits().AllNPASites(false); }

                if (portentrypass.PersonalPermits().permittype() != 'adhoc') {
                    self.portentrypassapplicationModel().PersonalPermits().AdhocPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'temporary') {
                    self.portentrypassapplicationModel().PersonalPermits().TemporaryPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'allports') {
                    self.portentrypassapplicationModel().PersonalPermits().AllPorts('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'contractor') {
                    self.portentrypassapplicationModel().PersonalPermits().ConstructionArea('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'permanent') {
                    self.portentrypassapplicationModel().PersonalPermits().PermanentPermits('');

                }

                self.portentrypassapplicationModel().PersonalPermits().permittype() == self.isPermittype();
                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validPhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validPhoneNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#MobileNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoNewA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoNewA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoNewA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }

            }
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCB') {
                var errors = 0; var PermitRequestContractorserrors = 0;
                var parent = 0; var Child = 0;
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());

                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })

                errors = parent.length;
                self.PermitRequestContractorsValidation = ko.observable(portentrypass.PermitRequestContractors);
                self.PermitRequestContractorsValidation().errors = ko.validation.group(self.PermitRequestContractorsValidation());
                Child = ko.utils.arrayFilter(self.PermitRequestContractorsValidation().errors(), function (error) {

                    return error != null;
                })
                PermitRequestContractorserrors = Child.length;
                self.PersonalPermitValidation = ko.observable(portentrypass.PersonalPermits);
                self.PersonalPermitValidation().errors = ko.validation.group(self.PersonalPermitValidation());
                Child1 = ko.utils.arrayFilter(self.PersonalPermitValidation().errors(), function (error) {

                    return error != null;
                })
                PermitRequestContractorserrors = Child1.length;

                if (parent.length != 0 || Child.length != 0 || Child1.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.PermitRequestContractorsValidation().errors.showAllMessages();
                    self.PersonalPermitValidation().errors.showAllMessages();
                    result = false;
                }

                if (portentrypass.PersonalPermits().PermitCategoryCode() != 'PERA')
                { portentrypass.PersonalPermits().AllNPASites(false); }

                if (portentrypass.PersonalPermits().permittype() != 'adhoc') {
                    self.portentrypassapplicationModel().PersonalPermits().AdhocPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'temporary') {
                    self.portentrypassapplicationModel().PersonalPermits().TemporaryPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'allports') {
                    self.portentrypassapplicationModel().PersonalPermits().AllPorts('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'contractor') {
                    self.portentrypassapplicationModel().PersonalPermits().ConstructionArea('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'permanent') {
                    self.portentrypassapplicationModel().PersonalPermits().PermanentPermits('');

                }
                //if (portentrypass.PersonalPermits().permittype() != 'temporaryNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().IndividualTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractortempNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractorpermanentNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorPermanentPermits('');

                //}

                self.portentrypassapplicationModel().PersonalPermits().permittype() == self.isPermittype();
                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                var filterContracterContactNumber = portentrypass.PermitRequestContractors().ContactNo();
                filterContracterContactNumber = filterContracterContactNumber.replace(/(\)|\()|_|-+/g, '');
                var filterContracterMobileNumber = portentrypass.PermitRequestContractors().MobileNo();
                filterContracterMobileNumber = filterContracterMobileNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validCompanyFaxNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validCompanyFaxNumber++;
                            result = false;
                        }

                    }
                }


                var validContracterContactNumber = 0;
                if (filterContracterContactNumber.length != 0) {
                    if (filterContracterContactNumber.length != 13) {
                        toastr.warning("Invalid Contractor Contact Number");
                        validContracterContactNumber++;
                        result = false;
                    } else if (filterContracterContactNumber.length == 13) {
                        var validNo = parseInt(filterContracterContactNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contractor Contact Number");
                            validContracterContactNumber++;
                            result = false;
                        }

                    }
                }

                var validContracterMobileNumber = 0;
                if (filterContracterMobileNumber.length != 0) {
                    if (filterContracterMobileNumber.length != 13) {
                        toastr.warning("Invalid Contractor Mobile Number");
                        validContracterMobileNumber++;
                        result = false;
                    } else if (filterContracterMobileNumber.length == 13) {
                        var validNo = parseInt(filterContracterMobileNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contractor Mobile Number");
                            validContracterMobileNumber++;
                            result = false;
                        }

                    }
                }

                if (validPhoneNumber != 0) {
                    $("#MobileNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#ConteractMobileNoNewB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#ConteractMobileNoNewB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoNewB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoNewB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoNewB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }


                if (validContracterMobileNumber != 0) {
                    $("#MobileNoNewB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyMobileNomaskedtextbox = $("#MobileNoNewB").data("kendoMaskedTextBox");
                    portentrypass.PermitRequestContractors().MobileNo(CompanyMobileNomaskedtextbox.value());
                    result = false;
                }

                if (validContracterContactNumber != 0) {
                    $("#ContactTelephoneNoNewB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var ContactTelephoneNomaskedtextbox = $("#ContactTelephoneNoNewB").data("kendoMaskedTextBox");
                    portentrypass.PermitRequestContractors().ContactNo(ContactTelephoneNomaskedtextbox.value());
                    result = false;
                }

            } else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCC') {
                var errors = 0;
                var parent = 0; var Child = 0;
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })

                self.VehiclePermitsValidation = ko.observable(portentrypass.VehiclePermits);
                self.VehiclePermitsValidation().errors = ko.validation.group(self.VehiclePermitsValidation());
                Child = ko.utils.arrayFilter(self.VehiclePermitsValidation().errors(), function (error) {

                    return error != null;
                })
                //PermitRequestContractorserrors = Child.length;
                if (parent.length != 0 || Child.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.VehiclePermitsValidation().errors.showAllMessages();
                    result = false;
                    return;
                }
                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');
                var validPhoneNumber = 0;
                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }
                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validPhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validPhoneNumber++;
                        result = false;
                    } else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#MobileNoNewC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoNewC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoNewC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoNewC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoNewC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoNewC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }
            }
                //added by divya
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCF') {
                var errors = 0; //var IndividualVehiclePermitsValidationerrors = 0;
                var parent = 0; var Child = 0;
                var IndividualApplicationDetailsValidationerrors = 0;
                var IndividualVehiclePermitsValidationerrors = 0;
                var IndividualPersonalPermitValidationerrors = 0;
                var PermitReasonValidationerrors = 0;

                if (self.isPortSelected()) {
                    if (portentrypass.IndividualPersonalPermits().permittype() === '') {
                        $("#isPermitNoMsg").text('* Please select permit type');
                        self.isPermitNoMsg(true);
                        result = false;
                        //errors++;
                    }
                    else {
                        $("#isPermitNoMsg").text('');
                        self.isPermitNoMsg(false);
                    }

                    if (portentrypass.IndividualPersonalPermits().permittype() === 'Inditemp') {
                        if (portentrypass.IndividualPersonalPermits().IndividualTemporaryPermits() === '') {
                            $("#isIndTempPermitNoMsg").text('* Please select atleast one Individual Temporary permit type');
                            self.isIndTempPermitNoMsg(true);
                            result = false;
                            // errors++;
                        } else {
                            $("#isIndTempPermitNoMsg").text('');
                            self.isIndTempPermitNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().TempFromDate() === '') {
                            $("#istempFromDateNoMsg").text('* Please select Individual Temporary from date');
                            self.istempFromDateNoMsg(true);
                            result = false;
                            // errors++;
                        } else {
                            $("#istempFromDateNoMsg").text('');
                            self.istempFromDateNoMsg(false);
                        }

                        if (portentrypass.IndividualPersonalPermits().TempToDate() === '') {
                            $("#istempToDateNoMsg").text('* Please select Individual Temporary to date');
                            self.istempToDateNoMsg(true);
                            result = false;
                            //errors++;
                        } else {
                            $("#istempToDateNoMsg").text('');
                            self.istempToDateNoMsg(false);
                        }
                        ///////////////////////
                        if ((portentrypass.IndividualPersonalPermits().TempFromDate() !== '') && (portentrypass.IndividualPersonalPermits().TempToDate() !== '')) {
                            var temppermit = portentrypass.IndividualPersonalPermits().IndividualTemporaryPermits();
                            self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                                    function (result) {
                                        if (result !== '') {
                                            if (result.indexOf('-') != -1) {
                                                var min = result.split('-')[0];
                                                var Compareparam = (result.split('-')[1]).split(" ");
                                                var max = Compareparam[0];
                                                var param = Compareparam[1];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().TempFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().TempToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().TempToDate('');
                                                    }

                                                }
                                            } else {
                                                var min = 1;
                                                var str = result.split(" ");
                                                var max = str[1];
                                                var param = str[2];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().TempFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().TempToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().TempToDate('');
                                                    }

                                                }


                                            }
                                        }
                                    });


                        }
                        /////////////////

                    }
                    else if (portentrypass.IndividualPersonalPermits().permittype() === 'Indiper') {
                        if (portentrypass.IndividualPersonalPermits().IndividualPermanentPermits() === '') {
                            $("#isIndPermPermitNoMsg").text('* Please select atleast one Individual Permanent permit type');
                            self.isIndPermPermitNoMsg(true);
                            result = false;
                            // errors++;
                        }
                        else {
                            $("#isIndPermPermitNoMsg").text('');
                            self.isIndPermPermitNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().PerFromDate() === '') {
                            $("#isIndPerFromDateNoMsg").text('* Please select Individual Permanent from date');
                            self.isIndPerFromDateNoMsg(true);
                            result = false;
                            // errors++;
                        }
                        else {
                            $("#isIndPerFromDateNoMsg").text('');
                            self.isIndPerFromDateNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().PerToDate() === '') {
                            $("#isPerToDateNoMsg").text('* Please select Individual Permanent to date');
                            self.isPerToDateNoMsg(true);
                            result = false;
                            // errors++;
                        } else {
                            $("#isPerToDateNoMsg").text('');
                            self.isPerToDateNoMsg(false);
                        }

                        /////////////
                        if ((portentrypass.IndividualPersonalPermits().TempFromDate() !== '') && (portentrypass.IndividualPersonalPermits().TempToDate() !== '')) {
                            var temppermit = portentrypass.IndividualPersonalPermits().IndividualPermanentPermits();
                            self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                                    function (result) {
                                        if (result !== '') {
                                            if (result.indexOf('-') != -1) {
                                                var min = result.split('-')[0];
                                                var Compareparam = (result.split('-')[1]).split(" ");
                                                var max = Compareparam[0];
                                                var param = Compareparam[1];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().PerFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().PerToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }

                                                }
                                                else if (param === "Weeks") {
                                                    var NewMin = min * 7;
                                                    var NewMax = max * 7;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }
                                                }
                                                else if (param === "Years") {
                                                    var NewMin = min * 365;
                                                    var NewMax = max * 365;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }
                                                }
                                            } else {
                                                var min = 1;
                                                var str = result.split(" ");
                                                var max = str[1];
                                                var param = str[2];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().PerFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().PerToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }

                                                }
                                                else if (param === "Weeks") {
                                                    var NewMin = min * 7;
                                                    var NewMax = max * 7;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }
                                                }
                                                else if (param === "Years") {
                                                    var NewMin = min * 365;
                                                    var NewMax = max * 365;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().PerToDate('');
                                                    }
                                                }


                                            }
                                        }
                                    });

                        }
                        /////////////////////
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().SACitizen() !== "") {
                        self.isSACitizenMsg(false);
                        $("#isSACitizenMsg").text('');

                    }
                    else {
                        $("#isSACitizenMsg").text('* Please Select The SA Citizen');
                        self.isSACitizenMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Gender() !== "") {
                        self.isNoGenderMsg(false);
                        $("#isNoGenderMsg").text('');

                    }
                    else {
                        $("#isNoGenderMsg").text('* Please Select The Gender');
                        self.isNoGenderMsg(true);
                        result = false;
                    }

                    if (portentrypass.IndividualPermitApplicationDetails().CountryOfOrigin() !== undefined) {
                        self.isNoCountryOfOriginMsg(false);
                        $("#isNoCountryOfOriginMsg").text('');

                    }
                    else {
                        $("#isNoCountryOfOriginMsg").text('* Please Select Country Of Origin');
                        self.isNoCountryOfOriginMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Date() !== "") {
                        self.isAppliDateNoMsg(false);
                        $("#isAppliDateNoMsg").text('');
                    }
                    else {
                        $("#isAppliDateNoMsg").text('* Please enter the Date Of Application');
                        self.isAppliDateNoMsg(true);
                        result = false;

                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Current_Permit_Exists() !== "") {
                        self.isCurrPerMsg(false);
                        $("#isCurrPerMsg").text('');

                    }

                    else {
                        $("#isCurrPerMsg").text('* Please Select The Current permit exists');
                        self.isCurrPerMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Port_Induction_Training() !== "") {
                        self.isPrtIndMsg(false);
                        $("#isPrtIndMsg").text('');

                    }
                    else {
                        $("#isPrtIndMsg").text('* Please Select The Port Induction Training');
                        self.isPrtIndMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Criminal_Bckground() !== "") {
                        self.isCrmBckMsg(false);
                        $("#isCrmBckMsg").text('');

                    }
                    else {
                        $("#isCrmBckMsg").text('* Please Select The Criminal back ground');
                        self.isCrmBckMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPersonalPermits().IsTools() !== "") {
                        self.isNoToolsMsg(false);
                        $("#isNoToolsMsg").text('');

                    }
                    else {
                        $("#isNoToolsMsg").text('* Please Select Tools');
                        self.isNoToolsMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPersonalPermits().IsCamera() !== "") {
                        self.isNoCameraMsg(false);
                        $("#isNoCameraMsg").text('');

                    }
                    else {
                        $("#isNoCameraMsg").text('* Please Select The Camera');
                        self.isNoCameraMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPersonalPermits().IsSpclEquipment() !== "") {
                        self.isSpclEqpMsg(false);
                        $("#isSpclEqpMsg").text('');

                    }
                    else {
                        $("#isSpclEqpMsg").text('* Please Select The Special Equipment');
                        self.isSpclEqpMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Current_Permit_Exists() === 'Y') {
                        if (portentrypass.IndividualPermitApplicationDetails().Reason_Reapplication() === '') {
                            $("#isReappNoMsg").text('* Please enter reason for re application ');
                            self.isReappNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isReappNoMsg").text('');
                            self.isReappNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPermitApplicationDetails().Port_Induction_Training() === 'Y') {
                        if (portentrypass.IndividualPermitApplicationDetails().Training_Date() === '') {
                            $("#istrainingDateNoMsg").text('* Please enter induction training date ');
                            self.istrainingDateNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#istrainingDateNoMsg").text('');
                            self.istrainingDateNoMsg(false);
                        }
                    }
                    if (self.RBportSelected()) {
                        if (portentrypass.PermitRequestAreas().length === 0) {
                            $("#ispermitAreaNoMsg").text('* Please select atleast one permit Area ');
                            self.ispermitAreaNoMsg(true);
                            result = false;
                        } else {
                            $("#ispermitAreaNoMsg").text('');
                            self.ispermitAreaNoMsg(false);
                        }
                        if (portentrypass.PermitRequestSubAreas().length === 0) {
                            $("#ispermitSubAreaNoMsg").text('* Please select atleast one sub permit Area ');
                            self.ispermitSubAreaNoMsg(true);
                            result = false;
                        } else {
                            $("#ispermitSubAreaNoMsg").text('');
                            self.ispermitSubAreaNoMsg(false);
                        }
                    }
                    if (portentrypass.PermitReasons().length === 0) {
                        $("#ispermitReasonNoMsg").text('* Please select atleast one Reason ');
                        self.ispermitReasonNoMsg(true);
                        result = false;
                    } else {
                        $("#ispermitReasonNoMsg").text('');
                        self.ispermitReasonNoMsg(false);
                    }
                    if (portentrypass.IndividualPersonalPermits().IsCamera() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().CameraDetails() === '') {
                            $("#isCameraNoMsg").text('* Please enter Camera details ');
                            self.isCameraNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isCameraNoMsg").text('');
                            self.isCameraNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPersonalPermits().IsTools() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().ToolsDetails() === '') {
                            $("#isToolsNoMsg").text('* Please enter Tools details ');
                            self.isToolsNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isToolsNoMsg").text('');
                            self.isToolsNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPersonalPermits().IsSpclEquipment() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().SpclEquipmentDetails() === '') {
                            $("#isSpclEqupNoMsg").text('* Please enter Special Equipment details ');
                            self.isSpclEqupNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isSpclEqupNoMsg").text('');
                            self.isSpclEqupNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPersonalPermits().SignatoryDate() !== "") {
                        self.isSigDateNoMsg(false);
                        $("#isSigDateNoMsg").text('');
                    }
                    else {
                        $("#isSigDateNoMsg").text('* Please enter the Date');
                        self.isSigDateNoMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualVehiclePermits().length === 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        //toastr.warning("Please Fill atleast one Vehicle Details", "Port Entry Pass Request");
                        result = false;
                    }
                }

                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {
                    return error != null;
                })

                self.IndividualApplicationDetailsValidation = ko.observable(portentrypass.IndividualPermitApplicationDetails);
                self.IndividualApplicationDetailsValidation().errors = ko.validation.group(self.IndividualApplicationDetailsValidation());
                Child = ko.utils.arrayFilter(self.IndividualApplicationDetailsValidation().errors(), function (error) {
                    return error != null;
                })

                self.IndividualVehiclePermitsValidation = ko.observable(portentrypass.IndividualVehiclePermits);
                self.IndividualVehiclePermitsValidation().errors = ko.validation.group(self.IndividualVehiclePermitsValidation());
                Child1 = ko.utils.arrayFilter(self.IndividualVehiclePermitsValidation().errors(), function (error) {
                    return error != null;
                })

                self.IndividualPersonalPermitValidation = ko.observable(portentrypass.IndividualPersonalPermits);
                self.IndividualPersonalPermitValidation().errors = ko.validation.group(self.IndividualPersonalPermitValidation());
                Child2 = ko.utils.arrayFilter(self.IndividualPersonalPermitValidation().errors(), function (error) {
                    return error != null;
                })

                PermitRequestContractorserrors = Child1.length;

                if (parent.length != 0 || Child.length != 0 || Child1.length != 0 || Child2.length != 0) {//|| Child3.length != 0) {

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.IndividualApplicationDetailsValidation().errors.showAllMessages();
                    self.IndividualPersonalPermitValidation().errors.showAllMessages();
                    // self.PermitReasonValidation().errors.showAllMessages();
                    self.IndividualVehiclePermitsValidation().errors.showAllMessages();

                    result = false;
                }

                self.portentrypassapplicationModel().PersonalPermits().permittype() == self.isPermittype();
                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');

                //var filterTelephoneWork = portentrypass.IndividualPersonalPermits().TelephoneWork();
                // filterTelephoneWork = filterTelephoneWork.replace(/(\)|\()|_|-+/g, '');
                var filterAuthorisedMobile = portentrypass.IndividualPersonalPermits().AuthorisedMobile();
                filterAuthorisedMobile = filterAuthorisedMobile.replace(/(\)|\()|_|-+/g, '');

                // var validTelephoneWork = 0;

                //if (filterTelephoneWork.length != 0) {
                //    if (filterTelephoneWork.length != 13) {
                //        toastr.warning("Invalid Telephone Work");
                //        validTelephoneWork++;
                //        result = false;
                //    }
                //    else if (filterTelephoneWork.length == 13) {
                //        var validNo = parseInt(filterTelephoneWork);
                //        if (validNo == 0) {
                //            toastr.warning("Invalid Telephone Work");
                //            validTelephoneWork++;
                //            result = false;
                //        }

                //    }
                //}

                var validAuthorisedMobile = 0;

                if (filterAuthorisedMobile.length != 0) {
                    if (filterAuthorisedMobile.length != 13) {
                        toastr.warning("Invalid Authorised Mobile");
                        validAuthorisedMobile++;
                        result = false;
                    }
                    //else if (filterTelephoneWork.length == 13) {
                    //    var validNo = parseInt(filterAuthorisedMobile);
                    //    if (validNo == 0) {
                    //        toastr.warning("Invalid Telephone Work");
                    //        validAuthorisedMobile++;
                    //        result = false;
                    //    }

                    //}
                }


                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                if (validPhoneNumber != 0) {
                    $("#MobileNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#ConteractMobileNoNewB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }

            }
                //divya1
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCH') {
                var errors = 0; //var IndividualVehiclePermitsValidationerrors = 0;
                var parent = 0; var Child = 0;
                var IndividualApplicationDetailsValidationerrors = 0;
                var IndividualPersonalPermitValidationerrors = 0;
                var PermitRequestContractorserrors = 0;
                var ContractorPermitApplicationDetailserrors = 0;
                var IndividualVehiclePermitsValidationerrors = 0;

                if (self.isPortSelected()) {
                    if (portentrypass.IndividualPersonalPermits().permittype() === '') {
                        $("#isPermit1NoMsg").text('* Please select permit type');
                        self.isPermit1NoMsg(true);
                        result = false;
                    } else {
                        $("#isPermit1NoMsg").text('');
                        self.isPermit1NoMsg(false);
                    }

                    if (portentrypass.IndividualPersonalPermits().permittype() === 'contemp') {
                        if (portentrypass.IndividualPersonalPermits().ContractorTemporaryPermits() === '') {
                            $("#isCntrTempPermitNoMsg").text('* Please select atleast one Contractor Temporary permit type');
                            self.isCntrTempPermitNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntrTempPermitNoMsg").text('');
                            self.isCntrTempPermitNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().ContractorTempFromDate() === '') {
                            $("#isCntrtempFromDateNoMsg").text('* Please select Contractor Temporary from date');
                            self.isCntrtempFromDateNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntrtempFromDateNoMsg").text('');
                            self.isCntrtempFromDateNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().ContractorTempToDate() === '') {
                            $("#isCntrtempToDateNoMsg").text('* Please select Contractor Temporary to date');
                            self.isCntrtempToDateNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntrtempToDateNoMsg").text('');
                            self.isCntrtempToDateNoMsg(false);
                        }
                        if ((portentrypass.IndividualPersonalPermits().ContractorTempFromDate() !== '') && (portentrypass.IndividualPersonalPermits().ContractorTempToDate() !== '')) {
                            var temppermit = portentrypass.IndividualPersonalPermits().ContractorTemporaryPermits();
                            self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                                    function (result) {
                                        if (result !== '') {
                                            if (result.indexOf('-') != -1) {
                                                var min = result.split('-')[0];
                                                var Compareparam = (result.split('-')[1]).split(" ");
                                                var max = Compareparam[0];
                                                var param = Compareparam[1];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //  //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                    }

                                                }
                                                else if (param === "Weeks") {
                                                    var NewMin = min * 7;
                                                    var NewMax = max * 7;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                    }
                                                }
                                            } else {
                                                var min = 1;
                                                var str = result.split(" ");
                                                if (str.length === 3) {
                                                    var max = str[1];
                                                    var param = str[2];
                                                    var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempFromDate());
                                                    var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempToDate());
                                                    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                    //alert(diffDays);
                                                    if (param === 'Days') {
                                                        if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                        }

                                                    }
                                                    else if (param === "Weeks") {
                                                        var NewMin = min * 7;
                                                        var NewMax = max * 7;
                                                        if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                        }
                                                    }


                                                }
                                                else if (str.length === 2) {
                                                    var max = str[0];
                                                    var param = str[1];
                                                    var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempFromDate());
                                                    var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorTempToDate());
                                                    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                    //alert(diffDays);
                                                    if (param === 'Days') {
                                                        if (diffDays !== parseInt(max)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                        }

                                                    }
                                                    else if (param === "Weeks") {
                                                        var NewMin = min * 7;
                                                        var NewMax = max * 7;
                                                        if (diffDays !== parseInt(NewMax)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorTempToDate('');
                                                        }
                                                    }



                                                }
                                            }
                                        }
                                    });

                        }

                    }
                    else if (portentrypass.IndividualPersonalPermits().permittype() === 'conper') {
                        if (portentrypass.IndividualPersonalPermits().ContractorPermanentPermits() === '') {
                            $("#isCntrPermPermitNoMsg").text('* Please select atleast one Temporary Permanent permit type');
                            self.isCntrPermPermitNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntrPermPermitNoMsg").text('');
                            self.isCntrPermPermitNoMsg(false);
                        }

                        if (portentrypass.IndividualPersonalPermits().ContractorPerFromDate() === '') {
                            $("#isCntPerFromDateNoMsg").text('* Please select Contractor Permanent from date');
                            self.isCntPerFromDateNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntPerFromDateNoMsg").text('');
                            self.isCntPerFromDateNoMsg(false);
                        }
                        if (portentrypass.IndividualPersonalPermits().ContractorPerToDate() === '') {
                            $("#isCntPerToDateNoMsg").text('* Please select Contractor Permanent to date');
                            self.isCntPerToDateNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntPerToDateNoMsg").text('');
                            self.isCntPerToDateNoMsg(false);
                        }
                        if ((portentrypass.IndividualPersonalPermits().ContractorPerFromDate() !== '') && (portentrypass.IndividualPersonalPermits().ContractorPerToDate() !== '')) {
                            var temppermit = portentrypass.IndividualPersonalPermits().ContractorPermanentPermits();
                            self.viewModelHelper.apiGet('api/SubCategory/GetSubCatName', { code: temppermit },
                                    function (result) {
                                        if (result !== '') {
                                            if (result.indexOf('-') != -1) {
                                                var min = result.split('-')[0];
                                                var Compareparam = (result.split('-')[1]).split(" ");
                                                var max = Compareparam[0];
                                                var param = Compareparam[1];
                                                var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerFromDate());
                                                var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerToDate());
                                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                //alert(diffDays);
                                                if (param === 'Days') {
                                                    if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().ContractorPerToDate('');
                                                    }

                                                }
                                                else if (param === "Weeks") {
                                                    var NewMin = min * 7;
                                                    var NewMax = max * 7;
                                                    if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                        toastr.options.closeButton = true;
                                                        toastr.options.positionClass = "toast-top-right";
                                                        toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                        portentrypass.IndividualPersonalPermits().ContractorPerToDate('');
                                                    }
                                                }
                                            } else {
                                                var min = 1;
                                                var str = result.split(" ");
                                                if (str.length === 3) {
                                                    var max = str[1];
                                                    var param = str[2];
                                                    var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerFromDate());
                                                    var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerToDate());
                                                    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                    //alert(diffDays);
                                                    if (param === 'Days') {
                                                        if (diffDays < parseInt(min) || diffDays > parseInt(max)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorPerToDate('');
                                                        }

                                                    }
                                                    else if (param === "Weeks") {
                                                        var NewMin = min * 7;
                                                        var NewMax = max * 7;
                                                        if (diffDays < parseInt(NewMin) || diffDays > parseInt(NewMax)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorPerToDate('');
                                                        }
                                                    }


                                                }
                                                else if (str.length === 2) {
                                                    var max = str[0];
                                                    var param = str[1];
                                                    var date1 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerFromDate());
                                                    var date2 = new Date(portentrypass.IndividualPersonalPermits().ContractorPerToDate());
                                                    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
                                                    //alert(diffDays);
                                                    if (param === 'Days') {
                                                        if (diffDays !== parseInt(max)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            model.ContractorPerToDate('');
                                                        }

                                                    }
                                                    else if (param === "Weeks") {
                                                        var NewMin = min * 7;
                                                        var NewMax = max * 7;
                                                        if (diffDays !== parseInt(NewMax)) {
                                                            toastr.options.closeButton = true;
                                                            toastr.options.positionClass = "toast-top-right";
                                                            toastr.warning("Please Select Exact dates to match " + result, "Port Entry Pass Request");
                                                            portentrypass.IndividualPersonalPermits().ContractorPerToDate('');
                                                        }
                                                    }



                                                }
                                            }
                                        }
                                    });

                        }
                    }
                    if (self.RBportSelected()) {
                        if (portentrypass.PermitRequestAreas().length === 0) {
                            $("#isCntpermitAreaNoMsg").text('* Please select atleast one permit Area ');
                            self.isCntpermitAreaNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntpermitAreaNoMsg").text('');
                            self.isCntpermitAreaNoMsg(false);
                        }
                        if (portentrypass.PermitRequestSubAreas().length === 0) {
                            $("#isCntpermitSubAreaNoMsg").text('* Please select atleast one sub permit Area ');
                            self.isCntpermitSubAreaNoMsg(true);
                            result = false;
                        } else {
                            $("#isCntpermitSubAreaNoMsg").text('');
                            self.isCntpermitSubAreaNoMsg(false);
                        }
                    }
                    if (portentrypass.PermitReasons().length === 0) {
                        $("#isCntpermitReasonNoMsg").text('* Please select atleast one Reason ');
                        self.isCntpermitReasonNoMsg(true);
                        result = false;
                    }
                    else {
                        $("#isCntpermitReasonNoMsg").text('');
                        self.isCntpermitReasonNoMsg(false);
                    }
                    if (portentrypass.IndividualPersonalPermits().IsTools() !== "") {
                        self.isCntNoToolsMsg(false);
                        $("#isCntNoToolsMsg").text('');

                    }
                    else {
                        $("#isCntNoToolsMsg").text('* Please Select Tools');
                        self.isCntNoToolsMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPersonalPermits().IsCamera() !== "") {
                        self.isCntNoCameraMsg(false);
                        $("#isCntNoCameraMsg").text('');

                    }
                    else {
                        $("#isCntNoCameraMsg").text('* Please Select The Camera');
                        self.isCntNoCameraMsg(true);
                        result = false;
                    }
                    if (portentrypass.IndividualPersonalPermits().IsSpclEquipment() !== "") {
                        self.isCntSpclEqpMsg(false);
                        $("#isCntSpclEqpMsg").text('');

                    }
                    else {
                        $("#isCntSpclEqpMsg").text('* Please Select The Special Equipment');
                        self.isCntSpclEqpMsg(true);
                        result = false;
                    }

                    if (portentrypass.IndividualPersonalPermits().IsCamera() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().CameraDetails() === '') {
                            $("#isCntCameraNoMsg").text('* Please enter Camera details ');
                            self.isCntCameraNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isCntCameraNoMsg").text('');
                            self.isCntCameraNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPersonalPermits().IsTools() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().ToolsDetails() === '') {
                            $("#isCntToolsNoMsg").text('* Please enter Tools details ');
                            self.isCntToolsNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isCntToolsNoMsg").text(' ');
                            self.isCntToolsNoMsg(false);
                        }
                    }
                    if (portentrypass.IndividualPersonalPermits().IsSpclEquipment() === 'Y') {
                        if (portentrypass.IndividualPersonalPermits().SpclEquipmentDetails() === '') {
                            $("#isCntSpclEqupNoMsg").text('* Please enter Special Equipment details ');
                            self.isCntSpclEqupNoMsg(true);
                            result = false;
                        }
                        else {
                            $("#isCntSpclEqupNoMsg").text('');
                            self.isCntSpclEqupNoMsg(false);
                        }
                    }
                    //if (portentrypass.IndividualPersonalPermits().SignatoryDate() !== "") {
                    //    self.isCntSigDateNoMsg(false);
                    //    $("#isCntSigDateNoMsg").text('');
                    //}
                    //else {
                    //    $("#isCntSigDateNoMsg").text('* Please enter the Date');
                    //    self.isCntSigDateNoMsg(true);
                    //    result = false;
                    //}
                    if (portentrypass.ContractorPermitEmployeeDetails().length === 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Fill atleast one employee Details", "Port Entry Pass Request");
                        result = false;
                    }
                }


                self.portentrypassValidation = ko.observable(portentrypass);

                self.ContractorPermitApplicationDetailValidation = ko.observable(portentrypass.ContractorPermitApplicationDetails);
                self.ContractorPermitApplicationDetailValidation().errors = ko.validation.group(self.ContractorPermitApplicationDetailValidation());
                parent = ko.utils.arrayFilter(self.ContractorPermitApplicationDetailValidation().errors(), function (error) {

                    return error != null;
                })


                self.IndividualPersonalPermitValidation = ko.observable(portentrypass.IndividualPersonalPermits);
                self.IndividualPersonalPermitValidation().errors = ko.validation.group(self.IndividualPersonalPermitValidation());
                Child = ko.utils.arrayFilter(self.IndividualPersonalPermitValidation().errors(), function (error) {
                    return error != null;
                })

                // PermitRequestContractorserrors = Child1.length;

                if (parent.length != 0 || Child.length != 0) {// || Child1.length != 0 || Child2.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.ContractorPermitApplicationDetailValidation().errors.showAllMessages();
                    self.IndividualPersonalPermitValidation().errors.showAllMessages();
                    result = false;
                }



                var filterTelephoneNumber = portentrypass.ContractorPermitApplicationDetails().TelephoneNumber();
                filterTelephoneNumber = filterTelephoneNumber.replace(/(\)|\()|_|-+/g, '');

                var filterSubContractorTelephoneNumber = portentrypass.ContractorPermitApplicationDetails().SubContractorTelephoneNumber();
                filterSubContractorTelephoneNumber = filterSubContractorTelephoneNumber.replace(/(\)|\()|_|-+/g, '');

                var filterTelephoneWork = portentrypass.IndividualPersonalPermits().TelephoneWork();
                filterTelephoneWork = filterTelephoneWork.replace(/(\)|\()|_|-+/g, '');

                var filterAuthorisedMobile = portentrypass.IndividualPersonalPermits().AuthorisedMobile();
                filterAuthorisedMobile = filterAuthorisedMobile.replace(/(\)|\()|_|-+/g, '');

                var validTelephoneWork = 0;

                if (filterTelephoneWork.length != 0) {
                    if (filterTelephoneWork.length != 13) {
                        toastr.warning("Invalid Telephone Work");
                        validTelephoneWork++;
                        result = false;
                    }
                    else if (filterTelephoneWork.length == 13) {
                        var validNo = parseInt(filterTelephoneWork);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Work");
                            validTelephoneWork++;
                            result = false;
                        }

                    }
                }

                var validAuthorisedMobile = 0;

                if (filterAuthorisedMobile.length != 0) {
                    if (filterAuthorisedMobile.length != 13) {
                        toastr.warning("Invalid Authorised Mobile");
                        validAuthorisedMobile++;
                        result = false;
                    }
                    else if (filterTelephoneWork.length == 13) {
                        var validNo = parseInt(filterAuthorisedMobile);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Work");
                            validAuthorisedMobile++;
                            result = false;
                        }

                    }
                }


                var validTelephoneNumber = 0;

                if (filterTelephoneNumber.length != 0) {
                    if (filterTelephoneNumber.length != 13) {
                        toastr.warning("Invalid Telephone Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterTelephoneNumber.length == 13) {
                        var validNo = parseInt(filterTelephoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            validTelephoneNumber++;
                            result = false;
                        }

                    }
                }
                var validSubContractorTelephoneNumber = 0;
                if (filterSubContractorTelephoneNumber.length != 0) {
                    if (filterSubContractorTelephoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validSubContractorTelephoneNumber++;
                        result = false;
                    }
                    else if (filterSubContractorTelephoneNumber.length == 13) {
                        var validNo = parseInt(filterSubContractorTelephoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validSubContractorTelephoneNumber++;
                            result = false;
                        }
                    }
                }

                if (validPhoneNumber != 0) {
                    $("#MobileNoNewA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#ConteractMobileNoNewB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
            }
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCE') {
                var errors = 0; var VisitorPermitsValidationerrors = 0;
                var parent = 0; var Child = 0;
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })
                self.VisitorPermitsValidation = ko.observable(portentrypass.VisitorPermits);
                self.VisitorPermitsValidation().errors = ko.validation.group(self.VisitorPermitsValidation());
                Child = ko.utils.arrayFilter(self.VisitorPermitsValidation().errors(), function (error) {

                    return error != null;
                })

                if (parent.length != 0 || Child.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.VisitorPermitsValidation().errors.showAllMessages();
                    result = false;
                }

                if (self.portentrypassapplicationModel().VisitorPermits().PermitCode() == "" || self.portentrypassapplicationModel().VisitorPermits().PermitCode() == null || self.portentrypassapplicationModel().VisitorPermits().PermitCode == undefined) {
                    result = false;
                    toastr.warning("Please Select No of Days Permit Required For", "Port Entry Pass Request");
                }

                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                var filterTelephoneNoNumber = portentrypass.VisitorPermits().TelephoneNo();
                filterTelephoneNoNumber = filterTelephoneNoNumber.replace(/(\)|\()|_|-+/g, '');

                var validPhoneNumber = 0;
                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validCompanyFaxNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validCompanyFaxNumber++;
                            result = false;
                        }

                    }
                }


                var validTelephoneNoNumber = 0;
                if (filterTelephoneNoNumber.length != 0) {
                    if (filterTelephoneNoNumber.length != 13) {
                        toastr.warning("Invalid Telephone Number");
                        validTelephoneNoNumber++;
                        result = false;
                    } else if (filterTelephoneNoNumber.length == 13) {
                        var validNo = parseInt(filterTelephoneNoNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            validTelephoneNoNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#MobileNoNEWE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoNEWE").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoNEWE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoNEWE").data("kendoMaskedTextBox");
                    portentrypass.CompanyTelephoneNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoNEWE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoNEWE").data("kendoMaskedTextBox");
                    portentrypass.CompanyFaxNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }


                if (validTelephoneNoNumber != 0) {
                    $("#TelephoneNovisitornew").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var TelephoneNomaskedtextbox = $("#TelephoneNovisitornew").data("kendoMaskedTextBox");
                    portentrypass.VisitorPermits().TelephoneNo(TelephoneNomaskedtextbox.value());
                    result = false;
                }
            }
            else
                if (self.portentrypassapplicationModel().PermitRequestTypeCode() == "") {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill Valid Data in The Form", "Port Entry Pass Request");
                    result = false;
                }
            return result;
        }
        function Resubmssionformvalidation(portentrypass) {
            var result = true;
            portentrypass.validationEnabled(true);
            var errors = 0;
            var PermitRequestContractorserrors = 0;
            var WharfVehiclePermitsValidationerrors = 0;
            var VisitorPermitsValidationerrors = 0;
            self.portentrypassValidation = ko.observable(portentrypass);
            self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
            errors = self.portentrypassValidation().errors().length;

            if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCA') {

                if (errors != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details in The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    result = false;
                    return;
                }


                if (portentrypass.PersonalPermits().permittype() != 'adhoc') {
                    self.portentrypassapplicationModel().PersonalPermits().AdhocPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'temporary') {
                    self.portentrypassapplicationModel().PersonalPermits().TemporaryPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'allports') {
                    self.portentrypassapplicationModel().PersonalPermits().AllPorts('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'contractor') {
                    self.portentrypassapplicationModel().PersonalPermits().ConstructionArea('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'permanent') {
                    self.portentrypassapplicationModel().PersonalPermits().PermanentPermits('');

                }
                //if (portentrypass.PersonalPermits().permittype() != 'temporaryNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().IndividualTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractortempNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractorpermanentNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorPermanentPermits('');

                //}

                self.portentrypassapplicationModel().PersonalPermits().permittype() == self.isPermittype();


                if (portentrypass.PersonalPermits().PermitCategoryCode() != 'PERA')
                { portentrypass.PersonalPermits().AllNPASites(false); }



                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validPhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validPhoneNumber++;
                        result = false;
                    } else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#MobileNoResubmissionA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoResubmissionA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoResubmissionA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoResubmissionA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoResubmissionA").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoResubmissionA").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }

            }
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCB') {
                self.PermitRequestContractorsValidation = ko.observable(portentrypass.PermitRequestContractors);
                self.PermitRequestContractorsValidation().errors = ko.validation.group(self.PermitRequestContractorsValidation());
                PermitRequestContractorserrors = self.PermitRequestContractorsValidation().errors().length;
                if (errors != 0 || PermitRequestContractorserrors != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details in The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.PermitRequestContractorsValidation().errors.showAllMessages();
                    result = false;
                    return;
                }

                if (portentrypass.PersonalPermits().permittype() != 'adhoc') {
                    self.portentrypassapplicationModel().PersonalPermits().AdhocPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'temporary') {
                    self.portentrypassapplicationModel().PersonalPermits().TemporaryPermits('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'allports') {
                    self.portentrypassapplicationModel().PersonalPermits().AllPorts('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'contractor') {
                    self.portentrypassapplicationModel().PersonalPermits().ConstructionArea('');

                }
                if (portentrypass.PersonalPermits().permittype() != 'permanent') {
                    self.portentrypassapplicationModel().PersonalPermits().PermanentPermits('');

                }
                //if (portentrypass.PersonalPermits().permittype() != 'temporaryNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().IndividualTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractortempNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorTemporaryPermits('');

                //}
                //if (portentrypass.PersonalPermits().permittype() != 'contractorpermanentNew') {
                //    self.portentrypassapplicationModel().PersonalPermits().ContractorPermanentPermits('');

                //}

                self.portentrypassapplicationModel().PersonalPermits().permittype() == self.isPermittype();

                if (portentrypass.PersonalPermits().PermitCategoryCode() != 'PERA')
                { portentrypass.PersonalPermits().AllNPASites(false); }

                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                var filterContracterContactNumber = portentrypass.PermitRequestContractors().ContactNo();
                filterContracterContactNumber = filterContracterContactNumber.replace(/(\)|\()|_|-+/g, '');
                var filterContracterMobileNumber = portentrypass.PermitRequestContractors().MobileNo();
                filterContracterMobileNumber = filterContracterMobileNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validCompanyFaxNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validCompanyFaxNumber++;
                            result = false;
                        }

                    }
                }


                var validContracterContactNumber = 0;
                if (filterContracterContactNumber.length != 0) {
                    if (filterContracterContactNumber.length != 13) {
                        toastr.warning("Invalid Contractor Contact Number");
                        validContracterContactNumber++;
                        result = false;
                    } else if (filterContracterContactNumber.length == 13) {
                        var validNo = parseInt(filterContracterContactNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contractor Contact Number");
                            validContracterContactNumber++;
                            result = false;
                        }

                    }
                }

                var validContracterMobileNumber = 0;
                if (filterContracterMobileNumber.length != 0) {
                    if (filterContracterMobileNumber.length != 13) {
                        toastr.warning("Invalid Contractor Mobile Number");
                        validContracterMobileNumber++;
                        result = false;
                    } else if (filterContracterMobileNumber.length == 13) {
                        var validNo = parseInt(filterContracterMobileNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contractor Mobile Number");
                            validContracterMobileNumber++;
                            result = false;
                        }

                    }
                }

                if (validPhoneNumber != 0) {
                    $("#MobileNoResubmissionB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoResubmissionB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoResubmissionB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoResubmissionB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoResubmissionB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoResubmissionB").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }


                if (validContracterMobileNumber != 0) {
                    $("#ContactMobileNoResubmissionB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyMobileNomaskedtextbox = $("#ContactMobileNoResubmissionB").data("kendoMaskedTextBox");
                    portentrypass.PermitRequestContractors().MobileNo(CompanyMobileNomaskedtextbox.value());
                    result = false;
                }

                if (validContracterContactNumber != 0) {
                    $("#ContactTelephoneNoResubmissionB").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var ContactTelephoneNomaskedtextbox = $("#ContactTelephoneNoResubmissionB").data("kendoMaskedTextBox");
                    portentrypass.PermitRequestContractors().ContactNo(ContactTelephoneNomaskedtextbox.value());
                    result = false;
                }

            }
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCC') {
                var parent = 0; var Child = 0;
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                parent = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })

                self.VehiclePermitsValidation = ko.observable(portentrypass.VehiclePermits);
                self.VehiclePermitsValidation().errors = ko.validation.group(self.VehiclePermitsValidation());
                Child = ko.utils.arrayFilter(self.VehiclePermitsValidation().errors(), function (error) {

                    return error != null;
                })
                //PermitRequestContractorserrors = Child.length;
                if (parent.length != 0 || Child.length != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.VehiclePermitsValidation().errors.showAllMessages();
                    result = false;
                    return;
                }

                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validPhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validPhoneNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#MobileNoResubmissionC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoResubmissionC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoResubmissionC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoResubmissionC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoResubmissionC").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoResubmissionC").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }

            }
            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCD') {
                self.WharfVehiclePermitsValidation = ko.observable(portentrypass.WharfVehiclePermits);
                self.WharfVehiclePermitsValidation().errors = ko.validation.group(self.WharfVehiclePermitsValidation());
                WharfVehiclePermitsValidationerrors = self.WharfVehiclePermitsValidation().errors().length;
                if (errors != 0 || WharfVehiclePermitsValidationerrors != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details in The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.WharfVehiclePermitsValidation().errors.showAllMessages();
                    result = false;
                }
                if (self.portentrypassapplicationModel().WharfVehiclePermits().PermitRequirement() == "") {
                    toastr.warning("Please Select Any Permit Requirement Type", "Port Entry Pass Request");
                    $("#isPermitRequeirementstypeswharfResubmissionMsg").text('* Please Enter Any Permit Requirement Type');
                    self.isPermitRequeirementstypeswharfResubmissionMsg(true);
                    result = false;
                }
                else {
                    $("#isPermitRequeirementstypeswharfResubmissionMsg").text('');
                    self.isPermitRequeirementstypeswharfResubmissionMsg(false);
                }


                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                var filterTelephoneNoNumber = portentrypass.WharfVehiclePermits().TelephoneNo();
                filterTelephoneNoNumber = filterTelephoneNoNumber.replace(/(\)|\()|_|-+/g, '');
                var filterHometelephoneNumber = portentrypass.WharfVehiclePermits().Hometelephone();
                filterHometelephoneNumber = filterHometelephoneNumber.replace(/(\)|\()|_|-+/g, '');

                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validCompanyFaxNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validCompanyFaxNumber++;
                            result = false;
                        }

                    }
                }


                var validTelephoneNoNumber = 0;
                if (filterTelephoneNoNumber.length != 0) {
                    if (filterTelephoneNoNumber.length != 13) {
                        toastr.warning("Invalid Telephone Number");
                        validTelephoneNoNumber++;
                        result = false;
                    } else if (filterTelephoneNoNumber.length == 13) {
                        var validNo = parseInt(filterTelephoneNoNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            validTelephoneNoNumber++;
                            result = false;
                        }

                    }
                }

                var validHometelephoneNumber = 0;
                if (filterHometelephoneNumber.length != 0) {
                    if (filterHometelephoneNumber.length != 13) {
                        toastr.warning("Invalid Home Telephone Number");
                        validHometelephoneNumber++;
                        result = false;
                    } else if (filterHometelephoneNumber.length == 13) {
                        var validNo = parseInt(filterHometelephoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Home Telephone Number");
                            validHometelephoneNumber++;
                            result = false;
                        }

                    }
                }

                if (validPhoneNumber != 0) {
                    $("#MobileNoWharfVehicleResubmissionD").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#MobileNoWharfVehicleResubmissionD").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoWharfVehicleResubmissionD").data("kendoMaskedTextBox");
                    portentrypass.CompanyTelephoneNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoWharfVehicleResubmissionD").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoWharfVehicleResubmissionD").data("kendoMaskedTextBox");
                    portentrypass.CompanyFaxNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }


                if (validTelephoneNoNumber != 0) {
                    $("#TelephoneNoWharfVehicleResubmissionD").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var TelephoneNomaskedtextbox = $("#TelephoneNoWharfVehicleResubmissionD").data("kendoMaskedTextBox");
                    portentrypass.WharfVehiclePermits().TelephoneNo(TelephoneNomaskedtextbox.value());
                    result = false;
                }

                if (validHometelephoneNumber != 0) {
                    $("#HometelephoneWharfVehicleResubmissionD").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var Hometelephonemaskedtextbox = $("#HometelephoneWharfVehicleResubmissionD").data("kendoMaskedTextBox");
                    portentrypass.WharfVehiclePermits().Hometelephone(Hometelephonemaskedtextbox.value());
                    result = false;
                }
            }

            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCE') {

                self.VisitorPermitsValidation = ko.observable(portentrypass.VisitorPermits);
                self.VisitorPermitsValidation().errors = ko.validation.group(self.VisitorPermitsValidation());
                VisitorPermitsValidationerrors = self.VisitorPermitsValidation().errors().length;

                if (errors != 0 || VisitorPermitsValidationerrors != 0) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details in The Form", "Port Entry Pass Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.VisitorPermitsValidation().errors.showAllMessages();
                    result = false;
                }
                if (self.portentrypassapplicationModel().VisitorPermits().PermitCode() == "" || self.portentrypassapplicationModel().VisitorPermits().PermitCode() == null || self.portentrypassapplicationModel().VisitorPermits().PermitCode == undefined) {
                    result = false;
                    toastr.warning("Please Select No of Days Permit Required For", "Port Entry Pass Request");
                }
                var filterPhoneNumber = portentrypass.MobileNo();
                filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyTelePhoneNumber = portentrypass.CompanyTelephoneNo();
                filterCompanyTelePhoneNumber = filterCompanyTelePhoneNumber.replace(/(\)|\()|_|-+/g, '');
                var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
                filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

                var filterTelephoneNoNumber = portentrypass.VisitorPermits().TelephoneNo();
                filterTelephoneNoNumber = filterTelephoneNoNumber.replace(/(\)|\()|_|-+/g, '');


                //CraftModel.PhoneNumber(filterPhoneNumber);
                var validPhoneNumber = 0;

                if (filterPhoneNumber.length != 0) {
                    if (filterPhoneNumber.length != 13) {
                        toastr.warning("Invalid Mobile Number");
                        validPhoneNumber++;
                        result = false;
                    }
                    else if (filterPhoneNumber.length == 13) {
                        var validNo = parseInt(filterPhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            validPhoneNumber++;
                            result = false;
                        }

                    }
                }
                var validCompanyTelePhoneNumber = 0;
                if (filterCompanyTelePhoneNumber.length != 0) {
                    if (filterCompanyTelePhoneNumber.length != 13) {
                        toastr.warning("Invalid Company Telephone Number");
                        validCompanyTelePhoneNumber++;
                        result = false;
                    }
                    else if (filterCompanyTelePhoneNumber.length == 13) {
                        var validNo = parseInt(filterCompanyTelePhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Telephone Number");
                            validCompanyTelePhoneNumber++;
                            result = false;
                        }
                    }
                }

                var validCompanyFaxNumber = 0;
                if (filterCompanyFaxNumber.length != 0) {
                    if (filterCompanyFaxNumber.length != 13) {
                        toastr.warning("Invalid Company Fax Number");
                        validCompanyFaxNumber++;
                        result = false;
                    } else if (filterCompanyFaxNumber.length == 13) {
                        var validNo = parseInt(filterCompanyFaxNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Company Fax Number");
                            validCompanyFaxNumber++;
                            result = false;
                        }

                    }
                }


                var validTelephoneNoNumber = 0;
                if (filterTelephoneNoNumber.length != 0) {
                    if (filterTelephoneNoNumber.length != 13) {
                        toastr.warning("Invalid Telephone Number");
                        validTelephoneNoNumber++;
                        result = false;
                    } else if (filterTelephoneNoNumber.length == 13) {
                        var validNo = parseInt(filterTelephoneNoNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            validTelephoneNoNumber++;
                            result = false;
                        }

                    }
                }


                if (validPhoneNumber != 0) {
                    $("#CompanyMobileNoResubmissionE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var MobileNomaskedtextbox = $("#CompanyMobileNoResubmissionE").data("kendoMaskedTextBox");
                    portentrypass.MobileNo(MobileNomaskedtextbox.value());
                }
                if (validCompanyTelePhoneNumber != 0) {
                    $("#CompanyTelephoneNoResubmissionE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNoResubmissionE").data("kendoMaskedTextBox");
                    portentrypass.CompanyTelephoneNo(CompanyTelephoneNomaskedtextbox.value());
                }

                if (validCompanyFaxNumber != 0) {
                    $("#CompanyFaxNoResubmissionE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var CompanyFaxNomaskedtextbox = $("#CompanyFaxNoResubmissionE").data("kendoMaskedTextBox");
                    portentrypass.CompanyFaxNo(CompanyFaxNomaskedtextbox.value());
                    result = false;
                }


                if (validTelephoneNoNumber != 0) {
                    $("#TelephoneNoResubmissionE").kendoMaskedTextBox({
                        mask: "(000)000-000-0000"
                    });
                    var TelephoneNomaskedtextbox = $("#TelephoneNoResubmissionE").data("kendoMaskedTextBox");
                    portentrypass.VisitorPermits().TelephoneNo(TelephoneNomaskedtextbox.value());
                    result = false;
                }



            }

            else if (self.portentrypassapplicationModel().PermitRequestTypeCode() == "") {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Fill Valid Details in The Form", "Port Entry Pass Request");
                result = false;
            }
            return result;
        }
        //***********************************************************************************************************************************************************************************************************
        //DeleteDocument method is delete upload document in API Service based on thedocument id and DocumentName into the pilotexemption request screen 
        self.multipleDeleteDocument = function (Adddoc) {
            self.portentrypassapplicationModel().PermitRequestDocuments.remove(Adddoc);
        }

        RefreshCaptach = function () {
            var captachText = randString(6);
            self.CaptachText(captachText);
        }
        RefreshCaptachResubmit = function () {
            var captachText = randString(6);
            self.CaptachTextResubmit(captachText);
        }
        RefreshCaptachRejected = function () {
            var captachText = randString(6);
            self.CaptachTextRejected(captachText);
        }
        self.validationContracterName = function () {

            if ($("#ContracterNameNewB").val() != "") {
                self.isCompanyNameMsg(false);
                $("#isCompanyNameMsg").text('');
            }
            else {
                self.isCompanyNameMsg(true);
                $("#isCompanyNameMsg").text('* Please Enter The Company Name');

            }
        }
        self.validationContracteNo = function () {

            if ($("#ContracterNONewB").val() != "") {
                $("#isContractNoMsg").text('');
                self.isContractNoMsg(false);
            }
            else {
                $("#isContractNoMsg").text('* Please Enter The Contract No.');
                self.isContractNoMsg(true);

            }
        }
        self.validationContractermaneger = function () {

            if ($("#ContractermanegerNewB").val() != "") {
                $("#isContractManagerNameMsg").text('');
                self.isContractManagerNameMsg(false);
            }
            else {
                $("#isContractManagerNameMsg").text('* Please Enter The Contractor Maneger');
                self.isContractManagerNameMsg(true);

            }
        }
        self.validationContracterduretion = function () {

            if ($("#ContracterduretionNewB").val() != "") {
                $("#isCDurationMsg").text('');
                self.isContractDurationMsg(false);
            }
            else {
                $("#isCDurationMsg").text('* Please Enter The Contract Duration');
                self.isContractDurationMsg(true);

            }
        }
        self.validationserviceCompenyname = function () {

            if ($("#serviceCompenynameNewB").val() != "") {
                $("#isServiceCompanyNameMsg").text('');
                self.isServiceCompanyNameMsg(false);
            }
            else {
                $("#isServiceCompanyNameMsg").text('* Please Enter The Service Company Name');
                self.isServiceCompanyNameMsg(true);

            }
        }
        self.validationResponsibleManager = function () {

            if ($("#ResponsibleManagerNewB").val() != "") {
                $("#isResponsibleManagerMsg").text('');
                self.isResponsibleManagerMsg(false);
            }
            else {
                $("#isResponsibleManagerMsg").text('* Please Enter The Responsible Manager');
                self.isResponsibleManagerMsg(true);

            }
        }
        self.validationContactTelephoneNo = function () {

            if ($("#ContactTelephoneNoNewB").val() != "") {
                $("#isContactNoMsg").text('');
                self.isContactNoMsg(false);
            }
            else {
                $("#isContactNoMsg").text('* Please Enter The Contact Telephone No.');
                self.isContactNoMsg(true);

            }
        }
        self.validationMobileNo = function () {

            if ($("#MobileNoNewB").val() != "") {
                $("#isMobileNoMsg").text('');
                self.isMobileNoMsg(false);
            }
            else {
                $("#isMobileNoMsg").text('* Please Enter The Mobile No.');
                self.isMobileNoMsg(true);

            }
        }
        self.validationVehicleMake = function () {
            if ($("#VehicleMakeNewD").val() == null || $("#VehicleMakeNewD").val() != "") {
                self.isVehicleMakeNewDMsg(false);
                $("#isVehicleMakeNewDMsg").text('');

            }
            else {
                $("#isVehicleMakeNewDMsg").text('* Please Enter The Vehicle Make');
                self.isVehicleMakeNewDMsg(true);
            }
        }
        self.validationVehicleModel = function () {
            if ($("#VehicleModelNewD").val() == null || $("#VehicleModelNewD").val() != "") {
                self.isVehicleModelNewDMsg(false);
                $("#isVehicleModelNewDMsg").text('');

            }
            else {
                $("#isVehicleModelNewDMsg").text('* Please Enter The Vehicle Model');
                self.isVehicleModelNewDMsg(true);
            }
        }
        self.validationVehicleRegnNo = function () {
            if ($("#VehicleRegnNoNewD").val() == null || $("#VehicleRegnNoNewD").val() != "") {
                self.isVehicleRegnNoNewDMsg(false);
                $("#isVehicleRegnNoNewDMsg").text('');

            }
            else {
                $("#isVehicleRegnNoNewDMsg").text('* Please Enter The Vehicle Regn No.');
                self.isVehicleRegnNoNewDMsg(true);
            }
        }
        self.validationVehicleDescription = function () {
            if ($("#VehicleDescriptionNewD").val() == null || $("#VehicleDescriptionNewD").val() != "") {
                self.isVehicleDescriptionNewDMsg(false);
                $("#isVehicleDescriptionNewDMsg").text('');

            }
            else {
                $("#isVehicleDescriptionNewDMsg").text('* Please Enter The Vehicle Description');
                self.isVehicleDescriptionNewDMsg(true);
            }
        }

        SearchRequesttoCal = function () {
            this.min($("#SearchRequestFrom").val());
            var myDatePicker = new Date($("#SearchRequestFrom").val());
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth() + 2;
            var year = myDatePicker.getFullYear();
            this.max(new Date(year, month, day));

        }

        self.validationVehiclePointed = function () {

            if (self.portentrypassapplicationModel().WharfVehiclePermits().VehiclePointed() != "") {
                self.isVehiclePointedMsg(false);
                $("#isVehiclePointedMsg").text('');

            }
            else {
                $("#isVehiclePointedMsg").text('* Please Select The Vehicle Pointed');
                self.isVehiclePointedMsg(true);
            }
        }
        ////
        self.validationSACitizen = function () {

            if (self.portentrypassapplicationModel().SACitizen() == "") {
                self.isSACitizenMsg(false);
                $("#isSACitizenMsg").text('');

            }
            else {
                $("#isSACitizenMsg").text('* Please Select The SA Citizen');
                self.isSACitizenMsg(true);
            }
        }
        ///
        self.validationVehicleRegisterd = function () {

            if (self.portentrypassapplicationModel().WharfVehiclePermits().VehicleRegisterd() == "") {
                self.isVehicleRegisterdMsg(false);
                $("#isVehicleRegisterdMsg").text('');

            }
            else {
                $("#isVehicleRegisterdMsg").text('* Please Select The Vehicle Registerd');
                self.isVehicleRegisterdMsg(true);
            }
        }
        self.validationCompanynamevisitornew = function () {
            if ($("#Companynamevisitornew").val() == "" || $("#Companynamevisitornew").val() == null) {
                $("#isVisitorpermitCompanynameNewGMsg").text('* Please Enter The Company Name');
                self.isVisitorpermitCompanynameNewGMsg(true);
                result = false;

            }
            else {
                self.isVisitorpermitCompanynameNewGMsg(false);
                $("#isVisitorpermitCompanynameNewGMsg").text('');
            }
        }
        self.validationDivisionvisitornew = function () {
            if ($("#Divisionvisitornew").val() == "" || $("#Divisionvisitornew").val() == null) {
                $("#isVisitorpermitDivisionNewGMsg").text('* Please Enter The Division Name');
                self.isVisitorpermitDivisionNewGMsg(true);
                result = false;
            }
            else {
                self.isVisitorpermitDivisionNewGMsg(false);
                $("#isVisitorpermitDivisionNewGMsg").text('');


            }
        }
        self.validationAuthorizedPersonNamevisitornew = function () {
            if ($("#AuthorizedPersonNamevisitornew").val() == "" || $("#AuthorizedPersonNamevisitornew").val() == null) {
                $("#isVisitorpermitAuthorizedPersonNameNewGMsg").text('* Please Enter The Authorized Person Name');
                self.isVisitorpermitAuthorizedPersonNameNewGMsg(true);
                result = false;
            }
            else {
                self.isVisitorpermitAuthorizedPersonNameNewGMsg(false);
                $("#isVisitorpermitAuthorizedPersonNameNewGMsg").text('');

            }
        }
        self.validationPositionHeldvisitornew = function () {
            if ($("#PositionHeldvisitornew").val() == "" || $("#PositionHeldvisitornew").val() == null) {
                $("#isVisitorpermitPositionHeldNewGMsg").text('* Please Enter The Position Held');
                self.isVisitorpermitPositionHeldNewGMsg(true);
                result = false;
            }
            else {
                self.isVisitorpermitPositionHeldNewGMsg(false);
                $("#isVisitorpermitPositionHeldNewGMsg").text('');


            }
        }
        self.validationTelephoneNovisitornew = function () {
            if ($("#TelephoneNovisitornew").val() == "" || $("#TelephoneNovisitornew").val() == null) {
                $("#isVisitorpermitTelephoneNoNewGMsg").text('* Please Enter The Telephone No.');
                self.isVisitorpermitTelephoneNoNewGMsg(true);
                result = false;
            }
            else {
                self.isVisitorpermitTelephoneNoNewGMsg(false);
                $("#isVisitorpermitTelephoneNoNewGMsg").text('');
            }
        }
        //self.validationEscortNamevisitornew = function () {
        //    if ($("#EscortNamevisitornew").val() == "" || $("#EscortNamevisitornew").val() == null) {
        //        $("#isVisitorpermitEscortNameNewGMsg").text('* Please Enter The Escort Name');
        //        self.isVisitorpermitEscortNameNewGMsg(true);
        //        result = false;
        //    }
        //    else {
        //        self.isVisitorpermitEscortNameNewGMsg(false);
        //        $("#isVisitorpermitEscortNameNewGMsg").text('');

        //    }
        //}
        //self.validationPermitNovisitornew = function () {
        //    if ($("#PermitNovisitornew").val() == "" || $("#PermitNovisitornew").val() == null) {
        //        $("#isVisitorpermitPermitNoNewGMsg").text('* Please Enter The Permit No.');
        //        self.isVisitorpermitPermitNoNewGMsg(true);
        //        result = false;
        //    }
        //    else {
        //        self.isVisitorpermitPermitNoNewGMsg(false);
        //        $("#isVisitorpermitPermitNoNewGMsg").text('');
        //    }
        //}

        self.AddVehicle = function () {
            //self.RepoInModel().RepoContainersDtos.unshift(new eGateRoot.RepoContainersDto());
            //$('#DriverMobileNo0').kendoMaskedMobile();
        }

        self.Validationclear = function () {

            ko.validation.group(self.portentrypassapplicationModel()).showAllMessages(false);
            ko.validation.group(self.portentrypassapplicationModel().PermitRequestContractors()).showAllMessages(false);
            ko.validation.group(self.portentrypassapplicationModel().PersonalPermits()).showAllMessages(false);
            ko.validation.group(self.portentrypassapplicationModel().VisitorPermits()).showAllMessages(false);
            ko.validation.group(self.portentrypassapplicationModel().VehiclePermits()).showAllMessages(false);
            ko.validation.group(self.portentrypassapplicationModel().WharfVehiclePermits()).showAllMessages(false);
            // ko.validation.group(self.portentrypassapplicationModel().IndividualVehiclePermits()).showAllMessages(false);
            $("#spanmultiplepfileToUpload11").text('');
            $("#spanNewFileToUpload").text('');
            $("#spanCaptachCode").text('');

        }
        self.validationPermitRequirementType = function () {

            if (self.portentrypassapplicationModel().WharfVehiclePermits().PermitRequirement() == "") {
                toastr.warning("Please Select Any Permit Requirement Type", "Port Entry Pass Request");
                $("#ispermitrequirementWharfvechiclenewDMsg").text('* Please Enter Any Permit Requirement Type');
                self.ispermitrequirementWharfvechiclenewDMsg(true);
                return false;
            }
            else {
                $("#ispermitrequirementWharfvechiclenewDMsg").text('');
                self.ispermitrequirementWharfvechiclenewDMsg(false);
                return true;
            }
        }
        //---------------------------------------Individual application-----------------------------------------
        self.validationIndividualApplicantMobileNo = function () {
            if ($("#IndividualApplicationMobileNo").val() == "" || $("#IndividualApplicationMobileNo").val() == null) {
                $("#isMobileNoMsg").text('* Please Enter The Mobile No.');
                self.isMobileNoMsg(true);
                result = false;
            }
            else {
                self.isMobileNoMsg(false);
                $("#isMobileNoMsg").text('');
            }

        }
        self.validationIndividualApplicantCmpnyTelephoneNo = function () {
            if ($("#IndividualApplicationCmpTelephoneNo").val() == "" || $("#IndividualApplicationCmpTelephoneNo").val() == null) {
                $("#isCmpnyTelephoneNoMsg").text('* Please Enter The Company Telephone No.');
                self.isCmpnyTelephoneNoMsg(true);
                result = false;
            }
            else {
                self.isCmpnyTelephoneNoMsg(false);
                $("#isCmpnyTelephoneNoMsg").text('');
            }

        }

        self.validationAuthorisedTelephoneWork = function () {
            if ($("#AuthorisedTelephoneWork").val() == "" || $("#AuthorisedTelephoneWork").val() == null) {
                $("#isAuthorisedTelephoneWorkMsg").text('* Please Enter The Authorised Telephone Work');
                self.isAuthorisedTelephoneWorkMsg(true);
                result = false;
            }
            else {
                self.isAuthorisedTelephoneWorkMsg(false);
                $("#isAuthorisedTelephoneWorkMsg").text('');
            }

        }

        self.validationAuthorisedMobileNo = function () {
            if ($("#AuthorisedMobile").val() == "" || $("#AuthorisedMobile").val() == null) {
                $("#isAuthorisedMobileMsg").text('* Please Enter The Authorised mobile No.');
                self.isAuthorisedMobileMsg(true);
                result = false;
            }
            else {
                self.isAuthorisedMobileMsg(false);
                $("#isAuthorisedMobileMsg").text('');
            }

        }
        //-----------------------------------------------contractor Application-----------------
        self.validationTelephoneNumber = function () {
            if ($("#TelephoneNumber").val() == "" || $("#TelephoneNumber").val() == null) {
                $("#isTelephoneNumberNoMsg").text('* Please Enter The Telephone Number');
                self.isTelephoneNumberNoMsg(true);
                result = false;
            }
            else {
                self.isTelephoneNumberNoMsg(false);
                $("#isTelephoneNumberNoMsg").text('');
            }

        }
        self.validateSubContractorTelephoneNumber = function () {
            if ($("#SubContractorTelephoneNumber").val() == "" || $("#SubContractorTelephoneNumber").val() == null) {
                $("#isSubContractorTelephoneNumberNoMsg").text('* Please Enter The Sub Contractor Telephone Number ');
                self.isSubContractorTelephoneNumberNoMsg(true);
                result = false;
            }
            else {
                self.isSubContractorTelephoneNumberNoMsg(false);
                $("#isSubContractorTelephoneNumberNoMsg").text('');
            }

        }
        self.validationCntAuthorisedTelephoneWork = function () {
            if ($("#CntAuthorisedTelephoneWork").val() == "" || $("#CntAuthorisedTelephoneWork").val() == null) {
                $("#isCntAuthorisedTelephoneWorkMsg").text('* Please Enter The Authorised Telephone Work');
                self.isCntAuthorisedTelephoneWorkMsg(true);
                result = false;
            }
            else {
                self.isCntAuthorisedTelephoneWorkMsg(false);
                $("#isCntAuthorisedTelephoneWorkMsg").text('');
            }

        }

        self.validationCntAuthorisedMobileNo = function () {
            if ($("#CntAuthorisedMobile").val() == "" || $("#CntAuthorisedMobile").val() == null) {
                $("#isCntAuthorisedMobileMsg").text('* Please Enter The Authorised mobile No.');
                self.isCntAuthorisedMobileMsg(true);
                result = false;
            }
            else {
                self.isCntAuthorisedMobileMsg(false);
                $("#isCntAuthorisedMobileMsg").text('');
            }

        }

        //---------------------------------------resubmissionvalidation-----------------------------------------
        self.validationCompanyNameResubmissionB = function () {

            if ($("#CompanyNameResubmissionB").val() == "" || $("#CompanyNameResubmissionB").val() == null) {
                $("#isCompanyNameResubmissionMsg").text('* Please Enter The Company Name');
                self.isCompanyNameResubmissionMsg(true);
                result = false;
            }
            else {
                self.isCompanyNameResubmissionMsg(false);
                $("#isCompanyNameResubmissionMsg").text('');
            }
        }
        self.validationContractNoResubmissionB = function () {

            if ($("#ContractNoResubmissionB").val() == "" || $("#ContractNoResubmissionB").val() == null) {
                $("#isContractNoResubmissionMsg").text('* Please Enter The Contract No.');
                self.isContractNoResubmissionMsg(true);
                result = false;
            }
            else {
                self.isContractNoResubmissionMsg(false);
                $("#isContractNoResubmissionMsg").text('');
            }
        }
        self.validationContractManagerNameResubmissionB = function () {
            if ($("#ContractManagerNameResubmissionB").val() == "" || $("#ContractManagerNameResubmissionB").val() == null) {
                $("#isContractManagerNameResubmissionMsg").text('* Please Enter The Contractor Maneger');
                self.isContractManagerNameResubmissionMsg(true);
                result = false;
            }
            else {
                self.isContractManagerNameResubmissionMsg(false);
                $("#isContractManagerNameResubmissionMsg").text('');
            }

        }
        self.validationContractDurationResubmissionB = function () {
            if ($("#ContractDurationResubmissionB").val() == "" || $("#ContractDurationResubmissionB").val() == null) {
                $("#isContractDurationResubmissionMsg").text('* Please Enter The Contract Duration');
                self.isContractDurationResubmissionMsg(true);
                result = false;
            }
            else {
                self.isContractDurationResubmissionMsg(false);
                $("#isContractDurationResubmissionMsg").text('');

            }
        }
        self.validationServiceCompanyNameResubmissionB = function () {
            if ($("#ServiceCompanyNameResubmissionB").val() == "" || $("#ServiceCompanyNameResubmissionB").val() == null) {
                $("#isServiceCompanyNameResubmissionMsg").text('* Please Enter The Service Company Name');
                self.isServiceCompanyNameResubmissionMsg(true);
                result = false;
            }
            else {
                self.isServiceCompanyNameResubmissionMsg(false);
                $("#isServiceCompanyNameResubmissionMsg").text('');

            }
        }
        self.validationResponsibleManagerResubmissionB = function () {
            if ($("#ResponsibleManagerResubmissionB").val() == "" || $("#ResponsibleManagerResubmissionB").val() == null) {
                $("#isResponsibleManagerResubmissionMsg").text('* Please Enter The Responsible Manager');
                self.isResponsibleManagerResubmissionMsg(true);
                result = false;
            }
            else {
                self.isResponsibleManagerResubmissionMsg(false);
                $("#isResponsibleManagerResubmissionMsg").text('');
            }
        }
        self.validationContactTelephoneNoResubmissionB = function () {
            if ($("#ContactTelephoneNoResubmissionB").val() == "" || $("#ContactTelephoneNoResubmissionB").val() == null) {
                $("#isContactNoResubmissionMsg").text('* Please Enter The Contact Telephone No.');
                self.isContactNoResubmissionMsg(true);
                result = false;
            }
            else {
                self.isContactNoResubmissionMsg(false);
                $("#isContactNoResubmissionMsg").text('');
            }
        }
        self.validationContactMobileNoResubmissionB = function () {
            if ($("#ContactMobileNoResubmissionB").val() == "" || $("#ContactMobileNoResubmissionB").val() == null) {
                $("#isMobileNoResubmissionMsg").text('* Please Enter The Mobile No.');
                self.isMobileNoResubmissionMsg(true);
                result = false;
            }
            else {
                self.isMobileNoResubmissionMsg(false);
                $("#isMobileNoResubmissionMsg").text('');
            }

        }
        self.validationVehicleMakewharfResubmissionD = function () {

            if ($("#VehicleMakewharfResubmissionD").val() == "" || $("#VehicleMakewharfResubmissionD").val() == null) {
                $("#isVehicleMakewharfResubmissionMsg").text('* Please Enter The Vehicle Make');
                self.isVehicleMakewharfResubmissionMsg(true);
                result = false;
            }
            else {
                self.isVehicleMakewharfResubmissionMsg(false);
                $("#isVehicleMakewharfResubmissionMsg").text('');

            }
        }
        self.validationVehicleModelwharfResubmissionD = function () {
            if ($("#VehicleModelwharfResubmissionD").val() == "" || $("#VehicleModelwharfResubmissionD").val() == null) {
                $("#isVehicleModelwharfResubmissionMsg").text('* Please Enter The Vehicle Model');
                self.isVehicleModelwharfResubmissionMsg(true);
                result = false;
            }
            else {
                self.isVehicleModelwharfResubmissionMsg(false);
                $("#isVehicleModelwharfResubmissionMsg").text('');

            }

        }
        self.validationVehicleRegnNowharfResubmissionD = function () {
            if ($("#VehicleRegnNowharfResubmissionD").val() == "" || $("#VehicleRegnNowharfResubmissionD").val() == null) {
                $("#isVehicleRegnNowharfResubmissionMsg").text('* Please Enter The Vehicle Regn No.');
                self.isVehicleRegnNowharfResubmissionMsg(true);
                result = false;
            }
            else {
                self.isVehicleRegnNowharfResubmissionMsg(false);
                $("#isVehicleRegnNowharfResubmissionMsg").text('');
            }
        }
        self.validationVehicleDescriptionwharfResubmissionD = function () {
            if ($("#VehicleDescriptionwharfResubmissionD").val() == "" || $("#VehicleDescriptionwharfResubmissionD").val() == null) {
                $("#isVehicleDescriptionwharfResubmissionMsg").text('* Please Enter The Vehicle Description');
                self.isVehicleDescriptionwharfResubmissionMsg(true);
                result = false;

            }
            else {
                self.isVehicleDescriptionwharfResubmissionMsg(false);
                $("#isVehicleDescriptionwharfResubmissionMsg").text('');

            }
        }
        self.validationisVehicleRegisterdwharfResubmission = function () {
            if (self.portentrypassapplicationModel().WharfVehiclePermits().VehiclePointed() == "") {
                $("#isVehicleRegisterdwharfResubmissionMsg").text('* Please Select Is The Vehicle Pointed');
                self.isVehicleRegisterdwharfResubmissionMsg(true);
                result = false;
            }
            else {
                self.isVehicleRegisterdwharfResubmissionMsg(false);
                $("#isVehicleRegisterdwharfResubmissionMsg").text('');

            }
        }
        self.validationVehiclePointedwharfResubmission = function () {
            if (self.portentrypassapplicationModel().WharfVehiclePermits().VehicleRegisterd() == "") {
                $("#isVehiclePointedwharfResubmissionMsg").text('* Please Select The Is The Vehicle Registerd');
                self.isVehiclePointedwharfResubmissionMsg(true);
                result = false;
            }
            else {
                self.isVehiclePointedwharfResubmissionMsg(false);
                $("#isVehiclePointedwharfResubmissionMsg").text('');
            }
        }
        self.validationPermitRequeirementstypeswharfResubmission = function () {
            if (self.portentrypassapplicationModel().WharfVehiclePermits().PermitRequirement() == "") {
                toastr.warning("Please Select Any Permit Requirement Type", "Port Entry Pass Request");
                $("#isPermitRequeirementstypeswharfResubmissionMsg").text('* Please Select Any Permit Requirement Type');
                self.isPermitRequeirementstypeswharfResubmissionMsg(true);
                result = false;
            }
            else {
                $("#isPermitRequeirementstypeswharfResubmissionMsg").text('');
                self.isPermitRequeirementstypeswharfResubmissionMsg(false);
            }


        }
        self.validationCompanyNamevisitorResubmissionE = function () {
            if ($("#CompanyNamevisitorResubmissionE").val() == "" || $("#CompanyNamevisitorResubmissionE").val() == null) {
                $("#isCompanyNamevisitorResubmissionMsg").text('* Please Enter The Company Name');
                self.isCompanyNamevisitorResubmissionMsg(true);
                result = false;

            }
            else {
                self.isCompanyNamevisitorResubmissionMsg(false);
                $("#isCompanyNamevisitorResubmissionMsg").text('');
            }
        }
        self.validationDivisionvisitorResubmissionEB = function () {
            if ($("#DivisionvisitorResubmissionE").val() == "" || $("#DivisionvisitorResubmissionE").val() == null) {
                $("#isDivisionvisitorResubmissionMsg").text('* Please Enter The Division Name');
                self.isDivisionvisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isDivisionvisitorResubmissionMsg(false);
                $("#isDivisionvisitorResubmissionMsg").text('');


            }
        }
        self.validationAuthorizedPersonNamevisitorResubmissionE = function () {
            if ($("#AuthorizedPersonNamevisitorResubmissionE").val() == "" || $("#AuthorizedPersonNamevisitorResubmissionE").val() == null) {
                $("#isAuthorizedPersonNamevisitorResubmissionMsg").text('* Please Enter The Authorized Person Name');
                self.isAuthorizedPersonNamevisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isAuthorizedPersonNamevisitorResubmissionMsg(false);
                $("#isAuthorizedPersonNamevisitorResubmissionMsg").text('');

            }
        }
        self.validationPositionHeldvisitorResubmissionE = function () {
            if ($("#PositionHeldvisitorResubmissionE").val() == "" || $("#PositionHeldvisitorResubmissionE").val() == null) {
                $("#isPositionHeldvisitorResubmissionMsg").text('* Please Enter The Position Held');
                self.isPositionHeldvisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isPositionHeldvisitorResubmissionMsg(false);
                $("#isPositionHeldvisitorResubmissionMsg").text('');

            }
        }
        self.validationTelephoneNoResubmissionE = function () {
            if ($("#TelephoneNoResubmissionE").val() == "" || $("#TelephoneNoResubmissionE").val() == null) {
                $("#isTelephoneNovisitorResubmissionMsg").text('* Please Enter The Telephone No.');
                self.isTelephoneNovisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isTelephoneNovisitorResubmissionMsg(false);
                $("#isTelephoneNovisitorResubmissionMsg").text('');
            }
        }
        self.validationEscortNamevisitorResubmissionE = function () {
            if ($("#EscortNamevisitorResubmissionE").val() == "" || $("#EscortNamevisitorResubmissionE").val() == null) {
                //$("#isEscortNamevisitorResubmissionMsg").text('* Please Enter The Escort Name');
                self.isEscortNamevisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isEscortNamevisitorResubmissionMsg(false);
                $("#isEscortNamevisitorResubmissionMsg").text('');

            }
        }
        self.validationPermitNovisitorResubmissionE = function () {
            if ($("#PermitNovisitorResubmissionE").val() == "" || $("#PermitNovisitorResubmissionE").val() == null) {
                $("#isPermitNovisitorResubmissionMsg").text('* Please Enter The Permit No.');
                self.isPermitNovisitorResubmissionMsg(true);
                result = false;
            }
            else {
                self.isPermitNovisitorResubmissionMsg(false);
                $("#isPermitNovisitorResubmissionMsg").text('');
            }
        }

        function onDeselect(e) {
            if ("kendoConsole" in window) {
                var dataItem = e.dataItem;
                kendoConsole.log("event :: deselect (" + dataItem.text + " : " + dataItem.value + ")");
            }
        };

        var PreAreaLength = 0;
        var CurrAreaLength = 0;
        selectAccessArea = function (data, model, event) {

            CurrAreaLength = data.PermitRequestAreas().length;
            if (parseInt(CurrAreaLength) === 0) {
                PreAreaLength = 0;
            }
            else if (parseInt(CurrAreaLength) === 1) {
                PreAreaLength = PreAreaLength;
            }
            //if (parseInt(CurrAreaLength) !== 1) {
            //    PreAreaLength = CurrAreaLength - 1;
            //}
            var actlength = data.PermitRequestAreas().length;
            var len = data.PermitRequestAreas().length - 1
            var supCatCode = data.PermitRequestAreas()[len];
            var areas = data.PermitRequestAreas();
            if (data.PermitRequestAreas().length === 0) {
                self.SubAccessAreasForRB.removeAll();
                if (data.PermitRequestSubAreas().length > 0) {
                    data.PermitRequestSubAreas('');
                }
            }
            if (parseInt(CurrAreaLength) === 0 && supCatCode !== undefined) {
                self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: supCatCode },
                         function (result1) {
                             $.each(result1, function (index, data) {
                                 self.SubAccessAreasForRB.remove(new IPMSRoot.SubAccessAreasForRB(data));
                             });
                         }, null, null, false);

            }

            if (parseInt(PreAreaLength) <= parseInt(CurrAreaLength) && supCatCode !== undefined) {
                self.SubAccessAreasForRB.removeAll();
                var sublength = data.PermitRequestSubAreas().length;
                $.each(data.PermitRequestAreas(), function (index, areadata) {
                    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                          function (result1) {
                              $.each(result1, function (index, resdata) {
                                  self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));

                              });


                          }, null, null, false);

                });
            }
            else {
                data.PermitRequestSubAreaDiss(data.PermitRequestSubAreas());
                self.SubAccessAreasForRB.removeAll();
                var length = data.PermitRequestSubAreas().length;
                data.PermitRequestSubAreas('');
                var permitreqSubArealist = [];
                $.each(data.PermitRequestAreas(), function (index, areadata) {
                    self.viewModelHelper.apiGet('api/PortEntryPassApplication/GetSubAccessAreasForRB/' + supCatCode, { supCatCode: areadata },
                         function (result1) {
                             $.each(result1, function (index, resdata) {
                                 self.SubAccessAreasForRB.push(new IPMSRoot.SubAccessAreasForRB(resdata));
                             });
                             $.each(data.PermitRequestSubAreaDiss(), function (index, subdata) {
                                 if (subdata.split('_')[1] === areadata) {
                                     permitreqSubArealist.push(subdata);
                                 }
                             });
                             data.PermitRequestSubAreas(permitreqSubArealist);

                         }, null, null, false);
                });

            }
            PreAreaLength = data.PermitRequestAreas().length;


        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        $("#MobileNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyTelephoneNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyFaxNoNewA").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        $("#ConteractMobileNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyTelephoneNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#ContactTelephoneNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#MobileNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyFaxNoNewB").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        $("#CompanyTelephoneNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyFaxNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#MobileNoNewC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        $("#CompanyTelephoneNonewD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyFaxNonewD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#NewTelephoneNoD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#HometelephoneNEWD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#MobileNoNEWD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        $("#MobileNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyFaxNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CompanyTelephoneNoNEWE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#TelephoneNovisitornew").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        //--------Indi Appli----------
        $("#IndividualApplicationMobileNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#IndividualApplicationCmpTelephoneNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#AuthorisedTelephoneWork").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#AuthorisedMobile").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        //---------------Contractor appli
        $("#TelephoneNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#SubContractorTelephoneNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CntAuthorisedTelephoneWork").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CntAuthorisedMobile").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        //-----------
        //--------Resubmission
        self.AddNewVehicle = function (data) {

            if (data.IndividualVehiclePermits().length > 0) {
                var ManError = "Y";
                $.map(data.IndividualVehiclePermits, function (item) {
                    var CommoditiesListC = item;
                    if (CommoditiesListC != null)
                        ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                            if (CommodChk !== undefined) {
                                if (CommodChk.VehicleRegnNo() == "" || CommodChk.VehicleMake() == "" || CommodChk.VehicleModel() == "" || CommodChk.Chassis_VinNo() == "" || CommodChk.Colour() == "") {
                                    toastr.warning("Please Enter Vehicle Details", "Port Entry Pass");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y") {
                    var acomodity = new IPMSROOT.IndividualVehiclePermit();
                    self.portentrypassapplicationModel().IndividualVehiclePermits.push(acomodity);
                }
            }
            else {
                var acomodity = new IPMSROOT.IndividualVehiclePermit();
                self.portentrypassapplicationModel().IndividualVehiclePermits.push(acomodity);
            }

        }


        self.AddNewEmployee = function (data) {

            if (data.ContractorPermitEmployeeDetails().length > 0) {
                var ManError = "Y";
                $.map(data.ContractorPermitEmployeeDetails, function (item) {
                    var CommoditiesListC = item;
                    if (CommoditiesListC != null)
                        ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                            if (CommodChk !== undefined) {
                                if (CommodChk.EmployeeName() == "" || CommodChk.IDNumber() == "" || CommodChk.JobTitle() == "" || CommodChk.CriminalRecord() == "" || CommodChk.EmpSignature() == "") {
                                    toastr.warning("Please Enter Employee Details", "Port Entry Pass");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y") {
                    var acomodity = new IPMSROOT.ContractorPermitEmployeeDetail();
                    self.portentrypassapplicationModel().ContractorPermitEmployeeDetails.push(acomodity);
                }
            }
            else {
                var acomodity = new IPMSROOT.ContractorPermitEmployeeDetail();
                self.portentrypassapplicationModel().ContractorPermitEmployeeDetails.push(acomodity);
            }

        }
        self.removeEmployee = function (data) {
            self.portentrypassapplicationModel().ContractorPermitEmployeeDetails.remove(data);
        }

        self.removeVehicle = function (data) {
            self.portentrypassapplicationModel().IndividualVehiclePermits.remove(data);
        }

        function randString(x) {
            var s = "";
            while (s.length < x && x > 0) {
                var r = Math.random();
                s += (r < 0.1 ? Math.floor(r * 100) : String.fromCharCode(Math.floor(r * 26) + (r > 0.5 ? 97 : 65)));
            }
            return s;
        }

        ChangeCaptachCode = function () {
            if ($("#txtCaptachCode").val() != "") {

                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                }
            }
            else {
                $("#spanCaptachCode").text('* This field is required');
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }

        ChangeCaptachCodeResubmit = function () {
            if ($("#txtCaptachCodeResubmit").val() != "") {

                if ($("#txtCaptachCodeResubmit").val() == $("#lblCaptachResubmit").text()) {
                    $("#spanCaptachCodeResubmit").text('');
                }
                else {
                    $("#spanCaptachCodeResubmit").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachTextResubmit(captachText);
                }
            }
            else {
                $("#spanCaptachCodeResubmit").text('* This field is required');
                var captachText = randString(6);
                self.CaptachTextResubmit(captachText);
            }
        }

        ChangeCaptachCodeRejected = function () {
            if ($("#txtCaptachCodeRejected").val() != "") {

                if ($("#txtCaptachCodeRejected").val() == $("#lblCaptachRejected").text()) {
                    $("#spanCaptachCodeRejected").text('');
                }
                else {
                    $("#spanCaptachCodeRejected").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachTextRejected(captachText);
                }
            }
            else {
                $("#spanCaptachCodeRejected").text('* This field is required');
                var captachText = randString(6);
                self.CaptachTextRejected(captachText);
            }
        }

        PermitRequestTypeCodeChange = function () {
            var captachText = randString(6);
            self.CaptachText(captachText);
        }

        self.Initialize();
    }
    IPMSRoot.PortEntryPassApplicationViewModel = PortEntryPassApplicationViewModel;
}(window.IPMSROOT));
ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});
toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";
