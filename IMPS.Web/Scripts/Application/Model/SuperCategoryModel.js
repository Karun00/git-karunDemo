(function (ipmsRoot) {

    var SuperCategoryModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.SupCatCode = ko.observable("").extend({ CodeUnique: self.SupCatCode, required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Super Category Code' } });
        self.SupCatName = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Super Category Name' } });
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");   

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.SupCatCodeSort;
        self.SupCatCode.subscribe(function (value) {
            self.SupCatCodeSort = value;
        });
        self.SupCatNameSort;
        self.SupCatName.subscribe(function (value) {
            self.SupCatNameSort = value;
        });      


        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.SuperCategoryModel = SuperCategoryModel;
}(window.IPMSROOT));

IPMSROOT.SuperCategoryModel.prototype.set = function (data) {
    var self = this;
    self.SupCatCode(data ? (data.SupCatCode || "") : "");
    self.SupCatName(data ? (data.SupCatName == 'NULL' ? "" : data.SupCatName || "") : "");   
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.SuperCategoryModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


