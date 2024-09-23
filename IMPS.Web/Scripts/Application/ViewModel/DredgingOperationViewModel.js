(function (IPMSRoot) {

    var DredgingOperationViewModel = function (dredgingoperationid, viewDetail) {
        var self = this;

        $('#spanBerthOccupationTitle').html("Berth Occupation");
        $('#spnTitile').html("Dredging Volume");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.dredgingOperationModel = ko.observable();
        self.viewMode = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.berthOccupationList = ko.observableArray();
        self.isfileToUpload = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.isUploadFileVisible = ko.observable(true);
        self.isDelUploadFileVisible = ko.observable(true);
        self.occupationFrom = ko.observable();
        self.monthvalue = ko.observable();
        self.toDate = ko.observable();
        self.fromDate = ko.observable();
        self.isOccupationFromMsg = ko.observable(false);
        self.isOccupationToMsg = ko.observable(false);
        self.isOccupationFromMsg1 = ko.observable(false);
        self.isOccupationToMsg1 = ko.observable(false);
        //For Common Validation
        self.validationHelper = new IPMSRoot.validationHelper();
        self.editableView = ko.observable(true);

        self.dredgingVolumeList = ko.observableArray();

        // Initialize method is fires in  pageload Initializetion mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.GetFileSizeConfigValue();
            self.dredgingOperationModel(new IPMSROOT.DredgingOperationModel());
            self.LoadBerthOccupationList();
            self.LoadDredgingVolumeList();
                
                if (viewDetail == true) {

                  
                    self.viewMode('Form');

                   
                }
                else {
                    self.viewMode('List');
                }
        }
       
        self.LoadBerthOccupationList = function () {
            if (viewDetail == true) {
                
                self.viewModelHelper.apiGet('api/DredgingOperation/GetBerthOccupationById', { id: dredgingoperationid },
                  function (result) {

                      self.berthOccupationList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.DredgingOperationModel(item);
                      }));

                      self.viewBerthOccupation(self.berthOccupationList()[0]);
                  });
            }

            else {
                self.viewModelHelper.apiGet('api/BerthOccupation', null,
                    function (result) {
                        self.berthOccupationList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSROOT.DredgingOperationModel(item);
                        }));
                    });
            }
        }

        self.LoadDredgingVolumeList = function () {
            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/DredgingOperation/GetDredgingVolumeById', { id: dredgingoperationid },
                  function (result) {



                      self.viewDredgingVolume(new IPMSRoot.DredgingOperationModel(result[0]));
                  });
                
            }
            else {
                self.viewModelHelper.apiGet('api/DredgingVolume', null,
                    function (result) {
                        self.dredgingVolumeList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSROOT.DredgingOperationModel(item);
                        }));
                    });
            }
        }
       
        self.addBerthOccupation = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.isDelUploadFileVisible(true);
            self.dredgingOperationModel(new IPMSRoot.DredgingOperationModel());
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

        self.CancelBerthOccupation = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.dredgingOperationModel().reset();
                $('#spanBerthOccupationTitle').html("Berth Occupation");
            }
        }

        self.CancelDredgingVolume = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.dredgingOperationModel().reset();
                $('#spnTitile').html("Dredging Volume");
            }
        }

        //--------------------
        self.viewBerthOccupation = function (dredgingoperation) {
            debugger;
            self.viewMode('Form');
           
            self.dredgingOperationModel(dredgingoperation);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.isDelUploadFileVisible(false);
            $('#spanBerthOccupationTitle').html("View Berth Occupation");
            var ReferenceID = dredgingoperation.DredgingOperationID();
            var WorkflowInstanceID = dredgingoperation.DOWorkflowInstanceID();
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
                                 self.dredgingOperationModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }
        self.cancelReqst = function (dredgingoperation) {
            debugger;
            self.viewMode('PopUp');

          //  dredgingoperation.workflowRemarks("Cancel");
            self.dredgingOperationModel(dredgingoperation);
        }
        self.closePopup = function () {
            self.dredgingOperationModel().workflowRemarks("");
            self.LoadBerthOccupationList();
            self.viewMode('List');

        }
        //self.cancelReqst = function (servicerequest) {
        //    self.LoadBerths();
        //    self.viewMode('List');
        //    self.viewMode('PopUp');
        //    self.IsUpdate(false);
        //    self.IsSave(false);
        //    self.IsReset(false);
        //    self.editableView(false);
        //    self.IsCodeEnable(false);

        //    self.servicerequestModel(servicerequest);

        //}
        //self.cancelWF = function (dredgingoperation) {
        //    $(".close").trigger("click");
        //    self.LoadBerthOccupationList();
        //    self.viewMode('List');
        //}
        self.cancelWFRequest = function (model) {



            if (model.workflowRemarks() == undefined || model.workflowRemarks() == "") {
                $('#spanremarks').text('Please Enter Reason');
                return;
            }

            self.viewModelHelper.apiPost('api/DredgingOperation/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Berth Occupation Cancelled Successfully", "Berth Occupation");
                                $(".close").trigger("click");
                                self.LoadBerthOccupationList();
                                self.viewMode('List');
                            });

        }
     
        self.editBerthOccupation = function (dredgingoperation) {
            debugger;
             self.occupationFrom(moment(dredgingoperation.RequiredDate()).format('YYYY-MM-DD hh:mm:ss tt'));

             self.toDate(moment(dredgingoperation.ToDate()).format('YYYY-MM-DD hh:mm:ss tt'));
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.isDelUploadFileVisible(true);
            self.dredgingOperationModel(dredgingoperation);
            $('#spanBerthOccupationTitle').html("Update Berth Occupation");
            var Monthvalue = (moment(dredgingoperation.ToDate()).format('YYYY-MM'));
            var NDate = (moment(new Date()).format('YYYY-MM'));
            if (NDate>Monthvalue)
            {
                $("#OccupationTo").data('kendoDateTimePicker').enable(false);
                $("#OccupationFrom").data('kendoDateTimePicker').enable(false);
                $("#spnVelidatiDate").text('* Priority Month already Exit');
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning('Date can not be changed after priority month Expired', "Berth Occupation");
            }
        }

        self.viewDredgingVolume = function (dredgingoperation) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.dredgingOperationModel(dredgingoperation);
            $('#spnTitile').html("View Dredging Volume");
            var ReferenceID = dredgingoperation.DredgingOperationID();
            var WorkflowInstanceID = dredgingoperation.DVWorkflowInstanceID();
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
                                 self.dredgingOperationModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        self.editDredgingVolume = function (dredgingoperation) {
            debugger;
            self.fromDate(moment(dredgingoperation.FromDate()).format('YYYY-MM-DD hh:mm:ss tt'));
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.dredgingOperationModel(dredgingoperation);
            $('#spnTitile').html("Update Dredging Volume");
            var Monthvalue = (moment(dredgingoperation.ToDate()).format('YYYY-MM'));
            var MMonth = (moment(dredgingoperation.ToDate()).format('MMMM'));
            var NDate = (moment(new Date()).format('YYYY-MM'));
            if (NDate < Monthvalue) {
                $("#VolumeOccupationTo").data('kendoDateTimePicker').enable(false);
                $("#VolumeOccupationFrom").data('kendoDateTimePicker').enable(false);
                $("#spnVelidatiDate").text('* Occupation Date will start from ' + MMonth + ' ');
                self.IsUpdate(false);
                self.IsReset(false);
                self.editableView(false);
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning('* Occupation Date will start from ' + MMonth + ' .Before that Date can not be changed', "Dredging Volume");
            }
        }

        //Validate Only Numberic
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        }
        self.HandleVolume = function (data, event) {
            debugger;
            $("#spanVolume").text('');
           
        }
        self.HandleDredgingtask = function (data, event) {

            debugger;
                $("#spanDredgingTask").text('');
        }
       self. HandleDredgername = function (data, event) {

            debugger;
           
            $('#spanDredgerName').text('');
            
        }
        self.ResetDredgingOperation = function (model) {
            ko.validation.reset();
            self.dredgingOperationModel().reset();
            $('#spanVolume').text('');
            $('#spanDredgingTask').text('');
            $('#spanDredgerName').text('');
            $("#isOccupationFromMsg").text('');
            self.isOccupationFromMsg(false);
            $("#isOccupationFromMsg1").text('');
            self.isOccupationFromMsg1(false);
            $("#isOccupationToMsg").text('');
            self.isOccupationToMsg(false);
            $("#isOccupationToMsg1").text('');
            self.isOccupationToMsg1(false);
        }

        // To Update Dredging Volume data
        self.UpdateDredgingVolume = function (model) {
            debugger;
            console.log('model', model);
            var errors = 0;
            if (model.Volume() == "" || model.Volume() == null) {
                $('#spanVolume').text('* Please enter the Volume');
                errors++;
            }
            if (model.DredgingTask() == "" || model.DredgingTask() == null) {
                $('#spanDredgingTask').text('* Please enter the Dredging Task');
                errors++;
            }
            if (model.DredgerName() == "" || model.DredgerName() == null) {
                $('#spanDredgerName').text('* Please enter the Dredger Name');
                errors++;
            }
            if ((model.VolumeOccupationFrom() == "") || (model.VolumeOccupationFrom() == null)) {
                $("#isOccupationFromMsg1").text('* Please Select Occupation From');
                self.isOccupationFromMsg1(true);
                errors++
            }

            if ((model.VolumeOccupationTo() == "") || (model.VolumeOccupationTo() == null)) {
                $("#isOccupationToMsg1").text('* Please Select Occupation To');
                self.isOccupationToMsg1(true);
                errors++
            }
            if ((model.VolumeOccupationFrom() != "") && (model.VolumeOccupationTo() != "")) {
                debugger
                var dtOccupationFrom = (moment(new Date(Date.parse(model.VolumeOccupationFrom()))));//.format('YYYY-MM-DD hh:mm')); new Date(Date.parse(moment(endDateValue)));
                var dtOccupationTo = (moment(new Date(Date.parse(model.VolumeOccupationTo()))));//.format('YYYY-MM-DD hh:mm'));
                if (dtOccupationFrom >= dtOccupationTo) {
                    self.isOccupationToMsg1(false);
                    // errors1 = errors1 + 1;
                    $("#isOccupationToMsg1").text('Occupation To should be greater than Occupation From ');
                    self.isOccupationToMsg1(true);
                    errors++;
                  //  return;
                }
            }
            if (errors == 0) {
                self.viewModelHelper.apiPut('api/DredgingVolume', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Dredging Volume Updated Successfully", "Dredging Volume");
                    self.LoadDredgingVolumeList();
                    $('#spnTitile').html("Dredging Volume");
                    self.viewMode('List');
                    
                });
            }
            else {
                //toastr.options.closeButton = true;
                //toastr.options.positionClass = "toast-top-right";
                //toastr.warning("Please enter all mandatory fields", "Dredging Volume");
                return;
            }
        }

        // To Update Berth Occupation data
        self.UpdateBerthOccupation = function (model) {
            console.log(ko.toJSON(model));
            var errors = 0;
            var errors1 = 0;
            debugger;
            if ($('#spnVelidatiDate').text() == '') {
                if ((self.dredgingOperationModel().OccupationFrom() == "") || (self.dredgingOperationModel().OccupationFrom() == null)) {
                    $("#isOccupationFromMsg").text('* Please Select Occupation From');
                    self.isOccupationFromMsg(true);
                    errors1 = errors1 + 1;
                }
                if ((self.dredgingOperationModel().OccupationTo() == "") || (self.dredgingOperationModel().OccupationTo() == null)) {
                    $("#isOccupationToMsg").text('* Please Select Occupation To');
                    self.isOccupationToMsg(true);
                    errors1 = errors1 + 1;
                }
                else {
                    var dtOccupationFrom = (moment(new Date(Date.parse(self.dredgingOperationModel().OccupationFrom()))).format('YYYY-MM-DD hh:mm:ss tt'));
                    var dtOccupationTo = (moment(new Date(Date.parse(self.dredgingOperationModel().OccupationTo()))).format('YYYY-MM-DD hh:mm:ss tt'));
                    if (dtOccupationFrom >= dtOccupationTo) {
                        self.isOccupationToMsg1(false);
                        // errors1 = errors1 + 1;
                        $("#isOccupationToMsg").text('Occupation To should be greater than Occupation From ');
                        self.isOccupationToMsg(true);
                        errors1 = errors1 + 1;
                    }
                }
                //if ((self.dredgingOperationModel().OccupationFrom() != "") && (self.dredgingOperationModel().OccupationFrom() != null))
                //{

                //    if ((self.dredgingOperationModel().OccupationTo() != "") && (self.dredgingOperationModel().OccupationTo() != null)) {
                //        self.isOccupationToMsg(false);
                //        var dtOccupationFrom = new Date(Date.parse(self.dredgingOperationModel().OccupationFrom()));
                //        var dtOccupationTo = new Date(Date.parse(self.dredgingOperationModel().OccupationTo()));
                //        if (dtOccupationFrom > dtOccupationTo) {
                //            errors1 = errors1 + 1;
                //            $("#isOccupationToMsg").text('Occupation To should be greater than Occupation From ');
                //            self.isOccupationToMsg(true);
                //        }
                //    }
                //    else {

                //        errors1 = errors1 + 1;
                //        $("#isOccupationToMsg").text('Please select Occupation To');
                //        self.isOccupationToMsg(true);
                //    }
                //}
                //else {
                //    errors1 = errors1 + 1;
                //    $("#isOccupationFromMsg").text('Please select Occupation From');
                //    self.isOccupationFromMsg(true);

                //}
            }
            else {
                errors1 = 0;
            }
            if (errors1 == 0) {
                var date = (moment(model.RequiredDate()).format('YYYY-MM-DD'));
                model.RequireDate(date);
                self.viewModelHelper.apiPut('api/BerthOccupation', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Berth Occupation Updated Successfully", "Berth Occupation");
                    self.LoadBerthOccupationList();
                    $('#spanBerthOccupationTitle').html("Berth Occupation");
                    self.viewMode('List');
                });
            }
            else {
               // toastr.options.closeButton = true;
               // toastr.options.positionClass = "toast-top-right";
              //  toastr.warning("Please enter all mandatory fields", "Berth Occupation");
                return;
            }
        }
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.dredgingOperationModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }
        self.Initialize();

        var uploadedFiles = [];
        var documentData = [];

        self.uploadFile = function () {
            $("#spanBOfileToUpload").text("");

            self.isUploadFileVisible(false);
            var documentType = $('#selUploadDocs option:selected').text();
            self.dredgingOperationModel().UploadedFiles([]);
            uploadedFiles = self.dredgingOperationModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {
                    var match = ko.utils.arrayFirst(self.dredgingOperationModel().BerthOccupationDocumentVO(), function (item) {
                        return item.FileName() === opmlFile.files[i].name;
                    });
                   
                    if (match == null) {
                        var fileSizeInBytes = opmlFile.files[i].size;
                        var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                        if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            elem.FileSize = opmlFile.files[i].size;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                elem.CategoryName = $('#selUploadDocs option:selected').text();
                                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                self.dredgingOperationModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                return;
                            }
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
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });
                var CategoryName = $('#selUploadDocs option:selected').text();
                var CategoryCode = $('#selUploadDocs option:selected').val();

                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.Listdocuments = ko.observableArray();
                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.BerthOccupationDocument();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
                        self.dredgingOperationModel().BerthOccupationDocumentVO.push(Adddoc);
                    }));
                });
            } else {
                $("#spanBOfileToUpload").text('Please Select File');
                self.isUploadFileVisible(true);

            }
            self.dredgingOperationModel().UploadedFiles([]);
            $('#fileToUpload').val('');
            return;
            // }
        }

        self.DeleteDocument = function (documentRow) {
            self.dredgingOperationModel().BerthOccupationDocumentVO.remove(documentRow);
        }

        isAdd = 0;
        index = 1;


        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        self.OccupationMaxDate = function () {
             var occupationFrom = (moment(new Date()).format('YYYY-MM-DD hh:mm:ss tt'));
            this.min(occupationFrom);
            this.max(self.toDate());
        }
        self.VolumeOccupationMaxDate = function () {
            var occupationFrom = (moment(new Date()).format('YYYY-MM-DD hh:mm:ss tt'));
            this.min(self.fromDate());
            this.max(occupationFrom);
        }
        self.OccupationMaxDate1 = function () {

            var StartDateValue = $("#OccupationFrom").val();
          //  $("#OccupationTo").attr("value", "");
            this.min(StartDateValue);
            this.max(self.toDate());
        }
        self.VolumeOccupationMaxDate1 = function () {

            var StartDateValue = $("#VolumeOccupationFrom").val();
          //  $("#VolumeOccupationTo").attr("value", "");
            this.min(StartDateValue);
            var occupationFrom = (moment(new Date()).format('YYYY-MM-DD hh:mm:ss tt'));
            this.max(occupationFrom);
        }
        CalcStartPeriodofOccupation = function () {
            $("#isOccupationFromMsg").text('');
            self.isOccupationFromMsg(false);
            var StartDateValue = $("#OccupationFrom").val();
            var EndDateValue = $("#OccupationTo").val();
            $("#OccupationTo").val('');
            $("#OccupationTo").attr("value", "");
            $("#OccupationDuration").val('');
            $("#OccupationTo").data('kendoDateTimePicker').min(StartDateValue);
            self.dredgingOperationModel().OccupationTo('');
            self.dredgingOperationModel().OccupationDuration('');
        }

        CalcPeriodofOccupation = function (data, event) {
            debugger;
            var startDateValue = self.dredgingOperationModel().OccupationFrom();
            var dtOccupationFrom = new Date(Date.parse(moment(startDateValue)));
            var endDateValue = data.sender._oldText;
            var dtOccupationTo = new Date(Date.parse(moment(endDateValue)));

            if ((self.dredgingOperationModel().OccupationFrom() != "") && (self.dredgingOperationModel().OccupationFrom() != null)) {
                self.isOccupationToMsg(false);
                self.isOccupationFromMsg(false);

                if (dtOccupationFrom > dtOccupationTo) {
                    $("#isOccupationToMsg").text('Occupation To  should be greater than Occupation From');
                    self.isOccupationToMsg(true);
                }
                else {
                    var currentDate = new Date();//  new Date(Date.parse(model.StartDate()));
                    var tOccupationFrom = dtOccupationFrom.getMilliseconds();

                    var tOccupationTo = dtOccupationTo.getMilliseconds();

                    // calculating differec time b/w start and end time of immobilazation
                    self.isOccupationToMsg(false);
                    var diff = dtOccupationTo - dtOccupationFrom;
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
                    // period = moment({ hour: hhh, minute: mints });
                    // model.PeriodofOccupation(period);
                    if (period == '00.00') {
                        $("#isOccupationToMsg").text('Occupation To should be greater than Occupation From');
                        self.isOccupationToMsg(true);
                    }
                    else {

                        self.dredgingOperationModel().OccupationDuration(period);
                    }
                }
            }
            else {

                $("#isOccupationFromMsg").text('Please Select Occupation From');
                self.isOccupationFromMsg(true);
            }

        }
        VolumeCalcStartPeriodofOccupation = function () {
            $("#isOccupationFromMsg1").text('');
            self.isOccupationFromMsg1(false);
            var StartDateValue = $("#VolumeOccupationFrom").val();
            var EndDateValue = $("#VolumeOccupationTo").val();
            $("#VolumeOccupationTo").val('');
            $("#VolumeOccupationTo").attr("value", "");
            $("#VolumeOccupationDuration").val('');
            $("#VolumeOccupationTo").data('kendoDateTimePicker').min(StartDateValue);
            self.dredgingOperationModel().VolumeOccupationTo('');
            self.dredgingOperationModel().VolumeOccupationDuration('');
        }
        VolumeCalcPeriodofOccupation = function (data, event) {
            debugger
            var startDateValue = self.dredgingOperationModel().VolumeOccupationFrom();
            var dtOccupationFrom = new Date(Date.parse(moment(startDateValue)));
            var endDateValue = data.sender._oldText;
            var dtOccupationTo = new Date(Date.parse(moment(endDateValue)));

            if ((self.dredgingOperationModel().VolumeOccupationFrom() != "") && (self.dredgingOperationModel().VolumeOccupationFrom() != null)) {
                self.isOccupationToMsg1(false);
                self.isOccupationFromMsg1(false);

                if (dtOccupationFrom > dtOccupationTo) {
                    $("#isOccupationToMsg1").text('Occupation To  should be greater than Occupation From');
                    self.isOccupationToMsg1(true);
                }
                else {
                    var currentDate = new Date();//  new Date(Date.parse(model.StartDate()));
                    var tOccupationFrom = dtOccupationFrom.getMilliseconds();

                    var tOccupationTo = dtOccupationTo.getMilliseconds();

                    // calculating differec time b/w start and end time of immobilazation
                    self.isOccupationToMsg(false);
                    var diff = dtOccupationTo - dtOccupationFrom;
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

                    // period = hhh + ':' + mints + ':' + sss;
                    period = hhh + '.' + mints;
                    // period = moment({ hour: hhh, minute: mints });
                    // model.PeriodofOccupation(period);
                    if (period == '00.00') {
                        $("#isOccupationToMsg1").text('Occupation To should be greater than Occupation From');
                        self.isOccupationToMsg1(true);
                    }
                    else {

                        self.dredgingOperationModel().VolumeOccupationDuration(period);
                    }
                }
            }
            else {

                $("#isOccupationFromMsg1").text('Please Select Occupation From');
                self.isOccupationFromMsg1(true);
            }

        }


        self.viewWorkFlowDO = function (dredgingoperation) {
            var workflowinstanceId = dredgingoperation.DOWorkflowInstanceID();
            if (workflowinstanceId == "" || workflowinstanceId == null) {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack1').modal('show');
            }
            else {
                self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {
                      self.dredgingOperationModel(new IPMSROOT.DredgingOperationModel());
                      self.dredgingOperationModel().WrkFlowRemark(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack1').modal('show');
                  });
            }
        }

        self.viewWorkFlowDV = function (dredgingoperation) {
            var workflowinstanceId = dredgingoperation.DVWorkflowInstanceID();
            if (workflowinstanceId == "" || workflowinstanceId == null) {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack1').modal('show');
            }
            else {
                self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {
                      self.dredgingOperationModel(new IPMSROOT.DredgingOperationModel());
                      self.dredgingOperationModel().WrkFlowRemark(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack1').modal('show');
                  });
            }
        }

    }

    IPMSRoot.DredgingOperationViewModel = DredgingOperationViewModel;

}(window.IPMSROOT));