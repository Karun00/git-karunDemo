(function (ipmsRoot) {
    var PlannedMovementsModel = function (data) {
        var self = this;

        self.VesselName = ko.observable();
        self.MovementType = ko.observable();
        self.MovementDateTime = ko.observable();
        self.BerthName = ko.observable();
        self.RegisteredName = ko.observable();
        self.VeselType = ko.observable();
        self.LOA = ko.observable();
        self.GRT = ko.observable();
        self.Draft = ko.observable();
        self.ReasonforvisitName = ko.observable();
        self.MovementDateTimeSort;
        self.MovementDateTime.subscribe(function (value) {
            self.MovementDateTimeSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.MovementTypeSort;
        self.MovementType.subscribe(function (value) {
            self.MovementTypeSort = value;
        });


        self.cache = function () { };
        self.set(data);

    }

    ipmsRoot.PlannedMovementsModel = PlannedMovementsModel;

}(window.IPMSROOT));

IPMSROOT.PlannedMovementsModel.prototype.set = function (data) {
    var self = this;

    self.VesselName(data ? (data.VesselName || "") : "");
    self.MovementType(data ? (data.MovementType || "") : "");
    self.MovementDateTime(data ? (data.MovementDateTime) : null);
    self.BerthName(data ? (data.BerthName || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.VeselType(data ? (data.VeselType || "") : "");
    self.LOA(data ? (data.LOA || "") : "");
    self.GRT(data ? (data.GRT || "") : "");
    self.Draft(data ? (data.Draft || "") : "");
    self.ReasonforvisitName(data ? (data.ReasonforvisitName || "") : "");
    self.cache.latestData = data;
}
IPMSROOT.PlannedMovementsModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



