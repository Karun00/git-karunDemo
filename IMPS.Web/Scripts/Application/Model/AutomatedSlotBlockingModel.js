(function (ipmsRoot) {

    var AutomatedReferenceData = function (data) {
        var self = this;
        self.Reasons = ko.observableArray(data ? $.map(data.Reasons, function (item) { return new ReasonType(item); }) : []);
        self.AllSlots = ko.observableArray(data ? $.map(data.Slots, function (item) { return new SlotType(item); }) : []);
        self.SlotTypes = ko.observableArray(data ? $.map(data.Slots, function (item) { return new SlotType(item); }) : []);
    }

    var ReasonType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var SlotType = function (data) {
        var self = this;
        self.SlotPeriod = ko.observable(data ? data.SlotPeriod : "");
        self.SlotNumber = ko.observable(data ? data.SlotNumber : "");
        self.Slot = ko.observable(data ? data.Slot : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.EndTime = ko.observable(data ? data.EndTime : "");
        self.SlotTime = ko.observable(data ? data.SlotPeriod.substring(0, 5) : "");

    }

    var AutomatedSlotBlockingModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.AutomatedSlotBlockingId = ko.observable("");
        self.FromDate = ko.observable("").extend({ required: true });
        self.ToDate = ko.observable("");
        self.SlotPeriod = ko.observable("");
        self.SlotFrom = ko.observable("").extend({ required: true });
        self.SlotTo = ko.observable("").extend({ required: true });
        self.StartTime = ko.observable("");
        self.ToStartTime = ko.observable("");
        self.SlotDateFrom = ko.observable("");
        self.SlotDateTo = ko.observable("");
        self.Reason = ko.observable("").extend({ required: true });
        self.Remarks = ko.observable("");
        self.Other = ko.observable("");

        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.TotalSlots = ko.observable("");
        self.EditVisible = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        
        self.isEditVisible = ko.computed(function () {
            
            if (self.EditVisible() == true) {
                return true;
            }
            else {
                return false;
            }
        }, this);

       
        self.DateSort;
        self.FromDate.subscribe(function (value) {
            self.DateSort = value;
        });
        self.SlotFromSort;
        self.SlotDateFrom.subscribe(function (value) {
            self.SlotFromSort = value;
        });
        self.SlotToSort;
        self.SlotDateTo.subscribe(function (value) {
            self.SlotToSort = value;
        });

  
        self.cache = function () { };
        self.set(data);
    }



    ipmsRoot.AutomatedSlotBlockingModel = AutomatedSlotBlockingModel;
    ipmsRoot.AutomatedReferenceData = AutomatedReferenceData;
    ipmsRoot.ReasonType = ReasonType;
    ipmsRoot.SlotType = SlotType;
    
    
}(window.IPMSROOT));

IPMSROOT.AutomatedSlotBlockingModel.prototype.set = function (data) {
    var self = this;
   
    self.AutomatedSlotBlockingId(data ? (data.AutomatedSlotBlockingId || "") : "");
    self.FromDate(data ? moment(data.FromDate).format('YYYY-MM-DD') || "" : "");
    self.ToDate(data ? data.ToDate || "" : "");
    self.SlotPeriod(data ? (data.SlotPeriod || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.SlotFrom(data ? (data.SlotFrom || "") : "");
    self.SlotTo(data ? (data.SlotTo || "") : "");
    self.TotalSlots(data ? (data.TotalSlots || "") : "");    
    self.Reason(data ? (data.Reason || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.SlotDateFrom(data ? (data.SlotFrom.substring(0, 5) || "") : "");
    self.SlotDateTo(data ? (data.SlotTo.substring(0, 5) || "") : "");
    self.StartTime(data ? (data.StartTime || "") : "");
    self.ToStartTime(data ? (data.ToStartTime || "") : "");
    self.EditVisible(data ? (data.EditVisible || "") : "");
    self.Other(data ? (data.Other || "") : "");    
    
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.AutomatedSlotBlockingModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

