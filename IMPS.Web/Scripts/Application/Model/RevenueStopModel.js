(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var RevenueStopListReferenceData = function (data) {
        var self = this;
        self.RevenueAccountStatus = ko.observableArray(data ? $.map(data.RevenueAccountStatus, function (item) { return new revenueaccountstatus(item); }) : []);
    }
    var revenueaccountstatus = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : undefined);
        self.SubCatName = ko.observable(data ? data.SubCatName : undefined);
    }

    //  RevenueStopModel Used For bind The All controls from view model
    var RevenueStopModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(false);
        self.RevenueStopListID = ko.observable();
        self.RevenueAccountStatusID = ko.observable();
        self.AgentID = ko.observable().extend({ required: true });
        self.AgentAccountID = ko.observable();
        self.PortCode = ko.observable();
        self.AgentCode = ko.observable();
        self.ReferenceNo = ko.observable();
        self.AccountNo = ko.observable();
        self.RegisteredName = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: ' Please Select Agent Name' } });
        self.StopDate = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: ' Please Select Stop Date' } });
        self.RecordStatus = ko.observable();
        self.RegistrationNumber = ko.observable();
        self.startDate = ko.observable();
        self.AccountStatus = ko.observable();
        self.AccountStatusName = ko.observable();
        self.toggleAssociation = function (item) {
            self.AccountStatus(item.SubCatCode());
            return true;
        };
        self.AgentIDSort;
        self.RegistrationNumber.subscribe(function (value) {
            self.AgentIDSort = value;
        });
        self.AccountNoSort;
        self.AccountNo.subscribe(function (value) {
            self.AccountNoSort = value;
        });
        self.RegisteredNameSort;
        self.RegisteredName.subscribe(function (value) {
            self.RegisteredNameSort = value;
        });
        self.AccountStatusNameSort;
        self.AccountStatusName.subscribe(function (value) {
            self.AccountStatusNameSort = value;
        });
        self.StopDateSort;
        self.StopDate.subscribe(function (value) {
            self.StopDateSort = value;
        });



        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.RevenueStopListReferenceData = RevenueStopListReferenceData;
    ipmsRoot.RevenueStopModel = RevenueStopModel;
}(window.IPMSROOT));

IPMSROOT.RevenueStopModel.prototype.set = function (data) {
    var self = this;

    self.RevenueStopListID(data ? (data.RevenueStopListID || "") : "");
    self.RevenueAccountStatusID(data ? (data.RevenueAccountStatusID || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.AgentCode(data ? (data.AgentCode || "") : "");
    self.AgentID(data ? (data.AgentID || "") : "");
    self.AgentAccountID(data ? (data.AgentAccountID || "") : "");
    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.RegistrationNumber(data ? (data.RegistrationNumber || "") : "");
    self.AccountNo(data ? (data.AccountNo || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.StopDate(data ? (moment(data.StopDate).format('YYYY-MM-DD') || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.startDate(data ? (data.startDate || "") : "");
    self.AccountStatus(data ? (data.AccountStatus || "") : "");
    self.AccountStatusName(data ? (data.AccountStatusName || "") : "");
    //self.AccountStatus(data ? (data.AccountStatus ? $.map(data.AccountStatus, function (item) { return item }) : []) : []);
    self.cache.latestData = data;
}

IPMSROOT.RevenueStopModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

//To validating the date
function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}




