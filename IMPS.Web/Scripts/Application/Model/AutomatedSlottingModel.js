(function (ipmsRoot) {
    var AutomatedSlotting = function (data) {
        var self = this;
        self.ReasonTypes = ko.observableArray();
        self.ReasonTypeCodes = ko.observable();


        self.cache = function () { };
        self.set(data);
    }

    var ConfigurationDetailsModel = function (data) {
        var self = this;
        self.SlotCofiguratinid = ko.observable(data ? data.SlotCofiguratinid : "");
        self.EffectiveFrm = ko.observable(data ? data.EffectiveFrm : "");
        self.Duration = ko.observable(data ? data.Duration : "");
        self.NoofSlots = ko.observable(data ? data.NoofSlots : "");
        self.ExtendableSlots = ko.observable(data ? data.ExtendableSlots : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
      
       
                           
    }

    var VesselInfoModel = function (data) {
      
        var self = this;
        self.VesselCallMovementID = ko.observable(data ? data.VesselCallMovementID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ETA = ko.observable(data ? data.ETA : "");
        self.MovementTypeCode = ko.observable(data?data.MovementTypeCode:"");


        self.PossitionX = ko.observable(data ? data.PossitionX : "");
        self.PossitionY = ko.observable(data ? data.PossitionY : "");

        self.shapeWidth = ko.observable();
        self.shapeHeight = ko.observable();

        self.SlotStatus = ko.observable(data ? data.SlotStatus : "");
        self.Slot = ko.observable(data ? data.Slot : "");
        self.SlotDate = ko.observable(data ? data.SlotDate : "");
        //  txt = "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td class='control-label'  style='text-align:right'>VCN</td><td>:</td><td style='text-align:left'>" + self.VCN() + " </td><td  style='text-align:right'>VesselName</td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'>Vessel Type</td><td>:</td><td style='text-align:left'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>ETA</td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD hh:mm:ss A') + " </td><td  style='text-align:right'>Reason For Visit</td><td>:</td><td style='text-align:left'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'>Cargo Type</td><td>:</td><td style='text-align:left'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>LOA</td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'>Beam</td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'>ArrivalDraft</td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>GRT</td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'>DWT</td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'>Last Port</td><td>:</td><td style='text-align:left'>" + self.PreviousPort() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>Tidal Condition</td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'>DayLight Condition</td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td><td  style='text-align:right'>Current Berth</td><td>:</td><td style='text-align:left'>" + self.CurrentBerth() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>Movement Type</td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'>To Berth</td><td>:</td><td style='text-align:left'>" + self.ToBerth() + " </td><td  style='text-align:right'>Current Bollard No's</td><td>:</td><td style='text-align:left'>" + self.FromBollard() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'>To Bollard No's</td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td><td  style='text-align:right'>Movement Date and Time</td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD hh:mm:ss A') + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> "
        //var txt = "<table style='border:0px solid #f00; line-height:30px;'>Vssel Call Movement ID</td><td width='10%'>:</td><td width='60%'>" + self.VesselCallMovementID() + " </td></tr><tr><td width='30%'>Vssel Name</td><td width='10%'>:</td><td width='60%'>" + self.VesselName() + " </td></tr><tr><td>VCN</td><td>:</td><td>" + self.VCN() + " </td></tr>  <tr><td>Vessel Type</td><td>:</td><td>" + self.VesselType() + " </td></tr></tr>  <tr><td>Slot Status</td><td>:</td><td>" + self.SlotStatus() + " </td></tr></table> ";
        var txt = "<table width='100%'><tr><td width='30%'>Vssel Call Movement ID</td><td width='10%'>:</td><td width='60%'>" + self.VesselCallMovementID() + " </td></tr><tr><td width='30%'>Vssel Name</td><td width='10%'>:</td><td width='60%'>" + self.VesselName() + " </td></tr><tr><td>VCN</td><td>:</td><td>" + self.VCN() + " </td></tr>  <tr><td>Vessel Type</td><td>:</td><td>" + self.VesselType() + " </td></tr></tr>  <tr><td>Slot Status</td><td>:</td><td>" + self.SlotStatus() + " </td></tr></table> ";
        self.ToolTip = ko.observable(txt)
        self.ExtendYn = ko.observable(data ? data.ExtendYn : "N");
        self.FromPositionPortCode = ko.observable(data ? data.FromPositionPortCode : "");
        self.ReasonForOverride = ko.observable(data ? data.ReasonName : "Nil");

        //self.ReasonName = ko.observable(data ? data.CategoryName : '');
        //self.ReasonCode = ko.observable(data ? data.CategoryCode : '');
        self.EnteredDateTime = ko.observable(data ? data.EnteredDateTime : '');

    }

    var PlannedVesselMovementsData = function (data) {
   
        var self = this;
        self.PlannedVesselMovements = ko.observableArray(data ? new PlannedMovements(data) : []);

    }

    //var PlannedMovements = function (data) {
    //    debugger;
    //    var self = this;

    //    self.endX = ko.observable(data ? data.endX() : "");
    //    self.endY = ko.observable(data ? data.endY() : "");
    //    self.VCN = ko.observable(data ? data.item().VCN() : "");
    //    self.VesselName = ko.observable(data ? data.item().VesselName() : "");
    //    self.VesselType = ko.observable(data ? data.item().VesselType() : "");
    //    self.MovementType = ko.observable(data ? data.item().MovementType() : "");
    //    self.ETA = ko.observable(data ? data.item().ETA() : "");

    //}


    ipmsRoot.AutomatedSlotting = AutomatedSlotting;
    ipmsRoot.VesselInfoModel = VesselInfoModel;
    ipmsRoot.ConfigurationDetailsModel = ConfigurationDetailsModel;
    ipmsRoot.PlannedVesselMovementsData = PlannedVesselMovementsData;

}(window.IPMSROOT));

IPMSROOT.AutomatedSlotting.prototype.set = function (data) {
    var self = this;

    self.SlotStatus = ko.observable(data ? (data.SlotStatus || "") : "");
    self.Slot = ko.observable(data ? (data.Slot || "") : "");
    self.SlotDate = ko.observable(data ? (data.SlotDate || "") : "");
    self.VesselCallMovementID = ko.observable(data ? (data.VesselCallMovementID || "") : "");

    self.cache.latestData = data;
}