(function (ipmsRoot) {

    var VesselAgentChangeReqReferenceData = function (data) {
        var self = this;
        self.PorposedAgents = ko.observableArray(data ? $.map(data.getPraposedAgents, function (item) { return new PorposedAgent(item); }) : []);
        self.ReasonForTransfers = ko.observableArray(data ? $.map(data.getResonForTransfer, function (item) { return new ReasonForTransfer(item); }) : []);        
        self.DocumentType = ko.observableArray(data ? $.map(data.getDocumentTypes, function (item) { return new DocumentTypes(item); }) : []);
    }

    var PorposedAgent = function (data) {
        var self = this;
        self.AgentName = ko.observable(data ? data.RegisteredName : "");
        self.AgeenID = ko.observable(data ? data.AgentID : "");
    }
    var ReasonForTransfer = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var DocumentTypes = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }    

    var VesselChangeAgentDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.FileName = ko.observable(data ? data.FileName : '');

    }

    var AgentChangeRequestModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.VCN = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select the VCN' } });;
        self.VesselAgentChangeID = ko.observable();
        self.VesselName = ko.observable();
        self.VesselType = ko.observable();
        self.VesselTypeName = ko.observable();
        //self.ProposedAgent = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select the Proposed Agent' } });
        self.ProposedAgent = ko.observable("");
        self.ProposedAgentName = ko.observable("")
        //  self.ReasonForTransferCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select the Reason For Transfer' } });
        self.ReasonForTransferCode = ko.observable("")
        self.ReasonForVisitName = ko.observable();

        self.RecordStatus = ko.observable('A');
        self.Status = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.WorkFlowStatus = ko.observable();
        self.EffectiveDateTime = ko.observable("").extend({ required: { message: '* Please Select Effective Date/Time' } });
        self.VesselData = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.UploadedFiles = ko.observableArray([]);
        self.DocumentCategoryList = ko.observableArray();
        self.UploadedFiles = ko.observableArray([]);
        self.WorkflowInstanceId = ko.observable();
        self.AnyDangerousGoodsonBoard = ko.observable();
        self.ETA = ko.observable();

        self.ETAFrom = ko.observable();
        self.ETATo = ko.observable();

        var todaydate = new Date();
        var todate = new Date(todaydate);
        var fromdate = new Date(todaydate);
        todate.setDate(todaydate.getDate() + 30);
        fromdate.setDate(fromdate.getDate() - 30);
        self.ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
        self.ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


        self.VCNSearch = ko.observable();
        self.VesselNameSearch = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.VesseltypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesseltypeSort = value;
        });

        self.ReasonForVisitSort;
        self.ReasonForVisitName.subscribe(function (value) {
            self.ReasonForVisitSort = value;
        });


        self.ProposedAgentSort;
        self.ProposedAgentName.subscribe(function (value) {
            self.ProposedAgentSort = value;
        });

        self.ETASortGrid;
        self.ETA.subscribe(function (value) {
            self.ETASortGrid = value;
        });

        self.VesselAgentChangeDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.VesselAgentChangeDocuments, function (document) {
            return new VesselChangeAgentDocument(document);
        }) : []);

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.cache = function () { };
        self.set(data);
    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    IPMSROOT.AgentChangeRequestModel = AgentChangeRequestModel;
    IPMSROOT.VesselAgentChangeReqReferenceData = VesselAgentChangeReqReferenceData;    
    ipmsRoot.VesselChangeAgentDocument = VesselChangeAgentDocument;
    ipmsRoot.pendingTask = pendingTask;

}(window.IPMSROOT));



IPMSROOT.AgentChangeRequestModel.prototype.set = function (data) {
    var self = this;    
    self.VCN(data ? (data.VCN || "") : "");
    self.VesselAgentChangeID(data ? (data.VesselAgentChangeID || "") : "");
    self.VesselName(data ? (data.ArrivalNotification.Vessel.VesselName == 'NULL' ? "" : data.ArrivalNotification.Vessel.VesselName || "") : "");

    self.VesselType(data ? (data.ArrivalNotification.VesselType == 'NULL' ? "" : data.ArrivalNotification.VesselType || "") : "");
    self.ProposedAgent(data ? (data.ProposedAgent == 'NULL' ? "" : data.ProposedAgent || "") : "");
    self.ProposedAgentName(data ? (data.Agent.RegisteredName == 'NULL' ? "" : data.Agent.RegisteredName || "") : "");
    self.ReasonForTransferCode(data ? (data.ReasonForTransferCode == 'NULL' ? "" : data.ReasonForTransferCode || "") : "");
    self.ReasonForVisitName(data ? (data.ArrivalNotification.ReasonforvisitName == 'NULL' ? "" : data.ArrivalNotification.ReasonforvisitName || "") : "");
    self.VesselTypeName(data ? (data.ArrivalNotification.VesselType == 'NULL' ? "" : data.ArrivalNotification.VesselType || "") : "");

    self.RecordStatus(data ? data.RecordStatus : "");

    self.EffectiveDateTime(data ? (moment(data.EffectiveDateTime).format('YYYY-MM-DD HH:mm') == 'NULL' ? "" : moment(data.EffectiveDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.WorkFlowStatus(data ? (data.WorkFlowStatus == 'NULL' ? "" : data.WorkFlowStatus || "") : "");
    self.WorkflowInstanceId(data ? data.WorkflowInstanceId : "");
    self.AnyDangerousGoodsonBoard(data ? data.ArrivalNotification.AnyDangerousGoodsonBoard : "N")
    self.ETA(data ? (moment(data.ArrivalNotification.ETA).format('YYYY-MM-DD') == 'NULL' ? "" : moment(data.ArrivalNotification.ETA).format('YYYY-MM-DD') || "") : "");
    self.cache.latestData = data;
}


IPMSROOT.AgentChangeRequestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}


