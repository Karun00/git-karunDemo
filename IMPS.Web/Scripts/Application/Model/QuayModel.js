
(function (ipmsRoot) {

    var QuayModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.PortCode = ko.observable("").extend({ required: true });
        self.PortName = ko.observable("");
        self.QuayCode = ko.observable("").extend({ CodeUnique: self.QuayCode, required: true }); 
        self.QuayName = ko.observable("").extend({ required: true }); 
        self.ShortName = ko.observable("").extend({ required: true });
        self.QuayLength = ko.observable("").extend({ required: true });
        self.Description = ko.observable("");
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable(""); 
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
           
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.QuayCodeSort;
        self.QuayCode.subscribe(function (value) {
            self.QuayCodeSort = value;
        });
        self.QuayNameSort;
        self.QuayName.subscribe(function (value) {
            self.QuayNameSort = value;
        });
        self.ShortNameSort;
        self.ShortName.subscribe(function (value) {
            self.ShortNameSort = value;
        });
        self.PortNameSort;
        self.PortName.subscribe(function (value) {
            self.PortNameSort = value;
        });
        self.QuayLengthSort;
        self.QuayLength.subscribe(function (value) {
            self.QuayLengthSort = value.toString();
        });


        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.QuayModel = QuayModel;
}(window.IPMSROOT));

IPMSROOT.QuayModel.prototype.set = function (data) {
    var self = this;
    self.QuayCode(data ? (data.QuayCode || "") : "");  
    self.PortName(data ? (data.PortName == 'NULL' ? "" : data.PortName || "") : "");
    self.QuayName(data ? (data.QuayName == 'NULL' ? "" : data.QuayName || "") : "");
    self.ShortName(data ? (data.ShortName == 'NULL' ? "" : data.ShortName || "") : "");
    self.QuayLength(data ? (data.QuayLength == 'NULL' ? "" : data.QuayLength || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");   
    self.PortCode(data ? (data.PortCode || "") : "");
    self.Description(data ? (data.Description || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.QuayModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}



