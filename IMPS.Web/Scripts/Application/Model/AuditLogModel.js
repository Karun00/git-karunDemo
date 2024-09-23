(function (IPMSROOT) {
    var AuditLogModel = function (data) {
        var self = this;
        self.AuditTrailID = ko.observable();
        self.UserName = ko.observable();
        self.UserFriendlyDescription = ko.observable();
        self.EntryOrExit = ko.observable();
        self.ControlerName = ko.observable();
        self.ActionName = ko.observable();
        self.AuditDateTime = ko.observable();
        self.UserIPAddress = ko.observable();
        self.UserComputerName = ko.observable();
        var AuditFromDateTime = new Date()
        AuditFromDateTime.setHours(AuditFromDateTime.getHours() - 1)
        self.AuditFromDateTime = ko.observable(AuditFromDateTime);
      
        self.AuditToDateTime = ko.observable(new Date());

        //////////////// KendoUI Grid Sorting/Filtering /////////
        self.UserNameSort;
        self.UserName.subscribe(function (value) {
            self.UserNameSort = value;
        });

        self.EntryOrExitSort;
        self.EntryOrExit.subscribe(function (value) {
            self.EntryOrExitSort = value;
        });

        self.UserIPAddressSort;
        self.UserIPAddress.subscribe(function (value) {
            self.UserIPAddressSort = value;
        });

        self.ControlerNameSort;
        self.ControlerName.subscribe(function (value) {
            self.ControlerNameSort = value;
        });

        self.ActionNameSort;
        self.ActionName.subscribe(function (value) {
            self.ActionNameSort = value;
        });

        self.UserFriendlyDescriptionSort;
        self.UserFriendlyDescription.subscribe(function (value) {
            self.UserFriendlyDescriptionSort = value;
        });

        self.AuditDateTimeSort;
        self.AuditDateTime.subscribe(function (value) {
            self.AuditDateTimeSort = kendo.parseDate(value);
        });

        self.UserComputerNameSort;
        self.UserComputerName.subscribe(function (value) {
            self.UserComputerNameSort = value;
        });

        ///////////////////////////////////////////////////

        self.cache = function () { };
        self.set(data);
    }
    IPMSROOT.AuditLogModel = AuditLogModel;
}(window.IPMSROOT));

IPMSROOT.AuditLogModel.prototype.set = function (data) {
    var self = this;
    self.AuditTrailID(data ? (data.AuditTrailID) || 0 : 0);
    self.UserName(data ? (data.UserName == 'NULL' ? "" : data.UserName || "") : "");
    self.EntryOrExit(data ? (data.EntryOrExit == 'NULL' ? "" : data.EntryOrExit || "") : "");
    self.ControlerName(data ? (data.ControlerName == 'NULL' ? "" : data.ControlerName || "") : "");
    self.ActionName(data ? (data.ActionName == 'NULL' ? "" : data.ActionName || "") : "");
    self.AuditDateTime(data ? (data.AuditDateTime == 'NULL' ? "" : moment(data.AuditDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.UserFriendlyDescription(data ? (data.UserFriendlyDescription == 'NULL' ? "" : data.UserFriendlyDescription || "") : "");
    self.UserIPAddress(data ? (data.UserIPAddress == 'NULL' ? "" : data.UserIPAddress || "") : "");
    self.UserComputerName(data ? (data.UserComputerName == 'NULL' ? "" : data.UserComputerName || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.AuditLogModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


