(function (IPMSRoot) {
    var LicensingRequestViewModel = function (LicenseRequestID, viewDetail) {
        var self = this;
        $('#spnTitle').html("Licensing Request");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.licensingrequestModel = ko.observable(new IPMSROOT.LicensingRequestModel());
        self.viewMode = ko.observable();
        self.licensingrequestList = ko.observableArray();
        self.licensingRequestReferenceData = ko.observable();
        self.LicensePortWFArray = ko.observableArray([]);

        self.PortReferenceData = ko.observable();
        self.craftReferenceData = ko.observable();
        self.viewDetail = ko.observable(viewDetail);
        self.viewModeForTabs = ko.observable();
        self.viewMode = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsPortSelectionEnable = ko.observable(true);
        self.IsRefNOEnable = ko.observable(false);
        self.Checkboxdisable = ko.observable(true);
        self.Checkboxenabledisable = ko.observable(true);
        self.isGoBackVisible = ko.observable(false);
        self.isSaveVisible = ko.observable(true);
        self.isUpdateVisible = ko.observable(false);
        self.isSubmitVisible = ko.observable(false);

        self.isReset = ko.observable(true);
        self.isCancelVisible = ko.observable(true);
        self.isVisible = ko.observable(true);
        self.ReferenceData = ko.observable();
        self.isfileToUpload = ko.observable(false);
        self.isDisplayDelete = ko.observable(true);
        self.termsConditions = ko.observable(false);

        // Initialize method is fires in  pageload Initialization mode
        self.fileSizeConfigValue = ko.observable();

        //Validation Visible
        self.isNumStreetMsg = ko.observable(false);
        self.isSuburbMsg = ko.observable(false);
        self.isTownCityMsg = ko.observable(false);
        self.isPostalCodeMsg = ko.observable(false);
        self.isNumStreet2Msg = ko.observable(false);
        self.isSuburb2Msg = ko.observable(false);
        self.isTownCity2Msg = ko.observable(false);
        self.isPostalCode2Msg = ko.observable(false);

        self.isResidentialAddressCountry = ko.observable(false);
        self.isPostalAddressCountry = ko.observable(false);
        self.CountriesList = ko.observableArray();

        self.CaptachText = ko.observable();
        self.TelephoneNo1 = ko.observable();
        self.FaxNo = ko.observable();
        self.CellularNo = ko.observable();

        ///Validate ReferenceNo
        self.isValidReferenceNo = ko.observable();

        var validationMessage = '* This Field is Required.';

        //Masking Of Personal MobileNo.
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.viewModeForTabs('registration');
            self.viewMode('Form');

            self.loadlicensingrequesttypes();
            self.loadAllPortList();
            self.LoadLicensingRequestData();
            self.LoadCountries();
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel(undefined));
            self.GetFileSizeConfigValue();

            self.IsReset(true);
            self.isSaveVisible(true);
            self.IsCodeEnable(true);

            self.isResidentialAddressCountry(false);
            self.isPostalAddressCountry(false);

            self.IsPortSelectionEnable = ko.observable(true);
            self.isGoBackVisible(false);

            self.viewModeForTabs('registration');
            self.viewMode('Form');
            var index = 0;
            HandleProgressBarAndActiveTab(index);

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            if (viewDetail == true) {
                self.isSaveVisible(false);
                self.isDisplayDelete(false);
                self.isSubmitVisible(false);
                self.IsReset(false);
                self.isGoBackVisible(false);
                self.termsConditions(true);
                self.IsPortSelectionEnable(false);
                self.LoadLicenseingRequestList();
            }
            else {
                self.viewMode('List');
            }

            var captachText = randString(6);
            self.CaptachText(captachText);
        }

        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue', null,
                function (result) {
                    self.fileSizeConfigValue(result);
                    ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
                });
        }

        //Countries list from Vessel Nationality
        self.LoadCountries = function () {
            self.viewModelHelper.apiGet('api/SubCategory/GetCountriesList', null, function (result) {

                self.CountriesList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Countries(item);
                }));
            });
        }

        // this is for grid binding
        // LoadLicenseingRequestList Used For fetch the data for List from backend
        self.LoadLicenseingRequestList = function () {
            if (viewDetail == true) {
                self.IsPortSelectionEnable(false);
                self.viewModelHelper.apiGet('api/LicensingRequest/GetLicensingRequest', { id: LicenseRequestID },
                  function (result) {
                      
                      self.licensingrequestList(new IPMSRoot.LicensingRequestModel(result));
                      self.ViewLicensingRequest(self.licensingrequestList());
                  });
            }
            else {
                self.viewModelHelper.apiGet('api/LicensingRequestList', null,
                  function (result) {
                      self.licensingrequestList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.LicensingRequestModel(item);
                      }));
                  });
            }
        }

        self.LoadLicensingRequestData = function () {
            self.viewModelHelper.apiGet('api/LicensingRequestList', null,
                 function (result) {
                     self.licensingrequestList(ko.utils.arrayMap(result, function (item) {
                         return new IPMSRoot.LicensingRequestModel(item);
                     }));
                 });
        }

        // ADDLicensingRequest method is fires when Addnew button press in Add mode
        self.ADDLicensingRequest = function () {

            self.viewMode('Form');
            self.viewModeForTabs('registration');
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel(undefined));
            self.IsReset(true);
            self.isSaveVisible(true);
            self.IsCodeEnable(true);
            self.IsPortSelectionEnable(false);
            self.isGoBackVisible(false);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.selectedChoices();
        }

        // EditLicensingRequest method is fires when Edit button press in Edit mode
        self.EditLicensingRequest = function (licensingRequest) {
            self.viewMode('Form');
            self.viewModeForTabs('registration');
            self.IsReset(true);
            self.isSubmitVisible(false);
            self.isSaveVisible(true);
            self.selectedChoices();
            self.IsCodeEnable(true);
            self.IsPortSelectionEnable(true);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            TelephoneMasking = function (item) {
                $("#TelephoneNo1").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var varTelephoneNo1 = $("#TelephoneNo1").data("kendoMaskedTextBox");
                self.TelephoneNo1(varTelephoneNo1.value());
            }

            FaxMasking = function (item) {
                $("#FaxNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var varFaxNo = $("#FaxNo").data("kendoMaskedTextBox");
                self.FaxNo(varFaxNo.value());
            }

            CellularMasking = function (item) {
                $("#CellularNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var varCellularNo = $("#CellularNo").data("kendoMaskedTextBox");
                self.CellularNo(varCellularNo.value());
            }

            self.licensingrequestModel(licensingRequest);
            var ReferenceID = licensingRequest.LicenseRequestID();

            var WorkflowInstanceID = licensingRequest.ReferenceWorkflowInstenceID();
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
                                 self.licensingrequestModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        // ViewLicensingRequest method is fires when View button press in View mode
        self.ViewLicensingRequest = function (licensingRequest) {
 
            self.viewMode('Form');
            self.viewModeForTabs('registration');
            self.licensingrequestModel().ViewModeForTabs('registration');
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.IsReset(false);
            self.isSubmitVisible(false);
            self.isSaveVisible(true);
            self.Checkboxenabledisable(false);
            //self.isGoBackVisible(true);
            self.selectedChoices();
            self.IsCodeEnable(false);
            self.IsPortSelectionEnable(false);
            self.licensingrequestModel(licensingRequest);
            $('#spnTitle').html("Licensing Request");
            var ReferenceID = licensingRequest.LicenseRequestID();
            var WorkflowInstanceID = licensingRequest.ReferenceWorkflowInstenceID();

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            var cnt = 0;
            $('#divPortList input[name="port"]').each(function () {
                if (this.checked)
                    cnt += 1;
            });

            if ($('#divPortList input[name="port"]').length == cnt) {
                $('#Allport').attr('checked', true);
            }
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
                                 self.licensingrequestModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.licensingrequestModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        // Copy Details from Business Address to Postal Address
        self.selectedChoices = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                self.licensingrequestModel().PostalAddress().NumberStreet((self.licensingrequestModel().BusinessAddress().NumberStreet()));
                self.licensingrequestModel().PostalAddress().Suburb((self.licensingrequestModel().BusinessAddress().Suburb()));
                self.licensingrequestModel().PostalAddress().TownCity((self.licensingrequestModel().BusinessAddress().TownCity()));
                self.licensingrequestModel().PostalAddress().CountryCode((self.licensingrequestModel().BusinessAddress().CountryCode()));
                self.licensingrequestModel().PostalAddress().PostalCode((self.licensingrequestModel().BusinessAddress().PostalCode()));
            }
            else {
                self.licensingrequestModel().PostalAddress().NumberStreet('');
                self.licensingrequestModel().PostalAddress().Suburb('');
                self.licensingrequestModel().PostalAddress().TownCity('');
                self.licensingrequestModel().PostalAddress().CountryCode('');
                self.licensingrequestModel().PostalAddress().PostalCode('');
            }
            return true;
        };

        //Copy Details from Business Address to Postal Address
        self.selectedEx = function () {
            if ($('#chkportextension').is(':checked')) {
                //self.IsCodeEnable(false);
                self.IsPortSelectionEnable(true);
                self.IsRefNOEnable(true);
                self.IsCodeEnable(false);

            } else {
                self.IsPortSelectionEnable(true);
                self.IsRefNOEnable(false);
                self.IsCodeEnable(true);
                self.licensingrequestModel(new IPMSRoot.LicensingRequestModel(undefined));
            }
            return true;
        };

        // Fetching Licensingrequesttypes
        self.loadlicensingrequesttypes = function () {
            self.viewModelHelper.apiGet('api/LicensingRequestTypes', null, function (result) {
                self.licensingRequestReferenceData(new IPMSRoot.LicensingRequestReferenceData(result));
            }, null, null, false);
        }

        // Fetching All Ports List
        self.loadAllPortList = function () {
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
                self.PortReferenceData(new IPMSRoot.PortReferenceData(result));
            }, null, null, false);
        }

        // SaveLicensingRequest Used For Save the data from all controls into backend
        self.SaveLicensingRequest = function (LicensingRequestData) {

    
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($("#txtCaptachCode").val() != "") {

                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    toastr.warning("Please Fill All The Required Fields.", "Warning");
                }
            }
            else {
                $("#spanCaptachCode").text(validationMessage);
                var captachText = randString(6);
                self.CaptachText(captachText);
                toastr.warning("You Have Some Form Errors. Please Check Below.");
            }

            var Acpt = document.getElementsByName('AccChk');
            if (!Acpt[0].checked) {
                toastr.warning("Please Select The Checkbox That You Have Accepted And Read The Set Of Instructions In This Page.", "Warning");
                return;
            }
            else {
                if ($("#txtCaptachCode").val() != "") {

                    if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                        $("#spanCaptachCode").text('');
                    }
                    else {
                        $("#spanCaptachCode").text('* Invalid security code');
                        var captachText = randString(6);
                        self.CaptachText(captachText);
                        return;
                    }
                }
                else {
                    $("#spanCaptachCode").text(validationMessage);
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    return;
                }

                checkboxesAll = document.getElementsByName('Allport');
                if (checkboxesAll[0].checked == true) {
                    self.licensingrequestModel().licenseRequestPortsArr([]);
                    checkboxes = document.getElementsByName('port');
                    for (var i = 0, n = checkboxes.length; i < n; i++) {
                        if (!checkboxes[i].checked) {
                            self.licensingrequestModel().licenseRequestPortsArr.push(checkboxes[i].value);
                        }
                    }
                }

                var faxno = self.licensingrequestModel().FaxNo;
                self.licensingrequestModel().FaxNo = faxno().replace(/[-@()]/g, "");

                var telno = self.licensingrequestModel().TelephoneNo1;
                self.licensingrequestModel().TelephoneNo1 = telno().replace(/[-@()]/g, "");

                var celno = self.licensingrequestModel().AuthorizedContactPerson().CellularNo()
                self.licensingrequestModel().AuthorizedContactPerson().CellularNo = celno.replace(/[-@()]/g, "");

                self.viewModelHelper.apiPost('api/LicensingRequest', ko.mapping.toJSON(self.licensingrequestModel()),
                    function Message(data) {
                        toastr.success("Licensing Request Details Submitted Successfully.", "Licensing Request");
                        setTimeout(function () {
                            window.location = '/Account/Login';
                        }, 4000);
                    });
            }
        }

        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.warning("Please Select At Least One Document Type.", "Warning");
                return;
            }
            else {
                $("#spanHWPSfileToUpload").text("");
                self.isfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();
                self.licensingrequestModel().UploadedFiles([]);
                uploadedFiles = self.licensingrequestModel().UploadedFiles();
                var opmlFile = $('#fileToUpload')[0];

                if (opmlFile.files.length > 0) {
                    for (var i = 0; i < opmlFile.files.length; i++) {
                        var match = ko.utils.arrayFirst(self.licensingrequestModel().LicenseRequestDocuments(), function (item) {
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
                                // elem.IsAlreadyExists = false
                                var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                                if ($.inArray(extension, fileExtension) != -1) {
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.licensingrequestModel().UploadedFiles(uploadedFiles);
                                }
                                else {
                                    toastr.error("Please Upload The Files With Formats (PDF, word, excel, .PNG) only", "Error");
                                    return;
                                }
                                //uploadedFiles.push(elem);
                                //self.licensingrequestModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " File Size Is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The Selected File Already Exists.! Please Upload Another File.", "Error");
                            return;
                        }
                    }

                    var formData = new FormData();
                    $.each(self.licensingrequestModel().UploadedFiles(), function (key, val) {
                        formData.append(val.name, val.FileDetails);
                    });

                    var CategoryCode = $('#selUploadDocs option:selected').val();
                    if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                                var Adddoc = new IPMSROOT.LicenseRequestDocument();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                self.licensingrequestModel().LicenseRequestDocuments.push(Adddoc);
                            }));
                        });
                    }
                }
                else {
                    $("#spanHWPSfileToUpload").text('Please Select At Least Single File To Upload.');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs').val('');
                self.licensingrequestModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }
        //-------------------------------

        // Delete Document from The list
        self.DeleteDocument = function (documentRow) {
            self.licensingrequestModel().LicenseRequestDocuments.remove(documentRow);
        }

        // Validate the form
        self.ValidateForm = function (LicensingRequestData) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            
            var databaseList = ko.toJSON(self.licensingrequestList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(self.licensingrequestModel()));
            var flag = true;
            if ($('#chkportextension').is(':Unchecked')) {
                $.each(jsonObj, function (index, value) {
                    if (value.RegistrationNumber == formList.RegistrationNumber) {
                        $('#spanvregn').text('Already exists.! Please enter another number.');
                        flag = false;
                    }
                    if (value.VATNumber == formList.VATNumber) {
                        $('#spanvvatno').text('Already exists.! Please enter another number.');
                        flag = false;
                    }
                    if (value.IncomeTaxNumber == formList.IncomeTaxNumber) {
                        $('#spanvitno').text('Already exists.! Please enter another number.');
                        flag = false;
                    }
                    return;
                });
            }

            if (flag == true) {
                $('#spanvregn').text('');
                $('#spanvitno').text('');
                $('#spanvvatno').text('');
                if (self.licensingrequestModel().ViewModeForTabs() == 'documentupload') {
                    if (self.licensingrequestModel().LicenseRequestDocuments().length == 0) {
                        toastr.warning("Please Select At Least Single Document To Upload.");
                        return;
                    }
                    else { GoToTab3(self, LicensingRequestData); }
                }
                else {
                    checkboxes = document.getElementsByName('port');
                    var chk = 0;
                    for (var i = 0, n = checkboxes.length; i < n; i++) {
                        if (checkboxes[i].checked) {
                            chk = 1;
                        }
                    }

                    if (chk == 1) {
                        if (ValidateFormValues(LicensingRequestData) == true) {
                            if (self.viewModeForTabs() == "registration") {
                                self.IsReset(false);
                                GoToTab2(self, LicensingRequestData);
                            }
                            else {
                                self.IsReset(false);
                                GoToTab3(self, LicensingRequestData);
                            }
                        }
                    }
                    else {
                        toastr.warning("Please Select At Least One Port.", "Warning");
                        return;
                    }
                }
            }
            else {
                toastr.warning("Registration Number / Income Tax No. / VAT Number Are Already Exists.");
            }
        }

        // Tab Navigations
        self.GotoTab1 = function (licensingrequestData) {
            self.IsReset(true);
            if (self.viewModeForTabs() == 'registration') {
                self.viewModeForTabs('registration');
                self.licensingrequestModel().ViewModeForTabs('registration');
                return;
            }
            else {
                self.viewModeForTabs('registration');
                self.licensingrequestModel().ViewModeForTabs('registration');
                GoToTab1(self, licensingrequestData);
            }
        }

        // Tab Navigations
        self.GotoTab2 = function (licensingrequestData) {
            self.IsReset(false);
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            checkboxes = document.getElementsByName('port');
            var chk = 0;

            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].checked) {
                    chk = 1;
                }
            }
            if (chk == 1) {
                if (ValidateFormValues(licensingrequestData) == true) {
                    if (self.viewModeForTabs() == 'registration') {
                        self.viewModeForTabs('documentupload');
                        self.licensingrequestModel().ViewModeForTabs('documentupload');
                        GoToTab2(self, licensingrequestData);
                    }
                    else {
                        if (self.viewModeForTabs() == 'declaretion') {
                            self.viewModeForTabs('documentupload');
                            self.licensingrequestModel().ViewModeForTabs('documentupload');
                            GoToTab2(self, licensingrequestData);
                        }
                        else {
                            GoToTab2(self, licensingrequestData);
                            self.viewModeForTabs('documentupload');
                            self.licensingrequestModel().ViewModeForTabs('documentupload');
                        }
                    }
                }
            }
            else {
                GoToTab1(self, licensingrequestData);
                toastr.warning("Please Select At Least One Port.", "Warning");
                return;
            }
        }

        // Tab Navigations
        self.GotoTab3 = function (licensingrequestData) {
            
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            checkboxes = document.getElementsByName('port');
            var chk = 0;

            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].checked) {
                    chk = 1;
                }
            }
            if (chk == 1) {
                if (ValidateFormValues(licensingrequestData) == true) {

                    if (self.viewModeForTabs() == 'documentupload') {

                        if (self.licensingrequestModel().LicenseRequestDocuments().length == 0) {
                            toastr.warning("Please Select At Least Single Document To Upload.");
                            return;
                        }
                        else if (self.licensingrequestModel().LicenseRequestDocuments().length != 0) {
                            self.IsReset(false);
                            GoToTab3(self, licensingrequestData);
                            self.viewModeForTabs('declaretion');
                            self.licensingrequestModel().ViewModeForTabs('declaretion');
                        }
                    }
                    else
                        if (self.viewModeForTabs() == 'registration') {
                            if (self.licensingrequestModel().LicenseRequestDocuments().length != 0) {
                                self.IsReset(false);
                            GoToTab3(self, licensingrequestData);
                            self.viewModeForTabs('declaretion');
                            self.licensingrequestModel().ViewModeForTabs('declaretion');
                        }
                        else
                        {
                            toastr.warning("Please Select At Least Single Document To Upload.");
                            return;
                        }
                        }
                        else {
                            self.IsReset(false);
                            GoToTab3(self, licensingrequestData);
                            self.viewModeForTabs('declaretion');
                            self.licensingrequestModel().ViewModeForTabs('declaretion');
                        }
                }
            }
            else {
                GoToTab1(self, licensingrequestData);
                toastr.warning("Please Select At Least One Port.", "Warning");
                return;
            }
        }

        // Tab Navigations
        self.GoToPrevTab = function (licensingrequestData) {
            if (self.viewModeForTabs() == 'declaretion') {
                GoToTab2(self, licensingrequestData);
                return;
            }
            else if (self.viewModeForTabs() == 'documentupload') {
                self.IsReset(true);
                GoToTab1(self, licensingrequestData);
            }
        }

        self.GetDetailsExistingLicensingRequest = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if ($("#Refnumber").val() != "" || $("#Refnumber").val() != null || $("#Refnumber").val() != undefined) {
                var res = ValidateReferenceNo(data.ReferenceNo());

                if (self.isValidReferenceNo()) {
                    self.viewModelHelper.apiGet('api/LicensingRequest/GetLicensingRequestbyreferenceid', { id: data.ReferenceNo() },
                        function (result) {
                            self.licensingrequestModel(new IPMSRoot.LicensingRequestModel(result));
                            self.LicensePortWFArray(data ? (result.LicensePortWFArray ? $.map(result.LicensePortWFArray,
                                function (item) { return item }) : []) : []);
                        }, null, null, false);

                    $("#Refnumber").prop("disabled", true);
                    $("#Refnumber").attr("disabled", "disabled");
                    self.IsPortSelectionEnable(true);
                    self.IsCodeEnable(false);
                    self.Checkboxdisable(false);
                    self.Checkboxenabledisable(true);
                    $(".checkportextension").attr('checked', true);

                    TelephoneMasking = function (item) {
                        $("#TelephoneNo1").kendoMaskedTextBox({
                            mask: "(000)000-000-0000"
                        });
                        var varTelephoneNo1 = $("#TelephoneNo1").data("kendoMaskedTextBox");
                        self.TelephoneNo1(varTelephoneNo1.value());
                    }

                    FaxMasking = function (item) {
                        $("#FaxNo").kendoMaskedTextBox({
                            mask: "(000)000-000-0000"
                        });
                        var varFaxNo = $("#FaxNo").data("kendoMaskedTextBox");
                        self.FaxNo(varFaxNo.value());
                    }

                    CellularMasking = function (item) {
                        $("#CellularNo").kendoMaskedTextBox({
                            mask: "(000)000-000-0000"
                        });
                        var varCellularNo = $("#CellularNo").data("kendoMaskedTextBox");
                        self.CellularNo(varCellularNo.value());
                    }
                    //$.each(self.LicensePortWFArray(), function (key, value) {
                    //    $('#chkPort_' + column[0]).attr('checked', false);
                    //    $('#chkPort_' + column[0]).css('outline', '')
                    //});
                    $.each(self.LicensePortWFArray(), function (key, value) {
                        var column = value.split('@');

                        if (column[1] == 'WFSA') {
                            $('#chkPort_' + column[0]).attr('checked', true);
                            $('#chkPort_' + column[0]).prop('disabled', true);
                            $('#chkPort_' + column[0]).css('outline', '2px solid green');
                        }
                        else if (column[1] == 'NEW') {
                            var pt = column[0];

                            $('#chkPort_' + pt).prop('checked', true);
                            $('#chkPort_' + pt).prop('disabled', true);
                            $('#chkPort_' + pt).css('outline', '2px solid yellow');
                        }
                        else if (column[1] == 'REJ' || column[1] == 'WFRE') {
                            $('#chkPort_' + column[0]).prop('checked', false);
                        }
                    });
                }
                else {
                    toastr.warning("Invalid Reference Number.", "Warning");
                }
            }
            else {
                toastr.warning("Please Enter The Reference Number.", "Warning");
            }
        }

        ///Validate ReferenceNo
        ValidateReferenceNo = function (ReferenceNo) {
            
            if (ReferenceNo != null || ReferenceNo != "" | ReferenceNo != undefined) {
                self.viewModelHelper.apiGet('api/CheckReferenceNoExists', { ReferenceNo: ReferenceNo },
                      function (result) {
                          self.isValidReferenceNo(result);
                          return;
                      }, null, null, false);
            }
            else {
                self.isValidReferenceNo(false);
                return;
            }
        };

        self.ChangeReferenceNumber = function () {
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel());

            $(".checkportextension").attr('checked', true);
            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        // ResetLicensingRequest method is fires when press the Reset button and set the all fields data is in stage that in page load time
        self.ResetLicensingRequest = function (licensingrequest) {
            var notify = self.viewModeForTabs();
            var checkboxesAll = document.getElementsByName('Allport');
            checkboxesAll[0].checked = false;

            //BugId: 5501
            $('#spanvitno').text('');
            $('#spanvvatno').text('');
            $('#spanvregn').text('');
            self.licensingrequestModel().reset();
            self.viewModeForTabs(notify);
            self.licensingrequestModel().ViewModeForTabs(notify);
            ko.validation.group(self.licensingrequestModel()).showAllMessages(false);

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        self.resetreferenceno = function (licensingrequest) {
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel(undefined));
            window.location = '/LicensingRequest';
        }

        self.CancelPortExtension = function () {
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel());

            $(".checkportextension").attr('checked', true);
            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        // CancelLicensingRequest method is fires when press the cancel button and set the all fields data is clear  move to list
        self.CancelLicensingRequest = function (licensingrequest) {
            self.licensingrequestModel().pendingTasks.removeAll();
            if (viewDetail == true) {
                self.IsPortSelectionEnable(false);
                window.location = '/Welcome';
            }
            else {;
                window.location = '/Account/Login';
            }
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber, Type) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            if (Type == 'Telephone') {
                var validPhoneNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Telephone Number.");
                    validPhoneNumber++;
                    return validPhoneNumber;
                }
                else {
                    return validPhoneNumber;
                }
            }

            if (Type == 'Fax') {
                var validFaxNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Fax Number.");
                    validFaxNumber++;
                    return validFaxNumber;
                }
                else {
                    return validFaxNumber;
                }
            }

            if (Type == 'CellularNo') {
                var validCellularNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Cellular Number.");
                    validCellularNumber++;
                    return validCellularNumber;
                }
                else {
                    return validCellularNumber;
                }
            }

        }

        // Validate the form
        function ValidateFormValues(licensingrequestData) {
            self.licensingrequestModel().validationEnabled(true);
            

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var result = true;
            var CurrentlicensingrequestModel = self.licensingrequestModel();
            self.AuthorizedContactPersonValidation = ko.observable(CurrentlicensingrequestModel.AuthorizedContactPerson());

            self.AuthorizedContactPersonValidation().errors = ko.validation.group(self.AuthorizedContactPersonValidation());
            var errors2 = self.AuthorizedContactPersonValidation().errors().length;

            self.licensingrequestDataValidation = ko.observable(licensingrequestData);
            self.licensingrequestDataValidation().errors = ko.validation.group(self.licensingrequestDataValidation());
            var errors = self.licensingrequestDataValidation().errors().length;

            //AuthorizedContactPerson
            if (ko.validation.group(self.AuthorizedContactPersonValidation())().length > 0) {
                ko.validation.group(self.AuthorizedContactPersonValidation()).showAllMessages(true);
                result = false;
            }

            if (ko.validation.group(self.licensingrequestModel())().length > 0) {
                ko.validation.group(self.licensingrequestModel()).showAllMessages(true);
                result = false;

                if (self.licensingrequestModel().ViewModeForTabs() == "registration") {
                    GoToTab1(self, licensingrequestData);
                }
                else if (self.licensingrequestModel().ViewModeForTabs() == "documentupload") {
                    GoToTab2(self, licensingrequestData);
                }
                else {
                    GoToTab3(self, licensingrequestData);
                }
            }

            var errors = 0;
            var filterTelephoneNumber = self.licensingrequestModel().TelephoneNo1;
            filterTelephoneNumber = filterTelephoneNumber().replace(/(\)|\()|_|-+/g, '');
            if (filterTelephoneNumber != null || filterTelephoneNumber != '' || filterTelephoneNumber != undefined) {
                var validTelephoneNumber = 0;

                if (filterTelephoneNumber.length != 0) {
                    validTelephoneNumber = CheckPhoneValidation(filterTelephoneNumber, 'Telephone');
                    if (validTelephoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
                if (filterTelephoneNumber.length == 13) {
                    var validNo = parseInt(filterTelephoneNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Telephone Number");
                        errors = errors + 1;
                    }

                }
            }

            var filterFaxNumber = self.licensingrequestModel().FaxNo;
            filterFaxNumber = filterFaxNumber().replace(/(\)|\()|_|-+/g, '');
            if (filterFaxNumber != null || filterFaxNumber != '' || filterFaxNumber != undefined) {
                var validFaxNumber = 0;

                if (filterFaxNumber.length != 0) {
                    validFaxNumber = CheckPhoneValidation(filterFaxNumber, 'Fax');
                    if (validFaxNumber > 0) {
                        errors = errors + 1;
                    }
                }
                if (filterFaxNumber.length == 13) {
                    var validNo = parseInt(filterFaxNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Fax Number");
                        errors = errors + 1;
                    }

                }
            }

            var filterCellularNumber = self.licensingrequestModel().AuthorizedContactPerson().CellularNo;
            filterCellularNumber = filterCellularNumber().replace(/(\)|\()|_|-+/g, '');
            if (filterCellularNumber != null || filterCellularNumber != '' || filterCellularNumber != undefined) {
                var validCellularNumber = 0;

                if (filterCellularNumber.length != 0) {
                    validCellularNumber = CheckPhoneValidation(filterCellularNumber, 'CellularNo');
                    if (validCellularNumber > 0) {
                        errors = errors + 1;
                    }
                }
                if (filterCellularNumber.length == 13) {
                    var validNo = parseInt(filterCellularNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Cellular Number");
                        errors = errors + 1;
                    }

                }
            }

            if (errors > 0) {
                result = false;
            }

            //////Validation for the input fields where .extend is not working 

            if ($("#NumStreet").val() == null || $("#NumStreet").val() == '' || $("#NumStreet").val() == undefined) {
                $("#errNumStreetMsg").text(validationMessage);
                self.isNumStreetMsg(true);
                result = false;
            }
            else {
                $("#errNumStreetMsg").text('');
                self.isNumStreetMsg(false);
            }

            if ($("#Suburb").val() == null || $("#Suburb").val() == '' || $("#Suburb").val() == undefined) {
                $("#errSuburbMsg").text(validationMessage);
                self.isSuburbMsg(true);
                result = false;
            }
            else {
                $("#errSuburbMsg").text('');
                self.isSuburbMsg(false);
            }

            if ($("#TownCity").val() == null || $("#TownCity").val() == '' || $("#TownCity").val() == undefined) {
                $("#errTownCityMsg").text(validationMessage);
                self.isTownCityMsg(true);
                result = false;
            }
            else {
                $("#errTownCityMsg").text('');
                self.isTownCityMsg(false);
            }

            if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                $("#ResidentialAddressCountryMsg").text(validationMessage);
                self.isResidentialAddressCountry(true);
                result = false;
            }
            else {
                $("#ResidentialAddressCountryMsg").text('');
                self.isResidentialAddressCountry(false);
            }

            if ($("#PostalCode").val() == null || $("#PostalCode").val() == '' || $("#PostalCode").val() == undefined) {
                $("#errPostalCodeMsg").text(validationMessage);
                self.isPostalCodeMsg(true);
                result = false;
            }
            else {
                $("#errPostalCodeMsg").text('');
                self.isPostalCodeMsg(false);
            }

            if ($("#NumStreet2").val() == null || $("#NumStreet2").val() == '' || $("#NumStreet2").val() == undefined) {
                $("#errNumStreet2Msg").text(validationMessage);
                self.isNumStreet2Msg(true);
                result = false;
            }
            else {
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
            }

            if ($("#Suburb2").val() == null || $("#Suburb2").val() == '' || $("#Suburb2").val() == undefined) {
                $("#errSuburb2Msg").text(validationMessage);
                self.isSuburb2Msg(true);
                result = false;
            }
            else {
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
            }

            if ($("#TownCity2").val() == null || $("#TownCity2").val() == '' || $("#TownCity2").val() == undefined) {
                $("#errTownCity2Msg").text(validationMessage);
                self.isTownCity2Msg(true);
                result = false;
            }
            else {
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
            }

            if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                $("#PostalAddressCountryMsg").text(validationMessage);
                self.isPostalAddressCountry(true);
                result = false;
            }
            else {
                $("#PostalAddressCountryMsg").text('');
                self.isPostalAddressCountry(false);
            }

            if ($("#PostalCode2").val() == null || $("#PostalCode2").val() == '' || $("#PostalCode2").val() == undefined) {
                $("#errPostalCode2Msg").text(validationMessage);
                self.isPostalCode2Msg(true);
                result = false;
            }
            else {
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);
            }

            if (!result) {

                self.licensingrequestModel().showAllMessages(true);
                self.AuthorizedContactPersonValidation().showAllMessages(true);
                toastr.warning("Please Fill All the Required Fields.", "Warning");
            }

            ///////////////////////////////////////////////////////////////////////////
            return result;
        }

        ValidRegn = function (data, event) {

            var databaseList = ko.toJSON(self.licensingrequestList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.RegistrationNumber == formList.RegistrationNumber) {
                    $('#spanvregn').text('Already exists.! Please enter another number.');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanvregn').text('');
            }
        }

        ValidVatNo = function (data, event) {
            var databaseList = ko.toJSON(self.licensingrequestList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;
            $.each(jsonObj, function (index, value) {
                if (value.VATNumber == formList.VATNumber) {
                    $('#spanvvatno').text('Already exists.! Please enter another number');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanvvatno').text('');
            }
        }

        ValidItNo = function (data, event) {
            var databaseList = ko.toJSON(self.licensingrequestList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;
            $.each(jsonObj, function (index, value) {
                if (value.IncomeTaxNumber == formList.IncomeTaxNumber) {
                    $('#spanvitno').text('Already exists.! Please enter another number');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanvitno').text('');
            }
        }

        //////Validation for the input fields where .extend is not working 
        NumStreetChange = function (item) {

            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errNumStreetMsg").text(validationMessage);
                self.isNumStreetMsg(true);
            }
            else {
                $("#errNumStreetMsg").text('');
                self.isNumStreetMsg(false);
            }
        }

        SuburbChange = function (item) {

            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errSuburbMsg").text(validationMessage);
                self.isSuburbMsg(true);
            }
            else {
                $("#errSuburbMsg").text('');
                self.isSuburbMsg(false);
            }
        }

        TownCityChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errTownCityMsg").text(validationMessage);
                self.isTownCityMsg(true);
            }
            else {
                $("#errTownCityMsg").text('');
                self.isTownCityMsg(false);
            }
        }

        //ResidentialAddressCountryChange = function (item) {
        //    if (item.value == null || item.value == '' || item.value == undefined) {
        //        $("#ResidentialAddressCountryMsg").text(validationMessage);
        //        self.isResidentialAddressCountry(true);
        //        alert(1);
        //    }
        //    else {
        //        $("#ResidentialAddressCountryMsg").text('');
        //        self.isResidentialAddressCountry(false);
        //    }
        //}

        PostalCodeChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errPostalCodeMsg").text(validationMessage);
                self.isPostalCodeMsg(true);
            }
            else {
                $("#errPostalCodeMsg").text('');
                self.isPostalCodeMsg(false);
            }
        }

        NumStreet2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errNumStreet2Msg").text(validationMessage);
                self.isNumStreet2Msg(true);
            }
            else {
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
            }
        }

        Suburb2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errSuburb2Msg").text(validationMessage);
                self.isSuburb2Msg(true);
            }
            else {
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
            }
        }

        TownCity2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errTownCity2Msg").text(validationMessage);
                self.isTownCity2Msg(true);
            }
            else {
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
            }
        }

        //PostalAddressCountryChange = function (item) {
        //    if (item.value == null || item.value == '' || item.value == undefined) {
        //        $("#PostalAddressCountryMsg").text(validationMessage);
        //        self.isPostalAddressCountry(true);
        //    }
        //    else {
        //        $("#PostalAddressCountryMsg").text('');
        //        self.isPostalAddressCountry(false);
        //    }
        //}

        PostalCode2Change = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#errPostalCode2Msg").text(validationMessage);
                self.isPostalCode2Msg(true);
            }
            else {
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);
            }
        }

        toggle = function (ctrl) {
            self.licensingrequestModel().licenseRequestPortsArr([]);
            checkboxes = document.getElementsByName('port');

            if (ctrl.checked) {
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    checkboxes[i].checked = ctrl.checked;
                    self.licensingrequestModel().licenseRequestPortsArr.push(checkboxes[i].value);
                }
            }
            else {
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    checkboxes[i].checked = ctrl.checked;
                }
            }
        }

        self.Initialize();

        //selectedChoices method is set the all details in BuisinessAddress put postal address fields the pilotexemption request screen 
        self.selectedChoices = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                self.licensingrequestModel().PostalAddress().NumberStreet((self.licensingrequestModel().BusinessAddress().NumberStreet()));
                $("#NumStreet2").prop("disabled", true);
                $("#NumStreet2").attr("disabled", "disabled");
                self.licensingrequestModel().PostalAddress().Suburb((self.licensingrequestModel().BusinessAddress().Suburb()));
                $("#Suburb2").prop("disabled", true);
                $("#Suburb2").attr("disabled", "disabled");
                self.licensingrequestModel().PostalAddress().TownCity((self.licensingrequestModel().BusinessAddress().TownCity()));
                $("#TownCity2").prop("disabled", true);
                $("#TownCity2").attr("disabled", "disabled");
                self.licensingrequestModel().PostalAddress().CountryCode((self.licensingrequestModel().BusinessAddress().CountryCode()));
                $("#PostalAddressCountry").prop("disabled", true);
                $("#PostalAddressCountry").attr("disabled", "disabled");                
                self.licensingrequestModel().PostalAddress().PostalCode((self.licensingrequestModel().BusinessAddress().PostalCode()));
                $("#PostalCode2").prop("disabled", true);
                $("#PostalCode2").attr("disabled", "disabled");
                $("#errNumStreet2Msg").text('');
                self.isNumStreet2Msg(false);
                $("#errSuburb2Msg").text('');
                self.isSuburb2Msg(false);
                $("#errTownCity2Msg").text('');
                self.isTownCity2Msg(false);
                $("#errPostalCode2Msg").text('');
                self.isPostalCode2Msg(false);

                if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                    $("#ResidentialAddressCountryMsg").text(validationMessage);
                    self.isResidentialAddressCountry(true);
                }
                else {
                    $("#ResidentialAddressCountryMsg").text('');
                    self.isResidentialAddressCountry(false);
                }

                if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                    $("#PostalAddressCountryMsg").text(validationMessage);
                    self.isPostalAddressCountry(true);
                }
                else {
                    $("#PostalAddressCountryMsg").text('');
                    self.isPostalAddressCountry(false);
                }
            }
            else {
                self.licensingrequestModel().PostalAddress().NumberStreet('');
                self.licensingrequestModel().PostalAddress().Suburb('');
                self.licensingrequestModel().PostalAddress().TownCity('');
                self.licensingrequestModel().PostalAddress().CountryCode('');
                self.licensingrequestModel().PostalAddress().PostalCode('');
                $("#NumStreet2").prop("disabled", false);
                $("#NumStreet2").attr("disabled", false);
                $("#Suburb2").prop("disabled", false);
                $("#Suburb2").attr("disabled", false);
                $("#TownCity2").prop("disabled", false);
                $("#TownCity2").attr("disabled", false);
                $("#PostalCode2").prop("disabled", false);
                $("#PostalCode2").attr("disabled", false);
                $("#PostalAddressCountry").prop("disabled", false);
                $("#PostalAddressCountry").attr("disabled", false);
                $("#errNumStreet2Msg").text('Please Enter Number and Street');
                $("#errNumStreet2Msg").text(validationMessage);
                self.isNumStreet2Msg(true);
                $("#errSuburb2Msg").text(validationMessage);
                self.isSuburb2Msg(true);
                $("#errTownCity2Msg").text(validationMessage);
                self.isTownCity2Msg(true);
                $("#PostalAddressCountryMsg").text(validationMessage);
                self.isPostalAddressCountry(true);
                $("#errPostalCode2Msg").text(validationMessage);
                self.isPostalCode2Msg(true);
            }
            return true;
        };

        //AddessKeyupEvents method is set the all details in BuisinessAddress put postal address fields  on key up event the pilotexemption request screen 
        self.AddessKeyupEvents = function () {
            if ($('#chkBuisinessAddress').is(':checked')) {
                $('#NumStreet').keyup(function () { $('#NumStreet2').val($(this).val()); });
                $('#Suburb').keyup(function () { $('#Suburb2').val($(this).val()); });
                $('#TownCity').keyup(function () { $('#TownCity2').val($(this).val()); });
                $('#ResidentialAddressCountry').keyup(function () { $('#PostalAddressCountry').val($(this).val()); });
                $('#PostalCode').keyup(function () { $('#PostalCode2').val($(this).val()); });
            }
            else {
                $('#NumStreet').keyup();
                $('#Suburb').keyup();
                $('#TownCity').keyup();
                $('#ResidentialAddressCountry').keyup();
                $('#PostalCode').keyup();
                return false;
            }
        };

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
                $("#spanCaptachCode").text(validationMessage);
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }

        TelephoneMasking = function (item) {
            $("#TelephoneNo1").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var varTelephoneNo1 = $("#TelephoneNo1").data("kendoMaskedTextBox");
            self.TelephoneNo1(varTelephoneNo1.value());
        }

        FaxMasking = function (item) {
            $("#FaxNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var varFaxNo = $("#FaxNo").data("kendoMaskedTextBox");
            self.FaxNo(varFaxNo.value());
        }

        CellularMasking = function (item) {
            $("#CellularNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var varCellularNo = $("#CellularNo").data("kendoMaskedTextBox");
            self.CellularNo(varCellularNo.value());
        }

        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }
    }

    IPMSRoot.LicensingRequestViewModel = LicensingRequestViewModel;

}(window.IPMSROOT));

// Port Selection Check box for single
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

// Tab Navigations - GoToTab-1
function GoToTab1(self, licensingrequestData) {
    self.viewModeForTabs('registration');
    self.licensingrequestModel().ViewModeForTabs('registration');
    self.isSubmitVisible(false);
    if (self.isReset()) {
        self.isSaveVisible(true);
    }

    self.isGoBackVisible(false);
    var index = 0;
    HandleProgressBarAndActiveTab(index);
    if (licensingrequestData.viewDetail() == true) {
        //self.isSaveVisible(false);
        self.IsPortSelectionEnable(false);
        self.isSubmitVisible(false);
        self.IsReset(false);
        //self.isGoBackVisible(false);
    }
}

// Tab Navigations - GoToTab-2
function GoToTab2(self, licensingrequestData) {
    self.viewModeForTabs('documentupload');
    self.licensingrequestModel().ViewModeForTabs('documentupload');
    self.isSubmitVisible(false);
    if (self.isReset()) {
        self.isSaveVisible(true);
    }
    self.isGoBackVisible(true);
    var index = 1;
    HandleProgressBarAndActiveTab(index);
    if (licensingrequestData.viewDetail() == true) {
        // self.isSaveVisible(false);
        self.IsPortSelectionEnable(false);
        self.isSubmitVisible(false);
        self.IsReset(false);
        // self.isGoBackVisible(false);
    }
}

// Tab Navigations - GoToTab-3
function GoToTab3(self, licensingrequestData) {
    self.viewModeForTabs('declaretion');
    self.licensingrequestModel().ViewModeForTabs('declaretion');
    if (self.isReset()) {
        self.isSubmitVisible(true);
    }

    self.isSaveVisible(false);
    self.isGoBackVisible(true);
    var index = 2;
    HandleProgressBarAndActiveTab(index);
    if (licensingrequestData.viewDetail() == true) {
        self.isSaveVisible(false);
        self.IsPortSelectionEnable(false);
        self.isSubmitVisible(false);
        self.IsReset(false);
        //self.isGoBackVisible(false);
    }
}

// Active Process Bar
function HandleProgressBarAndActiveTab(index) {
    var total = $('#ulTabs').find('li').length;
    if (total == 0)
        total = 3;

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
