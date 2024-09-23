(function (IPMSRoot) {

    var DockingPlanViewModel = function (dockingplanid, viewDetail) {

        var self = this;
        $('#spnTitile').html("Docking Plan");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.dockingplanModel = ko.observable();
        self.DockingPlanList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.vesselNameList = ko.observableArray();
        self.IsEditable = ko.observable(true);
        self.DocumentTypes = ko.observable();


        self.isDockingfileToUpload = ko.observable(false);

        self.fileSizeConfigValue = ko.observable();

        self.dockingplanModel = ko.observable(new IPMSROOT.DockingPlanModel());
        self.GetDocumentTypes = function () {
            self.viewModelHelper.apiGet('api/DocumentTypes', null, function (result) {
                self.DocumentTypes(result);
            }, null, null, false);

        }


        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.dockingplanModel(new IPMSROOT.DockingPlanModel());
            self.LoadDockingPlans();
            //self.GetDocumentTypes();
        //    self.LoadVesselNames();
            self.GetFileSizeConfigValue();
            self.viewMode('List');

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


        self.LoadDockingPlans = function () {
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/DockingPlan/' + dockingplanid,
                 { dockingplanid: dockingplanid },
                  function (result) {
                      self.DockingPlanList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.DockingPlanModel(item);
                      }));
                      self.viewdockingplan(self.DockingPlanList()[0]);
                  });
            }
            else {
                self.viewModelHelper.apiGet('api/DockingPlan',
                null,
                  function (result) {

                      self.DockingPlanList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.DockingPlanModel(item);
                      }));


                  });
            }

        }

        self.LoadVesselNames = function () {
            self.viewModelHelper.apiGet('api/DockingPlanVessel',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.vesselNameList);
              });
        }

        self.VesselSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            if (selecteddataItem.VesselID != "") {
                self.viewModelHelper.apiGet('api/DockingPlanVesselInfo/' + selecteddataItem.VesselID,
              //{ vcn: selecteddataItem.VCN },
              null,

                 function (result) {

                     self.dockingplanModel().VesselID(result.VesselID);
                     self.dockingplanModel().IMONo(result.IMONo);
                     self.dockingplanModel().VesselType(result.VesselType);
                     self.dockingplanModel().LengthOverallInM(result.LengthOverallInM);
                     self.dockingplanModel().BeamInM(result.BeamInM);
                     self.dockingplanModel().PortOfRegistry(result.PortOfRegistry);
                     self.dockingplanModel().VesselGRT(result.VesselGRT);


                 });
            }
            else {
                self.dockingplanModel().IMONo("");
                self.dockingplanModel().VesselType("");
                self.dockingplanModel().LengthOverallInM("");
                self.dockingplanModel().BeamInM("");
                self.dockingplanModel().PortOfRegistry("");
                self.dockingplanModel().VesselGRT("");
            }
        }



        VesselNameBlur = function () {

            if (self.dockingplanModel().VesselID() == "") {
                self.dockingplanModel().VesselName('');
                self.dockingplanModel().IMONo('');
                self.dockingplanModel().VesselType('');
                self.dockingplanModel().LengthOverallInM('');
                self.dockingplanModel().BeamInM('');
                self.dockingplanModel().PortOfRegistry('');
                self.dockingplanModel().VesselGRT('');

            }
        }

        VesselNameKeypress = function () {
      
            self.dockingplanModel().VesselID('');       
            self.dockingplanModel().VesselName('');
            self.dockingplanModel().IMONo('');
            self.dockingplanModel().VesselType('');
            self.dockingplanModel().LengthOverallInM('');
            self.dockingplanModel().BeamInM('');
            self.dockingplanModel().PortOfRegistry('');
            self.dockingplanModel().VesselGRT('');
        }

        $('#Vessel232').live('keydown', function (e) {



            var charCode = e.which || e.keyCode;
            if (charCode == 8 || charCode == 46) {
                self.dockingplanModel().VesselID('');     
                self.dockingplanModel().IMONo("");
                self.dockingplanModel().VesselType("");
                self.dockingplanModel().LengthOverallInM("");
                self.dockingplanModel().BeamInM("");
                self.dockingplanModel().PortOfRegistry("");
                self.dockingplanModel().VesselGRT("");
         
            }
        });





        self.UploadFile = function () {

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                $("#spanDockingfileToUpload").text("");
                self.isDockingfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                var uploadedFiles = [];
                uploadedFiles = self.dockingplanModel().UploadedFiles();
                var opmlFile = $('#DockingplanfileToUpload')[0];
                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.dockingplanModel().DockingPlanDocumentsVO(), function (item) {
                            return item.FileName() === opmlFile.files[i].name;
                        });
                        if (match == null) {
                            //-- Checking For File Format
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                            if ($.inArray(extension, fileExtension) != -1) {

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
                            toastr.error("The selected file already exist, Please upload another file", "Error");
                            return;
                        }
                    }



                    var formData = new FormData();
                    $.each(uploadedFiles, function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });
                    // if (opmlFile.files.length > 0) {    
                    //var CategoryCode = $('#selUploadDocs option:selected').val();
                    var CategoryName = $('#selUploadDocs option:selected').text();
                    var CategoryCode = $('#selUploadDocs option:selected').val();

                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                        self.Listdocuments = ko.observableArray();
                        self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IPMSROOT.DockingPlanDocument();
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

                            self.dockingplanModel().DockingPlanDocumentsVO.push(Adddoc);
                            $("select#selUploadDocs").prop('selectedIndex', 0);

                            //   self.dockingplanModel().DockingPlanVO().DockingPlanDocumentsVO.push(Adddoc);
                            // self.supplementaryServiceRequestModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO.push(Adddoc);
                        }));
                    });
                }
                    //  }
                else {
                    $("#spanDockingfileToUpload").text('Please select file');
                    self.isDockingfileToUpload(true);
                }
                self.dockingplanModel().UploadedFiles([]);
                $('#DockingplanfileToUpload').val('');
                return;
            }
        }

        self.DockingDeleteDocument = function (Adddoc) {
            self.dockingplanModel().DockingPlanDocumentsVO.remove(Adddoc);
        }
           

        self.adddockingplan = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.IsEditable(true);
            self.GetDocumentTypes();

            self.dockingplanModel(new IPMSRoot.DockingPlanModel());

            $('#spnTitile').html("Add Docking Plan");
        }

        self.SaveDockingPlan = function (model) {
    
                model.validationEnabled(true);
                self.DockingPlanValidation = ko.observable(model);
                self.DockingPlanValidation().errors = ko.validation.group(self.DockingPlanValidation());
                var errors = self.DockingPlanValidation().errors().length;
             
                //var match = ko.utils.arrayFirst(self.vesselNameList(), function (item) {
                //    return item.VesselID() === model.VesselID();
                //});

                //if (match == null) {
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.error("Please Select Valid Vessel Name", "Docking Plan");
                // //   self.IsVCNnum(false);
                //    return;
                //}

                if (errors == 0) {
                    if (model.DockingPlanDocumentsVO().length == 0) {
                        toastr.warning("Please Upload Atleast One File");
                    }
                    else {

                        //  if (self.IsSave() == true) {
                        self.viewModelHelper.apiPost('api/DockingPlan', ko.mapping.toJSON(model), function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Docking Plan Details Saved Successfully", "Docking Plan");
                            self.LoadDockingPlans();
                            $('#spnTitile').html("Docking Plan");
                            self.viewMode('List');

                        });
                        //  }
                    }
                }
                else {
                    self.DockingPlanValidation().errors.showAllMessages();
                    $('#divValidationError').removeClass('display-none');
                    return;
                }
         


           
        }
        //srinu submit
        debugger;
        self.cancelWFRequest = function (model) {



            ////if (model.workflowRemarks() == undefined || model.workflowRemarks() == "") {
            ////    $('#spanremarks').text('Please Enter Reason');
            ////    return;
            ////}

            self.viewModelHelper.apiPost('api/DockingPlan/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Docking Plan Cancelled Successfully", "Docking Plan");
                                $(".close").trigger("click");
                                self.LoadDockingPlans();
                                self.viewMode('List');
                            });

        }

        //Cancel Request 
        self.cancelReqst = function (dockingcancel) {
       self.viewMode('List');
       self.viewMode('PopUp');
       self.dockingplanModel(dockingcancel)

        }
       
        self.CancelButton = function () {
            $(".close").trigger("click");
            self.LoadDockingPlans();
            self.viewMode('List');

        }

        self.ModifyDockingPlan = function (model) {
       

            model.validationEnabled(true);
            self.DockingPlanValidation = ko.observable(model);
            self.DockingPlanValidation().errors = ko.validation.group(self.DockingPlanValidation());
            var errors = self.DockingPlanValidation().errors().length;


            if (errors == 0) {
                if (model.DockingPlanDocumentsVO().length == 0) {
                    toastr.warning("Please Upload Atleast One File");
                }
                else {
                    self.viewModelHelper.apiPut('api/DockingPlan', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Docking Plan Details Updated Successfully", "Docking Plan");
                        self.LoadDockingPlans();
                        $('#spnTitile').html("Docking Plan");
                        self.viewMode('List');

                    });                   
                }
            }
            else {
                self.DockingPlanValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
            
        }


        self.ResetDockingPlan = function (model) {
            
            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.dockingplanModel().reset();
            var uploadedFiles = [];
            vm.dockingplanModel().DockingPlanDocumentsVO.removeAll();

         

        }

        self.Cancel = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.dockingplanModel().reset();
                $('#spnTitile').html("Docking Plan");
            }

        }

        self.viewdockingplan = function (dockplan) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsEditable(false);
            self.dockingplanModel(dockplan);
            $('#spnTitile').html("View Docking Plan");

            self.dockingplanModel().pendingTasks.removeAll();
            var ReferenceID = dockplan.DockingPlanID();
            var WorkflowInstanceID = dockplan.WorkflowInstanceID();

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
                                 self.dockingplanModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        self.editdockingplan = function (dockplan) {
        
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsEditable(true);
            self.dockingplanModel(dockplan);
            self.IsCodeEnable(false);
            self.GetDocumentTypes();
            $('#spnTitile').html("Update Docking Plan");

        }
     
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.dockingplanModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.viewWorkFlow = function (dockingplan) {           
            var workflowinstanceId = dockingplan.WorkflowInstanceID();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {             

                  self.dockingplanModel(new IPMSROOT.DockingPlanModel());
                  self.dockingplanModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        self.Initialize();
    }
    IPMSRoot.DockingPlanViewModel = DockingPlanViewModel;


}(window.IPMSROOT));


