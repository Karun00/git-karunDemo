(function (ipmsRoot) {

    //Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PortGeneralConfigsModel = function (data) {
        var self = this;
        self.PortGeneralConfigID = ko.observable();
        self.GroupName = ko.observable();
        self.ConfigLabelName = ko.observable();
        self.ConfigName = ko.observable();
        self.ConfigValue = ko.observable();
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.GroupNamesList = ko.observable();
        self.GroupNames = ko.observableArray([]);

        self.GroupNameSort;
        self.GroupName.subscribe(function (value) {
            self.GroupNameSort = value;
        });

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.cache = function () { };
        self.set(data);
    }

    var GroupNamesList = function (data) {
        var self = this;
        self.PortGeneralConfigID = ko.observable(data ? data.PortGeneralConfigID : "");
        self.ConfigLabelName = ko.observable(data ? data.ConfigLabelName : "");
        self.ConfigValue = ko.observable(data ? data.ConfigValue : "");
        self.ConfigName = ko.observable(data ? data.ConfigName : "");
        self.GroupName = ko.observable(data ? data.GroupName : "");
    }

    ipmsRoot.GroupNamesList = GroupNamesList;
    ipmsRoot.PortGeneralConfigsModel = PortGeneralConfigsModel;
}(window.IPMSROOT));

IPMSROOT.PortGeneralConfigsModel.prototype.set = function (data) {
    var self = this;
    self.PortGeneralConfigID(data ? (data.PortGeneralConfigID || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.ConfigLabelName(data ? (data.ConfigLabelName || "") : "");
    self.ConfigName(data ? (data.ConfigName || "") : "");
    self.ConfigValue(data ? (data.ConfigValue || "") : "");
    self.GroupName(data ? (data.GroupName || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.GroupNames(data ? (data.GroupNames ? $.map(data.GroupNames, function (item) { return new IPMSROOT.GroupNamesList(item) }) : []) : []);

    self.cache.latestData = data;
}

IPMSROOT.PortGeneralConfigsModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}