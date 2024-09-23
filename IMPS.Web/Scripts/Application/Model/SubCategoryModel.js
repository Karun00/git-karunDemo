(function (ipmsRoot) {

    var SubCategoryModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.SupCatName = ko.observable("");
        self.SubCatCode = ko.observable("").extend({ CodeUnique: self.SubCatCode, required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.SubCatName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.SupCatCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
      
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.SubCategoryCodeSort;
        self.SubCatCode.subscribe(function (value) {
            self.SubCategoryCodeSort = value;
        });

        self.SubCategoryNameSort;
        self.SubCatName.subscribe(function (value) {
            self.SubCategoryNameSort = value;
        });

        self.StatusSort;
        self.RecordStatus.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.SubCategoryModel = SubCategoryModel;
}(window.IPMSROOT));

IPMSROOT.SubCategoryModel.prototype.set = function (data) {
    var self = this;
    self.SubCatCode(data ? (data.SubCatCode || "") : "");
    self.SupCatCode(data ? (data.SupCatCode == 'NULL' ? "" : data.SupCatCode || "") : "");
    self.SubCatName(data ? (data.SubCatName == 'NULL' ? "" : data.SubCatName || "") : "");
    self.SupCatName(data ? (data.SupCatName == 'NULL' ? "" : data.SupCatName || "") : "");

    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.ModifiedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.SubCategoryModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}
