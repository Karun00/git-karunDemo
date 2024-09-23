toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";
(function (IPMSRoot) {
    var isView = 0;
    var InternalEmployeePermitViewModel = function (viewDetail) {
        var self = this;    
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.internalemployeepermitModel = ko.observable(new IPMSROOT.InternalEmployeePermitModel());   
        self.permitrequestlist = ko.observableArray();
        self.Internalemployeepermitlist = ko.observableArray();
        self.approvedInternalemployeepermitlist = ko.observableArray();
        self.IsCodeEnable = ko.observable(true);
        self.portentrypassapplicationreferencedata = ko.observable();
        self.isfileToUpload = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.isPermittype = ko.observable();
        self.IsSaveEnable = ko.observable(false);
        self.AdvnceSearchmodel = ko.observable(new IPMSROOT.AdvnceSearchmodel());



        self.Initialize = function () {
            self.viewMode = ko.observable(true);  
            self.GetFileSizeConfigValue();           
            if (viewDetail == 1) {
               
                $('#spnTitle').html("Internal Employee Permit");
                self.viewMode('List');            
                self.LoadInternalEmployeePermitRequestlist();
                self.LoadInitialData();
                self.IsCodeEnable(true);
            }    
            else if (viewDetail == 3) {
               
                $('#spnTitle').html("Internal Employee Permit Approve");
                self.viewMode('List');            
                self.LoadInternalEmployeetobeapprovedPermitlist();
                self.LoadInitialData();
            }
        }
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {

                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);

            });
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/PortEntryPassReferenceData', null,
                    function (result1) {
                        self.portentrypassapplicationreferencedata(new IPMSRoot.PortEntryPassApplicationReferenceData(result1));
                    }, null, null, false);
        }
        
        
        self.GetDataClickApprovePermit = function (model) {
            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);
            self.LoadInternalEmployeetobeapprovedPermitlist();
        }

        self.ClearFilterApprovePermit = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadInternalEmployeetobeapprovedPermitlist();
        }



        self.GetDataClick = function (model) {
            var frmdt = self.AdvnceSearchmodel().RequestFrom();
            var todt = self.AdvnceSearchmodel().RequestTo();
            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');
            self.AdvnceSearchmodel().RequestFrom(frmdate);
            self.AdvnceSearchmodel().RequestTo(todate);
            self.LoadInternalEmployeePermitRequestlist();
        }

        self.ClearFilter = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate());
            fromdate.setDate(fromdate.getDate() - 1);
            self.AdvnceSearchmodel().RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.AdvnceSearchmodel().RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            self.LoadInternalEmployeePermitRequestlist();
        }


        
        self.LoadInternalEmployeePermitRequestlist = function () {
            self.viewModelHelper.apiPost('api/InternalEmployeePermitrequestlistSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
               

                self.Internalemployeepermitlist(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.InternalEmployeePermitModel(item);
                }));
            });
        }
        self.LoadApprovedPermitrequestlist = function () {
            self.viewModelHelper.apiGet('api/ApprovedPermitrequestlist', null,
                          function (result) {

                              self.permitrequestlist(ko.utils.arrayMap(result, function (item) {
                                  return new IPMSRoot.InternalEmployeePermitModel(item);

                              }));
                          });
        }
        self.LoadInternalEmployeetobeapprovedPermitlist = function () {
            //self.viewModelHelper.apiGet('api/InternalEmployeetobeapprovedPermitlist', null,
                        //  function (result) {
                              self.viewModelHelper.apiPost('api/InternalEmployeetobeapprovedPermitlistSearch', ko.mapping.toJSON(self.AdvnceSearchmodel()), function Message(result) {
                       
                              self.approvedInternalemployeepermitlist(ko.utils.arrayMap(result, function (item) {
                                  return new IPMSRoot.InternalEmployeePermitModel(item);

                              }));
                          });

        }



        self.CancelApprovedPermit = function () {
           
            $('#spnTitle').html("Internal Employee Permit Approve");
            self.viewMode('List');
            self.IsCodeEnable(true);
            self.LoadApprovedPermitrequestlist();

        }
        self.CancelInternalEmployee = function () {
           
            $('#spnTitle').html("Internal Employee Permit");
            self.viewMode('List');
            self.IsCodeEnable(true);
            self.LoadInternalEmployeePermitRequestlist();

        }
        self.ADDportentrypassapplication = function () {
            self.viewMode('Form');
            self.IsCodeEnable(true);
            $('#spnTitle').html("Add Internal Employee Permit");
            self.internalemployeepermitModel(new IPMSRoot.InternalEmployeePermitModel());
            $("#MobileNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNointernalemployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }

        self.viewInternalEmployeePermit = function (data) {
           
            self.viewMode('Form');
            self.IsCodeEnable(false);
            $('#spnTitle').html(" View Internal Employee Permit");
            self.internalemployeepermitModel(data);
            $("#MobileNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNointernalemployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }
        self.viewApprovedportentrypassapplication = function (data) {
            ;
            self.viewMode('Form');
            self.IsCodeEnable(false);
            $('#spnTitle').html(" View Internal Employee Permit Approve"); 
            self.internalemployeepermitModel(data);
            $("#MobileNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNointernalemployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }
        self.Approveportentrypassapplication = function (data) {
            self.viewMode('Form');
            $('#spnTitle').html("  Approve Internal Employee Permit  ");
            self.IsCodeEnable(false);
            self.IsSaveEnable(true);
            self.internalemployeepermitModel(data);
            $("#MobileNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyTelephoneNointernalEmployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CompanyFaxNointernalemployee").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        }

        self.Reset = function (data) {
            self.internalemployeepermitModel().reset();
            $('#spnTitle').html("Add Internal Employee Permit");
        }
        self.ResetApprove = function (data) {
            self.internalemployeepermitModel().reset();
            $('#spnTitle').html("  Approve Internal Employee Permit  ");
        }

        self.ApproveInternalEmployeepermit = function (data) {
            self.viewModelHelper.apiPost('api/ApproveInternalEmployeePermit', ko.mapping.toJSON(data), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Internal Employee Permit Request Approve Details Submitted Successfully", "Internal Employee Permit Request Approve");
                self.LoadInternalEmployeetobeapprovedPermitlist();
                self.viewMode('List');
                $('#spnTitle').html("Internal Employee Permit Approve");
            });
        }
        var uploadedFiles = [];
        var documentData = [];
        self.multipleuploadFile = function () {
            
            if ($('#selUploadDocs1').get(0).selectedIndex == 0) {
                toastr.warning("Please select document Type.");
                return;
            }
            else {
                $("#spanHWPSfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs1 option:selected').text();
                // alert(documentType);
                uploadedFiles = self.internalemployeepermitModel().UploadedFiles();
                var opmlFile = $('#fileToUpload1')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.internalemployeepermitModel().PermitRequestDocuments(), function (item) {
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
                                    self.internalemployeepermitModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                    return;
                                }
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " File Size is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.warning("The Selected File Already Exist.! Please Upload Another File.", "Warning");
                            return;
                        }

                    }
                    var formData = new FormData();
                    $.each(self.internalemployeepermitModel().UploadedFiles(), function (key, val) {
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
                                self.internalemployeepermitModel().PermitRequestDocuments.push(Adddoc);
                            }));

                        });
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Browse  Atleast One Document", "Internal Employee Permit Request");
                }
                $('#selUploadDocs1').val('');
                self.internalemployeepermitModel().UploadedFiles([]);
                $('#fileToUpload1').val('');
                return;
                //}

            }
        }
        self.multipleDeleteDocument = function (Adddoc) {
            self.internalemployeepermitModel().PermitRequestDocuments.remove(Adddoc);
        }
        self.saveportentrypassapplication = function (portentrypass) {
      
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
           
            var errors = 0;
            var childerros = 0;
            portentrypass.validationEnabled(true);
            portentrypass.validationEnabled(true);
            if (portentrypass.PermitRequestID() == 0) {
                self.portentrypassValidation = ko.observable(portentrypass);
                self.portentrypassValidation().errors = ko.validation.group(self.portentrypassValidation());
                //errors = self.portentrypassValidation().errors().length;
                errors = ko.utils.arrayFilter(self.portentrypassValidation().errors(), function (error) {

                    return error != null;
                })
                self.PersonalPermitValidation = ko.observable(portentrypass.PersonalPermits);
                self.PersonalPermitValidation().errors = ko.validation.group(self.PersonalPermitValidation());
                //childerros = self.PersonalPermitValidation().errors().length
                childerros = ko.utils.arrayFilter(self.PersonalPermitValidation().errors(), function (error) {

                    return error != null;
                })
        

                if (errors == 0 && childerros == 0) {
                    if (formvalidation(portentrypass) == true) {
                        if (portentrypass.PersonalPermits().PermitCategoryCode() != 'PERA')
                        { portentrypass.PersonalPermits().AllNPASites(false); }
               
                        if (portentrypass.PermitRequestDocuments().length <= 0) {                     
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.warning("Please Upload  Atleast One Document", "Internal Employee Permit Request");
                            return;
                        }
             
                        self.viewModelHelper.apiPost('api/AddInternalEmployeePermit', ko.mapping.toJSON(portentrypass), function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Internal Employee Permit Request Details Submitted Successfully", "Internal Employee Permit Request");
                            self.LoadInternalEmployeePermitRequestlist();
                            self.viewMode('List');
                            $('#spnTitle').html("Internal Employee Permit");
                        });
                    }
                    else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Fill All Mandatory Details In The Form", "Internal Employee Permit Request");
                        self.portentrypassValidation().errors.showAllMessages();
                        return;
                    }
                }
                else {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Fill All Mandatory Details In The Form", "Internal Employee Permit Request");
                    self.portentrypassValidation().errors.showAllMessages();
                    self.PersonalPermitValidation().errors.showAllMessages();
                    return;
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please Fill All Mandatory Details In The Form", "Internal Employee Permit Request");
    
                return;
            }
        }
        self.viewportentrypassapplication = function (data) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.internalemployeepermitModel(data);
        }
        self.selectedAPermitChoices = function () {
            
            if (self.internalemployeepermitModel().PersonalPermits().PermitCategoryCode() == 'PERA') {
                //if (self.portentrypassapplicationModel().PermitRequestTypeCode() == 'APCG') {
                $('#AllNPASites').prop('checked', "true");
                //toastr.warning("this is only applicable on an A Category Permit", "Prot Entry Pass");
                //self.internalemployeepermitModel().PersonalPermits().SpecificNPASites() == "";
                return true;
            }
            else {
                $('#AllNPASites').prop('checked', "false");
                toastr.warning("This Is Only Applicable On An 'A' Category Permit'", "Port Entry Pass Request");
                self.internalemployeepermitModel().PersonalPermits().AllNPASites(false);
                return false;
            }

            //}

        };
        self.unselectionrcheckbox = function () {
            
            if (self.internalemployeepermitModel().PersonalPermits().PermitCategoryCode() != 'PERA') {
                $('#AllNPASites').prop('checked', false);
                $('#AllNPASites').attr('checked', false);
                $('#AllNPASites').prop('checked', false);
                $('#AllNPASites').attr('checked', false);
                self.internalemployeepermitModel().PersonalPermits().AllNPASites(false);
             
            }
            return true;
            //else {
            //    $('#SpecificNPASites').prop('Unchecked', true);
            //    $('#SpecificNPASites').attr('Unchecked', true);
            //    $('#SpecificNPASites').prop('Unchecked', true);
            //    $('#SpecificNPASites').attr('Unchecked', true);
            //    self.internalemployeepermitModel().PersonalPermits().SpecificNPASites() == "";
            //    return true;
            //}
        }

        SearchRequesttoCal = function () {
            this.min($("#SearchRequestFrom").val());
            var myDatePicker = new Date($("#SearchRequestFrom").val());
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth() + 2;
            var year = myDatePicker.getFullYear();
            this.max(new Date(year, month, day));

        }


        function formvalidation(portentrypass) {
            var result = true;
            var filterPhoneNumber = portentrypass.MobileNo();
            filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');

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
                        toastr.warning("Invalid Mobile Number.");
                        validPhoneNumber++;
                        result = false;
                    }
                }
            }
            if (validPhoneNumber != 0) {
                $("#MobileNointernalEmployee").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var ContactNomaskedtextbox = $("#MobileNointernalEmployee").data("kendoMaskedTextBox");
                portentrypass.MobileNo(ContactNomaskedtextbox.value());
           
            }
            //if (portentrypass.PermitRequestDocuments().length <= 0) {
            //    toastr.options.closeButton = true;
            //    toastr.options.positionClass = "toast-top-right";
            //    toastr.warning("Please Upload  Atleast One Document", "Port Entry Pass Request");
            //    return;
            //}
            var filterCompanyPhoneNumber = portentrypass.CompanyTelephoneNo();
            filterCompanyPhoneNumber = filterCompanyPhoneNumber.replace(/(\)|\()|_|-+/g, '');

            var validCompanyPhoneNumber = 0;

            if (filterCompanyPhoneNumber.length != 0) {
                if (filterCompanyPhoneNumber.length != 13) {
                    toastr.warning("Invalid Company Phone Number.");
                    validCompanyPhoneNumber++;
                    result = false;
                }
                else if (filterCompanyPhoneNumber.length == 13) {
                    var validNo = parseInt(filterCompanyPhoneNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Company Phone Number.");
                        validCompanyPhoneNumber++;
                        result = false;
                    }
                }
            }
            if (validCompanyPhoneNumber != 0) {
                $("#CompanyTelephoneNointernalEmployee").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var CompanyTelephoneNomaskedtextbox = $("#CompanyTelephoneNointernalEmployee").data("kendoMaskedTextBox");
                portentrypass.CompanyTelephoneNo(CompanyTelephoneNomaskedtextbox.value());
      
            }

            var filterCompanyFaxNumber = portentrypass.CompanyFaxNo();
            filterCompanyFaxNumber = filterCompanyFaxNumber.replace(/(\)|\()|_|-+/g, '');

            var validCompanyFaxNumber = 0;

            if (filterCompanyFaxNumber.length != 0) {
                if (filterCompanyFaxNumber.length != 13) {
                    toastr.warning("Invalid Company Fax Number.");
                    validCompanyFaxNumber++;
                    result = false;
                }
                else if (filterCompanyFaxNumber.length == 13) {
                    var validNo = parseInt(filterCompanyFaxNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Company Fax Number.");
                        validCompanyFaxNumber++;
                        result = false;
                    }

                }
            }
                
            
            if (validCompanyFaxNumber != 0) {
                $("#CompanyFaxNointernalemployee").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var CompanyFaxNomaskedtextbox = $("#CompanyFaxNointernalemployee").data("kendoMaskedTextBox");
                portentrypass.CompanyFaxNo(CompanyFaxNomaskedtextbox.value());
        
            }         
            if (portentrypass.PersonalPermits().permittype() != 'adhoc') {
                self.internalemployeepermitModel().PersonalPermits().AdhocPermits('');
              
            }
            if (portentrypass.PersonalPermits().permittype() != 'temporary')
            {
                self.internalemployeepermitModel().PersonalPermits().TemporaryPermits('');
           
            }
            if (portentrypass.PersonalPermits().permittype() != 'allports')
            {
                self.internalemployeepermitModel().PersonalPermits().AllPorts('');
            
            }
            if (portentrypass.PersonalPermits().permittype() != 'contractor')
            {
                self.internalemployeepermitModel().PersonalPermits().ConstructionArea('');

            }
            if (portentrypass.PersonalPermits().permittype() != 'permanent')
            {
                self.internalemployeepermitModel().PersonalPermits().PermanentPermits('');

            }
            self.internalemployeepermitModel().PersonalPermits().permittype() == self.isPermittype();
            return result;
        }
        self.Initialize();
    }
    IPMSRoot.InternalEmployeePermitViewModel = InternalEmployeePermitViewModel;
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