(function (IPMSRoot) {

    var TptDocumentUploadViewModel = function () {

        var self = this;
        $('#spnTitile').html("Tpt Document Upload");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.TptDocumentUploadModel = ko.observable(new IPMSROOT.TptDocumentUploadModel());
        self.TerminalDelaysList = ko.observableArray();
        self.PortsDetails = ko.observableArray();
        self.isUploadEnable = ko.observable(true);
        self.UploadDocument = ko.observableArray([]);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        //self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsSaveEnable = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsAdd = ko.observable(false);
        self.IsValid = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false);
        self.isHWPSfileToUpload = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.TerminalDelays = ko.observableArray([]);
        self.OutTurnVolumes = ko.observableArray([]);
        self.TerminalData = ko.observableArray([]);
        self.ArrivalDate = ko.observable();
        self.WeekNo = ko.observable();
        self.WeekEnding = ko.observable();


        self.Initialize = function () {
            // self.viewMode = ko.observable(true);
        }

        function ShowLocalDate() {
            var dNow = new Date();
            var localdate = (dNow.getMonth() + 1) + '/' + dNow.getDate() + '/' + dNow.getFullYear() + ' ' + dNow.getHours() + ':' + dNow.getMinutes();
            $('#currentDate').text(localdate)
        }


        /// To upload Documents ......................................
        
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {  
            self.UploadDocument.removeAll();
            $("#spanmultiplepfileToUpload").text("");
            self.ismultiplepfileToUpload(false);
            var documentType = $("input:radio[name=NewUser]").val();
            uploadedFiles.length = 0;
            var fullDate = new Date() 
            console.log(fullDate);
            var portname = $('#port option:selected').text();
            var portcode = $('#port option:selected').val();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                if (opmlFile.files.length > 1) {
                    toastr.error("Please upload only one document", "Error");
                    return;
                }
                else {
                    var elem = {};
                    elem.FileName = opmlFile.files[0].name;
                    var extension = opmlFile.files[0].name.split('.').pop().toLowerCase();
                    var fileExtension = ['csv', 'xlsx'];
                    if ($.inArray(extension, fileExtension) != -1) {
                        elem.DocumentName = $("input:radio[name=NewUser]:checked").attr("title");
                        elem.CategoryCode = $("input:radio[name=NewUser]:checked").val();
                        elem.FileDetails = opmlFile.files[0];
                        elem.IsAlreadyExists = false;
                       
                        self.UploadDocument.push(elem);
                        uploadedFiles.push(elem);
                    }
                    else {
                        toastr.error("Please upload the files with formats (CSV) only", "Error");
                        return;
                    }
                }

                var formData = new FormData();
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });
                var CategoryName = $("input:radio[name=NewUser]:checked").attr("title");
                var CategoryCode = $("input:radio[name=NewUser]:checked").val();

                self.viewModelHelper.apiUpload('api/File/TptDocFileUpload?documentType=' + CategoryCode + '&PortCode=' + portcode, formData, function Message(data) {
                    if (typeof(data)=="string") {
                        toastr.error(data, "Error");
                    }
                    else {                   
                         if (CategoryCode == 'TLDL') {
                            self.TptDocumentUploadModel().TerminalDelay(ko.utils.arrayMap(data, function (item) {
                            return new IPMSRoot.TerminalDelays(item);
                        }));
                        if (self.TptDocumentUploadModel().TerminalDelay().length > 0)
                            self.ArrivalDate(moment(self.TptDocumentUploadModel().TerminalDelay()[0].ArrivalDate()).format('YYYY-MM-DD HH:mm:ss'));
                     
                    }
                    else if (CategoryCode == 'OTVL') {
                        self.TptDocumentUploadModel().OutTurnVolumess(ko.utils.arrayMap(data, function (item) {
                            return new IPMSRoot.OutTurnVolumes(item);
                        }));
                         
                        if (self.TptDocumentUploadModel().OutTurnVolumess().length > 0)
                            self.ArrivalDate(moment(self.TptDocumentUploadModel().OutTurnVolumess()[0].ArrivalDate()).format('YYYY-MM-DD HH:mm:ss'));
                    }
                    else {
                        self.TptDocumentUploadModel().TerminalWeeklyData(ko.utils.arrayMap(data, function (item) {
                            
                            return new IPMSRoot.TerminalData(item);
                        }));
                        if (self.TptDocumentUploadModel().TerminalWeeklyData().length > 0) {
                            self.WeekNo(self.TptDocumentUploadModel().TerminalWeeklyData()[0].WeekNo());
                            self.WeekEnding(moment(self.TptDocumentUploadModel().TerminalWeeklyData()[0].WeekEnding()).format('YYYY-MM-DD HH:mm:ss'));
                        }
                        self.TptDocumentUploadModel().TerminalDelay(ko.utils.arrayMap(data, function (item) {
                            return new IPMSRoot.TerminalDelays(item);
                        }));


                    }

                    }
                });

            }
            else {
                $("#spanmultiplepfileToUpload").text('Please select file');
                self.ismultiplepfileToUpload(true);
            }
            $('#fileToUpload').val('');
            return;

        }

        self.SaveTptDocumentValues = function () {
            var CategoryCode = $("input:radio[name=NewUser]:checked").val();
            if (CategoryCode == 'TLDL') {
                if (self.TptDocumentUploadModel().TerminalDelay().length != 0) {
                    var errFlag = 0;
                    var recLength = self.TptDocumentUploadModel().TerminalDelay().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TptDocumentUploadModel().TerminalDelay()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveTerminalDelays(self.TptDocumentUploadModel().TerminalDelay());
                    }
                }

            } else if (CategoryCode == 'OTVL') {
                if (self.TptDocumentUploadModel().OutTurnVolumess().length != 0) {
                    var errFlag = 0;
                var recLength = self.TptDocumentUploadModel().OutTurnVolumess().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TptDocumentUploadModel().OutTurnVolumess()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveOutTurnVolumes(self.TptDocumentUploadModel().OutTurnVolumess());
                    }
                }
            }
            else if (CategoryCode == 'TLDT') {
                if (self.TptDocumentUploadModel().TerminalWeeklyData().length != 0) {
                    var errFlag = 0;
                    var recLength = self.TptDocumentUploadModel().TerminalWeeklyData().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TptDocumentUploadModel().TerminalWeeklyData()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveTerminalWeeklyData(self.TptDocumentUploadModel().TerminalWeeklyData());
                    }
                }
            }

        }
        //To Save Terminal Dealay Values data
        self.SaveTerminalDelays = function (TerminalDelay) { 
            if (self.IsSave() == true) {  
                self.viewModelHelper.apiPost('api/TerminalDelayDetails', ko.mapping.toJSON(TerminalDelay),
                            function Message(data) {
                                if (data.length > 0) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Terminal Delays saved successfully.", "Terminal Delays");
                                }
                                else {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "Terminal Delays");
                                }
                            }); 

            } 
        }

        //To save Outturn volumes data
        self.SaveOutTurnVolumes = function (OutTurnVolume) {
            debugger;
            if (self.IsSave() == true) {  
                self.viewModelHelper.apiPost('api/OutTurnVolumeDetails', ko.mapping.toJSON(OutTurnVolume),
                            function Message(data) { 
                                if (data.length > 0) { 
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Outturn Volumes data saved successfully.", "OutTurn Volumes");
                                }
                                else {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "OutTurn Volumes");
                                }
                            });  
            } 
        }

        //To save Terminal Weekly data
        self.SaveTerminalWeeklyData = function (TerminalData) {
            if (self.IsSave() == true) {
                self.viewModelHelper.apiPost('api/TerminalWeeklyDataDetails', ko.mapping.toJSON(TerminalData),
                            function Message(data) {
                                if (data.length > 0) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Terminal data saved successfully.", "Terminal Data");
                                }
                                else {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "Terminal Data");
                                }
                            });
            }
        }
        //To Reset values
        self.Reset = function (model) {
            self.TptDocumentUploadModel().reset();
            self.Reset();
            ko.validation.reset();
        }

        self.Cancel = function () {
            self.TptDocumentUploadModel().reset();
            $("#UploadFileType").html("");
            $("#lblUploadFileName").html("");
            $("#lblTldlArrivalDate").html("");
            $("#lblOtvlArrivalDate").html(""); 
            $("#lblWeekNo").html("");
            $("#lblWeekEnding").html(""); 
            $("[name=NewUser]").removeAttr("checked");
            $("input:radio[name=NewUser]:first").attr('checked', true);
            $(".TLDT").show();
            $(".TLDL, .OTVL").hide();
        }


         self.Clear = function () {
            self.TptDocumentUploadModel().reset();
            $("#UploadFileType").html("");
            $("#lblUploadFileName").html("");
            $("#lblTldlArrivalDate").html("");
            $("#lblOtvlArrivalDate").html("");
            if ($(this).attr("value") == "TLDT") {
                $(".TLDT").show();
                $(".TLDL, .OTVL").hide();
            }
            if ($(this).attr("value") == "TLDL") {
                $(".TLDL").show();
                $(".TLDT, .OTVL").hide();
            }
            if ($(this).attr("value") == "OTVL") {
                $(".OTVL").show();
                $(".TLDT, .TLDL").hide();
            }
        }

        HandleDesignationCodeKeyUp = function (data, event) {
            if (self.UniqueDesignationCodeVisible() == true) {
                self.UniqueDesignationCodeVisible(false);
            }
        }

        self.Initialize();
    }
    IPMSRoot.TptDocumentUploadViewModel = TptDocumentUploadViewModel;
}(window.IPMSROOT));
