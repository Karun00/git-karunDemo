(function (ipmsRoot) {

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    var ChangeETAModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.Date = ko.observable();
        self.VesselAgent = ko.observable();
        self.ReportingTo = ko.observable();
        self.AgentName = ko.observable();
        self.LOA = ko.observable();
        self.GRT = ko.observable();
        self.Draft = ko.observable();
        self.ETA = ko.observable();
        self.ETD = ko.observable();
        self.NewETA = ko.observable();
        self.NewETD = ko.observable();
        self.VoyageIn = ko.observable("");
        self.VoyageOut = ko.observable("");
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.CreatedDateAndTime = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.StartDate = ko.observable();
        self.NoofTimesETAChanged = ko.observable();
        self.VesselCallMovementID = ko.observable();
        self.AnyDangerousGoodsonBoard = ko.observable();
        self.OldETA = ko.observable();
        self.OldETD = ko.observable();
        self.ATB = ko.observable();
        self.ATUB = ko.observable();
        self.ATA = ko.observable();
        self.ATD = ko.observable();

        self.ArrivalReasonforVisit = ko.observableArray([]);
        self.ReasonForVisit = ko.observable();
        self.PlanDateTimeOfBerth = ko.observable("").extend({ required: true });
        self.PlanDateTimeToVacateBerth = ko.observable("").extend({ required: true });
        self.PlanDateTimeToStartCargo = ko.observable("").extend({ required: true });
        self.PlanDateTimeToCompleteCargo = ko.observable("").extend({ required: true });
        self.OldPlanDateTimeOfBerth = ko.observable("");
        self.OldPlanDateTimeToStartCargo = ko.observable("");
        self.OldPlanDateTimeToCompleteCargo = ko.observable("");
        self.OldPlanDateTimeToVacateBerth = ko.observable("");

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        //self.ETAFrom = ko.observable(new Date());
        //self.ETATo = ko.observable(new Date());

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
        self.AgentNameSearch = ko.observable();             

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.AgentSort;
        self.AgentName.subscribe(function (value) {
            self.AgentSort = value;
        });

        self.OldETASort;
        self.OldETA.subscribe(function (value) {
            self.OldETASort = kendo.parseDate(value);
        });

        self.OldETDSort;
        self.OldETD.subscribe(function (value) {
            self.OldETDSort = kendo.parseDate(value);
        });

        self.NewETASort;
        self.NewETA.subscribe(function (value) {
            self.NewETASort = kendo.parseDate(value);
        });

        self.NewETDSort;
        self.NewETD.subscribe(function (value) {
            self.NewETDSort = kendo.parseDate(value);
        });

        self.VoyageInSort;
        self.VoyageIn.subscribe(function (value) {
            self.VoyageInSort = value;
        });

        self.VoyageOutSort;
        self.VoyageOut.subscribe(function (value) {
            self.VoyageOutSort = value;
        });

        self.cache = function () { };
        self.set(data);
    };
    ipmsRoot.ChangeETAModel = ChangeETAModel;
}(window.IPMSROOT));

IPMSROOT.ChangeETAModel.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? (data.VCN || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.CreatedDateAndTime(data ? (moment(data.CreatedDateAndTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.Date(data ? (moment(data.CreatedDateAndTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.VesselAgent(data ? (data.VesselAgent || "") : "");
    self.ReportingTo(data ? (data.ReportingTo || "") : "");
    self.AgentName(data ? (data.AgentName || "") : "");
    self.LOA(data ? (data.LOA || "") : "");
    self.GRT(data ? (data.GRT || "") : "");
    self.NoofTimesETAChanged(data ? (data.NoofTimesETAChanged || "") : "");
    self.ETA(data ? (moment(data.ETA).format('YYYY-MM-DD HH:mm') || "") : "");
    self.ETD(data ? (moment(data.ETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.NewETA(data ? (moment(data.NewETA).format('YYYY-MM-DD HH:mm') || "") : "");
    self.NewETD(data ? (moment(data.NewETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.OldETA(data ? (moment(data.OldETA).format('YYYY-MM-DD HH:mm') || "") : "");
    self.OldETD(data ? (moment(data.OldETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.Draft(data ? (data.Draft || "") : "");

    self.VoyageIn(data ? (data.VoyageIn || "") : "");
    self.VoyageOut(data ? (data.VoyageOut || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.VesselCallMovementID(data ? (data.VesselCallMovementID || "") : "");
    self.AnyDangerousGoodsonBoard(data ? data.AnyDangerousGoodsonBoard : "N");

    self.PlanDateTimeOfBerth(data ? data.PlanDateTimeOfBerth ? (moment(data.PlanDateTimeOfBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToVacateBerth(data ? data.PlanDateTimeToVacateBerth ? (moment(data.PlanDateTimeToVacateBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToStartCargo(data ? data.PlanDateTimeToStartCargo ? (moment(data.PlanDateTimeToStartCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToCompleteCargo(data ? data.PlanDateTimeToCompleteCargo ? (moment(data.PlanDateTimeToCompleteCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");

    self.OldPlanDateTimeOfBerth(data ? data.OldPlanDateTimeOfBerth ? (moment(data.OldPlanDateTimeOfBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.OldPlanDateTimeToStartCargo(data ? data.OldPlanDateTimeToStartCargo ? (moment(data.OldPlanDateTimeToStartCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.OldPlanDateTimeToCompleteCargo(data ? data.OldPlanDateTimeToCompleteCargo ? (moment(data.OldPlanDateTimeToCompleteCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.OldPlanDateTimeToVacateBerth(data ? data.OldPlanDateTimeToVacateBerth ? (moment(data.OldPlanDateTimeToVacateBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.ArrivalReasonforVisit(data ? (data.ArrivalReasonforVisit || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.ChangeETAModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

var keynum;
var keychar;
var charcheck;
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9]/;
    return charcheck.test(keychar);
}

function ValidateAlphanumericsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[A-Za-z\d\s]*$/;
    return charcheck.test(keychar);
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