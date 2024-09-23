(function (ipmsRoot) {

    var WegoBerthDashBoardModel = function (data, val) {
        var self = this;

        self.dashboadPrivilege = ko.observable(true);

        var fromdate = new Date();
        var todate = new Date();
        fromdate.setDate(1);
        fromdate.setMonth(3);

        if (todate.getMonth() >= 3) {
            fromdate.setFullYear(todate.getFullYear());
        }
        else {
            fromdate.setFullYear(todate.getFullYear() - 1);
        }


        self.FromDate = ko.observable(fromdate);
        self.ToDate = ko.observable(new Date());

        self.WegoBerthDashBoardDetails = ko.observableArray(data ? ko.utils.arrayMap(data.WegoBerthDashBoardDetails, function (item) {
            return new WegoBerthDashBoardDetail(item);
        }) : []);

        self.cache = function () { };
        self.set(data);

    };

    var WegoBerthDashBoardDetail = function (data) {
        var self = this;

        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.NoofVessels = ko.observable(data ? data.NoofVessels != null ? data.NoofVessels : "-" : "-");        
        self.AnchorageWaitingTime = ko.observable(data ? data.AnchorageWaitingTime != null ? data.AnchorageWaitingTime.replace(',', '.') : "-" : "-");
        self.STAT = ko.observable(data ? data.STAT != null ? data.STAT.replace(',', '.') : "-" : "-");
        self.PilotageTimeIn = ko.observable(data ? data.PilotageTimeIn != null ? data.PilotageTimeIn.replace(',', '.') : "-" : "-");
        self.PreCargoWorking = ko.observable(data ? data.PreCargoWorking != null ? data.PreCargoWorking.replace(',', '.') : "-" : "-");
        self.VesselWorkingTime = ko.observable(data ? data.VesselWorkingTime != null ? data.VesselWorkingTime.replace(',', '.') : "-" : "-");
        self.PostCargoWorkingTime = ko.observable(data ? data.PostCargoWorkingTime != null ? data.PostCargoWorkingTime.replace(',', '.') : "-" : "-");
        self.PilotageTimeOut = ko.observable(data ? data.PilotageTimeOut != null ? data.PilotageTimeOut.replace(',', '.') : "-" : "-");        
    }


    ipmsRoot.WegoBerthDashBoardModel = WegoBerthDashBoardModel;
    ipmsRoot.WegoBerthDashBoardDetail = WegoBerthDashBoardDetail;

}(window.IPMSROOT));

IPMSROOT.WegoBerthDashBoardModel.prototype.set = function (data) {
    var self = this;
    self.cache.latestData = data;

}
IPMSROOT.WegoBerthDashBoardModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


