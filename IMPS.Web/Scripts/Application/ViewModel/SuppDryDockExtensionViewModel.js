(function (IPMSRoot) {
    var SuppDryDockViewModel = function (SuppDryDockExtensionID, viewDetail) {

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

        //Auto Generated VCN Details
        self.getVCNDtls = ko.observable();

        //Variable for File Upload
        self.isfileToUpload = ko.observable(false);

        self.fileSizeConfigValue = ko.observable();

        self.ScheduleToDate = ko.observable();

        var validationErrorMessage = "* This field is required.";

        //////////////////////////////////Action  : Click functionality starts here//////////////////////////////////
        //Author  : Sandeep Appana
        //Date    : 10th Nov 2014
        //Purpose : adding Supplementary Dry Dock Application
        //Action  : Add New + Button
        self.addSuppDryDock = function (data) {

            //Binding the Supplementary Dry Dock Application Model
            self.suppDryDockModel(new IPMSROOT.SuppDryDockModel());

            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Add Dry Dock Extension");
            
            //Type of template we are binding // Form or List tempalate
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
            $('#spnTitle').html("View Dry Dock Extension");
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsEditable(false);
            self.isVCNEnabled(false);
            self.viewMode('Form');
            suppDryDock.FromDate(moment(suppDryDock.FromDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ToDate(moment(suppDryDock.ToDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ScheduleFromDate(moment(suppDryDock.ScheduleFromDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ScheduleToDate(moment(suppDryDock.ScheduleToDate()).format('YYYY-MM-DD HH:mm'));

            //suppDryDock.ExtensionDateTime(moment(suppDryDock.ExtensionDateTime()).format('YYYY-MM-DD HH:mm')); //suppDryDock.ExtensionDateTime();
            
           
            self.suppDryDockModel(suppDryDock);
           
         
           //$("#txtExtensionDateTime").text(suppDryDock.ExtensionDateTime());

           //$("#txtExtensionDateTime").kendoDatePicker({
           //    value: suppDryDock.ExtensionDateTime()
               
           //}) 
           
            //$("#txtExtensionDateTime").attr("disabled", "disabled");
            self.suppDryDockModel().VesselData().VCN(suppDryDock.VCN());
            self.suppDryDockModel().VesselData().VesselName(suppDryDock.VesselName());

            self.suppDryDockModel().VesselData().IMONo(suppDryDock.IMONo());
            self.suppDryDockModel().VesselData().GrossRegisteredTonnageInMT(suppDryDock.GrossRegisteredTonnageInMT());
            self.suppDryDockModel().VesselData().LengthOverallInM(suppDryDock.LengthOverallInM());
            self.suppDryDockModel().VesselData().BeamInM(suppDryDock.BeamInM());
            self.suppDryDockModel().VesselData().ArrDraft(suppDryDock.ArrDraft());

            self.suppDryDockModel().AgentData().RegisteredName(suppDryDock.RegisteredName());
            self.suppDryDockModel().AgentData().TradingName(suppDryDock.TradingName());
            self.suppDryDockModel().AgentData().TelephoneNo1(suppDryDock.TelephoneNo1());
            self.suppDryDockModel().AgentData().FaxNo(suppDryDock.FaxNo());
            self.suppDryDockModel().AgentData().TelephoneNo2(suppDryDock.TelephoneNo2());

            var obj = suppDryDock.ExtensionDateTime();
            $("#txtExtensionDateTime").val(obj);

            $("#txtExtensionDateTime").data('kendoDateTimePicker').enable(false);
            self.suppDryDockModel().TermsandConditions(true);


            
            self.suppDryDockModel().pendingTasks.removeAll();
            var ReferenceID = suppDryDock.SuppDryDockExtensionID();
            var WorkflowInstanceID = suppDryDock.WorkflowInstanceID();

            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                   null,
                         function (result) {

                             ko.utils.arrayForEach(result, function (val) {
                                 debugger;
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
        //Edit Details of selected Row in the Grid List of Supplementary Dry Dock Extension List
        //Author : Sandeep A
        self.EditSuppDryDock = function (suppDryDock) {
            //Title For the Displayed Content //Add or Edit or View or Main Page
            $('#spnTitle').html("Update Dry Dock Extension");
            
            self.IsSave(false);
            self.IsEditable(true);
            self.IsReset(true);
            self.IsUpdate(true);
            self.isVCNEnabled(false);
            self.viewMode('Form');
            suppDryDock.FromDate(moment(suppDryDock.FromDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ToDate(moment(suppDryDock.ToDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ScheduleFromDate(moment(suppDryDock.ScheduleFromDate()).format('YYYY-MM-DD HH:mm'));
            suppDryDock.ScheduleToDate(moment(suppDryDock.ScheduleToDate()).format('YYYY-MM-DD HH:mm'));

            suppDryDock.ExtensionDateTime(moment(suppDryDock.ExtensionDateTime()).format('YYYY-MM-DD HH:mm')); //suppDryDock.ExtensionDateTime();
            self.suppDryDockModel(suppDryDock);
            
            //$("#RequestToDate").data('kendoDatePicker').min($("#RequestFromDate").val());

         

            self.suppDryDockModel().VesselData().VCN(suppDryDock.VCN());
            self.suppDryDockModel().VesselData().VesselName(suppDryDock.VesselName());

            self.suppDryDockModel().VesselData().IMONo(suppDryDock.IMONo());
            self.suppDryDockModel().VesselData().GrossRegisteredTonnageInMT(suppDryDock.GrossRegisteredTonnageInMT());
            self.suppDryDockModel().VesselData().LengthOverallInM(suppDryDock.LengthOverallInM());
            self.suppDryDockModel().VesselData().BeamInM(suppDryDock.BeamInM());
            self.suppDryDockModel().VesselData().ArrDraft(suppDryDock.ArrDraft());

            self.suppDryDockModel().AgentData().RegisteredName(suppDryDock.RegisteredName());
            self.suppDryDockModel().AgentData().TradingName(suppDryDock.TradingName());
            self.suppDryDockModel().AgentData().TelephoneNo1(suppDryDock.TelephoneNo1());
            self.suppDryDockModel().AgentData().FaxNo(suppDryDock.FaxNo());
            self.suppDryDockModel().AgentData().TelephoneNo2(suppDryDock.TelephoneNo2());

            var dateTimePicker = $("#txtExtensionDateTime").data("kendoDateTimePicker");
            dateTimePicker.value(obj);

            var obj = suppDryDock.ExtensionDateTime();
            $("#txtExtensionDateTime").val(obj);

            self.suppDryDockModel().TermsandConditions(true);
          
           
          
            //$("#txtExtensionDateTime").data('kendoDateTimePicker').value(obj);
        }

        //Action : Button Cancel  Supplementary Dry Dock Extension)
        //Purpose : Cancel Supplementary Dry Dock Extension and redirect from FORM to List
        self.Cancel = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.suppDryDockModel().reset();
                $('#spnTitle').html(" Dry Dock Extension List");
                $('#spanchkTermsandConditions').text('');
                self.viewMode('List');
            }
        }

        //Action  : Button Save
        //Purpose : saving new Supplementary Dry Dock Extension
        self.SaveSuppDryDock = function (model) {
            model.validationEnabled(true);
            self.SuppDryDockValidation = ko.observable(model);
            self.SuppDryDockValidation().errors = ko.validation.group(self.SuppDryDockValidation());

            var errors = self.SuppDryDockValidation().errors().length;
           // console.log('model', model);
           // console.log('errors', errors);

            if (errors == 0) {
                if ($("#chkTermsandConditions").is(":checked") == false) {
                    $("#spanchkTermsandConditions").text('Please accept terms & conditions.');
                    return false;
                }
                else {
                    $("#spanchkTermsandConditions").text('');
                }
                model.ExtensionDateTime(moment(model.ExtensionDateTime()).format('YYYY/MM/DD HH:mm'));
                //success
                self.viewModelHelper.apiPost('api/SuppDryDockExtension', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Dry Dock Extension Added Successfully.", "Dry Dock Extension");
                        self.Initialize();
                        self.viewMode('List');
                        //location.reload();
                    });
            }
            else {
                self.SuppDryDockValidation().errors.showAllMessages(true);
                $('.validationElement:first').focus();
                $('#divValidationError').removeClass('display-none');

                $('#divValidationError').css('display', '');
                //location.reload();
                return;
            }
        }

        //Action  : Button Modify
        //Purpose : Modifiying the selected  Supplementary Dry Dock Extension
        self.ModifySuppDryDock = function (model) {
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
                model.ExtensionDateTime(moment(model.ExtensionDateTime()).format('YYYY/MM/DD HH:mm'));
                //success
                self.viewModelHelper.apiPut('api/SuppDryDockExtension', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Dry Dock Extension Updated Successfully.", "Dry Dock Extension");
                        self.LoadSuppDryDockList();
                        self.viewMode('List');
                    });
                $('#spnTitle').html(" Dry Dock Extension List");
            }
            else {
                self.SuppDryDockValidation().errors.showAllMessages(true);
                $('.validationElement:first').focus();
                $('#divValidationError').removeClass('display-none');

                $('#divValidationError').css('display', '');
            }
            
        }

        //Action  : Button Reset
        //Purpose : Reset  Supplementary Dry Dock Extension saved data
        self.Reset = function (model) {
           // self.SuppDryDockValidation().errors.showAllMessages(false);
           
         
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }
            $('#spanchkTermsandConditions').text('');
            //if (self.IsEditable()) {
            if (self.IsUpdate() == true) {
                self.suppDryDockModel().reset();
                self.suppDryDockModel(model);
                self.suppDryDockModel().TermsandConditions(true);
                //self.viewModelHelper.apiGet('api/GetSuppVCNDetailsForExtensionByVCN/' + $("#Vessel").val(),
                //null, function (result) {
                //    self.suppDryDockModel().AgentData(result);
                //});
              
                //self.viewModelHelper.apiGet('api/SuppVCNListForExtension', null, function (result1) {
                //    //self.getVCNDtls(new IPMSRoot.vesselModel(result1));
                //    debugger;
                //    var vcnDetails = ko.utils.arrayFilter(result1, function (item) {
                        
                //        if (item.VCN == $("#Vessel").val()) {
                //            return item;
                //        }
                //    })[0];
                //    //self.VesselDetails = new IPMSRoot.vesselDetail({ VesselName: vcnDetails.VesselName, IMONo: vcnDetails.IMONo, VCN: vcnDetails.VCN, GrossRegisteredTonnageInMT: vcnDetails.GrossRegisteredTonnageInMT, BeamInM: vcnDetails.BeamInM, ArrDraft: vcnDetails.ArrDraft, LengthOverallInM: "" });
                //    self.VesselDetails = new IPMSRoot.vesselDetail(vcnDetails);
                //    self.suppDryDockModel().VesselData.push(ko.toJSON(self.VesselDetails));
                //    debugger;

                //}, null, null, false);


            }
            else {
                self.suppDryDockModel().reset();
                ko.validation.reset();
            }
            $("#spanExtensionDateTime").text('');

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
            dataItem.ETA = moment(dataItem.ETA).format('YYYY-MM-DD HH:mm');
            dataItem.ETD = moment(dataItem.ETD).format('YYYY-MM-DD HH:mm');

            // To Get Agent details based on VCN
            self.viewModelHelper.apiGet('api/GetSuppVCNDetailsForExtensionByVCN/' + dataItem.VCN,
                null,
            function (result) {
                self.getDocumentsbyVCNLst = ko.observableArray();

                self.getDocumentsbyVCN = ko.observable();

                $.each(result.SuppDryDockDocument, function (item, index) {
                    
                    self.getDocumentsbyVCN = new IPMSRoot.SuppDryDockDocument(index);
                    self.getDocumentsbyVCNLst.push(self.getDocumentsbyVCN);

                });
                

                self.suppDryDockModel().SuppDryDockDocuments(self.getDocumentsbyVCNLst());
                //self.suppDryDockModel().su(result);
                self.suppDryDockModel().AgentData(result);
                self.suppDryDockModel().VesselAgent(result.RegisteredName);
                self.suppDryDockModel().FromDate(moment(result.FromDate).format('YYYY-MM-DD HH:mm'));
                self.suppDryDockModel().ToDate(moment(result.ToDate).format('YYYY-MM-DD HH:mm'));
                self.suppDryDockModel().ScheduleFromDate(moment(result.ScheduleFromDate).format('YYYY-MM-DD HH:mm'));
                self.suppDryDockModel().ScheduleToDate(moment(result.ScheduleToDate).format('YYYY-MM-DD HH:mm'));
                self.suppDryDockModel().SuppDryDockID(result.SuppDryDockID);
                self.ScheduleToDate(moment(result.ScheduleToDate).format('YYYY-MM-DD HH:mm'));
                var ScheduleToDateValue = self.ScheduleToDate();
                self.suppDryDockModel().CargoTons(result.CargoTons);
                self.suppDryDockModel().Ballast(result.Ballast);
                self.suppDryDockModel().Bunkers(result.Bunkers);
                self.suppDryDockModel().CurrentBerth(result.CurrentBerth);
                //self.suppDryDockModel().VesselData().LengthOverallInM(result.LengthOverallInM);
                $("#txtExtensionDateTime").val('');
                $("#txtExtensionDateTime").data('kendoDateTimePicker').min(ScheduleToDateValue);
               // console.log('AName', self.suppDryDockModel().VesselAgent());


            });

            self.suppDryDockModel().VesselData(dataItem);
            self.suppDryDockModel().VesselName(dataItem.VesselName);
           // console.log('dataItem', dataItem);

            //console.log('VName', self.suppDryDockModel().VesselName());
        }

        //Action : Button File Upload
        //Purpose : File Upload
        //Author : Sandeep A
        self.uploadFile = function () {
            

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
                        var fileSizeInBytes = opmlFile.files[i].size;
                        var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                        if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
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
                                self.suppDryDockModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB.", "Error");
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
                var CategoryCode = $('#selUploadDocs option:selected').val();
                //     if (opmlFile.files.length > 0) {
                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.Listdocuments = ko.observableArray();
                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.SuppDryDockDocument();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
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

       

        // Supplementary Dry Dock Extension Initialization(pageload) mode
        self.Initialize = function () {
            self.viewMode(true);
            self.suppDryDockModel(new IPMSROOT.SuppDryDockModel());
            //self.LoadVCNDetails();
            GetVCNdata();
            self.GetFileSizeConfigValue();
            self.viewMode('List');
            if (viewDetail == true) {

            }
            else {
                self.viewMode('List');
            }
            self.LoadSuppDryDockList();
            $('#spnTitle').html("Dry Dock Extension List");
           // alert("Version 18");
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


        self.LoadSuppDryDockList = function () {
            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/SuppDryDockExtension/' + SuppDryDockExtensionID,
              { SuppDryDockExtensionID: SuppDryDockExtensionID },
               function (result) {
                   self.SuppDryDockList(ko.utils.arrayMap(result, function (item) {
                       item.FromDate = (moment(item.FromDate).format('YYYY-MM-DD HH:mm'));
                       item.ToDate = (moment(item.ToDate).format('YYYY-MM-DD HH:mm'));
                       item.ScheduleFromDate = (moment(item.ScheduleFromDate).format('YYYY-MM-DD HH:mm'));
                       item.ScheduleToDate = (moment(item.ScheduleToDate).format('YYYY-MM-DD HH:mm'));
                       item.ExtensionDateTime = (moment(item.ExtensionDateTime).format('YYYY-MM-DD HH:mm'));
                       return new IPMSRoot.SuppDryDockModel(item);
                   }));

                   self.ViewSuppDryDock(self.SuppDryDockList()[0]);
               });
            }
            else {
                self.viewModelHelper.apiGet('api/SuppDryDockExtension', null,
                   function (result) {
                       self.SuppDryDockList(ko.utils.arrayMap(result, function (item) {
                           item.FromDate = (moment(item.FromDate).format('YYYY-MM-DD HH:mm'));
                           item.ToDate = (moment(item.ToDate).format('YYYY-MM-DD HH:mm'));
                           item.ScheduleFromDate = (moment(item.ScheduleFromDate).format('YYYY-MM-DD HH:mm'));
                           item.ScheduleToDate = (moment(item.ScheduleToDate).format('YYYY-MM-DD HH:mm'));
                           item.ExtensionDateTime = (moment(item.ExtensionDateTime).format('YYYY-MM-DD HH:mm'));
                           return new IPMSRoot.SuppDryDockModel(item);
                       }));
                   });
            }
        }

        //////////////////////////////////Action  : Dropdownlist Binding starts here //////////////////////////////////       

        //Filling VCN details for AutoComplete (textbox with dropdownlist)
        //Inherited By Sandeep which was already implemented in Service Request for same details : On 8th Nov 2014
        //self.LoadVCNDetails = function () {
        //    self.viewModelHelper.apiGet('api/SuppVCNListForExtension', null, function (result1) {
        //        self.getVCNDtls(new IPMSRoot.vesselModel(result1));
        //    }, null, null, false);
        //}

        GetVCNdata = function () {
            self.viewModelHelper.apiGet('api/SuppVCNListForExtension', null, function (result1) {
                self.getVCNDtls(new IPMSRoot.vesselModel(result1));
          }, null, null, false);
        }

        //////////////////////////////////Action  : Dropdownlist Binding ends here //////////////////////////////////


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        //Only Future Dates
        calOpen = function () {
            
            if (self.ScheduleToDate() != null || self.ScheduleToDate() != undefined) {
                $("#txtExtensionDateTime").data('kendoDateTimePicker').min(self.ScheduleToDate());
            }
            else {
                $("#txtExtensionDateTime").data('kendoDateTimePicker').min(new Date());
            }

            
        };

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