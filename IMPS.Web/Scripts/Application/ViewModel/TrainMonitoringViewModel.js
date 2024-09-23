(function (IPMSRoot) {

    var TrainMonitoringViewModel = function () {

        var self = this;
        $('#spnTitile').html("Tfr Document Upload");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.tfrDocumentUploadModel = ko.observable(new IPMSROOT.TfrDocumentUploadModel()); 
     
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(); 
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true); 
        self.IsValid = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false); 
        self.validationHelper = new IPMSRoot.validationHelper(); 
        self.RailPlan = ko.observableArray([]);
        self.WagonDetails = ko.observableArray([]);
        self.PlanDate = ko.observable(new Date());
        self.FromDate = ko.observable(new Date());
        self.ToDate = ko.observable(new Date());
        self.Initialize = function () { 
           // $("#PlanDate").data('kendoDatePicker').enable(true);
        } 
        function ShowLocalDate() {
            var dNow = new Date();
            var localdate = (dNow.getMonth() + 1) + '/' + dNow.getDate() + '/' + dNow.getFullYear() + ' ' + dNow.getHours() + ':' + dNow.getMinutes();
            $('#currentDate').text(localdate)
        }


        /// To upload Documents ......................................
        
        var uploadedFiles = [];
        var documentData = [];
       
        self.GetTrainData = function () {
            debugger;
            var parFromDate = $("[id=lblFromDate]").val();
            var parToDate = $("[id=lblToDate]").val();
           //var parCorridor = 'CAPECOR1-ITP'; 
            var parMovementStatus = $("[id=ddlMovementStatus]").val(); 
            // self.viewModelHelper.apiGet('api/TrainMonitoringDetails/' + parPlannedDate + '/' + parCorridor + '/' + parMovementStatus, {}, function (data) {
            self.viewModelHelper.apiGet('api/TrainMonitoringDetails/' + parFromDate + '/' + parToDate, {}, function (data) {
               self.tfrDocumentUploadModel().RailPlan(ko.utils.arrayMap(data, function (item) {
                    return new IPMSRoot.RailPlan(item);
                }));
                if (self.tfrDocumentUploadModel().RailPlan().length > 0)
                    self.PlanDate(self.tfrDocumentUploadModel().RailPlan()[0].PlannedDate());
            }, null, null, false); 
        }

        self.GetWagonData = function () {
            debugger;
            var trainNo = $("#btnTrainNo").text();
            var TrainOrigin = $("#TrainOrigin").text();
            var OriginDate = $("#lblOriginDate").text();
            
            self.viewModelHelper.apiGet('api/WagonDetails/' + trainNo + '/' + TrainOrigin+'/'+OriginDate, {}, function (data) {
                debugger;
                self.tfrDocumentUploadModel().WagonDetails(ko.utils.arrayMap(data, function (item) {
                    return new IPMSRoot.WagonDetails(item);
                }));
            }, null, null, false);
            }
        //To Save Rail Plan data
        self.SaveRailPlan = function (RailPlan) {
            debugger;
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
     
        //To Reset values
        self.Reset = function (model) { 
            self.tfrDocumentUploadModel().reset();
            self.Reset();
            ko.validation.reset(); 
        }

        self.Cancel = function () {
            debugger;
            self.tfrDocumentUploadModel().reset();
            self.tfrDocumentUploadModel(new IPMSRoot.TfrDocumentUploadModel());
           // $("input:radio[name=NewUser]:first").attr('checked', true); 
        }  
      
       
        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        HandleDesignationCodeKeyUp = function (data, event) {
            if (self.UniqueDesignationCodeVisible() == true) {
                self.UniqueDesignationCodeVisible(false);
            }
        } 
        self.Initialize();
    }
    IPMSRoot.TrainMonitoringViewModel = TrainMonitoringViewModel;
}(window.IPMSROOT)); 