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

    var PilotExemptionRequestViewModel = function (PilotID, viewDetail) {
        var self = this;
        $('#spnTitle').html("Pilot Exemption Request");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.pilotexemptionrequestModel = ko.observable(new IPMSROOT.PilotExemptionRequestModel());
        self.pilotexemptionrequestList = ko.observableArray();
        self.portReferenceData = ko.observable();
        self.IsEnable = ko.observable(false);
        self.isDateofBirthMsg = ko.observable(false);
        self.isIssuedDateMsg = ko.observable(false);
        self.isRenewalDateMsg = ko.observable(false);
        self.isMovementDateMsg = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false);
        self.CaptachText = ko.observable();
        self.fileSizeConfigValue = ko.observable();

        var errormessage = '* This field is required.';

        self.GetFileSizeConfigValue = function () {

            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {
                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);
            });
        }

        //Initialize method is fire at first time the Pilot Exemption Request screen loads
        self.Initialize = function () {
            if (viewDetail != true) {
                self.IsEnable = ko.observable(true);
            }
            self.LoadGetPilotExemptionRequestReferencesData();
            ko.validation.group(self.pilotexemptionrequestModel()).showAllMessages(false);
            var captachText = randString(6);
            self.CaptachText(captachText);
            self.GetFileSizeConfigValue();
        }

        //LoadGetPilotExemptionRequestReferencesData method is get the all dropdown data from API Service at one time into the pilotexemption request screen 
        self.LoadGetPilotExemptionRequestReferencesData = function () {
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/PilotExemptionRequest/GetPilotExemptionRequest', { id: PilotID },
                          function (result) {
                              self.pilotexemptionrequestList(new IPMSRoot.PilotExemptionRequestModel(result));
                              self.viewPilotExemptionRequest(self.pilotexemptionrequestList());
                          });

                self.viewModelHelper.apiGet('api/PilotExemptionRequestReferencesData', null,
                    function (result) {

                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.portReferenceData(new IPMSRoot.PortReferenceData(result));
                    }, null, null, false);
            }
            else {
                self.viewModelHelper.apiGet('api/PilotExemptionRequestReferencesData', null,
                        function (result) {

                            //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                            self.portReferenceData(new IPMSRoot.PortReferenceData(result));
                        }, null, null, false);
            }
        }

        //ResetPilotExemptionRequest method is reset the pilotexemption request  screen data
        self.ResetPilotExemptionRequest = function () {
            self.pilotexemptionrequestModel(new IPMSROOT.PilotExemptionRequestModel(undefined));
            self.pilotexemptionrequestModel().reset();
            $('#spnTitle').html("Pilot Exemption Request");
            self.isDateofBirthMsg(false);
            self.isRenewalDateMsg(false);
            self.isIssuedDateMsg(false);
            self.isMovementDateMsg(false);
            self.ismultiplepfileToUpload = ko.observable(false);
            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#txtCaptachCode").val() == "";
        }

        //CancelPilotExemptionRequest method is Cancel the pilotexemption request  screen data and navigate to login screen
        self.CancelPilotExemptionRequest = function (pilotexemptionrequest) {
            if (viewDetail == true) {
                window.location = '/Welcome';
                $("#txtCaptachCode").val() == "";
            }
            else {
                window.location = '/Account/Login';
                $("#txtCaptachCode").val() == "";
            }

            self.pilotexemptionrequestModel().pendingTasks.removeAll();
        }

        //Preventing Backspace
        PreventDrop = function (ev) {
            return self.validationHelper.PreventDrop(event);
        }

        self.viewPilotExemptionRequest = function (data) {
            self.pilotexemptionrequestModel(data);
            self.IsEnable(false);
            $("#txtCaptachCode").val() == "";

            $("#DateofBirth").data('kendoDatePicker').enable(false);
            $("#IssuedDate").data('kendoDatePicker').enable(false);
            $("#RenewalDate1").data('kendoDatePicker').enable(false);
            $("#MovementDate").data('kendoDatePicker').enable(false);
            $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            $('input[id="MovementDate"]').each(function () {
                $(this).data('kendoDatePicker').enable(false);
            });
            //Workflow
            var ReferenceID = data.PilotID();
            var WorkflowInstanceID = data.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID, null,
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
                        self.pilotexemptionrequestModel().pendingTasks.push(pendingtaskaction);
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
                action.SubmitAction(dat, self.pilotexemptionrequestModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.multipleuploadFile = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } {
                $("#spanHWPSfileToUpload").text("");
                self.ismultiplepfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();

                if (documentType != 'Select...') {
                    self.pilotexemptionrequestModel().UploadedFiles([]);

                    uploadedFiles = self.pilotexemptionrequestModel().UploadedFiles();
                    var opmlFile = $('#fileToUpload')[0];
                    if (opmlFile.files.length > 0) {
                        for (var i = 0; i < opmlFile.files.length; i++) {
                            var match = ko.utils.arrayFirst(self.pilotexemptionrequestModel().PilotExemptionRequestDocument(), function (item) {
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
                                        self.pilotexemptionrequestModel().UploadedFiles(uploadedFiles);
                                    }                                
                                else {
                                        toastr.warning("The " + opmlFile.files[i].name + " File Size is Exceeded The Maximum Set Limit " + self.fileSizeConfigValue() + "MB", "warning");
                                    return;
                                }
                                } else {
                                    toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                    return;
                                }

                            }
                            else {
                                toastr.warning("The selected file already exists.! Please upload another file.", "Warning");
                                return;
                            }
                        }
                        var formData = new FormData();
                        $.each(uploadedFiles, function (key, val) {  //self.pilotexemptionrequestModel().UploadedFiles()
                            formData.append(val.name, val.FileDetails);
                        });
                        var CategoryName = $('#selUploadDocs option:selected').text();
                        var CategoryCode = $('#selUploadDocs option:selected').val();

                        //var CategoryCode = $('#selUploadDocs option:selected').val();
                        //if (opmlFile.files.length > 0) {
                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                                var Adddoc = new IPMSROOT.PilotExemptionRequestDocuments();
                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(documentType);
                                self.pilotexemptionrequestModel().PilotExemptionRequestDocument.push(Adddoc);
                                $("select#selUploadDocs").prop('selectedIndex', 0);
                            }));
                        });
                        //}
                    }

                    else {
                        $("#spanmultiplepfileToUpload").text('Please select file');
                        self.ismultiplepfileToUpload(true);
                    }
                }
                else {
                    $("#spanmultiplepfileToUpload1").text('Please select Document Category');
                    self.ismultiplepfileToUpload(true);
                }
                self.pilotexemptionrequestModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        //DeleteDocument method is delete upload document in API Service based on thedocument id and DocumentName into the Pilot Exemption Request screen 
        self.multipleDeleteDocument = function (Adddoc) {
            self.pilotexemptionrequestModel().PilotExemptionRequestDocument.remove(Adddoc);
        }

        isAdd = 0;
        index = 1;

        //AddNewRowtotable method is Add new row to pilotexemptionrequest array and saved in API Service based on PlotexemptionRequestID and PilotID into the pilotexemption request screen 
        self.AddNewRowtotable = function (pilotexemptionrequest) {
            //self.pilotexemptionrequestModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());
            {


                if (pilotexemptionrequest.PilotExemptionRequest().length > 0) {
                    var ManError = "Y";
                    $.each(pilotexemptionrequest.PilotExemptionRequest(), function (index, item) {
                    //$.map(pilotexemptionrequest.PilotExemptionRequest, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                           // ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                            if (CommoditiesListC !== undefined) {
                                if (CommoditiesListC.MovementTypeCode() == "" || CommoditiesListC.PilotRoleCode() == "" || CommoditiesListC.MovementDate() == "" || CommoditiesListC.VesselID() == "") {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please enter All Fields of Pilot Exemption Request Details", "Pilot Exemption Request");
                                        ManError = "N";
                                    }
                                }
                            //});

                    });
                    if (ManError == "Y")
                        self.pilotexemptionrequestModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());
                }
                else {
                    self.pilotexemptionrequestModel().PilotExemptionRequest.push(new IPMSROOT.AddPilotExeptiontable());
                }
            }
        }

        self.VesselSelect = function (e) {
            //$("select#reasonforvisit").prop('selectedIndex', 0);

            //if (self.pilotexemptionrequestModel().PilotExemptionRequest().length == 0) {
            //    self.pilotexemptionrequestModel().PilotExemptionRequest()[0].VesselID(selecteddataItem.VesselID);
            //}
            //else {
            var selecteddataItem = this.dataItem(e.item.index());
            var i = self.pilotexemptionrequestModel().PilotExemptionRequest().length;
            if (i == 1) {

                for (j = 1; j <= i; j++)
                { self.pilotexemptionrequestModel().PilotExemptionRequest()[i - 1].VesselID(selecteddataItem.VesselID); }
            }
            else {
                for (j = 1; j < i; j++)
                { self.pilotexemptionrequestModel().PilotExemptionRequest()[i - 1].VesselID(selecteddataItem.VesselID); }

            }
        }

        //saveptilotexemptionRequest method is saved the all details inbackend based on PlotexemptionRequestID and PilotID into the pilotexemption request screen 
        self.saveptilotexemptionRequest = function (pilotexemptionrequest) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var filterPhoneNumber = pilotexemptionrequest.ContactNo();
            filterPhoneNumber = filterPhoneNumber.replace(/(\)|\()|_|-+/g, '');

            var filtermobileNumber = pilotexemptionrequest.CellNo();
            filtermobileNumber = filtermobileNumber.replace(/(\)|\()|_|-+/g, '');

            self.PilotExemptionRequestValidation = ko.observable(pilotexemptionrequest);
            self.PilotExemptionRequestValidation().errors = ko.validation.group(self.PilotExemptionRequestValidation());
            var errors1 = self.PilotExemptionRequestValidation().errors().length;

            if ($("#txtCaptachCode").val() != "") {
                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* Invalid security code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    errors1 = errors1 + 1;
                }
            }
            else {
                $("#spanCaptachCode").text(errormessage);
                var captachText = randString(6);
                self.CaptachText(captachText);
                errors1 = errors1 + 1;
            }

            var validPhoneNumber = 0;

            if (filterPhoneNumber.length != 0) {
                if (filterPhoneNumber.length != 13) {
                    toastr.warning("Invalid Telephone Number");
                    $("#isphoneMsg").text("Invalid Telephone Number ");
                    validPhoneNumber++;
                    errors1 = errors1 + 1;
                }

            }
             if (filterPhoneNumber.length == 13) {
                var validNo = parseInt(filterPhoneNumber);
                if (validNo == 0) {
                    toastr.warning("Invalid Telephone Number");
                    $("#isphoneMsg").text("Invalid Telephone Number ");
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
             if (filtermobileNumber.length == 13) {
                var validNo = parseInt(filtermobileNumber);
                if (validNo == 0) {
                    toastr.warning("Invalid Mobile Number");
                    $("#isCellNoMsg").text("Invalid Mobile Number");
                    filtermobileNumber++;
                    errors1 = errors1 + 1;
                }

            }

            if (pilotexemptionrequest.DateofBirth() == null || pilotexemptionrequest.DateofBirth() == '') {
                $("#isDateofBirthMsg").text(errormessage);
                self.isDateofBirthMsg(true);
                errors1 = errors1 + 1;
            }
            else {
                self.isDateofBirthMsg(false);
                $("#isDateofBirthMsg").text('');
            }

            if (pilotexemptionrequest.IssuedDate() == null || pilotexemptionrequest.IssuedDate() == '') {
                self.isIssuedDateMsg(true);
                $("#isIssuedDateMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                self.isIssuedDateMsg(false);
                $("#isIssuedDateMsg").text('');
            }

            if (pilotexemptionrequest.RenewalDate() == null || pilotexemptionrequest.RenewalDate() == '') {
                self.isRenewalDateMsg(true);
                $("#isRenewalDateMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                self.isRenewalDateMsg(false);
                $("#isRenewalDateMsg").text('');
            }

            if (pilotexemptionrequest.Certificate_of_Competency() == null || pilotexemptionrequest.Certificate_of_Competency() == '') {
                $("#isCertificate_of_CompetencyMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isCertificate_of_CompetencyMsg").text('');
            }

            if (pilotexemptionrequest.NationalityCode() == null || pilotexemptionrequest.NationalityCode() == '') {
                $("#isNationality").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isNationality").text('');
            }

            if ($("#ResidentialAddressstreet").val() == null || $("#ResidentialAddressstreet").val() == '' || $("#ResidentialAddressstreet").val() == undefined) {
                $("#isResidentialAddressstreetMsg").text(errormessage);

                errors1 = errors1 + 1;
            }
            else {
                $("#isResidentialAddressstreetMsg").text('');
            }

            if ($("#ResidentialAddresssuburb").val() == null || $("#ResidentialAddresssuburb").val() == '' || $("#ResidentialAddresssuburb").val() == undefined) {
                $("#isResidentialAddresssuburbMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isResidentialAddresssuburbMsg").text('');
            }

            if ($("#ResidentialAddresstowncity").val() == null || $("#ResidentialAddresstowncity").val() == '' || $("#ResidentialAddresstowncity").val() == undefined) {
                $("#isResidentialAddresstowncityMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isResidentialAddresstowncityMsg").text('');
            }

            if ($("#ResidentialAddressCountry").val() == null || $("#ResidentialAddressCountry").val() == '' || $("#ResidentialAddressCountry").val() == undefined) {
                $("#isResidentialAddressCountryMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isResidentialAddressCountryMsg").text('');
            }

            if ($("#ResidentialAddressPostalCode").val() == null || $("#ResidentialAddressPostalCode").val() == '' || $("#ResidentialAddressPostalCode").val() == undefined) {
                $("#isResidentialAddressPostalCodeMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isResidentialAddressPostalCodeMsg").text('');
            }

            if ($("#PostalAddressNumberStreet").val() == null || $("#PostalAddressNumberStreet").val() == '' || $("#PostalAddressNumberStreet").val() == undefined) {
                $("#isPostalAddressNumberStreetMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isPostalAddressNumberStreetMsg").text('');
            }

            if ($("#PostalAddresssuburb").val() == null || $("#PostalAddresssuburb").val() == '' || $("#PostalAddresssuburb").val() == undefined) {
                $("#isPostalAddresssuburbMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isPostalAddresssuburbMsg").text('');
            }

            if ($("#PostalAddressTownCity").val() == null || $("#PostalAddressTownCity").val() == '' || $("#PostalAddressTownCity").val() == undefined) {
                $("#isPostalAddressTownCityMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isPostalAddressTownCityMsg").text('');
            }


            if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                $("#isPostalAddressCountryMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isPostalAddressCountryMsg").text('');
            }


            if ($("#PostalAddressPostalCode").val() == null || $("#PostalAddressPostalCode").val() == '' || $("#PostalAddressPostalCode").val() == undefined) {
                $("#isPostalAddressPostalCodeMsg").text(errormessage);
                errors1 = errors1 + 1;
            }
            else {
                $("#isPostalAddressPostalCodeMsg").text('');
            }

            if (pilotexemptionrequest.PilotExemptionRequestDocument().length == 0) {
                errors1 = errors1 + 1;
            }
            if (pilotexemptionrequest.PilotExemptionRequest().length < 2) {
                errors1 = errors1 + 1;
            }
            if (pilotexemptionrequest.PilotExemptionRequest().length > 0) {
                //$.map(self.pilotexemptionrequestModel().PilotExemptionRequest, function (item) {
                $.each(pilotexemptionrequest.PilotExemptionRequest(), function (index, item) {
                    var CommoditiesListC = item;
                    if (CommoditiesListC != null)
                        //ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                        if (CommoditiesListC !== undefined) {
                            if (CommoditiesListC.MovementDate() == "" || CommoditiesListC.VesselName() == "" || CommoditiesListC.PilotRoleCode() == null || CommoditiesListC.PilotRoleCode() == undefined || CommoditiesListC.MovementTypeCode() == null || CommoditiesListC.MovementTypeCode() == undefined || CommoditiesListC.Remarks() == "") {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("Please enter All Fields of  vessel movements for pilot exemption request details", "Pilot Exemption Request");
                                errors1 = errors1 + 1;
                            }
                        }

                    //});

                });
            }


            else {
                toastr.warning("Please enter All Fields of  vessel movements for pilot exemption request details", "Pilot Exemption Request");
                errors1 = errors1 + 1;
            }

            //if (telnumber.length != 0) {
            //    if (telnumber.length != 13) {
            //        errors1 = errors1 + 1;
            //        $("#isphoneMsg").text("Invalid telnumber number");
            //    }
            //    else if (telnumber.length == 13) {
            //        var validNo = parseInt(telnumber);
            //    }
            //}

            //if (mobnumber.length != 0) {
            //    if (mobnumber.length != 13) {
            //        errors1 = errors1 + 1;
            //        $("#isCellNoMsg").text("Invalid mobile number");

            //    }
            //    else if (mobnumber.length == 13) {
            //        var validNo = parseInt(mobnumber);
            //    }
            //}

            //$.map(pilotexemptionrequest.PilotExemptionRequest(), function (item) {
            //    var CommodChk = item;
            //    if (CommodChk != null)
            //        if (CommodChk !== undefined) {
            //            if (CommodChk.MovementDate() == '' || CommodChk.MovementTypeCode() == '' || CommodChk.VesselID() == '' || CommodChk.PilotRoleCode() == '') {
            //                errors1 = errors1 + 1;
            //                return;
            //            }
            //        }
            //})
            $.map(pilotexemptionrequest.PilotExemptionRequest(), function (item) {
                var CommodChk = item;
                if (CommodChk != null)
                    if (CommodChk !== undefined) {
                        if (CommodChk.MovementDate() == '' || CommodChk.MovementTypeCode() == '' || CommodChk.VesselID() == '' || CommodChk.PilotRoleCode() == '') {
                            errors1 = errors1 + 1;
                            return;
                        }
                    }
            })

            if (errors1 == 0) {
                pilotexemptionrequest.IssueDate(moment(pilotexemptionrequest.IssuedDate()).format('YYYY-MM-DD'));
                pilotexemptionrequest.RenewDate(moment(pilotexemptionrequest.RenewalDate()).format('YYYY-MM-DD'));
                self.viewModelHelper.apiPost('api/PilotExemptionRequest', ko.mapping.toJSON(pilotexemptionrequest),
                    function Message(data) {
                        toastr.success("Pilot Exemption request details saved successfully.", "Pilot Exemption Request");
                        $("#txtCaptachCode").val() == "";

                        setTimeout(function () {
                            window.location = '/Account/Login';
                        }, 4000);
                    });
            }
            else {
                self.PilotExemptionRequestValidation().errors.showAllMessages();
                toastr.warning("Please fill all the required fields.", "Pilot Exemption Request");

                if (pilotexemptionrequest.PilotExemptionRequest().length < 2) {
                    toastr.warning("Please select at least 2 vessel movements for pilot exemption request details.", "Pilot Exemption Request");
                }
                else {
                    $.map(pilotexemptionrequest.PilotExemptionRequest(), function (item) {
                        var CommodChk = item;
                        if (CommodChk != null) {
                            if (CommodChk !== undefined) {
                                if (CommodChk.MovementDate() == '' || CommodChk.MovementTypeCode() == '' || CommodChk.VesselID() == '' || CommodChk.PilotRoleCode() == '') {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("Please select vessel details.", "Pilot Exemption Request");
                                    ManError = "N";
                                    return;
                                }
                            }
                        }
                    })
                }

                if (pilotexemptionrequest.PilotExemptionRequestDocument().length == 0) {
                    toastr.warning("Please select at least one document to upload.", "Pilot Exemption Request");
                }
                return;
            }
        }

        self.clearallerrormsgs = function () {
            self.isDateofBirthMsg(false);
            self.isRenewalDateMsg(false);
            self.isIssuedDateMsg(false);
            self.isMovementDateMsg(false);
        }

        self.RemoveAddNewRowtotable = function (row) {
            document.getElementById("RemoveAddNewRowtotable").focus();
            self.pilotexemptionrequestModel().PilotExemptionRequest.remove(row);
        }

        //selectedChoices method is set the all details in residential Address put postal address fields the pilotexemption request screen 
        self.selectedChoices = function () {
            if ($('#chkBuisinessAddress').is(':checked')) {
                self.pilotexemptionrequestModel().PostalAddress().NumberStreet((self.pilotexemptionrequestModel().ResidentialAddress().NumberStreet()));
                self.pilotexemptionrequestModel().PostalAddress().Suburb((self.pilotexemptionrequestModel().ResidentialAddress().Suburb()));
                self.pilotexemptionrequestModel().PostalAddress().TownCity((self.pilotexemptionrequestModel().ResidentialAddress().TownCity()));
                self.pilotexemptionrequestModel().PostalAddress().PostalCode((self.pilotexemptionrequestModel().ResidentialAddress().PostalCode()));
                self.pilotexemptionrequestModel().PostalAddress().CountryCode((self.pilotexemptionrequestModel().ResidentialAddress().CountryCode()));
            }
            else {
                self.pilotexemptionrequestModel().PostalAddress().NumberStreet('');
                self.pilotexemptionrequestModel().PostalAddress().Suburb('');
                self.pilotexemptionrequestModel().PostalAddress().TownCity('');
                self.pilotexemptionrequestModel().PostalAddress().PostalCode('');
            }

            if ($("#PostalAddressNumberStreet").val() == null || $("#PostalAddressNumberStreet").val() == '' || $("#PostalAddressNumberStreet").val() == undefined) {
                $("#isPostalAddressNumberStreetMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressNumberStreetMsg").text('');
            }

            if ($("#PostalAddresssuburb").val() == null || $("#PostalAddresssuburb").val() == '' || $("#PostalAddresssuburb").val() == undefined) {
                $("#isPostalAddresssuburbMsg").text(errormessage);
            }
            else {
                $("#isPostalAddresssuburbMsg").text('');
            }

            if ($("#PostalAddressTownCity").val() == null || $("#PostalAddressTownCity").val() == '' || $("#PostalAddressTownCity").val() == undefined) {
                $("#isPostalAddressTownCityMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressTownCityMsg").text('');
            }

            if ($("#PostalAddressPostalCode").val() == null || $("#PostalAddressPostalCode").val() == '' || $("#PostalAddressPostalCode").val() == undefined) {
                $("#isPostalAddressPostalCodeMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressPostalCodeMsg").text('');
            }

            if ($("#PostalAddressCountry").val() == null || $("#PostalAddressCountry").val() == '' || $("#PostalAddressCountry").val() == undefined) {
                $("#isPostalAddressCountryMsg").text(errormessage);
            }
            else {
                $("#isPostalAddressCountryMsg").text('');
            }

            return true;
        }

        //AddessKeyupEvents method is set the all details in residential Address put postal address fields  on key up event the Pilot Exemption Request screen 
        self.AddessKeyupEvents = function () {

            if ($('#chkBuisinessAddress').is(':checked')) {
                $('#NumStreet').keyup(function () { $('#NumStreet2').val($(this).val()); });
                $('#Suburb').keyup(function () { $('#Suburb2').val($(this).val()); });
                $('#TownCity').keyup(function () { $('#TownCity2').val($(this).val()); });
                $('#PostalCode').keyup(function () { $('#PostalCode2').val($(this).val()); });
                $('#PostalAddressCountry').keyup(function () { $('#PostalAddressCountry2').val($(this).val()); });
            }
            else {
                var error = 4;
                if ($("#PostalAddressNumberStreet").val().trim() == $("#ResidentialAddressstreet").val().trim()) {
                    error = error - 1
                }
                if ($("#PostalAddresssuburb").val().trim() == $("#ResidentialAddresssuburb").val().trim()) {
                    error = error - 1
                }
                if ($("#PostalAddressTownCity").val().trim() == $("#ResidentialAddresstowncity").val().trim()) {
                    error = error - 1
                }
                if ($("#PostalAddressPostalCode").val().trim() == $("#ResidentialAddressPostalCode").val().trim()) {
                    error = error - 1
                }
                if ($("#PostalAddressCountry").val().trim() == $("#ResidentialAddressCountry").val().trim()) {
                    error = error - 1
                }

                if (error <= 0) {
                    self.pilotexemptionrequestModel().AddressCheckbox = true;
                    $('#chkBuisinessAddress').attr('checked', true);
                }
                else {
                    self.pilotexemptionrequestModel().AddressCheckbox = false;
                    $('#chkBuisinessAddress').attr('checked', false);
                }

                $('#NumStreet').keyup();
                $('#Suburb').keyup();
                $('#TownCity').keyup();
                $('#PostalCode').keyup();

                return false;
            }
        }

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
            var bdate
            dobDate.setFullYear(dobDate.getFullYear());//+18
            var IssuedDatepicker = $("#IssuedDate").data("kendoDatePicker");
            IssuedDatepicker.min(dobDate);//+18
            IssuedDatepicker.value('');
            $('#isDateofBirthMsg').text('');
        };

        CalcIssuedDate = function () {

            var IssuedDate = this.value();
            IssuedDate.setFullYear(IssuedDate.getFullYear());
            var RenewalDate1Datepicker = $("#RenewalDate1").data("kendoDatePicker");
            RenewalDate1Datepicker.min(IssuedDate);
            RenewalDate1Datepicker.value('');
            $('#isIssuedDateMsg').text('');

        };

        CalcRenewalDate = function () {
            $('#isRenewalDateMsg').text('');
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
                if ($("#PostalAddressTownCity").val().trim() != $("#ResidentialAddresstowncity").val().trim()) {
                    $('#chkBuisinessAddress').attr('checked', false);
            }
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

        $("#phone").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#Mobileno").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
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
                $("#spanCaptachCode").text('* This field is required.');
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }

        HandleContactnumber = function (data, event) {
            $("#isphoneMsg").text('');
        }

        HandleMobilenumber = function (data, event) {
            $("#isCellNoMsg").text('');
        }

        self.Initialize();
    }

    IPMSRoot.PilotExemptionRequestViewModel = PilotExemptionRequestViewModel;
}(window.IPMSROOT));