(function (IPMSRoot) {

    var PortInformationViewModel = function () {

        var self = this;
        $('#PortInfMaster').html("Port Information");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.id = ko.observable();
        self.editableView = ko.observable(true);
        self.portInformationModel = ko.observable();
        self.ContentRoleTypes = ko.observableArray([]);

        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.Roles = ko.observableArray();
        self.portcontentRoledet = ko.observable()
        self.IsEditable = ko.observable(true);
        self.isHWPSfileToUpload = ko.observable(false);
        self.moduleSelected = ko.observable(true);
        self.PortContentRole = ko.observableArray();
        self.UploadedFiles = ko.observableArray([]);
        self.Uploadfiledetails = ko.observable();
        self.DocumentID = ko.observable(null);
        self.DocumentName = ko.observable("");

        //page load
        self.Initialize = function () {
            self.viewMode(true);
            $('#PortInfMaster').html("Port Information");
            self.portInformationModel(new IPMSROOT.PortInformationModel());
            self.viewMode('List');
            self.LoadModulesTreeview();
            self.LoadRoledetails();
        }

        //Tree view 
        self.ContentdataTreeView = ko.observableArray();

        // Loads the module data for the tree view
        self.LoadModulesTreeview = function () {
            self.viewModelHelper.apiGet('api/PortInformation', null,
                function (result) {
                    $.each(result, function (key, val) {
                        $.each(val.PortContent1, function (key1, val1) {
                        });
                    });
                    ko.mapping.fromJS(result, {}, self.ContentdataTreeView);
                });
        }

        self.LoadRoledetails = function () {
            self.viewModelHelper.apiGet('api/PortRoles', null, function (result) {
                self.ContentRoleTypes(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.Roles(item);
                }));
            });
        }

        self.ContentChecked = function () {
            if ($("#lnkConId").is(':checked') == true) {
                $("#editorId").show();
                $("#uploaddocId").hide();
                $("#lnkConId").attr('checked', 'checked');
                $("#lnkDocId").removeAttr('checked', 'checked');
            }
        }

        self.DocumentChecked = function () {
            if ($("#lnkDocId").is(':checked') == true) {
                $("#uploaddocId").show();
                $("#editorId").hide();
                $("#lnkDocId").attr('checked', 'checked');
                $("#lnkConId").removeAttr('checked', 'checked');
            }
        }

        self.Employeechecked = function () {
            $("#ChkRolelistId").show();
        }

        //PortInformation in Add mode
        self.addPortInformation = function () {
            $("#stack1").modal('show');
            self.portInformationModel(new IPMSROOT.PortInformationModel());
            $('#PortInfMaster').html("Port Information");
            self.viewMode('List');
            self.IsSave(true);
            self.IsUpdate(false);
            self.IsReset(true);
        }

        self.EditPortInformation = function (model) {
            $("#stack1").modal('show');
            self.portInformationModel(new IPMSROOT.PortInformationModel(ko.mapping.toJS(model)));
            $('#spnTitile1').html("Edit Category")
            self.viewMode('List');
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsReset(true);
        }

        self.AddLinkInformation = function (model) {

            self.portInformationModel(new IPMSROOT.PortInformationModel());
            self.portInformationModel().ParentPortContentID(model.PortContentID());
            $('#PortInfMaster').html("Add Port Link Information");
            self.viewMode('Form');
            $("#stack2").show();

            $('#EmpChkLIstId').click(function () {
                if ($("#EmpChkLIstId").is(':checked') == true) {
                    $("#ChkRolelistId").show();
                }
                else {
                    $("#ChkRolelistId").hide();
                    var checkboxes = document.getElementsByName('Rolelist');
                    for (var i = 0, n = checkboxes.length; i < n; i++) {
                        checkboxes[i].checked = false;
                    }
                }
            });

            $("#lnkDocId").click(function () {
                if ($("#lnkDocId").is(':checked') == true) {
                    $("#uploaddocId").show();
                    $("#editorId").hide();
                }
            });

            $("#lnkConId").click(function () {
                if ($("#lnkConId").is(':checked') == true) {
                    $("#editorId").show();
                    $("#uploaddocId").hide();
                }
            });

            $("#GenId").click(function () {
                $("#LnkViewId").hide();
                $("#ChkRolelistId").hide();
            });

            $("#usrId").click(function () {
                $("#LnkViewId").show();
            });

            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
        }

        //Edit EditPortCategory button
        self.EditPortCategory = function (model) {
            self.portInformationModel(new IPMSROOT.PortInformationModel(ko.mapping.toJS(model)));
            $('#stack1').modal('show')
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true)
            $('#PortInfMaster').html("Update Port Link");
        }

        self.Contentview = function (model) {
            //if (model.DocumentID() != null) {
            //    var id = model.DocumentID();
            //    var url = '/FileDownload/Download/' + id;
            //    window.location.href = url;

            //anusha 20/03/2024  start

            if (model.DocumentID() != null) {
                var id = model.DocumentID();
                var url = '/FileDownload/DownloadPortInfo/' + id;
                var a = document.createElement('a');
                a.style.display = 'none';
                a.href = url;
                url.Editable = false;
                a.download = 'Your not authorized for this document';
                a.contentEditable = false;
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            }
                //anusha 20/03/2024  end


            else {
                $("#stack1").modal('show');
                self.portInformationModel(new IPMSROOT.PortInformationModel(ko.mapping.toJS(model)));
                $('#spnTitile1').html("");
                $("#categoryId").hide();
                $("#contentid").show();

                $("#field1").css({ width: '80%', height: '100%' });

                $('.k-editor-toolbar').hide();

                var editor = $("#linkId").data("kendoEditor"),
               editorBody = $(editor.body);

                //make readonly
                editorBody.add("td", editorBody).removeAttr("contenteditable");

                self.viewMode('List');
                self.IsSave(false);
                self.IsUpdate(false);
                self.IsReset(false);
            }
        }
//srinivas
        self.moduleSelected = function (model) {
          
            self.portInformationModel(new IPMSROOT.PortInformationModel(ko.mapping.toJS(model)));
            self.viewMode('Form');
            $("#stack2").show();
            var PortcontentId = model.PortContentID();
            var usertype = "";
           

            self.viewModelHelper.apiGet('api/PortRoleInformation/' + PortcontentId, null, function (result) {
                
                if (result.length > 0) {
                    $("#GenId").attr('disabled', 'disabled');
                    self.portInformationModel().PortContentRole(result);
                    var checkboxes = document.getElementsByName('Rolelist');
                    self.portInformationModel(ko.utils.arrayMap(result, function (item) {
                       
                            //var DocumentName = item.DocumentName();
                            $("#HDocId").text(item.DocumentName);
                                
                            
                        
                        usertype = item.UserType;
                        if (item.UserType == "EMP") {
                            $("#EmpChkLIstId ").attr('checked', 'checked');
                            $("#ChkRolelistId").show();
                        }

                        if (item.UserType == "AGNT") {
                            $("#AgenchkListId").attr('checked', 'checked');
                        }

                        if (item.UserType == "TO") {
                            $("#TerchkListId").attr('checked', 'checked');
                        }

                        for (var i = 0, n = checkboxes.length; i < n; i++) {
                            var testnumber = document.getElementsByName("Rolelist")[i].value;
                            if (item.RoleID == testnumber) {
                                checkboxes[i].checked = true;
                            }
                        }
                    }));
                } else {
                    $("#ChkRolelistId").hide();
                }
                

            });

            if (model.DocumentID() != null) {
                $("#editorId").hide();
                $("#uploaddocId").show();
                $("#lnkConId").attr('disabled', 'disabled');
            }
            else {
                $("#lnkDocId").attr('disabled', 'disabled');

            }

            if (model.DocumentID() != null) {
                var DocumentId = model.DocumentID();
                self.viewModelHelper.apiGet('api/Portdocumentdetails/' + DocumentId, {}, function (result) {
                    $("#HDocId").attr('href', '/FileDownload/Download/' + result.DocumentID);
                    $("#HDocId").text(result.DocumentPath);
                    model.DocumentName = result.DocumentPath;
                    model.Document = result;
                });
            }

            if (model.LinkVisibility() == "G") {
                $("#LnkViewId").hide();
                $("#ChkRolelistId").hide();
                $("#usrId").attr('disabled', 'disabled');
            }
            else {
                $("#GenId").attr('disabled', 'disabled');
            }

            $('#EmpChkLIstId').click(function () {
                if ($("#EmpChkLIstId").is(':checked') == true) {
                    $("#ChkRolelistId").show();
                }
                else {
                    $("#ChkRolelistId").hide();
                }
            });

            $("#lnkDocId").click(function () {
                if ($("#lnkDocId").is(':checked') == true) {
                    $("#uploaddocId").show();
                    $("#editorId").hide();
                }
            });

            $("#lnkConId").click(function () {
                if ($("#lnkConId").is(':checked') == true) {
                    $("#editorId").show();
                    $("#uploaddocId").hide();
                }
            });

            $("#GenId").click(function () {
                $("#LnkViewId").hide();
                $("#ChkRolelistId").hide();
            });

            $("#usrId").click(function () {
                $("#LnkViewId").show();
            });

            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            $('#PortInfMaster').html("Update Port Link Information");
            $("#EditLink").html("Edit Link");
        }

        //Action : Button File Upload
        //Purpose : Hot Work Permit Service File Upload
        self.HWPSuploadFile = function (model) {
            $("#spanHWPSfileToUpload").text("");
            self.isHWPSfileToUpload(false);
            var documentType = $('#selUploadDocs option:selected').text();
            var uploadedFiles = [];
            uploadedFiles = self.UploadedFiles();

            var opmlFile = $('#HWPSfileToUpload')[0];

            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {
                    var elem = {};
                    elem.FileName = opmlFile.files[i].name;
                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                    elem.FileDetails = opmlFile.files[i];
                    elem.IsAlreadyExists = false

                    var ext = elem.FileName.split('.').pop().toLowerCase();

                    if ($.inArray(ext, ['pdf', 'docx', 'xlsx', 'jpg ']) == -1) {
                        $("#spanHWPSfileToUpload").text('Select .pdf, .doc, .xls, .jpg files only');
                        self.isHWPSfileToUpload(true);
                    }
                    else {
                        uploadedFiles.push(elem);
                    }
                }

                var formData = new FormData();
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });
                if (opmlFile.files.length > 0) {
                    self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData,
                        function Message(data) {
                            self.Listdocuments = ko.observableArray();
                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                                if (model.PortContentID() != 0) {
                                    model.DocumentID(item.DocumentID);
                                }
                                else {
                                    self.portInformationModel().DocumentID(item.DocumentID);
                                }

                                $("#HDocId").text(item.FileName);
                                $("#HDocId").attr('href', '/FileDownload/Download/' + item.DocumentID);
                            }));
                        });
                }
            }
            else {
                $("#spanHWPSfileToUpload").text('Please select file.');
                self.isHWPSfileToUpload(true);
            }
            self.UploadedFiles([]);
            $('#HWPSfileToUpload').val('');
            return;
        }

        //Add Portcontent data saving data in API Service 
        self.SavePortInformation = function (model) {
            self.PortInformationValidation = ko.observable(model);
            self.PortInformationValidation().errors = ko.validation.group(self.PortInformationValidation());

            var errors = self.PortInformationValidation().errors().length;

            if (errors == 0) {
                var rolelistObjArry = new Array();
                $('#divRoleList input[name="Rolelist"]:checked').each(function () {
                    var applPortWorkFlowObj = new ApplicantWorkFlow(this.value, "EMP", self.portInformationModel().CreatedBy(), self.portInformationModel().CreatedDate());
                    rolelistObjArry.push(applPortWorkFlowObj);
                });

                if ($("#AgenchkListId").is(':checked') == true) {
                    var applPortWorkFlowObj = new ApplicantWorkFlow(2, "AGNT", self.portInformationModel().CreatedBy(), self.portInformationModel().CreatedDate());
                    rolelistObjArry.push(applPortWorkFlowObj);
                }

                if ($("#TerchkListId").is(':checked') == true) {
                    var applPortWorkFlowObj = new ApplicantWorkFlow(4, "TO", self.portInformationModel().CreatedBy(), self.portInformationModel().CreatedDate());
                    rolelistObjArry.push(applPortWorkFlowObj);
                }
                var recordstatus = document.getElementsByName("status").item(0).value
                self.portInformationModel().RecordStatus(recordstatus);
                self.portInformationModel().PortContentRole(rolelistObjArry);
                self.viewModelHelper.apiPost('api/PortInformation', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Port information saved successfully.", "Port Information");
                    $("#stack1").modal('hide');
                    self.viewMode('List');
                    self.LoadModulesTreeview();
                });
            }
            else {
                self.PortInformationValidation().errors.showAllMessages();
                return false;
            }
        }

        self.ModifyPortInformation = function (model) {
            self.PortInformationValidation = ko.observable(model);
            self.PortInformationValidation().errors = ko.validation.group(self.PortInformationValidation());
            var errors = self.PortInformationValidation().errors().length;

            if (errors == 0) {

                var rolelistObjArry = new Array();

                $('#divRoleList input[name="Rolelist"]:checked').each(function () {

                    var item = this.value;
                    function ApplicantWorkFlow(RoleID, UserType, CreatedBy, CreatedDate) {
                        this.RoleID = RoleID;
                        this.UserType = UserType;
                        this.CreatedBy = CreatedBy;
                        this.CreatedDate = CreatedDate;
                    }
                    var applPortWorkFlowObj = new ApplicantWorkFlow(this.value, 'EMP', '', '');
                    rolelistObjArry.push(applPortWorkFlowObj);
                });
                if ($("#AgenchkListId").is(':checked') == true) {
                    var applPortWorkFlowObj = new ApplicantWorkFlow(2, "AGNT", "", "");
                    rolelistObjArry.push(applPortWorkFlowObj);
                }
                if ($("#TerchkListId").is(':checked') == true) {
                    var applPortWorkFlowObj = new ApplicantWorkFlow(4, "TO", "", "");
                    rolelistObjArry.push(applPortWorkFlowObj);

                }
                model.PortContentRole = rolelistObjArry;

                self.viewModelHelper.apiPut('api/PortInformation', ko.mapping.toJSON(model), function Message(model) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Port information updated successfully.", "Port Information");
                    $("#stack1").modal('hide');
                    self.viewMode('List');
                    $('#PortInfMaster').html("Port Information");
                    self.LoadModulesTreeview();

                });
            }
            else {
                self.PortInformationValidation().errors.showAllMessages();
                return false;
            }
        }

        self.Cancel = function (model) {

            $("#stack1").modal('hide');
            self.viewMode('List');
            $('#PortInfMaster').html("Port Information");
            self.PortInformationValidation = ko.observable(model);
            model.validationEnabled(false);
            if (model.PortContentID() != 0) {
                model.reset();
            }
            else {
                self.portInformationModel().reset();
            }
        }

        self.ResetPortInformation = function (model) {
            ko.validation.reset();
            self.PortInformationValidation = ko.observable(model);
            model.validationEnabled(false);

            if (model.PortContentID() != 0) {
                var checkboxes = document.getElementsByName('Rolelist');
                var contname = model.ContentName();
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    var testnumber = document.getElementsByName("Rolelist")[i].value;
                    checkboxes[i].checked = false;

                    $.map(model.PortContentRole(), function (item) {
                        var dets = item;
                        if (dets.UserType == "EMP") {
                            $("#EmpChkLIstId ").attr('checked', 'checked');
                            $("#ChkRolelistId").show();
                        }

                        if (dets.RoleID == 2) {
                            $("#AgenchkListId").attr('checked', 'checked');
                        }

                        if (dets.RoleID == 4) {
                            $("#TerchkListId").attr('checked', 'checked');
                        }

                        if (testnumber == dets.RoleID) {
                            checkboxes[i].checked = true;
                        }
                    });
                }

                if (model.DocumentID() != null || model.DocumentID() != 0) {
                    var item = model.Document();
                    $("#HDocId").attr('href', '/FileDownload/Download/' + item.DocumentID);
                    $("#HDocId").text(item.DocumentPath);
                }

                model.reset();
            }
            else {
                $("#EmpChkLIstId").removeAttr("checked", "true");
                $("#AgenchkListId").removeAttr("checked", "true");
                $("#TerchkListId").removeAttr("checked", "true");
                $("#ChkRolelistId").hide();
                $("#editorId").show();
                $("#uploaddocId").hide();
                $("#LnkViewId").show();
                self.portInformationModel().reset();
            }
        }

        self.Initialize();
    }

    IPMSRoot.PortInformationViewModel = PortInformationViewModel;

}(window.IPMSROOT));

function ApplicantWorkFlow(RoleID, UserType, CreatedBy, CreatedDate) {
    this.RoleID = RoleID;
    this.UserType = UserType;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
}