(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var ReferenceData = function (data) {
        var self = this;       
        self.Agents = ko.observableArray(data ? $.map(data.Agents, function (item) { return new Agent(item); }) : []);
        self.CargoTypes = ko.observableArray(data ? $.map(data.CargoType, function (item) { return new CargoType(item); }) : []);
        self.VesselTypes = ko.observableArray(data ? $.map(data.VesselType, function (item) { return new VesselType(item); }) : []);
        self.ReasonsforVisit = ko.observableArray(data ? $.map(data.ReasonForVisit, function (item) { return new ReasonforVisit(item); }) : []);
        self.MovementsStatus = ko.observableArray(data ? $.map(data.MovementStatus, function (item) { return new MovementStatus(item); }) : []);
        self.selectedAgentID = ko.observable(data ? data.selectedAgentID : "");
        self.ETA = ko.observable(data ? data.ETA : "");
        self.ETD = ko.observable(data ? data.ETD : "");
        self.selectedVesselType = ko.observable(data ? data.selectedVesselType : "");
        self.selectedReasonforVisit = ko.observable(data ? data.selectedReasonforVisit : "");
        self.selectedCargoType = ko.observable(data ? data.selectedCargoType : "");
        self.selectedMovementStatus = ko.observable(data ? data.selectedMovementStatus : "");

    }
    var Agent = function (data) {
        var self = this;
        self.AgentID = ko.observable(data ? data.AgentID : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
    }
    var CargoType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var VesselType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var ReasonforVisit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var MovementStatus = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var Berth = function (data) {
        var self = this;
        self.BerthID = ko.observable(data ? data.BerthID : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.Bollards = ko.observableArray(data ? (data.Bollards ? $.map(data.Bollards, function (item) { return new Bollard(item) }) : []) : []);


    }
    var Bollard = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BollardCode = ko.observable(data ? data.BollardCode : "");
        self.BollardName = ko.observable(data ? data.BollardName : "");
        self.FromMeter = ko.observable(data ? data.FromMeter : "");
        self.ToMeter = ko.observable(data ? data.ToMeter : "");
        self.Continous = ko.observable(data ? data.Continous : "");
        self.ContinousStatus = ko.observable(data ? data.ContinousStatus : "");
    }


    var BerthPreSchedulingModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(true);        
        self.AgentID = ko.observable("");
        self.VCN = ko.observable("");
       
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);       
        self.VesselCallID = ko.observable(data ? data.VesselCallID : "");
        self.VesselCallMovementID = ko.observable(data ? data.VesselCallMovementID : "");       
        self.IsScheduleStatus = ko.observable(data ? data.IsScheduleStatus : "");
        self.BerthCode = ko.observable("");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.SheduledBerth = ko.observable("");
        self.FromBollardCode = ko.observable(data ? data.FromBollardCode : "");
        self.ToBollardCode = ko.observable(data ? data.ToBollardCode : "");      
        self.ETA = ko.observable();
        self.ETD = ko.observable();
        self.ETB = ko.observable(data ? data.ETB : "").extend({ required: true });
        self.ETUB = ko.observable(data ? data.ETUB : "").extend({ required: true });
        self.BerthTime = ko.observable();
        self.UnBerthTime = ko.observable();
        self.Agent = ko.observable();
        self.VesselName = ko.observable();
        self.VesselType = ko.observable();
        self.Berth = ko.observable();
        self.IMDG = ko.observable("");
        self.MovementType = ko.observable("");
        self.MovementTypeName=ko.observable("");
        self.LengthOverallInM = ko.observable();
        self.MaxDraft = ko.observable();
        self.NominationDate = ko.observable();
        self.PreferredBerth = ko.observable();
        self.AlternateBerth = ko.observable();
        self.ReasonForVisitName = ko.observable();
        self.ReasonForVisit = ko.observable();
        self.CurrentDate = ko.observable();
        self.ETBStartDate = ko.observable();
        self.ETUBStartDate = ko.observable();
        self.NoofTimesETAChanged = ko.observable();
        self.ReasonforAlternateBerth = ko.observable();
        self.CargoTypeName = ko.observable();
        self.MinStartDate = ko.observable();
        self.BerthTime = ko.observable();
        self.UnBerthTime = ko.observable();
        self.isDryDock = ko.observable();
        self.Bollards = ko.observableArray(data ? $.map(data.Bollards, function (item) { return new Bollard(item); }) : []);
        self.FromBollardMeter = ko.observable(data ? data.FromBollardMeter : "");
        self.Color = ko.observable();
        self.cache = function () { };
        self.set(data);
    }

    
    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.BerthPreSchedulingModel = BerthPreSchedulingModel; 
    
}(window.IPMSROOT));
//CraftModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.BerthPreSchedulingModel.prototype.set = function (data) {
    var self = this;
    self.VesselCallID(data ? (data.VesselCallID || "") : "");
    self.VesselCallMovementID(data ? (data.VesselCallMovementID == 'NULL' ? "" : data.VesselCallMovementID || "") : "");   
    self.ETA(data ? (moment(data.ETA).format('YYYY-MM-DD HH:mm') || "") : "");
    self.ETD(data ? (moment(data.ETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.Berth(data ? (data.Berth || "") : "");
    self.AgentID(data ? (data.AgentID || "") : ""); 
    self.VCN(data ? (data.VCN || "") : "");
    self.ETB(data ? (moment(data.ETB).format('YYYY-MM-DD HH:mm') || "") : "");
    self.ETUB(data ? (moment(data.ETUB).format('YYYY-MM-DD HH:mm') || "") : "");
    self.BerthTime(data ? (moment(data.BerthTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.UnBerthTime(data ? (moment(data.UnBerthTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.SheduledBerth(data ? (data.SheduledBerth || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.Agent(data ? (data.Agent || "") : "");
    self.IMDG(data ? (data.IMDG || "") : "");
    self.LengthOverallInM(data ? (data.LengthOverallInM || "") : "");
    self.MaxDraft(data ? (data.MaxDraft || "") : "");
    self.NominationDate(data ? (moment(data.NominationDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.PreferredBerth(data ? (data.PreferredBerth || "") : "");
    self.AlternateBerth(data ? (data.AlternateBerth || "") : "");
    self.ReasonForVisitName(data ? (data.ReasonForVisitName || "") : "");
    self.ReasonForVisit(data ? (data.ReasonForVisit || "") : "");
    self.CurrentDate(data ? (moment(data.CurrentDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.NoofTimesETAChanged(data ? (data.NoofTimesETAChanged || "") : "");
    self.CargoTypeName(data ? (data.CargoTypeName || "") : "");
    self.ReasonforAlternateBerth(data ? (data.ReasonforAlternateBerth || "") : "");
    self.MovementType(data ? (data.MovementType || "") : "");
    self.MovementTypeName(data ? (data.MovementTypeName || "") : "");
    self.isDryDock(data ? (data.isDryDock || "") : "");   
 
    self.cache.latestData = data;
}

IPMSROOT.BerthPreSchedulingModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

