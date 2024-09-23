(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })
    var AutomatedSlotConfigReferenceData = function (data) {
        var self = this;       
        self.VesselType = ko.observableArray(data ? $.map(data.VesselType, function (item) { return new Vessel(item); }) : []);
        self.PrioprtySeqList = ko.observableArray(data ? $.map(data.PrioprtySeqList, function (item) { return new PrioprtySeq(item); }) : []);
        
    }
    var Vessel = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var PrioprtySeq = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
    }

    var AutomatedSlotConfigurationModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(true);
        self.SlotCofiguratinid = ko.observable("");       
        self.Duration = ko.observable(data ? data.Duration : "").extend({ required: true });
        self.EffectiveFrm = ko.observable(data ? data.EffectiveFrm : "").extend({ required: true }); 
        self.NoofSlots = ko.observable(data ? data.NoofSlots : "").extend({ required: true });
        self.OperationalPeriod1 = ko.observable(data ? data.OperationalPeriod1 : "").extend({ required: true });
        self.ExtendableSlots = ko.observable(data ? data.ExtendableSlots : "").extend({ required: true });
        self.PortCode = ko.observable(data ? data.PortCode : ""); 
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.OperationalTime = ko.observable("");

        self.SlotPriorityConfigurations = ko.observableArray(data ? ko.utils.arrayMap(data.SlotPriorityConfigurations, function (slotpriorityconfiguration) {
            return new SlotPriorityConfiguration(slotpriorityconfiguration);
        }) : []);

        self.CurrentDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.IsEdit = ko.computed(function () {
            if (moment(self.EffectiveFrm()).format('YYYY-MM-DD') <= moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                return false;
            }
            else {
                return true;
            }
        });

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });


        self.EffectiveFrmSort;
        self.EffectiveFrm.subscribe(function (value) {
            self.EffectiveFrmSort = value;
        });
        self.DurationSort;
        self.Duration.subscribe(function (value) {
            self.DurationSort = value;
        });
        self.NoofSlotsSort;
        self.NoofSlots.subscribe(function (value) {
            self.NoofSlotsSort = value;
        });
        self.ExtendableSlotsSort;
        self.ExtendableSlots.subscribe(function (value) {
            self.ExtendableSlotsSort = value;
        });  

        self.cache = function () { };
        self.set(data);
    }
    var SlotPriorityConfiguration = function (data) {
        var self = this;
        self.vesseltype = ko.observable(data ? data.VesselType : "");
        self.Priority = ko.observable(data ? data.Priority : "");
        self.NoofVessels = ko.observable(data ? data.NoofVessels : "");
    }


    ipmsRoot.AutomatedSlotConfigurationModel = AutomatedSlotConfigurationModel;
    ipmsRoot.SlotPriorityConfiguration = SlotPriorityConfiguration;
    ipmsRoot.AutomatedSlotConfigReferenceData = AutomatedSlotConfigReferenceData;

    ipmsRoot.PrioprtySeq = PrioprtySeq;
    ipmsRoot.Vessel = Vessel;
    

}(window.IPMSROOT));

IPMSROOT.AutomatedSlotConfigurationModel.prototype.set = function (data) {
    var self = this;

    self.SlotCofiguratinid(data ? (data.SlotCofiguratinid) || 0 : 0);
    self.Duration(data ? (data.Duration || "") : "");
    self.OperationalTime(data ? (data.OperationalPeriod1 || "") : "");
    self.OperationalPeriod1(data ? (moment(data.OperationalPeriod1).format('HH:mm') || "") : "");
    self.EffectiveFrm(data ? (data.EffectiveFrm || "") : "");
    self.NoofSlots(data ? (data.NoofSlots || "") : "");
    self.ExtendableSlots(data ? (data.ExtendableSlots || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    //self.slotpriorityconfiguration(data ? (data.SlotPriorityConfiguration ? $.map(data.SlotPriorityConfiguration, function (item) { return item }) : []) : []);
    self.cache.latestData = data;
}

IPMSROOT.AutomatedSlotConfigurationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}