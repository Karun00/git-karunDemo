(function (IPMSRoot) {
    var SuppDryDockViewModel = function (suppdrydockid, viewDetail) {

        var self = this;

        self.viewMode = ko.observable();
        self.suppDryDockModel = ko.observable();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        //For Common Validation
        self.validationHelper = new IPMSRoot.validationHelper();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.isVCNEnabled = ko.observable(true);
        self.IsEditable = ko.observable(true);
        self.SuppDryDockList = ko.observableArray();
        self.SuppVCNList = ko.observableArray();

        self.DocumentTypes = ko.observable();
        //Auto Generated VCN Details
        self.getVCNDtls = ko.observable();

        //Variable for File Upload
        self.isfileToUpload = ko.observable(false);

        self.fileSizeConfigValue = ko.observable();

        var validationErrorMessage = "* This field is required."

        self.GetDocumentTypes = function () {
            self.viewModelHelper.apiGet('api/SuppDryDockDocumentTypes', null, function (result) {
                self.DocumentTypes(result);
            }, null, null, false);

        }

        //////////////////////////////////Action  : Click functionality starts here//////////////////////////////////
        //Author  : Sandeep Appana
        //Date    : 10th Nov 2014
        //Purpose : adding Supplementary Dry Dock Application
        //Action  : Add New + Button
        self.addSuppDryDock = function (data) {

            //Binding the Supplementary Dry Dock Application Model
            self.suppDryDockModel(new IPMSROOT.SuppDryDockModel());

            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Add Dry Dock Application");

            //Type of template we are binding // Form or List tempalate
            self.GetDocumentTypes();
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsReset(true);
            self.IsSave(true);
            self.IsEditable(true);
            self.isVCNEnabled(true);
        }

        //Grid Action
        //View Details of selected Row in the Grid List of Supplementary Dry Dock Application List
        //Author : Sandeep A
        self.ViewSuppDryDock = function (suppDryDock) {
            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("View Dry Dock Application");
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsEditable(false);
            self.isVCNEnabled(false);
            self.viewMode('Form');

            self.suppDryDockModel(suppDryDock);
            $("#RequestFromDate").data("kendoDateTimePicker").enable(false);
            $("#RequestToDate").data("kendoDateTimePicker").enable(false);

            self.suppDryDockModel().pendingTasks.removeAll();
            var ReferenceID = suppDryDock.SuppDryDockID();
            var WorkflowInstanceID = suppDryDock.WorkflowInstanceID();

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
                                 self.suppDryDockModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        //Grid Action
        //Edit Details of selected Row in the Grid List of Supplementary Dry Dock Application List
        //Author : Sandeep A
        self.EditSuppDryDock = function (suppDryDock) {
            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Update  Dry Dock Application");

            self.IsSave(false);
            self.IsEditable(true);
            self.IsReset(true);
            self.IsUpdate(true);
            self.isVCNEnabled(false);
            self.viewMode('Form');
            self.suppDryDockModel(suppDryDock);
            self.GetDocumentTypes();
            $("#RequestToDate").data('kendoDateTimePicker').min($("#RequestFromDate").val());

            self.suppDryDockModel().pendingTasks.removeAll();
            var ReferenceID = suppDryDock.SuppDryDockID();
            var WorkflowInstanceID = suppDryDock.WorkflowInstanceID();

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
                                self.suppDryDockModel().pendingTasks.push(pendingtaskaction);
                            });
                        });
        }

        //Action : Button Cancel  Supplementary Dry Dock Application)
        //Purpose : Cancel Supplementary Dry Dock Application and redirect from FORM to List
        self.Cancel = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.suppDryDockModel().reset();
                $('#spnTitle').html(" Dry Dock Application List");
                $('#spanchkTermsandConditions').text('');
                self.viewMode('List');
            }
        }

        //Action  : Button Save
        //Purpose : saving new Supplementary Dry Dock Application
        self.SaveSuppDryDock = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.SuppDryDockValidation = ko.observable(model);
            self.SuppDryDockValidation().errors = ko.validation.group(self.SuppDryDockValidation());

            var errors = self.SuppDryDockValidation().errors().length;
            console.log('model', model);
            console.log('errors', errors);


            var match = ko.utils.arrayFirst(self.SuppVCNList(), function (item) {
                debugger;
                return item.VCN === model.VCN();
            });

            if (match == null) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Select Valid VCN", "Dry Dock Application");
             //   self.IsVCNnum(false);
                return;
            }

            if (errors == 0) {
                if ($("#chkTermsandConditions").is(":checked") == false) {
                    $("#spanchkTermsandConditions").text('Please accept terms & conditions.');
                    return false;
                }
                else {
                    $("#spanchkTermsandConditions").text('');
                }
                //success
                self.viewModelHelper.apiPost('api/SuppDryDockApplication', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.success("Dry Dock Application Saved Successfully.", "Dry Dock Application");
                        self.LoadSuppDryDockList();
                        self.viewMode('List');
                        $('#spnTitle').html("Dry Dock Application List");

                    });
            }
            else {
                self.SuppDryDockValidation().errors.showAllMessages(true);
                $('.validationElement:first').focus();
                $('#divValidationError').removeClass('display-none');

                $('#divValidationError').css('display', '');

                return;
            }
        }

        //Action  : Button Modify
        //Purpose : Modifiying the selected  Supplementary Dry Dock Application
        self.ModifySuppDryDock = function (model) {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.SuppDryDockValidation = ko.observable(model);
            self.SuppDryDockValidation().errors = ko.validation.group(self.SuppDryDockValidation());

            var errors = self.SuppDryDockValidation().errors().length;

            if (errors == 0) {

                if ($("#chkTermsandConditions").is(":checked") == false) {
                    $("#spanchkTermsandConditions").text('Please accept terms & conditions.');
                    return false;
                }
                else {
                    $("#spanchkTermsandConditions").text('');
                }

                //success
                self.viewModelHelper.apiPut('api/SuppDryDockApplication', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.success("Dry Dock Application Updated Successfully.", "Dry Dock Application");
                        self.LoadSuppDryDockList();
                        self.viewMode('List');
                        $('#spnTitle').html("Dry Dock Application List");
                    });
            }
            else {
                self.SuppDryDockValidation().errors.showAllMessages(true);
                $('.validationElement:first').focus();
                $('#divValidationError').removeClass('display-none');

                $('#divValidationError').css('display', '');
            }
        }

        //Action  : Button Reset
        //Purpose : Reset  Supplementary Dry Dock Application saved data
        self.Reset = function (model) {
            ko.validation.reset();
            self.suppDryDockModel().reset();
            self.SuppDryDockValidation().errors.showAllMessages(false);
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            $('#spanchkTermsandConditions').text('');
            var uploadedFiles = [];
            vm.suppDryDockModel().SuppDryDockDocuments.removeAll();

        }

        // change Event for RequestFromDate DateTime Picker
        ChangeRequestFromDate = function () {
            if ($("#RequestFromDate").val() == "" || $("#RequestFromDate").val() == null) {
                $("#spanRequestFromDate").text(validationErrorMessage);
                $("#RequestToDate").val('');
                self.suppDryDockModel().ToDate("");
            }
            else {
                $("#spanRequestFromDate").text('');
                //Date Validation starts here
                var StartDateValue = $("#RequestFromDate").val();
                var EndDateValue = $("#RequestToDate").val();
                $("#RequestToDate").val('');
                self.suppDryDockModel().ToDate("");
                $("#RequestToDate").data('kendoDateTimePicker').min(StartDateValue);
                //Date Validation ends here
            }
        }

        // change Event for RequestToDate DateTime Picker
        ChangeRequestToDate = function () {
            if (!(($("#RequestFromDate").val() == "" || $("#RequestFromDate").val() == null))) {
                if ($("#RequestToDate").val() == "" || $("#RequestToDate").val() == null) {
                    $("#spanRequestToDate").text(validationErrorMessage);
                }
                else {
                    $("#spanRequestToDate").text('');
                }
            }
            else {
                $("#RequestFromDate").focus();
                $("#spanRequestFromDate").text(validationErrorMessage);
                $("#RequestToDate").val('');
                $("#spanRequestToDate").text('');
            }
        }

        //Action  : Textbox Selecting VCN
        //Purpose : Inherited By sandeep which was already implemented in Service Request for same details.Dated : 08 Nov 2014
        self.VesselSelect = function (e) {
           
            var dataItem = this.dataItem(e.item.index());

            if (dataItem.VCN != null) {
            
                self.viewModelHelper.apiGet('api/GetSuppDryDockVCN/' + dataItem.VCN,
                null,
                function (result) {                 
                    if (result != null) {
                        self.suppDryDockModel().SuppWFStatus(result.SuppWFStatus);
                        self.suppDryDockModel().LeftDockDateTime1(result.LeftDockDateTime1);
                    }

               //     if (self.suppDryDockModel().SuppWFStatus() != "") {
                        if (self.suppDryDockModel().SuppWFStatus() == 'NEW') {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("Dry Dock Application is already raised for the VCN.", "Dry Dock Application");
                            self.suppDryDockModel().VCN("");

                        }           
                        else if (self.suppDryDockModel().SuppWFStatus() == 'WFSA' || self.suppDryDockModel().SuppWFStatus() == 'WFCO' && self.suppDryDockModel().LeftDockDateTime1() == null) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("Dry Dock Application is already raised for the VCN.", "Dry Dock Application");
                            self.suppDryDockModel().VCN("");
                        }
                        else {
                            dataItem.ETA = moment(dataItem.ETA).format('YYYY-MM-DD hh:mm:ss A');
                            dataItem.ETD = moment(dataItem.ETD).format('YYYY-MM-DD hh:mm:ss A');

                            // To Get Agent details based on VCN
                            self.viewModelHelper.apiGet('api/GetAgentDetailsInVesselCallByVCN/' + dataItem.VCN,
                                null,
                            function (result) {
                                self.suppDryDockModel().AgentData(result);
                                self.suppDryDockModel().VesselAgent(result.RegisteredName);

                            });

                            self.suppDryDockModel().VesselData(dataItem);
                            self.suppDryDockModel().VesselName(dataItem.VesselName);
                            self.suppDryDockModel().VCN(dataItem.VCN);
                        }
                //    }



                });
            }



        }
              

        //Action : Button File Upload
        //Purpose : File Upload
        //Author : Sandeep A
        self.uploadFile = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {

                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                $("#spanfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                self.suppDryDockModel().UploadedFiles([]);
                var uploadedFiles = [];
                uploadedFiles = self.suppDryDockModel().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.suppDryDockModel().SuppDryDockDocuments(), function (item) {
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
                                    self.suppDryDockModel().UploadedFiles(uploadedFiles);
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
                    //     if (opmlFile.files.length > 0) {
                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.SuppDryDockDocument();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);

                            Adddoc.DocumentName(item.DocumentName);
                            Adddoc.DocumentType(item.DocumentType);
                            Adddoc.DocumentPath(item.DocumentPath);
                            Adddoc.FileName(item.FileName);
                            Adddoc.CategoryName(CategoryName);
                            Adddoc.CategoryCode(CategoryCode);
                            Adddoc.CreatedBy(item.CreatedBy);
                            Adddoc.CreatedBy(item.CreatedDate);
                            Adddoc.ModifiedBy(item.ModifiedBy);
                            Adddoc.ModifiedDate(item.ModifiedDate);
                            Adddoc.DocumentTypeName(CategoryName);

                            //    //Adddoc.DocumentName(documentType);
                            self.suppDryDockModel().SuppDryDockDocuments.push(Adddoc);
                            $("select#selUploadDocs").prop('selectedIndex', 0);
                        }));
                    });
                    // }
                }
                else {
                    $("#spanfileToUpload").text('Please select file.');
                    self.isfileToUpload(true);
                }
                self.suppDryDockModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        //Action : Button Delete
        //Purpose : Delete documents
        //Author : Sandeep A
        self.DeleteDocument = function (Adddoc) {
            self.suppDryDockModel().SuppDryDockDocuments.remove(Adddoc);
        }

        //////////////////////////////////Action : Click ends here //////////////////////////////////

        //Validate Only Numberic
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        }

        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function () {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //////////////////////////////////Action  : Validation ends here //////////////////////////////////

        //Only Future Dates
        calOpen = function () {
            this.min(new Date());
        };

        // Supplementary Dry Dock Application Initialization(pageload) mode
        self.Initialize = function () {

            self.viewMode(true);
            self.suppDryDockModel(new IPMSROOT.SuppDryDockModel());
            //self.LoadVCNDetails();
            self.GetFileSizeConfigValue();
            self.viewMode('List');
            self.LoadSuppDryDockList();
            $('#spnTitle').html("Dry Dock Application List");
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
        //Added by srinivas start


        //srinu submit
       
        self.cancelWFRequest = function (model) {

            if (model.WorkFlowRemarks() == undefined || model.WorkFlowRemarks() == "") {
                $('#spanremarks').text('Please Enter Reason');
                return;
            }
            if (model.ScheduleStatus() == "PLND") {
                toastr.warning(" Cancellation is not Allowed the until the Vessel is unscheduled");
            }
            else {
                self.viewModelHelper.apiPost('api/DockingPlanApplication/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Docking Plan Application Cancelled Successfully", "Docking Plan Application");
                                $(".close").trigger("click");
                                self.LoadSuppDryDockList();
                                self.viewMode('List');
                            });
            }
        }
        
        //Confirmcancel
        self.cancelConfirmWFRequest = function (model) {
            if (model.WorkFlowRemarks() == undefined || model.WorkFlowRemarks() == "") {
                $('#spanremarks1').text('Please Enter Reason');
                return;
            }
            else {
                $('#spanremarks1').text('');

            }

            if (model.ScheduleStatus() == "PLND") {
                toastr.warning(" Cancellation is not Allowed the until the Vessel is UnScheduled");
            }
            
            else {
                model.IsConfirmCancel("Y");
                self.viewModelHelper.apiPost('api/DockingPlanApplication/GridCancel', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Dry Plan Application Confirm Cancellation Request Sent Successfully", "Docking Plan Application");
                                    $(".close").trigger("click");
                                    self.LoadSuppDryDockList();
                                    self.viewMode('List');
                                });
            }
        }


        //Cancel Request 
        self.cancelReqst = function (dockingapplicationcancel) {
            self.viewMode('List');
            self.viewMode('PopUp');
            self.suppDryDockModel(dockingapplicationcancel)

        }
        //Confirm Cancel
        self.cancelConfirmReqst = function (dockingapplicationcancel) {
            self.viewMode('List');
            self.viewMode('PopUp1');
            self.suppDryDockModel(dockingapplicationcancel)
            //self.IsUpdate(false);
            //self.IsSave(false);
            //self.IsReset(false);
            //self.editableView(false);
            //self.IsCodeEnable(false);
            //self.printView(false);

            //self.servicerequestModel(servicerequest);

        }

        self.CancelButton = function () {
            $(".close").trigger("click");
            self.LoadSuppDryDockList();
            self.viewMode('List');

        }

        self.closePopup = function () {
            self.suppDryDockModel().WorkFlowRemarks("");
            self.viewMode('List');

        }

        //Added by srinivas end

        self.LoadSuppDryDockList = function () {
            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/SuppDryDockApplication/' + suppdrydockid,
              { suppdrydockid: suppdrydockid },
               function (result) {
                   self.SuppDryDockList(ko.utils.arrayMap(result, function (item) {
                       return new IPMSRoot.SuppDryDockModel(item);
                   }));
                   self.ViewSuppDryDock(self.SuppDryDockList()[0]);
               });
            }
            else {
                self.viewModelHelper.apiGet('api/SuppDryDockApplication', null,
                   function (result) {
                       self.SuppDryDockList(ko.utils.arrayMap(result, function (item) {
                           return new IPMSRoot.SuppDryDockModel(item);
                       }));
                   });
            }
        }

        //////////////////////////////////Action  : Dropdownlist Binding starts here //////////////////////////////////       

        //Filling VCN details for AutoComplete (textbox with dropdownlist)
        //Inherited By Sandeep which was already implemented in Service Request for same details : On 8th Nov 2014
        //  self.LoadVCNDetails = function () {
        //      self.viewModelHelper.apiGet('api/SuppVCNList', null,
        //function (result1) {
        //    self.getVCNDtls(new IPMSRoot.vesselModel(result1));
        //}, null, null, false);
        //  }

        //////////////////////////////////Action  : Dropdownlist Binding ends here //////////////////////////////////

   
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.suppDryDockModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.viewWorkFlow = function (suppdrydock) {
            var workflowinstanceId = suppdrydock.WorkflowInstanceID();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.suppDryDockModel(new IPMSROOT.SuppDryDockModel());
                  self.suppDryDockModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        self.Initialize();

    }
    IPMSRoot.SuppDryDockViewModel = SuppDryDockViewModel;
}(window.IPMSROOT));