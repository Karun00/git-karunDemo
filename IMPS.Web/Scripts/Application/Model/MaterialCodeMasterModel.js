(function (ipmsRoot) {

    var MaterialCodeMasterModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.GroupCode = ko.observable("");
        self.MaterialCode = ko.observable("");
        self.Remarks = ko.observable("");
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

     

        self.MaterialGroupSort;
        self.GroupCode.subscribe(function (value) {
            self.MaterialGroupSort = value;
        });
        self.MaterialCodeSort;
        self.MaterialCode.subscribe(function (value) {
            self.MaterialCodeSort = value;
        });

        self.DescriptionSort;
        self.Remarks.subscribe(function (value) {
            self.DescriptionSort = value;
        });

        
        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.MaterialCodeMasterModel = MaterialCodeMasterModel;
}(window.IPMSROOT));

IPMSROOT.MaterialCodeMasterModel.prototype.set = function (data) {
    var self = this;
    self.GroupCode(data ? (data.GroupCode || "") : "");
    self.MaterialCode(data ? (data.MaterialCode || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.MaterialCodeMasterModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


