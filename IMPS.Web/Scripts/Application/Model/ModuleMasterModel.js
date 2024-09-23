(function (ipmsRoot) {
    var ModuleMasterModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);

        self.ModuleName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.Module1 = ko.observable();
        self.ModuleID = ko.observable("");
        self.OrderNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.CreatedBy = ko.observable();
        self.ModifiedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedDate = ko.observable();

        self.Module = ko.observable("True");

        self.Module.subscribe(function (item) {
            if (item == 'True') {
                self.ParentModuleID("");
            }
        });

        self.ParentModuleID = ko.observable();
        self.ParentModuleID.subscribe(function (item) {
            if (item > 0) {
                self.Module("False");
            }
        });

        self.RecordStatus = ko.observable();
        self.Status = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.ModuleMasterModel = ModuleMasterModel;
}(window.IPMSROOT));

// Sets all values, will be called at the time of reset
IPMSROOT.ModuleMasterModel.prototype.set = function (data) {
    var self = this;

    self.ModuleName(data ? (data.ModuleName == 'NULL' ? "" : data.ModuleName || "") : "");
    self.OrderNo(data ? (data.OrderNo == 'NULL' ? "" : data.OrderNo || "") : "");
    self.ParentModuleID(data ? (data.ParentModuleID == 'NULL' ? null : data.ParentModuleID || "") : null);
    self.ModuleID(data ? (data.ModuleID == 'NULL' ? "" : data.ModuleID || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? data.CreatedBy || "" : "");
    self.ModifiedBy(data ? data.ModifiedBy || "" : "");
    self.CreatedDate(data ? data.CreatedDate || "" : "");
    self.ModifiedDate(data ? data.ModifiedDate || "" : "");

    self.cache.latestData = data;
}

IPMSROOT.ModuleMasterModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}