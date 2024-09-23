(function (ipmsRoot) {

    var BerthPlanningGISModel = function (data) {

        var self = this;
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.FromDateTime = ko.observable(data ? moment(data.FromDateTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ToDateTime = ko.observable(data ? moment(data.ToDateTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.FromPortCode = ko.observable(data ? data.FromPortCode : "");
        self.FromQuayCode = ko.observable(data ? data.FromQuayCode : "");
        self.FromBerthCode = ko.observable(data ? data.FromBerthCode : "");
        self.FromBollardCode = ko.observable(data ? data.FromBollardCode : "");
        self.FromBollardCoordinates = ko.observable(data ? data.FromBollardCoordinates : "");
        self.FromBollardOffsetCoordinates = ko.observable(data ? data.FromBollardOffsetCoordinates : "");
        self.FromBollardMidCoordinates = ko.observable(data ? data.FromBollardMidCoordinates : "");
        self.ToPortCode = ko.observable(data ? data.ToPortCode : "");
        self.ToQuayCode = ko.observable(data ? data.ToQuayCode : "");
        self.ToBerthCode = ko.observable(data ? data.ToBerthCode : "");
        self.ToBollardCode = ko.observable(data ? data.ToBollardCode : "");
        self.ToBollardCoordinates = ko.observable(data ? data.ToBollardCoordinates : "");
        self.ToBollardOffsetCoordinates = ko.observable(data ? data.ToBollardOffsetCoordinates : "");
        self.ToBollardMidCoordinates = ko.observable(data ? data.ToBollardMidCoordinates : "");
        self.precoord = ko.observable(data ? data.precoord : "");
        self.preoffsetcord = ko.observable(data ? data.preoffsetcord : "");
        self.MovementStatus = ko.observable(data ? data.MovementStatus : ""
            );
        self.Image = ko.computed(function () {
            return self.MovementStatus() == 'MPEN' ? "../Content/Images/vessel-gray.png" : self.MovementStatus() == 'SCH' ? "../Content/Images/vesselsch.png" : self.MovementStatus() == 'CONF' ? "../Content/Images/vesselconf.png" : self.MovementStatus() == 'BERT' ? "../Content/Images/vesselbert.png" : " ";
        });
        self.VCNDisplay = ko.computed(function () {
            return self.VCN() + '-' + self.VesselName();
        });

        self.MovementType = ko.observable(data ? data.MovementType : "");

        self.LOA = ko.observable(data ? data.LOA : "");
        self.GRT = ko.observable(data ? data.GRT : "");
        self.Beam = ko.observable(data ? data.Beam : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.Agent = ko.observable(data ? data.Agent : "");
        self.Draft = ko.observable(data ? data.Draft : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.BerthNo = ko.observable(data ? data.BerthNo : "");
        self.ETA = ko.observable(data ? moment(data.ETA).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ETD = ko.observable(data ? moment(data.ETD).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ETB = ko.observable(data ? moment(data.ETB).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ETUB = ko.observable(data ? moment(data.ETUB).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ATA = ko.observable(data ? moment(data.ATA).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ATD = ko.observable(data ? moment(data.ATD).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ATB = ko.observable(data ? moment(data.ATB).format('YYYY-MM-DD HH:mm:ss') : "");
        self.ATUB = ko.observable(data ? moment(data.ATUB).format('YYYY-MM-DD HH:mm:ss') : "");
        self.VesselStatusColor = ko.computed(function () {
            return self.MovementStatus() == 'SCH' ? "#FFBF00" : self.MovementStatus() == 'CONF' ? "#006400" : self.MovementStatus() == 'BERT' ? "#CD6090" : "#0000A0";
        });
        //  self.VesselStatusColor = ko.observable(data ? data.VesselStatusColor : "");
        self.ReasonforVisit = ko.observable(data ? data.ReasonforVisit : "");
        self.ArrivalCommoditiesNames = ko.observable(data ? data.ArrivalCommoditiesNames : "");
        self.ReasonforVisitName = ko.observable(data ? data.ReasonforVisitName : "");
        self.BerthTime = ko.observable(data ? moment(data.BerthTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.UnBerthTime = ko.observable(data ? moment(data.UnBerthTime).format('YYYY-MM-DD HH:mm:ss') : "");
        self.FromCoordinates = ko.observable(data ? data.FromCoordinates : "");
        self.ToCoordinates = ko.observable(data ? data.ToCoordinates : "");
        self.FromOffsetCoordinates = ko.observable(data ? data.FromOffsetCoordinates : "");
        self.ToOffsetCoordinates = ko.observable(data ? data.ToOffsetCoordinates : "");
        self.FromMidCoordinates = ko.observable(data ? data.FromMidCoordinates : "");
        self.ToMidCoordinates = ko.observable(data ? data.ToMidCoordinates : "");
        self.SideAlongSide = ko.observable(data ? data.SideAlongSide : "");
        self.precoord = ko.observable(data ? data.precoord : "");
        self.preoffsetcord = ko.observable(data ? data.preoffsetcord : "");
        self.MooringBowBollard = ko.observable(data ? data.MooringBowBollard : "");
        self.MooringStemBollard = ko.observable(data ? data.MooringStemBollard : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
    }

    //Anchored vessel code
    var AnchoredVesselGISModel = function (data) {

        var self = this;

        self.VesselName = ko.observable(data ? data.VesselName : "");

        self.VesselType = ko.observable(data ? data.VesselType : "");

        self.DeadWeightTonnageInMT = ko.observable(data ? data.DeadWeightTonnageInMT : "");

        self.IMONo = ko.observable(data ? data.IMONo : "");

        self.VCN = ko.observable(data ? data.VCN : "");

        self.NextPortOfCall = ko.observable(data ? data.NextPortOfCall : "");

        self.ETA = ko.observable(data ? moment(data.ETA).format('YYYY-MM-DD HH:mm:ss') : "");

        self.ETD = ko.observable(data ? moment(data.ETD).format('YYYY-MM-DD HH:mm:ss') : "");


        self.ReasonforvisitName = ko.observable(data ? data.ReasonforvisitName : "");

        self.CargoTypes = ko.observable(data ? data.CargoTypes : "");

        self.ArrDraft = ko.observable(data ? data.ArrDraft : "");

        self.DepDraft = ko.observable(data ? data.DepDraft : "");

        self.LastPortOfCall = ko.observable(data ? data.LastPortOfCall : "");

        self.Reason = ko.observable(data ? data.Reason : "");

        self.AnchorPosition = ko.observable(data ? data.AnchorPosition : "");

        self.BearingDistanceFromBreakWater = ko.observable(data ? data.BearingDistanceFromBreakWater : "");

        self.PortOfRegistry = ko.observable(data ? data.PortOfRegistry : "");

        self.LengthOverallInM = ko.observable(data ? data.LengthOverallInM : "");

        self.BeamInM = ko.observable(data ? data.BeamInM : "");

        self.GrossRegisteredTonnageInMT = ko.observable(data ? data.GrossRegisteredTonnageInMT : "");

        self.VesselNationality = ko.observable(data ? data.VesselNationality : "");

        self.AgentName = ko.observable(data ? data.AgentName : "");

        self.VesselBuildYear = ko.observable(data ? data.VesselBuildYear : "");
        self.PortCode = ko.observable(data ? data.VesselBuildYear : "");
        self.Coordinates = ko.observable(data ? data.Cooordinates : "");
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

    var ConfigurationModel = function (data) {
        var self = this;
        self.ConfigLabelName = ko.observable(data ? data.ConfigLabelName : "");
        self.ConfigName = ko.observable(data ? data.ConfigName : "");
        self.ConfigValue = ko.observable(data ? data.ConfigValue : "");
    }

    var BerthModel = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayLength = ko.observable(data ? data.QuayLength : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthLength = ko.observable(data ? data.Lengthm : "");
        self.Draft = ko.observable(data ? data.Draftm : "");
        self.TidalDraft = ko.observable(data ? data.TidalDraft : "");
        self.BerthType = ko.observable(data ? data.BerthType : "");
        self.CargoTypes = ko.observableArray(data ? data.CargoType : []);
        self.VesselTypes = ko.observableArray(data ? data.VesselTypes : []);
        self.ReasonsForVisitType = ko.observableArray(data ? data.ReasonsForVisitType : []);
        self.TerminalOperators = ko.observableArray(data ? data.TerminalOperators : []);
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
        self.Coordinates = ko.observable(data ? data.Coordinates : "");
        self.OffsetCoordinates = ko.observable(data ? data.OffsetCoordinates : "");
        self.MidCoordinates = ko.observable(data ? data.MidCoordinates : "");
        self.ContinousStatus = ko.observable(data ? data.ContinousStatus : "");


    }

    ipmsRoot.QuayModel = QuayModel;
    ipmsRoot.BerthModel = BerthModel;
    ipmsRoot.Bollard = Bollard;
    ipmsRoot.BerthPlanningGISModel = BerthPlanningGISModel;
    ipmsRoot.AnchoredVesselGISModel = AnchoredVesselGISModel;





}(window.IPMSROOT));
