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
        self.SuppDryDockExtensionID = ko.observable();
        self.validationEnabled = ko.observable(false);
        self.VCN = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* VCN is required' } });
        self.VesselName = ko.observable();
        self.VesselData = ko.observable();
        self.AgentData = ko.observable();
        self.IsValidationEnabled = ko.observable(false);
        self.DockPortCode = ko.observable();
        self.FromDate = ko.observable("");
        self.ToDate = ko.observable("");
        self.BarkeelCode = ko.observable();
        self.CargoTons = ko.observable();
        self.Ballast = ko.observable();
        self.Bunkers = ko.observable();
        self.ExtensionDateTime = ko.observable("").extend({ required: { message: '* Extension Date Time is required' } })
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable();
        self.TermsandConditions = ko.observable();
        self.WorkflowInstanceID = ko.observable();
        self.WFStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();

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
        self.ScheduleStatusText = ko.observable();
        self.SuppDryDockDocuments = ko.observableArray();
        self.CurrentBerth = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        //self.SuppDryDockDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.SuppDryDockDocuments, function (item) {
        //    return new SuppDryDockDocument(item);
        //}) : []);

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

        self.ApplicationDtAndTimeSort;
        self.FromDate.subscribe(function (value) {
            self.ApplicationDtAndTimeSort = value;
        });


        self.ExtensionDtAndTimeSort;
        self.ExtensionDateTime.subscribe(function (value) {
            self.ExtensionDtAndTimeSort = value;
        });

        self.StatusSort;
        self.ScheduleStatusText.subscribe(function (value) {
            self.StatusSort = value;
        });

        ///////////////////////////////////////////////////

        self.UploadedFiles = ko.observableArray([]);

        //self.FromDate = ko.observable();
        //self.ToDate = ko.observable();
        self.CargoTons = ko.observable();
        self.Ballast = ko.observable();
        self.Bunkers = ko.observable();
        //self.ScheduleStatus = ko.observable();
        
        


        //self.IMONo = ko.observable(data ? (data.Vessel ? data.Vessel.IMONo : data.IMONo) : "");
        //self.GrossRegisteredTonnageInMT = ko.observable(data ? (data.Vessel ? data.Vessel.GrossRegisteredTonnageInMT : data.GrossRegisteredTonnageInMT) : "");
        //self.LengthOverallInM = ko.observable(data ? (data.Vessel ? data.Vessel.LengthOverallInM : data.LengthOverallInM) : "");
        //self.BeamInM = ko.observable(data ? (data.Vessel ? data.Vessel.BeamInM : data.BeamInM) : "");
        //self.ArrDraft = ko.observable(data ? data.ArrDraft : "");


        //self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
        //self.TradingName = ko.observable(data ? data.TradingName : "");
        //self.TelephoneNo1 = ko.observable(data ? data.TelephoneNo1 : "");
        //self.FaxNo = ko.observable(data ? data.FaxNo : "");
        //self.TelephoneNo2 = ko.observable(data ? data.TelephoneNo2 : "");

        self.RegisteredName = ko.observable();
        self.TradingName = ko.observable();
        self.TelephoneNo1 = ko.observable();
        self.FaxNo = ko.observable();
        self.TelephoneNo2 = ko.observable();

        self.IMONo = ko.observable();
        self.GrossRegisteredTonnageInMT = ko.observable();
        self.LengthOverallInM = ko.observable();
        self.BeamInM = ko.observable();
        self.ArrDraft = ko.observable();

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
    self.SuppDryDockExtensionID(data ? data.SuppDryDockExtensionID : 0);
    self.VCN(data ? data.VCN : null);
    self.VesselName(data ? data.VesselName : "");


    self.DockPortCode(data ? data.DockPortCode : "");
    self.FromDate(data ? data.FromDate : "");
    self.ToDate(data ? data.ToDate : "");
    self.BarkeelCode(data ? data.BarkeelCode : "N");
    self.CargoTons(data ? data.CargoTons : null);
    self.Ballast(data ? data.Ballast : null);
    self.Bunkers(data ? data.Bunkers : null);
    // self.ExtensionDateTime(data ? data.ExtensionDateTime : "");
    self.ExtensionDateTime(data ? ((data.ExtensionDateTime != null && data.ExtensionDateTime != "") ? moment(data.ExtensionDateTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.Remarks(data ? data.Remarks : "");
    self.RecordStatus(data ? data.RecordStatus : "A");
    self.TermsandConditions(data ? data.TermsandConditions : false);
    self.WorkflowInstanceID(data ? data.WorkflowInstanceID : null);
    self.WFStatus(data ? data.WFStatus : "");
    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ApplicationDateTime(data ? data.ApplicationDateTime : "");

    self.DockQuayCode(data ? data.DockQuayCode : null);
    self.DockBerthCode(data ? data.DockBerthCode : null);
    self.DockBerthCode(data ? data.DockBerthCode : null);

    //self.ScheduleFromDate(data ? data.ScheduleFromDate : null);
    self.ScheduleFromDate(data ? (data.ScheduleFromDate != null ? moment(data.ScheduleFromDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.CurrentBerth(data ? data.CurrentBerth : "");
    //self.ScheduleToDate(data ? data.ScheduleToDate : null);
    self.ScheduleToDate(data ? (data.ScheduleToDate != null ? moment(data.ScheduleToDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");


    self.ScheduleStatus(data ? data.ScheduleStatus : null);
    self.ScheduleStatusText(data ? data.ScheduleStatusText : null);

    // self.RegisteredName(data ? new IPMS.agentDetail(data.RegisteredName) : "");


    //self.SuppDryDockDocuments(data ? data.SuppDryDockDocuments : []);

    self.Chamber(data ? data.Chamber : "");
    self.EnteredDockDateTime(data ? data.EnteredDockDateTime : null);
    self.OnBlocksDateTime(data ? data.OnBlocksDateTime : null);
    self.DryDockDateTime(data ? data.DryDockDateTime : null);
    self.FinishedDockDateTime(data ? data.FinishedDockDateTime : null);
    self.OffBlocksDateTime(data ? data.OffBlocksDateTime : null);
    self.LeftDockDateTime(data ? data.LeftDockDateTime : null);

    self.FromDate(data ? data.FromDate : null);
    self.ToDate(data ? data.ToDate : null);
    self.CargoTons(data ? data.CargoTons : null);
    self.Ballast(data ? data.Ballast : null);
    self.Bunkers(data ? data.Bunkers : null);
    //self.ScheduleStatus(data ? data.ScheduleStatus : null);



    self.IMONo(data ? data.IMONo : null);
    self.GrossRegisteredTonnageInMT(data ? data.GrossRegisteredTonnageInMT : null);
    self.LengthOverallInM(data ? data.LengthOverallInM : null);
    self.BeamInM(data ? data.BeamInM : null);
    self.ArrDraft(data ? data.ArrDraft : null);

    self.RegisteredName(data ? data.RegisteredName : null);
    self.TradingName(data ? data.TradingName : null);
    self.TelephoneNo1(data ? data.TelephoneNo1 : null);
    self.FaxNo(data ? data.FaxNo : null);
    self.TelephoneNo2(data ? data.TelephoneNo2 : null);


    self.SuppDryDockDocuments(data ? ko.utils.arrayMap(data.SuppDryDockDocuments, function (item) {
        return new IPMSROOT.SuppDryDockDocument(item);
    }) : []);

    self.AgentData(data ? new IPMSROOT.agentDetail(data.Agent) : "");
    self.VesselData(data ? new IPMSROOT.vesselDetail(data.ArrivalNotification) : "");


    self.cache.latestData = data;
}

IPMSROOT.SuppDryDockModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}