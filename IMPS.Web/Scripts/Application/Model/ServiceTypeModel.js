(function (ipmsRoot) {
    var ServiceTypeModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.ServiceTypeID = ko.observable();
        self.ServiceTypeName = ko.observable("").extend({ NameUnique: self.ServiceTypeName, required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Service Type Name' } });
        self.ServiceTypeCode = ko.observable("").extend({ CodeUnique: self.ServiceTypeCode, required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Service Type Code' } });
        self.RecordStatus = ko.observable('A');
        self.IsCraft = ko.observable(data ? data.IsCraft : false);
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.IsCraftStatus = ko.computed(function () {
            if (self.IsCraft() == false || self.IsCraft() == "" || self.IsCraft() == null || self.IsCraft() == undefined) {
                self.IsCraftStatus = "No";
                self.IsCraft(false);
            }
            if (self.IsCraft() == true) {
                self.IsCraftStatus = "Yes";
            }

            return self.IsCraftStatus;
        });
        
        self.ServiceTypeNameSort;
        self.ServiceTypeName.subscribe(function (value) {
            self.ServiceTypeNameSort = value;
        });

        self.ServiceTypeCodeSort;
        self.ServiceTypeCode.subscribe(function (value) {
            self.ServiceTypeCodeSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.ServiceTypeModel = ServiceTypeModel;
}(window.IPMSROOT));

IPMSROOT.ServiceTypeModel.prototype.set = function (data) {
    var self = this;

    self.ServiceTypeID(data ? (data.ServiceTypeID == 'NULL' ? "" : data.ServiceTypeID || "") : "");
    self.ServiceTypeName(data ? (data.ServiceTypeName == 'NULL' ? "" : data.ServiceTypeName || "") : "");
    self.ServiceTypeCode(data ? (data.ServiceTypeCode == 'NULL' ? "" : data.ServiceTypeCode || "") : "");
    self.IsCraft(data ? data.IsCraft : false);
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy == 'NULL' ? "" : data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate == 'NULL' ? "" : data.ModifiedDate || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.ServiceTypeModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



