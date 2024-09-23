(function (IPMSRoot) {
    var MobileIncidentReportViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.incidentReportReferenceData = ko.observable();
        self.mobileIncidentReportModel = ko.observable(new IPMSROOT.MobileIncidentReportModel());
        self.Initialize = function () {
            self.IncidentTypes();
        }

        // To get the different type of incidents
        self.IncidentTypes = function () {

            self.viewModelHelper.apiGet('api/MobileIncidentReporting/GetIncidentTypes',
                  null,
                function (result) {
                    self.incidentReportReferenceData(new IPMSRoot.IncidentReportReferenceData(result));
                }, null, null, false);
        }

        // To save the incident details
        self.SaveIncident = function (model) {
            self.MIRValidation = ko.observable(model);
            self.MIRValidation().errors = ko.validation.group(self.MIRValidation());
            var errors = self.MIRValidation().errors().length;
            if (errors == 0) {
                checkboxes = document.getElementsByName('IncidentTypes');
                selectedDocument = document.getElementsByName('files[]');
                var chk = 0;
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    if (checkboxes[i].checked) {
                        chk = 1;
                    }
                }
                if (chk == 1) {
                    if (selectedDocument[0].files.length >= 1) {
                        self.viewModelHelper.apiPost('api/MobileIncidentReporting/PostIncidentData', ko.mapping.toJSON(model),
                                 function Message(data) {
                                     toastr.options.closeButton = true;
                                     toastr.options.positionClass = "toast-top-right";
                                     toastr.success("Incident Reporting details Saved successfully", "Incident Reporting");
                                     window.location.href = "/Mobile/DashBoard/";
                                 });
                    }
                    else {
                        toastr.error("Please Upload one image.");
                    }
                }
                else {
                    toastr.error("Please Select at least One Incident nature.");

                }
            }
            else {
                self.MIRValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // To upload a document and save it to the database.
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            var documentType = $('#selUploadDocs option:selected').text();
            uploadedFiles = self.mobileIncidentReportModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            for (var i = 0; i < opmlFile.files.length; i++) {
                var elem = {};
                elem.FileName = opmlFile.files[i].name;
                elem.CategoryName = $('#selUploadDocs option:selected').text();
                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                elem.FileDetails = opmlFile.files[i];
                elem.IsAlreadyExists = false
                uploadedFiles.push(elem);
                self.mobileIncidentReportModel().UploadedFiles(uploadedFiles);
            }

            var formData = new FormData();
            $.each(self.mobileIncidentReportModel().UploadedFiles(), function (key, val) {
                formData.append(val.name, val.FileDetails);
            });

            self.viewModelHelper.apiUpload('api/File/Upload', formData, function Message(data) {
                var Adddoc = new IPMSROOT.IncidentDocumnet();
                Adddoc.DocumentID(data.DocumentID);
                Adddoc.FileName(data.FileName);
                self.mobileIncidentReportModel().IncidentDocuments.push(Adddoc);
            });
            return;
        }

        self.Initialize();
    }

    IPMSRoot.MobileIncidentReportViewModel = MobileIncidentReportViewModel;
}(window.IPMSROOT));

