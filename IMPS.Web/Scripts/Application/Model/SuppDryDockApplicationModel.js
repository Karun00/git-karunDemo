(function (ipmsRoot) {

    var vesselModel = function (data) {
        var self = this;

        self.Vessels = ko.observableArray(data ? $.map(data, function (item1) { return new vesselDetail(item1); }) : []);
    }

    //Getting all the details of ArrivalNotification Based on VCN Number
    var vesselDetail = function (data) {

        var self = this;

        self.VCN = ko.observable(data ? data.VCN : "");
        self.VesselName = ko.observable(data ? (data.Vessel ? data.Vessel.VesselName : data.VesselName) : "");
        self.IMONo = ko.observable(data ? (data.Vessel ? data.Vessel.IMONo : data.IMONo) : "");
        self.GrossRegisteredTonnageInMT = ko.observable(data ? (data.Vessel ? data.Vessel.GrossRegisteredTonnageInMT : data.GrossRegisteredTonnageInMT) : "");
        self.LengthOverallInM = ko.observable(data ? (data.Vessel ? data.Vessel.LengthOverallInM : data.LengthOverallInM) : "");
        self.BeamInM = ko.observable(data ? (data.Vessel ? data.Vessel.BeamInM : data.BeamInM) : "");
        self.ArrDraft = ko.observable(data ? data.ArrDraft : "");
        self.CurrentBerth = ko.observable(data ? data.CurrentBerth : "");
    }

    //Getting all the details of Agent Based on VCN Number
    var agentDetail = function (data) {

        var self = this;

        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
        self.TradingName = ko.observable(data ? data.TradingName : "");
        self.TelephoneNo1 = ko.observable(data ? data.TelephoneNo1 : "");
        self.FaxNo = ko.observable(data ? data.FaxNo : "");
        self.TelephoneNo2 = ko.observable(data ? data.TelephoneNo2 : "");

    }
  
    var SuppDryDockModel = function (data) {

        var self = this;
        
        self.SuppDryDockID = ko.observable();
        self.validationEnabled = ko.observable(false);
        self.VCN = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select VCN' } });
        self.VesselName = ko.observable();
        self.VesselData = ko.observable();
        self.AnyDangerousGoods = ko.observable();
        self.AgentData = ko.observable();
        self.IsValidationEnabled = ko.observable(false);
        self.DockPortCode = ko.observable();
        self.FromDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.ToDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.BarkeelCode = ko.observable();
        self.CargoTons = ko.observable();
        self.Ballast = ko.observable();
        self.Bunkers = ko.observable();
        self.ExtensionDateTime = ko.observable();
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable();
        self.TermsandConditions = ko.observable();
        self.IsConfirmCancel = ko.observable(data ? data.IsConfirmCancel : "N");
        self.WorkflowInstanceID = ko.observable();
        self.WorkflowTaskCode = ko.observable();
        self.WorkflowInstanceTaskName = ko.observable();
        self.WFStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedDate = ko.observable();

        self.ApplicationDateTime = ko.observable();

        self.VesselAgent = ko.observable();

        self.Chamber = ko.observable("");
        self.EnteredDockDateTime = ko.observable();
        self.OnBlocksDateTime = ko.observable();
        self.DryDockDateTime = ko.observable();
        self.FinishedDockDateTime = ko.observable();
        self.OffBlocksDateTime = ko.observable();
        self.LeftDockDateTime = ko.observable();

        self.DockQuayCode = ko.observable();
        self.DockBerthCode = ko.observable();
        self.ScheduleFromDate = ko.observable();
        self.ScheduleToDate = ko.observable();
        self.ScheduleStatus = ko.observable();
        self.CurrentBerth = ko.observable();

        self.SuppWFStatus = ko.observable();
        self.LeftDockDateTime1 = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.DocumentTypes = ko.observableArray();
        self.DocumentTypeCode = ko.observable();
        self.WorkflowTaskCode = ko.observable(data ? data.WorkflowTaskCode : "");       
        self.WFStatusSupp = ko.computed(function () {
            return self.WFStatus() == 'Approve' ? "Approved" : self.WFStatus() == 'Reject' ? "Rejected" : self.WFStatus() == 'Confirm' ? "Confirmed" : self.WFStatus() == 'Cancel' ? "Cancelled" : self.WFStatus() == 'Update Request' ? "Update Request" : self.WFStatus() == 'Confirmation Cancel' ? "Cancel Confirmation Pending" : self.WFStatus() == "Cancel Request Approve" ? "Cancellation Approved" : self.WFStatus() == 'Cancel Request Reject' ? "Confirmed" : "New Request";
        });

        //Added by srinivas start
        self.isCancelVisible = ko.observable();
        self.isCancelVisible = ko.computed(function () {
            if (self.RecordStatus() === "I" ||
                            self.WorkflowTaskCode() == "WFCA" ||
                            self.WorkflowTaskCode() == "WFRE" ||
                            self.WorkflowTaskCode() == "WFCC" ||
                            self.WorkflowTaskCode() == "WSSA" ||
                            self.WorkflowTaskCode() == "WSRE"
                            ) {
                return false;
            } else if (self.RecordStatus() !== "I" && self.WorkflowTaskCode() == "WFCO") {
                return false;
            }else if (self.RecordStatus() !== "I" && self.WorkflowTaskCode() == "NEW") {
                return true;
            }
            else if (self.RecordStatus() !== "I" && self.WorkflowTaskCode() == "UPDT" ) {
                return true;
            }
            else if (self.RecordStatus() !== "I" && self.WorkflowTaskCode() == "WFSA") {
                return true;
            }
            
        }, this);

        //    if (self.RecordStatus() === "I")
        //        return false;
        //    else {
        //        if (self.RecordStatus === "I" ||                   
        //            self.WorkflowTaskCode() == "WFCA" ||
        //            self.WorkflowTaskCode() == "WFRE" ||
        //            self.WorkflowTaskCode() == "WFCC" ||
        //            self.WorkflowTaskCode() == "WSSA" ||
        //            self.WorkflowTaskCode() == "WSRE"
        //            ) {
        //            return false;
        //        }else
        //            return false;
        //        if (self.WorkflowTaskCode() == "WFCO")
        //        {
        //            return false;
        //        }

        //    }
        //}, this);

        self.isConfirmCancelVisible = ko.observable();
        self.isConfirmCancelVisible = ko.computed(function () {

            if (self.RecordStatus() === "I")
                return false;
            else {
                if (self.WorkflowTaskCode() == "WFCO" || self.WorkflowTaskCode() == 'WSRE') {
                    return true;
               } else {
                    return false;
                }
            }
       }, this);


        //Added by srinivas end


        //self.SuppDryDockDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.SuppDryDockDocuments, function (item) {
        //    return new SuppDryDockDocument(item);
        //}) : []);
        self.SuppDryDockDocuments = ko.observableArray();

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        ////////////////KendoUI Grid Sorting/////////
        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });


        // -- Changed by sandeep on 29-01-2015
        //self.ApplicationDtAndTimeSort;
        //self.FromDate.subscribe(function (value) {
        //    self.ApplicationDtAndTimeSort = moment(value).format('YYYY-MM-DD');
        //});

        self.ApplicationDtAndTimeSort;
        self.ModifiedDate.subscribe(function (value) {
            self.ApplicationDtAndTimeSort = moment(value).format('YYYY-MM-DD HH:mm');
        });
        // -- end

        //self.StatusSort;
        //self.WFStatus.subscribe(function (value) {
        //    self.StatusSort = value;
        //});


        self.WorkflowInstanceTaskNameSort;
        self.WorkflowInstanceTaskName.subscribe(function (value) {
            self.WorkflowInstanceTaskNameSort = value;
        });

        self.EditPending = ko.computed(function () {
           
            if (self.WFStatus() == "New Request") {
                return true;
            }
            if (self.WFStatus() == "Update Request") {
                return true;
            }           
            else {
                return false;
            }
        });
        ///////////////////////////////////////////////////

        self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);
    }

    var SuppDryDockDocument = function (data) {

        var self = this;

        self.SuppDryDockDocumentID = ko.observable(data ? data.SuppDryDockDocumentID : 0);
        self.SuppDryDockID = ko.observable(data ? data.SuppDryDockID : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.FileName = ko.observable(data ? data.FileName : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "A");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");

        self.DocumentName = ko.observable(data ? data.CategoryName : '');
        self.DocumentType = ko.observable(data ? data.CategoryCode : '');
        self.DocumentPath = ko.observable(data ? data.FileName : '');
        self.CategoryName = ko.observable(data ? data.CategoryName : '');
        self.CategoryCode = ko.observable(data ? data.CategoryCode : '');
        self.DocumentTypeName = ko.observable(data ? data.DocumentTypeName : '');
    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Please enter remarks.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");


    }

    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.agentDetail = agentDetail;
    ipmsRoot.vesselModel = vesselModel;
    ipmsRoot.vesselDetail = vesselDetail;
    ipmsRoot.SuppDryDockDocument = SuppDryDockDocument;
    ipmsRoot.SuppDryDockModel = SuppDryDockModel;

}(window.IPMSROOT));

IPMSROOT.SuppDryDockModel.prototype.set = function (data) {
    var self = this;

    self.SuppDryDockID(data ? data.SuppDryDockID : 0);
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    self.AnyDangerousGoods(data ? data.AnyDangerousGoods : "I");
    self.VesselData(data ? new IPMSROOT.vesselDetail(data.ArrivalNotification) : "");
    self.VesselAgent(data ? data.VesselAgent : "");
    self.AgentData(data ? new IPMSROOT.agentDetail(data.Agent) : "");
    self.DockPortCode(data ? data.DockPortCode : "");
    self.FromDate(data ? data.FromDate : "");
    self.ToDate(data ? data.ToDate : "");
    self.BarkeelCode(data ? data.BarkeelCode : "N");
    self.CargoTons(data ? data.CargoTons : null);
    self.Ballast(data ? data.Ballast : null);
    self.Bunkers(data ? data.Bunkers : null);
    self.ExtensionDateTime(data ? data.ExtensionDateTime : null);
    self.Remarks(data ? data.Remarks : "");
    self.RecordStatus(data ? data.RecordStatus : "A");
    self.TermsandConditions(data ? data.TermsandConditions : false);
    self.WorkflowInstanceID(data ? data.WorkflowInstanceID : null);
    self.WFStatus(data ? data.WFStatus : "");
    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ApplicationDateTime(data ? data.ApplicationDateTime : "");
    self.IsConfirmCancel(data ? data.IsConfirmCancel : "N");
    self.WorkflowInstanceTaskName(data ? data.WorkflowInstanceTaskName == "Confirmation Cancel" ? "Cancel Confirmation Pending" : data.WorkflowInstanceTaskName == "Cancel Request Approve" ? "Cancellation Approved" : data.WorkflowInstanceTaskName || null : null);
    self.DockQuayCode(data ? data.DockQuayCode : null);
    self.DockBerthCode(data ? data.DockBerthCode : null);
    self.ScheduleFromDate(data ? data.ScheduleFromDate : null);
    self.ScheduleToDate(data ? data.ScheduleToDate : null);
    self.ScheduleStatus(data ? data.ScheduleStatus : null);

   // self.RegisteredName(data ? new IPMS.agentDetail(data.RegisteredName) : "");


    //self.SuppDryDockDocuments(data ? data.SuppDryDockDocuments : []);

    self.Chamber(data ? data.Chamber : "");
    self.EnteredDockDateTime(data ? data.EnteredDockDateTime : null);
    self.OnBlocksDateTime(data ? data.OnBlocksDateTime : null);
    self.DryDockDateTime(data ? data.DryDockDateTime : null);
    self.FinishedDockDateTime(data ? data.FinishedDockDateTime : null);
    self.OffBlocksDateTime(data ? data.OffBlocksDateTime : null);
    self.LeftDockDateTime(data ? data.LeftDockDateTime : null);


    self.LeftDockDateTime1(data ? data.LeftDockDateTime1 : null);

    self.CurrentBerth(data ? data.CurrentBerth : null);
    self.SuppDryDockDocuments(data ? ko.utils.arrayMap(data.SuppDryDockDocuments, function (item) {
        return new IPMSROOT.SuppDryDockDocument(item);
    }) : []);

    self.ModifiedDate(data ? data.ModifiedDate : "");

    self.SuppWFStatus(data ? data.SuppWFStatus : "");

    



    self.cache.latestData = data;
}

IPMSROOT.SuppDryDockModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}