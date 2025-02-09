﻿toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot) {
    var isView = true;

    var AgentViewModel = function (vcn, viewDetail) {
        var self = this;
        self.AgentsList = ko.observableArray();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.viewMode = ko.observable();
        self.List = ko.observableArray();
        self.isfileToUpload = ko.observable(false);
        self.agentModel = ko.observable();
        self.isAddMode = ko.observable(false);
        self.Agents = ko.observableArray();
        self.IsViewfrmApproval = ko.observable('Y');
        self.agentportDtls = ko.observableArray([]);
        self.AgentApproveVisiable = ko.observable(false);
        self.Approvedports = ko.observableArray([]);

        self.FromDate = ko.observable();
        self.ToDate = ko.observable();

        self.AgentData = ko.observable(new IPMSROOT.AgentModel());

        self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
            self.AgentData().PortData(result);
        }, null, null, false);

        self.viewModelHelper.apiGet('api/Agent/GetDocumentTypes', null, function (result) {
            self.AgentData().DocumentTypes(result);
        }, null, null, false);

        self.LoadNewAgentRegistration = function () {
            if (viewDetail == true) {
                isView = false;
                $('#spnTitle').text('View Agent Details');
                self.AgentData().viewModeForTabs('register1');
                GetAgentDetails(vcn, self, isView, viewDetail);
            }
        }

        //Initializing the page load events
        self.Initialize = function () {

            self.LoadAgents();
            self.LoadNewAgentRegistration();
            self.agentModel(new IPMSROOT.AgentModel());
            if (viewDetail == true) {
                self.AgentData().isView(false);
                self.viewMode('EditView');
                self.AgentData().viewModeForTabs('register1');
            }
            else {
                window.scrollTo(500, 0);
                self.viewMode('List');
            }

        }

        //for edit option in the grid
        self.EditAgent = function (agent) {

            $('#spnTitle').text('Update Agent Details');
            isView = true;
            self.IsViewfrmApproval('N');
            viewDetail = false;
            //self.FromDate(agent.FromDate);
            //self.ToDate(agent.ToDate);

            self.AgentData().AddMode(false);
            self.AgentData().EditMode(true);
            self.AgentData().ViewMode(false);

            GetAgentDetails(agent.AgentID, self, isView, viewDetail);
            self.AgentData().FromDate(agent.FromDate);
            self.AgentData().ToDate(agent.ToDate);
            self.viewMode('EditView');
            self.AgentData().isView(true);
            if (agent.FromDate != '' && agent.ToDate != '' && agent.FromDate != null && agent.ToDate != null)
                self.isAddMode(true);
            else
                self.isAddMode(false);
            $('#btnSaveContinue').html('Next');
            self.AgentData().viewModeForTabs('register1');
            $('#FromDate').attr('disabled', true);
            $('#ToDate').attr('disabled', true);
            $("#FromDate").data('kendoDatePicker').enable(false);
            $("#ToDate").data('kendoDatePicker').enable(false);
            $('#btnGoBack').css('display', 'none');
            $('#btnSubmit').css('display', 'none');
            $('#btnReset').css('display', '');
            $('#btnSaveContinue').css('display', '');
            var index = 0;

            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);

            //var ReferenceID = self.AgentData().AgentID();
            //var WorkflowInstanceID = agent.WorkflowInstanceId() == null ? 0 : agent.WorkflowInstanceId();
            //self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
            //       null,
            //             function (result) {

            //                 ko.utils.arrayForEach(result, function (val) {
            //                     var pendingtaskaction = new IPMSROOT.pendingTask();
            //                     pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
            //                     pendingtaskaction.ReferenceID(val.ReferenceID);
            //                     pendingtaskaction.TaskCode(val.WorkflowTaskCode);
            //                     pendingtaskaction.APIUrl(val.APIUrl);
            //                     pendingtaskaction.TaskName(val.TaskName);
            //                     self.AgentData().pendingTasks.push(pendingtaskaction);
            //                 });
            //             }, null, null, false);


            HandleProgressBarAndActiveTab(index);
            $("#phone").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var phone = $("#phone").data("kendoMaskedTextBox");
            self.AgentData().TelephoneNo1(phone.value());

            $("#faxno").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var faxno = $("#faxno").data("kendoMaskedTextBox");
            self.AgentData().FaxNo(faxno.value());

            $("#CellularNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var cellularNo = $("#CellularNo").data("kendoMaskedTextBox");
            self.AgentData().CellularNo(cellularNo.value());


        }

        //for View option in the grid
        self.ViewAgent = function (agent) {

            $('#spnTitle').text('View Agent Details');
            $('#chkBuisinessAddress').prop("disabled", true);
            isView = false;
            self.AgentData().AddMode(false);
            self.AgentData().EditMode(false);
            self.AgentData().ViewMode(true);

            self.IsViewfrmApproval('N');
            GetAgentDetails(agent.AgentID, self, isView, viewDetail);
            self.AgentData().FromDate(agent.FromDate);
            self.AgentData().ToDate(agent.ToDate);
            self.viewMode('EditView');
            self.AgentData().isView(false);
            if (agent.FromDate != '' && agent.ToDate != '' && agent.FromDate != null && agent.ToDate != null)
                self.isAddMode(true);
            else
                self.isAddMode(false);
            self.AgentData().viewModeForTabs('register1');
            self.AgentData().isGoBackVisible(false);
            self.AgentData().isSubmitVisible(false);
            $('#FromDate').attr('disabled', true);
            $('#ToDate').attr('disabled', true);
            $("#FromDate").data('kendoDatePicker').enable(false);
            $("#ToDate").data('kendoDatePicker').enable(false);
            $('#btnSaveContinue').html('Next');
            $('#btnGoBack').css('display', 'none');
            $('#btnSubmit').css('display', 'none');
            $('#btnReset').css('display', 'none');
            $('#btnSaveContinue').css('display', '');

            var index = 0;
            debugger
            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);

            //var ReferenceID = agent.AgentID == null ? 0 : agent.AgentID;
            //var WorkflowInstanceID = agent.WorkflowInstanceId() == null ? 0 : agent.WorkflowInstanceId();


            //self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
            //     null,
            //           function (result) {
            //               
            //               ko.utils.arrayForEach(result, function (val) {
            //                   var pendingtaskaction = new IPMSROOT.pendingTask();
            //                   pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
            //                   pendingtaskaction.ReferenceID(val.ReferenceID);
            //                   pendingtaskaction.TaskCode(val.WorkflowTaskCode);
            //                   pendingtaskaction.APIUrl(val.APIUrl);
            //                   pendingtaskaction.TaskName(val.TaskName);
            //                   pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
            //                   pendingtaskaction.TaskDescription(val.TaskDescription);
            //                   self.VesselRegisrationDetModel().pendingTasks.push(pendingtaskaction);
            //               });
            //           });




            HandleProgressBarAndActiveTab(index);
            $("#phone").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var phone = $("#phone").data("kendoMaskedTextBox");
            self.AgentData().TelephoneNo1(phone.value());

            $("#faxno").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var faxno = $("#faxno").data("kendoMaskedTextBox");
            self.AgentData().FaxNo(faxno.value());

            $("#CellularNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var cellularNo = $("#CellularNo").data("kendoMaskedTextBox");
            self.AgentData().CellularNo(cellularNo.value());
        }

        //for Delete option in the grid
        self.DeleteAgent = function (agent) {
            InactiveAgent(agent, self);
        }

        //for Loading the agents in the grid
        self.LoadAgents = function () {


            LoadAgents(self);
        }

        //for filling the postal address
        self.FillPostal = function () {
            if ($('#chkBuisinessAddress').is(':checked') == true) {
                //self.AgentData().NumStreet2((self.AgentData().NumberStreet()));
                //self.AgentData().Suburb2((self.AgentData().Suburb()));
                //self.AgentData().TownCity2((self.AgentData().TownCity()));
                //self.AgentData().PostalCode2((self.AgentData().PostalCode()));
                if ($('#txtBusinessstreet').val() != '')
                    self.AgentData().NumStreet2($('#txtBusinessstreet').val());

                if ($('#txtBusinesssuburb').val() != '')
                    self.AgentData().Suburb2($('#txtBusinesssuburb').val());

                if ($('#txtBusinesstowncity').val() != '')
                    self.AgentData().TownCity2($('#txtBusinesstowncity').val());

                if ($('#txtBusinesscode').val() != '')
                    self.AgentData().PostalCode2($('#txtBusinesscode').val());

                $('#fldPostalAddress').find('input').attr('readonly', true)
            }
            else {
                self.AgentData().NumStreet2('');
                self.AgentData().Suburb2('');
                self.AgentData().TownCity2('');
                self.AgentData().PostalCode2('');
                $('#fldPostalAddress').find('input').attr('readonly', false)
            }
            return true;
        }

        /*Form wizard tab navigation*/
        self.GotoTab1 = function (agentData) {
            if (self.AgentData().viewModeForTabs() == 'register1') {
                return;
            }
            GoToTab1(self, agentData, viewDetail);
        }

        self.GotoTab2 = function (agentData) {

            checkboxes = document.getElementsByName('port');
            $('#btnReset').css('display', 'none');
            var chk = 0;
            var msg = '';
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].checked) {
                    chk = 1;
                }
            }
            if (chk == 1) {

                if (ValidateFormValues(agentData) == true) {

                    if (self.AgentData().viewModeForTabs() == 'register1') {
                        //self.AgentData().viewModeForTabs('register2');
                        //GoToTab2(self, agentData);
                        if (self.AgentData().AgentID() <= 0 || self.AgentData().AgentID() == undefined) {
                            var result_Reg = CheckForRegCombinationExistance_tab(self, agentData);
                            var result_INTAX = CheckForTaxCombinationExistance_tab(self, agentData);
                            var result_VAT = CheckForVatCombinationExistance_tab(self, agentData);
                            if (result_VAT == 0 && result_INTAX == 0 && result_Reg == 0) {
                                self.AgentData().viewModeForTabs('register2');
                                GoToTab2(self, agentData);

                            } else {
                                GoToTab1(self, agentData, viewDetail);
                                if (result_Reg == 1) {
                                    self.AgentData().RegistrationNumber('');
                                    msg += "<p>Entered Reg no. already exists !.</p>";
                                    //toastr.error("Entered Reg no. already exists !.", "Error");
                                } else if (result_INTAX == 1) {
                                    self.AgentData().IncomeTaxNumber('');
                                    msg += "<p>Entered Tax no. already exists !.<p>";
                                    //toastr.error("Entered Tax no. already exists !.", "Error");
                                } else if (result_VAT == 1) {
                                    self.AgentData().VATNumber('');
                                    msg += "<p>Entered Vat no. already exists !.<p>";
                                    //toastr.error("Entered Vat no. already exists !.", "Error");
                                }

                                toastr.error(msg, "Error");
                            }
                        }
                        else {
                            GoToTab2(self, agentData);
                            self.AgentData().viewModeForTabs('register2');
                        }

                    }
                    else {
                        GoToTab2(self, agentData);
                        self.AgentData().viewModeForTabs('register2');
                    }
                }


            }
            else {
                GoToTab1(self, agentData, viewDetail);
                toastr.error("Please Select at least One port.");
                return;

            }
        }

        self.GoToPrevTab = function (agentData) {
            if (self.AgentData().viewModeForTabs() == 'register3') {
                GoToTab2(self, agentData);
            }
            else if (self.AgentData().viewModeForTabs() == 'register2') {
                GoToTab1(self, agentData, viewDetail);
            }
        }

        self.GotoAgentsList = function (agentData) {

            var flag = false;

            $('#divAgentFields').find("input[type=text]").each(function () {
                if ($(this).val().length != 0) {
                    flag = true;
                }
            });
            $('#divAgentFields').find("input[type=radio]:checked").each(function () {
                if ($(this).val() === 'Y') {
                    flag = true;
                }
            });
            $('#divPortList input[name="port"]:checked').each(function () {
                if ($(this).val() != '') {
                    flag = true;
                }
            });

            if ($("#DDBBBEEStatus option:selected").text() != '0')
                flag = true;

            if (self.IsViewfrmApproval() == 'N') {
                if (flag === true && self.AgentData().ViewMode() != true) {
                    //if (!confirm('Are sure you want to cancel ? if any unsaved changes will lose that you have made '))
                    //{ return false; } else {
                    //    $("#form").trigger('reset');
                    //    self.viewMode('List');
                    //    $('#spnTitle').text('Agent Master');
                    //}

                    $.confirm({
                        'title': 'Agent Request confirmation',
                        'message': 'Are sure you want to cancel ? if any unsaved changes will lose that you have made ',
                        'buttons': {
                            'Yes': {
                                'class': 'blue',
                                'action': function () {

                                    $("#form").trigger('reset')
                                    self.viewMode('List');
                                    window.scrollTo(500, 0);
                                    $('#spnTitle').text('Agent Master');
                                }
                            },
                            'No': {
                                'class': 'gray',
                                'action': function () {
                                }
                            }
                        }
                    });
                } else {
                    $('#spnTitle').text('Agent Master');
                    window.scrollTo(500, 0);
                    self.viewMode('List');
                }
            } else {

                window.location.href = "/Dashboard";
            }
        }

        self.ValidateForm = function (agentData) {
            var msg = "";

            if (ValidateFormValues(agentData) == false) {
                return;
            }

            if (self.AgentData().viewModeForTabs() == 'register1') {

                if ((self.AgentData().AgentID() <= 0 || self.AgentData().AgentID() == undefined)) {


                    var result_Reg = CheckForRegCombinationExistance_tab(self, agentData);
                    var result_INTAX = CheckForTaxCombinationExistance_tab(self, agentData);
                    var result_VAT = CheckForVatCombinationExistance_tab(self, agentData);

                    if (result_VAT == 0 && result_INTAX == 0 && result_Reg == 0) {
                        self.AgentData().viewModeForTabs('register2');
                        GoToTab2(self, agentData);
                    } else {
                        GoToTab1(self, agentData, viewDetail);
                        if (result_Reg == 1) {
                            self.AgentData().RegistrationNumber('');
                            msg += "<p>Entered Reg no. already exists !.</p>";
                            //toastr.error("Entered Reg no. already exists !.", "Error");
                        } else if (result_INTAX == 1) {
                            self.AgentData().IncomeTaxNumber('');
                            msg += "<p>Entered Tax no. already exists !.<p>";
                            //toastr.error("Entered Tax no. already exists !.", "Error");
                        } else if (result_VAT == 1) {
                            self.AgentData().VATNumber('');
                            msg += "<p>Entered Vat no. already exists !.<p>";
                            //toastr.error("Entered Vat no. already exists !.", "Error");
                        }

                        toastr.error(msg, "Error");
                    }
                }
                else
                    GoToTab2(self, agentData);
            }
        }

        //To saving the agent details
        self.AgentData().errors = ko.validation.group(self.AgentData());

        self.SaveAgent = function (agentData) {

            if (ValidateFormValues(agentData) == false) {
                return;
            }

            //if (ValidateDocuments(agentData) == false) {
            //    return;
            //}

            /*declaring applPortWorkFlowObjArry object to fill list of Appl_Port_Workflow*/
            var applPortWorkFlowObjArry = new Array();
            $('#divPortList input[name="port"]:checked').each(function () {
                var applPortWorkFlowObj = new ApplicantWorkFlow(self.AgentData().AgentID(), this.value, '', 1, 1, self.AgentData().CreatedDate(), self.AgentData().CreatedBy(), "A");
                applPortWorkFlowObjArry.push(applPortWorkFlowObj);
            });
            self.AgentData().AgentPorts(applPortWorkFlowObjArry);
            /*filling applicant address object*/
            var applicantAddressObj = new ApplicantAddress(
                    self.AgentData().BusinessAddressID(),
                    self.AgentData().NumberStreet(),
                    self.AgentData().Suburb(),
                    self.AgentData().TownCity(),
                    self.AgentData().PostalCode(),
                    'ADPE',
                    'A',
                    self.AgentData().CreatedBy(),
                     self.AgentData().CreatedDate()
                );
            self.AgentData().Address(applicantAddressObj);
            if (self.AgentData().NumStreet2() != '' || self.AgentData().Suburb2() != '') {
                var applicantAddressObjForAddrType2 = new ApplicantAddress(
                self.AgentData().PostalAddressID(),
                self.AgentData().NumStreet2(),
                self.AgentData().Suburb2(),
                self.AgentData().TownCity2(),
                self.AgentData().PostalCode2(),
                'ADTE',
                'A',
                 self.AgentData().CreatedBy(),
                 self.AgentData().CreatedDate()
                );
            }
            self.AgentData().Address1(applicantAddressObjForAddrType2);//pushing to applicant address observable

            /*filling applicant autherization contact details object*/

            var applicantAuthorizedContactDetails = new ApplicantContactPersonDetails(
                self.AgentData().AuthorizedContactPersonID(),
                'EMP',//self.AgentData().AuthorizedContactPersonType(),
                self.AgentData().FirstName(),
                self.AgentData().SurName(),
                self.AgentData().IdentityNo(),
                self.AgentData().Designation(),
                self.AgentData().CellularNo(),
                self.AgentData().EmailID(),
                self.AgentData().CreatedBy(),
                 self.AgentData().CreatedDate(),
                "A"
                );
            self.AgentData().AuthorizedContactPerson(applicantAuthorizedContactDetails);
            /*Filling document object*/
            var agentDocuments = [];
            //$.each(self.AgentData().UploadedFiles(), function (key, val) {


            $.each(self.AgentData().AgentDocumentsVO(), function (key, val) {
                //if (val.IsAlreadyExists == false) {
                agentDocuments.push(new AgentDocument(0, val.DocumentID(), val.CreatedBy(), new Document(val.DocumentID(), val.CategoryCode(), val.CategoryCode(), val.FileName(), val.FileName(), val.CreatedBy(), '', val.FileType())));
                //}
            });
            //self.AgentData().AgentDocuments(agentDocuments);
            self.AgentData().AgentDocuments(agentDocuments);
            /*end*/
            self.viewModelHelper.apiPost('api/Agent/PostAgentRegistration',
                     ko.mapping.toJSON(agentData),
                  function (applicantId) {
                      if (applicantId > 0) {
                          ResetAllFields(agentData);
                          UploadDocuments(self, applicantId);
                          ResetErrorMessages(self.AgentData());
                          $('#spnTitle').text('Agent Master');
                          if (self.isAddMode() == false)
                              toastr.success("Agent Details saved successfully.", "Agent Registration");
                          else
                              toastr.success("Agent Details updated successfully.", "Agent Registration");
                          LoadAgents(self);
                          self.viewMode('List');
                          window.scrollTo(500, 0);
                      }
                      else {
                          toastr.error("An error occured. Please try again later!", "Error");
                      }
                  });

        }

        var uploadedFiles = [];
        var documentData = [];

        self.uploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } {
                $("#spanHWPSfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                self.AgentData().UploadedFiles([]);
                uploadedFiles = self.AgentData().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {

                        var match = ko.utils.arrayFirst(self.AgentData().AgentDocumentsVO(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {

                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                elem.CategoryName = $('#selUploadDocs option:selected').text();
                                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                self.AgentData().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The selected file already exist, Please upload another file", "Error");
                            return;
                        }

                    }

                    var formData = new FormData();
                    //$.each(self.AgentData().UploadedFiles(), function (key, val) {
                    $.each(uploadedFiles, function (key, val) {
                        //formData.append(val.name, val.FileDetails);
                        formData.append(val.name, val.FileDetails);
                    });
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();

                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.AgentDocuments();
                            //Adddoc.Data(item.Data);
                            Adddoc.DocumentName(item.DocumentName);
                            Adddoc.DocumentType(item.DocumentType);
                            Adddoc.DocumentPath(item.DocumentPath);
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            Adddoc.CategoryName(CategoryName);
                            Adddoc.CategoryCode(CategoryCode);
                            Adddoc.CreatedBy(item.CreatedBy);
                            Adddoc.CreatedBy(item.CreatedDate);
                            Adddoc.ModifiedBy(item.ModifiedBy);
                            Adddoc.ModifiedDate(item.ModifiedDate);
                            Adddoc.isView(isView);
                            Adddoc.FileType(item.FileType);
                            //    //Adddoc.DocumentName(documentType);
                            //self.AgentData().AgentDocuments.push(Adddoc);
                            self.AgentData().AgentDocumentsVO.push(Adddoc);
                            $("select#selUploadDocs").prop('selectedIndex', 0);
                            //self.AgentData().UploadedFiles.push(Adddoc);

                        }));


                    });
                } else {
                    $("#spanHWPSfileToUpload").text('Please select file');
                    self.isfileToUpload(true);

                }
                self.AgentData().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        self.DeleteDocument = function (documentRow) {

            self.AgentData().AgentDocumentsVO.remove(documentRow);
        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.AgentData());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.addAgent = function () {



            $('#spnTitle').text('Add Agent Details');
            self.AgentData(new IPMSROOT.AgentModel());
            self.isAddMode(false);
            self.IsViewfrmApproval('N');
            self.viewMode('EditView');
            self.AgentData().viewModeForTabs('register1');
            self.AgentData().AddMode(true);
            self.AgentData().EditMode(false);
            self.AgentData().ViewMode(false);
            $('#btnGoBack').css('display', 'none');
            $('#btnSubmit').css('display', 'none');
            $('#btnSaveContinue').css('display', '');
            $('#btnReset').css('display', '');
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts',
                  null,
                function (result) {
                    self.AgentData().PortData(result);
                }, null, null, false);
            self.viewModelHelper.apiGet('api/Agent/GetDocumentTypes',
                            null,
                          function (result) {
                              self.AgentData().DocumentTypes(result);
                          }, null, null, false);

            $("#phone").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var phone = $("#phone").data("kendoMaskedTextBox");
            self.AgentData().TelephoneNo1(phone.value());

            $("#faxno").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var faxno = $("#faxno").data("kendoMaskedTextBox");
            self.AgentData().FaxNo(faxno.value());

            $("#CellularNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var cellularNo = $("#CellularNo").data("kendoMaskedTextBox");
            self.AgentData().CellularNo(cellularNo.value());

        }

        // To Reset functionality
        self.ResetAgent = function (model) {
            var flag = false;

            $('#divAgentFields').find("input[type=text]").each(function () {
                if ($(this).val().length != 0) {
                    flag = true;
                }
            });
            $('#divAgentFields').find("input[type=radio]:checked").each(function () {
                if ($(this).val() === 'Y') {
                    flag = true;
                }
            });
            $('#divPortList input[name="port"]:checked').each(function () {
                if ($(this).val() != '') {
                    flag = true;
                }
            });

            if ($("#DDBBBEEStatus option:selected").text() != '0')
                flag = true;

            if (flag === true) {
                //if (confirm('Are sure you want to reset ? if any unsaved changes will lose that you have made ')) {
                //    self.AgentData().reset();
                //    ko.validation.reset();
                //    // model.validationEnabled(false);
                //    // self.agentModel().reset();
                //    //  self.ENValidation().errors.showAllMessages(false);
                //    //model.AgentDocumentsVO.removeAll();
                //    $('#divPortList input[name="port"]:checked').attr('checked', false);
                //    $('#chkAllport').attr('checked', false);
                //    ResetErrorMessages(self.AgentData());

                //}
                //else { $(this).dialog("close"); }

                $.confirm({
                    'title': 'Agent request confirmation',
                    'message': 'Are sure you want to reset ? if any unsaved changes will lose that you have made.',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {

                                self.AgentData().reset();
                                ko.validation.reset();

                                $('#divPortList input[name="port"]:checked').attr('checked', false);
                                $('#chkAllport').attr('checked', false);
                                ResetErrorMessages(self.AgentData());

                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                            }
                        }
                    }
                });

            } else {

                $('#divAgentFields').find(".validationError").each(function () {
                    if ($(this).text().length != 0) {
                        $(this).css("display", "none");
                    }
                });
            }

        }

        self.LoadGetportdetails = function (data) {

            self.viewModelHelper.apiGet('api/GetAgentAccountport', { Agent: data.AgentID },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.Approvedports);
                });

        }

        self.LoadAgentAccountdetails = function (data) {

            self.viewModelHelper.apiGet('api/GetAgentAccountdetails', { Agent: data.AgentID },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.AgentData().AgentAccountVO);
                });

        }

        self.cancelAgentAccounts = function (data) {

            self.viewMode('List');
            LoadAgents();

        }

        self.AddAgentAccounts = function (data) {

            if (data.AgentAccountVO().length > 0) {
                if ($('#AccountPortcode').get(0).selectedIndex == 0) {
                    self.viewMode('List');
                    window.scrollTo(500, 0);
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please select Port.");
                    return;
                }
                if ($("#AccountNo").val() == null || $("#AccountNo").val() == '' || $("#AccountNo").val() == undefined) {
                    self.viewMode('List');
                    window.scrollTo(500, 0);
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Enter Account number");
                    return;
                }

                self.viewModelHelper.apiPost('api/Agent/PostAgentAccountDetails',
            ko.mapping.toJSON(data),
              function (result) {

                  self.viewMode('List');
                  window.scrollTo(500, 0);
                  self.LoadAgents();
                  toastr.options.closeButton = true;
                  toastr.options.positionClass = "toast-top-right";
                  toastr.success("Agent Account details Submitted successfully", "Agent Master");

              }, null, null, false);
            }
            else {
                self.viewMode('List');
                window.scrollTo(500, 0);
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please fill Account details ", "Agent Master");
            }


        }

        self.AddNewRowtotable = function (model) {
            //self.pilotexemptionrequestModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());


            if (model.AgentAccountVO().length > 0) {
                var ManError = "Y";
                $.map(model.AgentAccountVO, function (item) {
                    var CommoditiesListC = item;
                    if (CommoditiesListC != null)
                        ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                            if (CommodChk !== undefined) {
                                if (CommodChk.PortCode() == "" || CommodChk.AccountNo() == "" || CommodChk.RecordStatus() == "") {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("Please enter Agent Accounts Details", "Agent Master");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y")
                    self.AgentData().AgentAccountVO.push(new IPMSROOT.AgentAccount());
            }
            else {
                self.AgentData().AgentAccountVO.push(new IPMSROOT.AgentAccount());
            }

        }

        self.AddAgentAccountDetails = function (data) {


            self.AgentData().AgentID(data.AgentID);
            self.LoadGetportdetails(data);
            self.LoadAgentAccountdetails(data);
            self.viewMode('PopUp');

        }


        //self.SelectAll = ko.computed({
        //    read: function () {

        //        var item = ko.utils.arrayFirst(self.AgentData().PortData(), function (item) {
        //            
        //            return !item.Selected();
        //        });
        //        return item == null;
        //    },
        //    write: function (value) {
        //        ko.utils.arrayForEach(self.AgentData().PortData(), function (port) {
        //            
        //            port.Selected(value);
        //        });
        //    }
        //}).extend({ throttle: 1 });

        /*Navigation ends*/


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }

        //The Income Tax number already exists.
        ValidITNO = function (data, event) {
            var databaseList = ko.toJSON(self.AgentsList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.IncomeTaxNumber.toLowerCase() == formList.IncomeTaxNumber.toLowerCase()) {
                    $('#spanitno').text('Income Tax No. already exists!');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanitno').text('');
            }
        }

        //The VAT number already exists.
        ValidVATNo = function (data, event) {

            var databaseList = ko.toJSON(self.AgentsList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.VATNumber.toLowerCase() == formList.VATNumber.toLowerCase()) {
                    $('#spanvatno').text('VAT No. already exists!');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanvatno').text('');
            }
        }

        //The REgnumber already exists.
        ValidRegNo = function (data, event) {
            var databaseList = ko.toJSON(self.AgentsList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.RegistrationNumber.toLowerCase() == formList.RegistrationNumber.toLowerCase()) {
                    $('#spanregNo').text('Registration No. already exists!');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanregNo').text('');
            }
        }

        self.Initialize();
    }

    IPMSRoot.AgentViewModel = AgentViewModel;

}(window.IPMSROOT));

//To Get Date Time
function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}
var keynum;
var keychar;
var charcheck;
//To validate alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\?\b ]+$/;
    return charcheck.test(keychar);
}

//To validate numeric
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9\b]/;
    return charcheck.test(keychar);
}

//To validate alphabet with spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z\b ]*$/;
    return charcheck.test(keychar);
}

////To get agetnt Details of the agent registration
function GetAgentDetails(AgentID, model, isView, viewDetail) {
    if (viewDetail == true) {
        isView = false;
        model.viewModelHelper.apiGet('api/Agent/GetzAgent', { vcn: AgentID },
          function (result) {

              FillAgentDetailsForUpdation(result, model, isView, viewDetail);

              $("#phone").kendoMaskedTextBox({
                  mask: "(000)000-000-0000"
              });
              var phone = $("#phone").data("kendoMaskedTextBox");

              $("#faxno").kendoMaskedTextBox({
                  mask: "(000)000-000-0000"
              });
              var faxno = $("#faxno").data("kendoMaskedTextBox");

              $("#CellularNo").kendoMaskedTextBox({
                  mask: "(000)000-000-0000"
              });
              var cellularNo = $("#CellularNo").data("kendoMaskedTextBox");

              var ReferenceID = model.AgentData().AgentID();
              var WorkflowInstanceID = model.AgentData().WorkflowInstanceId() == null ? 0 : model.AgentData().WorkflowInstanceId();
              model.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
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
                                           model.AgentData().pendingTasks.push(pendingtaskaction);
                                       });
                                   });
          });



    }
    else {

        model.viewModelHelper.apiGet('api/Agent/GetAgent', { id: AgentID },
                 function (result) {
                     FillAgentDetailsForUpdation(result, model, isView, viewDetail);
                 }, null, null, false);
    }
}

//Handle the Tabe clicks
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

//validating the form values of the agent registration
function ValidateFormValues(agentData) {

    var result = true;
    var count = $('#divPortList input[name="port"]:checked').length;

    if (count == 0) {
        toastr.error("Please select at least one port.", "Error");
        result = false;
    }

    var errors = ko.validation.group(agentData);
    var error1 = agentData.errors().length;


    //if (errors.length > 0) {
    //    //  agentData.errors.showAllMessages();
    //    toastr.warning("You have some form errors. Please check below.");
    //    result = false;
    //}

    if (agentData.errors().length > 0) {
        $('#divAgentFields').find(".validationError").each(function () {
            if ($(this).text().length != 0) {
                $(this).css("display", "");
            }
        });
        agentData.errors.showAllMessages();
        toastr.error("Please fill the fields", "Error");
        result = false;
    }
    if (agentData.viewModeForTabs() == 'register2') {
        if (agentData.AgentDocumentsVO().length == 0) {
            //toastr.error("Please upload documents", "Error");

            result = ValidateDocuments(agentData);
        }
        else {
            if (ValidateDocuments(agentData) == false) {
                result = false;
            }
        }
    }
    //if (result == false) {
    //    $('#ulTabs li:first a').click();
    //}
    //  result = false;
    return result;

}

function ValidateDocuments(agentData) {

    var result = true;

    var cntforSARSTaxClearance = 0;
    var cntforSAASOA = 0;
    var cntforQualifyBBBEECodes = 0;
    var cntforBBBEEStatus = 0;
    var cntforVerifyBBBEEStatus = 0;
    var msg = "<p>Please provide below list of document's :</p>";
    var documents = 0;
    var seletedcatCode = $('#selUploadDocs option:selected').val();
    if (agentData.SARSTaxClearance() == 'Y') {
        $.each(agentData.AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC3') {
                cntforSARSTaxClearance = 1;
            }
        });

        if (cntforSARSTaxClearance == 0) {
            msg = msg + "<p>Valid SARS tax clearance certificate as annexure 1C</p>";
            //toastr.warning("if you check SARS Tax Clearance Certified? ' as Yes,  Please  provide an annexure 1C document");
            //agentData.AgentDocumentsVO.removeAll();
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }

    }
    if (agentData.SAASOA() == 'Y') {
        $.each(agentData.AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC4') {
                cntforSAASOA = 1;
            }
        });

        if (cntforSAASOA == 0) {
            msg = msg + "<p>>Proof of current membership copy as annexure 1D</p>"
            //toastr.warning("if you check 'Are you a member of SAASOA? ' as Yes,  Please  provide an annexure 1D document");
            //  agentData.AgentDocumentsVO.removeAll();
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }
    }
    if (agentData.QualifyBBBEECodes() == 'Y') {
        $.each(agentData.AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC5') {
                cntforQualifyBBBEECodes = 1;
            }
        });

        if (cntforQualifyBBBEECodes == 0) {
            msg = msg + "<p>Auditors certificate/accounting officers certificate/verifying agencies certificate confirming that applicant qualifying as exempted micro enterprise as annexure 2A</p>"
            //toastr.warning("if you check  'Do you qualify asMicro-enterprise exempt in terms of BBBEE codes of good practice? ' as Yes,  Please  provide an annexure 2A document");
            // agentData.AgentDocumentsVO.removeAll();
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;

        }
    }

    if (agentData.VerifyBBBEEStatus() == 'Y') {
        $.each(agentData.AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC7') {
                cntforVerifyBBBEEStatus = 1;
            }
        });

        if (cntforVerifyBBBEEStatus == 0) {
            msg = msg + "<p>As annexure 2C,Certificate from the verification agency which has been accredited by SANAS</p>"
            //toastr.warning("if you check  'Is your BBBEE status been verified by SANAS Accredited BEE verification agency? ' as Yes,  Please  provide an annexure 2C document");
            // agentData.AgentDocumentsVO.removeAll();
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;

        }
    }

    if (parseInt(agentData.BBBEEStatus()) >= 5 && parseInt(agentData.BBBEEStatus()) <= 9) {
        $.each(agentData.AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC6') {
                cntforBBBEEStatus = 1;
            }
        });

        if (cntforBBBEEStatus == 0) {
            msg = msg + "<p>An annexure 2B that the applicant will reach at least the level of 4 within 18 months from the date of registration</p>"
            //toastr.warning("If you selected BBBEE between 5 t0 9,  Please  provide an annexure 2B document.");
            // agentData.AgentDocumentsVO.removeAll();
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;

        }
    }

    var documents1 = 0; var documents2 = 0

    $.each(agentData.AgentDocumentsVO(), function (key, val) {
        if (val.CategoryCode() == 'DOC1') {
            documents1 = 1;
        }
        if (val.CategoryCode() == 'DOC2') {
            documents2 = 1;
        }
    });

    if ((agentData.AgentDocumentsVO().length == 0) || (documents1 == 0 && documents2 == 0)) {
        msg = msg + "<p>1. Copy of Resolution authorizing the person declared in authorized person section as Annexure 1B </p> 2. Copy Of Company or Close Corporation Registration Certificate As annexure 1A</p>"
        //toastr.warning("<p>Please provide below list of document's </p> <p>1. Copy of Resolution Authorizing the Person Declared in Authorized Person Section as Annexure 1B </p> 2. Copy Of Company 	or Close Corporation Registration Certificate As annexure 1A");
        result = false;
    } else if (parseInt(documents1) == 0) {
        msg = msg + "<p>Copy of Resolution authorizing the person declared in authorized person section as Annexure 1B </p>"
        //toastr.warning("Please provide below  document's <p> 1. Copy of Resolution Authorizing the Person Declared in Authorized Person Section as Annexure 1B </p> ");
        result = false;
    }
    else if (parseInt(documents2) == 0) {
        msg = msg + "<p>  Copy Of Company 	or Close Corporation Registration Certificate As annexure 1A</p>"
        //toastr.warning("Please provide below  document's <p> 1. Copy Of Company 	or Close Corporation Registration Certificate As annexure 1A</p>");
        result = false;
    }

    if (result == false) {
        toastr.warning(msg);
    }

    return result;
}

//Reusable ApplicantAddress Object
function ApplicantAddress(AddressID, NumberStreet, Suburb, TownCity, PostalCode, AddressType, RecordStatus, CreatedBy, CreatedDate) {
    this.AddressID = AddressID;
    this.NumberStreet = NumberStreet;
    this.Suburb = Suburb;
    this.TownCity = TownCity;
    this.PostalCode = PostalCode;
    this.AddressType = AddressType;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
}

//Reusable ApplicantContactPersonDetails Object
function ApplicantContactPersonDetails(AuthorizedContactPersonID, AuthorizedContactPersonType, FirstName, SurName, IdentityNo, Designation, CellularNo, EmailID, CreatedBy, CreatedDate, RecordStatus) {
    this.AuthorizedContactPersonID = AuthorizedContactPersonID;
    this.AuthorizedContactPersonType = AuthorizedContactPersonType;
    this.FirstName = FirstName;
    this.SurName = SurName;
    this.IdentityNo = IdentityNo;
    this.Designation = Designation;
    this.CellularNo = CellularNo;
    this.EmailID = EmailID;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
    this.RecordStatus = RecordStatus;
}

//Reusable ApplicantWorkFlow Object
function ApplicantWorkFlow(AgentID, PortCode, WFStatus, VerifiedBy, ApprovedBy, CreatedDate, CreatedBy, RecordStatus) {
    this.AgentID = AgentID;
    this.PortCode = PortCode;
    this.WFStatus = WFStatus;
    this.VerifiedBy = VerifiedBy;
    this.ApprovedBy = ApprovedBy;
    this.CreatedDate = CreatedDate;
    this.CreatedBy = CreatedBy;
    this.RecordStatus = RecordStatus;
}

//Reusable Documents Object
function Document(DocumentID, DocumentType, DocumentName, DocumentPath, FileName, CreatedBy, Data, FileType) {
    this.Data = Data;
    this.DocumentID = DocumentID;
    this.DocumentType = DocumentType;
    this.DocumentName = DocumentName;
    this.DocumentPath = DocumentPath;
    this.FileName = FileName;
    this.FileType = FileType;
    this.CreatedBy = CreatedBy;
}

//Reusable AgentDocument Object
function AgentDocument(AgentID, DocumentID, CreatedBy, Document) {
    this.AgentID = AgentID;
    this.DocumentID = DocumentID;
    this.CreatedBy = CreatedBy;
    this.Document = Document;
}

//To reset the all fields in the Data
function ResetAllFields(agentData) {
    agentData.RegisteredName('');
    agentData.TradingName('');
    agentData.RegistrationNumber('');
    agentData.VATNumber('');
    agentData.IncomeTaxNumber('');
    agentData.SkillsDevLevyNumber('');
    agentData.SARSTaxClearance('N');
    agentData.SAASOA('N');
    agentData.QualifyBBBEECodes('N');
    agentData.BBBEEStatus(0);
    agentData.VerifyBBBEEStatus('N');
    agentData.RecordStatus('A');

    agentData.NumberStreet('');
    agentData.Suburb('');
    agentData.TownCity('');
    agentData.PostalCode('');
    agentData.TelephoneNo1('');
    agentData.FaxNo('');

    agentData.NumStreet2('');
    agentData.Suburb2('');
    agentData.TownCity2('');
    agentData.PostalCode2('');

    agentData.FirstName('');
    agentData.SurName('');
    agentData.IdentityNo('');
    agentData.Designation('');
    agentData.CellularNo('');
    agentData.EmailID('');

    //agentData.FromDate('');
    //agentData.ToDate('');

    agentData.AgentPort([]);
    agentData.Address();
    agentData.Address1();
    agentData.AuthorizedContactPerson([]);
    $('#divPortList input[name="port"]:checked').attr('checked', false);
    $('#chkSelectAll').attr('checked', false);


}

//To reset error messages
function ResetErrorMessages(agentData) {
    agentData.RegisteredName.isModified(false);
    agentData.TradingName.isModified(false);
    agentData.RegistrationNumber.isModified(false);
    agentData.VATNumber.isModified(false);
    agentData.IncomeTaxNumber.isModified(false);
    agentData.SkillsDevLevyNumber.isModified(false);

    agentData.NumberStreet.isModified(false);
    agentData.Suburb.isModified(false);
    agentData.TownCity.isModified(false);
    agentData.PostalCode.isModified(false);
    agentData.TelephoneNo1.isModified(false);
    agentData.FaxNo.isModified(false);

    agentData.NumStreet2.isModified(false);
    agentData.Suburb2.isModified(false);
    agentData.TownCity2.isModified(false);
    agentData.PostalCode2.isModified(false);

    agentData.FirstName.isModified(false);
    agentData.SurName.isModified(false);
    agentData.IdentityNo.isModified(false);
    agentData.Designation.isModified(false);
    agentData.CellularNo.isModified(false);
    agentData.EmailID.isModified(false);



    //agentData.FromDate.isModified(false);
    //agentData.ToDate.isModified(false);
}

//Filling the Agent registration details for the edit or view
function FillAgentDetailsForUpdation(agentData, vm, isView, viewDetail) {
    ResetAllFields(vm.AgentData());
    if (agentData != null) {
        vm.AgentData().AgentID(agentData.AgentID);
        vm.AgentData().RegisteredName(agentData.RegisteredName);
        vm.AgentData().RegistrationNumber(agentData.RegistrationNumber);
        vm.AgentData().TradingName(agentData.TradingName);
        vm.AgentData().VATNumber(agentData.VATNumber);
        vm.AgentData().IncomeTaxNumber(agentData.IncomeTaxNumber);
        vm.AgentData().SkillsDevLevyNumber(agentData.SkillsDevLevyNumber);
        vm.AgentData().SARSTaxClearance(agentData.SARSTaxClearance);
        vm.AgentData().SAASOA(agentData.SAASOA);
        vm.AgentData().QualifyBBBEECodes(agentData.QualifyBBBEECodes);

        vm.AgentData().BBBEEStatuses = ko.observableArray([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);


        vm.AgentData().BBBEEStatus(agentData.BBBEEStatus);

        vm.AgentData().VerifyBBBEEStatus(agentData.VerifyBBBEEStatus);
        vm.AgentData().RecordStatus(agentData.RecordStatus);
        vm.AgentData().CreatedBy(agentData.CreatedBy);
        vm.AgentData().CreatedDate(agentData.CreatedDate);
        vm.AgentData().ModifiedBy(agentData.ModifiedBy);
        vm.AgentData().ModifiedDate(agentData.ModifiedDate);

        vm.AgentData().BusinessAddressID(agentData.BusinessAddressID);
        vm.AgentData().AddressType(agentData.Address.AddressType);
        vm.AgentData().NumberStreet(agentData.Address.NumberStreet);
        vm.AgentData().Suburb(agentData.Address.Suburb);
        vm.AgentData().TownCity(agentData.Address.TownCity);
        vm.AgentData().PostalCode(agentData.Address.PostalCode);



        vm.AgentData().TelephoneNo1(agentData.TelephoneNo1);
        vm.AgentData().FaxNo(agentData.FaxNo);
        vm.AgentData().AnonymousUserYn(agentData.AnonymousUserYn);
        //if (agentData.AgentID == 0) {
        //    vm.AgentData().FromDate(agentData.FromDate);
        //    vm.AgentData().ToDate(agentData.ToDate);
        //} {
        //    vm.AgentData().FromDate(vm.AgentData().FromDate);
        //    vm.AgentData().ToDate(vm.AgentData().ToDate);
        //}

        vm.AgentData().ReferenceNo(agentData.ReferenceNo);
        vm.AgentData().WorkflowInstanceId(agentData.WorkflowInstanceId);
        vm.AgentData().FromDate(agentData.FromDate);
        vm.AgentData().ToDate(agentData.ToDate);
        if (agentData.Address1 != null) {
            vm.AgentData().PostalAddressID(agentData.PostalAddressID);
            vm.AgentData().AddressType1(agentData.AddressType1);
            vm.AgentData().NumStreet2(agentData.Address1.NumberStreet);
            vm.AgentData().Suburb2(agentData.Address1.Suburb);
            vm.AgentData().TownCity2(agentData.Address1.TownCity);
            vm.AgentData().PostalCode2(agentData.Address1.PostalCode == "0" ? "" : agentData.Address1.PostalCode);
        }





        vm.AgentData().AuthorizedContactPersonID(agentData.AuthorizedContactPersonID);
        vm.AgentData().AuthorizedContactPersonType(agentData.AuthorizedContactPerson.AuthorizedContactPersonType);
        vm.AgentData().FirstName(agentData.AuthorizedContactPerson.FirstName);
        vm.AgentData().SurName(agentData.AuthorizedContactPerson.SurName);
        vm.AgentData().IdentityNo(agentData.AuthorizedContactPerson.IdentityNo);
        vm.AgentData().Designation(agentData.AuthorizedContactPerson.Designation);
        vm.AgentData().CellularNo(agentData.AuthorizedContactPerson.CellularNo);
        vm.AgentData().EmailID(agentData.AuthorizedContactPerson.EmailID);



        $.each(agentData.AgentPorts, function (key, value) {

            if (value.WFStatus == 'WFSA') {
                $('#chkPort_' + value.PortCode).attr('checked', true);
                $('#chkPort_' + value.PortCode).prop('disabled', true);
                $('#chkPort_' + value.PortCode).css('outline', '2px solid green')
            }
            else if (value.WFStatus != 'WFSA' && (value.WFStatus != 'REJ' || value.WFStatus != 'WFRE')) {
                $('#chkPort_' + value.PortCode).attr('checked', true);
                $('#chkPort_' + value.PortCode).prop('disabled', true);
                $('#chkPort_' + value.PortCode).css('outline', '2px solid yellow')
            } else if (value.WFStatus == 'REJ' || value.WFStatus == 'WFRE') {
                $('#chkPort_' + value.PortCode).attr('checked', false);
            }
        });
        var cnt = 0;
        $('#divPortList input[name="port"]').each(function () {
            if (this.checked)
                cnt += 1;

        });
       

        if ($('#divPortList input[name="port"]').length == cnt)
            $('#chkAllport').attr('checked', true);

        vm.AgentData().AgentPorts(agentData.AgentPorts);
        if (isView == false) {
            $('#divAgentFields input[type=text],#divAgentFields input[type=radio],#divPortsList input[type=checkbox],#divAgentFields input[type=email],#chkBuisinessAddress,#DDBBBEEStatus').prop('disabled', true);
            vm.AgentData().isSaveVisible(false);
            vm.AgentData().isSubmitVisible(false);
            vm.AgentData().viewModeForTabs('register1');
        }
        else {
            //$('#divPortsList input[type=checkbox]').prop('disabled', true);
            $('#chkAllport').prop('disabled', true);
            $('#divAgentFields input[type=text],#divAgentFields input[type=radio],#divAgentFields input[type=email],#chkBuisinessAddress,#DDBBBEEStatus').prop('disabled', false);
            vm.AgentData().isSaveVisible(true);
            vm.AgentData().isGoBackVisible(false);
            vm.AgentData().isSubmitVisible(false);
        }



        //var uploadedFiles = [];
        //     $.each(agentData.AgentDocuments, function (key, value) {
        //         var elem = {};
        //         elem.FileName = value.Document.DocumentPath;
        //         $.each(vm.AgentData().DocumentTypes(), function (key, val) {
        //             if (val.SubCatCode == value.Document.DocumentType) {
        //                 elem.CategoryName = val.SubCatName;
        //             }
        //         });

        //         elem.DocumentID = value.DocumentID;

        //         elem.CategoryCode = value.Document.DocumentType;
        //         elem.FileDetails = [];
        //         elem.IsAlreadyExists = true;
        //         uploadedFiles.push(elem);
        //         vm.AgentData().UploadedFiles.removeAll();
        //         //vm.AgentData().UploadedFiles(uploadedFiles);

        //         vm.AgentData().AgentDocumentsVO.push(uploadedFiles);
        //     });
        var uploadedFiles = [];
        vm.AgentData().AgentDocumentsVO.removeAll();
        $.each(agentData.AgentDocuments, function (key, value) {
            var elem = {};
            elem.FileName = value.Document.DocumentPath;
            $.each(vm.AgentData().DocumentTypes(), function (key, val) {
                if (val.SubCatCode == value.Document.DocumentType) {
                    elem.CategoryName = val.SubCatName;
                }
            });

            elem.DocumentID = value.DocumentID;

            elem.CategoryCode = value.Document.DocumentType;
            elem.FileDetails = [];
            elem.IsAlreadyExists = true;

            elem.DocumentName = elem.CategoryName;
            elem.DocumentType = value.Document.DocumentType;
            elem.DocumentPath = value.Document.DocumentPath;
            elem.DocumentID = value.DocumentID;
            elem.FileName = value.Document.DocumentPath;
            elem.CategoryName = elem.CategoryName;
            elem.CategoryCode = value.Document.DocumentType;
            elem.CreatedBy = value.CreatedBy;
            elem.CreatedBy = value.CreatedDate;
            elem.ModifiedBy = value.ModifiedBy;
            elem.ModifiedDate = value.ModifiedDate;

            var Adddoc = new IPMSROOT.AgentDocuments();
            //Adddoc.Data(item.Data);
            Adddoc.DocumentName(elem.CategoryName);
            Adddoc.DocumentType(value.Document.DocumentType);
            Adddoc.DocumentPath(value.Document.DocumentPath);
            Adddoc.DocumentID(value.DocumentID);
            Adddoc.FileName(value.Document.DocumentPath);
            Adddoc.CategoryName(elem.CategoryName);
            Adddoc.CategoryCode(value.Document.DocumentType);
            Adddoc.CreatedBy(value.CreatedBy);
            Adddoc.CreatedBy(value.CreatedDate);
            Adddoc.ModifiedBy(value.ModifiedBy);
            Adddoc.ModifiedDate(value.ModifiedDate);
            Adddoc.isView(isView);
            //    //Adddoc.DocumentName(documentType);
            //self.AgentData().AgentDocuments.push(Adddoc);

            vm.AgentData().AgentDocumentsVO.push(Adddoc);





            //uploadedFiles.push(elem);

            ////vm.AgentData().UploadedFiles(uploadedFiles);

            //vm.AgentData().AgentDocumentsVO.push(uploadedFiles);
        });
        //console.log(vm.AgentData().AgentDocumentsVO());

    }
}

//To Inactive Agent Registration 
function InactiveAgent(agent, self) {
    self.viewModelHelper.apiGet('api/Agent/InactiveAgent', { id: agent.AgentID },
             function (result) {
                 if (result == true) {
                     //Bad coding. For time being i have commented. This has to be fixed
                     //self.AgentsList.remove(ko.toJS(agent));
                     LoadAgents(self);
                 }
             }, null, null, false);
}

//Loading the all agent registration records in the agent registration
function LoadAgents(self) {
    self.viewModelHelper.apiGet('api/Agent/GetAgents',
              { status: 'A' },
              function (result) {
                  /*self.AgentsList(ko.utils.arrayMap(result, function (item) {

           
                      return new IPMSRoot.AgentModel(item);
                  }));*/

                  //function (result) {
                  //                         
                  //    $.each(result, function (key, val) {  
                  //        if (val.WorkFlowTask == 'Approve') 
                  //                {
                  //                    
                  //                    self.AgentApproveVisiable(true);
                  //                    self.AgentsList(val);

                  //                }
                  //    });

                  //});

                  self.AgentsList(result);
                  //if (AgentsList().WorkflowTaskCode == 'Approve')
                  //{ AgentApproveVisiable(true); }
              }, null, null, false);
}

function CheckForTaxCombinationExistance_tab(self, agentData) {
    var resultval = 0;
    self.viewModelHelper.apiGet('api/Agent/CheckForTaxCombinationExistance',
    { incTaxNo: self.AgentData().IncomeTaxNumber() },
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

function CheckForVatCombinationExistance_tab(self, agentData) {
    var resultval = 0;
    self.viewModelHelper.apiGet('api/Agent/CheckForVatCombinationExistance',
    { vatNo: self.AgentData().VATNumber() },
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

function CheckForRegCombinationExistance_tab(self, agentData) {
    var resultval = 0;
    self.viewModelHelper.apiGet('api/Agent/CheckForRegCombinationExistance',
    { regNo: self.AgentData().RegistrationNumber() },
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

// To check the Tax No. for uniqueness
function CheckForTaxCombinationExistance(self, agentData) {
    self.viewModelHelper.apiGet('api/Agent/CheckForTaxCombinationExistance',
                        { incTaxNo: self.AgentData().IncomeTaxNumber() },
                      function (result) {
                          if (result < 1) {
                              GoToTab2(self, agentData);
                          }
                          else {
                              //  self.AgentData().RegistrationNumber('');
                              //  self.AgentData().VATNumber('');
                              //  self.AgentData().IncomeTaxNumber('');
                              toastr.error("Entered Tax no. already exists !.", "Error");

                          }
                      }, null, null, false);
}

// To check the VAT No. for uniqueness
function CheckForVatCombinationExistance(self, agentData) {
    self.viewModelHelper.apiGet('api/Agent/CheckForVatCombinationExistance',
                        { vatNo: self.AgentData().VATNumber() },
                      function (result) {
                          if (result < 1) {
                              GoToTab2(self, agentData);
                          }
                          else {
                              //  self.AgentData().RegistrationNumber('');
                              //  self.AgentData().VATNumber('');
                              //  self.AgentData().IncomeTaxNumber('');
                              toastr.error("Entered Vat no. already exists !.", "Error");

                          }
                      }, null, null, false);
}

// To check the Reg. No. for uniqueness
function CheckForRegCombinationExistance(self, agentData) {
    self.viewModelHelper.apiGet('api/Agent/CheckForRegCombinationExistance',
                        { regNo: self.AgentData().RegistrationNumber() },
                      function (result) {
                          if (result < 1) {
                              GoToTab2(self, agentData);
                          }
                          else {
                              //  self.AgentData().RegistrationNumber('');
                              //  self.AgentData().VATNumber('');
                              //  self.AgentData().IncomeTaxNumber('');
                              toastr.error("Entered Reg no. already exists !.", "Error");

                          }
                      }, null, null, false);
}

// To Uploading the documents in the agent regsitration
function UploadDocuments(self, applicantId) {
    var formData = new FormData();

    var documentData = [];
    documentData.push(self.AgentData().AgentDocumentsVO());

    var result = $.inArray('Masters functionality.xlsx', documentData);

    var match = ko.utils.arrayFirst(self.AgentData().AgentDocumentsVO(), function (item) {
        return item.FileName() === 'Masters functionalit.xl';
    });

    $.each(self.AgentData().AgentDocumentsVO(), function (key, val) {
        //if (val.IsAlreadyExists === false) {
        //if (CheckFileExists(val.name, self.AgentData().AgentID())) {


        if (CheckFileExists(val.FileName(), self.AgentData().AgentID())) {
            if (confirm("This file is already exists. Do you want to replace it")) {
                return;
            }
        }
        else {
            formData.append("Prefix", "Agent_" + applicantId);
            formData.append(val.name, val.FileDetails);
        }
        //}
    });
    //$.ajax({
    //    url: '/api/agent/FileUpload', // We'll send to our Web API UploadController
    //    data: formData, // Pass through our fancy form data
    //    // To prevent jQuery from trying to do clever things with our post which
    //    // will break our upload, we'll set the following to false
    //    cache: false,
    //    contentType: false,
    //    processData: false,
    //    type: 'POST',
    //    success: function (data) {

    //    }
    //});
}

//for Tab functionality
function GoToTab1(self, agentData, viewDetail) {
    self.AgentData().viewModeForTabs('register1');
    //$('#btnGoBack').css('display', 'none');
    //$('#btnSubmit').css('display', 'none');
    //$('#btnSaveContinue').css('display', '');
    //if (self.AgentData().isView() == true) {
    //    self.AgentData().isSubmitVisible(false);
    //    self.AgentData().isSaveVisible(true);
    //}
    //else {
    //    self.AgentData().isSubmitVisible(false);
    //    self.AgentData().isSaveVisible(false);
    //}

    //if (self.AgentData().isView() == true) {
    //    $('#btnGoBack').css('display', 'none');
    //    $('#btnSubmit').css('display', 'none');
    //    $('#btnSaveContinue').css('display', '');
    //    $('#btnReset').css('display', '');
    //} else if (viewDetail == true) {
    //    $('#btnReset').css('display', 'none');
    //    $('#btnGoBack').css('display', 'none');
    //    $('#btnSubmit').css('display', 'none');
    //    $('#btnSaveContinue').css('display', 'none');
    //}
    //else {
    //    $('#btnReset').css('display', '');
    //    $('#btnGoBack').css('display', 'none');
    //    $('#btnSubmit').css('display', 'none');
    //    $('#btnSaveContinue').css('display', 'none');
    //}

    if (self.AgentData().ViewMode() == true) {
        $('#btnGoBack').css('display', 'none');
        $('#btnSubmit').css('display', 'none');
        $('#btnSaveContinue').css('display', '');
        $('#btnReset').css('display', 'none');
        $('#btnSaveContinue').html('Next');
    }
    else if (self.AgentData().EditMode() == true) {
        $('#btnReset').css('display', '');
        $('#btnGoBack').css('display', 'none');
        $('#btnSubmit').css('display', 'none');
        $('#btnSaveContinue').css('display', '');
    }
    else if (viewDetail == true) {
        $('#btnReset').css('display', 'none');
        $('#btnGoBack').css('display', 'none');
        $('#btnSubmit').css('display', 'none');
        $('#btnSaveContinue').css('display', 'none');
    }
    else {
        $('#btnReset').css('display', '');
        $('#btnGoBack').css('display', 'none');
        $('#btnSubmit').css('display', 'none');
        $('#btnSaveContinue').css('display', '');
    }

    //self.AgentData().isGoBackVisible(false);
    var index = 0;
    HandleProgressBarAndActiveTab(index);
}

//for Tab functionality
function GoToTab2(self, agentData) {
    self.AgentData().viewModeForTabs('register2');
    $('#btnReset').css('display', 'none');

    if (self.AgentData().isView() == true) {
        $('#btnGoBack').css('display', '');
        $('#btnSubmit').css('display', '');
        $('#btnSaveContinue').css('display', 'none');
    }
    else {

        $('#btnGoBack').css('display', '');
        $('#btnSubmit').css('display', 'none');
        $('#btnSaveContinue').css('display', 'none');
    }

    //self.AgentData().isSaveVisible(false);
    var index = 1;
    HandleProgressBarAndActiveTab(index);
}

//Checking the file exisitng or not
function CheckFileExists(fileName, agentId) {
    var result = false;
    $.ajax({
        //url: '/Agent/CheckFileExists',
        url: 'api/Agent/CheckFileExists',
        type: 'post',
        data: { fileName: fileName, agentId: agentId },
        success: function (data) {
            result = data;
        }
    });
    return result;
}

//Checking for the ports
function toggle(source) {
    checkboxes = document.getElementsByName('port');
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }


}

//  Port Selection Check box for single
function toggleSingle(source) {

    checkboxes = document.getElementsByName('port');
    var chk = true;
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (!checkboxes[i].checked) {
            chk = false;
        }
    }

    checkboxesAll = document.getElementsByName('Allport');
    checkboxesAll[0].checked = chk;
}

function CheckSameAsPrincepleAddress() {
    var isCheked = 0;
    if (($.trim($('#txtBusinessstreet').val()) == $.trim($('#txtPostalstreet').val())) && ($.trim($('#txtBusinesssuburb').val()) == $.trim($('#txtPostalsuburb').val())) && ($.trim($('#txtBusinesstowncity').val()) == $.trim($('#txtPostaltowncity').val())) && ($.trim($('#txtBusinesscode').val()) == $.trim($('#txtPostalcode').val())))
        isCheked = 1;
    return isCheked;
}

