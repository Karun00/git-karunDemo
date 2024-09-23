(function (IPMSRoot) {
    var IncidentReportViewModel = function () {
        var self = this;
        $('#incRptTitle').html("Incident Reporting");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.incidentReportReferenceData = ko.observable();
        self.IncidentReportList = ko.observableArray();
        self.incidentReportModel = ko.observable();
        self.IncidentTypes = ko.observableArray([]);
        self.fileSizeConfigValue = ko.observable();
        self.IsCodeEnable = ko.observable(true);
        self.Initialize = function () {
            self.incidentReportModel(new IPMSROOT.IncidentReportModel());
            self.LoadIncidentReportList();
            self.LoadIncidentTypes();
            self.GetFileSizeConfigValue();
            self.viewMode('List');
        }

        self.LoadIncidentReportList = function () {
            self.viewModelHelper.apiGet('api/IncidentReports', null, function (result) {
                self.IncidentReportList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.IncidentReportModel(item);
                }));
            }, null, null, false);
        }

        // To get the different type of incidents
        self.LoadIncidentTypes = function () {
            self.viewModelHelper.apiGet('api/MobileIncidentReporting/GetIncidentTypes',
                  null,
                function (result) {
                    self.IncidentTypes(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.IncidentType(item);
                    }));
                }, null, null, false);
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            $.ajax({
                url: '/api/File/GetFileSizeConfigValue',
                dataType: 'json',
                success: function (result) {
                    self.fileSizeConfigValue(result);
                }
            });
        }

        self.viewIncidentReport = function (model) {
            self.IsCodeEnable(false);
            self.viewMode('Form');
            $('#incRptTitle').html("View Incident Reporting");
            self.incidentReportModel(model);
        }

        self.editIncidentReport = function (model) {
            self.IsCodeEnable(true);
            self.viewMode('Form');
            $('#incRptTitle').html("Update Incident Reporting");
            self.incidentReportModel(model);
        }

        self.ModifyIncident = function (model) {
            self.IRValidation = ko.observable(model);
            self.IRValidation().errors = ko.validation.group(self.IRValidation());
            var errors = self.IRValidation().errors().length;
            if (errors == 0) {
                checkboxes = document.getElementsByName('IncidentTypes');
                var chk = 0;
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    if (checkboxes[i].checked) {
                        chk = 1;
                    }
                }
                if (chk == 1) {
                    self.viewModelHelper.apiPut('api/MobileIncidentReporting/ModifyIncidentData', ko.mapping.toJSON(model),
                   function Message(data) {
                       toastr.options.closeButton = true;
                       toastr.options.positionClass = "toast-top-right";
                       toastr.success("Incident Reporting details updated successfully.", "Incident Reporting");
                       self.LoadIncidentReportList();
                       self.viewMode('List');
                       $('#incRptTitle').html("Incident Reporting");
                   });
                }
                else {
                    toastr.warning("Please Select at least One Incident nature.");
                }
            }
            else {
                self.IRValidation().errors.showAllMessages();
                return;
            }
        }

        // To upload a document and save it to the database.
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            //window.location.href = "#Report-Incident";

            uploadedFiles = self.incidentReportModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {

                    var match = ko.utils.arrayFirst(self.incidentReportModel().IncidentDocuments(), function (item) {
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
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                self.incidentReportModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.warning("Please upload the files with formats (PDF, word, excel, .PNG) only", "Warning");
                                return;
                            }
                        }
                        else {
                            toastr.warning("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                            return;
                        }
                    }
                    else {
                        toastr.warning("The selected file already exist, Please upload another file", "Error");
                        return;
                    }
                }

                var formData = new FormData();

                ko.utils.arrayMap(self.incidentReportModel().UploadedFiles(), function (item) {
                    formData.append(item.FileName, item.FileDetails);
                });

                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.IncidentDocumnet();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
                        self.incidentReportModel().IncidentDocuments.push(Adddoc);
                    });
                });

            } else {
                toastr.warning("Please select image", "Error");
            }

            self.incidentReportModel().UploadedFiles([]);
            $('#fileToUpload').val('');
            return;
        }

        self.DeleteDocument = function (documentRow) {
            self.incidentReportModel().IncidentDocuments.remove(documentRow);
        }

        self.ResetIncidentReport = function (model) {
            self.incidentReportModel().reset();
        }

        self.Cancel = function (model) {
            self.viewMode('List');
            self.incidentReportModel().reset();
            self.LoadIncidentReportList();
            $('#incRptTitle').html("Incident Reporting");
        }

        self.Initialize();
    }

    IPMSRoot.IncidentReportViewModel = IncidentReportViewModel;
}(window.IPMSROOT));

