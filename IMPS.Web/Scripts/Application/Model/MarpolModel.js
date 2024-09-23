(function (ipmsRoot) {

    var MarpolReferenceData = function (data) {
        var self = this;
        self.MarpolTypes = ko.observableArray(data ? $.map(data.MarpolTypes, function (item) { return new MarpolType(item); }) : []);      
    }

    var MarpolType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }   

    var MarpolModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.ClassCode = ko.observable("").extend({ required: true });
        self.ClassName = ko.observable("").extend({ required: true });
        self.MarpolCode = ko.observable("").extend({ required: true });
        self.MarpolName = ko.observable("");

        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });       


        self.ClassCodeSort;
        self.ClassCode.subscribe(function (value) {
            self.ClassCodeSort = value;
        });
        self.ClassNameSort;
        self.ClassName.subscribe(function (value) {
            self.ClassNameSort = value;
        });
        self.MarpolNameSort;
        self.MarpolName.subscribe(function (value) {
            self.MarpolNameSort = value;
        });


        self.cache = function () { };
        self.set(data);
    }



    ipmsRoot.MarpolModel = MarpolModel;
    ipmsRoot.MarpolReferenceData = MarpolReferenceData;
    ipmsRoot.MarpolType = MarpolType;
    


}(window.IPMSROOT));

IPMSROOT.MarpolModel.prototype.set = function (data) {
    var self = this;

    self.ClassCode(data ? (data.ClassCode || "") : "");    
    self.ClassName(data ? data.ClassName || "" : "");
    self.MarpolCode(data ? (data.MarpolCode || "") : "");
    self.MarpolName(data ? (data.MarpolName || "") : "");


    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy == 'NULL' ? "" : data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate == 'NULL' ? "" : data.ModifiedDate || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.MarpolModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

