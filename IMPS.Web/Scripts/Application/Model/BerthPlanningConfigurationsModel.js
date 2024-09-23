(function (ipmsRoot) {

    var BerthPlanningConfigurationsModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.PortCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select the Port Name' } });
        self.Days = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please select Days' } });
        self.Slot = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please select Slot' } });
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.BerthPlanConfigid = ko.observable();
        self.PortName = ko.observable("");

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "In Active";
        });
        
        self.DaysSort;
        self.Days.subscribe(function (value) { self.DaysSort = value; });
        self.SlotSort;
        self.Slot.subscribe(function (value) { self.SlotSort = value; });
        self.PortCodeSort;
        self.PortCode.subscribe(function (value) { self.PortCodeSort = value; });
      
        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.BerthPlanningConfigurationsModel = BerthPlanningConfigurationsModel;
}(window.IPMSROOT));

IPMSROOT.BerthPlanningConfigurationsModel.prototype.set = function (data) {
    var self = this;
    self.BerthPlanConfigid(data ? (data.BerthPlanConfigid || "") : "");
    self.PortName(data ? (data.PortName == 'NULL' ? "" : data.PortName || "") : "");
    self.Days(data ? (data.Days || "") : "");
    self.Slot(data ? (data.Slot || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.BerthPlanningConfigurationsModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-5]/;

    return charcheck.test(keychar);
}
