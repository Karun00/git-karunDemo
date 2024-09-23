(function (ipmsRoot) {

    var TotalMovementsDashboardModel = function (data, val) {
        var self = this;

        self.dashboadPrivilege = ko.observable(true);

        var fromdate = new Date();
        var todate = new Date();
        fromdate.setDate(fromdate.getDate() - 7);
        //fromdate.setDate(1);
        //fromdate.setMonth(3);
        //fromdate.setHours(0);
        //fromdate.setMinutes(0);
        //fromdate.setSeconds(0)

        //if (todate.getMonth() >= 3) {
        //    fromdate.setFullYear(todate.getFullYear());
        //}
        //else {
        //    fromdate.setFullYear(todate.getFullYear() - 1);
        //}


        self.FromDate = ko.observable(fromdate);
        self.ToDate = ko.observable(new Date());


        self.TotalMovementsDashboardDetails = ko.observableArray(data ? ko.utils.arrayMap(data.TotalMovementsDashboardDetails, function (item) {
            return new TotalMovementsDashboardDetail(item);
        }) : []);
        self.cache = function () { };
        self.set(data);

    };


    var TotalMovementsDashboardDetail = function (data) {
        var self = this;
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.CapeTown = ko.observable(data ? data.CapeTown : "");
        self.Durban = ko.observable(data ? data.Durban : "");
        self.EastLondon = ko.observable(data ? data.EastLondon : "");
        self.MosselBay = ko.observable(data ? data.MosselBay : "");
        self.Ngqura = ko.observable(data ? data.Ngqura : "");
        self.PortElizabeth = ko.observable(data ? data.PortElizabeth : "");
        self.RichardsBay = ko.observable(data ? data.RichardsBay : "");
        self.SaldanhaBay = ko.observable(data ? data.SaldanhaBay : "");
        self.Total = ko.observable(data ? data.Total : "");
    }
    ipmsRoot.TotalMovementsDashboardModel = TotalMovementsDashboardModel;
    ipmsRoot.TotalMovementsDashboardDetail = TotalMovementsDashboardDetail;

}(window.IPMSROOT));


IPMSROOT.TotalMovementsDashboardModel.prototype.set = function (data) {
    var self = this;
    self.cache.latestData = data;

}

IPMSROOT.TotalMovementsDashboardModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

