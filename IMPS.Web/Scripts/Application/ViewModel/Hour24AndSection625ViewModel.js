toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot)   {
    var isView = 0;

    var Hour24AndSection625ViewModel = function (viewDetail) {
        var self = this;
        $('#spnTitle').html(" 24 Hour Report 62(5)  ");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.hour24andsection625Model = ko.observable(new IPMSROOT.Hour24AndSection625Model());
        self.viewMode = ko.observable();
        self.hour24reportreferencedata = ko.observable();
        self.Selected = ko.observable(false);
        self.hours24report625list = ko.observableArray([]);
        self.viewMode = ko.observable();
        self.IsEnable = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.viewModeForTabs = ko.observable();
        self.IsReset = ko.observable(false);
        self.isSubmitVisible = ko.observable(false);
        self.isSaveVisible = ko.observable(true);
        self.isGoBackVisible = ko.observable(false);
        self.isSavebuttonvisable = ko.observable(false);
        self.CaptachText = ko.observable();
        self.CDContactNumber = ko.observable();
        self.CDMobileNumber = ko.observable();

        self.Initialize = function () {

            //self.viewMode = ko.observable(true);           
            self.LoadInitialData();
            self.Loadhour24report625list();
            self.viewModeForTabs('notification1');
            if (viewDetail == true) {
                self.isSavebuttonvisable(true);
                self.IsEnable(true);
                var index = 0;
                HandleProgressBarAndActiveTab(index);

            }
            else {
                self.viewMode('List');
                var index = 0;
                self.isSavebuttonvisable(false);
                HandleProgressBarAndActiveTab(index);
            }
            var captachText = randString(6);
            self.CaptachText(captachText);

        }

        var validationTextMessage = 'This field is required';

        self.Cancel = function () {
            self.viewMode('List');
            self.hour24andsection625Model().reset();
            $('#spnTitle').html("24 Hour Report 62(5) ");
        }

        calOpen = function () {
            this.max(new Date());
        };

        self.Reset = function (data) {
            self.viewMode('List');
            self.hour24andsection625Model().reset();
            //  $('input[name=naturetype]').attr('checked', false);

        }

        //***************************************************************************************************************
        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/Hour24ReportReferenceData', null,
                    function (result1) {

                        self.hour24reportreferencedata(new IPMSRoot.Hour24ReportReferenceData(result1));
                    }, null, null, false);
        }
        self.Loadhour24report625list = function () {
            self.viewModelHelper.apiGet('api/hour24report625List', null,
                       function (result) {
                           self.hours24report625list(ko.utils.arrayMap(result, function (item) {
                               return new IPMSRoot.Hour24AndSection625Model(item);
                               self.hour24andsection625Model(new IPMSROOT.Hour24AndSection625Model(undefined));
                           }));
                       });
        }
        self.LoadFromhours24report625Data = function (data) {

            self.viewModelHelper.apiGet('api/FormHour24Report625', { ID: data.Hour24Report625ID(), Value: data.NONatureCode() },
                function (result) {

                    self.hour24andsection625Model(new IPMSROOT.Hour24AndSection625Model(result));
                    var index = 0;
                    HandleProgressBarAndActiveTab(index);
                    $("#CDContactNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDContactNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDContactNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDContactNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDContactNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDContactNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#WIContactNo1SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#WIContactNo2SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#ContactNoOfComplainantSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                    $("#CDMobileNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#CDMobileNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#MObilenoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                    $("#WITelephoneNoSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#WITelephoneNoSectionC2").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                    $("#TelephoneNoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
                });

        }

        //*****************************************************************************************************************************************************************************
        self.viewHours24andSection = function (data) {

            self.LoadFromhours24report625Data(data);
            $("#CDContactNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo1SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo2SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactNoOfComplainantSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#CDMobileNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MObilenoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#WITelephoneNoSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WITelephoneNoSectionC2").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


            //self.LoadFromhours24report625Data(data);
            self.viewMode("Form");
            self.viewModeForTabs('notification1');
            self.hour24andsection625Model().ViewModeForTabs('notification1');

            self.IsEnable(false);
            self.IsReset(false);

            self.isGoBackVisible(false);
            self.isSaveVisible(true);
            self.isSubmitVisible(false);
            self.isGoBackVisible(false);
            self.isSavebuttonvisable(true);


            $("#IODOccuranceDateTime").data('kendoDateTimePicker').enable(false);
            $("#IODOccuranceDateTime").data('kendoDateTimePicker').enable(false);
            $("#ChangeControlDateTime").data('kendoDateTimePicker').enable(false);
        }
       
        self.Edit24hoursAndSection = function (data)
        {
            self.LoadFromhours24report625Data(data);
            $("#CDContactNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo1SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo2SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactNoOfComplainantSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#CDMobileNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MObilenoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#WITelephoneNoSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WITelephoneNoSectionC2").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });


       
            self.viewMode("Form");
            self.viewModeForTabs('notification1');
            self.hour24andsection625Model().ViewModeForTabs('notification1');

            self.IsEnable(true);
            self.IsReset(true);

            self.isGoBackVisible(true);
            self.isSaveVisible(true);
            self.isSubmitVisible(false);
            self.isGoBackVisible(false);
            self.isSavebuttonvisable(true);


            $("#IODOccuranceDateTime").data('kendoDateTimePicker').enable(false);
            $("#IODOccuranceDateTime").data('kendoDateTimePicker').enable(false);
            $("#ChangeControlDateTime").data('kendoDateTimePicker').enable(false);
        }


        ///*******************************************************************************************************************************************************************************

        self.AddNewRowtotable = function (data) {
            self.hour24andsection625Model().Section625BUnion.push(new IPMSROOT.Section625BUnionDetails());
        }

        self.AddNewRowtoSection625CPrevent = function (data) {
            self.hour24andsection625Model().Section625CPrevent.push(new IPMSROOT.Section625CPreventDetails());
        }

        self.AddNewRowtoSection625CRecommended = function (data) {
            self.hour24andsection625Model().Section625CRecommended.push(new IPMSROOT.Section625CRecommendedDetails());
        }


        self.AddNewRowtoSection625G = function (data) {
            self.hour24andsection625Model().Section625GDetail2.push(new IPMSROOT.Section625GDetail2Details());
        }


        self.AddNewRowtoSection625EChild = function (data) {
            self.hour24andsection625Model().Section625EDetail.push(new IPMSROOT.Section625EChildDetails());
        }

        //******************************************************************************************************************************************************************************************        

        self.SaveHour24reports = function(data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var errors = 0;
            data.validationEnabled(true);
            if (data.Hour24Report625ID() == 0) {

                if ($("#txtCaptachCode").val() != "") {

                    if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                        $("#spanCaptachCode").text('');
                    } else {
                        $("#spanCaptachCode").text('Invalid security code');
                        var captachText = randString(6);
                        self.CaptachText(captachText);
                        errors = errors + 1;
                    }
                } else {
                    $("#spanCaptachCode").text(validationTextMessage);
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    errors = errors + 1;
                }
            }
        

        //**********************************************************SectionASevingStarted*******************************************************************************************************************************************
            if (data.NONatureCode() == '625A') {

                var cellnumber = $("#CDContactNumber1").val();
                cellnumber = cellnumber.replace(/(\)|\()|_|-+/g, '');

                var mobnumber = $("#CDMobileNumber1").val();
                mobnumber = mobnumber.replace(/(\)|\()|_|-+/g, '');

                if ($("#TOMSLogEntryNo").val() != "") {
                    $("#spanTOMSLogEntryNosectiona").text('');
                }
                else {
                    $("#spanTOMSLogEntryNosectiona").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#OperatorName1").val() != "") {
                    $("#spanOperatorName1sectiona").text('');
                }
                else {
                    $("#spanOperatorName1sectiona").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumber1").val() != "") {
                    $("#spanLincseNumber1sectiona").text('');
                }
                else {
                    $("#spanLincseNumber1sectiona").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CompanyRegistrationNumber").val() != "") {
                    $("#spanCompanyRegistrationNumbersectiona").text('');
                }
                else {
                    $("#spanCompanyRegistrationNumbersectiona").text(validationTextMessage);
                    errors = errors + 1;

                }
                if ($("#SiteTerminal").val() != "") {
                    $("#spanSiteTerminalsectiona").text('');
                }
                else {
                    $("#spanSiteTerminalsectiona").text(validationTextMessage);
                    errors = errors + 1;

                }

                if ($("#ChangeControlDateTime").val() != "") {
                    $("#spanChangeControlDateTimesectiona").text('');
                }
                else {
                    $("#spanChangeControlDateTimesectiona").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDName1").val() != "") {
                    $("#spansection625abcdCDName1").text('');
                }
                else {
                    $("#spansection625abcdCDName1").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignation1").val() != "") {
                    $("#spansection625abcdCDDesignation1").text('');
                }
                else {
                    $("#spansection625abcdCDDesignation1").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumber1").val() != "") {
                    $("#spansection625abcdCDContactNumber1").text('');
                }
                else {
                    $("#spansection625abcdCDContactNumber1").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumber1").val() != "") {
                    $("#spansection625abcdCDMobileNumber1").text('');
                }
                else {
                    $("#spansection625abcdCDMobileNumber1").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddress1").val() != "") {
                    $("#spansection625abcdCDAddress1").text('');
                }
                else {
                    $("#spansection625abcdCDAddress1").text(validationTextMessage);
                    errors = errors + 1;
                }


                if ($("#CDEmailID1").val() != "") {
                    
                    if (checkEmail($("#CDEmailID1").val())) {
                        
                        $("#spansection625abcdCDCDEmailID1").text('');
                    }
                   else {
                        $("#spansection625abcdCDCDEmailID1").text(validationTextMessage);
                        errors = errors + 1;
                    }
                }
                else {
                    $("#spansection625abcdCDCDEmailID1").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#ChangeControlLicensedOperator").val() != "") {
                    $("#spansection625abcdChangeControlLicensedOperator").text('');
                }
                else {
                    $("#spansection625abcdChangeControlLicensedOperator").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#AnticipatedImpactOnBBBEERating").val() != "") {
                    $("#spansection625abcdAnticipatedImpactOnBBBEERating").text('');
                }
                else {
                    $("#spansection625abcdAnticipatedImpactOnBBBEERating").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (cellnumber.length != 0) {
                    if (cellnumber.length != 13) {
                        $("#spansection625abcdCDContactNumber1").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber.length == 13) {
                        var validNo = parseInt(cellnumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spansection625abcdCDContactNumber1").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (mobnumber.length != 0) {
                    if (mobnumber.length != 13) {
                        $("#spansection625abcdCDMobileNumber1").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber.length == 13) {
                        var validNo = parseInt(mobnumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spansection625abcdCDMobileNumber1").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }


                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;
                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    //self.Hours24reportValidation().errors.showAllMessages();

                    //$('#divValidationError1').removeClass('display-none');
                    //$('#divValidationError2').removeClass('display-none');
                    toastr.warning(" Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");
                    return;
                }
            }
            //*******************************************************************SectionASevingCompleted****************************************************************************************************************************
            //*******************************************************************SectionBSevingStarted****************************************************************************************************************************

            if (data.NONatureCode() == '625B') {


                var cellnumber2 = $("#CDContactNumbersectionb").val();
                cellnumber2 = cellnumber2.replace(/(\)|\()|_|-+/g, '');

                var mobnumber2 = $("#CDMobileNumbersectionb").val();
                mobnumber2 = mobnumber2.replace(/(\)|\()|_|-+/g, '');

                if ($("#TOMSLogEntryNosectionb").val() != "") {
                    $("#spanTOMSLogEntryNosectionB").text('');
                }
                else {
                    $("#spanTOMSLogEntryNosectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OperatorNamesectionb").val() != "") {
                    $("#spanOperatorNamesectionB").text('');
                }
                else {
                    $("#spanOperatorNamesectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumbersectionb").val() != "") {
                    $("#spanLincseNumbersectionB").text('');
                }
                else {
                    $("#spanLincseNumbersectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SiteTerminalsectionb").val() != "") {
                    $("#spanSiteTerminalsectionB").text('');
                }
                else {
                    $("#spanSiteTerminalsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDNamesectionb").val() != "") {
                    $("#spanCDNamesectionB").text('');
                }
                else {
                    $("#spanCDNamesectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignationsectionb").val() != "") {
                    $("#spanCDDesignationsectionB").text('');
                }
                else {
                    $("#spanCDDesignationsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumbersectionb").val() != "") {
                    $("#spanCDContactNumbersectionB").text('');
                }
                else {
                    $("#spanCDContactNumbersectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumbersectionb").val() != "") {
                    $("#spanCDMobileNumbersectionB").text('');
                }
                else {
                    $("#spanCDMobileNumbersectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddresssectionb").val() != "") {
                    $("#spanCDAddresssectionB").text('');
                }
                else {
                    $("#spanCDAddresssectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDEmailIDsectionb").val() != "") {
                   
                    if (checkEmail($("#CDEmailIDsectionb").val())) {
                        
                        $("#spanCDEmailIDsectionb").text('');
                    }
                    else {
                        $("#spanCDEmailIDsectionb").text(validationTextMessage);
                        errors = errors + 1;
                    }
                }
                
                else {
                    $("#spanCDEmailIDsectionb").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDIndustrialDisputeDateTime").val() != "") {
                    $("#spanIDIndustrialDisputeDateTimesectionB").text('');
                }
                else {
                    $("#spanIDIndustrialDisputeDateTimesectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDTimeReported").val() != "") {
                    $("#spanIDTimeReportedsectionB").text('');
                }
                else {
                    $("#spanIDTimeReportedsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDDisputeSpecificLocationsectionb").val() != "") {
                    $("#spanIDDisputeSpecificLocationsectionB").text('');
                }
                else {
                    $("#spanIDDisputeSpecificLocationsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDTradeUnionNamesectionb").val() != "") {
                    $("#spanIDTradeUnionNamesectionB").text('');
                }
                else {
                    $("#spanIDTradeUnionNamesectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDTotalNoOfEmployeessectionb").val() != "") {
                    $("#spanIDTotalNoOfEmployeessectionB").text('');
                }
                else {
                    $("#spanIDTotalNoOfEmployeessectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IndustrialDisputeDescription").val() != "") {
                    $("#spanIndustrialDisputeDescriptionsectionB").text('');
                }
                else {
                    $("#spanIndustrialDisputeDescriptionsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().section625b().IDStrikeStatuS() == "" || self.hour24andsection625Model().section625b().IDStrikeStatuS() == 'undefined' || self.hour24andsection625Model().section625b().IDStrikeStatuS() == null) {
                    $("#spanIDStrikeStatuSsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                else { $("#spanIndustrialDisputeDescriptionsectionB").text(''); }
                if (self.hour24andsection625Model().section625b().IDImpactOperations() == "" || self.hour24andsection625Model().section625b().IDImpactOperations() == 'undefined' || self.hour24andsection625Model().section625b().IDImpactOperations() == null) {
                    $("#spanIDIDImpactOperationssectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                else { $("#spanIDIDImpactOperationssectionB").text(''); }

                if (self.hour24andsection625Model().section625b().IDViolencePresent() == "" || self.hour24andsection625Model().section625b().IDViolencePresent() == 'undefined' || self.hour24andsection625Model().section625b().IDViolencePresent() == null) {
                    $("#spanIDViolencePresentsectionB").text(validationTextMessage);
                    errors = errors + 1;
                }
                else { $("#spanIDViolencePresentsectionB").text(''); }
                //if (self.hour24andsection625Model().Section625BUnion().length == 0) {
                //    toastr.warning("Please Enter Union Details.", "24 Hour Report 62(5)");
                //    errors = errors + 1;
                //}

                if (self.hour24andsection625Model().Section625BUnion().length > 0) {
                    $.map(self.hour24andsection625Model().Section625BUnion, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.UnionName() == "" || CommodChk.TotalMembership() == "" || CommodChk.TotalRosteredForShift() == "" || CommodChk.TotalPresent() == "" || CommodChk.TotalStrike() == "" || CommodChk.TotalLeave() == "" || CommodChk.TotalSick() == "" || CommodChk.ReplacementLeave() == "") {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Union Details", "24 Hour Report 62(5)");
                                        errors = errors + 1;
                                    }
                                }
                            });

                    });
                }
                else {
                    toastr.warning("Please Enter Union Details.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }

                if (cellnumber2.length != 0) {
                    if (cellnumber2.length != 13) {
                        $("#spanCDContactNumbersectionB").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber2.length == 13) {
                        var validNo = parseInt(cellnumber2);
                        if (validNo == 0) {
                            $("#spanCDContactNumbersectionB").text("Invalid Contact Number");
                            toastr.warning("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }

                }
                if (mobnumber2.length != 0) {
                    if (mobnumber2.length != 13) {
                        $("#spanCDMobileNumbersectionB").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber2.length == 13) {
                        var validNo = parseInt(mobnumber2);
                        if (validNo == 0) {
                            $("#spanCDMobileNumbersectionB").text("Invalid Mobile Number");
                            toastr.warning("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }




                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;
                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    //self.section625bValidation().errors.showAllMessages();
                    //$('#divValidationError1').removeClass('display-none');
                    //$('#divValidationError3').removeClass('display-none');
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");
                    return;
                }
            }
            //*******************************************************************SectionBSevingCompleted****************************************************************************************************************************
            //*******************************************************************SectionCSevingStarted****************************************************************************************************************************

            if (data.NONatureCode() == '625C') {




                var cellnumber3 = $("#CDContactNumberSectionC").val();
                cellnumber3 = cellnumber3.replace(/(\)|\()|_|-+/g, '');

                var mobnumber3 = $("#CDMobileNumbersectionb").val();
                mobnumber3 = mobnumber3.replace(/(\)|\()|_|-+/g, '');

                var telenumber1 = $("#WITelephoneNoSectionC").val();
                telenumber1 = telenumber1.replace(/(\)|\()|_|-+/g, '');

                var telenumber2 = $("#WITelephoneNoSectionC2").val();
                telenumber2 = telenumber2.replace(/(\)|\()|_|-+/g, '');

                if ($("#TOMSLogEntryNosectionC").val() != "") {
                    $("#spanTOMSLogEntryNosectionC").text('');
                }
                else {
                    $("#spanTOMSLogEntryNosectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OperatorNamesectionc").val() != "") {
                    $("#spanOperatorNamesectionC").text('');
                }
                else {
                    $("#spanOperatorNamesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumbersectionc").val() != "") {
                    $("#spanLincseNumbersectionC").text('');
                }
                else {
                    $("#spanLincseNumbersectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SiteTerminalSectionc").val() != "") {
                    $("#spanSiteTerminalsectionC").text('');
                }
                else {
                    $("#spanSiteTerminalsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDIncidentDateTimesectionc").val() != "") {
                    $("#spanIDIncidentDateTimesectionC").text('');
                }
                else {
                    $("#spanIDIncidentDateTimesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDTimeReportedsectionc").val() != "") {
                    $("#spanIDTimeReportedsectionC").text('');
                }
                else {
                    $("#spanIDTimeReportedsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDIncidentSpecificLocation").val() != "") {
                    $("#spanIDIncidentSpecificLocationsectionC").text('');
                }
                else {
                    $("#spanIDIncidentSpecificLocationsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDNamesectionc").val() != "") {
                    $("#spanCDNamesectionC").text('');
                }
                else {
                    $("#spanCDNamesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignationSectionc").val() != "") {
                    $("#spanCDDesignationsectionC").text('');
                }
                else {
                    $("#spanCDDesignationsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumberSectionC").val() != "") {
                    $("#spanCDContactNumbersectionC").text('');
                }
                else {
                    $("#spanCDContactNumbersectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumberSectionC").val() != "") {
                    $("#spanCDMobileNumbersectionC").text('');
                }
                else {
                    $("#spanCDMobileNumbersectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddressSectionC").val() != "") {
                    $("#spanCDAddresssectionC").text('');
                }
                else {
                    $("#spanCDAddresssectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDEmailIDSectionC").val() != "") {


                    if (checkEmail($("#CDEmailIDSectionC").val())) {

                        $("#spanCDEmailIDsectionC").text('');
                    }
                    else {
                        $("#spanCDEmailIDsectionC").text(validationTextMessage);
                        errors = errors + 1;
                    }
                   
                }
                else {
                    $("#spanCDEmailIDsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIName").val() != "") {
                    $("#spanPINamesectionC").text('');
                }
                else {
                    $("#spanPINamesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PISurname").val() != "") {
                    $("#spanPISurnamesectionC").text('');
                }
                else {
                    $("#spanPISurnamesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIEmployeeNo").val() != "") {
                    $("#spanPIEmployeeNosectionC").text('');
                }
                else {
                    $("#spanPIEmployeeNosectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PINoOfDaysLost").val() != "") {
                    $("#spanPINoOfDaysLostsectionC").text('');
                }
                else {
                    $("#spanPINoOfDaysLostsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIGender").val() != "") {
                    $("#spanPIGendersectionC").text('');
                }
                else {
                    $("#spanPIGendersectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIAge").val() != "") {
                    $("#spanPIAgesectionC").text('');
                }
                else {
                    $("#spanPIAgesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIGradePosition").val() != "") {
                    $("#spanPIGradePositionsectionC").text('');
                }
                else {
                    $("#spanPIGradePositionsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#PIPartOfBody").val() != "") {
                    $("#spanPIPartOfBodysectionC").text('');
                }
                else {
                    $("#spanPIPartOfBodysectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#DescriptionofIncidentsectionc").val() != "") {
                    $("#spanIncidentDescriptionsectionC").text('');
                }
                else {
                    $("#spanIncidentDescriptionsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IIInvestigatorName").val() != "") {
                    $("#spanIIInvestigatorNamesectionC").text('');
                }
                else {
                    $("#spanIIInvestigatorNamesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IIDesignation").val() != "") {
                    $("#spanIIDesignationsectionC").text('');
                }
                else {
                    $("#spanIIDesignationsectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IIInvestigationDate").val() != "") {
                    $("#spanIIInvestigationDatesectionC").text('');
                }
                else {
                    $("#spanIIInvestigationDatesectionC").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().selectedgeneralagenciesdetails() == "" || self.hour24andsection625Model().selectedgeneralagenciesdetails() == 'undefined' || self.hour24andsection625Model().selectedgeneralagenciesdetails() == null) {
                    toastr.warning("Please Select General Agencies.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                else {
                    if (self.hour24andsection625Model().selectedgeneralagenciesdetails() == '6GEY') {
                        if ($("#GAOthersSpecify").val() != "") {
                            $("#spanGAOthersSpecifysectionC").text('');
                        }
                        else {
                            $("#spanGAOthersSpecifysectionC").text(validationTextMessage);
                            errors = errors + 1;
                        }
                    }
                }
                if (self.hour24andsection625Model().selectedOccupationalHygieneAgencies() == "" || self.hour24andsection625Model().selectedOccupationalHygieneAgencies() == 'undefined' || self.hour24andsection625Model().selectedOccupationalHygieneAgencies() == null) {
                    toastr.warning("Please Select Occupational Hygiene Agencies.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                else {
                    if (self.hour24andsection625Model().selectedOccupationalHygieneAgencies() == '6OCP') {
                        if ($("#GAOHAOthersSpecify").val() != "") {
                            $("#spanGAOHAOthersSpecifysectionC").text('');
                        }
                        else {
                            $("#spanGAOHAOthersSpecifysectionC").text(validationTextMessage);
                            errors = errors + 1;
                        }
                    }
                }

                if (self.hour24andsection625Model().selectedSubstandardCondition() == "" || self.hour24andsection625Model().selectedSubstandardCondition() == 'undefined' || self.hour24andsection625Model().selectedSubstandardCondition() == null) {
                    toastr.warning("Please Select Substandard Conditions.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                else {
                    if (self.hour24andsection625Model().selectedSubstandardCondition() == '6SCO') {
                        if ($("#IDCOthersSpecify").val() != "") {
                            $("#spanIDCOthersSpecifysectionC").text('');
                        }
                        else {
                            $("#spanIDCOthersSpecifysectionC").text(validationTextMessage);
                            errors = errors + 1;
                        }
                    }
                }


                if (self.hour24andsection625Model().selectedStandardAct() == "" || self.hour24andsection625Model().selectedStandardAct() == 'undefined' || self.hour24andsection625Model().selectedStandardAct() == null) {
                    toastr.warning("Please Select Standard Acts.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }

                if (self.hour24andsection625Model().selectedPersonalFactors() == "" || self.hour24andsection625Model().selectedPersonalFactors() == 'undefined' || self.hour24andsection625Model().selectedPersonalFactors() == null) {
                    toastr.warning("Please Select Personal Factors.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                if (self.hour24andsection625Model().selectedJobFactors() == "" || self.hour24andsection625Model().selectedJobFactors() == 'undefined' || self.hour24andsection625Model().selectedJobFactors() == null) {
                    toastr.warning("Please Select Job Factors.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                if (self.hour24andsection625Model().selectedTypeofContact().length == 0) {
                    toastr.warning("Please Select  Type of Contact.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                if (self.hour24andsection625Model().selectedControlStepsToPreventRecurrence().length == 0) {
                    toastr.warning("Please Select Control Steps To Prevent Recurrence.", "24 Hour Report 62(5)");
                    errors = errors + 1;

                }
                if (self.hour24andsection625Model().Section625CRecommended().length > 0) {
                    $.map(self.hour24andsection625Model().Section625CRecommended, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.RecommendedStep() == "" || CommodChk.TargetDateTime() == "" || CommodChk.ActionBy() == "" || CommodChk.CompletedDate() == "" ) {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Recommended Steps by Investigator to Prevent Recurrence.", "24 Hour Report 62(5)");
                                        errors = errors + 1;
                                    }
                                }
                            });

                    });
                }
                else {
                    toastr.warning("Please Enter Recommended Steps by Investigator to Rrevent Recurrence.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }
                //if (self.hour24andsection625Model().Section625CPrevent().length == 0) {
                //    toastr.warning("Please enter additional steps to be taken to prevent a recurrence.", "24 Hour Report 62(5)");
                //    errors = errors + 1;

                //}
                if (self.hour24andsection625Model().Section625CPrevent().length > 0) {
                    var ManError = "Y";
                    $.map(self.hour24andsection625Model().Section625CPrevent, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.PreventStep() == "" || CommodChk.TargetDateTime() == "" || CommodChk.ActionBy() == "" || CommodChk.CompletedDate() == "" ) {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Additional Steps To Be Taken To Prevent A Recurrence.", "24 Hour Report 62(5)");
                                        errors = errors + 1;
                                    }
                                }
                            });

                    });
                }
                else {
                    toastr.warning("Please Enter Additional Steps To Be Taken To Prevent A Recurrence.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }

                if ($("#CurrentControlssectionc").val() != "") {
                    $("#spanCurrentControlssectionC").text('');
                }
                else {
                    $("#spanCurrentControlssectionC").text(validationTextMessage);
                    errors = errors + 1;
                }

                if (cellnumber3.length != 0) {
                    if (cellnumber3.length != 13) {
                        $("#spanCDContactNumbersectionC").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber3.length == 13) {
                        var validNo = parseInt(cellnumber3);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spanCDContactNumbersectionC").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (mobnumber3.length != 0) {
                    if (mobnumber3.length != 13) {
                        $("#spanCDMobileNumbersectionC").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber3.length == 13) {
                        var validNo = parseInt(mobnumber3);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spanCDMobileNumbersectionC").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }

                if (telenumber1.length != 0) {
                    if (telenumber1.length != 13) {
                        $("#spanWITelephoneNosectionC").text("Invalid Telephone Number");
                        errors = errors + 1;
                    }
                    else if (telenumber1.length == 13) {
                        var validNo = parseInt(telenumber1);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            $("#spanWITelephoneNosectionC").text("Invalid Telephone Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (telenumber2.length != 0) {
                    if (telenumber2.length != 13) {
                        $("#spanWITelephoneNo2sectionC").text("Invalid Telephone Number");
                        errors = errors + 1;
                    }
                    else if (telenumber2.length == 13) {
                        var validNo = parseInt(telenumber2);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            $("#spanWITelephoneNo2sectionC").text("Invalid Telephone Number");
                            errors = errors + 1;
                        }
                    }
                }



                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;

                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    //self.Hours24reportValidation().errors.showAllMessages();

                    //$('#divValidationError1').removeClass('display-none');
                    //$('#divValidationError4').removeClass('display-none');
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");
                    return;
                }
            }
            //*******************************************************************SectionCSevingCompleted****************************************************************************************************************************
            //*******************************************************************SectionDSevingStarted****************************************************************************************************************************

            if (data.NONatureCode() == '625D') {


                var cellnumber4 = $("#CDContactNumberSectionD").val();
                cellnumber4 = cellnumber4.replace(/(\)|\()|_|-+/g, '');

                var mobnumber4 = $("#CDMobileNumberSectionD").val();
                mobnumber4 = mobnumber4.replace(/(\)|\()|_|-+/g, '');

                if ($("#TOMSLogEntryNosectionD").val() != "") {
                    $("#spanTOMSLogEntryNosectionD").text('');
                }
                else {
                    $("#spanTOMSLogEntryNosectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OperatorNamesectionD").val() != "") {
                    $("#spanOperatorNamesectionD").text('');
                }
                else {
                    $("#spanOperatorNamesectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumbersectionD").val() != "") {
                    $("#spanLincseNumbersectionD").text('');
                }
                else {
                    $("#spanLincseNumbersectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SiteTerminalSectionD").val() != "") {
                    $("#spanSiteTerminalsectionD").text('');
                }
                else {
                    $("#spanSiteTerminalsectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IncidentDateTimesectionD").val() != "") {
                    $("#spanIncidentDateTimesectionD").text('');
                }
                else {
                    $("#spanIncidentDateTimesectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TimeReportedSectionD").val() != "") {
                    $("#spanTimeReportedectionD").text('');
                }
                else {
                    $("#spanTimeReportedectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SpecifyLocationOfFireSectionD").val() != "") {
                    $("#spanSpecifyLocationOfFireSectionD").text('');
                }
                else {
                    $("#spanSpecifyLocationOfFireSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }

                if (self.hour24andsection625Model().section625D().FireDepartmentAttend() == "") {
                    $("#spanFireDepartmentAttendSetionD").text(validationTextMessage);
                    toastr.warning("Please Select Fire Department Attend.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }

                if (self.hour24andsection625Model().section625D().FireDepartmentAttend() == "Y")
                {

                    if (self.hour24andsection625Model().selectedFireDepartment() == "" || self.hour24andsection625Model().selectedFireDepartment() == 'undefined' || self.hour24andsection625Model().selectedFireDepartment() == null) {
                        toastr.warning("Please Select The Name of The Fire Department.", "24 Hour Report 62(5)");
                        errors = errors + 1;
                    }
                    else {
                        if (self.hour24andsection625Model().selectedFireDepartment() == '6FDC') {
                            if ($("#OthersSpecifySectionD").val() != "") {
                                $("#spanOthersSpecifySectionD").text('');
                            }
                            else {
                                $("#spanOthersSpecifySectionD").text(validationTextMessage);
                                errors = errors + 1;
                            }
                        }
                    }
                }

                if ($("#CDNameSectionD").val() != "") {
                    $("#spanCDNameSectionD").text('');
                }
                else {
                    $("#spanCDNameSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignationSectionD").val() != "") {
                    $("#spanCDDesignationSectionD").text('');
                }
                else {
                    $("#spanCDDesignationSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumberSectionD").val() != "") {
                    $("#spanCDContactNumberSectionD").text('');
                }
                else {
                    $("#spanCDContactNumberSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumberSectionD").val() != "") {
                    $("#spanCDMobileNumberSectionD").text('');
                }
                else {
                    $("#spanCDMobileNumberSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddressSectionD").val() != "") {
                    $("#spanCDAddressSectionD").text('');
                }
                else {
                    $("#spanCDAddressSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDEmailIDSectionD").val() != "") {

                    if (checkEmail($("#CDEmailIDSectionD").val())) {

                        $("#spanCDEmailIDSectionD").text('');
                    }
                    else {
                        $("#spanCDEmailIDSectionD").text(validationTextMessage);
                        errors = errors + 1;
                    }
          
                }
                else {
                    $("#spanCDEmailIDSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().section625D().FICommercial() == "" || self.hour24andsection625Model().section625D().FICommercial() == 'undefined' || self.hour24andsection625Model().section625D().FICommercial() == null) {
                    $("#spanAttend1SectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                else {
                    $("#spanAttend1SectionD").text('');
                }
                if (self.hour24andsection625Model().section625D().FIStorage() == "" || self.hour24andsection625Model().section625D().FIStorage() == 'undefined' || self.hour24andsection625Model().section625D().FIStorage() == null) {
                    $("#spanAttend2SectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                else {
                    $("#spanAttend2SectionD").text('');
                }
                if (self.hour24andsection625Model().section625D().FIIndustry() == "" || self.hour24andsection625Model().section625D().FIIndustry() == 'undefined' || self.hour24andsection625Model().section625D().FIIndustry() == null) {
                    $("#spanAttend3SectionD").text(validationTextMessage);
                    errors = errors + 1;

                }
                else {
                    $("#spanAttend3SectionD").text('');

                }
                if (self.hour24andsection625Model().section625D().FITransport() == "" || self.hour24andsection625Model().section625D().FITransport() == 'undefined' || self.hour24andsection625Model().section625D().FITransport() == null) {
                    $("#spanAttend4SectionD").text(validationTextMessage);
                    errors = errors + 1;

                }
                else {
                    $("#spanAttend4SectionD").text('');

                }
                if (self.hour24andsection625Model().section625D().FIOthers() == "" || self.hour24andsection625Model().section625D().FIOthers() == 'undefined' || self.hour24andsection625Model().section625D().FIOthers() == null) {
                    $("#spanAttend5SectionD").text(validationTextMessage);
                    errors = errors + 1;

                }
                else {
                    $("#spanAttend5SectionD").text('');
                    if (self.hour24andsection625Model().section625D().FIOthers() == 'M') {
                        if ($("#FIMiscillaniousSpecifySectionD").val() != "") {
                            $("#spanFIMiscillaniousSpecifySectionD").text('');
                        }
                        else {
                            $("#spanFIMiscillaniousSpecifySectionD").text(validationTextMessage);
                            errors = errors + 1;
                        }
                    }
                }
                if (self.hour24andsection625Model().selectedIncidentClassification().length == 0) {
                    toastr.warning("Please Select Incident Classification.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }
                else {
                    if (self.hour24andsection625Model().selectedIncidentClassification() == '6ICI')
                        if ($("#ICOthersSpecify").val() != "") {
                            $("#spanICOthersSpecifySectionD").text('');
                        }
                        else {
                            $("#spanICOthersSpecifySectionD").text(validationTextMessage);
                            errors = errors + 1;
                        }
                }

                if (self.hour24andsection625Model().selectedDiscriptionofExposedRisk().length == 0) {
                    toastr.warning("Please Select Description Of Exposed Risk.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }
                else {
                    if (self.hour24andsection625Model().selectedDiscriptionofExposedRisk() == '6ERJ')
                        if ($("#DEROthersSpecifySectionD").val() != "") {
                            $("#spanDEROthersSpecifySectionD").text('');
                        }
                        else {
                            $("#spanDEROthersSpecifySectionD").text(validationTextMessage);
                            errors = errors + 1;
                        }
                }

                if ($("#APDDamageSectionD").val() != "") {
                    $("#spanAPDDamageSectionD").text('');
                }
                else {
                    $("#spanAPDDamageSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#APDMaximumEstimatedFinancialLossSectionD").val() != "") {
                    $("#spanAPDMaximumEstimatedFinancialLossSectionD").text('');
                }
                else {
                    $("#spanAPDMaximumEstimatedFinancialLossSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#APDActualLossSectionD").val() != "") {
                    $("#spanAPDActualLossSectionD").text('');
                }
                else {
                    $("#spanAPDActualLossSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#MEByWhomSectionD").val() != "") {
                    $("#spanMEByWhomSectionD").text('');
                }
                else {
                    $("#spanMEByWhomSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#MEWithWhatBeforeFireSecionD").val() != "") {
                    $("#spanMEWithWhatBeforeFireSecionD").text('');
                }
                else {
                    $("#spanMEWithWhatBeforeFireSecionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#MEWithWhatAfterFireSectionD").val() != "") {
                    $("#spanMEWithWhatAfterFireSectionD").text('');
                }
                else {
                    $("#spanMEWithWhatAfterFireSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WCWeatherSectionD").val() != "") {
                    $("#spanWCWeatherSectionD").text('');
                }
                else {
                    $("#spanWCWeatherSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WCWindDirectionSectionD").val() != "") {
                    $("#spanWCWindDirectionSectionD").text('');
                }
                else {
                    $("#spanWCWindDirectionSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WCWindSpeedSectionD").val() != "") {
                    $("#spanWCWindSpeedSectionD").text('');
                }
                else {
                    $("#spanWCWindSpeedSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WCTemperatureSectionD").val() != "") {
                    $("#spanWCTemperatureSectionD").text('');
                }
                else {
                    $("#spanWCTemperatureSectionD").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (cellnumber4.length != 0) {
                    if (cellnumber4.length != 13) {
                        $("#spanCDContactNumberSectionD").text("Invalid Telephone Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber4.length == 13) {
                        var validNo = parseInt(cellnumber4);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            $("#spanCDContactNumberSectionD").text("Invalid Telephone Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (mobnumber4.length != 0) {
                    if (mobnumber4.length != 13) {
                        $("#spanCDMobileNumberSectionD").text("Invalid Mobile Number");
                        toastr.warning("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber4.length == 13) {
                        var validNo = parseInt(mobnumber4);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spanCDMobileNumberSectionD").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }

                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;
                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {
                    self.Hours24reportValidation().errors.showAllMessages();
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");
                    return;
                }
            }
            //*******************************************************************SectionDSevingCompleted****************************************************************************************************************************
            //*******************************************************************SectionESevingStarted****************************************************************************************************************************

            if (data.NONatureCode() == '625E') {

                var cellnumber5 = $("#CDContactNumberSectionE").val();
                cellnumber5 = cellnumber5.replace(/(\)|\()|_|-+/g, '');

                var mobnumber5 = $("#CDMobileNumberSectionE").val();
                mobnumber5 = mobnumber5.replace(/(\)|\()|_|-+/g, '');


                var telenumber5 = $("#TelephoneNoSectionE").val();
                telenumber5 = telenumber5.replace(/(\)|\()|_|-+/g, '');

                var telmobnumber1 = $("#MObilenoSectionE").val();
                telmobnumber1 = telmobnumber1.replace(/(\)|\()|_|-+/g, '');



                if ($("#TOMSLogEntryNosectionE").val() != "") {
                    $("#spanTOMSLogEntryNosectionE").text('');
                }
                else {
                    $("#spanTOMSLogEntryNosectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OperatorNameSectionE").val() != "") {
                    $("#spanOperatorNameSectionE").text('');
                }
                else {
                    $("#spanOperatorNameSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumberSectionE").val() != "") {
                    $("#spanLincseNumberSectionE").text('');
                }
                else {
                    $("#spanLincseNumberSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SiteTerminalSectionE").val() != "") {
                    $("#spanSiteTerminalSectionE").text('');
                }
                else {
                    $("#spanSiteTerminalSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#DateIncidentSectionE").val() != "") {
                    $("#spanDateIncidentSectionE").text('');
                }
                else {
                    $("#spanDateIncidentSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TimeReportedSectionE").val() != "") {
                    $("#spanTimeReportedSectionE").text('');
                }
                else {
                    $("#spanTimeReportedSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDNameSectionE").val() != "") {
                    $("#spanCDNameSectionE").text('');
                }
                else {
                    $("#spanCDNameSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignationSectionE").val() != "") {
                    $("#spanCDDesignationSectionE").text('');
                }
                else {
                    $("#spanCDDesignationSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumberSectionE").val() != "") {
                    $("#spanCDContactNumberSectionE").text('');
                }
                else {
                    $("#spanCDContactNumberSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumberSectionE").val() != "") {
                    $("#spanCDMobileNumberSectionE").text('');
                }
                else {
                    $("#spanCDMobileNumberSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddresssectionE").val() != "") {
                    $("#spanCDAddresssectionE").text('');
                }
                else {
                    $("#spanCDAddresssectionE").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#CDEmailIDSectionE").val() != "") {

                    if (checkEmail($("#CDEmailIDSectionE").val())) {

                        $("#spanCDEmailIDSectionE").text('');
                    }
                    else {
                        $("#spanCDEmailIDSectionE").text(validationTextMessage);
                        errors = errors + 1;
                    }
               
                }
                else {
                    $("#spanCDEmailIDSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#EmailIDSectionE").val() != "") {

                    if (checkEmail($("#EmailIDSectionE").val())) {

                        $("#spanEmailIDSectionE").text('');
                    }
                    else {
                        $("#spanEmailIDSectionE").text(validationTextMessage);
                        errors = errors + 1;
                    }
                    
                }
                else {
                    $("#spanEmailIDSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#OwnerNameofStolenItemSectionE").val() != "") {
                    $("#spanOwnerNameofStolenItemSectionE").text('');
                }
                else {
                    $("#spanOwnerNameofStolenItemSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OwnerAddressSectionE").val() != "") {
                    $("#spanOwnerAddressSectionE").text('');
                }
                else {
                    $("#spanOwnerAddressSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TelephoneNoSectionE").val() != "") {
                    $("#spanTelephoneNoSectionE").text('');
                }
                else {
                    $("#spanTelephoneNoSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#MObilenoSectionE").val() != "") {
                    $("#spanMObilenoSectionE").text('');
                }
                else {
                    $("#spanMObilenoSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TargetDateSectionE").val() != "") {
                    $("#spanTargetDateSectionE").text('');
                }
                else {
                    $("#spanTargetDateSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDWhenandWhereStolenLocationSectionE").val() != "") {
                    $("#spanIDWhenandWhereStolenLocationSectionE").text('');
                }
                else {
                    $("#spanIDWhenandWhereStolenLocationSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TargetDate2SectionE").val() != "") {
                    $("#spanTargetDate2SectionE").text('');
                }
                else {
                    $("#spanTargetDate2SectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IDWhenWasDiscoveredLocationSectionE").val() != "") {
                    $("#spanIDWhenWasDiscoveredLocationSectionE").text('');
                }
                else {
                    $("#spanIDWhenWasDiscoveredLocationSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TheftOccurSectionE").val() != "") {
                    $("#spanTheftOccurSectionE").text('');
                }
                else {
                    $("#spanTheftOccurSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#ProtectTheftSectionE").val() != "") {
                    $("#spanProtectTheftSectionE").text('');
                }
                else {
                    $("#spanProtectTheftSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CircumstancesSectionE").val() != "") {
                    $("#spanCircumstancesSectionE").text('');
                }
                else {
                    $("#spanCircumstancesSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TheftAvoidedSectionE").val() != "") {
                    $("#spanTheftAvoidedSectionE").text('');
                }
                else {
                    $("#spanTheftAvoidedSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().section625E().PoliceAdviced()=='Y')
                {
                    if ($("#SAPSOBNumberSectionE").val() != "") {
                        $("#spanSAPSOBNumberSectionE").text('');
                    }
                    else {
                        $("#spanSAPSOBNumberSectionE").text(validationTextMessage);
                        errors = errors + 1;
                    }
                    if ($("#PoliceStationReportedToSEctionE").val() != "") {
                        $("#spanPoliceStationReportedToSEctionE").text('');
                    }
                    else {
                        $("#spanPoliceStationReportedToSEctionE").text(validationTextMessage);
                        errors = errors + 1;
                    }
                }

                //if (self.hour24andsection625Model().Section625EDetail().length == 0) {
                //    toastr.warning("Please enter details of items lost/stolen.", "Hour 24 Section 625");
                //    errors = errors + 1;
                //}
                if (self.hour24andsection625Model().Section625EDetail().length > 0) {
                    var ManError = "Y";
                    $.map(self.hour24andsection625Model().Section625EDetail, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.Item() == "" || CommodChk.Quantity() == "" || CommodChk.ReplacementValue() == "" ) {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Details of Items Lost/Stolen.", "24 Hour Report 62(5)");
                                        errors = errors + 1;
                                    }
                                }
                            });

                    });
                }
                else {
                    toastr.warning("Please Enter Details of Items Lost/Stolen.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().section625E().PoliceAdviced() == "" || self.hour24andsection625Model().section625E().PoliceAdviced() == 'undefined' || self.hour24andsection625Model().section625E().PoliceAdviced() == null) {
                    toastr.warning("Please Enter Police Advice.", "24 Hour Report 62(5)");
                    $("#spanPoliceAdvicedSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().section625E().StolenFromBuilding() == "" || self.hour24andsection625Model().section625E().StolenFromBuilding() == 'undefined' || self.hour24andsection625Model().section625E().StolenFromBuilding() == null) {
                    toastr.warning("Please Select Were There Signs of Forced Entry (If Stolen From a Building).", "24 Hour Report 62(5)");
                    $("#spanStolenFromBuildingSectionE").text(validationTextMessage);
                    errors = errors + 1;
                }

                if (self.hour24andsection625Model().section625E().ISPSBreach() == "" || self.hour24andsection625Model().section625E().ISPSBreach() == 'undefined' || self.hour24andsection625Model().section625E().ISPSBreach() == null) {
                    toastr.warning("Please Select ISPS Breach.", "24 Hour Report 62(5)");
                    $("#spanISPSBreachSEctionE").text(validationTextMessage);
                    errors = errors + 1;//ISPSBreach
                }


                if (cellnumber5.length != 0) {
                    if (cellnumber5.length != 13) {
                        $("#spanCDContactNumberSectionE").text("Invalid Telephone Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber5.length == 13) {
                        var validNo = parseInt(cellnumber5);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            $("#spanCDContactNumberSectionE").text("Invalid Telephone Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (mobnumber5.length != 0) {
                    if (mobnumber5.length != 13) {
                        $("#spanCDMobileNumberSectionE").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber5.length == 13) {
                        var validNo = parseInt(mobnumber5);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spanCDMobileNumberSectionE").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }


                if (telenumber5.length != 0) {
                    if (telenumber5.length != 13) {
                        $("#spanTelephoneNoSectionE").text("Invalid Telephone Number");
                        errors = errors + 1;
                    }
                    else if (telenumber5.length == 13) {
                        var validNo = parseInt(telenumber5);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            $("#spanTelephoneNoSectionE").text("Invalid Telephone Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (telmobnumber1.length != 0) {
                    if (telmobnumber1.length != 13) {
                        $("#spanMObilenoSectionE").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (telmobnumber1.length == 13) {
                        var validNo = parseInt(telmobnumber1);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spanMObilenoSectionE").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }




                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;

                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");

                    return;
                }
            }
            //*******************************************************************SectionESevingCompleted****************************************************************************************************************************
            if (data.NONatureCode() == '625F') {
                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;

                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");

                    return;
                }
            }

            //*******************************************************************SectionGSevingStarted****************************************************************************************************************************

            if (data.NONatureCode() == '625G') {


                var cellnumber6 = $("#CDContactNumberSectionG").val();
                cellnumber6 = cellnumber6.replace(/(\)|\()|_|-+/g, '');

                var mobnumber6 = $("#CDMobileNumberSectionG").val();
                mobnumber6 = mobnumber6.replace(/(\)|\()|_|-+/g, '');


                var contactnum1 = $("#WIContactNo1SectionG").val();
                contactnum1 = contactnum1.replace(/(\)|\()|_|-+/g, '');

                var contactnum2 = $("#WIContactNo2SectionG").val();
                contactnum2 = contactnum2.replace(/(\)|\()|_|-+/g, '');


                var contactcomp = $("#ContactNoOfComplainantSectionG").val();
                contactcomp = contactcomp.replace(/(\)|\()|_|-+/g, '');


                if ($("#TOMSLogEntryNoSectionG").val() != "") {
                    $("#spanTOMSLogEntryNoSectionG").text('');
                }
                else {
                    $("#spanTOMSLogEntryNoSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#OperatorNameSectionG").val() != "") {
                    $("#spanOperatorNameSectionG").text('');
                }
                else {
                    $("#spanOperatorNameSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LincseNumberSectionG").val() != "") {
                    $("#spanLincseNumberSectionG").text('');
                }
                else {
                    $("#spanLincseNumberSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#SiteTerminalSectionG").val() != "") {
                    $("#spanSiteTerminalSectionG").text('');
                }
                else {
                    $("#spanSiteTerminalSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#DateIncident9SectionG").val() != "") {
                    $("#spanDateIncident9SectionG").text('');
                }
                else {
                    $("#spanDateIncident9SectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#TimeReportedSectonG").val() != "") {
                    $("#spanTimeReportedSectonG").text('');
                }
                else {
                    $("#spanTimeReportedSectonG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDNameSectionG").val() != "") {
                    $("#spanCDNameSectionG").text('');
                }
                else {
                    $("#spanCDNameSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDDesignationSectionG").val() != "") {
                    $("#spanCDDesignationSectionG").text('');
                }
                else {
                    $("#spanCDDesignationSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDContactNumberSectionG").val() != "") {
                    $("#spanCDContactNumberSectionG").text('');
                }
                else {
                    $("#spanCDContactNumberSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDMobileNumberSectionG").val() != "") {
                    $("#spanCDMobileNumberSectionG").text('');
                }
                else {
                    $("#spanCDMobileNumberSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#CDAddressSectionG").val() != "") {
                    $("#spanCDAddressSectionG").text('');
                }
                else {
                    $("#spanCDAddressSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }

                if ($("#CDEmailIDSectionG").val() != "") {

                    if (checkEmail($("#CDEmailIDSectionG").val())) {

                        $("#spanCDEmailIDSectionG").text('');
                    }
                    else {
                        $("#spanCDEmailIDSectionG").text(validationTextMessage);
                        errors = errors + 1;
                    }
                    
                }
                else {
                    $("#spanCDEmailIDSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WIWitnessName1SectionG").val() != "") {
                    $("#spanWIWitnessName1SectionG").text('');
                }
                else {
                    $("#spanWIWitnessName1SectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WIContactNo1SectionG").val() != "") {
                    $("#spanWIContactNo1SectionG").text('');
                }
                else {
                    $("#spanWIContactNo1SectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WIWitnessName2SectionG").val() != "") {
                    $("#spanWIWitnessName2SectionG").text('');
                }
                else {
                    $("#spanWIWitnessName2SectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#WIContactNo2SectionG").val() != "") {
                    $("#spanWIContactNo2SectionG").text('');
                }
                else {
                    $("#spanWIContactNo2SectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IncidentDescriptionSectionG").val() != "") {
                    $("#spanIncidentDescriptionSectionG").text('');
                }
                else {
                    $("#spanIncidentDescriptionSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IncidentExtentSectionG").val() != "") {
                    $("#spanIncidentExtentSectionG").text('');
                }
                else {
                    $("#spanIncidentExtentSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#QuantityVolumeMaterialSectionG").val() != "") {
                    $("#spanQuantityVolumeMaterialSectionG").text('');
                }
                else {
                    $("#spanQuantityVolumeMaterialSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#EstimateDistanceNearestWaterwaySectionG").val() != "") {
                    $("#spanEstimateDistanceNearestWaterwaySectionG").text('');
                }
                else {
                    $("#spanEstimateDistanceNearestWaterwaySectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#ActivityTypeIncidentSectionG").val() != "") {
                    $("#spanActivityTypeIncidentSectionG").text('');
                }
                else {
                    $("#spanActivityTypeIncidentSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#IncidentIdentifiedSectionG").val() != "") {
                    $("#spanIncidentIdentifiedSectionG").text('');
                }
                else {
                    $("#spanIncidentIdentifiedSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#ImmediateReleventActionsTakenSectionG").val() != "") {
                    $("#spanImmediateReleventActionsTakenSectionG").text('');
                }
                else {
                    $("#spanImmediateReleventActionsTakenSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#EnvironmentalImpactDescriptionSectionG").val() != "") {
                    $("#spanEnvironmentalImpactDescriptionSectionG").text('');
                }
                else {
                    $("#spanEnvironmentalImpactDescriptionSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#ContributingFactorsCoursesSectionG").val() != "") {
                    $("#spanContributingFactorsCoursesSectionG").text('');
                }
                else {
                    $("#spanContributingFactorsCoursesSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                if ($("#LikelyUnderlyingCausesSectionG").val() != "") {
                    $("#spanLikelyUnderlyingCausesSectionG").text('');
                }
                else {
                    $("#spanLikelyUnderlyingCausesSectionG").text(validationTextMessage);
                    errors = errors + 1;
                }
                //if (self.hour24andsection625Model().Section625GDetail2().length == 0) {
                //    toastr.warning("Please enter corrective & preventive actions.", "Hour 24 Section 625");
                //    errors = errors + 1;
                //}
                if (self.hour24andsection625Model().section625G().LIMinorEnvironmentalIncident() == "") {
                    if (self.hour24andsection625Model().section625G().LISignificantEnvironmentalIncident() == "") {
                        if (self.hour24andsection625Model().section625G().LIMajorEnvironmentalIncident() == "") {
                            toastr.warning("Please Enter Level of Incident.", "24 Hour Report 62(5)");
                            $("#spanLIMinorEnvironmentalIncidentSectionG").text(validationTextMessage);
                            $("#spanLISignificantEnvironmentalIncidentSectionG").text(validationTextMessage);
                            $("#spanLIMajorEnvironmentalIncidentSectionG").text(validationTextMessage);
                            errors = errors + 1;//ISPSBreach
                        }
                    }
                }


                if (cellnumber6.length != 0) {
                    if (cellnumber6.length != 13) {
                        $("#spanCDContactNumberSectionG").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (cellnumber6.length == 13) {
                        var validNo = parseInt(cellnumber6);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spanCDContactNumberSectionG").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (mobnumber6.length != 0) {
                    if (mobnumber6.length != 13) {
                        $("#spanCDMobileNumberSectionG").text("Invalid Mobile Number");
                        errors = errors + 1;
                    }
                    else if (mobnumber6.length == 13) {
                        var validNo = parseInt(mobnumber6);
                        if (validNo == 0) {
                            toastr.warning("Invalid Mobile Number");
                            $("#spanCDMobileNumberSectionG").text("Invalid Mobile Number");
                            errors = errors + 1;
                        }
                    }
                }


                if (contactnum1.length != 0) {
                    if (contactnum1.length != 13) {
                        $("#spanWIContactNo1SectionG").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (contactnum1.length == 13) {
                        var validNo = parseInt(contactnum1);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spanWIContactNo1SectionG").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }
                if (contactnum2.length != 0) {
                    if (contactnum2.length != 13) {
                        $("#spanWIContactNo2SectionG").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (contactnum2.length == 13) {
                        var validNo = parseInt(contactnum2);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spanWIContactNo2SectionG").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }

                if (contactcomp.length != 0) {
                    if (contactcomp.length != 13) {
                        $("#spanContactNoOfComplainantSectionG").text("Invalid Contact Number");
                        errors = errors + 1;
                    }
                    else if (contactcomp.length == 13) {
                        var validNo = parseInt(contactcomp);
                        if (validNo == 0) {
                            toastr.warning("Invalid Contact Number");
                            $("#spanContactNoOfComplainantSectionG").text("Invalid Contact Number");
                            errors = errors + 1;
                        }
                    }
                }

                if (self.hour24andsection625Model().selectedRecordingofIncident() == "" || self.hour24andsection625Model().selectedRecordingofIncident() == 'undefined' || self.hour24andsection625Model().selectedRecordingofIncident() == null) {
                    toastr.warning("Please Select Recording of Incident.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }
                if (self.hour24andsection625Model().Section625GDetail2().length > 0) {
                    var ManError = "Y";
                    $.map(self.hour24andsection625Model().Section625GDetail2, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.Description() == "" || CommodChk.ResponsiblePerson() == "" || CommodChk.TargetCompletion() == "" || CommodChk.DateCompletion() == "") {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Corrective & Preventive Actions.", "24 Hour Report 62(5)");
                                        errors = errors + 1;
                                    }
                                }
                            });

                    });
                }
                else {
                    toastr.warning("Please Enter Corrective & Preventive Actions.", "24 Hour Report 62(5)");
                    errors = errors + 1;
                }

                self.Hours24reportValidation = ko.observable(data);
                self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
                errors = errors + self.Hours24reportValidation().errors().length;

                if (errors == 0) {
                    if (data.Hour24Report625ID() == 0) {

                        self.viewModelHelper.apiPost('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Saved Successfully.", "24 Hour Report 62(5)");
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 4000);
                        });
                    }
                    else {
                        self.viewModelHelper.apiPut('api/Hour24Report625', ko.mapping.toJSON(self.hour24andsection625Model()), function Message(data) {
                            toastr.success("24 Hour Report 62(5)  Details Updated Successfully.", "24 Hour Report 62(5)");
                            self.viewMode('List');
                            var index = 0;
                            self.isSavebuttonvisable(false);
                            HandleProgressBarAndActiveTab(index);
                            self.Loadhour24report625list();
                        });
                    }
                }
                else {

                    self.Hours24reportValidation().errors.showAllMessages();
                    //self.Hours24reportValidation().errors.showAllMessages();

                    //$('#divValidationError1').removeClass('display-none');
                    //$('#divValidationError7').removeClass('display-none');
                    toastr.warning("Please Fill All Mandatory Details in 24 Hour Report 62(5) Form.", "24 Hour Report 62(5)");
                    return;
                }
            }

            //*******************************************************************SectionGSevingCompleted****************************************************************************************************************************
        }

        //*******************************************************************Validationfunction****************************************************************************************************************************
        //sectionA
        self.validationTOMSLogEntryNoSectionA = function () {

            if ($("#TOMSLogEntryNo").val() != "") {
                $("#spanTOMSLogEntryNosectiona").text('');
            }
            else {
                $("#spanTOMSLogEntryNosectiona").text(validationTextMessage);

            }
        }
        self.validationOperatorName1sectionA = function () {

            if ($("#OperatorName1").val() != "") {
                $("#spanOperatorName1sectiona").text('');
            }
            else {
                $("#spanOperatorName1sectiona").text(validationTextMessage);

            }
        }
        self.validationLincseNumber1sectionA = function () {

            if ($("#LincseNumber1").val() != "") {
                $("#spanLincseNumber1sectiona").text('');
            }
            else {
                $("#spanLincseNumber1sectiona").text(validationTextMessage);

            }
        }
        self.validationCompanyRegistrationNumbersectionA = function () {
            if ($("#CompanyRegistrationNumber").val() != "") {
                $("#spanCompanyRegistrationNumbersectiona").text('');
            }
            else {
                $("#spanCompanyRegistrationNumbersectiona").text(validationTextMessage);
            }
        }
        self.validationSiteTerminalsectionA = function () {
            if ($("#SiteTerminal").val() != "") {
                $("#spanSiteTerminalsectiona").text('');
            }
            else {
                $("#spanSiteTerminalsectiona").text(validationTextMessage);
            }
        }
        self.validationChangeControlDateTimesectionA = function () {
            if ($("#ChangeControlDateTime").val() != "") {
                $("#spanChangeControlDateTimesectiona").text('');
            }
            else {
                $("#spanChangeControlDateTimesectiona").text("This field is required");
            }
        }
        self.validationCDName1sectionA = function () {
            if ($("#CDName1").val() != "") {
                $("#spansection625abcdCDName1").text('');
            }
            else {
                $("#spansection625abcdCDName1").text(validationTextMessage);

            }
        }
        self.validationCDDesignation1sectionA = function () {
            if ($("#CDDesignation1").val() != "") {
                $("#spansection625abcdCDDesignation1").text('');
            }
            else {
                $("#spansection625abcdCDDesignation1").text(validationTextMessage);

            }
        }
        self.validationCDContactNumber1sectionA = function () {
            if ($("#CDContactNumber1").val() != "") {
                $("#spansection625abcdCDContactNumber1").text('');
            }
            else {
                $("#spansection625abcdCDContactNumber1").text(validationTextMessage);

            }
        }
        self.validationCDMobileNumber1sectionA = function () {
            if ($("#CDMobileNumber1").val() != "") {
                $("#spansection625abcdCDMobileNumber1").text('');
            }
            else {
                $("#spansection625abcdCDMobileNumber1").text(validationTextMessage);
            }
        }
        self.validationCDAddress1sectionA = function () {
            if ($("#CDAddress1").val() != "") {
                $("#spansection625abcdCDAddress1").text('');
            }
            else {
                $("#spansection625abcdCDAddress1").text(validationTextMessage);
            }
        }
        self.validationChangeControlLicensedOperatorsectionA = function () {
            if ($("#ChangeControlLicensedOperator").val() != "") {
                $("#spansection625abcdChangeControlLicensedOperator").text('');
            }
            else {
                $("#spansection625abcdChangeControlLicensedOperator").text(validationTextMessage);

            }
        }
        self.validationAnticipatedImpactOnBBBEERatingsectionA = function () {
            if ($("#AnticipatedImpactOnBBBEERating").val() != "") {
                $("#spansection625abcdAnticipatedImpactOnBBBEERating").text('');
            }
            else {
                $("#spansection625abcdAnticipatedImpactOnBBBEERating").text(validationTextMessage);

            }
        }
        self.validationCDEmailID1sectionA = function () {
            if ($("#CDEmailID1").val() != "") {
                $("#spansection625abcdCDCDEmailID1").text('');
            }
            else {
                $("#spansection625abcdCDCDEmailID1").text(validationTextMessage);
                //errors = errors + 1;
            }
        }
        //sectionAEnd

        //SectionB
        self.validationTOMSLogEntryNoSectionB = function () {
            if ($("#TOMSLogEntryNosectionb").val() != "") {
                $("#spanTOMSLogEntryNosectionB").text('');
            }
            else {
                $("#spanTOMSLogEntryNosectionB").text(validationTextMessage);

            }
        }
        self.validationOperatorNamesectionB = function () {
            if ($("#OperatorNamesectionb").val() != "") {
                $("#spanOperatorNamesectionB").text('');
            }
            else {
                $("#spanOperatorNamesectionB").text(validationTextMessage);

            }
        }
        self.validationLincseNumbersectionB = function () {
            if ($("#LincseNumbersectionb").val() != "") {
                $("#spanLincseNumbersectionB").text('');
            }
            else {
                $("#spanLincseNumbersectionB").text(validationTextMessage);

            }
        }
        self.validationSiteTerminalsectionB = function () {
            if ($("#SiteTerminalsectionb").val() != "") {
                $("#spanSiteTerminalsectionB").text('');
            }
            else {
                $("#spanSiteTerminalsectionB").text(validationTextMessage);

            }
        }
        self.validationCDNamesectionB = function () {
            if ($("#CDNamesectionb").val() != "") {
                $("#spanCDNamesectionB").text('');
            }
            else {
                $("#spanCDNamesectionB").text(validationTextMessage);

            }
        }
        self.validationCDDesignationsectionB = function () {
            if ($("#CDDesignationsectionb").val() != "") {
                $("#spanCDDesignationsectionB").text('');
            }
            else {
                $("#spanCDDesignationsectionB").text(validationTextMessage);

            }
        }
        self.validationCDContactNumbersectionB = function () {
            if ($("#CDContactNumbersectionb").val() != "") {
                $("#spanCDContactNumbersectionB").text('');
            }
            else {
                $("#spanCDContactNumbersectionB").text(validationTextMessage);
            }
        }
        self.validationCDMobileNumbersectionB = function () {
            if ($("#CDMobileNumbersectionb").val() != "") {
                $("#spanCDMobileNumbersectionB").text('');
            }
            else {
                $("#spanCDMobileNumbersectionB").text(validationTextMessage);
            }
        }
        self.validationCDAddresssectionB = function () {
            if ($("#CDAddresssectionb").val() != "") {
                $("#spanCDAddresssectionB").text('');
            }
            else {
                $("#spanCDAddresssectionB").text(validationTextMessage);
            }
        }
        self.validationIDIndustrialDisputeDateTimeSectionB = function () {
            if ($("#IDIndustrialDisputeDateTime").val() != "") {
                $("#spanIDIndustrialDisputeDateTimesectionB").text('');
            }
            else {
                $("#spanIDIndustrialDisputeDateTimesectionB").text(validationTextMessage);
            }
        }
        self.validationIDTimeReportedSectionB = function () {
            if ($("#IDTimeReported").val() != "") {
                $("#spanIDTimeReportedsectionB").text('');
            }
            else {
                $("#spanIDTimeReportedsectionB").text(validationTextMessage);
            }
        }
        self.validationIDDisputeSpecificLocationsectionB = function () {
            if ($("#IDDisputeSpecificLocationsectionb").val() != "") {
                $("#spanIDDisputeSpecificLocationsectionB").text('');
            }
            else {
                $("#spanIDDisputeSpecificLocationsectionB").text(validationTextMessage);

            }
        }
        self.validationIDTradeUnionNamesectionB = function () {
            if ($("#IDTradeUnionNamesectionb").val() != "") {
                $("#spanIDTradeUnionNamesectionB").text('');
            }
            else {
                $("#spanIDTradeUnionNamesectionB").text(validationTextMessage);

            }
        }
        self.validationIDTotalNoOfEmployeessectionB = function () {
            if ($("#IDTotalNoOfEmployeessectionb").val() != "") {
                $("#spanIDTotalNoOfEmployeessectionB").text('');
            }
            else {
                $("#spanIDTotalNoOfEmployeessectionB").text(validationTextMessage);

            }
        }
        self.validationIndustrialDisputeDescriptionSectionB = function () {
            if ($("#IndustrialDisputeDescription").val() != "") {
                $("#spanIndustrialDisputeDescriptionsectionB").text('');
            }
            else {
                $("#spanIndustrialDisputeDescriptionsectionB").text(validationTextMessage);

            }
        }
        self.validationIDStrikeStatuSSectionB = function () {
            if (self.hour24andsection625Model().section625b().IDStrikeStatuS() == "" || self.hour24andsection625Model().section625b().IDStrikeStatuS() == 'undefined' || self.hour24andsection625Model().section625b().IDStrikeStatuS() == null) {
                $("#spanIDStrikeStatuSsectionB").text(validationTextMessage);

            }
            else { $("#spanIDStrikeStatuSsectionB").text(""); }
            return true;
        }

        self.validationIDIDImpactOperationsSectionB = function () {
            if (self.hour24andsection625Model().section625b().IDImpactOperations() == "" || self.hour24andsection625Model().section625b().IDImpactOperations() == 'undefined' || self.hour24andsection625Model().section625b().IDImpactOperations() == null) {
                $("#spanIDIDImpactOperationssectionB").text(validationTextMessage);
                errors = errors + 1;
            }
            else { $("#spanIDIDImpactOperationssectionB").text(''); }
            return true;
        }

        self.validationIDViolencePresentSectionB = function () {
            if (self.hour24andsection625Model().section625b().IDViolencePresent() == "" || self.hour24andsection625Model().section625b().IDViolencePresent() == 'undefined' || self.hour24andsection625Model().section625b().IDViolencePresent() == null) {
                $("#spanIDViolencePresentsectionB").text(validationTextMessage);
                errors = errors + 1;
            }
            else { $("#spanIDViolencePresentsectionB").text(''); }
            return true;
        }
        self.validationCDEmailIDsectionb = function () {
            if ($("#CDEmailIDsectionb").val() != "") {
                $("#spanCDEmailIDsectionb").text('');
            }
            else {
                $("#spanCDEmailIDsectionb").text(validationTextMessage);

            }
        }
        //sectionBEnd


        //SectionCStart
        self.validationTOMSLogEntryNoSectionC = function () {
            if ($("#TOMSLogEntryNosectionC").val() != "") {
                $("#spanTOMSLogEntryNosectionC").text('');
            }
            else {
                $("#spanTOMSLogEntryNosectionC").text(validationTextMessage);

            }
        }
        self.validationOperatorNamesectionC = function () {
            if ($("#OperatorNamesectionc").val() != "") {
                $("#spanOperatorNamesectionC").text('');
            }
            else {
                $("#spanOperatorNamesectionC").text(validationTextMessage);
            }
        }
        self.validationLincseNumbersectionC = function () {
            if ($("#LincseNumbersectionc").val() != "") {
                $("#spanLincseNumbersectionC").text('');
            }
            else {
                $("#spanLincseNumbersectionC").text(validationTextMessage);
            }
        }
        self.validationSiteTerminalSectionC = function () {
            if ($("#SiteTerminalSectionc").val() != "") {
                $("#spanSiteTerminalsectionC").text('');
            }
            else {
                $("#spanSiteTerminalsectionC").text(validationTextMessage);
            }
        }
        self.validationIDIncidentDateTimesectionC = function () {
            if ($("#IDIncidentDateTimesectionc").val() != "") {
                $("#spanIDIncidentDateTimesectionC").text('');
            }
            else {
                $("#spanIDIncidentDateTimesectionC").text(validationTextMessage);
            }
        }
        self.validationIDTimeReportedsectionC = function () {
            if ($("#IDTimeReportedsectionc").val() != "") {
                $("#spanIDTimeReportedsectionC").text('');
            }
            else {
                $("#spanIDTimeReportedsectionC").text(validationTextMessage);
            }
        }
        self.validationIDIncidentSpecificLocationSectionC = function () {
            if ($("#IDIncidentSpecificLocation").val() != "") {
                $("#spanIDIncidentSpecificLocationsectionC").text('');
            }
            else {
                $("#spanIDIncidentSpecificLocationsectionC").text(validationTextMessage);
            }
        }
        self.validationCDNamesectionC = function () {
            if ($("#CDNamesectionc").val() != "") {
                $("#spanCDNamesectionC").text('');
            }
            else {
                $("#spanCDNamesectionC").text(validationTextMessage);
            }
        }
        self.validationCDDesignationSectionC = function () {
            if ($("#CDDesignationSectionc").val() != "") {
                $("#spanCDDesignationsectionC").text('');
            }
            else {
                $("#spanCDDesignationsectionC").text(validationTextMessage);
            }
        }
        self.validationCDContactNumberSectionC = function () {
            if ($("#CDContactNumberSectionC").val() != "") {
                $("#spanCDContactNumbersectionC").text('');
            }
            else {
                $("#spanCDContactNumbersectionC").text(validationTextMessage);
            }
        }
        self.validationCDEmailIDSectionC = function () {
            if ($("#CDEmailIDSectionC").val() != "") {
                $("#spanCDEmailIDsectionC").text('');
            }
            else {
                $("#spanCDEmailIDsectionC").text(validationTextMessage);
            }
        }
        self.validationCDMobileNumberSectionC = function () {
            if ($("#CDMobileNumberSectionC").val() != "") {
                $("#spanCDMobileNumbersectionC").text('');
            }
            else {
                $("#spanCDMobileNumbersectionC").text(validationTextMessage);
            }
        }
        self.validationCDAddressSectionC = function () {
            if ($("#CDAddressSectionC").val() != "") {
                $("#spanCDAddresssectionC").text('');
            }
            else {
                $("#spanCDAddresssectionC").text(validationTextMessage);
            }
        }
        self.validationPINameSectionC = function () {
            if ($("#PIName").val() != "") {
                $("#spanPINamesectionC").text('');
            }
            else {
                $("#spanPINamesectionC").text(validationTextMessage);
            }
        }
        self.validationPISurnameSectionC = function () {
            if ($("#PISurname").val() != "") {
                $("#spanPISurnamesectionC").text('');
            }
            else {
                $("#spanPISurnamesectionC").text(validationTextMessage);
            }
        }
        self.validationPIEmployeeNoSectionC = function () {
            if ($("#PIEmployeeNo").val() != "") {
                $("#spanPIEmployeeNosectionC").text('');
            }
            else {
                $("#spanPIEmployeeNosectionC").text(validationTextMessage);
            }
        }
        self.validationPINoOfDaysLostSectionC = function () {
            if ($("#PINoOfDaysLost").val() != "") {
                $("#spanPINoOfDaysLostsectionC").text('');
            }
            else {
                $("#spanPINoOfDaysLostsectionC").text(validationTextMessage);
            }
        }
        self.validationPIGenderSectionC = function () {
            if ($("#PIGender").val() != "") {
                $("#spanPIGendersectionC").text('');
            }
            else {
                $("#spanPIGendersectionC").text(validationTextMessage);
            }
        }
        self.validationPIAgeSectionC = function () {
            if ($("#PIAge").val() != "") {
                $("#spanPIAgesectionC").text('');
            }
            else {
                $("#spanPIAgesectionC").text(validationTextMessage);
            }
        }
        self.validationPIGradePositionSectionC = function () {
            if ($("#PIGradePosition").val() != "") {
                $("#spanPIGradePositionsectionC").text('');
            }
            else {
                $("#spanPIGradePositionsectionC").text(validationTextMessage);
            }
        }
        self.validationPIPartOfBodySectionC = function () {
            if ($("#PIPartOfBody").val() != "") {
                $("#spanPIPartOfBodysectionC").text('');
            }
            else {
                $("#spanPIPartOfBodysectionC").text(validationTextMessage);
            }
        }
        self.validationDescriptionofIncidentsectionC = function () {
            if ($("#DescriptionofIncidentsectionc").val() != "") {
                $("#spanIncidentDescriptionsectionC").text('');
            }
            else {
                $("#spanIncidentDescriptionsectionC").text(validationTextMessage);
            }
        }
        self.validationIIInvestigatorNameSectionC = function () {
            if ($("#IIInvestigatorName").val() != "") {
                $("#spanIIInvestigatorNamesectionC").text('');
            }
            else {
                $("#spanIIInvestigatorNamesectionC").text(validationTextMessage);
            }
        }
        self.validationIIDesignationSectionC = function () {
            if ($("#IIDesignation").val() != "") {
                $("#spanIIDesignationsectionC").text('');
            }
            else {
                $("#spanIIDesignationsectionC").text(validationTextMessage);
            }
        }
        self.validationIIInvestigationDateSectionC = function () {
            if ($("#IIInvestigationDate").val() != "") {
                $("#spanIIInvestigationDatesectionC").text('');
            }
            else {
                $("#spanIIInvestigationDatesectionC").text(validationTextMessage);
            }
        }
        self.validationCurrentControlssectionC = function () {
            if ($("#CurrentControlssectionc").val() != "") {
                $("#spanCurrentControlssectionC").text('');
            }
            else {
                $("#spanCurrentControlssectionC").text(validationTextMessage);
            }
        }
        self.validationCDEmailIDSectionC = function () {
            if ($("#CDEmailIDSectionC").val() != "") {
                $("#spanCDEmailIDsectionC").text('');
            }
            else {
                $("#spanCDEmailIDsectionC").text(validationTextMessage);

            }
        }
        self.validationWITelephoneNoSectionC2 = function () {
            if ($("#WITelephoneNoSectionC2").val() != "") {
                $("#spanWITelephoneNo2sectionC").text('');
            }
            else {
                $("#spanWITelephoneNo2sectionC").text(validationTextMessage);
            }
        }
        self.validationWITelephoneNoSectionC = function () {
            if ($("#WITelephoneNoSectionC").val() != "") {
                $("#spanWITelephoneNosectionC").text('');
            }
            else {
                $("#spanWITelephoneNosectionC").text(validationTextMessage);
            }
        }


        //SectionCEnd


        //SectionDStart
        self.validationTOMSLogEntryNoSectionD = function () {
            if ($("#TOMSLogEntryNosectionD").val() != "") {
                $("#spanTOMSLogEntryNosectionD").text('');
            }
            else {
                $("#spanTOMSLogEntryNosectionD").text(validationTextMessage);
            }
        }
        self.validationOperatorNamesectionD = function () {
            if ($("#OperatorNamesectionD").val() != "") {
                $("#spanOperatorNamesectionD").text('');
            }
            else {
                $("#spanOperatorNamesectionD").text(validationTextMessage);
            }
        }
        self.validationLincseNumbersectionD = function () {
            if ($("#LincseNumbersectionD").val() != "") {
                $("#spanLincseNumbersectionD").text('');
            }
            else {
                $("#spanLincseNumbersectionD").text(validationTextMessage);
            }
        }
        self.validationSiteTerminalSectionD = function () {
            if ($("#SiteTerminalSectionD").val() != "") {
                $("#spanSiteTerminalsectionD").text('');
            }
            else {
                $("#spanSiteTerminalsectionD").text(validationTextMessage);
            }
        }
        self.validationIncidentDateTimesectionD = function () {
            if ($("#IncidentDateTimesectionD").val() != "") {
                $("#spanIncidentDateTimesectionD").text('');
            }
            else {
                $("#spanIncidentDateTimesectionD").text(validationTextMessage);
            }
        }
        self.validationTimeReportedSectionD = function () {
            if ($("#TimeReportedSectionD").val() != "") {
                $("#spanTimeReportedectionD").text('');
            }
            else {
                $("#spanTimeReportedectionD").text(validationTextMessage);
            }
        }
        self.validationSpecifyLocationOfFireSectionD = function () {
            if ($("#SpecifyLocationOfFireSectionD").val() != "") {
                $("#spanSpecifyLocationOfFireSectionD").text('');
            }
            else {
                $("#spanSpecifyLocationOfFireSectionD").text(validationTextMessage);
            }
        }
        self.validationCDNameSectionD = function () {
            if ($("#CDNameSectionD").val() != "") {
                $("#spanCDNameSectionD").text('');
            }
            else {
                $("#spanCDNameSectionD").text(validationTextMessage);
            }
        }
        self.validationCDDesignationSectionD = function () {
            if ($("#CDDesignationSectionD").val() != "") {
                $("#spanCDDesignationSectionD").text('');
            }
            else {
                $("#spanCDDesignationSectionD").text(validationTextMessage);
            }
        }
        self.validationCDContactNumberSectionD = function () {
            if ($("#CDContactNumberSectionD").val() != "") {
                $("#spanCDContactNumberSectionD").text('');
            }
            else {
                $("#spanCDContactNumberSectionD").text(validationTextMessage);
            }
        }
        self.validationCDMobileNumberSectionD = function () {
            if ($("#CDMobileNumberSectionD").val() != "") {
                $("#spanCDMobileNumberSectionD").text('');
            }
            else {
                $("#spanCDMobileNumberSectionD").text(validationTextMessage);
            }
        }
        self.validationCDEmailIDSectionD = function () {
            if ($("#CDEmailIDSectionD").val() != "") {
                $("#spanCDEmailIDSectionD").text('');
            }
            else {
                $("#spanCDEmailIDSectionD").text(validationTextMessage);
            }
        }
        self.validationCDAddressSectionD = function () {
            if ($("#CDAddressSectionD").val() != "") {
                $("#spanCDAddressSectionD").text('');
            }
            else {
                $("#spanCDAddressSectionD").text(validationTextMessage);
            }
        }
        self.validationAPDDamageSectionD = function () {
            if ($("#APDDamageSectionD").val() != "") {
                $("#spanAPDDamageSectionD").text('');
            }
            else {
                $("#spanAPDDamageSectionD").text(validationTextMessage);
            }
        }
        self.validationAPDMaximumEstimatedFinancialLossSectionD = function () {
            if ($("#APDMaximumEstimatedFinancialLossSectionD").val() != "") {
                $("#spanAPDMaximumEstimatedFinancialLossSectionD").text('');
            }
            else {
                $("#spanAPDMaximumEstimatedFinancialLossSectionD").text(validationTextMessage);
            }
        }
        self.validationAPDActualLossSectionD = function () {
            if ($("#APDActualLossSectionD").val() != "") {
                $("#spanAPDActualLossSectionD").text('');
            }
            else {
                $("#spanAPDActualLossSectionD").text(validationTextMessage);
            }
        }
        self.validationMEByWhomSectionD = function () {
            if ($("#MEByWhomSectionD").val() != "") {
                $("#spanMEByWhomSectionD").text('');
            }
            else {
                $("#spanMEByWhomSectionD").text(validationTextMessage);
            }
        }
        self.validationMEWithWhatBeforeFireSecionD = function () {
            if ($("#MEWithWhatBeforeFireSecionD").val() != "") {
                $("#spanMEWithWhatBeforeFireSecionD").text('');
            }
            else {
                $("#spanMEWithWhatBeforeFireSecionD").text(validationTextMessage);
            }
        }
        self.validationMEWithWhatAfterFireSectionD = function () {
            if ($("#MEWithWhatAfterFireSectionD").val() != "") {
                $("#spanMEWithWhatAfterFireSectionD").text('');
            }
            else {
                $("#spanMEWithWhatAfterFireSectionD").text(validationTextMessage);
            }
        }
        self.validationWCWeatherSectionD = function () {
            if ($("#WCWeatherSectionD").val() != "") {
                $("#spanWCWeatherSectionD").text('');
            }
            else {
                $("#spanWCWeatherSectionD").text(validationTextMessage);
            }
        }
        self.validationWCWindDirectionSectionD = function () {
            if ($("#WCWindDirectionSectionD").val() != "") {
                $("#spanWCWindDirectionSectionD").text('');
            }
            else {
                $("#spanWCWindDirectionSectionD").text(validationTextMessage);
            }
        }
        self.validationWCWindSpeedSectionD = function () {
            if ($("#WCWindSpeedSectionD").val() != "") {
                $("#spanWCWindSpeedSectionD").text('');
            }
            else {
                $("#spanWCWindSpeedSectionD").text(validationTextMessage);
            }
        }
        self.validationWCTemperatureSectionD = function () {
            if ($("#WCTemperatureSectionD").val() != "") {
                $("#spanWCTemperatureSectionD").text('');
            }
            else {
                $("#spanWCTemperatureSectionD").text(validationTextMessage);
            }
        }
        self.validationFireinvolvedFICommercialSectionD = function () {
            if (self.hour24andsection625Model().section625D().FICommercial() == "" && self.hour24andsection625Model().section625D().FICommercial() == 'undefined' && self.hour24andsection625Model().section625D().FICommercial() == null) {
                $("#spanAttend1SectionD").text(validationTextMessage);

            }
            else {
                $("#spanAttend1SectionD").text('');
                return true;
            }
        }
        self.validationFireinvolvedFIStorageSectionD = function () {
            if (self.hour24andsection625Model().section625D().FIStorage() == "" && self.hour24andsection625Model().section625D().FIStorage() == 'undefined' && self.hour24andsection625Model().section625D().FIStorage() == null) {
                $("#spanAttend2SectionD").text(validationTextMessage);

            }
            else {
                $("#spanAttend2SectionD").text('');
                return true;
            }
        }
        self.validationFireinvolvedFIIndustrySectionD = function () {
            if (self.hour24andsection625Model().section625D().FIIndustry() == "" && self.hour24andsection625Model().section625D().FIIndustry() == 'undefined' && self.hour24andsection625Model().section625D().FIIndustry() == null) {
                $("#spanAttend3SectionD").text(validationTextMessage);


            }
            else {
                $("#spanAttend3SectionD").text('');
                return true;

            }
        }
        self.validationFireinvolvedFITransportSectionD = function () {
            if (self.hour24andsection625Model().section625D().FITransport() == "" && self.hour24andsection625Model().section625D().FITransport() == 'undefined' && self.hour24andsection625Model().section625D().FITransport() == null) {
                $("#spanAttend4SectionD").text(validationTextMessage);


            }
            else {
                $("#spanAttend4SectionD").text('');
                return true;

            }
        }
        self.validationFireinvolvedFIOthersSectionD = function () {
            if (self.hour24andsection625Model().section625D().FIOthers() == "" && self.hour24andsection625Model().section625D().FIOthers() == 'undefined' && self.hour24andsection625Model().section625D().FIOthers() == null)
            {
                $("#spanAttend5SectionD").text(validationTextMessage);


            }
            else {
                $("#spanAttend5SectionD").text('');
                return true;
            }
        }
        self.validationFireDepartmentAttendsectionD = function () {

            if (self.hour24andsection625Model().section625D().FireDepartmentAttend() == "") {
                $("#spanFireDepartmentAttendSetionD").text(validationTextMessage);
            }
            else { $("#spanFireDepartmentAttendSetionD").text(''); }
            return true;
        }
        //SectionDEND

        //SectionEStart

        self.validationTOMSLogEntryNosectionE = function () {
            if ($("#TOMSLogEntryNosectionE").val() != "") {
                $("#spanTOMSLogEntryNosectionE").text('');
            }
            else {
                $("#spanTOMSLogEntryNosectionE").text(validationTextMessage);
            }
        }
        self.validationOperatorNameSectionE = function () {
            if ($("#OperatorNameSectionE").val() != "") {
                $("#spanOperatorNameSectionE").text('');
            }
            else {
                $("#spanOperatorNameSectionE").text(validationTextMessage);
            }
        }
        self.validationLincseNumberSectionE = function () {
            if ($("#LincseNumberSectionE").val() != "") {
                $("#spanLincseNumberSectionE").text('');
            }
            else {
                $("#spanLincseNumberSectionE").text(validationTextMessage);
            }
        }
        self.validationSiteTerminalSectionE = function () {
            if ($("#SiteTerminalSectionE").val() != "") {
                $("#spanSiteTerminalSectionE").text('');
            }
            else {
                $("#spanSiteTerminalSectionE").text(validationTextMessage);
            }
        }
        self.validationDateIncidentSectionE = function () {
            if ($("#DateIncidentSectionE").val() != "") {
                $("#spanDateIncidentSectionE").text('');
            }
            else {
                $("#spanDateIncidentSectionE").text(validationTextMessage);
            }
        }
        self.validationTimeReportedSectionE = function () {
            if ($("#TimeReportedSectionE").val() != "") {
                $("#spanTimeReportedSectionE").text('');
            }
            else {
                $("#spanTimeReportedSectionE").text(validationTextMessage);
            }
        }
        self.validationCDNameSectionE = function () {
            if ($("#CDNameSectionE").val() != "") {
                $("#spanCDNameSectionE").text('');
            }
            else {
                $("#spanCDNameSectionE").text(validationTextMessage);
            }
        }
        self.validationCDDesignationSectionE = function () {
            if ($("#CDDesignationSectionE").val() != "") {
                $("#spanCDDesignationSectionE").text('');
            }
            else {
                $("#spanCDDesignationSectionE").text(validationTextMessage);
            }
        }
        self.validationCDContactNumberSectionE = function () {
            if ($("#CDContactNumberSectionE").val() != "") {
                $("#spanCDContactNumberSectionE").text('');
            }
            else {
                $("#spanCDContactNumberSectionE").text(validationTextMessage);
            }
        }
        self.validationCDMobileNumberSectionE = function () {
            if ($("#CDMobileNumberSectionE").val() != "") {
                $("#spanCDMobileNumberSectionE").text('');
            }
            else {
                $("#spanCDMobileNumberSectionE").text(validationTextMessage);
            }
        }
        self.validationCDAddresssectionE = function () {
            if ($("#CDAddresssectionE").val() != "") {
                $("#spanCDAddresssectionE").text('');
            }
            else {
                $("#spanCDAddresssectionE").text(validationTextMessage);
            }
        }
        self.validationOwnerNameofStolenItemSectionE = function () {
            if ($("#OwnerNameofStolenItemSectionE").val() != "") {
                $("#spanOwnerNameofStolenItemSectionE").text('');
            }
            else {
                $("#spanOwnerNameofStolenItemSectionE").text(validationTextMessage);
            }
        }
        self.validationOwnerAddressSectionE = function () {
            if ($("#OwnerAddressSectionE").val() != "") {
                $("#spanOwnerAddressSectionE").text('');
            }
            else {
                $("#spanOwnerAddressSectionE").text(validationTextMessage);
            }
        }
        self.validationTelephoneNoSectionE = function () {

            if ($("#TelephoneNoSectionE").val() != "") {
                $("#spanTelephoneNoSectionE").text('');
            }
            else {
                $("#spanTelephoneNoSectionE").text(validationTextMessage);
            }
        }
        self.validationMObilenoSectionE = function () {
            if ($("#MObilenoSectionE").val() != "") {
                $("#spanMObilenoSectionE").text('');
            }
            else {
                $("#spanMObilenoSectionE").text(validationTextMessage);
            }
        }
        self.validationTargetDateSectionE = function () {
            if ($("#TargetDateSectionE").val() != "") {
                $("#spanTargetDateSectionE").text('');

                if (self.hour24andsection625Model().section625E().IDWhenandWhereStolenDateTime() >= self.hour24andsection625Model().section625E().IDWhenWasDiscoveredDateTime())
                { $("#TargetDate2SectionE").val(''); }
            }
            else {
                $("#spanTargetDateSectionE").text(validationTextMessage);
            }
        }
        self.validationIDWhenandWhereStolenLocationSectionE = function () {
            if ($("#IDWhenandWhereStolenLocationSectionE").val() != "") {
                $("#spanIDWhenandWhereStolenLocationSectionE").text('');
            }
            else {
                $("#spanIDWhenandWhereStolenLocationSectionE").text(validationTextMessage);
            }
        }
        self.validationTargetDate2SectionE = function () {
            if ($("#TargetDate2SectionE").val() != "") {
                $("#spanTargetDate2SectionE").text('');

            }
            else {
                $("#spanTargetDate2SectionE").text(validationTextMessage);
            }
        }
        self.validationIDWhenWasDiscoveredLocationSectionE = function () {
            if ($("#IDWhenWasDiscoveredLocationSectionE").val() != "") {
                $("#spanIDWhenWasDiscoveredLocationSectionE").text('');
            }
            else {
                $("#spanIDWhenWasDiscoveredLocationSectionE").text(validationTextMessage);
            }
        }
        self.validationTheftOccurSectionE = function () {
            if ($("#TheftOccurSectionE").val() != "") {
                $("#spanTheftOccurSectionE").text('');
            }
            else {
                $("#spanTheftOccurSectionE").text(validationTextMessage);
            }
        }
        self.validationProtectTheftSectionE = function () {
            if ($("#ProtectTheftSectionE").val() != "") {
                $("#spanProtectTheftSectionE").text('');
            }
            else {
                $("#spanProtectTheftSectionE").text(validationTextMessage);
            }
        }
        self.validationCircumstancesSectionE = function () {
            if ($("#CircumstancesSectionE").val() != "") {
                $("#spanCircumstancesSectionE").text('');
            }
            else {
                $("#spanCircumstancesSectionE").text(validationTextMessage);
            }
        }
        self.validationTheftAvoidedSectionE = function () {
            if ($("#TheftAvoidedSectionE").val() != "") {
                $("#spanTheftAvoidedSectionE").text('');
            }
            else {
                $("#spanTheftAvoidedSectionE").text(validationTextMessage);
            }
        }
        self.validationSAPSOBNumberSectionE = function () {
            if ($("#SAPSOBNumberSectionE").val() != "") {
                $("#spanSAPSOBNumberSectionE").text('');
            }
            else {
                $("#spanSAPSOBNumberSectionE").text(validationTextMessage);
            }
        }
        self.validationPoliceStationReportedToSEctionE = function () {
            if ($("#PoliceStationReportedToSEctionE").val() != "") {
                $("#spanPoliceStationReportedToSEctionE").text('');
            }
            else {
                $("#spanPoliceStationReportedToSEctionE").text(validationTextMessage);
            }
        }
        self.validationEmailIDSectionE = function () {
            if ($("#EmailIDSectionE").val() != "") {
                $("#spanEmailIDSectionE").text('');
            }
            else {
                $("#spanEmailIDSectionE").text(validationTextMessage);

            }
        }
        self.validationCDEmailIDSectionE = function () {
            if ($("#CDEmailIDSectionE").val() != "") {
                $("#spanCDEmailIDSectionE").text('');
            }
            else {
                $("#spanCDEmailIDSectionE").text(validationTextMessage);

            }
        }

        //self.validationTelephoneNoSectionE = function () {
        //    if ($("#TelephoneNoSectionE").val() != "") {
        //        $("#spanTelephoneNoSectionE").text('');
        //    }
        //    else {
        //        $("#spanTelephoneNoSectionE").text(validationTextMessage);
        //    }
        //}

        calEndDatetoday = function () {
            var startTime = $("#TargetDateSectionE").val();
            $("#TargetDate2SectionE").data('kendoDateTimePicker').min(startTime);
        };
        self.validationStolenFromBuildingSectionE = function () {

            if (self.hour24andsection625Model().section625E().StolenFromBuilding() == "") {
                $("#spanStolenFromBuildingSectionE").text(validationTextMessage);
            }
            else { $("#spanStolenFromBuildingSectionE").text(''); }
            return true;
        }

        self.validationISPSBreachSectionE = function () {

            if (self.hour24andsection625Model().section625E().ISPSBreach() == "") {
                $("#spanISPSBreachSEctionE").text(validationTextMessage);
            }
            else { $("#spanISPSBreachSEctionE").text(''); }
            return true;
        }
        //SectionEend

        //SectionGStart
        self.validationTOMSLogEntryNoSectionG = function () {
            if ($("#TOMSLogEntryNoSectionG").val() != "") {
                $("#spanTOMSLogEntryNoSectionG").text('');
            }
            else {
                $("#spanTOMSLogEntryNoSectionG").text(validationTextMessage);
            }
        }
        self.validationOperatorNameSectionG = function () {
            if ($("#OperatorNameSectionG").val() != "") {
                $("#spanOperatorNameSectionG").text('');
            }
            else {
                $("#spanOperatorNameSectionG").text(validationTextMessage);
            }
        }
        self.validationLincseNumberSectionG = function () {
            if ($("#LincseNumberSectionG").val() != "") {
                $("#spanLincseNumberSectionG").text('');
            }
            else {
                $("#spanLincseNumberSectionG").text(validationTextMessage);
            }
        }
        self.validationSiteTerminalSectionG = function () {
            if ($("#SiteTerminalSectionG").val() != "") {
                $("#spanSiteTerminalSectionG").text('');
            }
            else {
                $("#spanSiteTerminalSectionG").text(validationTextMessage);
            }
        }
        self.validationDateIncident9SectionG = function () {
            if ($("#DateIncident9SectionG").val() != "") {
                $("#spanDateIncident9SectionG").text('');
            }
            else {
                $("#spanDateIncident9SectionG").text(validationTextMessage);
            }
        }
        self.validationTimeReportedSectonG = function () {
            if ($("#TimeReportedSectonG").val() != "") {
                $("#spanTimeReportedSectonG").text('');
            }
            else {
                $("#spanTimeReportedSectonG").text(validationTextMessage);
            }
        }
        self.validationCDNameSectionG = function () {
            if ($("#CDNameSectionG").val() != "") {
                $("#spanCDNameSectionG").text('');
            }
            else {
                $("#spanCDNameSectionG").text(validationTextMessage);
            }
        }
        self.validationCDDesignationSectionG = function () {
            if ($("#CDDesignationSectionG").val() != "") {
                $("#spanCDDesignationSectionG").text('');
            }
            else {
                $("#spanCDDesignationSectionG").text(validationTextMessage);
            }
        }
        self.validationCDContactNumberSectionG = function () {
            if ($("#CDContactNumberSectionG").val() != "") {
                $("#spanCDContactNumberSectionG").text('');
            }
            else {
                $("#spanCDContactNumberSectionG").text(validationTextMessage);
            }
        }
        self.validationCDMobileNumberSectionG = function () {
            if ($("#CDMobileNumberSectionG").val() != "") {
                $("#spanCDMobileNumberSectionG").text('');
            }
            else {
                $("#spanCDMobileNumberSectionG").text(validationTextMessage);
            }
        }
        self.validationCDAddressSectionG = function () {
            if ($("#CDAddressSectionG").val() != "") {
                $("#spanCDAddressSectionG").text('');
            }
            else {
                $("#spanCDAddressSectionG").text(validationTextMessage);
            }
        }
        self.validationWIWitnessName1SectionG = function () {
            if ($("#WIWitnessName1SectionG").val() != "") {
                $("#spanWIWitnessName1SectionG").text('');
            }
            else {
                $("#spanWIWitnessName1SectionG").text(validationTextMessage);
            }
        }
        self.validationWIContactNo1SectionG = function () {
            if ($("#WIContactNo1SectionG").val() != "") {
                $("#spanWIContactNo1SectionG").text('');
            }
            else {
                $("#spanWIContactNo1SectionG").text(validationTextMessage);
            }
        }
        self.validationWIWitnessName2SectionG = function () {
            if ($("#WIWitnessName2SectionG").val() != "") {
                $("#spanWIWitnessName2SectionG").text('');
            }
            else {
                $("#spanWIWitnessName2SectionG").text(validationTextMessage);
            }
        }
        self.validationWIContactNo2SectionG = function () {
            if ($("#WIContactNo2SectionG").val() != "") {
                $("#spanWIContactNo2SectionG").text('');
            }
            else {
                $("#spanWIContactNo2SectionG").text(validationTextMessage);
            }
        }
        self.validationIncidentDescriptionSectionG = function () {
            if ($("#IncidentDescriptionSectionG").val() != "") {
                $("#spanIncidentDescriptionSectionG").text('');
            }
            else {
                $("#spanIncidentDescriptionSectionG").text(validationTextMessage);
            }
        }
        self.validationIncidentExtentSectionG = function () {
            if ($("#IncidentExtentSectionG").val() != "") {
                $("#spanIncidentExtentSectionG").text('');
            }
            else {
                $("#spanIncidentExtentSectionG").text(validationTextMessage);
            }
        }
        self.validationQuantityVolumeMaterialSectionG = function () {
            if ($("#QuantityVolumeMaterialSectionG").val() != "") {
                $("#spanQuantityVolumeMaterialSectionG").text('');
            }
            else {
                $("#spanQuantityVolumeMaterialSectionG").text(validationTextMessage);
            }
        }
        self.validationEstimateDistanceNearestWaterwaySectionG = function () {
            if ($("#EstimateDistanceNearestWaterwaySectionG").val() != "") {
                $("#spanEstimateDistanceNearestWaterwaySectionG").text('');
            }
            else {
                $("#spanEstimateDistanceNearestWaterwaySectionG").text(validationTextMessage);
            }
        }
        self.validationActivityTypeIncidentSectionG = function () {
            if ($("#ActivityTypeIncidentSectionG").val() != "") {
                $("#spanActivityTypeIncidentSectionG").text('');
            }
            else {
                $("#spanActivityTypeIncidentSectionG").text(validationTextMessage);
            }
        }
        self.validationIncidentIdentifiedSectionG = function () {
            if ($("#IncidentIdentifiedSectionG").val() != "") {
                $("#spanIncidentIdentifiedSectionG").text('');
            }
            else {
                $("#spanIncidentIdentifiedSectionG").text(validationTextMessage);
            }
        }
        self.validationImmediateReleventActionsTakenSectionG = function () {
            if ($("#ImmediateReleventActionsTakenSectionG").val() != "") {
                $("#spanImmediateReleventActionsTakenSectionG").text('');
            }
            else {
                $("#spanImmediateReleventActionsTakenSectionG").text(validationTextMessage);
            }
        }
        self.validationEnvironmentalImpactDescriptionSectionG = function () {
            if ($("#EnvironmentalImpactDescriptionSectionG").val() != "") {
                $("#spanEnvironmentalImpactDescriptionSectionG").text('');
            }
            else {
                $("#spanEnvironmentalImpactDescriptionSectionG").text(validationTextMessage);
            }
        }
        self.validationContributingFactorsCoursesSectionG = function () {
            if ($("#ContributingFactorsCoursesSectionG").val() != "") {
                $("#spanContributingFactorsCoursesSectionG").text('');
            }
            else {
                $("#spanContributingFactorsCoursesSectionG").text(validationTextMessage);
            }
        }
        self.validationLikelyUnderlyingCausesSectionG = function () {
            if ($("#LikelyUnderlyingCausesSectionG").val() != "") {
                $("#spanLikelyUnderlyingCausesSectionG").text('');
            }
            else {
                $("#spanLikelyUnderlyingCausesSectionG").text(validationTextMessage);
            }
        }

        self.validationLevelofIncidentSectionG = function () {
            if (self.hour24andsection625Model().section625G().LIMinorEnvironmentalIncident() == "") {
                if (self.hour24andsection625Model().section625G().LISignificantEnvironmentalIncident() == "") {
                    if (self.hour24andsection625Model().section625G().LIMajorEnvironmentalIncident() == "") {

                        $("#spanLIMinorEnvironmentalIncidentSectionG").text(validationTextMessage);
                        $("#spanLISignificantEnvironmentalIncidentSectionG").text(validationTextMessage);
                        $("#spanLIMajorEnvironmentalIncidentSectionG").text(validationTextMessage);
                        //ISPSBreach
                    }
                    else {
                        $("#spanLIMinorEnvironmentalIncidentSectionG").text("");
                        $("#spanLISignificantEnvironmentalIncidentSectionG").text("");
                        $("#spanLIMajorEnvironmentalIncidentSectionG").text("");
                    }
                }
                else {
                    $("#spanLIMinorEnvironmentalIncidentSectionG").text("");
                    $("#spanLISignificantEnvironmentalIncidentSectionG").text("");
                    $("#spanLIMajorEnvironmentalIncidentSectionG").text("");
                }
            }
            else {
                $("#spanLIMinorEnvironmentalIncidentSectionG").text("");
                $("#spanLISignificantEnvironmentalIncidentSectionG").text("");
                $("#spanLIMajorEnvironmentalIncidentSectionG").text("");
            }
            return true;
        }
        self.validationPoliceAdvicedSectionG = function () {
            if (self.hour24andsection625Model().section625E().PoliceAdviced() == "" || self.hour24andsection625Model().section625E().PoliceAdviced() == 'undefined' || self.hour24andsection625Model().section625E().PoliceAdviced() == null) {
                toastr.warning("Please Enter Police Advice.", "24 Hour Report 62(5) ");
                $("#spanPoliceAdvicedSectionE").text(validationTextMessage);
            }
            else { $("#spanPoliceAdvicedSectionE").text(""); }
            return true;
        }
        self.validationCDEmailIDSectionG = function () {
            if ($("#CDEmailIDSectionG").val() != "") {
                $("#spanCDEmailIDSectionG").text('');
            }
            else {
                $("#spanCDEmailIDSectionG").text(validationTextMessage);
                errors = errors + 1;
            }
        }
        //SectionGend



        self.validationIODOccuranceDateTimeMainScreen = function () {
            if ($("#IODOccuranceDateTime").val() != "") {
                $("#spanIODOccuranceDateTimemainscreen").text('');
            }
            else {
                $("#spanIODOccuranceDateTimemainscreen").text(validationTextMessage);
            }
        }



        //*****************************************************************************ValidationfunctionEnd**************************************************************************************************************************


        self.GotoTab1 = function (divingtaskexecutionData) {
            if (self.viewModeForTabs == "notification1") {
                self.viewModeForTabs('notification1');
                self.hour24andsection625Model().ViewModeForTabs('notification1');
                //GoToTab1(self, divingtaskexecutionData);
            }
            else {
                self.viewModeForTabs('notification1');
                self.hour24andsection625Model().ViewModeForTabs('notification1');
                GoToTab1(self, divingtaskexecutionData);
            }
            self.isGoBackVisible(false);
            self.isSaveVisible(true);
            self.isSubmitVisible(false);
            self.isGoBackVisible(false);
        }

        self.GotoTab2 = function (divingtaskexecutionData) {
            // aaaaaaaaaaaaaaaaa

            if (self.ValidateForm(divingtaskexecutionData)) {
                if (self.viewModeForTabs == "notification2") {
                    self.viewModeForTabs('notification2');
                    self.hour24andsection625Model().ViewModeForTabs('notification2');
                }
                else {
                    self.viewModeForTabs('notification2');
                    self.hour24andsection625Model().ViewModeForTabs('notification2');
                    GoToTab2(self, divingtaskexecutionData);
                }
                self.isGoBackVisible(true);
                self.isSaveVisible(false);

                if (self.isSavebuttonvisable() == true) {
                    if (self.IsEnable() == true) {
                        self.isSubmitVisible(true);
                    }
                    else { self.isSubmitVisible(false); }
                }
                else { self.isSubmitVisible(true); }
            }
        }
        //this method is  fires when back  button is pressed
        self.GoToPrevTab = function (arrivalnotificationData) {
            GoToTab1(self, arrivalnotificationData);
        }

        // validate the data 
        self.ValidateForm = function (ArrivalNotificationData) {

            // ArrivalNotificationData.validationEnabled(true);
            var cdnumber = $("#CDContactNumber").val();
            cdnumber = cdnumber.replace(/(\)|\()|_|-+/g, '');

            var mdnumber = $("#CDMobileNumber").val();
            //   var mdnumber = ArrivalNotificationData.CDMobileNumber();
            mdnumber = mdnumber.replace(/(\)|\()|_|-+/g, '');



            self.ArrivalNotificationValidation = ko.observable(ArrivalNotificationData);
            self.ArrivalNotificationValidation().errors = ko.validation.group(self.ArrivalNotificationValidation());
            var errors = self.ArrivalNotificationValidation().errors().length;


            if (cdnumber.length != 0) {
                if (cdnumber.length != 13) {
                    toastr.warning("Invalid Contact Number");
                    $("#ContactNum").text("Invalid Contact Number");
                    ArrivalNotificationData.errors().length = 1;

                }
                else if (cdnumber.length == 13) {
                    var validNo = parseInt(cdnumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Contact Number");
                        $("#ContactNum").text("Invalid Contact Number");
                        ArrivalNotificationData.errors().length = 1;
                    }
                }
            }
            if (mdnumber.length != 0) {
                if (mdnumber.length != 13) {
                    toastr.warning("Invalid Mobile Number");
                    $("#MobileNum").text("Invalid Mobile Number");
                    ArrivalNotificationData.errors().length = 1;

                }
                else if (mdnumber.length == 13) {
                    var validNo = parseInt(mdnumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Mobile Number");
                        $("#MobileNum").text("Invalid Mobile Number");
                        ArrivalNotificationData.errors().length = 1;
                    }
                }

            }

            $("#CDContactNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDContactNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo1SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WIContactNo2SectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#ContactNoOfComplainantSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumbersectionb").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#CDMobileNumberSectionD").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionG").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CDMobileNumberSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MObilenoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#WITelephoneNoSectionC").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#WITelephoneNoSectionC2").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#TelephoneNoSectionE").kendoMaskedTextBox({ mask: "(000)000-000-0000" });



            if (ArrivalNotificationData.NONatureCode() != "") {

                self.validationIODOccuranceDateTimeMainScreen();
                if (ValidateFormValues(self, ArrivalNotificationData) == true) {
                    if (ArrivalNotificationData.IODOccuranceDateTime() != "" || ArrivalNotificationData.IODOccuranceBriefDescription() != "" || ArrivalNotificationData.IODSpecificLocation() != "") {
                        if (self.viewModeForTabs() == "notification1") {
                            GoToTab2(self, ArrivalNotificationData);
                        }
                        else {
                            GoToTab1(self, ArrivalNotificationData);
                        }
                    }
                    else {

                        toastr.warning("You Are Not Enter The Initial Occurrence Details. Please Enter The Section.");
                        result = false;
                    }
                }



            }
            else {
                toastr.warning("You Have Not Selected The Nature Of Occurrence Type. Please Select The Section.");
                result = false;
            }

        }

        // Reason : Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //*******************************************************************************************************************************************************************************************************
        self.Cancelform = function () {
            self.hour24andsection625Model().reset();
            $('#spnTitle').html("24 Hour Report 62(5) ");
            window.location = '/Account/Login';


        }
        self.Resetform = function (data) {
            //var notify = self.viewModeForTabs();    
            self.hour24andsection625Model().reset();
            debugger;
            if (self.hour24andsection625Model().ViewModeForTabs() == "notification2") {
                GoToTab1(self, data);
            }

        
            self.Hours24reportValidation = ko.observable(data);
            self.Hours24reportValidation().errors = ko.validation.group(self.Hours24reportValidation());
            self.Hours24reportValidation().errors.showAllMessages(false);
            $("#spanIODOccuranceDateTimemainscreen").text('');
            $('input[name=naturetype]').attr('checked', false);
            $('#naturetype').val('');
        }
        RefreshCaptach = function () {
            var captachText = randString(6);
            self.CaptachText(captachText);
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
                $("#spanCaptachCode").text(validationTextMessage);
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }


        ContactMasking = function (item) {
            $("#CDContactNumber").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var varCDContactNumber = $("#CDContactNumber").data("kendoMaskedTextBox");
            self.CDContactNumber(varCDContactNumber.value());
        }

        MobileMasking = function (item) {
            $("#CDMobileNumber").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var varCDMobileNumber = $("#CDMobileNumber").data("kendoMaskedTextBox");
            self.CDMobileNumber(varCDMobileNumber.value());
        }


        HandleContactnumber = function (data, event) {
            $("#ContactNum").text('');
        }

        HandleMobilenumber = function (data, event) {
            $("#MobileNum").text('');
        }

        HandleContactComplaint = function (data, event) {
            $("#spanContactNoOfComplainantSectionG").text('');
        }


        self.Initialize();

    }

    IPMSRoot.Hour24AndSection625ViewModel = Hour24AndSection625ViewModel;

}(window.IPMSROOT));

function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}

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

var keynum;
var keychar;
var charcheck;
// validate the data 
function ValidateFormValues(self, ArrivalNotificationData) {
    ArrivalNotificationData.validationEnabled(true);
    var result = true;
    if (ArrivalNotificationData.errors().length > 0) {
        ArrivalNotificationData.errors.showAllMessages();
        toastr.warning("Please Fill All the Required Fields.");

        result = false;
        if (self.hour24andsection625Model().ViewModeForTabs() == "notification1") {
            GoToTab1(self, ArrivalNotificationData);
        }
        else if (self.hour24andsection625Model().ViewModeForTabs() == "notification2") {
            GoToTab1(self, ArrivalNotificationData);
        }

    }
    //if (result == false) {
    //    $('#ulTabs li:first a').click();
    //}
    return result;
}
function GoToTab1(self, arrivalnotificationData) {
    //if (self.viewModeForTabs() == 'notification1') {

    self.viewModeForTabs('notification1');
    self.hour24andsection625Model().ViewModeForTabs('notification1');
    var index = 0;
    HandleProgressBarAndActiveTab(index);
    self.isGoBackVisible(false);
    self.isSaveVisible(true);
    self.isSubmitVisible(false);
    self.isGoBackVisible(false);


}
function GoToTab2(self, arrivalnotificationData) {
    var index = 1;
    HandleProgressBarAndActiveTab(index);
    self.viewModeForTabs('notification2');
    self.hour24andsection625Model().ViewModeForTabs('notification2');
    self.isGoBackVisible(true);
    self.isSaveVisible(false);

    if (self.isSavebuttonvisable() == true) {
        if (self.IsEnable() == true) {
            self.isSubmitVisible(true);
        }
        else { self.isSubmitVisible(false); }
    }


}

function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$^[]$:/;
    return charcheck.test(keychar);
}


function checkEmail(email) {
    debugger;
    //var email = document.getElementById('txtEmail');
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (filter.test(email)) {
        email.focus;
        return true;
    } else {
        toastr.warning("Please Provide a Valid Email Address.", "24 Hour Report 62(5)");
        email.focus;
        return false;
    }
}
