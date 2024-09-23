(function (ipmsRoot) {

    var ServiceTypeDesignationModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);

        self.ServiceTypeDesignationID = ko.observable("");
        self.ServiceTypeID = ko.observable("");
        self.PortCode = ko.observable("");
        self.ServiceTypeName = ko.observable("");
        self.ServiceTypeCode = ko.observable("");
        self.DesignationName = ko.observable("");
        self.IsCraft = ko.observable(data ? data.IsCraft : false);
        self.PortName = ko.observable("");
        self.CraftType = ko.observable("");
        self.DesignationCode = ko.observable("");
        self.CraftName = ko.observable("");

        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.ServiceTypeDesignations = ko.observableArray();

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

    var ServiceTypeDesignation = function (data) {    
        var self = this;
        self.ServiceTypeDesignationID = ko.observable(data ? data.ServiceTypeDesignationID : 0);
        self.ServiceTypeID = ko.observable(data ? data.ServiceTypeID : "");
        self.DesignationCode = ko.observable(data ? data.DesignationCode : "");
        self.CraftType = ko.observable(data ? data.CraftType : "");
        self.PortCode = ko.observable(data ? (data.PortCode == null ? "" : data.PortCode || "") : "");

        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? (data.CreatedBy == null ? "" : data.CreatedBy || "") : "");
        self.CreatedDate = ko.observable(data ? (data.CreatedDate == null ? "" : data.CreatedDate || "") : "");
        self.ModifiedBy = ko.observable(data ? (data.ModifiedBy == null ? "" : data.ModifiedBy || "") : "");
        self.ModifiedDate = ko.observable(data ? (data.ModifiedDate == null ? "" : data.ModifiedDate || "") : "");
    }

    ipmsRoot.ServiceTypeDesignation = ServiceTypeDesignation;
    ipmsRoot.ServiceTypeDesignationModel = ServiceTypeDesignationModel;
}(window.IPMSROOT));

IPMSROOT.ServiceTypeDesignationModel.prototype.set = function (data) {   
    var self = this;

    self.ServiceTypeDesignationID(data ? (data.ServiceTypeDesignationID == null ? "" : data.ServiceTypeDesignationID || "") : "");
    self.ServiceTypeID(data ? (data.ServiceTypeID == null ? "" : data.ServiceTypeID || "") : "");
    self.PortCode(data ? (data.PortCode == null ? "" : data.PortCode || "") : "");
    self.PortName(data ? (data.PortCode == null ? "" : data.PortName || "") : "");
    self.CraftName(data ? (data.PortCode == null ? "" : data.CraftName || "") : "");
    self.IsCraft(data ? data.IsCraft : false);
    self.ServiceTypeName(data ? (data.ServiceTypeName == null ? "" : data.ServiceTypeName || "") : "");
    self.ServiceTypeCode(data ? (data.ServiceTypeCode == null ? "" : data.ServiceTypeCode || "") : "");
    self.DesignationCode(data ? (data.DesignationCode == null ? "" : data.DesignationCode || "") : "");
    self.DesignationName(data ? (data.DesignationName == null ? "" : data.DesignationName || "") : "");
    self.CraftType(data ? (data.CraftType == null ? "" : data.CraftType || "") : "");

    self.ServiceTypeDesignations(data ? ko.utils.arrayMap(data.ServiceTypeDesignations, function (data) {
        return new IPMSROOT.ServiceTypeDesignation(data);
    }) : []);

    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == null ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == null ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy == null ? "" : data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate == null ? "" : data.ModifiedDate || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.ServiceTypeDesignationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



