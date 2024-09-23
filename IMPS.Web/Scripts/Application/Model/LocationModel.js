(function (ipmsRoot) {

    var LocationModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.LocationID = ko.observable();
        self.LocationName = ko.observable("").extend({ CodeUnique: self.LocationName, required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Location Name' } });
        self.PortCode = ko.observable();
        self.PortName = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.LocationPortCode = ko.observable("");
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.LocationIDSort;
        self.LocationID.subscribe(function (value) {
            self.LocationIDSort = value;
        });
        self.LocationNameSort;
        self.LocationName.subscribe(function (value) {
            self.LocationNameSort = value;
        });
        self.PortNameSort;
        self.PortName.subscribe(function (value) {
            self.PortNameSort = value;
        });
        self.RecordStatusSort;
        self.RecordStatus.subscribe(function (value) {
            self.RecordStatusSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.LocationModel = LocationModel;
}(window.IPMSROOT));

IPMSROOT.LocationModel.prototype.set = function (data) {
    var self = this;
    self.LocationID(data ? (data.LocationID == 'NULL' ? "" : data.LocationID || "") : "");
    self.LocationName(data ? (data.LocationName == 'NULL' ? "" : data.LocationName || "") : "");
    self.PortCode(data ? (data.PortCode == 'NULL' ? "" : data.PortCode || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.LocationPortCode(data ? (data.LocationPortCode == 'NULL' ? "" : data.LocationPortCode || "") : "");
    self.PortName(data ? (data.PortName == 'NULL' ? "" : data.PortName || "") : "");
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.LocationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



