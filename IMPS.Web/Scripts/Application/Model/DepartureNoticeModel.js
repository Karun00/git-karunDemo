(function (ipmsRoot) {
    var DepartureNoticeModel = function (data) {
        var self = this;
        self.isValidationEnabled = ko.observable(true);

        self.WFStatus = ko.observable();
        self.SubmissionDate = ko.observable();
        self.DateTimeFormatConfigValue = ko.observable();
        self.DepartureID = ko.observable();
        self.Tidal = ko.observable();
        self.DaylightRestriction = ko.observable();
        self.NoMainEngine = ko.observable();
        self.WillSheBeUnderTow = ko.observable();
        self.TowingDetails = ko.observable();
        self.CurrentBerth = ko.observable();
        self.SideAlongSideName = ko.observable();
        self.SideAlongSideCode = ko.observable();
        self.IsVesselDoubleBank = ko.observable();
        self.EstimatedDatetimeOfSR = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.EstimatedDatetimeOfSRConverted = ko.observable("").extend({ required: { message: '* This field is required.' } });;
        self.WorkflowInstanceId = ko.observable();
        self.WorkflowInstanceTaskName = ko.observable();
        self.CurrentBerthCode = ko.observable();
        self.WorkFlowRemarks = ko.observable();
        self.AgentName = ko.observable();
        self.AgentMobileNo = ko.observable();
        self.AgentFaxNo = ko.observable();
        self.AgentRepName = ko.observable();
        self.AgentTelephoneNo = ko.observable();
        self.AgentEmailID = ko.observable();
        self.VCN = ko.observable();
        self.VesselID = ko.observable();
        self.VesselName = ko.observable();
        self.VoyageIn = ko.observable();
        self.VoyageOut = ko.observable();
        self.VesselType = ko.observable();
        self.CallSign = ko.observable();
        self.IMONo = ko.observable();
        self.LengthOverallInM = ko.observable();
        self.BeamInM = ko.observable();
        self.VesselNationality = ko.observable();
        self.ArrDraft = ko.observable();
        self.DepDraft = ko.observable();
        self.NextPortOfCall = ko.observable();
        self.GrossRegisteredTonnageInMT = ko.observable();
        self.DeadWeightTonnageInMT = ko.observable();
        self.RecordStatus = ko.observable();
        self.AgentID = ko.observable();
        self.PortCode = ko.observable();
        self.CurrentWorkStatus = ko.observable();
        self.workflowRemarks = ko.observable();

        self.CreatedBy = ko.observable()
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.ATB = ko.observable();

        self.VCNSearch = ko.observable();
        self.VesselNameSearch = ko.observable('');
         self.VCNSelected = ko.observable('');
        self.VesselNameSelected = ko.observable('');

        self.SubmissionDateFrom = ko.observable();
        self.SubmissionDateTO = ko.observable();

        var todaydate = new Date();
        var todate = new Date(todaydate);
        var fromdate = new Date(todaydate);
        todate.setDate(todaydate.getDate() + 30);
        fromdate.setDate(fromdate.getDate() - 30);
        self.SubmissionDateFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
        self.SubmissionDateTO(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


        self.IsFinal = ko.observable(data ? data.IsFinal : "");
        self.IsBanked = ko.observable(data ? data.IsBanked : "");

        self.WorkFlowRemarks = ko.observable();
        self.isAcknowledgeVisible = ko.observable();
        self.isAcknowledgeVisible = ko.computed(function () {
            if (self.WFStatus() == "Invalid Code") {
                return true;
            }
            else {
                return false;
            }
        }, this);

        self.isViewVisible = ko.observable();
        self.isViewVisible = ko.computed(function () {
            if (self.WFStatus() != null && self.WFStatus() != "Invalid Code") {
                return true;
            }
            else {
                return false;
            }
        }, this);

        self.isEditVisible = ko.observable();
        self.isEditVisible = ko.computed(function () {
            if (self.IsFinal() == "" || self.IsFinal() == null || self.IsFinal() == 'Y') {
                return false;
            }
            else if (self.IsFinal() == 'N' || self.IsFinal() == 'NF') {
                return true;
            }
        }, this);

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.VesselTypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSort = value;
        });

        self.SubmissionDateSort;
        self.SubmissionDate.subscribe(function (value) {
            self.SubmissionDateSort = kendo.parseDate(value);
        });

        self.EstimatedDatetimeOfSRSort;
        self.EstimatedDatetimeOfSR.subscribe(function (value) {
            self.EstimatedDatetimeOfSRSort = kendo.parseDate(value);
        });

        

        self.WFStatust = ko.computed(function () {
            return self.WFStatus() == "Invalid Code" ? "Not Submitted" : self.WFStatus();
        });
        self.WFStatusSort;
        self.WFStatust.subscribe(function (value) {
            self.WFStatusSort = value;
        });

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
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '*Please enter remarks.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    ipmsRoot.pendingTask = pendingTask;

    ipmsRoot.DepartureNoticeModel = DepartureNoticeModel;
}(window.IPMSROOT));

IPMSROOT.DepartureNoticeModel.prototype.set = function (data) {
    var self = this;

    self.WFStatus(data ? data.WFStatus || "" : "");
    self.DateTimeFormatConfigValue(data ? data.DateTimeFormatConfigValue || "" : "");
    self.SubmissionDate(data ? (data.SubmissionDate != null ? moment(data.SubmissionDate).format('YYYY-MM-DD HH:mm') : "") : "");
    self.DepartureID(data ? data.DepartureID || "" : "");
    self.Tidal(data ? data.Tidal : "I");
    self.DaylightRestriction(data ? data.DaylightRestriction : "I");
    self.NoMainEngine(data ? (data.NoMainEngine != null ? data.NoMainEngine : "Y") : "Y");
    self.WillSheBeUnderTow(data ? (data.WillSheBeUnderTow != null ? data.WillSheBeUnderTow : "Y") : "Y");
    self.TowingDetails(data ? data.TowingDetails || "" : "");
    self.CurrentBerth(data ? data.CurrentBerth || "" : "");
    self.SideAlongSideName(data ? data.SideAlongSideName || "" : "");
    self.SideAlongSideCode(data ? data.SideAlongSideCode || "" : "");
    self.IsVesselDoubleBank(data ? (data.IsBanked == 0 ? "N" : "Y") : "N");    
    self.EstimatedDatetimeOfSR(data ? moment(data.EstimatedDatetimeOfSR).format('YYYY-MM-DD HH:mm') || "" : "");
    self.EstimatedDatetimeOfSRConverted(data ? data.EstimatedDatetimeOfSRConverted || "" : "");
    self.WorkflowInstanceId(data ? data.WorkflowInstanceId || "" : "");
    self.CreatedBy(data ? data.CreatedBy || "" : "");
    self.ModifiedBy(data ? data.ModifiedBy || "" : "");
    self.CreatedDate(data ? data.CreatedDate || "" : "");
    self.ModifiedDate(data ? data.ModifiedDate || "" : "");
    self.WorkflowInstanceTaskName(data ? data.WorkflowInstanceTaskName || "" : "");
    self.CurrentBerthCode(data ? data.CurrentBerthCode || "" : "");
    self.WorkFlowRemarks(data ? data.WorkFlowRemarks || "" : "");
    self.AgentName(data ? data.AgentName || "" : "");
    self.AgentMobileNo(data ? data.AgentMobileNo || "" : "");
    self.AgentFaxNo(data ? data.AgentFaxNo || "" : "");
    self.AgentRepName(data ? data.AgentRepName || "" : "");
    self.AgentTelephoneNo(data ? data.AgentTelephoneNo || "" : "");
    self.AgentEmailID(data ? data.AgentEmailID || "" : "");
    self.VCN(data ? data.VCN || "" : "");
    self.VesselID(data ? data.VesselID || "" : "");
    self.VesselName(data ? data.VesselName || "" : "");
    self.VoyageIn(data ? data.VoyageIn || "" : "");
    self.VoyageOut(data ? data.VoyageOut || "" : "");
    self.VesselType(data ? data.VesselType || "" : "");
    self.CallSign(data ? data.CallSign || "" : "");
    self.IMONo(data ? data.IMONo || "" : "");
    self.LengthOverallInM(data ? data.LengthOverallInM || "" : "");
    self.BeamInM(data ? data.BeamInM || "" : "");
    self.VesselNationality(data ? data.VesselNationality || "" : "");
    self.ArrDraft(data ? data.ArrDraft || "" : "");
    self.DepDraft(data ? data.DepDraft || "" : "");
    self.NextPortOfCall(data ? data.NextPortOfCall || "" : "");
    self.GrossRegisteredTonnageInMT(data ? data.GrossRegisteredTonnageInMT || "" : "");
    self.DeadWeightTonnageInMT(data ? data.DeadWeightTonnageInMT || "" : "");
    self.RecordStatus(data ? data.RecordStatus || "" : "");
    self.AgentID(data ? data.AgentID || "" : "");
    self.PortCode(data ? data.PortCode || "" : "");
    self.CurrentWorkStatus(data ? data.CurrentWorkStatus || "" : "");
    self.IsFinal(data ? data.IsFinal || "" : "");
    self.VCNSearch(data ? data.VCNSearch || "" : "");
    self.VesselNameSearch(data ? data.VesselNameSearch || "" : "");
    self.VCNSelected(data ? data.VCNSelected || "" : "");
    self.VesselNameSelected(data ? data.VesselNameSelected || "" : "");
    self.ATB(data ? data.ATB || "" : "");
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.DepartureNoticeModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-5]/;

    return charcheck.test(keychar);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}