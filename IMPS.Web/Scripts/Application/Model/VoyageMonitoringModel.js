(function (ipmsRoot) {

    var serviceReqDtls = function (data) {
        var self = this;
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.RequestDatetime = ko.observable(data ? data.RequestDatetime : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    }

    var vesselModel = function (data) {
        var self = this;
        self.Vessels = ko.observableArray(data ? $.map(data, function (item1) { return new vesselDetail(item1); }) : []);
    }

    var vesselDetail = function (data) {
        var self = this;
        self.VCN = ko.observable(data ? data.VCN : "");
    }

    var VoyageMonitoringModel = function (data) {
        var self = this;

        self.VesselData = ko.observable();
        self.getServiceRequestDetailss = ko.observable();
        self.VCN = ko.observable("");
        self.VCNVesselName = ko.observable("");
        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.VoyageMonitoringModel = VoyageMonitoringModel;
    ipmsRoot.vesselDetail = vesselDetail;
    ipmsRoot.vesselModel = vesselModel;

}(window.IPMSROOT));

IPMSROOT.VoyageMonitoringModel.prototype.set = function (data) {
    var self = this;

    self.VCN(data ? data.VCN || "" : "");
    self.VCNVesselName(data ? data.VCNVesselName || "" : "");
    self.cache.latestData = data;
}
