(function (ipmsRoot) {


    var BerthModel = function (data) {
        var self = this;
        debugger;
       // self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.Lengthm = ko.observable(data ? data.Lengthm : "");

    }

    var DryDockSchedulerModel = function (data) {

        var self = this;

        self.SuppDryDockID = ko.observable();
        self.validationEnabled = ko.observable(false);
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselData = ko.observable();
        self.AgentData = ko.observable();
        self.IsValidationEnabled = ko.observable(false);
        self.FromDate = ko.observable();
        self.ToDate = ko.observable();
        self.BarkeelCode = ko.observable();
        self.CargoTons = ko.observable();
        self.Ballast = ko.observable();
        self.Bunkers = ko.observable();
        self.ExtensionDateTime = ko.observable();
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable();
        self.TermsandConditions = ko.observable();
        self.WorkflowInstanceID = ko.observable();
        self.WFStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.VesselAgent = ko.observable();
        self.DockPortCode = ko.observable();
        self.DockQuayCode = ko.observable();
        self.DockBerthCode = ko.observable('');
        self.ScheduleFromDate = ko.observable();
        self.ScheduleToDate = ko.observable();
        self.ScheduleStatus = ko.observable();
        self.EnteredDockDateTime = ko.observable();
        self.FinishedDockDateTime = ko.observable();
        self.LengthOverallInM = ko.observable();
        self.ArrDraft = ko.observable();
        self.DryDocks = ko.observableArray();

        self.PortCode = ko.observable();
        self.QuayCode = ko.observable();
        self.BerthCode = ko.observable();
        self.Lengthm = ko.observable();

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.LengthOverallInMSort;
        self.LengthOverallInM.subscribe(function (value) {
            self.LengthOverallInMSort = value.toString();
        });

        self.ArrDraftSort;
        self.ArrDraft.subscribe(function (value) {
            self.ArrDraftSort = value.toString();
        });


        self.FromDateSort;
        self.FromDate.subscribe(function (value) {
            self.FromDateSort = value;
        });

        self.ToDateSort;
        self.ToDate.subscribe(function (value) {
            self.ToDateSort = value;
        });


        self.cache = function () { };
        self.set(data);
    }

    var ScheduleModel = function (data) {
        var self = this;
        self.id = ko.observable(data ? data.SuppDryDockID : "");
        self.start = ko.observable(data ? data.ScheduleFromDate : "");
        self.end = ko.observable(data ? data.ScheduleToDate : "");
        self.title = ko.observable(data ? data.VCN : "");
        self.ScheduleStatus = ko.observable(data ? data.ScheduleStatus : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.ScheduleFromDate = ko.observable(data ? (moment(data.ScheduleFromDate).format('YYYY-MM-DD HH:mm') || "") : "");
        self.ScheduleToDate = ko.observable(data ? (moment(data.ScheduleToDate).format('YYYY-MM-DD HH:mm') || "") : "");        
        self.DockBerthCode = ko.observable(data ? data.DockBerthCode : "");
        self.ReqFromDate = ko.observable(data ? data.FromDate : "");
        self.ReqToDate = ko.observable(data ? data.ToDate : "");
        self.IMONo = ko.observable(data ? (data.ArrivalNotification ? (data.ArrivalNotification.Vessel ? data.ArrivalNotification.Vessel.IMONo : "") : "") : "");
        self.AgentName = ko.observable(data ? data.VesselAgent : "");

        self.EnteredDockDateTime = ko.observable(data ? (data.EnteredDockDateTime != null ? moment(data.EnteredDockDateTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.LeftDockDateTime = ko.observable(data ? (data.LeftDockDateTime != null ? moment(data.LeftDockDateTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.ExtensionDateTime = ko.observable(data ? (data.ExtensionDateTime != null ? moment(data.ExtensionDateTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.LengthOverallInM = ko.observable(data ? data.LengthOverallInM : "");


        //self.EnteredDockDateTime = ko.observable(data ? (moment(data.EnteredDockDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
        //self.LeftDockDateTime = ko.observable(data ? (moment(data.LeftDockDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
        var txt = '';
       // var txt = "<table  style='border:0px solid #f00; line-height:25px; ' width='100%'>
       //     <tr ><td  style='text-align:right'><b>VCN</b></td><td width=5px;>:</td><td style='text-align:left'>" + self.title() + " </td></tr><tr><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td></tr><tr><td  style='text-align:right'><b>IMO No.</b></td><td>:</td><td style='text-align:left'>" + self.IMONo() + " </td></tr><tr><td  style='text-align:right'><b>Requested From Date</b></td><td>:</td><td style='text-align:left'>" + moment(self.ReqFromDate()).format('YYYY-MM-DD HH:mm') + " </td></tr><tr><td  style='text-align:right'><b>Requested To Date</b></td><td>:</td><td style='text-align:left'>" + moment(self.ReqToDate()).format('YYYY-MM-DD HH:mm') + " </td></tr><tr><td  style='text-align:right'><b>Schedule From Date</b></td><td>:</td><td style='text-align:left'>" + moment(self.ScheduleFromDate()).format('YYYY-MM-DD HH:mm') + " </td></tr><tr><td  style='text-align:right'><b>Schedule To Date</b></td><td>:</td><td style='text-align:left'>" + moment(self.ScheduleToDate()).format('YYYY-MM-DD HH:mm') + " </td></tr><tr><td  style='text-align:right'><b>Dock</b></td><td>:</td><td style='text-align:left'>" + self.DockBerthCode() + " </td></tr></table>";
        if (!(self.title() == null || self.title() == '' || self.title() == undefined)) {
            txt = "<table style='border:0px solid #f00; line-height:18px; font-size:11px;'><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left'>" + self.title() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right; width:80px;'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>IMO No.</b></td><td>:</td><td style='text-align:left'>" + self.IMONo() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Agent Name</b></td><td>:</td><td style='text-align:left'>" + self.AgentName() + " </td></tr><tr><td colspan='3' align='center'><b>Request</b></td></tr><tr><td  style='text-align:right'><b>From Date</b></td><td>:</td><td style='text-align:left'>" + self.ReqFromDate() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>To Date</b></td><td>:</td><td style='text-align:left'>" + self.ReqToDate() + " </td></tr><tr><td colspan='3' align='center'><b>Schedule</b></td></tr><tr><td  style='text-align:right'><b>From Date</b></td><td>:</td><td style='text-align:left'>" + self.ScheduleFromDate() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>To Date</b></td><td>:</td><td style='text-align:left'>" + self.ScheduleToDate() + " </td></tr><tr><td colspan='3' align='center'><b>Dock Undock Time</b></td></tr><tr><td  style='text-align:right'><b>Entered Date</b></td><td>:</td><td style='text-align:left'>" + self.EnteredDockDateTime() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Left Date</b></td><td>:</td><td style='text-align:left'>" + self.LeftDockDateTime() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Dock</b></td><td>:</td><td style='text-align:left'>" + self.BerthName() + " </td></tr></table>";
        }
        else {
            txt = '';
        }
        self.ToolTip = ko.observable(txt);
    }

    

    ipmsRoot.DryDockSchedulerModel = DryDockSchedulerModel;
    ipmsRoot.ScheduleModel = ScheduleModel;
    ipmsRoot.BerthModel = BerthModel;
}(window.IPMSROOT));

IPMSROOT.DryDockSchedulerModel.prototype.set = function (data) {
    var self = this;

    self.SuppDryDockID(data ? data.SuppDryDockID : 0);
    self.VCN(data ? data.VCN : null);
    self.VesselName(data ? data.VesselName : "");
    //self.VesselData(data ? new IPMSROOT.vesselDetail(data.ArrivalNotification) : "");
    //self.AgentData(data ? new IPMSROOT.agentDetail(data.Agent) : "");
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

    self.DockPortCode(data ? data.DockPortCode : "");
    self.DockQuayCode(data ? data.DockQuayCode : "");
    self.DockBerthCode(data ? data.DockBerthCode : "");
    self.ScheduleFromDate(data ? data.ScheduleFromDate : "");
    self.ScheduleToDate(data ? data.ScheduleToDate : "");
    self.ScheduleStatus(data ? data.ScheduleStatus : "");
    self.EnteredDockDateTime(data ? data.EnteredDockDateTime : "");
    self.FinishedDockDateTime(data ? data.FinishedDockDateTime : "");
    self.LengthOverallInM(data ? data.LengthOverallInM : "");
    self.ArrDraft(data ? data.ArrDraft : "");
    
    self.cache.latestData = data;
}

IPMSROOT.DryDockSchedulerModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}