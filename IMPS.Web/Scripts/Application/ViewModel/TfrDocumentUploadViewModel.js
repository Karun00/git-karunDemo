(function (IPMSRoot) {

    var TfrDocumentUploadViewModel = function () {

        var self = this;
        $('#spnTitile').html("Tfr Document Upload");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.TfrDocumentUploadModel = ko.observable(new IPMSROOT.TfrDocumentUploadModel()); 
        //self.PortsDetails = ko.observableArray();
        self.isUploadEnable = ko.observable(true);
        self.UploadDocument = ko.observableArray([]);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsAdd = ko.observable(false);
        self.IsValid = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false);
        //self.isHWPSfileToUpload = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper(); 
        self.RailPlan = ko.observableArray([]);
        self.PlanDate = ko.observable();

        self.Initialize = function () { 
        
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
            debugger;
            self.UploadDocument.removeAll();
            $("#spanmultiplepfileToUpload").text("");
            self.ismultiplepfileToUpload(false);
            var documentType = $("input:radio[name=NewUser]").val();
            uploadedFiles.length = 0; 
            var fullDate = new Date() 
            console.log(fullDate);
            var portcode = $('#port option:selected').val();
            var portname = $('#port option:selected').text();
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

                self.viewModelHelper.apiUpload('api/File/TfrDocFileUpload?documentType=' + CategoryCode + '&PortCode=' + portcode, formData, function Message(data) {
                    if (typeof (data) == "string") {
                        toastr.error(data, "Error");
                    }
                    else {
                        if (CategoryCode == 'RLOP') {
                            self.TfrDocumentUploadModel().RailPlan(ko.utils.arrayMap(data, function (item) {
                                return new IPMSRoot.RailPlan(item);
                            }));
                            if (self.TfrDocumentUploadModel().RailPlan().length > 0) 
                              self.PlanDate(moment(self.TfrDocumentUploadModel().RailPlan()[0].PlannedDate()).format('YYYY-MM-DD HH:mm:ss'));
                        }
                        else if (CategoryCode == 'RLCN') {
                            self.TfrDocumentUploadModel().RailChangeNotifications(ko.utils.arrayMap(data, function (item) {
                                return new IPMSRoot.RailChangeNotifications(item);
                            }));

                            if (self.TfrDocumentUploadModel().RailChangeNotifications().length > 0)                                 
                                self.PlanDate(moment(self.TfrDocumentUploadModel().RailChangeNotifications()[0].PlannedDate()).format('YYYY-MM-DD HH:mm:ss'));
                        }
                        else if (CategoryCode == 'DAAN') {
                            self.TfrDocumentUploadModel().ArrivalAndDepartureTimes(ko.utils.arrayMap(data, function (item) {
                                return new IPMSRoot.ArrivalAndDepartureTimes(item);
                            }));
                            if (self.TfrDocumentUploadModel().ArrivalAndDepartureTimes().length > 0)                                
                            self.PlanDate(moment(self.TfrDocumentUploadModel().ArrivalAndDepartureTimes()[0].PlannedDate()).format('YYYY-MM-DD HH:mm:ss'));
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

        self.SaveTfrDocumentValues = function () {
           
            var CategoryCode = $("input:radio[name=NewUser]:checked").val();
            if (CategoryCode == 'RLOP') {
                if (self.TfrDocumentUploadModel().RailPlan().length != 0) {
                    var errFlag=0;
                    var recLength = self.TfrDocumentUploadModel().RailPlan().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TfrDocumentUploadModel().RailPlan()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveRailPlan(self.TfrDocumentUploadModel().RailPlan());
                    }
                }

            } else if (CategoryCode == 'RLCN') {
                if (self.TfrDocumentUploadModel().RailChangeNotifications().length != 0) {
                    var errFlag=0;
                    var recLength = self.TfrDocumentUploadModel().RailChangeNotifications().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TfrDocumentUploadModel().RailChangeNotifications()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveRailChangeNotifications(self.TfrDocumentUploadModel().RailChangeNotifications());
                    }
                }
            }
            else if (CategoryCode == 'DAAN') {
               
                if (self.TfrDocumentUploadModel().ArrivalAndDepartureTimes().length != 0) {
                    var errFlag=0;
                    var recLength = self.TfrDocumentUploadModel().ArrivalAndDepartureTimes().length;
                    for (var i = 0; i < recLength; i++) {
                        if (self.TfrDocumentUploadModel().ArrivalAndDepartureTimes()[i].RecordStatus() == "E") {
                            errFlag = errFlag + 1;
                        }
                    }
                    if (errFlag != 0) {
                        toastr.error("Please correct the data error before saving.", "Error");
                        return;
                    }
                    else {
                        self.SaveArrivalAndDepartureTimes(self.TfrDocumentUploadModel().ArrivalAndDepartureTimes());
                    }
                }
            }
            

        }
        //To Save Rail Plan data
        self.SaveRailPlan = function (RailPlan) {
            
            if (self.IsSave() == true) {  
                self.viewModelHelper.apiPost('api/RailPlanDetails', ko.mapping.toJSON(RailPlan),
                            function Message(data) {
                                if (data.length > 0) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Rail Plan saved successfully.", "Rail Plan");
                                }
                                else {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "Rail Plan");
                                }
                            }); 

            } 
        }

        //To save Rail Change Notifications data
        self.SaveRailChangeNotifications = function (RailChangeNotifications) {            
            if (self.IsSave() == true) {  
                self.viewModelHelper.apiPost('api/RailChangeNotifications', ko.mapping.toJSON(RailChangeNotifications),
                            function Message(data) { 
                                if (data.length > 0) { 
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Rail Change Notifications data saved successfully.", "Rail Change Notifications");
                                }
                                else
                                {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "Rail Change Notifications");
                                }
                            });  
            } 
        }

        //To save Arrival And Departure Times data
        self.SaveArrivalAndDepartureTimes = function (ArrivalAndDepartureTimes) {
          
            if (self.IsSave() == true) {
                self.viewModelHelper.apiPost('api/ArrivalAndDepartureTimes', ko.mapping.toJSON(ArrivalAndDepartureTimes),
                            function Message(data) {
                                if (data.length > 0) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Arrival and Departure times saved successfully.", "Arrival And Departure Times");
                                }
                                else {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("There is an error occured while saving the data.Please contact the administrator.", "Arrival And Departure Times");
                                }
                            });
            }
        }
        //To Reset values
        self.Reset = function (model) { 
            self.TfrDocumentUploadModel().reset();
            self.Reset();
            ko.validation.reset(); 
        }

        self.Cancel = function () { 
            self.TfrDocumentUploadModel().reset();
            $("#UploadFileType").html("");
            $("#lblUploadFileName").html("");
            $("#lblPlanDate").html("");
            $("#lblRCNPlanDate").html("");
            $("#lblADTPlanDate").html("");
            
            $("[name=NewUser]").removeAttr("checked");
           
            $("input:radio[name=NewUser]:first").attr('checked', true);
            $(".RLOP").show();
            $(".RLCN, .DAAN").hide();
        } 

        HandleDesignationCodeKeyUp = function (data, event) {
            if (self.UniqueDesignationCodeVisible() == true) {
                self.UniqueDesignationCodeVisible(false);
            }
        } 
        self.Initialize();
    }
    IPMSRoot.TfrDocumentUploadViewModel = TfrDocumentUploadViewModel;
}(window.IPMSROOT)); 