(function (ipmsRoot) {

    var CargoDashboardModel = function (data, val) {
        var self = this;

        self.dashboadPrivilege = ko.observable(true);

        var fromdate = new Date();
        var todate = new Date();

        //fromdate.setDate(1);
        //fromdate.setMonth(3);
        //fromdate.setHours(0);
        //fromdate.setMinutes(0);
        //fromdate.setSeconds(0)
        fromdate.setDate(fromdate.getDate() - 7);

        //if (todate.getMonth() >= 3) {
        //    fromdate.setFullYear(todate.getFullYear());
        //}
        //else {
        //    fromdate.setFullYear(todate.getFullYear() - 1);
        //}


        self.FromDate = ko.observable(fromdate);
        self.ToDate = ko.observable(new Date());


        self.CargoDashboardDetail = ko.observableArray(data ? ko.utils.arrayMap(data.CargoDashboardDetail, function (item) {
            return new CargoDashboardDetail(item);
        }) : []);
        self.cache = function () { };
        self.set(data);

    };

    var CargoDashboardDetail = function (data) {
        var self = this;
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        self.ONBERTH = ko.observable(data ? data.ONBERTH : "");
        self.PLANNEDMOVEMENTS = ko.observable(data ? data.PLANNEDMOVEMENTS : "");
        self.ANCHORAGE = ko.observable(data ? data.ANCHORAGE : "");

    }

    ipmsRoot.CargoDashboardModel = CargoDashboardModel;
    ipmsRoot.CargoDashboardDetail = CargoDashboardDetail;

}(window.IPMSROOT));

IPMSROOT.CargoDashboardModel.prototype.set = function (data) {
    var self = this;
    self.cache.latestData = data;

}
IPMSROOT.CargoDashboardModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



