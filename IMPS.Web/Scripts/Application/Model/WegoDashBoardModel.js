(function (ipmsRoot) { 

    var WegoDashBoardModel = function (data, val) {
        var self = this;

        self.dashboadPrivilege = ko.observable(true);     
        
        var fromdate = new Date();
        var todate = new Date();
        
        fromdate.setDate(1);
        fromdate.setMonth(3);
        fromdate.setHours(0);
        fromdate.setMinutes(0);
        fromdate.setSeconds(0)
        
        if (todate.getMonth() >= 3) {
            fromdate.setFullYear(todate.getFullYear());
        }
        else {
            fromdate.setFullYear(todate.getFullYear() - 1);
        }


        self.FromDate = ko.observable(fromdate);
        self.ToDate = ko.observable(new Date());        

        self.WegoDashBoardDetails = ko.observableArray(data ? ko.utils.arrayMap(data.WegoDashBoardDetails, function (item) {
            return new WegoDashBoardDetail(item);
        }) : []);
       
        self.cache = function () { };
        self.set(data);

    };

    var WegoDashBoardDetail = function (data) {
        var self = this;
        
        self.WegoKPI = ko.observable(data ? data.WegoKPI : "");        
        self.Automotive = ko.observable(data ? data.Automotive != null ? data.Automotive.replace(',', '.') : "-" : "-");
        self.BreakBulk = ko.observable(data ? data.BreakBulk != null ? data.BreakBulk.replace(',', '.') : "-" : "-");
        self.Bulk = ko.observable(data ? data.Bulk != null ? data.Bulk.replace(',', '.') : "-" : "-");
        self.Container = ko.observable(data ? data.Container != null ? data.Container.replace(',', '.') : "-" : "-");
        self.LiquidBulk = ko.observable(data ? data.LiquidBulk != null ? data.LiquidBulk.replace(',', '.') : "-" : "-");
        self.NonOperational = ko.observable(data ? data.NonOperational != null ? data.NonOperational.replace(',', '.') : "-" : "-");
        self.Bunkers = ko.observable(data ? data.Bunkers != null ? data.Bunkers.replace(',', '.') : "-" : "-");
        self.Passengers = ko.observable(data ? data.Passengers != null ? data.Passengers.replace(',', '.') : "-" : "-");
        self.ALL = ko.observable(data ? data.ALL != null ? data.ALL.replace(',', '.') : "-" : "-");

        self.ShipWorkingHour = ko.observable(data ? data.ShipWorkingHour != null ? data.ShipWorkingHour.replace(',', '.') : "-" : "-");
        self.BerthProductivity = ko.observable(data ? data.BerthProductivity != null ? data.BerthProductivity.replace(',', '.') : "-" : "-");
        self.ShipProductivityIndicator = ko.observable(data ? data.ShipProductivityIndicator != null ? data.ShipProductivityIndicator.replace(',', '.') : "-" : "-");
        self.TotalVolumes = ko.observable(data ? data.TotalVolumes != null ? data.TotalVolumes.replace(',', '.') : "-" : "-");
        self.ParcelSizes = ko.observable(data ? data.ParcelSizes != null ? data.ParcelSizes.replace(',', '.') : "-" : "-");
        self.PreCargoWorking = ko.observable(data ? data.PreCargoWorking != null ? data.PreCargoWorking.replace(',', '.') : "-" : "-");
        self.WorkingTime = ko.observable(data ? data.WorkingTime != null ? data.WorkingTime.replace(',', '.') : "-" : "-");
        self.DepartureWaiting = ko.observable(data ? data.DepartureWaiting != null ? data.DepartureWaiting.replace(',', '.') : "-" : "-");
    }

    ipmsRoot.WegoDashBoardModel = WegoDashBoardModel;
    ipmsRoot.WegoDashBoardDetail = WegoDashBoardDetail;  
}(window.IPMSROOT));

IPMSROOT.WegoDashBoardModel.prototype.set = function (data) {
    var self = this;
    self.cache.latestData = data;

}
IPMSROOT.WegoDashBoardModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

