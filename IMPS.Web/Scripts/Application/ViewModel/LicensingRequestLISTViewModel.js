(function (IPMSRoot) {
    var LicensingRequestLISTViewModel = function () {
        var self = this;
        $('#spnTitle').html("Licensing Request");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.licensingrequestModel = ko.observable(new IPMSROOT.LicensingRequestModel());
        self.viewMode = ko.observable();
        self.viewMode = ko.observable();
        self.licensingrequestList = ko.observableArray();
        self.isSaveVisible = ko.observable(false);
        self.licensingRequestReferenceData = ko.observable();
        self.PortReferenceData = ko.observable();
        self.craftReferenceData = ko.observable();
        self.viewModeForTabs = ko.observable();
        self.viewMode = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.isGoBackVisible = ko.observable(false);
        self.isSubmitVisible = ko.observable(false);
        self.isUpdateVisible = ko.observable(false);
        self.isReset = ko.observable(false);
        self.isCancelVisible = ko.observable(true);
        self.isVisible = ko.observable(true);
        self.ReferenceData = ko.observable();
        self.isfileToUpload = ko.observable(false);
        self.isDisplayDelete = ko.observable(true);
        self.termsConditions = ko.observable(false);
        self.isviewmodedisable = ko.observable(false);
        self.LicensePortWFArray = ko.observableArray([]);
        //Validation Visible
        self.isNumStreetMsg = ko.observable(false);
        self.isSuburbMsg = ko.observable(false);
        self.isTownCityMsg = ko.observable(false);
        self.isPostalCodeMsg = ko.observable(false);
        self.isNumStreet2Msg = ko.observable(false);
        self.isSuburb2Msg = ko.observable(false);
        self.isTownCity2Msg = ko.observable(false);
        self.isPostalCode2Msg = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.isfileToUpload = ko.observable(false);

        self.isResidentialAddressCountry = ko.observable(false);
        self.isPostalAddressCountry = ko.observable(false);
        self.CountriesList = ko.observableArray();

        var validationMessage = '* This field is required.';

        // Initialize method is fires in  pageload Initialization mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.viewModeForTabs('registration');
            self.licensingrequestModel().ViewModeForTabs('registration');
            self.LoadLicenseingRequestList();
            self.loadlicensingrequesttypes();
            self.loadAllPortList();
            self.LoadCountries();
            self.licensingrequestModel(new IPMSROOT.LicensingRequestModel(undefined));
            self.GetFileSizeConfigValue();
            self.viewMode('List');
        }

        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue', null,
                function (result) {
                    self.fileSizeConfigValue(result);
                    ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
                });
        }

        self.LoadLicenseingRequestbyID = function (data) {
            self.viewModelHelper.apiGet('api/LicensingRequest/GetLicensingRequest', { id: data.LicenseRequestID() }, function (result) {
                         self.licensingrequestModel(new IPMSRoot.LicensingRequestModel(result));
                         self.LicensePortWFArray(data ? (result.LicensePortWFArray ? $.map(result.LicensePortWFArray, function (item) { return item }) : []) : []);
                     }, null, null, false);

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

        //Countries list from Vessel Nationality
        self.LoadCountries = function () {
            self.viewModelHelper.apiGet('api/SubCategory/GetCountriesList', null, function (result) {

                self.CountriesList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Countries(item);
                }));
            });
        }

        self.LoadLicenseingRequestList = function () {
            self.viewModelHelper.apiGet('api/LicensingRequestList', null,
                 function (result) {
                     self.licensingrequestList(ko.utils.arrayMap(result, function (item) {
                         return new IPMSRoot.LicensingRequestModel(item);
                     }));
                 });
        }

        self.EditLicensingRequest = function (licensingRequest) {
            self.viewMode('Form');
            self.isviewmodedisable(true);
            self.isReset(true);
            self.IsCodeEnable(true);
            self.isUpdateVisible(true);
            self.isGoBackVisible(false);
            //self.isSubmitVisible(true);
            self.loadlicensingrequesttypes();
            self.loadAllPortList();
            self.LoadLicenseingRequestbyID(licensingRequest);
            $('#AccChk').attr('checked', true);

            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);

            var index = 0;
            HandleProgressBarAndActiveTab(index);

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var phone = $("#TelephoneNo1").data("kendoMaskedTextBox");
            self.licensingrequestModel().TelephoneNo1(phone.value());

            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var faxno = $("#FaxNo").data("kendoMaskedTextBox");

            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var celno = $("#CellularNo").data("kendoMaskedTextBox");

            $('#spnTitle').html("Update Licensing Request");
        }

        // ViewLicensingRequest method is fires when View button press in View mode
        self.ViewLicensingRequest = function (licensingRequest) {
            self.viewMode('Form');
            self.isviewmodedisable(false);
            self.isGoBackVisible(false);
            self.loadlicensingrequesttypes();
            self.loadAllPortList();
            self.LoadLicenseingRequestbyID(licensingRequest);
            self.IsCodeEnable(false);
            self.isReset(false);
            self.isSubmitVisible(false);
            self.isSaveVisible(false);
            self.isUpdateVisible(true);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            $('#AccChk').attr('checked', true);
            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var phone = $("#TelephoneNo1").data("kendoMaskedTextBox");
            self.licensingrequestModel().TelephoneNo1(phone.value());

            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var faxno = $("#FaxNo").data("kendoMaskedTextBox");

            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var celno = $("#CellularNo").data("kendoMaskedTextBox");

            $('#spnTitle').html("View Licensing Request");
        }

        // Fetching Licensingrequesttypes
        self.loadlicensingrequesttypes = function () {
            self.viewModelHelper.apiGet('api/LicensingRequestTypes',
                   null,
                 function (result) {
                     self.licensingRequestReferenceData(new IPMSRoot.LicensingRequestReferenceData(result));
                 }, null, null, false);
        }

        self.SaveLicensingRequest = function (LicensingRequestData) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            //var Acpt = document.getElementsByName('AccChk');
            //if (!Acpt[0].checked) {
            //    toastr.warning("Please accept that have read the instructions set out on this instructions page.");
            //    return;
            //}
            //else {
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
                var faxno = LicensingRequestData.FaxNo();
                self.licensingrequestModel().FaxNo = faxno.replace(/[-@()]/g, "");

                var telno = LicensingRequestData.TelephoneNo1();
                self.licensingrequestModel().TelephoneNo1 = telno.replace(/[-@()]/g, "");

                var celno = LicensingRequestData.AuthorizedContactPerson().CellularNo()
                self.licensingrequestModel().AuthorizedContactPerson().CellularNo = celno.replace(/[-@]/g, "");

                self.viewModelHelper.apiPut('api/LicensingRequest', ko.mapping.toJSON(self.licensingrequestModel()),
                    function Message(data) {
                        toastr.success("Licensing request details submitted successfully.", "Licensing Request");
                        self.viewMode('List');
                        $('#spnTitle').html("Licensing Request");
                    });
          //  }

        }


        ////-------------------------------
        // Upload Documents add selected Document in List
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.warning("Please select document Type.", "Warning");
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
                                //elem.IsAlreadyExists = false
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
                                    toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                    return;
                                }
                                //uploadedFiles.push(elem);
                                //self.licensingrequestModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The selected file already exists.! Please upload another file.", "Error");
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
                    $("#spanHWPSfileToUpload").text('Please select file to upload.');
                    self.isfileToUpload(true);
                }
                $('#selUploadDocs').val('');
                self.licensingrequestModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }
        // Delete Document from The list
        self.DeleteDocument = function (documentRow) {
            self.licensingrequestModel().LicenseRequestDocuments.remove(documentRow);
        }

        // Validate the form
        self.ValidateForm = function (LicensingRequestData) {
            if (self.licensingrequestModel().ViewModeForTabs() == 'documentupload') {
                if (self.licensingrequestModel().LicenseRequestDocuments().length == 0) {
                    toastr.warning("Please select at least one document to upload.");
                    return;
                }
                else {
                    GoToTab1(self, LicensingRequestData);
                }
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
                            GoToTab2(self, LicensingRequestData);
                        }
                        else {
                            GoToTab1(self, LicensingRequestData);
                        }
                    }
                }
                else {
                    toastr.warning("Please select at least one port.");
                    return;
                }
            }
        }

        // Tab Navigations
        self.GotoTab1 = function (licensingrequestData) {

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
                    else
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
            else {
                GoToTab1(self, licensingrequestData);
                toastr.warning("Please select at least one port.");
                return;
            }
        }

        // Tab Navigations
        //self.GotoTab3 = function (licensingrequestData) {
        //    checkboxes = document.getElementsByName('port');
        //    var chk = 0;

        //    for (var i = 0, n = checkboxes.length; i < n; i++) {
        //        if (checkboxes[i].checked) {
        //            chk = 1;
        //        }
        //    }
        //    if (chk == 1) {

        //        if (ValidateFormValues(licensingrequestData) == true) {

        //            if (self.viewModeForTabs() == 'documentupload') {

        //                if (self.licensingrequestModel().LicenseRequestDocuments().length == 0) {
        //                    toastr.warning("Please select at least one document to upload.");
        //                    return;
        //                }
        //                else {
        //                    GoToTab3(self, licensingrequestData);
        //                    self.viewModeForTabs('declaretion');
        //                    self.licensingrequestModel().ViewModeForTabs('declaretion');
        //                }
        //            }
        //            else
        //                if (self.viewModeForTabs() == 'registration') {
        //                    GoToTab3(self, licensingrequestData);
        //                    self.viewModeForTabs('declaretion');
        //                    self.licensingrequestModel().ViewModeForTabs('declaretion');
        //                }
        //                else {
        //                    GoToTab3(self, licensingrequestData);
        //                    self.viewModeForTabs('declaretion');
        //                    self.licensingrequestModel().ViewModeForTabs('declaretion');
        //                }
        //        }
        //    }
        //    else {
        //        GoToTab1(self, licensingrequestData);
        //        toastr.warning("Please select at least one port.");
        //        return;
        //    }
        //}

        // Tab Navigations
        self.GoToPrevTab = function (licensingrequestData) {
            if (self.viewModeForTabs() == 'documentupload') {
                GoToTab1(self, licensingrequestData);
                return;
            }
            else if (self.viewModeForTabs() == 'registration') {
                GoToTab2(self, licensingrequestData);
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
                    toastr.warning("Invalid Telephone number.");
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
                    toastr.warning("Invalid Fax number.");
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
                    toastr.warning("Invalid Cellular number.");
                    validCellularNumber++;
                    return validCellularNumber;
                }
                else {
                    return validCellularNumber;
                }
            }
        }

        //// Validate the form
        function ValidateFormValues(licensingrequestData) {
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

            if (ko.validation.group(self.licensingrequestDataValidation())().length > 0) {
                ko.validation.group(self.licensingrequestModel()).showAllMessages(true);
                result = false;

                if (self.licensingrequestModel().ViewModeForTabs() == "registration") {
                    GoToTab1(self, licensingrequestData);
                }
                else if (self.licensingrequestModel().ViewModeForTabs() == "documentupload") {
                    GoToTab2(self, licensingrequestData);
                }
                //else {
                //    GoToTab3(self, licensingrequestData);
                //}
            }

            var errors = 0;
            var filterTelephoneNumber = licensingrequestData.TelephoneNo1();
            if (filterTelephoneNumber != null || filterTelephoneNumber != '' || filterTelephoneNumber != undefined) {
                var validTelephoneNumber = 0;

                if (filterTelephoneNumber.length != 0) {
                    validTelephoneNumber = CheckPhoneValidation(filterTelephoneNumber, 'Telephone');
                    if (validTelephoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterFaxNumber = licensingrequestData.FaxNo();
            if (filterFaxNumber != null || filterFaxNumber != '' || filterFaxNumber != undefined) {
                var validFaxNumber = 0;

                if (filterFaxNumber.length != 0) {
                    validFaxNumber = CheckPhoneValidation(filterFaxNumber, 'Fax');
                    if (validFaxNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterCellularNumber = licensingrequestData.AuthorizedContactPerson().CellularNo();
            if (filterCellularNumber != null || filterCellularNumber != '' || filterCellularNumber != undefined) {
                var validCellularNumber = 0;

                if (filterCellularNumber.length != 0) {
                    validCellularNumber = CheckPhoneValidation(filterCellularNumber, 'CellularNo');
                    if (validCellularNumber > 0) {
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
                toastr.warning("Please fill in all required fields.", "Warning");
            }
            ///////////////////////////////////////////////////////////////////////////

            return result;
        }

        // ResetLicensingRequest method is fires when press the Reset button and set the all fields data is in stage that in page load time
        self.ResetLicensingRequest = function (licensingrequest) {
        
            var notify = self.viewModeForTabs();
            var checkboxesAll = document.getElementsByName('Allport');
            checkboxesAll[0].checked = false;
            self.licensingrequestModel().reset();
            self.viewModeForTabs(notify);
            self.licensingrequestModel().ViewModeForTabs(notify);
            ko.validation.group(self.licensingrequestModel()).showAllMessages(false);

            $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        // CancelLicensingRequest method is fires when press the cancel button and set the all fields data is clear  move to list
        self.CancelLicensingRequest = function (licensingrequest) {
            self.licensingrequestModel().reset();
            self.viewModeForTabs('registration');
            self.licensingrequestModel().ViewModeForTabs('registration');
            self.viewMode('List');
            $('#spnTitle').html("Licensing Request");
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
                    $('#spanvvatno').text('Already exists.! Please enter another number.');
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
                    $('#spanvitno').text('Already exists.! Please enter another number.');
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
            //alert('event');
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

        //ResidentialAddressCountryChange = function (item) {
        //    if (item.value == null || item.value == '' || item.value == undefined) {
        //        $("#ResidentialAddressCountryMsg").text(validationMessage);
        //        self.isResidentialAddressCountry(true);
        //    }
        //    else {
        //        $("#ResidentialAddressCountryMsg").text('');
        //        self.isResidentialAddressCountry(false);
        //    }
        //}

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

        // Fetching All Ports List
        self.loadAllPortList = function () {
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
                      self.PortReferenceData(new IPMSRoot.PortReferenceData(result));
                  }, null, null, false);
        }

        self.Initialize();

        //selectedChoices method is set the all details in BuisinessAddress put postal address fields the pilotexemption request screen 
        self.selectedChoices = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                self.licensingrequestModel().PostalAddress().NumberStreet((self.licensingrequestModel().BusinessAddress().NumberStreet()));
                self.licensingrequestModel().PostalAddress().Suburb((self.licensingrequestModel().BusinessAddress().Suburb()));
                self.licensingrequestModel().PostalAddress().TownCity((self.licensingrequestModel().BusinessAddress().TownCity()));
                self.licensingrequestModel().PostalAddress().CountryCode((self.licensingrequestModel().BusinessAddress().CountryCode()));
                self.licensingrequestModel().PostalAddress().PostalCode((self.licensingrequestModel().BusinessAddress().PostalCode()));
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
    }


    //    };

        self.viewWorkFlow = function (license) {
            var workflowinstanceId = license.WorkflowInstanceID();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {
                  self.licensingrequestModel(new IPMSROOT.LicensingRequestModel());
                  self.licensingrequestModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

    //Preventing Backspace
    PreventDrop = function (ev) {
        return self.validationHelper.PreventDrop(event);
    }

    IPMSRoot.LicensingRequestLISTViewModel = LicensingRequestLISTViewModel;

}(window.IPMSROOT));

//  Port Selection Check box for All
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

// Tab Navigations - GoToTab-1
function GoToTab1(self, licensingrequestData) {
    self.viewModeForTabs('registration');
    self.licensingrequestModel().ViewModeForTabs('registration');
   // self.isSaveVisible(false);
    self.isGoBackVisible(false);
    self.isUpdateVisible(true);
    var index = 0;
    HandleProgressBarAndActiveTab(index);
    if (self.isviewmodedisable() == true) {
        self.isGoBackVisible(false);
        self.isUpdateVisible(true);
        self.isSubmitVisible(false);
    }
}

// Tab Navigations - GoToTab-2
function GoToTab2(self, licensingrequestData) {
    self.viewModeForTabs('documentupload');
    self.licensingrequestModel().ViewModeForTabs('documentupload');
    self.isGoBackVisible(true);
    self.isUpdateVisible(false);
    var index = 1;
    HandleProgressBarAndActiveTab(index);
    if (self.isviewmodedisable() == true) {
        self.isGoBackVisible(true);
        self.isSubmitVisible(true);
    }
}

// Tab Navigations - GoToTab-3
//function GoToTab3(self, licensingrequestData) {
//    self.viewModeForTabs('declaretion');
//    self.licensingrequestModel().ViewModeForTabs('declaretion');
//    if (self.isReset()) {
//        self.isUpdateVisible(false);
//    }
//    self.isSubmitVisible(true);
//    self.isSaveVisible(false);
//    self.isUpdateVisible(false);
//    self.isGoBackVisible(true);
//    var index = 2;
//    HandleProgressBarAndActiveTab(index);
//    if (self.isviewmodedisable() == true) {
//        self.isSaveVisible(false);
//        self.isSubmitVisible(false);
//        self.isReset(false);
//        self.isGoBackVisible(true);
//        self.isUpdateVisible(false);
//    }
//}

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

/// CheckSameAsPrincepleAddress
function CheckSameAsPrincepleAddress() {
    var isCheked = 0;
    if (($.trim($('#NumStreet').val()) == $.trim($('#NumStreet2').val()))
        && ($.trim($('#Suburb').val()) == $.trim($('#Suburb2').val()))
        && ($.trim($('#TownCity').val()) == $.trim($('#TownCity2').val()))
        && ($.trim($('#ResidentialAddressCountry').val()) == $.trim($('#PostalAddressCountry').val()))
        && ($.trim($('#PostalCode').val()) == $.trim($('#PostalCode2').val()))) {
        isCheked = 1;
    }
    return isCheked;
}
