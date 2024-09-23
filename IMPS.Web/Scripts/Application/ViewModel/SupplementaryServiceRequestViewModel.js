(function (IPMSRoot) {
    var SupplementaryServiceRequestViewModel = function (SuppServiceRequestID, viewDetail) {
        var self = this;
        self.isValidVCN = ko.observable(false);
        self.viewMode = ko.observable();
        self.supplementaryServiceRequestModel = ko.observable();
        //self.SupplementaryGridDetails = ko.observable();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        //For Common Validation
        self.validationHelper = new IPMSRoot.validationHelper();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.SuppReqeditableView = ko.observable(true);
        self.IsEditable = ko.observable();
        self.SupplementaryServiceRequestList = ko.observableArray();
        self.SupplementaryServiceRequestGridList = ko.observableArray();
        self.arrivalCommodityList = ko.observableArray();
        self.vcnlocal = ko.observable("");
        self.vcntype = ko.observable("");
        //Service Type Data
        self.serviceTypeData = ko.observable("");
        //Document Type Data
        self.documentTypeData = ko.observable("");
        self.ETADateValue = ko.observable();
        self.ETDDateValue = ko.observable();
        self.IMDGInformationList = ko.observableArray();
        self.AnyDangerousGoodsonBoardObsr = ko.observable("Yes");
        self.ETBObsr = ko.observable();
        self.ETUBObsr = ko.observable();
        self.fileSizeConfigValue = ko.observable();
        self.isHWPSlocationEnabled = ko.observable(false);
        self.isCWPSlocationEnabled = ko.observable(false);
        self.isHCWPSlocationEnabled = ko.observable(false);
        self.IsPrintVisible = ko.observable(false);
        self.suppServiceRequestModelGrid = ko.observable(new IPMSROOT.SuppServiceRequestModelGrid());
        //self.SupplementaryGridDetails = ko.observable(new IPMSROOT.SupplementaryGridDetails());
        //Common required field error message
        var validationMessage = "* This field is required."

        //Location DDL
        self.locationData = ko.observable();
        //Berth DDL
        self.berthData = ko.observable();

        //Auto Generated VCN Details
        self.getVCNDtls = ko.observable();
        self.isUploadFileMsg = ko.observable(false);

        //Variable for Hot Work Permit service File Upload
        self.isHWPSfileToUpload = ko.observable(false);

        //Variable for Hot and Cold Work Permit service File Upload
        self.isHCWPSfileToUpload = ko.observable(false);

        //Variable for Cold Work Permit service File Upload
        self.isCWPSfileToUpload = ko.observable(false);

        //////////////////////////////////Action  : Click functionality starts here//////////////////////////////////
        //Author  : Omprakash Kotha on 20th August 2014
        //Purpose : adding Supplementary Service Request
        //Action  : Add New + Button
        self.addSupplimentaryServRequest = function (data) {
           
            actionVisibility_True();
            //Binding the Supplementary service Request Model
            self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel());

            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Add Supplementary Service Request");

            //Type of template we are binding // Form or List tempalate
            self.viewMode('Form');

            self.IsUpdate(false);
            self.IsReset(true);
            self.IsSave(true);
            self.IsReset(true);
            self.SuppReqeditableView(true);
            self.IsEditable(true);
            self.SuppReqeditableView(true);
            self.isHWPSlocationEnabled(false);
            self.isCWPSlocationEnabled(false);
            self.isHCWPSlocationEnabled(false);
            self.IsPrintVisible(false);
            document.getElementById("serviceType").disabled = true;
        }
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.supplementaryServiceRequestModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }


        //Grid Action
        //View Details of selected Row in the Grid List of Supplementary Service Request List
        //Author : Sandeep A
       
        self.ViewServiceRequest = function (supplementaryservicerequest) {
            self.viewMode('Form');
            self.viewModelHelper.apiGet('api/SuppServiceRequestsById/' + supplementaryservicerequest.SuppServiceRequestID(), null,
             function (result) {


                 if (result.ArrivalNotification.ETA != null) {
                     result.ArrivalNotification.ETA = moment(result.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETA = "NA";
                 }

                 if (result.ArrivalNotification.ETD != null) {
                     result.ArrivalNotification.ETD = moment(result.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETD = "NA";
                 }
                 if (result.ArrivalNotification.ETB != null) {
                     result.ArrivalNotification.ETB = moment(result.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETB = "NA";
                 }
                 if (result.ArrivalNotification.ETUB != null) {
                     result.ArrivalNotification.ETUB = moment(result.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETUB = "NA";
                 }
                 if (supplementaryservicerequest.AnyDangerousGoods() == "A" || supplementaryservicerequest.AnyDangerousGoods() == "Yes") {
                     self.AnyDangerousGoodsonBoardObsr("Yes");
                 }
                 else {
                     self.AnyDangerousGoodsonBoardObsr("No");
                 }
                
                 self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel(result));

                 //lf.supplementaryServiceRequestModel = result;
                 // self.supplementaryServiceRequestModel(result)

                 //
                 //self.SupplementaryServiceRequestList(
                 //     new IPMSRoot.supplementaryServiceRequestModel(result)
                 //);
                 ////new IPMSRoot.SupplementaryServiceRequestModel(result);
                 ////    self.supplementaryServiceRequestModel() = result;


                
                 // self.supplementaryServiceRequestModel(supplementaryservicerequest);
             },
             null, null, null, false);


    
            self.viewModelHelper.apiGet('api/ETB_ETUBFromVCM', { vcn: supplementaryservicerequest.VCN() },
        function (result) {

            self.ETBObsr(moment(result.ETB).format('YYYY-MM-DD HH:mm'));
            self.ETUBObsr(moment(result.ETUB).format('YYYY-MM-DD HH:mm'));

            self.supplementaryServiceRequestModel().VesselData().ETA(moment(result.ETA).format('YYYY-MM-DD HH:mm'));
            self.supplementaryServiceRequestModel().VesselData().ETD(moment(result.ETD).format('YYYY-MM-DD HH:mm'));
            //commented by divya n getting these details from SP to increase the performance
            //self.supplementaryServiceRequestModel().VesselData().VesselType(result.CargoType);
           // self.supplementaryServiceRequestModel().VesselData().VesselNationality(result.VesselNationality);
          //  self.supplementaryServiceRequestModel().VesselData().NextPortOfCall(result.NextPortOfCall);
            //self.supplementaryServiceRequestModel().VesselData().LastPortOfCall(result.LastPortOfCall);

        }, null, null, false);

            if (supplementaryservicerequest.AnyDangerousGoods() == "A" && (supplementaryservicerequest.ServiceTypeName() == 'Cold Work Permit Service' || supplementaryservicerequest.ServiceTypeName() == 'Hot and Cold Work Permit Service' || supplementaryservicerequest.ServiceTypeName() == 'Hot Work Permit Service')) {//adeed by divya  to increase the performance
                //IMDGForSupplementaryServiceRequests
                self.IMDGInformationList.removeAll();
                self.viewModelHelper.apiGet('api/IMDGForSupplementaryServiceRequests', { vcn: supplementaryservicerequest.VCN() },
               function (result) {

                   $.each(result, function (index, value) {
                       self.IMDGInformationList.push(new IPMSRoot.IMDGDetails(value));

                   });
               }, null, null, false);
            }




            //Title For the Displayed Content //Add or Edit or View or Main Page

            $('#spnTitle').html("View Supplementary Service Request");
            self.IsSave(false);
            self.IsUpdate(false);
            self.SuppReqeditableView(false);
            self.IsReset(false);
            self.IsEditable(false);
            self.SuppReqeditableView(false);
            //self.IsEditable(true);
            self.isHWPSlocationEnabled(false);
            self.isCWPSlocationEnabled(false);
            self.isHCWPSlocationEnabled(false);
            self.IsPrintVisible(true);
            self.vcnlocal = supplementaryservicerequest.VCNSort;

            //actionVisibility_False();
            self.supplementaryServiceRequestModel().pendingTasks.removeAll();
            var ReferenceID = supplementaryservicerequest.SuppServiceRequestID();
            var WorkflowInstanceID = supplementaryservicerequest.WorkflowInstanceID();
            //;
          

            if (viewDetail == true) {


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
                                     self.supplementaryServiceRequestModel().pendingTasks.push(pendingtaskaction);
                                     //self.SupplementaryGridDetails().pendingTasks.push(pendingtaskaction);

                                 });
                             });
            }

            //if ($("#CWPSAvailable_N").is(':checked') == true) {
            //    $("#CWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqCWPSValidity").text('');
            //    $("#reqCWPSIssuingAuthority").text('');
            //}
            //else {
            //    $("#CWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqCWPSValidity").text('*');
            //    $("#reqCWPSIssuingAuthority").text('*');
            //}

            //if ($("#HWPSAvailable_N").is(':checked') == true) {
            //    $("#HWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqHWPSValidity").text('');
            //    $("#reqHWPSIssuingAuthority").text('');
            //}
            //else {
            //    $("#HWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqHWPSValidity").text('*');
            //    $("#reqHWPSIssuingAuthority").text('*');
            //}

            //if ($("#HCWPSAvailable_N").is(':checked') == true) {
            //    $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqHCWPSValidity").text('');
            //    $("#reqHCWPSIssuingAuthority").text('');
            //}
            //else {
            //    $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);
            //    $("#reqHCWPSValidity").text('*');
            //    $("#reqHCWPSIssuingAuthority").text('*');
            //}
        }

        //Grid Action
        //Edit Details of selected Row in the Grid List of Supplementary Service Request List
        //Author : Sandeep A
        self.EditServiceRequest = function (supplementaryservicerequest) {
            
            self.viewModelHelper.apiGet('api/SuppServiceRequestsById/' + supplementaryservicerequest.SuppServiceRequestID(), null,
            function (result) {


                if (result.ArrivalNotification.ETA != null) {
                    result.ArrivalNotification.ETA = moment(result.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                }
                else {
                    result.ArrivalNotification.ETA = "NA";
                }

                if (result.ArrivalNotification.ETD != null) {
                    result.ArrivalNotification.ETD = moment(result.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                }
                else {
                    result.ArrivalNotification.ETD = "NA";
                }
                if (result.ArrivalNotification.ETB != null) {
                    result.ArrivalNotification.ETB = moment(result.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                }
                else {
                    result.ArrivalNotification.ETB = "NA";
                }
                if (result.ArrivalNotification.ETUB != null) {
                    result.ArrivalNotification.ETUB = moment(result.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                }
                else {
                    result.ArrivalNotification.ETUB = "NA";
                }
                if (supplementaryservicerequest.AnyDangerousGoods() == "A" || supplementaryservicerequest.AnyDangerousGoods() == "Yes") {
                    self.AnyDangerousGoodsonBoardObsr("Yes");
                }
                else {
                    self.AnyDangerousGoodsonBoardObsr("No");
                }
                self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel(result));

                //lf.supplementaryServiceRequestModel = result;
                // self.supplementaryServiceRequestModel(result)

                //
                //self.SupplementaryServiceRequestList(
                //     new IPMSRoot.supplementaryServiceRequestModel(result)
                //);
                ////new IPMSRoot.SupplementaryServiceRequestModel(result);
                ////    self.supplementaryServiceRequestModel() = result;


                
                // self.supplementaryServiceRequestModel(supplementaryservicerequest);
            },
            null, null, null, false);


            self.viewModelHelper.apiGet('api/ETB_ETUBFromVCM', { vcn: supplementaryservicerequest.VCN() },
        function (result) {

            self.ETBObsr(moment(result.ETB).format('YYYY-MM-DD HH:mm'));
            self.ETUBObsr(moment(result.ETUB).format('YYYY-MM-DD HH:mm'));

            self.supplementaryServiceRequestModel().VesselData().ETA(moment(result.ETA).format('YYYY-MM-DD HH:mm'));
            self.supplementaryServiceRequestModel().VesselData().ETD(moment(result.ETD).format('YYYY-MM-DD HH:mm'));
            //self.supplementaryServiceRequestModel().VesselData().VesselType(result.CargoType);
            //self.supplementaryServiceRequestModel().VesselData().VesselNationality(result.VesselNationality);
            //self.supplementaryServiceRequestModel().VesselData().NextPortOfCall(result.NextPortOfCall);
            //self.supplementaryServiceRequestModel().VesselData().LastPortOfCall(result.LastPortOfCall);

        }, null, null, false);


            //    self.viewModelHelper.apiGet('api/ETB_ETUBFromVCM', { vcn: supplementaryservicerequest.VCN() },
            //function (result) {

            //    self.ETBObsr(moment(result.ETB).format('YYYY-MM-DD HH:mm'));
            //    self.ETUBObsr(moment(result.ETUB).format('YYYY-MM-DD HH:mm'));


            //    supplementaryservicerequest.VesselData().ETA(moment(result.ETA).format('YYYY-MM-DD HH:mm'));
            //    supplementaryservicerequest.VesselData().ETD(moment(result.ETD).format('YYYY-MM-DD HH:mm'));

            //    supplementaryservicerequest.VesselData().VesselType(result.CargoType);
            //    supplementaryservicerequest.VesselData().VesselNationality(result.VesselNationality);
            //    supplementaryservicerequest.VesselData().NextPortOfCall(result.NextPortOfCall);
            //    supplementaryservicerequest.VesselData().LastPortOfCall(result.LastPortOfCall);

            //}, null, null, false);

            if (supplementaryservicerequest.AnyDangerousGoods() == "A" || supplementaryservicerequest.AnyDangerousGoods() == "Yes") {
                self.AnyDangerousGoodsonBoardObsr("Yes");
            }
            else {
                self.AnyDangerousGoodsonBoardObsr("No");
            }
    
            //IMDGForSupplementaryServiceRequests
            self.IMDGInformationList.removeAll();
            if (supplementaryservicerequest.AnyDangerousGoods() == "A" && (supplementaryservicerequest.ServiceTypeName() == 'Cold Work Permit Service' || supplementaryservicerequest.ServiceTypeName() == 'Hot and Cold Work Permit Service' || supplementaryservicerequest.ServiceTypeName() == 'Hot Work Permit Service')) {//adeed by divya  to increase the performance
                self.viewModelHelper.apiGet('api/IMDGForSupplementaryServiceRequests', { vcn: supplementaryservicerequest.VCN() },
               function (result) {
                   $.each(result, function (index, value) {
                       self.IMDGInformationList.push(new IPMSRoot.IMDGDetails(value));

                   });

               }, null, null, false);
            }


            self.IsEditable(true);
            self.SuppReqeditableView(true);

            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Update Supplementary Service Request");

            var varFromDate = moment(self.supplementaryServiceRequestModel().FromDate());
            // var dt=new Date();
            self.supplementaryServiceRequestModel().FromDate(varFromDate);




            actionVisibility_True();
            self.IsSave(false);
            self.SuppReqeditableView(false);
            self.IsPrintVisible(false);
            self.IsReset(true);
            self.IsUpdate(true);
            self.viewMode('Form');
            $("#reqCWPSValidity").text('');
            $("#reqCWPSIssuingAuthority").text('');
            $("#reqHWPSValidity").text('');
            $("#reqHWPSIssuingAuthority").text('');
            $("#reqHCWPSValidity").text('');
            $("#reqHCWPSIssuingAuthority").text('');
            $("#Vessel").data('kendoAutoComplete').enable(true);
            // self.supplementaryServiceRequestModel(supplementaryservicerequest);
            if ($("#CWPSAvailable_N").is(':checked') == true) {
                document.getElementById("CWPSValidity").disabled = true;
                document.getElementById("CWPSIssuingAuthority").disabled = true;
                $("#CWPSValidity").data('kendoDateTimePicker').enable(false);
                $("#reqCWPSValidity").text('');
                $("#reqCWPSIssuingAuthority").text('');
            }
            else {
                document.getElementById("CWPSValidity").disabled = false;
                document.getElementById("CWPSIssuingAuthority").disabled = false;
                $("#CWPSValidity").data('kendoDateTimePicker').enable(true);
                $("#reqCWPSValidity").text('*');
                $("#reqCWPSIssuingAuthority").text('*');
            }

            if ($("#HWPSAvailable_N").is(':checked') == true) {
                document.getElementById("HWPSValidity").disabled = true;
                document.getElementById("HWPSIssuingAuthority").disabled = true;
                $("#HWPSValidity").data('kendoDateTimePicker').enable(false);
                $("#reqHWPSValidity").text('');
                $("#reqHWPSIssuingAuthority").text('');
                $("#HWPSValidity").val('');
                $("#HWPSValidity").data("kendoDateTimePicker").value('');
            }
            else {
                document.getElementById("HWPSValidity").disabled = false;
                document.getElementById("HWPSIssuingAuthority").disabled = false;
                $("#HWPSValidity").data('kendoDateTimePicker').enable(true);
                $("#reqHWPSValidity").text('*');
                $("#reqHWPSIssuingAuthority").text('*');
            }

            if ($("#HCWPSAvailable_N").is(':checked') == true) {
                document.getElementById("HCWPSValidity").disabled = true;
                document.getElementById("HCWPSIssuingAuthority").disabled = true;
                $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);
                $("#reqHCWPSValidity").text('');
                $("#reqHCWPSIssuingAuthority").text('');
            }
            else {
                document.getElementById("HCWPSValidity").disabled = false;
                document.getElementById("HCWPSIssuingAuthority").disabled = false;
                $("#HCWPSValidity").data('kendoDateTimePicker').enable(true);
                $("#reqHCWPSValidity").text('*');
                $("#reqHCWPSIssuingAuthority").text('*');
            }
            self.ETADateValue(self.supplementaryServiceRequestModel().VesselData().ETA());
            self.ETDDateValue(self.supplementaryServiceRequestModel().VesselData().ETD());
            $("#WSDateOfService").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#WSDateOfService").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#FCSFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#FCSFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#FCSToDate").data('kendoDateTimePicker').min(self.supplementaryServiceRequestModel().FromDate());
            $("#FCSToDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#CWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#CWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#CWPSRequestToDate").data('kendoDateTimePicker').min(self.supplementaryServiceRequestModel().FromDate());
            $("#CWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());


            $("#HWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#HWPSRequestToDate").data('kendoDateTimePicker').min(self.supplementaryServiceRequestModel().FromDate());
            $("#HWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());


            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#HCWPSRequestToDate").data('kendoDateTimePicker').min(self.supplementaryServiceRequestModel().FromDate());
            $("#HCWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            if ($("#HWPSlocation option:selected").text() == "Others") {
                self.isHWPSlocationEnabled(true);
            }
            else {
                self.isHWPSlocationEnabled(false);
            }

            if ($("#CWPSlocation option:selected").text() == "Others") {
                self.isCWPSlocationEnabled(true);
            }
            else {
                self.isCWPSlocationEnabled(false);
            }

            if ($("#HCWPSlocation option:selected").text() == "Others") {
                self.isHCWPSlocationEnabled(true);
            }
            else {
                self.isHCWPSlocationEnabled(false);
            }

        }

        //Floating Crane 
        //Action : Button Add (Supplementary Service Request Form)
        //Author : Sandeep A
        //Purpose : Dynamically adding rows to "Services to be Performed"
        self.AddFloatingCrane = function (floatingcrane) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            $("#spantableFloatingCrane").text('');
            var databaseList = ko.toJSON(self.supplementaryServiceRequestModel().SuppFloatingCranesVO);
            var jsonObj = JSON.parse(databaseList);
            var rid = 0;
            var status = true;
            $.each(jsonObj, function (index, value) {
                rid = rid + 1;
                if (value.Quantity == "") {
                    toastr.warning('Services to be performed details grid error : Please enter Quantity data at row number : ' + rid);
                    status = false;
                    return;
                }
                if (value.MassMT == "") {
                    toastr.warning('Services to be performed details grid error : Please enter MassMT data at row number : ' + rid);
                    status = false;
                    return;
                }
                if (value.Description == "") {
                    toastr.warning('Services to be performed details grid error : Please enter Description data at row number : ' + rid);
                    status = false;
                    return;
                }
            });
            if (status) {
                self.supplementaryServiceRequestModel().SuppFloatingCranesVO.push(new IPMSROOT.SuppFloatingCranes());
            }
        }

        //Action : Button Cancel (Supplementary Service Request Form)
        //Purpose : Cancel Supplementary Service Request and redirect from FORM to List
        self.Cancel = function () {

            if (viewDetail == true) {
                if (self.vcntype == 'VM') {
                    window.location.href = '/VoyageMonitoring/ManageVoyageMonitoring/' + self.vcnlocal;
                }
                else {
                    window.location.href = '/Welcome';
                }
            }
            else {
                self.supplementaryServiceRequestModel().reset();
                $('#spnTitle').html("Supplementary Service Request List");
                self.viewMode('List');
                self.supplementaryServiceRequestModel().pendingTasks.removeAll();
            }

        }

        //Action  : Button Save
        //Purpose : saving new Supplementary Service Request

        self.SaveRequest = function (model) {
            var servicetype = $("#serviceType option:selected").text();
            model.ServiceTypeName($("#serviceType option:selected").text());
            if ($("#Vessel").val() != null && $("#Vessel").val() != '') {
                if (!($("#serviceType :selected").index() == 0)) {

                    if ($("#CWPSAvailable_N").is(':checked') == true) {
                        model.SuppHotColdWorkPermitsVO().GassFreeCertificateValidity(null);
                        model.SuppHotColdWorkPermitsVO().GassFreeIssuingAuthority('');
                    }
                    if ($("#HWPSAvailable_Y").is(':checked') == true) {

                        var isGasCerticateUploaded = false;

                        $.each(model.SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO(), function (index, value) {
                            if (value.DocumentTypeName() == "Gas Free Certificate") {
                                isGasCerticateUploaded = true;
                            }

                        });

                        if (!isGasCerticateUploaded) {
                            toastr.error("Please Submit the Gas Free Certificate");

                            ModifyRequest().disabled = true;

                        }
                    }
                    $("#spanchkTermsandConditions").text('');
                    var errors = Validation();

                    if (errors == 0) {
                        if ($("#chkTermsandConditions").is(":checked") == true) {
                            $('#divValidationError').text('');
                            $("#divValidationError").removeClass('alert alert-danger');
                            //success
                            self.viewModelHelper.apiPost('api/SupplementaryServiceRequests', ko.mapping.toJSON(model),
                                        function Message(data) {
                                            toastr.options.closeButton = true;
                                            toastr.options.positionClass = "toast-top-right";
                                            toastr.success("Supplementary service request saved successfully.", "Supplementary Service Request");
                                            $('#spnTitle').html("Supplementary Service Request List");
                                            self.LoadSupplementaryServiceRequestList();
                                            self.viewMode('List');
                                        });
                        }
                        else {
                            $("#spanchkTermsandConditions").text('Please accept terms and conditions.');
                            return;
                        }

                    }
                    else {
                        //error
                        $('#divValidationError').removeClass('display-none');
                        return;
                    }

                }
                else {
                    self.supplementaryServiceRequestModel().IsValidationEnabled(true);
                    $('#divValidationError').removeClass('display-none');
                    return;
                }

            }
            else {
                $("#spanvcnd").text('* Please select VCN.');
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        //Action  : Button Modify
        //Purpose : Modifiying the selected Supplementary Service SuppServiceRequestID
        self.ModifyRequest = function (model) {
            var servicetype = $("#serviceType option:selected").text();
            model.ServiceTypeName($("#serviceType option:selected").text());
            var errors = Validation();
            if (errors == 0) {
                if ($("#CWPSAvailable_N").is(':checked') == true) {
                    model.SuppHotColdWorkPermitsVO().GassFreeCertificateValidity(null);
                    model.SuppHotColdWorkPermitsVO().GassFreeIssuingAuthority('');
                }
                if ($("#HWPSAvailable_Y").is(':checked') == true) {

                    var isGasCerticateUploaded = false;

                    $.each(model.SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO(), function (index, value) {
                        if (value.DocumentTypeName() == "Gas Free Certificate") {
                            isGasCerticateUploaded = true;
                        }

                    });

                    if (!isGasCerticateUploaded) {
                        toastr.error("Please Submit the Gas Free Certificate");

                        ModifyRequest().disabled = true;

                    }
                }
                $('#divValidationError').text('');
                $("#divValidationError").removeClass('alert alert-danger');
                //success
                ;
                self.viewModelHelper.apiPut('api/SupplementaryServiceRequests', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";

                                toastr.success("Supplementary service request updated successfully.", "Supplementary Service Request");
                                $('#spnTitle').html("Supplementary Service Request List");
                                self.LoadSupplementaryServiceRequestList();
                                self.viewMode('List');
                            });
            }
            else {
                //error
                $('#divValidationError').removeClass('display-none');
                return;
            }


        }
        //Action  : Button Delete for single row under Floating Crane
        //Purpose : Dynamically Removing the "Services to be Performed details" from the floating Crane list
        self.removeServiceToBePerformedDelete = function (SuppFloatingCranes) {
            self.supplementaryServiceRequestModel().SuppFloatingCranesVO.remove(SuppFloatingCranes);
        }

        //Action  : Button Reset
        //Purpose : Reset Supplementary Service Request saved data
        self.ResetRequest = function (model) {
            if (self.IsSave()) {

                $("#HWPSfileToUpload").val("");
                ko.validation.reset();
                self.supplementaryServiceRequestModel().reset();
                document.getElementById("serviceType").disabled = true;
                $("#HWPSRequestToDate").data("kendoDateTimePicker").value('');
                $("#HWPSRequestFromDate").data("kendoDateTimePicker").value('');
                $("#HWPSValidity").data("kendoDateTimePicker").value('');
                $("#Vessel").val('');
            }
            else {
                ko.validation.reset();
                self.supplementaryServiceRequestModel().reset();
                self.EditServiceRequest(self.supplementaryServiceRequestModel());
            }
        }

        //Action  : Textbox Selecting VCN
        //Purpose : Inherited By Omprakash which was already implemented in Service Request for same details.Dated : 21st Aug 2014
        self.VesselSelect = function (e) {
            var dataItem = this.dataItem(e.item.index());
            self.IMDGInformationList.removeAll();
            self.viewModelHelper.apiGet('api/ETB_ETUBFromVCM', { vcn: dataItem.VCN },
           function (result) {

               self.ETBObsr(moment(result.ETB).format('YYYY-MM-DD HH:mm'));
               self.ETUBObsr(moment(result.ETUB).format('YYYY-MM-DD HH:mm'));
               dataItem.ETB = self.ETBObsr();
               dataItem.ETUB = self.ETUBObsr();
               dataItem.ETA = moment(result.ETA).format('YYYY-MM-DD HH:mm');
               dataItem.ETD = moment(result.ETD).format('YYYY-MM-DD HH:mm');

           }, null, null, false);

            self.ETADateValue(dataItem.ETA);
            self.ETDDateValue(dataItem.ETD);

            //IMDGForSupplementaryServiceRequests
            self.IMDGInformationList.removeAll();
            self.viewModelHelper.apiGet('api/IMDGForSupplementaryServiceRequests', { vcn: dataItem.VCN },
           function (result) {
               $.each(result, function (index, value) {
                   self.IMDGInformationList.push(new IPMSRoot.IMDGDetails(value));

               });

               if (result.length > 0) {
                   self.AnyDangerousGoodsonBoardObsr("Yes");
               }
               else {
                   self.AnyDangerousGoodsonBoardObsr("No");
               }

           }, null, null, false);


            self.supplementaryServiceRequestModel().VesselData(dataItem);
            document.getElementById("serviceType").disabled = false;
            var VCN = dataItem.VCN;
            //Author : Sandeep A
            self.viewModelHelper.apiGet('api/ArrivalNotification/GetArrivalCommoditiesByVCN/', { vcn: VCN },
            function (result) {
                self.arrivalCommodityList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSROOT.ArrivalCommodity(item);
                }));
            }, null, null, false);
            $('#spanvcnd').text('');
            self.supplementaryServiceRequestModel().ArrivalCommodities(self.arrivalCommodityList());
            self.supplementaryServiceRequestModel().VCN(dataItem.VCN);
            self.supplementaryServiceRequestModel().VesselName(dataItem.VesselName);
            ChangeserviceType();
        }

        //Action : Button File Upload
        //Purpose : Hot Work Permit Service File Upload
        self.HWPSuploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                $("#spanHWPSfileToUpload").text("");
                self.isHWPSfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                var uploadedFiles = [];
                uploadedFiles = self.supplementaryServiceRequestModel().UploadedFiles();
                var opmlFile = $('#HWPSfileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO(), function (item) {
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
                                //-- Checking File Size
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.supplementaryServiceRequestModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
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
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    //if (opmlFile.files.length > 0) {
                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.SuppHotColdWorkPermitDocument();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            Adddoc.DocumentTypeName(CategoryName);

                            //Adddoc.DocumentName(documentType);
                            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.push(Adddoc);
                            //$("select#selUploadDocs").prop('selectedIndex', 0);
                            //self.supplementaryServiceRequestModel().SuppHotColdWorkPermitDocumentsVO.push(Adddoc);

                        }));
                    });
                    //}
                }
                else {
                    $("#spanHWPSfileToUpload").text('Please select file.');
                    self.isHWPSfileToUpload(true);
                }
                self.supplementaryServiceRequestModel().UploadedFiles([]);
                $('#HWPSfileToUpload').val('');

                return;
            }
        }

        //Action : Button File Upload
        //Purpose : Hot and Cold Work Permit Service File Upload
        //Author : Sandeep A
        self.HCWPSuploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                $("#spanHCWPSfileToUpload").text("");
                self.isHCWPSfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                var uploadedFiles = [];
                uploadedFiles = self.supplementaryServiceRequestModel().UploadedFiles();
                var opmlFile = $('#HCWPSfileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });

                        if (match == null) {
                            //-- Checking For File Format
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                //elem.CategoryName = $('#selUploadDocs option:selected').text();
                                //elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                //elem.FileDetails = opmlFile.files[i];
                                //elem.IsAlreadyExists = false
                                //uploadedFiles.push(elem);
                                //self.suppDryDockModel().UploadedFiles(uploadedFiles);

                                //-- Checking File Size
                                //-- Checking File Size
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.supplementaryServiceRequestModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
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
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    //if (opmlFile.files.length > 0) {
                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.SuppHotColdWorkPermitDocument();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            Adddoc.DocumentTypeName(CategoryName);
                            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.push(Adddoc);
                            //$("select#selUploadDocs").prop('selectedIndex', 0);
                        }));
                    });
                    //}
                }
                else {
                    $("#spanHCWPSfileToUpload").text('Please select file.');
                    self.isHCWPSfileToUpload(true);
                }
                self.supplementaryServiceRequestModel().UploadedFiles([]);
                $('#HCWPSfileToUpload').val('');

                return;
            }
        }

        //Action : Button File Upload
        //Purpose : Cold Work Permit Service File Upload
        //Author : Sandeep A
        self.CWPSuploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                $("#spanCWPSfileToUpload").text("");
                self.isCWPSfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                var uploadedFiles = [];
                uploadedFiles = self.supplementaryServiceRequestModel().UploadedFiles();
                var opmlFile = $('#CWPSfileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });

                        if (match == null) {
                            //-- Checking For File Format
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                //elem.CategoryName = $('#selUploadDocs option:selected').text();
                                //elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                //elem.FileDetails = opmlFile.files[i];
                                //elem.IsAlreadyExists = false
                                //uploadedFiles.push(elem);
                                //self.suppDryDockModel().UploadedFiles(uploadedFiles);

                                //-- Checking File Size
                                //-- Checking File Size
                                var fileSizeInBytes = opmlFile.files[i].size;
                                var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                                if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                    var elem = {};
                                    elem.FileName = opmlFile.files[i].name;
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.supplementaryServiceRequestModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
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
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    //if (opmlFile.files.length > 0) {
                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.SuppHotColdWorkPermitDocument();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            Adddoc.DocumentTypeName(CategoryName);
                            //    //Adddoc.DocumentName(documentType);
                            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.push(Adddoc);
                            //$("select#selUploadDocs").prop('selectedIndex', 0);
                        }));
                    });                    //}
                }
                else {
                    $("#spanCWPSfileToUpload").text('Please select file.');
                    self.isCWPSfileToUpload(true);
                }
                self.supplementaryServiceRequestModel().UploadedFiles([]);
                $('#CWPSfileToUpload').val('');

                return;
            }
        }

        //Action : Button Delete
        //Purpose : Hot Work Permit Service Delete documents
        //Author : Sandeep A
        self.HWPSDeleteDocument = function (Adddoc) {
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.remove(Adddoc);
        }

        //Action : Button Delete
        //Purpose : Cold Work Permit Service Delete documents
        //Author : Sandeep A
        self.CWPSDeleteDocument = function (Adddoc) {
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.remove(Adddoc);
        }

        //Action : Button Delete
        //Purpose : Hot and Cold Work Permit Service Delete documents
        //Author : Sandeep A
        self.HCWPDeleteDocument = function (Adddoc) {
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.remove(Adddoc);
        }

        //////////////////////////////////Action : Click ends here //////////////////////////////////


        //////////////////////////////////Action  : Dropdownlist Binding starts here //////////////////////////////////
        //////////////////////////////////Author  : Omprakash Aug 2014//////////////////////////////////

        //Purpose : To get Berths From API (DDL for Berths)
        self.LoadBerths = function () {
            self.viewModelHelper.apiGet('api/BerthsWithPort', null,
                        function (result1) {
                            self.berthData(new IPMSRoot.Berths(result1));
                        }, null, null, false);
            var sk = ko.toJS(self.berthData().berthList());
            $.each(sk, function (key, val) {
                self.supplementaryServiceRequestModel().LocationID = val.LocationID;
            });
        }

        //Purpose : To get Location From API (DDL for Locations)
        self.LoadLocations = function () {
            self.viewModelHelper.apiGet('api/AllLocations', null,
                        function (result1) {
                            self.locationData(new IPMSRoot.Locations(result1));
                        }, null, null, false);
            var sk = ko.toJS(self.locationData().locationList());
            $.each(sk, function (key, val) {
                self.supplementaryServiceRequestModel().LocationID = val.LocationID;
            });
        }
        //Purpose : To get Service Type From API (DDL for Service Type)
        self.LoadServiceTypes = function () {
            self.viewModelHelper.apiGet('api/ServiceTypes', null,
                        function (result1) {
                            self.serviceTypeData(new IPMSRoot.ServiceType(result1));
                        }, null, null, false);
            var sk = ko.toJS(self.serviceTypeData().serviceTypeList());
            $.each(sk, function (key, val) {
                self.supplementaryServiceRequestModel().SubCatCode = val.SubCatCode;
            });
        }
        //Purpose : To get Document Type From API (DDL for Document Type)
        self.LoadDocumentTypes = function () {
            self.viewModelHelper.apiGet('api/SubCategoryDetails/SSDC', null,
                        function (result1) {
                            self.documentTypeData(new IPMSRoot.DocumentType(result1));

                        }, null, null, false);
            var sk = ko.toJS(self.documentTypeData().documentTypeList());
            $.each(sk, function (key, val) {
                self.supplementaryServiceRequestModel().SubCatCode = val.SubCatCode;
            });
        }
        //////Filling VCN details for AutoComplete (textbox with dropdownlist)
        //////Inherited By Omprakash which was already implemented in Service Request for same details : On 21st Aug GetVCNDetailsForServiceRequest GetVCNDetails
        self.LoadVCNDetails = function () {
            self.viewModelHelper.apiGet('api/ServiceRequest/GetVCNDetailsForServiceRequest', null,
           // self.viewModelHelper.apiGet('api/VesselArrestImmobilizationSAMSAStop/GetVcnDetails', null,
      function (result1) {
          self.getVCNDtls(new IPMSRoot.vesselModel(result1));
      }, null, null, false);
        }

        //////////////////////////////////Action  : Dropdownlist Binding ends here //////////////////////////////////



        //////////////////////////////////Action  : Validation starts here //////////////////////////////////
        ////Purpose : To Validate all fields
        ////Author : Omprakash  23th August 2014
        Validation = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var NoOfErrors = 0;
            if ($("#Vessel").val() != null && $("#Vessel").val() != '' && $("#serviceType").val() != null && $("#serviceType").val() != '') {
                //DefaultValidation();
                if ($("#serviceType").val() == 'WTST') {
                    // Water Services
                    if ($("#WServiceReqBerth").val() == "" || $("#WServiceReqBerth").val() == null) {
                        $("#spanWServiceReqBerth").text(validationMessage);
                        NoOfErrors++;
                    }
                    if ($("#WSDateOfService").val() == "" || $("#WSDateOfService").val() == null) {
                        $("#spanWSDateOfService").text(validationMessage);
                        NoOfErrors++;
                    }

                    if ($("#WSDateOfService").val() != "" && $("#WSDateOfService").val() != null) {

                        var dtETUB = new Date(Date.parse($("#txtETUB").text()));
                        var dtDateOfService = new Date(Date.parse($("#WSDateOfService").val()));
                        var currentDate = new Date();
                        currentDate = currentDate.setHours(currentDate.getHours() + 24);

                        //var dtOcupationFromDate = new Date(Date.parse(self.divingRequestModel().OcupationFromDate()));
                        //var dtOcupationToDate = new Date(Date.parse(self.divingRequestModel().OcupationToDate()));

                        if (dtDateOfService >= dtETUB) {

                            $("#spanWSDateOfService").text('Date of Service should be less than ETUB.');
                            NoOfErrors++;
                        }
                        else {
                            if (currentDate >= dtETUB) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("Water Service Request should be raised 24 Hours before ETUB.", "Supplementary Service Request");
                                NoOfErrors++;
                            }
                        }
                    }
                }
                else if ($("#serviceType").val() == 'CWST') {
                    //Cold Work  Berth 
                    if ($("#CWPSServiceReqBerth").val() == "" || $("#CWPSServiceReqBerth").val() == null) {
                        $("#spanCWPSServiceReqBerth").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Cold Work Location 
                    if ($("#CWPSlocation").val() == "" || $("#CWPSlocation").val() == null) {
                        $("#spanCWPSlocation").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Cold Work From Date 
                    if ($("#CWPSRequestFromDate").val() == "" || $("#CWPSRequestFromDate").val() == null) {
                        $("#spanCWPSRequestFromDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Cold Work To Date 
                    if ($("#CWPSRequestToDate").val() == "" || $("#CWPSRequestToDate").val() == null) {
                        $("#spanCWPSRequestToDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    ///Availablity
                    if ($("#CWPSAvailable_Y").is(':checked') == true) {
                        //Cold Work Validity 
                        if ($("#CWPSValidity").val() == "" || $("#CWPSValidity").val() == null) {
                            $("#spanCWPSValidity").text(validationMessage);
                            NoOfErrors++;
                        }
                        //Cold Work Issuing Authority 
                        if ($("#CWPSIssuingAuthority").val() == "" || $("#CWPSIssuingAuthority").val() == null) {
                            $("#spanCWPSIssuingAuthority").text(validationMessage);
                            NoOfErrors++;
                        }
                    }
                    if ($("#CWPSlocation option:selected").text() == "Others") {
                        if ($("#CWPSOthers").val() == "" || $("#CWPSOthers").val() == null) {
                            $("#spanCWPSOthers").text(validationMessage);
                            NoOfErrors++;
                        }
                    }
                }
                else if ($("#serviceType").val() == 'HWST') {
                    //Hot Work Service Berth 
                    if ($("#HWPSServiceReqBerth").val() == "" || $("#HWPSServiceReqBerth").val() == null) {
                        $("#spanHWPSServiceReqBerth").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot Work From Date 
                    if ($("#HWPSRequestFromDate").val() == "" || $("#HWPSRequestFromDate").val() == null) {
                        $("#spanHWPSRequestFromDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot Work To Date 
                    if ($("#HWPSRequestToDate").val() == "" || $("#HWPSRequestToDate").val() == null) {
                        $("#spanHWPSRequestToDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot Work Location 
                    if ($("#HWPSlocation").val() == "" || $("#HWPSlocation").val() == null) {
                        $("#spanHWPSlocation").text(validationMessage);
                        NoOfErrors++;
                    }
                    ///Availablity
                    if ($("#HWPSAvailable_Y").is(':checked') == true) {
                        //Hot Work Validity 
                        if ($("#HWPSValidity").val() == "" || $("#HWPSValidity").val() == null) {
                            $("#spanHWPSValidity").text(validationMessage);
                            //alert("Gas Free Certificate is mandatory");
                            NoOfErrors++;
                        }
                        //Hot Work Issuing Authority
                        if ($("#HWPSIssuingAuthority").val() == "" || $("#HWPSIssuingAuthority").val() == null) {
                            $("#spanHWPSIssuingAuthority").text(validationMessage);
                            NoOfErrors++;
                        }
                    }
                    if ($("#HWPSlocation option:selected").text() == "Others") {
                        if ($("#HWPSOthers").val() == "" || $("#HWPSOthers").val() == null) {
                            $("#spanHWPSOthers").text(validationMessage);
                            NoOfErrors++;
                        }
                    }
                }
                else if ($("#serviceType").val() == 'FCST') {
                    //Floating Service Berth Change
                    if ($("#FCSServiceReqBerth").val() == "" || $("#FCSServiceReqBerth").val() == null) {
                        $("#spanFCSServiceReqBerth").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Floating Service From Date Change
                    if ($("#FCSFromDate").val() == "" || $("#FCSFromDate").val() == null) {
                        $("#spanFCSFromDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Floating Service To Date Change
                    if ($("#FCSToDate").val() == "" || $("#FCSToDate").val() == null) {
                        $("#spanFCSToDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //if (self.supplementaryServiceRequestModel().SuppFloatingCranesVO().length > 0) {
                    if ($("#tblFloatingCrane tbody tr").length > 0) {

                        //$("#spantableFloatingCrane").text('* Floating Crane length is more than 1');
                        $("#spantableFloatingCrane").text('');

                        var databaseList = ko.toJSON(self.supplementaryServiceRequestModel().SuppFloatingCranesVO);
                        var jsonObj = JSON.parse(databaseList);
                        var rid = 0;

                        $.each(jsonObj, function (index, value) {
                            rid = rid + 1;
                            if (value.Quantity == "") {
                                toastr.warning('Services to be performed details grid error : Please enter Quantity data at row number : ' + rid);
                                NoOfErrors++;
                                return;
                            }
                            if (value.MassMT == "") {
                                toastr.warning('Services to be performed details grid error : Please enter MassMT data at row number : ' + rid);
                                NoOfErrors++;
                                return;
                            }
                            if (value.Description == "") {
                                toastr.warning('Services to be performed details grid error : Please enter Description data at row number : ' + rid);
                                NoOfErrors++;
                                return;
                            }
                        });
                    }
                    else {
                        $("#spantableFloatingCrane").text('* Please create atleast one Floating crane.');
                        NoOfErrors++;
                    }

                }
                else if ($("#serviceType").val() == 'HCST') {
                    //Hot and Cold Work Berth Change
                    if ($("#HCWPSServiceReqBerth").val() == "" || $("#HCWPSServiceReqBerth").val() == null) {
                        $("#spanHCWPSServiceReqBerth").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot and Cold Work From Date Change
                    if ($("#HCWPSRequestFromDate").val() == "" || $("#HCWPSRequestFromDate").val() == null) {
                        $("#spanHCWPSRequestFromDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot and Cold Work To Date Change
                    if ($("#HCWPSRequestToDate").val() == "" || $("#HCWPSRequestToDate").val() == null) {
                        $("#spanHCWPSRequestToDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Hot and Colde Work Location Change
                    if ($("#HCWPSlocation").val() == "" || $("#HCWPSlocation").val() == null) {
                        $("#spanHCWPSlocation").text(validationMessage);
                        NoOfErrors++;
                    }
                    ///Availablity
                    if ($("#HCWPSAvailable_Y").is(':checked') == true) {
                        //Hot and Cold Work Validity 
                        if ($("#HCWPSValidity").val() == "" || $("#HCWPSValidity").val() == null) {
                            $("#spanHCWPSValidity").text(validationMessage);
                            NoOfErrors++;
                        }
                        //Hot and Cold Work Issuing Authority
                        if ($("#HCWPSIssuingAuthority").val() == "" || $("#HCWPSIssuingAuthority").val() == null) {
                            $("#spanHCWPSIssuingAuthority").text(validationMessage);
                            NoOfErrors++;
                        }
                    }
                    if ($("#HCWPSlocation option:selected").text() == "Others") {
                        if ($("#HCWPSOthers").val() == "" || $("#HCWPSOthers").val() == null) {
                            $("#spanHCWPSOthers").text(validationMessage);
                            NoOfErrors++;

                        }
                    }
                }
                else if ($("#serviceType").val() == 'DDST') {
                    //Dry Dock Service From Date 
                    if ($("#DDRequestFromDate").val() == "" || $("#DDRequestFromDate").val() == null) {
                        $("#spanDDRequestFromDate").text(validationMessage);
                        NoOfErrors++;
                    }
                    //Dry Dock Service To Date 
                    if ($("#DDRequestToDate").val() == "" || $("#DDRequestToDate").val() == null) {
                        $("#spanDDRequestToDate").text(validationMessage);
                        NoOfErrors++;
                    }

                }
            }
            else {
                NoOfErrors++;
            }
            if (NoOfErrors > 0) {
                $("#ErrorMsg").text('* ErrorMsg');
            }
            return NoOfErrors;
        }

        ////Purpose : To Reset all field to origin values
        ////Author : Omprakash  23th August 2014
        function DefaultValidation() {
            self.supplementaryServiceRequestModel().UploadedFiles([]);
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO([]);
            //self.supplementaryServiceRequestModel().UploadedFile([]);
            $("#HWPSfileToUpload").val("");

            ////Dry Dock Service Section
            //Value are reset to Null
            $("#DDRequestFromDate").val('');
            $("#DDRequestToDate").val('');
            $("#DDCargo").val('');
            $("#DDBallast").val('');
            $("#DDBunkers").val('');
            $("#DDRemarks").val('');
            //Error msg reset to Null
            $("#spanDDRequestFromDate").text('');
            $("#spanDDRequestToDate").text('');

            ////WaterServiceSection
            //Value are reset to Null
            $("select#WServiceReqBerth").prop('selectedIndex', 0);
            $("#WSDateOfService").val('');
            $("#WSQuantity").val('');
            $("#WSComments").val('');
            //Error msg reset to Null
            $("#spanWServiceReqBerth").text('');
            $("#spanWSDateOfService").text('');

            ////Hot Work Service Section
            //Value are reset to Null
            $("select#HWPSServiceReqBerth").prop('selectedIndex', 0);
            $("#HWPSRequestFromDate").val('');
            $("#HWPSRequestToDate").val('');
            $("select#HWPSlocation").prop('selectedIndex', 0);
            $("#HWPSOthers").val('');

            $("#HWPSAvailable_N").attr("checked", true);
            $("#HWPSAvailable_N").val('N');
            $("#reqHWPSValidity").text('');
            $("#HWPSValidity").attr("disabled", "disabled");
            $("#HWPSValidity").data('kendoDateTimePicker').enable(false);
            $("#HWPSValidity").data("kendoDateTimePicker").value('');
            $("#reqHWPSIssuingAuthority").text('');
            $("#HWPSIssuingAuthority").attr("disabled", "disabled");

            $("#HWPSValidity").val('');
            $("#HWPSIssuingAuthority").val('');
            $("#HWPSRemarks").val('');
            //Error msg reset to Null
            $("#spanHWPSServiceReqBerth").text('');
            $("#spanHWPSRequestFromDate").text('');
            $("#spanHWPSRequestToDate").text('');
            $("#spanHWPSlocation").text('');
            $("#spanHWPSValidity ").text('');
            $("#spanHWPSIssuingAuthority ").text('');


            //Cold Work Service Section
            //Value are reset to Null
            $("select#CWPSServiceReqBerth").prop('selectedIndex', 0);
            $("#CWPSRequestFromDate").val('');
            $("#CWPSRequestToDate").val('');
            $("select#CWPSlocation").prop('selectedIndex', 0);
            $("#CWPSOthers").val('');

            $("#CWPSAvailable_N").attr("checked", true);
            $("#reqCWPSValidity").text('');
            $("#CWPSValidity").attr("disabled", "disabled");
            $("#CWPSValidity").data('kendoDateTimePicker').enable(false);
            $("#reqCWPSIssuingAuthority").text('');
            $("#CWPSIssuingAuthority").attr("disabled", "disabled");
            $("#CWPSValidity").val('');
            $("#CWPSIssuingAuthority").val('');
            $("#CWPSRemarks").val('');
            //Error msg reset to Null
            $("#spanCWPSServiceReqBerth").text('');
            $("#spanCWPSlocation").text('');
            $("#spanCWPSRequestFromDate").text('');
            $("#spanCWPSRequestToDate").text('');
            $("#spanCWPSValidity ").text('');
            $("#spanCWPSIssuingAuthority ").text('');

            ////Hot and Cold Work Service Section
            //Value are reset to Null            
            $("select#HCWPSServiceReqBerth").prop('selectedIndex', 0);
            $("#HCWPSRequestFromDate").val('');
            $("#HCWPSRequestToDate").val('');
            $("select#HCWPSlocation").prop('selectedIndex', 0);
            $("#HCWPSOthers").val('');
            $("#HCWPSAvailable_N").attr("checked", true);
            $("#reqHCWPSValidity").text('');
            $("#HCWPSValidity").attr("disabled", "disabled");
            $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);
            $("#reqHCWPSIssuingAuthority").text('');
            $("#HCWPSIssuingAuthority").attr("disabled", "disabled");
            $("#HCWPSValidity").val('');
            $("#HCWPSIssuingAuthority").val('');
            $("#HCWPSRemarks").val('');
            //Error msg reset to Null
            $("#spanHCWPSServiceReqBerth").text('');
            $("#spanHCWPSRequestFromDate").text('');
            $("#spanHCWPSRequestToDate").text('');
            $("#spanHCWPSlocation").text('');
            $("#spanHCWPSValidity ").text('');
            $("#spanHCWPSIssuingAuthority ").text('');

            //// Floating Work Service Section
            //Value are reset to Null
            $("select#FCSServiceReqBerth").prop('selectedIndex', 0);
            $("#FCSFromDate").val('');
            $("#FCSToDate").val('');
            $("#FCSComments").val('');
            ////Error msg reset to Null
            $("#spanFCSFromDate").text('');
            $("#spanFCSToDate").text('');
            $("#spanFCSServiceReqBerth").text('');

            self.isHWPSlocationEnabled(false);
            self.isCWPSlocationEnabled(false);
            self.isHCWPSlocationEnabled(false);

        }

        ///Hide details ON VIEW Action
        ////Author : Omprakash August 2014
        function actionVisibility_False() {
            //Hiding for delete and add link button from FORM
            $(".NeedToHide").hide();
            //For Checkbox
            document.getElementById("HWPSAvailable_Y").disabled = true;
            document.getElementById("HWPSAvailable_N").disabled = true;

            document.getElementById("CWPSAvailable_Y").disabled = true;
            document.getElementById("CWPSAvailable_N").disabled = true;
            document.getElementById("HCWPSAvailable_Y").disabled = true;
            document.getElementById("HCWPSAvailable_N").disabled = true;

            document.getElementById("HWPSValidity").disabled = true;
            document.getElementById("HWPSIssuingAuthority").disabled = true;
            document.getElementById("CWPSValidity").disabled = true;
            document.getElementById("CWPSIssuingAuthority").disabled = true;
            document.getElementById("HCWPSValidity").disabled = true;
            document.getElementById("HCWPSIssuingAuthority").disabled = true;


            ////kendoDateTimePicker 
            $("#CWPSRequestFromDate").data('kendoDateTimePicker').enable(false);
            $("#CWPSRequestToDate").data('kendoDateTimePicker').enable(false);
            $("#CWPSValidity").data('kendoDateTimePicker').enable(false);

            $("#DDRequestFromDate").data('kendoDateTimePicker').enable(false);
            $("#DDRequestToDate").data('kendoDateTimePicker').enable(false);

            $("#FCSFromDate").data('kendoDateTimePicker').enable(false);
            $("#FCSToDate").data('kendoDateTimePicker').enable(false);

            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').enable(false);
            $("#HCWPSRequestToDate").data('kendoDateTimePicker').enable(false);
            $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);

            $("#HWPSRequestFromDate").data('kendoDateTimePicker').enable(false);
            $("#HWPSRequestToDate").data('kendoDateTimePicker').enable(false);
            $("#HWPSValidity").data('kendoDateTimePicker').enable(false);

            $("#WSDateOfService").data('kendoDateTimePicker').enable(false);


        }
        ///Show details ON Add/ Edit Action
        ////Author : Omprakash August 2014
        function actionVisibility_True() {
            $(".NeedToHide").show();
            // CWPSAvailable_Y
            //$("#CWPSAvailable_Y").
            //$("#CWPSAvailable_Y").attr("disabled", "disabled");

            //$("#CWPSAvailable_N").attr("disabled", "disabled");
            document.getElementById("CWPSAvailable_Y").disabled = false;
            document.getElementById("CWPSAvailable_N").disabled = false;
            document.getElementById("HCWPSAvailable_Y").disabled = false;
            document.getElementById("HCWPSAvailable_N").disabled = false;
            $("#CWPSRequestFromDate").data('kendoDateTimePicker').enable(true);
            $("#CWPSRequestToDate").data('kendoDateTimePicker').enable(true);
            $("#CWPSValidity").data('kendoDateTimePicker').enable(true);

            $("#DDRequestFromDate").data('kendoDateTimePicker').enable(true);
            $("#DDRequestToDate").data('kendoDateTimePicker').enable(true);

            $("#FCSFromDate").data('kendoDateTimePicker').enable(true);
            $("#FCSToDate").data('kendoDateTimePicker').enable(true);


            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').enable(true);
            $("#HCWPSRequestToDate").data('kendoDateTimePicker').enable(true);
            $("#HCWPSValidity").data('kendoDateTimePicker').enable(true);

            $("#HWPSRequestFromDate").data('kendoDateTimePicker').enable(true);
            $("#HWPSRequestToDate").data('kendoDateTimePicker').enable(true);
            $("#HWPSValidity").data('kendoDateTimePicker').enable(true);

            $("#WSDateOfService").data('kendoDateTimePicker').enable(true);


        }


        //Validate Only Numberic
        ValidateNumeric = function (event) {

            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        }

        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function (event) {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }
        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        //////////////////////////////////Action  : Validation ends here //////////////////////////////////


        //////////////////////////////////Action : Change Events for Validation starts here//////////////////////////////////
        ////Author : Omprakash Kotha August 2014
        //Service Type Change
        ChangeserviceType = function () {
            DefaultValidation();


            $("#WSDateOfService").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#WSDateOfService").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#FCSFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#FCSFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#FCSToDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#FCSToDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#CWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#CWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#CWPSRequestToDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#CWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());


            $("#HWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#HWPSRequestToDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());


            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HCWPSRequestFromDate").data('kendoDateTimePicker').max(self.ETDDateValue());

            $("#HCWPSRequestToDate").data('kendoDateTimePicker').min(self.ETADateValue());
            $("#HCWPSRequestToDate").data('kendoDateTimePicker').max(self.ETDDateValue());

        }
        //Water Service Berth Change
        ChangeWServiceReqBerth = function () {
            if ($("#WServiceReqBerth").val() == "" || $("#WServiceReqBerth").val() == null) {
                $("#spanWServiceReqBerth").text(validationMessage);
            }
            else {
                $("#spanWServiceReqBerth").text('');
            }
        }

        //Water Service Date Of Service Change
        ChangeWSDateOfService = function () {
            if ($("#WSDateOfService").val() == "" || $("#WSDateOfService").val() == null) {
                $("#spanWSDateOfService").text(validationMessage);
            }
            else {
                $("#spanWSDateOfService").text('');
            }
        }

        //Hot Work Service Berth Change
        ChangeHWPSServiceReqBerth = function () {
            if ($("#HWPSServiceReqBerth").val() == "" || $("#HWPSServiceReqBerth").val() == null) {
                $("#spanHWPSServiceReqBerth").text(validationMessage);
            }
            else {
                $("#spanHWPSServiceReqBerth").text('');
            }
        }

        //Hot Work From Date Change
        ChangeHWPSRequestFromDate = function () {
            if ($("#HWPSRequestFromDate").val() == "" || $("#HWPSRequestFromDate").val() == null) {
                $("#spanHWPSRequestFromDate").text(validationMessage);
                $("#HWPSRequestToDate").val('');
            }
            else {
                $("#spanHWPSRequestFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#HWPSRequestFromDate").val();
                var EndDateValue = $("#HWPSRequestToDate").val();
                $("#HWPSRequestToDate").val('');
                $("#HWPSRequestToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }
        }

        //Hot Work To Date Change
        ChangeHWPSRequestToDate = function () {
            if (!(($("#HWPSRequestFromDate").val() == "" || $("#HWPSRequestFromDate").val() == null))) {
                if ($("#HWPSRequestToDate").val() == "" || $("#HWPSRequestToDate").val() == null) {
                    $("#spanHWPSRequestToDate").text(validationMessage);
                }
                else {
                    $("#spanHWPSRequestToDate").text('');
                }
            }
            else {
                $("#HWPSRequestFromDate").focus();
                $("#spanHWPSRequestFromDate").text(validationMessage);
                $("#HWPSRequestToDate").val('');
                $("#spanHWPSRequestToDate").text('');
            }
        }

        //Hot Work Location Change
        ChangeHWPSlocation = function () {
            $("#spanHCWPSOthers").text('');
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().OtherLocation('');
            if ($("#HWPSlocation").val() == "" || $("#HWPSlocation").val() == null) {
                $("#spanHWPSlocation").text(validationMessage);
            }
            else {
                $("#spanHWPSlocation").text('');
                self.isHWPSlocationEnabled(false);

                if ($("#HWPSlocation option:selected").text() == "Others") {
                    self.isHWPSlocationEnabled(true);

                }
            }
        }


        //Hot Work Others Change event
        changeHWPSOthers = function () {
            if ($("#HWPSOthers").val() == "" || $("#HWPSOthers").val() == null || $("#HWPSOthers").val() == undefined) {
                $("#spanHWPSOthers").text(validationMessage);
            }
            else {
                $("#spanHWPSOthers").text('');
            }
        }

        //Hot Work Gass Free Certificate Available Change
        ChangeHWPSGassFreeCertificateAvailable = function (selectedValue) {
            $("#spanHWPSValidity ").text('');
            $("#spanHWPSIssuingAuthority ").text('');
            $("#HWPSValidity").val('');
            $("#HWPSIssuingAuthority").val('');
            var myVal = $(selectedValue).val();
            if ($(selectedValue).val() == "N") {
                $("#reqHWPSValidity").text('');
                $("#reqHWPSIssuingAuthority").text('');
                $("#HWPSValidity").attr("disabled", "disabled");
                $("#HWPSIssuingAuthority").attr("disabled", "disabled");
                $("#HWPSValidity").data('kendoDateTimePicker').enable(false);
            }
            else {
                $("#reqHWPSValidity").text('*');
                $("#reqHWPSIssuingAuthority").text('*');
                $("#HWPSValidity").removeAttr("disabled");
                $("#HWPSIssuingAuthority").removeAttr("disabled");
                $("#HWPSValidity").data('kendoDateTimePicker').enable(true);
                //toastr.error("Please Submit the Gas Free Certificate");
            }
        }

        //Hot Work Validity Change
        ChangeHWPSValidity = function () {
            if ($("#HWPSValidity").val() == "" || $("#HWPSValidity").val() == null) {
                $("#spanHWPSValidity").text(validationMessage);
            }
            else {
                $("#spanHWPSValidity ").text('');
                $("#HWPSValidity").data("kendoDateTimePicker").value('');
                $("#HWPSValidity").val('');
            }
        }

        //Hot Work Issuing Authority
        ChangeHWPSIssuingAuthority = function () {
            if ($("#HWPSIssuingAuthority").val() == "" || $("#HWPSIssuingAuthority").val() == null) {
                $("#spanHWPSIssuingAuthority").text(validationMessage);
            }
            else {
                $("#spanHWPSIssuingAuthority ").text('');
            }
        }

        //Cold Work  Berth Change
        ChangeCWPSServiceReqBerth = function () {
            if ($("#CWPSServiceReqBerth").val() == "" || $("#CWPSServiceReqBerth").val() == null) {
                $("#spanCWPSServiceReqBerth").text(validationMessage);
            }
            else {
                $("#spanCWPSServiceReqBerth").text('');
            }
        }

        //Cold Work Location Change
        ChangeCWPSlocation = function () {
            $("#spanCWPSOthers").text('');
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().OtherLocation('');
            if ($("#CWPSlocation").val() == "" || $("#CWPSlocation").val() == null) {
                $("#spanCWPSlocation").text(validationMessage);
            }
            else {
                $("#spanCWPSlocation").text('');
                self.isCWPSlocationEnabled(false);

                if ($("#CWPSlocation option:selected").text() == "Others") {
                    self.isCWPSlocationEnabled(true);
                }
            }
        }

        //Cold Work Others Change event
        changeCWPSOthers = function () {
            if ($("#CWPSOthers").val() == "" || $("#CWPSOthers").val() == null || $("#CWPSOthers").val() == undefined) {
                $("#spanCWPSOthers").text(validationMessage);
            }
            else {
                $("#spanCWPSOthers").text('');
            }
        }

        //Cold Work From Date Change
        ChangeCWPSRequestFromDate = function () {
            if ($("#CWPSRequestFromDate").val() == "" || $("#CWPSRequestFromDate").val() == null) {
                $("#spanCWPSRequestFromDate").text(validationMessage);
            }
            else {
                $("#spanCWPSRequestFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#CWPSRequestFromDate").val();
                var EndDateValue = $("#CWPSRequestToDate").val();
                $("#CWPSRequestToDate").val('');
                $("#CWPSRequestToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }
        }

        //Cold Work To Date Change
        ChangeCWPSRequestToDate = function () {
            if (!($("#CWPSRequestFromDate").val() == "" || $("#CWPSRequestFromDate").val() == null)) {
                if ($("#CWPSRequestToDate").val() == "" || $("#CWPSRequestToDate").val() == null) {
                    $("#spanCWPSRequestToDate").text(validationMessage);
                }
                else {
                    $("#spanCWPSRequestToDate").text('');
                }
            }
            else {
                $("#CWPSRequestFromDate").focus();
                $("#spanCWPSRequestFromDate").text(validationMessage);
                $("#CWPSRequestToDate").val('');
                $("#spanCWPSRequestToDate").text('');
            }
        }

        //Cold Work Gass Free Certificate Available Change
        ChangeCWPSGassFreeCertificateAvailable = function (selectedValue) {
            $("#spanCWPSValidity ").text('');
            $("#spanCWPSIssuingAuthority ").text('');
            $("#CWPSValidity").val('');
            $("#CWPSIssuingAuthority").val('');
            var myVal = $(selectedValue).val();
            if ($(selectedValue).val() == "N") {
                $("#reqCWPSValidity").text('');
                $("#reqCWPSIssuingAuthority").text('');
                $("#CWPSValidity").attr("disabled", "disabled");
                $("#CWPSIssuingAuthority").attr("disabled", "disabled");
                $("#CWPSValidity").data('kendoDateTimePicker').enable(false);
            }
            else {
                $("#reqCWPSValidity").text('*');
                $("#reqCWPSIssuingAuthority").text('*');
                $("#CWPSValidity").removeAttr("disabled");
                $("#CWPSIssuingAuthority").removeAttr("disabled");
                $("#CWPSValidity").data('kendoDateTimePicker').enable(true);
            }

        }

        //Cold Work Validity Change
        ChangeCWPSValidity = function () {
            if ($("#CWPSValidity").val() == "" || $("#CWPSValidity").val() == null) {
                $("#spanCWPSValidity").text(validationMessage);
            }
            else {
                $("#spanCWPSValidity ").text('');
            }
        }

        //Cold Work Issuing Authority Change
        ChangeCWPSIssuingAuthority = function () {
            if ($("#CWPSIssuingAuthority").val() == "" || $("#CWPSIssuingAuthority").val() == null) {
                $("#spanCWPSIssuingAuthority").text(validationMessage);
            }
            else {
                $("#spanCWPSIssuingAuthority ").text('');
            }
        }

        //Hot and Cold Work Berth Change
        ChangeHCWPSServiceReqBerth = function () {
            if ($("#HCWPSServiceReqBerth").val() == "" || $("#HCWPSServiceReqBerth").val() == null) {
                $("#spanHCWPSServiceReqBerth").text(validationMessage);
            }
            else {
                $("#spanHCWPSServiceReqBerth").text('');
            }
        }

        //Hot and Cold Work From Date Change
        ChangeHCWPSRequestFromDate = function () {
            if ($("#HCWPSRequestFromDate").val() == "" || $("#HCWPSRequestFromDate").val() == null) {
                $("#spanHCWPSRequestFromDate").text(validationMessage);
            }
            else {
                $("#spanHCWPSRequestFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#HCWPSRequestFromDate").val();
                var EndDateValue = $("#HCWPSRequestToDate").val();
                $("#HCWPSRequestToDate").val('');
                $("#HCWPSRequestToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }

        }

        //Hot and Cold Work To Date Change
        ChangeHCWPSRequestToDate = function () {
            if (!($("#HCWPSRequestFromDate").val() == "" || $("#HCWPSRequestFromDate").val() == null)) {
                if ($("#HCWPSRequestToDate").val() == "" || $("#HCWPSRequestToDate").val() == null) {
                    $("#spanHCWPSRequestToDate").text(validationMessage);
                }
                else {
                    $("#spanHCWPSRequestToDate").text('');
                }
            }
            else {
                $("#HCWPSRequestFromDate").focus();
                $("#spanHCWPSRequestFromDate").text(validationMessage);
                $("#HCWPSRequestToDate").val('');
                $("#spanHCWPSRequestToDate").text('');
            }
        }

        //Hot and Cold Work Location Change
        ChangeHCWPSlocation = function () {
            $("#spanHCWPSOthers").text('');
            self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().OtherLocation('');
            if ($("#HCWPSlocation").val() == "" || $("#HCWPSlocation").val() == null) {
                $("#spanHCWPSlocation").text(validationMessage);
            }
            else {
                $("#spanHCWPSlocation").text('');
                self.isHCWPSlocationEnabled(false);

                if ($("#HCWPSlocation option:selected").text() == "Others") {
                    self.isHCWPSlocationEnabled(true);
                }
            }
        }

        //Hot and Cold Work Others Change event
        changeHCWPSOthers = function () {
            if ($("#HCWPSOthers").val() == "" || $("#HCWPSOthers").val() == null || $("#HCWPSOthers").val() == undefined) {
                $("#spanHCWPSOthers").text(validationMessage);
            }
            else {
                $("#spanHCWPSOthers").text('');
            }
        }

        //Hot and Cold Work Gass Free Certificate Available Change
        ChangeHCWPSGassFreeCertificateAvailable = function (selectedValue) {

            $("#spanHCWPSValidity ").text('');
            $("#spanHCWPSIssuingAuthority ").text('');
            $("#HCWPSValidity").val('');
            $("#HCWPSIssuingAuthority").val('');
            var myVal = $(selectedValue).val();
            if ($(selectedValue).val() == "N") {
                $("#reqHCWPSValidity").text('');
                $("#reqHCWPSIssuingAuthority").text('');
                $("#HCWPSValidity").attr("disabled", "disabled");
                $("#HCWPSIssuingAuthority").attr("disabled", "disabled");
                $("#HCWPSValidity").data('kendoDateTimePicker').enable(false);
            }
            else {
                $("#reqHCWPSValidity").text('*');
                $("#reqHCWPSIssuingAuthority").text('*');
                $("#HCWPSValidity").removeAttr("disabled");
                $("#HCWPSIssuingAuthority").removeAttr("disabled");
                $("#HCWPSValidity").data('kendoDateTimePicker').enable(true);
            }
        }

        //Hot and Cold Work Validity Change
        ChangeHCWPSValidity = function () {
            if ($("#HCWPSValidity").val() == "" || $("#HCWPSValidity").val() == null) {
                $("#spanHCWPSValidity").text(validationMessage);
            }
            else {
                $("#spanHCWPSValidity ").text('');
            }
        }

        //Hot and Cold Work Issuing Authority
        ChangeHCWPSIssuingAuthority = function () {
            if ($("#HCWPSIssuingAuthority").val() == "" || $("#HCWPSIssuingAuthority").val() == null) {
                $("#spanHCWPSIssuingAuthority").text(validationMessage);
            }
            else {
                $("#spanHCWPSIssuingAuthority ").text('');
            }
        }

        //Floating Service Berth Change
        ChangeFCSServiceReqBerth = function () {
            if ($("#FCSServiceReqBerth").val() == "" || $("#FCSServiceReqBerth").val() == null) {
                $("#spanFCSServiceReqBerth").text(validationMessage);
            }
            else {
                $("#spanFCSServiceReqBerth").text('');
            }
        }

        //Floating Service From Date Change
        ChangeFCSFromDate = function () {
            if ($("#FCSFromDate").val() == "" || $("#FCSFromDate").val() == null) {
                $("#spanFCSFromDate").text(validationMessage);
            }
            else {
                $("#spanFCSFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#FCSFromDate").val();
                var EndDateValue = $("#FCSToDate").val();
                $("#FCSToDate").val('');
                $("#FCSToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }
        }

        //Floating Service To Date Change
        ChangeFCSToDate = function () {
            if (!($("#FCSFromDate").val() == "" || $("#FCSFromDate").val() == null)) {
                if ($("#FCSToDate").val() == "" || $("#FCSToDate").val() == null) {
                    $("#spanFCSToDate").text(validationMessage);
                }
                else {
                    $("#spanFCSToDate").text('');
                }
            }
            else {
                $("#FCSFromDate").focus();
                $("#spanFCSFromDate").text(validationMessage);
                $("#FCSToDate").val('');
                $("#spanFCSToDate").text('');
            }
        }

        //Dry Dock Service From Date Change
        ChangeDDRequestFromDate = function () {
            if ($("#DDRequestFromDate").val() == "" || $("#DDRequestFromDate").val() == null) {
                $("#spanDDRequestFromDate").text(validationMessage);
            }
            else {
                $("#spanDDRequestFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#DDRequestFromDate").val();
                var EndDateValue = $("#DDRequestToDate").val();
                $("#DDRequestToDate").val('');
                $("#DDRequestToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }
        }

        //Dry Dock Service To Date Change
        ChangeDDRequestToDate = function () {
            if (!($("#DDRequestFromDate").val() == "" || $("#DDRequestFromDate").val() == null)) {
                if ($("#DDRequestToDate").val() == "" || $("#DDRequestToDate").val() == null) {
                    $("#spanDDRequestToDate").text(validationMessage);
                }
                else {
                    $("#spanDDRequestToDate").text('');
                }
            }
            else {
                $("#DDRequestFromDate").focus();
                $("#spanDDRequestFromDate").text(validationMessage);
                $("#DDRequestToDate").val('');
                $("#spanDDRequestToDate").text('');
            }
        }

        //Terms and Condition Change 
        ChangeTermsandConditions = function (e) {
            if ($(e).is(":checked")) {
                $("#spanchkTermsandConditions").text('');
            }
            else {
                $("#spanchkTermsandConditions").text('Please accept terms and conditions');
            }
        }

        //////////////////////////////////Action : Change Events for Validation ends here//////////////////////////////////

        //Only Future Dates
        calOpen = function () {
            //this.min(new Date());
        };
        calOpenWater = function () {
            //this.min(new Date());
        };

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {

                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);

            });
        }

        //Supplementary Service Request Initialization(pageload) mode
        self.Initialize = function () {
            self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel());
            //  self.LoadVCNDetails();
            //loading Service Type DDL on page load
            self.LoadSupplementaryServiceRequestList();
            //loading Document Type DDl on page  load
            self.LoadDocumentTypes();

            self.LoadServiceTypes();
            
            //loading location DDL on page load
            self.LoadLocations();
            //loading berths DDL on page load
            self.LoadBerths();
            // self.viewMode('List');
            self.GetFileSizeConfigValue();

            if (viewDetail == true) {
                $('#spnTitle').html("View Supplementary Service Request");
                self.IsSave(false);
                self.IsUpdate(false);
                self.SuppReqeditableView(false);
                self.IsReset(false);
                //self.IsEditable(false);
                self.viewMode('Form');

                //   self.supplementaryServiceRequestModel(Supplementaryservicerequest);
                // actionVisibility_False();
            }
            else {
                self.viewMode('List');
            }
            //$('select#HWPSlocation').append('<option>Others</option>');

            $("#HWPSlocation").append($("<option>Others</option>").attr("value", 'Others').text('Others'));

            $('#spnTitle').html("Supplementary Service Request List");
        }
        ;
        self.LoadSupplementaryServiceRequestList = function () {



            if (viewDetail == true) {

                var s1 = SuppServiceRequestID;

                SuppServiceRequestID = s1.split("x")[0];

                self.vcntype = s1.split("x")[1];

                self.viewModelHelper.apiGet('api/SuppServiceRequestsById/' + SuppServiceRequestID, null,
               function (result) {


                   if (result.ArrivalNotification.ETA != null) {
                       result.ArrivalNotification.ETA = moment(result.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                   }
                   else {
                       result.ArrivalNotification.ETA = "NA";
                   }

                   if (result.ArrivalNotification.ETD != null) {
                       result.ArrivalNotification.ETD = moment(result.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                   }
                   else {
                       result.ArrivalNotification.ETD = "NA";
                   }
                   if (result.ArrivalNotification.ETB != null) {
                       result.ArrivalNotification.ETB = moment(result.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                   }
                   else {
                       result.ArrivalNotification.ETB = "NA";
                   }
                   if (result.ArrivalNotification.ETUB != null) {
                       result.ArrivalNotification.ETUB = moment(result.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                   }
                   else {
                       result.ArrivalNotification.ETUB = "NA";
                   }
                   self.ViewServiceRequest(new IPMSRoot.SupplementaryServiceRequestModel(result));
               }, null, null, false);


            }
            else {
                var frmdate = self.suppServiceRequestModelGrid().RequestFrom();
                var todate = self.suppServiceRequestModelGrid().RequestTo();
                var vcnSearch = self.suppServiceRequestModelGrid().VCN();
                var vesselName = self.suppServiceRequestModelGrid().VesselName();

                if (vcnSearch == "") {
                    vcnSearch = "All";
                }
                if (vesselName == "") {
                    vesselName = "All";
                }



                self.viewModelHelper.apiGet('api/GetSupplementaryGridDetails/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName,
                null,

                    function (result) {
                        self.SupplementaryServiceRequestGridList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.SupplementaryGridDetails(item);
                        }));

                    });



                //self.viewModelHelper.apiGet('api/SupplementaryServiceRequests/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName,
                //    null,
                //function (result) {

                //    self.SupplementaryServiceRequestList(ko.utils.arrayMap(result, function (item) {

                //        if (item.ArrivalNotification.ETA != null) {
                //            item.ArrivalNotification.ETA = moment(item.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                //        }
                //        else {
                //            item.ArrivalNotification.ETA = null;
                //        }

                //        if (item.ArrivalNotification.ETD != null) {
                //            item.ArrivalNotification.ETD = moment(item.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                //        }
                //        else {
                //            item.ArrivalNotification.ETD = null;
                //        }
                //        if (item.ArrivalNotification.ETB != null) {
                //            item.ArrivalNotification.ETB = moment(item.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                //        }
                //        else {
                //            item.ArrivalNotification.ETB = null;
                //        }
                //        if (item.ArrivalNotification.ETUB != null) {
                //            item.ArrivalNotification.ETUB = moment(item.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                //        }
                //        else {
                //            item.ArrivalNotification.ETUB = null;
                //        }

                //        return new IPMSRoot.SupplementaryServiceRequestModel(item);
                //    }));
                //});
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

        SearchValidDate = function (data, event) {
            self.suppServiceRequestModelGrid().RequestFrom(self.suppServiceRequestModelGrid().RequestFrom());
        }


        self.GetDataClick = function (model) {


            var frmdt = self.suppServiceRequestModelGrid().RequestFrom();
            var todt = self.suppServiceRequestModelGrid().RequestTo();

            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');

            var vcnSearch = self.suppServiceRequestModelGrid().VCN();
            var vesselName = self.suppServiceRequestModelGrid().VesselName();


            if (vcnSearch == "") {
                vcnSearch = "All";
            }
            if (vesselName == "") {
                vesselName = "All";
            }



            self.viewModelHelper.apiGet('api/GetSupplementaryGridDetails/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName,
            null,

                function (result) {
                    self.SupplementaryServiceRequestGridList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.SupplementaryGridDetails(item);
                    }));

                });


            // self.GetSupplementaryGridDetails(frmdate,todate,vcnSearch,vesselName);
            //    ;
            //    self.viewModelHelper.apiGet('api/SupplementaryServiceRequests/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName,
            //  null,
            //function (result) {
            //    self.SupplementaryServiceRequestList(ko.utils.arrayMap(result, function (item) {

            //        if (item.ArrivalNotification.ETA != null) {
            //            item.ArrivalNotification.ETA = moment(item.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
            //        }
            //        else {
            //            item.ArrivalNotification.ETA = null;
            //        }

            //        if (item.ArrivalNotification.ETD != null) {
            //            item.ArrivalNotification.ETD = moment(item.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
            //        }
            //        else {
            //            item.ArrivalNotification.ETD = null;
            //        }
            //        if (item.ArrivalNotification.ETB != null) {
            //            item.ArrivalNotification.ETB = moment(item.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
            //        }
            //        else {
            //            item.ArrivalNotification.ETB = null;
            //        }
            //        if (item.ArrivalNotification.ETUB != null) {
            //            item.ArrivalNotification.ETUB = moment(item.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
            //        }
            //        else {
            //            item.ArrivalNotification.ETUB = null;
            //        }

            //        return new IPMSRoot.SupplementaryServiceRequestModel(item);
            //    }));
            //});

            //}


            self.GetSupplementaryGridDetails = function (SuppServiceRequestID) {


                self.viewModelHelper.apiGet('api/SuppServiceRequestsById/' + SuppServiceRequestID,
             function (result) {


                 if (result.ArrivalNotification.ETA != null) {
                     result.ArrivalNotification.ETA = moment(result.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETA = "NA";
                 }

                 if (result.ArrivalNotification.ETD != null) {
                     result.ArrivalNotification.ETD = moment(result.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETD = "NA";
                 }
                 if (result.ArrivalNotification.ETB != null) {
                     result.ArrivalNotification.ETB = moment(result.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETB = "NA";
                 }
                 if (result.ArrivalNotification.ETUB != null) {
                     result.ArrivalNotification.ETUB = moment(result.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETUB = "NA";
                 }
                 self.ViewServiceRequest(new IPMSRoot.SupplementaryServiceRequestModel(result));
             }, null, null, false);

            }

        }
       
        self.ClearFilter = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate()+14);
            fromdate.setDate(fromdate.getDate() -14);


            self.suppServiceRequestModelGrid().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.suppServiceRequestModelGrid().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


            self.suppServiceRequestModelGrid().VCN('');
            self.suppServiceRequestModelGrid().VesselName('');
            // self.suppServiceRequestModelGrid().MovementType('');

            self.LoadSupplementaryServiceRequestList();
        }


        self.viewWorkFlow = function (suppservicerequest) {
            var workflowinstanceId = suppservicerequest.WorkflowInstanceID();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel());
                  self.supplementaryServiceRequestModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        self.cancelReqst = function (model) {
            self.viewMode('List');
            self.viewMode('PopUp'); self.viewModelHelper.apiGet('api/SuppServiceRequestsById/' + model.SuppServiceRequestID(), null,
             function (result) {


                 if (result.ArrivalNotification.ETA != null) {
                     result.ArrivalNotification.ETA = moment(result.ArrivalNotification.ETA).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETA = "NA";
                 }

                 if (result.ArrivalNotification.ETD != null) {
                     result.ArrivalNotification.ETD = moment(result.ArrivalNotification.ETD).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETD = "NA";
                 }
                 if (result.ArrivalNotification.ETB != null) {
                     result.ArrivalNotification.ETB = moment(result.ArrivalNotification.ETB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETB = "NA";
                 }
                 if (result.ArrivalNotification.ETUB != null) {
                     result.ArrivalNotification.ETUB = moment(result.ArrivalNotification.ETUB).format('YYYY-MM-DD HH:mm');
                 }
                 else {
                     result.ArrivalNotification.ETUB = "NA";
                 }

                 self.supplementaryServiceRequestModel(new IPMSROOT.SupplementaryServiceRequestModel(result));

                 //lf.supplementaryServiceRequestModel = result;
                 // self.supplementaryServiceRequestModel(result)

                 //
                 //self.SupplementaryServiceRequestList(
                 //     new IPMSRoot.supplementaryServiceRequestModel(result)
                 //);
                 ////new IPMSRoot.SupplementaryServiceRequestModel(result);
                 ////    self.supplementaryServiceRequestModel() = result;



                 self.viewModelHelper.apiGet('api/ETB_ETUBFromVCM', { vcn: model.VCN() },
             function (result) {

                 self.ETBObsr(moment(result.ETB).format('YYYY-MM-DD HH:mm'));
                 self.ETUBObsr(moment(result.ETUB).format('YYYY-MM-DD HH:mm'));

                 self.supplementaryServiceRequestModel().VesselData().ETA(moment(result.ETA).format('YYYY-MM-DD HH:mm'));
                 self.supplementaryServiceRequestModel().VesselData().ETD(moment(result.ETD).format('YYYY-MM-DD HH:mm'));
                 self.supplementaryServiceRequestModel().VesselData().VesselType(result.CargoType);
                 self.supplementaryServiceRequestModel().VesselData().VesselNationality(result.VesselNationality);
                 self.supplementaryServiceRequestModel().VesselData().NextPortOfCall(result.NextPortOfCall);
                 self.supplementaryServiceRequestModel().VesselData().LastPortOfCall(result.LastPortOfCall);

             }, null, null, false);
                 // self.supplementaryServiceRequestModel(supplementaryservicerequest);
             },
             null, null, null, false);



        }

        self.closePopup = function () {
            self.supplementaryServiceRequestModel().WorkFlowRemarks("");
            self.viewMode('List');

        }
        self.cancelWFRequest = function (model) {

            if (model.WorkFlowRemarks() == undefined || model.WorkFlowRemarks() == "") {
                $('#spanremarks').text('Please Enter Reason');
                return;
            }

            self.viewModelHelper.apiPost('api/SuppServiceRequests/WFCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Supplementary Service Request Cancelled Successfully", "Supplementary Service Request");
                                $(".close").trigger("click");
                                self.LoadSupplementaryServiceRequestList();
                                self.viewMode('List');
                            });

        }



        self.Initialize();

    }
    IPMSRoot.SupplementaryServiceRequestViewModel = SupplementaryServiceRequestViewModel;
}(window.IPMSROOT));



