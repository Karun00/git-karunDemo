ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});
toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot) {
    var isView = 0;

    var PilotExemptionRequestListViewUpdateViewModel = function () {
        var self = this;
        $('#spnTitle').html("Pilot Exemption Request");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.pilotexemptionrequestList = ko.observableArray([]);
        self.pilotexemptionrequestlistviewupdateModel = ko.observable();
        self.viewMode = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsCodeEnable = ko.observable(false);
        self.portReferenceData = ko.observable();
        self.IsEnable = ko.observable(false);
        self.isDateofBirthMsg = ko.observable(false);
        self.isIssuedDateMsg = ko.observable(false);
        self.isRenewalDateMsg = ko.observable(false);
        self.isMovementDateMsg = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false);
        self.IsEditDisable = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();

        var errormessage = '* This field is required.';

        //Grid binding
        //LoadPilotexemptionRequests method is get the all  data from API Service at one time into the pilotexemption request screen 
        self.LoadPilotexemptionRequests = function () {
            self.viewModelHelper.apiGet('api/PilotExemptionRequestlist', null,
                function (result) {
                    self.pilotexemptionrequestList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.PilotExemptionRequestListViewUpdateModel(item);
                        self.pilotexemptionrequestlistviewupdateModel(new IPMSROOT.PilotExemptionRequestListViewUpdateModel(undefined));
                    }));
                });
        }

        //LoadGetPilotExemptionRequestReferencesData method is get the all dropdown data from API Service at one time into the pilotexemption request screen 
        self.LoadGetPilotExemptionRequestReferencesData = function () {
            self.viewModelHelper.apiGet('api/PilotExemptionRequestReferencesData', null,
                function (result) {

                    //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                    self.portReferenceData(new IPMSRoot.PortReferenceData(result));
                }, null, null, false);
        }


        self.GetFileSizeConfigValue = function () {

            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {
                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
            });
        }
        //Initialize method is fire at first time the pilotexemption request screen loads
        self.Initialize = function () {
            self.LoadPilotexemptionRequests();
            self.LoadGetPilotExemptionRequestReferencesData();
            self.viewMode('List');
            $('#spnTitle').html("Pilot Exemption Request");
            self.GetFileSizeConfigValue();
        }

        var uploadedFiles = [];
        var documentData = [];

        //uploadfile method is upload document saved in API Service and get thedocument id and DocumentName into the pilotexemption request screen 
        self.multipleuploadFile = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please Select Document Type.");
                return;
            } {
                $("#spanHWPSfileToUpload").text("");
                self.ismultiplepfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();

                if (documentType != 'Select...') {
                    self.pilotexemptionrequestlistviewupdateModel().UploadedFiles([]);
                    //var uploadedFiles = [];
                    uploadedFiles = self.pilotexemptionrequestlistviewupdateModel().UploadedFiles();
                    var opmlFile = $('#fileToUpload')[0];
                    if (opmlFile.files.length > 0) {
                        for (var i = 0; i < opmlFile.files.length; i++) {
                            var match = ko.utils.arrayFirst(self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequestDocument(), function (item) {
                                return item.FileName() === opmlFile.files[i].name;
                            });
                            if (match == null) {
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
                                        self.pilotexemptionrequestlistviewupdateModel().UploadedFiles(uploadedFiles);
                                    }
                                    else {
                                        toastr.warning("The " + opmlFile.files[i].name + " File Size is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "warning");
                                        return;
                                    }
                                }
                                else {
                                    toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("The Selected File Already Exists.! Please Upload Another File.", "Warning");
                                return;
                            }
                        }
                        var formData = new FormData();
                        $.each(uploadedFiles, function (key, val) {
                            formData.append(val.name, val.FileDetails);
                        });
                        var CategoryName = $('#selUploadDocs option:selected').text();
                        var CategoryCode = $('#selUploadDocs option:selected').val();

                        //var CategoryCode = $('#selUploadDocs option:selected').val();
                        //if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData,
                            function Message(data) {
                                self.Listdocuments = ko.observableArray();
                                self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                                    var Adddoc = new IPMSROOT.PilotExemptionRequestDocuments();
                                    Adddoc.DocumentID(item.DocumentID);
                                    Adddoc.FileName(item.FileName);
                                    Adddoc.DocumentName(documentType);
                                    self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequestDocument.push(Adddoc);
                                    $("select#selUploadDocs").prop('selectedIndex', 0);
                                }));
                            });
                        //}
                    }

                    else {
                        $("#spanmultiplepfileToUpload").text('* This Field is Required.');
                        self.ismultiplepfileToUpload(true);
                    }
                }
                else {
                    $("#spanmultiplepfileToUpload1").text('* This Field is Required.');
                    self.ismultiplepfileToUpload(true);
                }
                self.pilotexemptionrequestlistviewupdateModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        //DeleteDocument method is delete upload document in API Service based on thedocument id and DocumentName into the pilotexemption request screen 
        self.multipleDeleteDocument = function (Adddoc) {
            self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequestDocument.remove(Adddoc);
        }

        isAdd = 0;
        index = 1;

        //AddNewRowtotable method is Add new row to pilotexemptionrequest array and saved in API Service based on PlotexemptionRequestID and PilotID into the pilotexemption request screen 
        self.AddNewRowtotable = function (pilotexemptionrequest) {
            self.IsEnable(true);
            //self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());        
            {
                if (pilotexemptionrequest.PilotExemptionRequest().length > 0) {
                    var ManError = "Y";
                    $.map(pilotexemptionrequest.PilotExemptionRequest, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.MovementTypeCode() == "" || CommodChk.PilotRoleCode() == "" || CommodChk.MovementDate() == "" || CommodChk.VesselID() == "" || CommodChk.Remarks().trim() == "") {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please Enter Pilot Exemption Request Details.", "Pilot Exemption Request");
                                        ManError = "N";
                                    }
                                }
                            });

                    });
                    if (ManError == "Y")
                        self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());
                }
                else {
                    self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());
                }
            }
        }

        self.VesselSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            var i = self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest().length;
            for (j = 1; j <= i; j++) {
                self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest()[e.item.index()].VesselID(selecteddataItem.VesselID);
            }
        }

        //selectedChoices method is set the all details in residential Address put postal address fields the pilotexemption request screen 
        self.selectedChoices = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().NumberStreet((self.pilotexemptionrequestlistviewupdateModel().ResidentialAddress().NumberStreet()));
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().Suburb((self.pilotexemptionrequestlistviewupdateModel().ResidentialAddress().Suburb()));
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().TownCity((self.pilotexemptionrequestlistviewupdateModel().ResidentialAddress().TownCity()));
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().PostalCode((self.pilotexemptionrequestlistviewupdateModel().ResidentialAddress().PostalCode()));
            }
            else {
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().NumberStreet('');
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().Suburb('');
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().TownCity('');
                self.pilotexemptionrequestlistviewupdateModel().PostalAddress().PostalCode('');
            }
            return true;
        };

        // Display Maximum date in DateTime picker as today date
        calmaxtoday = function () {
            this.max(new Date());
        };

        // Display Maximum date in DateTime picker as today date
        calmaxtoday18 = function () {

            //var tenYears = new Date();
            //this.max(new tenYears().setTime(tenYears.valueOf() - 18 * 365 * 24 * 60 * 60 * 1000));
            var today = new Date();
            var dd = Number(today.getDate());
            var mm = Number(today.getMonth() + 1);
            var yyyy = Number(today.getFullYear());

            var year = new Date().getYear() - 18;

            this.max(new Date(yyyy - 18, mm - 1, dd - 1));
        };

        CalcDateofBirth = function () {
            var dobDate = this.value()
            dobDate.setFullYear(dobDate.getFullYear() + 18);
            var IssuedDatepicker = $("#IssuedDate").data("kendoDatePicker");
            IssuedDatepicker.min(dobDate);
            IssuedDatepicker.value('');
            $('#isDateofBirthMsg').text('');
        };

        CalcIssuedDate = function () {
            var IssuedDate = this.value();
            var RenewalDate1Datepicker = $("#RenewalDate").data("kendoDatePicker");
            RenewalDate1Datepicker.min(IssuedDate);
            RenewalDate1Datepicker.value('');
            $('#isIssuedDateMsg').text('');
        };

        CalcRenewalDate = function () {
            $('#isRenewalDateMsg').text('');
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //AddessKeyupEvents method is set the all details in residential Address put postal address fields  on key up event the pilotexemption request screen 
        self.AddessKeyupEvents = function () {
            if ($('#chkBuisinessAddress').is(':checked')) {
                $('#NumStreet').keyup(function () { $('#NumStreet2').val($(this).val()); });
                $('#Suburb').keyup(function () { $('#Suburb2').val($(this).val()); });
                $('#TownCity').keyup(function () { $('#TownCity2').val($(this).val()); });
                $('#PostalCode').keyup(function () { $('#PostalCode2').val($(this).val()); });
            }
            else {
                var error = 4;
                if ($("#ResidentialAddressstreet").val().trim() == $("#PostalAddressNumberStreet").val().trim())
                { error = error - 1 }
                if ($("#ResidentialAddresssuburb").val().trim() == $("#PostalAddresssuburb").val().trim())
                { error = error - 1 }
                if ($("#ResidentialAddresstowncity").val().trim() == $("#PostalAddressTownCity").val().trim())
                { error = error - 1 }
                if ($("#ResidentialAddressPostalCode").val().trim() == $("#PostalAddressPostalCode").val().trim())
                { error = error - 1 }
                if (error <= 0)
                { self.pilotexemptionrequestlistviewupdateModel().AddressCheckbox = true; }
                else { self.pilotexemptionrequestlistviewupdateModel().AddressCheckbox = false; }
                $('#NumStreet').keyup();
                $('#Suburb').keyup();
                $('#TownCity').keyup();
                $('#PostalCode').keyup();
                return false;
            }
        };

        self.Initialize();

        //ResetPilotExemptionRequest method is reset the pilotexemption request  screen data
        self.ResetPilotExemptionRequest = function (data) {
            self.pilotexemptionrequestlistviewupdateModel().reset();
            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        }

        //editPilotExemptionRequest method is used to Edit-mode the changes in view
        self.editPilotExemptionRequest = function (data) {
            self.pilotexemptionrequestlistviewupdateModel(data);
            self.viewMode("Form");
            self.IsReset(true);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsEditDisable(false);

            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#MovementDate").data('kendoDatePicker').enable(false);

            $('input[id="MovementDate"]').each(function () {
                $(this).data('kendoDatePicker').enable(false);
            });

            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);
        }

        //CancelPilotExemptionRequest method is Cancel the pilotexemptionrequestinedit  screen data and navigate to list screen
        self.viewPilotExemptionRequest = function (data) {
            self.pilotexemptionrequestlistviewupdateModel(data);
            self.viewMode("Form");
            self.IsReset(false);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            self.IsEditDisable(false);

            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            $("#DateofBirth").data('kendoDatePicker').enable(false);
            $("#IssuedDate12").data('kendoDatePicker').enable(false);
            $("#RenewalDate12").data('kendoDatePicker').enable(false);
            $("#MovementDate").data('kendoDatePicker').enable(false);

            $('input[id="MovementDate"]').each(function () {
                $(this).data('kendoDatePicker').enable(false);
            });

            var isSameasPrincipleAddress = CheckSameAsPrincepleAddress();
            if (isSameasPrincipleAddress == 1)
                $('#chkBuisinessAddress').attr('checked', true);
            else
                $('#chkBuisinessAddress').attr('checked', false);
        };

        //UpdatePilotExemptionRequest method is used to update the changes in view
        self.UpdatePilotExemptionRequest = function (pilotexemptionrequest) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var validationMessageText = '* This field is required.';

            var filterPhoneNumber = pilotexemptionrequest.ContactNo();
            filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');

            var filtermobileNumber = pilotexemptionrequest.CellNo();
            filtermobileNumber = filtermobileNumber.replace(/(\)|\()|_|-+/g, '');

            self.PilotExemptionRequestValidation = ko.observable(pilotexemptionrequest);
            self.PilotExemptionRequestValidation().errors = ko.validation.group(self.PilotExemptionRequestValidation());
            var errors = self.PilotExemptionRequestValidation().errors().length;

            self.PostalValidation = ko.observable(pilotexemptionrequest.PostalAddress);
            self.PostalValidation().errors = ko.validation.group(self.PostalValidation());
            var Postalerrors = self.PostalValidation().errors().length;

            self.ResidentialAddressValidation = ko.observable(pilotexemptionrequest.ResidentialAddress);
            self.ResidentialAddressValidation().errors = ko.validation.group(self.ResidentialAddressValidation());
            var Businesserrors = self.ResidentialAddressValidation().errors().length;

            var errors1 = 0;

            var validPhoneNumber = 0;

            if (filterPhoneNumber.length != 0) {
                if (filterPhoneNumber.length != 13) {
                    toastr.warning("Invalid TelePhone Number");
                    $("#isphoneMsg").text("Invalid TelePhone Number ");
                    validPhoneNumber++;
                    errors1 = errors1 + 1;
                }
            }

            var validmobileNumber = 0;

            if (filtermobileNumber.length != 0) {
                if (filtermobileNumber.length != 13) {
                    toastr.warning("Invalid Mobile Number");
                    $("#isCellNoMsg").text("Invalid Mobile Number");
                    validmobileNumber++;
                    errors1 = errors1 + 1;
                }
            }

            if (Postalerrors == 0 && Businesserrors == 0) {
                if ((self.pilotexemptionrequestlistviewupdateModel().DateofBirth() != "") && (self.pilotexemptionrequestlistviewupdateModel().DateofBirth() != null)) {
                    self.isDateofBirthMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isDateofBirthMsg").text(validationMessageText);
                    self.isDateofBirthMsg(true);
                }

                if ((self.pilotexemptionrequestlistviewupdateModel().RenewalDate() != "") && (self.pilotexemptionrequestlistviewupdateModel().RenewalDate() != null)) {
                    self.isRenewalDateMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isRenewalDateMsg").text(validationMessageText);
                    self.isRenewalDateMsg(true);
                }

                if ((self.pilotexemptionrequestlistviewupdateModel().IssuedDate() != "") && (self.pilotexemptionrequestlistviewupdateModel().IssuedDate() != null)) {
                    self.isIssuedDateMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isIssuedDateMsg").text(validationMessageText);
                    self.isIssuedDateMsg(true);
                }

                if (pilotexemptionrequest.Certificate_of_Competency() == null || pilotexemptionrequest.Certificate_of_Competency() == '') {
                    $("#isCertificate_of_CompetencyMsg").text(validationMessageText);
                    errors1 = errors1 + 1;
                }
                else {
                    $("#isCertificate_of_CompetencyMsg").text('');
                }

                if (pilotexemptionrequest.NationalityCode() == null || pilotexemptionrequest.NationalityCode() == '') {
                    $("#isNationality").text(validationMessageText);
                    errors1 = errors1 + 1;
                }
                else {
                    $("#isNationality").text('');
                }

                if ((self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest()[0].MovementDate() != "") && (self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest()[0].MovementDate() != null)) {
                    self.isMovementDateMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isMovementDateMsg").text(validationMessageText);
                    self.isMovementDateMsg(true);
                }

                if (errors1 == 0) {
                    self.viewModelHelper.apiPut('api/PilotExemptionRequest', ko.mapping.toJSON(pilotexemptionrequest),
                        function Message(pilotexemptionrequest) {
                            toastr.success("Pilot Exemption request details updated successfully.", "Pilot Exemption Request");
                            self.LoadPilotexemptionRequests();
                            self.LoadGetPilotExemptionRequestReferencesData();
                            self.viewMode('List');
                            $('#spnTitle').html("Pilot Exemption Request");
                        });
                }
            }
            else {
                if ((self.pilotexemptionrequestlistviewupdateModel().DateofBirth() != "") && (self.pilotexemptionrequestlistviewupdateModel().DateofBirth() != null)) {
                    self.isDateofBirthMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isDateofBirthMsg").text(validationMessageText);
                    self.isDateofBirthMsg(true);
                }

                if (pilotexemptionrequest.NationalityCode() == null || pilotexemptionrequest.NationalityCode() == '') {
                    $("#isNationality").text(validationMessageText);
                    errors1 = errors1 + 1;
                }
                else {
                    $("#isNationality").text('');
                }

                if ((self.pilotexemptionrequestlistviewupdateModel().RenewalDate() != "") && (self.pilotexemptionrequestlistviewupdateModel().RenewalDate() != null)) {
                    self.isRenewalDateMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isRenewalDateMsg").text(validationMessageText);
                    self.isRenewalDateMsg(true);
                }

                if ((self.pilotexemptionrequestlistviewupdateModel().IssuedDate() != "") && (self.pilotexemptionrequestlistviewupdateModel().IssuedDate() != null)) {
                    self.isIssuedDateMsg(false);
                }
                else {
                    errors1 = errors1 + 1;
                    $("#isIssuedDateMsg").text(validationMessageText);
                    self.isIssuedDateMsg(true);
                }

                self.PilotExemptionRequestValidation().errors.showAllMessages();
                self.ResidentialAddressValidation().errors.showAllMessages();
                self.PostalValidation().errors.showAllMessages();
                toastr.error("Please fill all the required fields.", "Pilot Exemption Request");
                return;
            }
        }

        ResidentialAddressstreetChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isResidentialAddressstreetMsg").text(errormessage);
            }
            else {
                $("#isResidentialAddressstreetMsg").text('');
                if ($("#PostalAddressNumberStreet").val().trim() != $("#ResidentialAddressstreet").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        ResidentialAddresssuburbChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isResidentialAddresssuburbMsg").text(errormessage);
            }
            else {
                $("#isResidentialAddresssuburbMsg").text('');
                if ($("#PostalAddresssuburb").val().trim() != $("#ResidentialAddresssuburb").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        ResidentialAddresstowncityChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isResidentialAddresstowncityMsg").text(errormessage);
            }
            else {
                $("#isResidentialAddresstowncityMsg").text('');
                if ($("#PostalAddressTownCity").val().trim() != $("#ResidentialAddresstowncity").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        ResidentialAddressPostalCodeChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isResidentialAddressPostalCodeMsg").text(errormessage);
            }
            else {
                $("#isResidentialAddressPostalCodeMsg").text('');
                if ($("#PostalAddressPostalCode").val().trim() != $("#ResidentialAddressPostalCode").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        ResidentialAddressCountryChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isResidentialAddressCountryMsg").text(errormessage);
            }
            else {
                $("#isResidentialAddressCountryMsg").text('');
                if ($("#PostalAddressCountry").val().trim() != $("#ResidentialAddressCountry").val().trim()) {
                    $('#chkBuisinessAddress').attr('checked', false);
                }
            }
        }

        PostalAddressNumberStreetChange = function (item) {

            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isPostalAddressNumberStreetMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressNumberStreetMsg").text('');
                if ($("#PostalAddressNumberStreet").val().trim() != $("#ResidentialAddressstreet").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        PostalAddresssuburbChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isPostalAddresssuburbMsg").text(errormessage);
            }
            else {
                $("#isPostalAddresssuburbMsg").text('');
                if ($("#PostalAddresssuburb").val().trim() != $("#ResidentialAddresssuburb").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        PostalAddressTownCityChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isPostalAddressTownCityMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressTownCityMsg").text('');
                if ($("#PostalAddressTownCity").val().trim() != $("#ResidentialAddresstowncity").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        PostalAddressPostalCodeChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isPostalAddressPostalCodeMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressPostalCodeMsg").text('');
                if ($("#PostalAddressPostalCode").val().trim() != $("#ResidentialAddressPostalCode").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        PostalAddressCountryChange = function (item) {
            if (item.value == null || item.value == '' || item.value == undefined) {
                $("#isPostalAddressCountryMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressCountryMsg").text('');
                if ($("#PostalAddressCountry").val().trim() != $("#ResidentialAddressCountry").val().trim())
                { $('#chkBuisinessAddress').attr('checked', false); }
            }
        }

        self.clearallerrormsgs = function () {
            self.isDateofBirthMsg(false);
            self.isRenewalDateMsg(false);
            self.isIssuedDateMsg(false);
            self.isMovementDateMsg(false);
        }

        self.RemoveAddNewRowtotable = function (row) {
            self.pilotexemptionrequestlistviewupdateModel().PilotExemptionRequest.remove(row);
        };

        //CancelPilotExemptionRequest method is Cancel the pilotexemptionrequestinedit  screen data and navigate to list screen
        self.CancelPilotExemptionRequest = function (pilotexemptionrequest) {
            self.pilotexemptionrequestlistviewupdateModel().reset();
            self.viewMode('List');
            $('#spnTitle').html("Pilot Exemption Request");
        }
    }

    IPMSRoot.PilotExemptionRequestListViewUpdateViewModel = PilotExemptionRequestListViewUpdateViewModel;
}(window.IPMSROOT));

/// CheckSameAsPrincepleAddress
function CheckSameAsPrincepleAddress() {
    var isCheked = 0;
    if (($.trim($('#ResidentialAddressstreet').val()) == $.trim($('#PostalAddressNumberStreet').val()))
        && ($.trim($('#ResidentialAddresssuburb').val()) == $.trim($('#PostalAddresssuburb').val()))
        && ($.trim($('#ResidentialAddresstowncity').val()) == $.trim($('#PostalAddressTownCity').val()))
        && ($.trim($('#ResidentialAddressCountry').val()) == $.trim($('#ResidentialAddressCountry').val()))
        && ($.trim($('#ResidentialAddressPostalCode').val()) == $.trim($('#PostalAddressPostalCode').val()))) {
        isCheked = 1;
    }
    return isCheked;
}