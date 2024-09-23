(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var ReferenceData = function (data) {
        var self = this;
        self.RACrulereferencedata = ko.observable();
        self.serviceTypes = ko.observableArray(data ? $.map(data.ServiceTypes, function (item) { return new serviceTypes(item); }) : []);
        self.pilotcapacities = ko.observableArray(data ? $.map(data.PilotCapacity, function (item) { return new pilotcapacities(item); }) : []);
    }

    var pilotcapacities = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var serviceTypes = function (data) {
        var self = this;
        self.ServiceTypeID = ko.observable(data ? data.ServiceTypeID : "");
        self.ServiceTypeName = ko.observable(data ? data.ServiceTypeName : "");
    }

    var ResourceGangConfigsDetails = function (data) {
        var self = this;

        self.ResourceGangConfigID = ko.observable(data ? data.ResourceGangConfigID : "");
        self.ResourceAllocationConfigRuleID = ko.observable(data ? data.ResourceAllocationConfigRuleID : "");
        self.FromMeter = ko.observable(data ? data.FromMeter : "");
        self.ToMeter = ko.observable(data ? data.ToMeter : "");
        self.NoOfGangs = ko.observable(data ? data.NoOfGangs : "");
    }

    var ResourceAllocationConfigRuleModel = function (data) {
        var self = this;

        self.ResourceAllocationConfigRuleID = ko.observable();
        self.PortCode = ko.observable("");
        self.PilotCapacity = ko.observable("");
        self.TotalTugs = ko.observable("").extend({ required: { message: '* This field is required' } });
        self.EffectedFrom = ko.observable("").extend({ required: { message: '* This field is required' } });
        self.RecordStatus = ko.observable('A');
        self.ResourceGangConfigsVO = ko.observableArray(data ? ko.utils.arrayMap(data.ResourceGangConfigsVO, function (resourceGangConfigsDetail) {
            return new ResourceGangConfigsDetails(resourceGangConfigsDetail);
        }) : []);
        self.arrivalservicetype = ko.observableArray();
        self.shiftingservicetype = ko.observableArray();
        self.sailingservicetype = ko.observableArray();
        self.warpingservicetype = ko.observableArray();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.PilotCapacitySort;
        self.PilotCapacity.subscribe(function (value) {
            self.PilotCapacitySort = value;
        });

        self.TotalTugsSort;
        self.TotalTugs.subscribe(function (value) {
            self.TotalTugsSort = value;
        });

        self.CurrentDate = ko.observable(new Date()).extend({ isoDate: 'dd/mm/yyyy' });

        self.IsEdit = ko.computed(function () {
            if (moment(self.EffectedFrom()).format('YYYY-MM-DD') <= moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                return false;
            }
            else {
                return true;
            }
        });
        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.serviceTypes = serviceTypes;
    ipmsRoot.pilotcapacities = pilotcapacities;
    ipmsRoot.ResourceGangConfigsDetails = ResourceGangConfigsDetails;
    ipmsRoot.ResourceAllocationConfigRuleModel = ResourceAllocationConfigRuleModel;

}(window.IPMSROOT));

IPMSROOT.ResourceAllocationConfigRuleModel.prototype.set = function (data) {
    var self = this;

    self.ResourceAllocationConfigRuleID(data ? (data.ResourceAllocationConfigRuleID || "0") : "0");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PilotCapacity(data ? (data.PilotCapacity || "") : "");
    self.TotalTugs(data ? (data.TotalTugs || "") : "");
    self.EffectedFrom(data ? (data.EffectedFrom || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.arrivalservicetype(data ? (data.arrivalservicetype ? $.map(data.arrivalservicetype, function (item) { return item }) : []) : []);
    self.shiftingservicetype(data ? (data.shiftingservicetype ? $.map(data.shiftingservicetype, function (item) { return item }) : []) : []);
    self.sailingservicetype(data ? (data.sailingservicetype ? $.map(data.sailingservicetype, function (item) { return item }) : []) : []);
    self.warpingservicetype(data ? (data.warpingservicetype ? $.map(data.warpingservicetype, function (item) { return item }) : []) : []);
    self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
    self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
    self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
    self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
    self.cache.latestData = data;
}
IPMSROOT.ResourceAllocationConfigRuleModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function ValidateNumeric(data,event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.]/;

    return charcheck.test(keychar);
}