
(function (ipmsRoot) {

    var BerthPlanningModel = function (data) {

        var self = this;
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.VesselCallMovementID = ko.observable(data ? data.VesselCallMovementID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.FromDateTime = ko.observable(data ? moment(data.FromDateTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ToDateTime = ko.observable(data ? moment(data.ToDateTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.FromPortCode = ko.observable(data ? data.FromPortCode : "");
        self.FromQuayCode = ko.observable(data ? data.FromQuayCode : "");
        self.FromBerthCode = ko.observable(data ? data.FromBerthCode : "");
        self.FromBollardCode = ko.observable(data ? data.FromBollardCode : "");
        self.FromBollardMeter = ko.observable(data ? data.FromBollardMeter : "");
        self.ToPortCode = ko.observable(data ? data.ToPortCode : "");
        self.ToQuayCode = ko.observable(data ? data.ToQuayCode : "");
        self.ToBerthCode = ko.observable(data ? data.ToBerthCode : "");
        self.ToBollardCode = ko.observable(data ? data.ToBollardCode : "");
        self.ToBollardMeter = ko.observable(data ? data.ToBollardMeter : "");
        self.PositionX = ko.observable(data ? data.PositionX : "");
        self.PositionY = ko.observable(data ? data.PositionY : "");
        self.MovementStatus = ko.observable(data ? data.MovementStatus : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.MovementTypeName = ko.observable(data ? data.MovementTypeName : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.VesselLength = ko.observable(data ? data.VesselLength : "");
        self.VesselWidth = ko.observable(data ? data.VesselWidth : "");
        self.Tidal = ko.observable(data ? data.Tidal : "");
        self.IsTidal = ko.observable(data ? (data.Tidal == 'A' ? 'Yes' : 'No') : 'No');
        self.LOA = ko.observable(data ? data.LOA : "");
        self.GRT = ko.observable(data ? data.GRT : "");
        self.Beam = ko.observable(data ? data.Beam : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.Agent = ko.observable(data ? data.Agent : "");
        self.Draft = ko.observable(data ? data.Draft : "");
        self.DeptDraft = ko.observable(data ? data.DeptDraft : "");
        self.MaxDraft = ko.computed(function () {
            return self.Draft() > self.DeptDraft() ? self.Draft() : self.DeptDraft();
        });
        self.ShippingLine = ko.observable(data ? data.ShippingLine : "");
        self.CallSign = ko.observable(data ? data.CallSign : "");
        self.NextPortOfCall = ko.observable(data ? data.NextPortOfCall : "");
        self.BerthingSide = ko.observable(data ? data.BerthingSide : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.ToBerthName = ko.observable(data ? data.ToBerthName : "");
        self.NominationDate = ko.observable(data ? moment(data.NominationDate).format('YYYY-MM-DD HH:mm') : "");
        self.ETA = ko.observable(data ? moment(data.ETA).format('YYYY-MM-DD HH:mm') : "");
        self.ETD = ko.observable(data ? moment(data.ETD).format('YYYY-MM-DD HH:mm') : "");
        self.ETB = ko.observable(data ? moment(data.ETB).format('YYYY-MM-DD HH:mm') : "");
        self.ETUB = ko.observable(data ? moment(data.ETUB).format('YYYY-MM-DD HH:mm') : "");
        self.ATA = ko.observable(data ? moment(data.ATA).format('YYYY-MM-DD HH:mm') : "");
        self.ATD = ko.observable(data ? moment(data.ATD).format('YYYY-MM-DD HH:mm') : "");
        self.ATB = ko.observable(data ? moment(data.ATB).format('YYYY-MM-DD HH:mm') : "");
        self.ATUB = ko.observable(data ? moment(data.ATUB).format('YYYY-MM-DD HH:mm') : "");
        self.BerthTime = ko.observable(data ? moment(data.BerthTime).format('YYYY-MM-DD HH:mm') : "");
        self.UnBerthTime = ko.observable(data ? moment(data.UnBerthTime).format('YYYY-MM-DD HH:mm') : "");
        self.Diff = ko.computed(function () {
            return moment.duration(moment(self.UnBerthTime()).diff(self.BerthTime())).asHours();
        });
        self.VesselStatusColor = ko.observable(data ? data.VesselStatusColor : "");
        self.DoubleBankedVessel = ko.observable(data ? data.DoubleBankedVessel : "");
        self.TripleBankedVessel = ko.observable(data ? data.TripleBankedVessel : "");
        self.IsBanked = ko.observable(data ? data.IsBanked : 0);
        self.ReasonforVisit = ko.observable(data ? data.ReasonforVisit : "");
        self.ReasonforVisitName = ko.observable(data ? data.ReasonforVisitName : "");
        self.PositionX = ko.observable(data ? data.PositionX : "");
        self.PositionY = ko.observable(data ? data.PositionY : "");
        self.ArrivalCommodities = ko.observableArray(data ? data.ArrivalCommodities : []);
        self.ArrivalReasons = ko.observableArray(data ? data.ArrivalReasons : []);
        self.SuitableBerthsList = ko.observableArray([]);
        self.ArrivalCommoditiesNames = ko.observable(data ? data.ArrivalCommoditiesNames : "");
        self.PreferredBerth = ko.observable(data ? data.PreferredBerth : "");
        self.AlternateBerth = ko.observable(data ? data.AlternateBerth : "");
        self.isVesselArrested = ko.observable(data ? data.isVesselArrested : "");
        self.SideAlongSide = ko.observable(data ? data.SideAlongSide : "");
        self.MooringBowBollard = ko.observable(data ? data.MooringBowBollard : "");
        self.MooringStemBollard = ko.observable(data ? data.MooringStemBollard : "");

        self.ArrivalReasonsString = ko.observable(data ? data.ArrivalReasonsString : "");
        self.ArrivalCommoditiesString = ko.observable(data ? data.ArrivalCommoditiesString : "");

        self.FromBollardName = ko.observable(data ? data.FromBollardName : "");
        self.ToBollardName = ko.observable(data ? data.ToBollardName : "");

    }

    var BerthMaintainenceModel = function (data) {
        var self = this;
        self.BerthMaintenanceID = ko.observable(data ? data.BerthMaintenanceID : "");
        self.MaintPortCode = ko.observable(data ? data.MaintPortCode : "");
        self.MaintQuayCode = ko.observable(data ? data.MaintQuayCode : "");
        self.MaintBerthCode = ko.observable(data ? data.MaintBerthCode : "");
        self.FromBerthCode = ko.observable(data ? data.FromBerthCode : "");
        self.FromBollard = ko.observable(data ? data.FromBollard : "");
        self.ToBerthCode = ko.observable(data ? data.ToBerthCode : "");
        self.ToBollard = ko.observable(data ? data.ToBollard : "");
        self.PeriodFrom = ko.observable(data ? moment(data.PeriodFrom).format('YYYY-MM-DD HH:mm:ss') : "");
        self.PeriodTo = ko.observable(data ? moment(data.PeriodTo).format('YYYY-MM-DD HH:mm:ss') : "");
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.PositionX = ko.observable(data ? data.PositionX : "");
        self.PositionY = ko.observable(data ? data.PositionY : "");
        self.Length = ko.observable(data ? data.Length : "");
    }

    var Quay = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayName = ko.observable(data ? data.QuayName : "");
        self.QuayLength = ko.observable(data ? data.QuayLength : "");
    }

    var QuayModel = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayName = ko.observable(data ? data.QuayName : "");
        self.QuayLength = ko.observable(data ? data.QuayLength : "");
        self.Berths = ko.observableArray(data ? (data.Berths ? $.map(data.Berths, function (item) { return new BerthModel(item) }) : []) : []);
    }

    //var ConfigurationModel = function (data) {
    //    var self = this;
    //    self.ConfigLabelName = ko.observable(data ? data.ConfigLabelName : "");
    //    self.ConfigName = ko.observable(data ? data.ConfigName : "");
    //    self.ConfigValue = ko.observable(data ? data.ConfigValue : "");             
    //}

    var BerthModel = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayLength = ko.observable(data ? data.QuayLength : "");
        self.BerthID = ko.observable();
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthLength = ko.observable(data ? data.Lengthm : "");
        self.Draft = ko.observable(data ? data.Draftm : "");
        self.TidalDraft = ko.observable(data ? data.TidalDraft : "");
        self.BerthType = ko.observable(data ? data.BerthType : "");
        self.CargoTypes = ko.observableArray(data ? data.CargoType : []);
        self.CargoTypeNames = ko.observable(data ? data.CargoTypeNames : "");
        self.VesselTypes = ko.observableArray(data ? data.VesselTypes : []);
        self.ReasonsForVisitType = ko.observableArray(data ? data.ReasonsForVisitType : []);
        self.TerminalOperators = ko.observableArray(data ? data.TerminalOperators : []);
        self.Bollards = ko.observableArray(data ? (data.Bollards ? $.map(data.Bollards, function (item) { return new Bollard(item) }) : []) : []);

    }


    var UserModel = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : "");
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.RoleCode = ko.observable(data ? data.RoleCode : "");
        self.RoleName = ko.observable(data ? data.RoleName : "");
        self.UserTypeID = ko.observable(data ? data.UserTypeID : "");
        self.isTerminalOperator = ko.observable(data ? data.isTerminalOperator : "");
        self.isBerthPlanner = ko.observable(data ? data.isBerthPlanner : "");
        self.Berths = ko.observableArray(data ? data.Berths : []);
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


    var ConfigurationModel = function (data) {
        var self = this;
        self.Slot = ko.observable(data ? data.Slot : "");
        self.Days = ko.observable(data ? data.Days : "");
        self.UnderKeelClearance = ko.observable(data ? data.UnderKeelClearance : 0);
        self.SafeDistance = ko.observable(data ? data.SafeDistance : 0);
        self.ScheduleColor = ko.observable(data ? data.ScheduleColor : "");
        self.ConfirmColor = ko.observable(data ? data.ConfirmColor : "");
        self.BerthedColor = ko.observable(data ? data.BerthedColor : "");
        self.MaintainenceColor = ko.observable(data ? data.MaintainenceColor : "");
        self.ArrestedColor = ko.observable(data ? data.ArrestedColor : "");
        self.SailedColor = ko.observable(data ? data.SailedColor : "");
    }

    ipmsRoot.Quay = Quay;
    ipmsRoot.QuayModel = QuayModel;
    ipmsRoot.BerthModel = BerthModel;
    ipmsRoot.Bollard = Bollard;
    ipmsRoot.BerthPlanningModel = BerthPlanningModel;
    ipmsRoot.BerthMaintainenceModel = BerthMaintainenceModel;
    ipmsRoot.UserModel = UserModel;
    ipmsRoot.ConfigurationModel = ConfigurationModel;
}(window.IPMSROOT));



