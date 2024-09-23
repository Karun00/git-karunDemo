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

    var AgentRegistrationViewModel = function (vcn, viewDetail) {

        var self = this;
        $('#spnTitle').html("Agent");

        self.BBBEEStatuses = ko.observableArray([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
        self.BBBEEStatus = ko.observable("");
        self.isfileToUpload = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.AgentData = ko.observable(new IPMSROOT.AgentModel());
        self.validationHelper = new IPMSROOT.validationHelper();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.CaptachText = ko.observable();
        self.CountriesList = ko.observableArray();

        self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
            self.AgentData().PortData(result);

            //Masking Of Agent Telephone No.
            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var phone = $("#phone").data("kendoMaskedTextBox");
            self.AgentData().TelephoneNo1(phone.value());

            //Masking Of Agent Fax No.
            $("#faxno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var faxno = $("#faxno").data("kendoMaskedTextBox");
            self.AgentData().FaxNo(faxno.value());

            //Masking Of Agent Cellular No.
            $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            var cellularNo = $("#CellularNo").data("kendoMaskedTextBox");
            self.AgentData().CellularNo(cellularNo.value());
        });

        self.viewModelHelper.apiGet('api/Agent/GetDocumentTypes', null, function (result) {
            self.AgentData().DocumentTypes(result);
        });

        self.AnonymousUserYn = ko.observable();

        self.viewMode = ko.observable();

        self.IsReset = ko.observable(true);

        self.Initialize = function () {
            self.GetFileSizeConfigValue();
            self.viewMode('register1');
            var index = 0;
            HandleProgressBarAndActiveTab(index);

            self.LoadNewAgentRegistration();
            if (viewDetail == true) {
            }
            var captachText = randString(6);
            self.CaptachText(captachText);
            self.LoadCountries();
        }

        //Countries list from Vessel Nationality
        self.LoadCountries = function () {
            self.viewModelHelper.apiGet('api/SubCategory/GetCountriesList', null, function (result) {

                self.CountriesList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Countries(item);
                }));
            });
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue', null,
                function (result) {
                    self.fileSizeConfigValue(result);
                    ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
                });
        }

        //To saving the agent details
        self.AgentData().errors = ko.validation.group(self.AgentData());

        self.GotoTab1 = function (agentData) {
            if (self.viewMode() == 'register1') {
                return;
            }
            GoToTab1(self, agentData);
        }

        self.GotoTab2 = function (agentData) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

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
                    if (self.viewMode() == 'register1') {

                        var result_Reg = CheckForRegCombinationExistance_tab(self, agentData);
                        var result_INTAX = CheckForTaxCombinationExistance_tab(self, agentData);
                        var result_VAT = CheckForVatCombinationExistance_tab(self, agentData);

                        if (result_VAT == 0 && result_INTAX == 0 && result_Reg == 0) {
                            self.viewMode('register2');
                            GoToTab2(self, agentData);
                        }
                        else {
                            GoToTab1(self, agentData);
                            if (result_Reg == 1) {
                                self.AgentData().RegistrationNumber('');
                                msg += "<p>Reg no. already exists.! Please enter another.</p>";
                            }
                            else if (result_INTAX == 1) {
                                self.AgentData().IncomeTaxNumber('');
                                msg += "<p>Tax no. already exists.! Please enter another.<p>";
                            }
                            else if (result_VAT == 1) {
                                self.AgentData().VATNumber('');
                                msg += "<p>Vat no. already exists.! Please enter another.<p>";
                            }

                            toastr.warning(msg, "Agent Registration");
                        }
                    }
                    else
                        if (self.viewMode() == 'register3') {

                            if (agentData.AgentData().AgentDocumentsVO().length == 0) {
                                self.viewMode('register2');
                                ValidateDocuments(agentData);
                                GoToTab2(self, agentData);

                            }
                            else {
                                if (ValidateDocuments(agentData) == false) {
                                    self.viewMode('register2');
                                    GoToTab2(self, agentData);
                                    //return false;
                                }

                            }
                            self.viewMode('register2');
                            GoToTab2(self, agentData);
                        }
                        else {
                            GoToTab2(self, agentData);
                            self.viewMode('register2');
                        }
                }
            }
            else {


                GoToTab1(self, agentData);
                toastr.warning("Please select at least one port.");
                return true;
            }
        }

        self.GotoTab3 = function (agentData) {

            $('#btnReset').css('display', 'none');
            checkboxes = document.getElementsByName('port');
            var chk = 0;

            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].checked) {
                    chk = 1;
                }
            }


            if (chk == 1) {
                if (ValidateFormValues(agentData) == true) {

                    if (self.viewMode() == 'register2') {
                        //GoToTab3(self, agentData);
                        //self.viewMode('register3');
                        if (agentData.AgentData().AgentDocumentsVO().length == 0) {
                            self.viewMode('register2');
                            ValidateDocuments(agentData);
                            GoToTab2(self, agentData);

                        }
                        else {
                            if (ValidateDocuments(agentData) == false) {
                                self.viewMode('register2');
                                ValidateDocuments(agentData);
                                GoToTab2(self, agentData);
                            } else {
                                GoToTab3(self, agentData);
                                self.viewMode('register3');
                            }
                        }
                    }
                    else
                        if (self.viewMode() == 'register1') {
                            //GoToTab3(self, agentData);
                            //self.viewMode('register3');

                            if (agentData.AgentData().AgentDocumentsVO().length == 0) {
                                self.viewMode('register2');
                                GoToTab2(self, agentData);
                                return ValidateDocuments(agentData);
                            }
                            else {
                                if (ValidateDocuments(agentData) == false) {
                                    self.viewMode('register2');
                                    GoToTab2(self, agentData);
                                    return false;
                                } else {
                                    GoToTab3(self, agentData);
                                    self.viewMode('register3');
                                }
                            }

                        }
                        else {
                            GoToTab3(self, agentData);
                            self.viewMode('register3');
                            //self.AgentData().viewMode('register3');
                        }
                }
            }
            else {
                GoToTab1(self, agentData);
                toastr.warning("Please select at least one port.");
                return;

            }
        }

        self.GoToPrevTab = function (agentData) {
            if (self.viewMode() == 'register3') {
                GoToTab2(self, agentData);
            }
            else if (self.viewMode() == 'register2') {
                GoToTab1(self, agentData);
            }
        }

        self.FillPostal = function (data) {

            if ($('#chkBuisinessAddress').is(':checked') == true) {
                if ($('#txtBusinessstreet').val() != '')
                    self.AgentData().NumStreet2($('#txtBusinessstreet').val());

                if ($('#txtBusinesssuburb').val() != '')
                    self.AgentData().Suburb2($('#txtBusinesssuburb').val());

                if ($('#txtBusinesstowncity').val() != '')
                    self.AgentData().TownCity2($('#txtBusinesstowncity').val());

                if ($('#ResidentialAddressCountry').val() != '')
                    self.AgentData().CountryCode2($('#ResidentialAddressCountry').val());

                if ($('#txtBusinesscode').val() != '')
                    self.AgentData().PostalCode2($('#txtBusinesscode').val());

                $('#fldPostalAddress').find('input').attr('readonly', true);
                //$('#fldPostalAddress').find('select').attr('disabled', 'disabled');
                $('#fldPostalAddress').find('select').attr('disabled', true);
            }
            else {
                self.AgentData().NumStreet2('');
                self.AgentData().Suburb2('');
                self.AgentData().TownCity2('');
                self.AgentData().CountryCode2('');
                self.AgentData().PostalCode2('');
                $('#fldPostalAddress').find('input').attr('readonly', false);
                //$('#fldPostalAddress').find('select').attr('disabled', 'disabled');
                $('#fldPostalAddress').find('select').attr('disabled', false);
            }
            return true;
        }

        self.SaveAgent = function (agentData) {

            if (ValidateFormValues(agentData) == false) {
                return;
            }

            if (agentData.AgentData().termsConditions() == false) {
                toastr.warning("Please accept terms and conditions.", "Agent Registration");
                return;
            }

            if ($("#txtCaptachCode").val() != "") {
                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* Invalid security code.');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    toastr.error("Invalid security code.", "Error");
                    return;
                }
            }
            else {
                $("#spanCaptachCode").text('* This field is required.');
                var captachText = randString(6);
                self.CaptachText(captachText);
                toastr.warning("Please fill all required fields.", "Warning");
                return;
            }

            /*declaring applPortWorkFlowObjArry object to fill list of AgentPort*/
            var applPortWorkFlowObjArry = new Array();

            $('#divPortList input[name="port"]:checked').each(function () {
                var applPortWorkFlowObj = new ApplicantWorkFlow(0, this.value, '', 1, 1, GetDateTime(), 1, "A");
                applPortWorkFlowObjArry.push(applPortWorkFlowObj);
            });

            //pushing applicant workflow array object
            self.AgentData().AgentPorts(applPortWorkFlowObjArry);

            /*filling applicant address object*/
            var applicantAddressObj = new ApplicantAddress(
                self.AgentData().BusinessAddressID(),
                self.AgentData().NumberStreet(),
                self.AgentData().Suburb(),
                self.AgentData().TownCity(),
                self.AgentData().CountryCode(),
                self.AgentData().PostalCode(),
                'ADPE',
                'A',
                self.AgentData().CreatedBy(),
                GetDateTime()
            );
            self.AgentData().Address(applicantAddressObj);

            if (self.AgentData().NumStreet2() != '' || self.AgentData().Suburb2() != '') {
                var applicantAddressObjForAddrType2 = new ApplicantAddress(
               self.AgentData().PostalAddressID(),
               self.AgentData().NumStreet2(),
               self.AgentData().Suburb2(),
               self.AgentData().TownCity2(),
               self.AgentData().CountryCode2(),
               self.AgentData().PostalCode2(),
               'ADTE',
               'A',
               self.AgentData().CreatedBy(),
               GetDateTime()
               );
            }
            self.AgentData().Address1(applicantAddressObjForAddrType2);//pushing to applicant address observable

            /*filling applicant autherization contact details object*/
            var applicantAuthorizedContactDetails = new ApplicantContactPersonDetails(
                 self.AgentData().AuthorizedContactPersonID(),
                 'EMP',
                 self.AgentData().FirstName(),
                 self.AgentData().SurName(),
                 self.AgentData().IdentityNo(),
                 self.AgentData().Designation(),
                 self.AgentData().CellularNo(),
                 self.AgentData().EmailID(),
                 1,
                 GetDateTime(),
                 "A"
                 );
            self.AgentData().AuthorizedContactPerson(applicantAuthorizedContactDetails);

            /*Filling document object*/
            var agentDocuments = [];
            $.each(self.AgentData().AgentDocumentsVO(), function (key, val) {
                agentDocuments.push(new AgentDocument(0, val.DocumentID(), val.CreatedBy(), new Document(val.DocumentID(), val.CategoryCode(), val.CategoryCode(),
                    val.FileName(), val.FileName(), val.CreatedBy(), '', val.FileType())));
            });
            self.AgentData().AgentDocuments(agentDocuments);
            /*end*/

            self.viewModelHelper.apiPost('api/Agent/PostAgentRegistration', ko.mapping.toJSON(agentData.AgentData()),
                function (applicantId) {
                    if (applicantId > 0) {
                        UploadDocuments(self, applicantId);
                        toastr.success("Agent details saved successfully.", "Agent Registration");
                        if (isAuthenticated == 'True') {
                            setTimeout(function () {
                                window.location = '/Agent/AgentMaster';
                            }, 5000);
                        }
                        else {
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 5000);
                        }
                    }
                    else {
                        toastr.error("An error occured. Please try again later.!", "Error");
                    }
                });
        }

        //To validate form variables or properties
        self.ValidateForm = function (agentData) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            agentData.AgentData().validationEnabled(true);

            var msg = "";
            if (ValidateFormValues(agentData) == false) {
                return;
            }

            if (self.viewMode() == 'register1') {
                var result_Reg = CheckForRegCombinationExistance_tab(self, agentData);
                var result_INTAX = CheckForTaxCombinationExistance_tab(self, agentData);
                var result_VAT = CheckForVatCombinationExistance_tab(self, agentData);

                if (result_VAT == 0 && result_INTAX == 0 && result_Reg == 0) {
                    self.viewMode('register2');
                    GoToTab2(self, agentData);
                }
                else {
                    GoToTab1(self, agentData);

                    if (result_Reg == 1) {
                        self.AgentData().RegistrationNumber('');
                        msg += "<p>Reg no. already exists.! Please enter another.</p>";
                    }
                    else if (result_INTAX == 1) {
                        self.AgentData().IncomeTaxNumber('');
                        msg += "<p>Tax no. already exists.! Please enter another.<p>";
                    }
                    else if (result_VAT == 1) {
                        self.AgentData().VATNumber('');
                        msg += "<p>Vat no. already exists.! Please enter another.<p>";
                    }

                    toastr.error(msg, "Error");
                }
            }
            else if (self.viewMode() == 'register2') {

                if (agentData.AgentData().AgentDocumentsVO().length == 0) {
                    self.viewMode('register2');
                    GoToTab2(self, agentData);
                    return ValidateDocuments(agentData);
                }
                else {
                    if (ValidateDocuments(agentData) == false) {
                        self.viewMode('register2');
                        GoToTab2(self, agentData);
                        return;
                    } else {
                        GoToTab3(self, agentData);
                    }

                }
                //GoToTab3(self, agentData);
            }
        }

        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.warning("Please select document Type.");
                return;
            }
            else {
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
                            var fileSizeInBytes = opmlFile.files[i].size;
                            var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                            if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                                var elem = {};
                                elem.FileName = opmlFile.files[i].name;
                                elem.FileSize = opmlFile.files[i].size;
                                var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx'];

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
                            toastr.error("The selected file already exist.! Please upload another file.", "Error");
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
                            Adddoc.CreatedDate(item.CreatedDate);
                            Adddoc.ModifiedBy(item.ModifiedBy);
                            Adddoc.ModifiedDate(item.ModifiedDate);
                            Adddoc.FileType(item.FileType);
                            //    //Adddoc.DocumentName(documentType);
                            //self.AgentData().AgentDocuments.push(Adddoc);
                            self.AgentData().AgentDocumentsVO.push(Adddoc);
                            //self.AgentData().UploadedFiles.push(Adddoc);
                            $("select#selUploadDocs").prop('selectedIndex', 0);

                        }));


                    });
                } else {
                    $("#spanHWPSfileToUpload").text('Please select file');
                    self.isfileToUpload(true);
                }

                //$('#selUploadDocs').val('');
                self.AgentData().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        //To deleting the documents in the grid
        self.DeleteDocument = function (documentRow) {
            self.AgentData().AgentDocumentsVO.remove(documentRow);
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

            if ($("#DDBBBEEStatus option:selected").text() != '0') {
                flag = true;
            }

            if ($("#PostalAddressCountry option:selected").text() != '0') {
                flag = true;
            }

            $('#chkBuisinessAddress').attr('checked', false);
            $('#fldPostalAddress').find('input').attr('readonly', false)
            $('#fldPostalAddress').find('select').prop('disabled', false);
            if (flag === true) {
                self.AgentData().reset();
                $('#divPortList input[name="port"]:checked').attr('checked', false);
                $('#chkAllport').attr('checked', false);

                //Masking Of Agent Telephone No.
                $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                //Masking Of Agent Fax No.
                $("#faxno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

                //Masking Of Agent Cellular No.
                $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

            }
            else {
                $('#divAgentFields').find(".validationError").each(function () {
                    if ($(this).text().length != 0) {
                        $(this).css("display", "none");
                    }
                });
            }
        }

        //To cancel functionality of the screen
        self.cancel = function () {
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
                $.confirm({
                    'title': 'New Agent Request',
                    'message': 'Any unsaved changes/data will be lost.Are you sure you want to cancel?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {

                                if (isAuthenticated == 'True') {
                                    self.viewMode("List");
                                    $('#spnTitle').html("Agent");

                                    window.location = '/Agent/AgentMaster';
                                }
                                else
                                    window.location = '/Account/Login';

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

                if (isAuthenticated == 'True') {
                    self.viewMode("List");
                    $('#spnTitle').html("Agent");

                    window.location = '/Agent/AgentMaster';
                }
                else
                    window.location = '/Account/Login';
            }

        }

        //To get view screen from the pending task of the workflow
        self.LoadNewAgentRegistration = function () {
            if (viewDetail == true) {
                isView = 1;

                //GetAgentDetails(vcn, self, isView);

                $('#btnGoBack').css('display', 'none');
                $('#btnSubmit').css('display', 'none');
                $('#btnSaveContinue').css('display', '');
                //var index = 0;
                //HandleProgressBarAndActiveTab(index);
            }
        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (errors == 0) {
                action.SubmitAction(dat, self.AgentData());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }

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
                $("#spanCaptachCode").text('* This field is required');
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }

        ValidRegNo = function (data, event) {

            var result_Reg = CheckForRegCombinationExistance_tab(self, "");
            if (result_Reg > 0) {
                toastr.warning("Entered Reg no. already exists !.", "Agent Registration");
                self.AgentData().RegistrationNumber('');
            }
        }

        ValidITNO = function () {
            var result_INTAX = CheckForTaxCombinationExistance_tab(self, "");
            if (result_INTAX > 0) {
                toastr.warning("Entered Tax no. already exists !.", "Agent Registration");
                self.AgentData().IncomeTaxNumber('');
            }
        }

        ValidVATNo = function () {
            var result_VAT = CheckForVatCombinationExistance_tab(self, "");
            if (result_VAT > 0) {
                toastr.warning("Entered Vat no. already exists !.", "Agent Registration");
                self.AgentData().VATNumber('');
            }
        }

        self.Initialize();
    }

    IPMSRoot.AgentRegistrationViewModel = AgentRegistrationViewModel;
}(window.IPMSROOT));

$('input').bind("paste", function (e) {
    e.preventDefault();
});

//Reusable ApplicantAddress Object
function ApplicantAddress(AddressID, NumberStreet, Suburb, TownCity, CountryCode, PostalCode, AddressType, RecordStatus, CreatedBy, CreatedDate) {
    this.AddressID = AddressID;
    this.NumberStreet = NumberStreet;
    this.Suburb = Suburb;
    this.TownCity = TownCity;
    this.CountryCode = CountryCode;
    this.PostalCode = PostalCode;
    this.AddressType = AddressType;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
}

//Reusable ApplicantContactPersonDetails Object
function ApplicantContactPersonDetails(AuthorizedContactPersonID, AuthorizedContactPersonType, FirstName, SurName, IdentityNo,
    Designation, CellularNo, EmailID, CreatedBy, CreatedDate, RecordStatus) {
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
    this.CreatedBy = CreatedBy;
    this.FileType = FileType;
}

function AgentDocument(AgentID, DocumentID, CreatedBy, Document) {
    this.AgentID = AgentID;
    this.DocumentID = DocumentID;
    this.CreatedBy = CreatedBy;
    this.Document = Document;
}

//To reset the all fields of the agent registration screen
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
    agentData.CountryCode('');
    agentData.PostalCode('');
    agentData.TelephoneNo1('');
    agentData.FaxNo('');

    agentData.NumStreet2('');
    agentData.Suburb2('');
    agentData.TownCity2('');
    agentData.CountryCode2('');
    agentData.PostalCode2('');

    agentData.FirstName('');
    agentData.SurName('');
    agentData.IdentityNo('');
    agentData.Designation('');
    agentData.CellularNo('');
    agentData.EmailID('');

    agentData.FromDate('');
    agentData.ToDate('');

    agentData.AgentPort([]);
    agentData.Address();
    agentData.Address1();
    agentData.AuthorizedContactPerson([]);
    $('#divPortList input[name="port"]:checked').attr('checked', false);

    $('#chkSelectAll').attr('checked', false);
}

// To handling the Tab functionality of the agent registration screen
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

/// Check Phone Number Validation
function CheckPhoneValidation(PhoneNumber, Type) {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";
    PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

    if (Type == 'Telephone') {
        var validPhoneNumber = 0;
        if (PhoneNumber.length != 13) {
            toastr.warning("Invalid Telephone number.");
            validPhoneNumber++;
           
        }
        else if (PhoneNumber.length == 13) {
            var validNo = parseInt(PhoneNumber);
                        if (validNo == 0) {
                            toastr.warning("Invalid Telephone Number");
                            validPhoneNumber++;
                           
                        }

        }
        return validPhoneNumber;
    }

    if (Type == 'Fax') {
        var validFaxNumber = 0;
        if (PhoneNumber.length != 13) {
            toastr.warning("Invalid Fax number.");
            validFaxNumber++;
          
        }
        else if (PhoneNumber.length == 13) {
            var validNo = parseInt(PhoneNumber);
            if (validNo == 0) {
                toastr.warning("Invalid Fax Number");
                validFaxNumber++;

            }

        }
        return validFaxNumber;
    }

    if (Type == 'Cellular') {
        var validCellularNumber = 0;
        if (PhoneNumber.length != 13) {
            toastr.warning("Invalid Cellular number.");
            validCellularNumber++;
            //return validCellularNumber;
        }
        else if (PhoneNumber.length == 13) {
            var validNo = parseInt(PhoneNumber);
            if (validNo == 0) {
                toastr.warning("Invalid Cellular Number");
                validCellularNumber++;

            }

        }
        return validCellularNumber;
    }

}

//To validate the form values of the agent registration
function ValidateFormValues(agentData) {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";

    var result = true;
    var count = $('#divPortList input[name="port"]:checked').length;
    if (count == 0) {
        toastr.warning("Please select at least one port.");
        result = false;
    }

    var errors = 0;
    var filterTelephoneNumber = agentData.AgentData().TelephoneNo1();
    if (filterTelephoneNumber != null || filterTelephoneNumber != '' || filterTelephoneNumber != undefined) {
        var validTelephoneNumber = 0;

        if (filterTelephoneNumber.length != 0) {
            validTelephoneNumber = CheckPhoneValidation(filterTelephoneNumber, "Telephone");
            if (validTelephoneNumber > 0) {
                errors = errors + 1;
            }
        }
    }

    var filterFaxNumber = agentData.AgentData().FaxNo();
    if (filterFaxNumber != null || filterFaxNumber != '' || filterFaxNumber != undefined) {
        var validFaxNumber = 0;

        if (filterFaxNumber.length != 0) {
            validFaxNumber = CheckPhoneValidation(filterFaxNumber, "Fax");
            if (validFaxNumber > 0) {
                errors = errors + 1;
            }
        }
    }

    var filterCellularNumber = agentData.AgentData().CellularNo();
    if (filterCellularNumber != null || filterCellularNumber != '' || filterCellularNumber != undefined) {
        var validCellularNumber = 0;

        if (filterCellularNumber.length != 0) {
            validCellularNumber = CheckPhoneValidation(filterCellularNumber, "Cellular");
            if (validCellularNumber > 0) {
                errors = errors + 1;
            }
        }
    }

    if (agentData.AgentData().errors().length > 0 || errors > 0) {
        $('#divAgentFields').find(".validationError").each(function () {
            if ($(this).text().length != 0) {
                $(this).css("display", "");
            }
        });
        agentData.AgentData().errors.showAllMessages();
        toastr.warning("Please fill all the required fields.", "Warning");
        result = false;
    }

    //if (agentData.viewMode() == 'register2') {
    //    if (agentData.AgentData().AgentDocumentsVO().length == 0) {
    //        return ValidateDocuments(agentData);
    //    }
    //    else {
    //        if (ValidateDocuments(agentData) == false) {
    //            return false;
    //        }
    //    }
    //}

    var startdatevalue = $('#FromDate').val();
    var enddatevalue = $('#ToDate').val();

    if (new Date(enddatevalue) < new Date(startdatevalue)) {
        toastr.warning("To date should be greater than from date.", "Warning");
        return false;
    }
    return result;
}

//To validate documents of the agent registration
function ValidateDocuments(agentData) {
    var result = true;
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";

    var cntforSARSTaxClearance = 0;
    var cntforSAASOA = 0;
    var cntforQualifyBBBEECodes = 0;
    var cntforBBBEEStatus = 0;
    var cntforVerifyBBBEEStatus = 0;
    var documents = 0;
    var msg = "<p>Please provide below list of document's :</p>";
    var seletedcatCode = $('#selUploadDocs option:selected').val();

    if (agentData.AgentData().SARSTaxClearance() == 'Y') {
        $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC3') {
                cntforSARSTaxClearance = 1;
            }
        });

        if (cntforSARSTaxClearance == 0) {
            msg = msg + "<p>Valid SARS tax clearance certificate as annexure 1C</p>";
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }

    }
    if (agentData.AgentData().SAASOA() == 'Y') {
        $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC4') {
                cntforSAASOA = 1;
            }
        });

        if (cntforSAASOA == 0) {
            msg = msg + "<p>Proof of current membership copy as annexure 1D</p>"
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }
    }
    if (agentData.AgentData().QualifyBBBEECodes() == 'Y') {
        $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC5') {
                cntforQualifyBBBEECodes = 1;
            }
        });

        if (cntforQualifyBBBEECodes == 0) {
            msg = msg + "<p>Auditors certificate/accounting officers certificate/verifying agencies certificate confirming that applicant qualifying as exempted micro enterprise as annexure 2A</p>"
            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }
    }

    if (agentData.AgentData().VerifyBBBEEStatus() == 'Y') {
        $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC7') {
                cntforVerifyBBBEEStatus = 1;
            }
        });

        if (cntforVerifyBBBEEStatus == 0) {
            msg = msg + "<p>As annexure 2C,Certificate from the verification agency which has been accredited by SANAS</p>"

            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }
    }

    if (parseInt(agentData.AgentData().BBBEEStatus()) >= 5 && parseInt(agentData.AgentData().BBBEEStatus()) <= 9) {
        $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
            if (val.CategoryCode() == 'DOC6') {
                cntforBBBEEStatus = 1;
            }
        });

        if (cntforBBBEEStatus == 0) {
            msg = msg + "<p>An annexure 2B that the applicant will reach at least the level of 4 within 18 months from the date of registration</p>"

            $("select#selUploadDocs").prop('selectedIndex', 0);
            result = false;
        }
    }

    var documents1 = 0; var documents2 = 0

    $.each(agentData.AgentData().AgentDocumentsVO(), function (key, val) {
        if (val.CategoryCode() == 'DOC1') {
            documents1 = 1;
        }
        if (val.CategoryCode() == 'DOC2') {
            documents2 = 1;
        }
    });

    if ((agentData.AgentData().AgentDocumentsVO().length == 0) || (documents1 == 0 && documents2 == 0)) {
        msg = msg + "<p>1. Copy of Resolution authorizing the person declared in authorized person section as Annexure 1B </p> 2. Copy Of Company or Close Corporation Registration Certificate As annexure 1A</p>"

        result = false;
    } else if (parseInt(documents1) == 0) {
        msg = msg + "<p>Copy of Resolution authorizing the person declared in authorized person section as Annexure 1B </p>"
        result = false;
    } else if (parseInt(documents2) == 0) {
        msg = msg + "<p> Copy Of Company 	or Close Corporation Registration Certificate As annexure 1A</p>"
        result = false;
    }

    if (result == false) {
        toastr.warning(msg);
    }

    return result;
}

//To get date time of the screen
function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

var keynum;
var keychar;
var charcheck;
//To validate alpha numeric of the text boxes
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\?\b ]+$/;
    return charcheck.test(keychar);
}

//To validate numeric of the text boxes.
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9\b ]/;
    return charcheck.test(keychar);
}

//To get validate alphabet with spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z\b ]*$/;
    return charcheck.test(keychar);
}

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

//For Tabs functionality
function GoToTab1(self, agentData) {
    self.viewMode('register1');
    $('#btnGoBack').css('display', 'none');
    $('#btnSubmit').css('display', 'none');
    $('#btnSaveContinue').css('display', '');
    $('#btnReset').css('display', '');
    var index = 0;

    HandleProgressBarAndActiveTab(index);
}

//For Tabs functionality
function GoToTab2(self, agentData) {
    self.viewMode('register2');
    $('#btnGoBack').css('display', '');
    $('#btnSubmit').css('display', 'none');
    $('#btnSaveContinue').css('display', '');
    $('#btnReset').css('display', 'none');
    var index = 1;
    HandleProgressBarAndActiveTab(index);
}

//For Tabs functionality
function GoToTab3(self, agentData) {
    if (ValidateFormValues(agentData) == false) {
        return;
    }
    self.viewMode('register3');
    $('#btnGoBack').css('display', '');
    $('#btnSubmit').css('display', '');
    $('#btnSaveContinue').css('display', 'none');
    $('#btnReset').css('display', 'none');
    var index = 2;
    HandleProgressBarAndActiveTab(index);
}

//To upload documents of the agent registration screen
function UploadDocuments(self, applicantId) {
    var formData = new FormData();
    $.each(self.AgentData().AgentDocumentsVO(), function (key, val) {
        formData.append("Prefix", "Agent_" + applicantId);
        formData.append(val.name, val.FileDetails);
    });
    $.ajax({
        url: '/api/agent/FileUpload', // We'll send to our Web API UploadController
        data: formData, // Pass through our fancy form data
        // To prevent jQuery from trying to do clever things with our post which
        // will break our upload, we'll set the following to false
        cache: false,
        contentType: false,
        processData: false,
        type: 'POST',
        success: function (data) {
        }
    });
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
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";
    self.viewModelHelper.apiGet('api/Agent/CheckForTaxCombinationExistance', { incTaxNo: self.AgentData().IncomeTaxNumber() },
        function (result) {
            if (result == 0) {
                GoToTab2(self, agentData);
            }
            else {
                GoToTab1(self, agentData);
                self.AgentData().IncomeTaxNumber('');
                toastr.error("Entered Tax no. already exists.! Please enter another Tax no.", "Error");
            }
        });
}

// To check the VAT No. for uniqueness
function CheckForVatCombinationExistance(self, agentData) {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";
    self.viewModelHelper.apiGet('api/Agent/CheckForVatCombinationExistance', { vatNo: self.AgentData().VATNumber() },
        function (result) {
            if (result == 0) {
                GoToTab2(self, agentData);
            }
            else {
                GoToTab1(self, agentData);
                self.AgentData().VATNumber('');
                toastr.error("Entered Vat no. already exists.! Please enter another Vat no.", "Error");
            }
        });
}

// To check the Reg. No. for uniqueness
function CheckForRegCombinationExistance(self, agentData) {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";
    self.viewModelHelper.apiGet('api/Agent/CheckForRegCombinationExistance', { regNo: self.AgentData().RegistrationNumber() },
        function (result) {
            if (result == 0) {
                GoToTab2(self, agentData);
            }
            else {
                GoToTab1(self, agentData);
                self.AgentData().RegistrationNumber('');
                toastr.error("Entered Reg no. already exists.! Please enter another Reg no.", "Error");
            }
        });
}

//To getting the agent registration details form the model
function GetAgentDetails(vcn, model, isView) {

    model.viewModelHelper.apiGet('api/Agent/GetzAgent', { vcn: vcn },
             function (result) {
                 FillAgentDetailsForUpdation(result, model, isView);
             });
}

//To get agent registration details for Updation or view screen
function FillAgentDetailsForUpdation(agentData, vm, isView) {
    //ResetAllFields(vm.AgentData());
    isView = 1;
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

        vm.AgentData().NumberStreet(agentData.Address.NumberStreet);
        vm.AgentData().Suburb(agentData.Address.Suburb);
        vm.AgentData().TownCity(agentData.Address.TownCity);
        vm.AgentData().CountryCode(agentData.Address.CountryCode);
        vm.AgentData().PostalCode(agentData.Address.PostalCode);
        vm.AgentData().TelephoneNo1(agentData.TelephoneNo1);
        vm.AgentData().FaxNo(agentData.FaxNo);

        vm.AgentData().ReferenceNo(agentData.ReferenceNo);

        if (agentData.Address1 != null) {
            vm.AgentData().PostalAddressID(agentData.PostalAddressID);
            vm.AgentData().NumStreet2(agentData.Address1.NumberStreet);
            vm.AgentData().Suburb2(agentData.Address1.Suburb);
            vm.AgentData().TownCity2(agentData.Address1.TownCity);
            vm.AgentData().CountryCode2(agentData.Address1.CountryCode);
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
            $('#chkPort_' + value.PortCode).attr('checked', true);
        });
        var uploadedFiles = [];
        $.each(agentData.AgentDocuments, function (key, value) {
            var elem = {};
            elem.FileName = value.Document.DocumentPath;
            $.each(vm.AgentData().DocumentTypes(), function (key, val) {
                if (val.SubCatCode == value.Document.DocumentType) {
                    elem.CategoryName = val.SubCatName;
                }
            });
            elem.CategoryCode = value.Document.DocumentType;
            elem.FileDetails = [];
            elem.IsAlreadyExists = true;
            uploadedFiles.push(elem);
            vm.AgentData().UploadedFiles(uploadedFiles);
        });
        console.log(vm.AgentData().UploadedFiles());
        if (isView == 1) {
            $('#divAgentFields input[type=text],#divAgentFields input[type=radio],#divPortsList input[type=checkbox],#divAgentFields input[type=email],#chkBuisinessAddress,#DDBBBEEStatus').prop('disabled', true);
            vm.AgentData().isSaveVisible(false);
            vm.AgentData().isSubmitVisible(false);
        }
        else {
            $('#divAgentFields input[type=text],#divAgentFields input[type=radio],#divPortsList input[type=checkbox],#chkBuisinessAddress,#DDBBBEEStatus').prop('disabled', false);
            vm.AgentData().isSaveVisible(true);
            vm.AgentData().isGoBackVisible(false);
            vm.AgentData().isSubmitVisible(false);
        }

    }
}
