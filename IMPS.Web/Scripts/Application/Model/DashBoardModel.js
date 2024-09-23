(function (ipmsRoot) {

    var DashBoardModel = function (data, val) {
        var self = this;




        self.BerthOccupancy = ko.observable(data ? data.BerthOccupancy : null);
        self.BerthUtilization = ko.observable(data ? data.BerthUtilization : null);
        self.ResourceUtilization = ko.observable(data ? data.ResourceUtilization : null);
        self.VesselMovements = ko.observable(data ? data.VesselMovements : null);
        self.CommodityHandle = ko.observable(data ? data.CommodityHandle : null);
        self.DelayStatistics = ko.observable(data ? data.DelayStatistics : null);
        self.ISPSClearance = ko.observable(data ? data.ISPSClearance : null);
        self.SafetyStatistics = ko.observable(data ? data.SafetyStatistics : null);
        self.ISPSClearance_NC = ko.observable(data ? data.ISPSClearance_NC : null);
        self.ISPSClearance_Ex = ko.observable(data ? data.ISPSClearance_Ex : null);
        self.SafetyStatistics_DF = ko.observable(data ? data.SafetyStatistics_DF : null);
        self.SafetyStatistics_ADC = ko.observable(data ? data.SafetyStatistics_ADC : null);

        // self.dashboadPrivilege = ko.observable();


        self.dashboadPrivilege = ko.computed(function () {

            return self.BerthUtilization() == null && self.BerthOccupancy() == null && self.ResourceUtilization() == null && self.VesselMovements() == null && self.CommodityHandle() == null && self.DelayStatistics() == null && self.ISPSClearance() == null && self.SafetyStatistics() == null ? false : true;
        });

        self.FromDate = ko.observable(moment().add('days', -(Number, val)).toDate());
        self.ToDate = ko.observable(new Date());


        self.PlannedMovementsCount = ko.observable();
        self.PlannedMovtsArrivalCount = ko.observable();
        self.PlannedMovtsShiftingCount = ko.observable();
        self.PlannedMovtsWarpingCount = ko.observable();
        self.PlannedMovtsSailingCount = ko.observable();


        self.AnchorCount = ko.observable();
        self.BerthCount = ko.observable();
        self.Sailed = ko.observable();
        self.BerthName = ko.observable();

        self.cache = function () { };
        self.set(data);

    };


    ipmsRoot.DashBoardModel = DashBoardModel;

}(window.IPMSROOT));

IPMSROOT.DashBoardModel.prototype.set = function (data) {
    var self = this;

    self.PlannedMovementsCount(data ? data.PlannedMovementsCount : null);
    self.PlannedMovtsArrivalCount(data ? data.PlannedMovtsArrivalCount : null);
    self.PlannedMovtsShiftingCount(data ? data.PlannedMovtsShiftingCount : null);
    self.PlannedMovtsWarpingCount(data ? data.PlannedMovtsWarpingCount : null);
    self.PlannedMovtsSailingCount(data ? data.PlannedMovtsSailingCount : null);
    self.AnchorCount(data ? data.AnchoredCount : null);
    self.BerthCount(data ? data.BerthedCount : null);
    self.Sailed(data ? data.SailedCount : null);
    self.BerthName(data ? data.BerthName : null);
    self.cache.latestData = data;

}
IPMSROOT.DashBoardModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



