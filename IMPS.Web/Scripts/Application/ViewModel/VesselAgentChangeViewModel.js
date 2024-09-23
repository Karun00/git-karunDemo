toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";
(function (IPMSRoot) {
    //var isView = 0;

    var AgentChangeRequestViewModel = function (vcn, viewDetail) {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.isVisible = ko.observable(true);
        self.isView = ko.observable(0);
        self.isGoBackVisible = ko.observable(false);
        self.isSaveVisible = ko.observable(true);
        self.isSubmitVisible = ko.observable(false);
        self.isCancelVisible = ko.observable(true);
        self.isUploadFileVisible = ko.observable(true);
        self.isDelUploadFileVisible = ko.observable(true);
        self.fileSizeConfigValue = ko.observable();
        self.isReset = ko.observable(true);
        self.viewModeForTabs = ko.observable();
        self.IsVCNEnable = ko.observable(true);
        self.IsEffectiveDateEnable = ko.observable(true);
        self.IsProposedAgentEnable = ko.observable(true);
        self.IsReasonforTransferEnable = ko.observable(true);
        self.vesselAgentChangeReqReferenceData = ko.observable();
        self.vesselagentchangelist = ko.observableArray();
        self.agentchangerequestmodel = ko.observable(new IPMSROOT.AgentChangeRequestModel());
        self.getVCNDtls = ko.observableArray();
        self.isfileToUpload = ko.observable(false);
        self.validationHelper = new IPMSROOT.validationHelper();

        //To load approved VCN details
        self.LoadVCNDetails = function () {
            //self.viewModelHelper.apiGet('api/VesselAgentChange/GetVCNDetails',//'api/ServiceRequest/GetVCNDetails',
            self.viewModelHelper.apiGet('api/VCNDetails',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getVCNDtls);

              });
        }

        // To load Change of Agent Request list
        self.LoadVesselAgentChangeList = function () {

            if (viewDetail == true) {

                //self.viewModelHelper.apiGet('api/VesselAgentChange/GetzVesselAgentChangeRequests',
                self.viewModelHelper.apiGet('api/VesselAgentChange/' + vcn,
                 { vcn: vcn },
                  function (result) {

                      self.vesselagentchangelist(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.AgentChangeRequestModel(item);
                      }));
                      self.viewData(self.vesselagentchangelist()[0]);
                  }, null, null, false);
            }
            else {
                //self.viewModelHelper.apiGet('api/VesselAgentChange/GetVesselAgentChangeRequests',

                var etafrom = $('#ETAFrom').val() != undefined ? moment($('#ETAFrom').val()).format('YYYY-MM-DD') : moment(self.agentchangerequestmodel().ETAFrom()).format('YYYY-MM-DD');
                var etato = $('#ETATo').val() != undefined ? moment($('#ETATo').val()).format('YYYY-MM-DD') : moment(self.agentchangerequestmodel().ETATo()).format('YYYY-MM-DD');

                self.viewModelHelper.apiGet('api/VesselAgentChange/' + etafrom + '/' + etato,
               null,
                 function (result) {

                     self.vesselagentchangelist(ko.utils.arrayMap(result, function (item) {
                         return new IPMSRoot.AgentChangeRequestModel(item);
                     }));
                 }, null, null, false);
            }
        }
        // To initalze objects
        self.Initialize = function () {

            self.viewMode("List");
            self.LoadVesselAgentChangeList();
            self.LoadVesselagentChangesReferences();

            self.LoadVCNDetails();
            self.LoadVCNActiveDetails();
            self.GetFileSizeConfigValue();
            self.viewModeForTabs('notification1');


            if (viewDetail == true) {

            }
            else {
                self.agentchangerequestmodel(new IPMSRoot.AgentChangeRequestModel());
                self.viewMode('List');
            }
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {

                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);

            });
        }


        // To load veesel agent change reference data
        self.LoadVesselagentChangesReferences = function () {
            //self.viewModelHelper.apiGet('api/VesselAgentChange/GetVesselAgentChangeDtl', null,

            var modeview = self.IsProposedAgentEnable() === false ? 'VIEW' : 'ADDMODIFY';
            self.viewModelHelper.apiGet('api/VesselAgentChangeReferenceData/' + modeview, { mode: modeview },
                   function (result1) {
                       self.vesselAgentChangeReqReferenceData(new IPMSRoot.VesselAgentChangeReqReferenceData(result1));
                   }, null, null, false);
        }

        // Add Change of Agent Request request mode
        self.addvesselagentreq = function () {
            self.isVisible(true);
            self.isView(0);
            self.isReset(true);
            self.agentchangerequestmodel(new IPMSRoot.AgentChangeRequestModel());
            self.viewMode('Form');
            self.IsVCNEnable(true);
            self.isSaveVisible(true);
            self.isSubmitVisible(false);
            self.IsEffectiveDateEnable(true);
            self.IsProposedAgentEnable(true);
            self.IsReasonforTransferEnable(true);
            self.isGoBackVisible(false);
            self.isUploadFileVisible(true);
            self.isDelUploadFileVisible(true);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.viewModeForTabs('notification1');
            //   $('#spnTitile').html("Add Change Of Agent");
        }
        // View Change of Agent Request request mode
        minDate = function () {
            this.min(new Date());

        }
        self.viewData = function (data) {

            self.isVisible(false);
            self.isReset(false);
            $('#fileupload').hide();
            self.isView(1);
            self.viewMode("Form");
            self.IsProposedAgentEnable(false);
            self.LoadVesselagentChangesReferences();
            self.agentchangerequestmodel(data);
            self.isSubmitVisible(false);
            self.isSaveVisible(true);
            self.isGoBackVisible(false);
            self.IsVCNEnable(false);
            self.IsEffectiveDateEnable(false);
            self.IsReasonforTransferEnable(false);
            self.isUploadFileVisible(false);
            self.isDelUploadFileVisible(false);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.viewModeForTabs('notification1');
            //$('#spnTitile').html("View Change Of Agent");            

            self.agentchangerequestmodel().pendingTasks.removeAll();
            var ReferenceID = data.VesselAgentChangeID();
            var WorkflowInstanceID = data.WorkflowInstanceId();

            if (ReferenceID > 0 && WorkflowInstanceID > 0 && viewDetail != '') {
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
                                           pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                           pendingtaskaction.TaskDescription(val.TaskDescription);
                                           pendingtaskaction.HasRemarks(val.HasRemarks);
                                           self.agentchangerequestmodel().pendingTasks.push(pendingtaskaction);
                                       });
                                   });
            }



        };
        // Edit Change of Agent Request request mode
        self.editData = function (data) {
            self.isVisible(true);
            self.viewMode("Form");
            self.isReset(true);
            self.IsVCNEnable(false);
            self.isSaveVisible(true);
            self.IsEffectiveDateEnable(true);
            self.IsProposedAgentEnable(true);
            self.IsReasonforTransferEnable(true);
            self.isUploadFileVisible(true);
            self.isDelUploadFileVisible(true);
            self.agentchangerequestmodel(data);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.viewModeForTabs('notification1');
            //$('#spnTitile').html("Update Change Of Agent");
        }
        self.LoadVCNActiveDetails = function () {
            //self.viewModelHelper.apiGet('api/VesselAgentChange/GetVCNDetails',//'api/ServiceRequest/GetVCNDetails',
            self.viewModelHelper.apiGet('api/VCNActive',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getVCNDtls);

              });


        }

        self.VesselSelect = function (e) {
            var dataItem = this.dataItem(e.item.index())
            self.agentchangerequestmodel().VesselData(dataItem);
            self.agentchangerequestmodel().VesselName(dataItem.VesselName);
            self.agentchangerequestmodel().VesselType(dataItem.VesselType);
            self.agentchangerequestmodel().VesselTypeName(dataItem.VesselTypeName);
            self.agentchangerequestmodel().ReasonForVisitName(dataItem.ReasonForVisitName);
        }
        // To validate Change of Agent Request details
        self.ValidateForm = function (data) {
            if (ValidateFormValues(data) == false) {
                return;
            }
            else {
                if (self.viewModeForTabs() == "notification1") {
                    GoToTab2(self, data, self.isView());
                }
            }
        }
        //To save Change of Agent Request details
        self.SaveRequest = function (model) {

            model.validationEnabled(true);
            //if (ValidateFormValues(model) == false) {
            //    return;
            //}
            self.VesselagentValidation = ko.observable(model);
            self.VesselagentValidation().errors = ko.validation.group(self.VesselagentValidation());
            var result = true;
            if ($("#selproposedagent").val() == "" || $("#selproposedagent").val() == null) {
                $('#spanselproposedagent').text('Please select the Propsed Agent');
                result = false;
            }
            else {
                $("#spanselproposedagent").text('');
            }
            if ($("#selreasonfortransfer").val() == "" || $("#selreasonfortransfer").val() == null) {
                $('#spanselreasonfortransfer').text('Please select the Reason For Transfer');
                result = false;
            }
            else {
                $("#spanselreasonfortransfer").text('');
            }

            var match = ko.utils.arrayFirst(self.getVCNDtls(), function (item) {
                return item.VCN() === model.VCN();
            });

            if (match == null) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("Please Select Valid VCN", "Change of Agent Request");
                GoToTab1(self, model);
                return;
            }

            if (parseInt(ValidateVCN(model.VCN())) > 0) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("Request for the particular VCN is already done", "Change of Agent Request");
                GoToTab1(self, model);
                return;
            }

            var errors = self.VesselagentValidation().errors().length;
            errors = Validation();
            if (errors == 0 && result == true) {
                if (model.VesselAgentChangeID() == '') {
                    //self.viewModelHelper.apiPost('api/VesselAgentChange/PostVesselAgentChangeData', ko.mapping.toJSON(model),
                    ////self.viewModelHelper.apiPost('api/VesselAgentChange', ko.mapping.toJSON(model),
                    //           function Message(data) {
                    //               toastr.options.closeButton = true;
                    //               toastr.options.positionClass = "toast-top-right";
                    //               toastr.success("Vessel Change Request details saved successfully", "Vessel Change Request");

                    //           });

                    //self.viewModelHelper.apiPost('api/VesselAgentChange/PostVesselAgentChangeData', ko.mapping.toJSON(model), function Message(data) {
                    self.viewModelHelper.apiPost('api/VesselAgentChanges', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Change of Agent Request details saved successfully", "Change of Agent Request");
                        self.LoadVesselAgentChangeList();
                        self.LoadVCNDetails();
                        self.LoadVCNActiveDetails();
                        self.viewMode('List');

                    });

                }
            } else {

                self.VesselagentValidation().errors.showAllMessages();
                //$('#divValidationError').removeClass('display-none');
            }
        }

        self.Initialize();
        //To cancel Change of Agent Request details
        self.CancelData = function (data) {
            if (viewDetail == false) {
                self.viewMode('List');
                self.IsProposedAgentEnable(true);
                self.LoadVesselagentChangesReferences();
                self.agentchangerequestmodel().reset();
                self.agentchangerequestmodel(new IPMSRoot.AgentChangeRequestModel());
                self.SrearchVesselAgentDet(data);
                self.viewModeForTabs('notification1');


            } else { window.location.href = "/Welcome"; }
            //$('#spnTitile').html("Change Of Agent");
        }
        //To reset Change of Agent Request details
        self.ResetData = function (model) {
            model.validationEnabled(false);
            var notify = self.viewModeForTabs();
            // self.VesselagentValidation = ko.observable(model);
            self.agentchangerequestmodel().reset();
            self.agentchangerequestmodel(new IPMSRoot.AgentChangeRequestModel());
            self.viewModeForTabs(notify);
            //  self.VesselagentValidation().errors.showAllMessages(false);
            //$('#divValidationError').removeClass('display-none');
            $("#spanselproposedagent").text('');
            $("#spanselreasonfortransfer").text('');
            GoToTab1(self, model);
        }

        self.GotoTab1 = function (data) {

            if (self.viewModeForTabs() == 'notification1') {
                return;
            }
            GoToTab1(self, data);
        }
        self.GotoTab2 = function (data) {

            if (ValidateFormValues(data) == true) {
                if (self.viewModeForTabs() == 'notification1') {
                    self.viewModeForTabs('notification2');
                    GoToTab2(self, data, self.isView());

                }

            }

            //if (self.viewModeForTabs() == 'notification2') {
            //    return;
            //}

            //GoToTab2(self, data, isView);
        }
        self.GoToPrevTab = function (data) {
            if (self.viewModeForTabs() == 'notification2') {
                GoToTab1(self, data);
            }

        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.agentchangerequestmodel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }
        self.SrearchVesselAgentDet = function (data) {

            var vcn = data.VCNSearch() != undefined && data.VCNSearch() != '' ? data.VCNSearch() : 'ALL';
            var vesselName = data.VesselNameSearch() != undefined && data.VesselNameSearch() != '' ? data.VesselNameSearch() : 'ALL';
            var etafrom = moment(data.ETAFrom()).format('YYYY-MM-DD');
            var etato = moment(data.ETATo()).format('YYYY-MM-DD');

            self.viewModelHelper.apiGet('api/SearchVesselAgentChange/' + vcn + '/' + vesselName + '/' + etafrom + '/' + etato,
              {},
               function (result) {
                   self.vesselagentchangelist(ko.utils.arrayMap(result, function (item) {
                       return new IPMSRoot.AgentChangeRequestModel(item);
                   }));

                   var grid = $("#divVesselagentchangeList").data("kendoGrid");

                   if (self.vesselagentchangelist().length <= 5)
                       grid.dataSource.pageSize(5);
                   else
                       grid.dataSource.pageSize(20);

                   grid.refresh();

               }, null, null, true);
        }

        self.ResetSearchDet = function (data) {
            data.VCNSearch('');
            data.VesselNameSearch('')
            data.ETAFrom(new Date());
            data.ETATo(new Date())

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);
            data.ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            data.ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");

            var grid = $("#divVesselagentchangeList").data("kendoGrid");

            if (self.vesselagentchangelist().length <= 5)
                grid.dataSource.pageSize(5);
            else
                grid.dataSource.pageSize(20);

            grid.refresh();

            self.SrearchVesselAgentDet(data);
        }



        //var uploadedFiles = [];
        //var documentData = [];
        //self.uploadFile = function () {
        //    alert('1');
        //    if ($('#selUploadDocs').get(0).selectedIndex == 0) {
        //        toastr.error("Please select document Type.");
        //        return;
        //    }
        //    else {
        //        alert('2')
        //        $("#spanHWPSfileToUpload").text("");
        //        alert('3')
        //        self.isUploadFileVisible(false);
        //        alert('4')
        //        var documentType = $('#selUploadDocs option:selected').text();
        //        // alert(documentType);
        //        uploadedFiles = self.agentchangerequestmodel().UploadedFiles();
        //        var opmlFile = $('#fileToUpload')[0];
        //        if (opmlFile.files.length > 0) {
        //            for (var i = 0; i < opmlFile.files.length; i++) {
        //                var elem = {};
        //                elem.FileName = opmlFile.files[i].name;
        //                elem.CategoryName = $('#selUploadDocs option:selected').text();
        //                elem.CategoryCode = $('#selUploadDocs option:selected').val();
        //                elem.FileDetails = opmlFile.files[i];
        //                elem.IsAlreadyExists = false
        //                uploadedFiles.push(elem);
        //                self.agentchangerequestmodel().UploadedFiles(uploadedFiles);
        //            }

        //            var formData = new FormData();
        //            $.each(self.agentchangerequestmodel().UploadedFiles(), function (key, val) {
        //                formData.append(val.name, val.FileDetails);
        //            });
        //            var CategoryCode = $('#selUploadDocs option:selected').val();
        //            if (opmlFile.files.length > 0) {
        //                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
        //                    self.Listdocuments = ko.observableArray();
        //                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
        //                        var Adddoc = new IPMSROOT.VesselChangeAgentDocument();
        //                        Adddoc.DocumentID(item.DocumentID);
        //                        Adddoc.FileName(item.FileName);
        //                        Adddoc.DocumentName(documentType);
        //                        self.agentchangerequestmodel().VesselAgentChangeDocuments.push(Adddoc);
        //                    }));
        //                });
        //            }
        //        }
        //        else {
        //            $("#spanHWPSfileToUpload").text('Please select file');
        //            self.isUploadFileVisible(true);
        //        }
        //        self.agentchangerequestmodel().UploadedFiles([]);
        //        return;
        //    }
        //}


        Changeselproposedagent = function () {

            if ($("#selproposedagent").val() == "" || $("#selproposedagent").val() == null) {
                $('#spanselproposedagent').text('Please select the Propsed Agent');

            }
            else {
                $("#spanselproposedagent").text('');
            }
        }
        Changeselreasonfortransfer = function () {

            if ($("#selreasonfortransfer").val() == "" || $("#selreasonfortransfer").val() == null) {
                $('#spanselreasonfortransfer').text('Please select the Reason For Transfer');

            }
            else {
                $("#spanselreasonfortransfer").text('');
            }
        }

        ValidDate = function () {
            self.agentchangerequestmodel().ETATo(self.agentchangerequestmodel().ETAFrom());
        }

        Validation = function () {
            var NoOfErrors = 0;
            //DefaultValidation();


            $('#spanselproposedagent').text('');
            $('#spanselreasonfortransfer').text('');
            if ($("#selproposedagent").val() == "" || $("#selproposedagent").val() == null) {
                $('#spanselproposedagent').text('Please select the Propsed Agent');
                NoOfErrors++;
            }

            if ($("#selreasonfortransfer").val() == "" || $("#selreasonfortransfer").val() == null) {
                $('#spanselreasonfortransfer').text('Please select the Reason For Transfer');
                NoOfErrors++;
            }

            return NoOfErrors;
        }

        var uploadedFiles = [];
        var documentData = [];

        self.uploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {
                $("#spanHWPSfileToUpload").text("");

                self.isUploadFileVisible(false);
                var documentType = $('#selUploadDocs option:selected').text();
                self.agentchangerequestmodel().UploadedFiles([]);
                uploadedFiles = self.agentchangerequestmodel().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.agentchangerequestmodel().VesselAgentChangeDocuments(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
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
                                self.agentchangerequestmodel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The selected file already exist, Please upload another file", "Error");
                            return;
                        }

                    }

                    var formData = new FormData();
                    //$.each(self.agentchangerequestmodel().UploadedFiles(), function (key, val) {
                    $.each(uploadedFiles, function (key, val) {
                        //formData.append(val.name, val.FileDetails);
                        formData.append(val.name, val.FileDetails);
                    });
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();

                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.VesselChangeAgentDocument();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            Adddoc.DocumentName(documentType);
                            self.agentchangerequestmodel().VesselAgentChangeDocuments.push(Adddoc);
                            $("select#selUploadDocs").prop('selectedIndex', 0);
                            //self.agentchangerequestmodel().UploadedFiles.push(Adddoc);

                        }));


                    });
                } else {
                    $("#spanHWPSfileToUpload").text('Please select file');
                    self.isUploadFileVisible(true);

                }
                self.agentchangerequestmodel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        self.viewWorkFlow = function (agentchange) {
            var workflowinstanceId = agentchange.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.agentchangerequestmodel(new IPMSROOT.AgentChangeRequestModel());
                  self.agentchangerequestmodel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.DeleteDocument = function (documentRow) {
            self.agentchangerequestmodel().VesselAgentChangeDocuments.remove(documentRow);
        }
        isAdd = 0;
        index = 1;

    }
    IPMSRoot.AgentChangeRequestViewModel = AgentChangeRequestViewModel;

}(window.IPMSROOT));

function ValidateFormValues(data) {

    var result = true;
    self.AgentchangereqValidation = ko.observable(data);
    self.AgentchangereqValidation().errors = ko.validation.group(self.AgentchangereqValidation());


    var errors = self.AgentchangereqValidation().errors().length;
    if (errors > 0) {
        data.errors.showAllMessages();
        //toastr.warning("You have some form errors. Please check below.");
        var result = true;
        if ($("#selproposedagent").val() == "" || $("#selproposedagent").val() == null) {
            $('#spanselproposedagent').text('Please select the Propsed Agent');
            result = false;

        }
        else {
            $("#spanselproposedagent").text('');
        }
        if ($("#selreasonfortransfer").val() == "" || $("#selreasonfortransfer").val() == null) {
            $('#spanselreasonfortransfer').text('Please select the Reason For Transfer');
            result = false;
        }
        else {
            $("#spanselreasonfortransfer").text('');
        }
        self.AgentchangereqValidation().errors.showAllMessages();
        //$('#divValidationError').removeClass('display-none');
        result = false;
    }
    return result;
}
function GoToTab1(self, data) {
    self.viewModeForTabs('notification1');
    self.isGoBackVisible(false);
    self.isSubmitVisible(false);
    self.isSaveVisible(true);

    //if (self.isView() == 1) {
    //    self.isSaveVisible(false);

    //}


    var index = 0;
    HandleProgressBarAndActiveTab(index);
}
function GoToTab2(self, data, isView) {

    self.viewModeForTabs('notification2');
    //$('#divValidationError').addClass('display-none');
    //self.isSubmitVisible(true);
    self.isSaveVisible(false);
    self.isGoBackVisible(true);

    if (self.isView() == 1) {
        self.isSubmitVisible(false);
    }
    else {
        self.isSubmitVisible(true);
    }
    var index = 1;
    HandleProgressBarAndActiveTab(index);
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
function ValidateVCN(VCN) {
    var resultval = true;
    var self = this;
    self.viewModelHelper = new IPMSROOT.viewModelHelper();
    self.viewModelHelper.apiGet('api/ValidateVCN',
{ vcn: VCN },
    function (result) {
        if (result == 0) {
            resultval = 0;
        }
        else {
            resultval = 1;
        }
    }, null, null, false);

    return resultval;
}


