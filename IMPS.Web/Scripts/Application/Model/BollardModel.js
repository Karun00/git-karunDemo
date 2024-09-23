(function (ipmsRoot) {

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
    ko.validation.registerExtenders();
    var BollardModel = function (data) {
        var self = this;
        //     self.BollardID = ko.observable("");

        self.validationHelper = new IPMSROOT.validationHelper();
        self.BollardCode = ko.observable("").extend({ required: true }); //{ message: '* Bollard Code is required' } });
        self.BollardName = ko.observable("").extend({ required: true }); //{ message: '* Bollard Name is required' }});
        self.ShortName = ko.observable("").extend({ required: true }); //{ message: '* Short Name is required' }});
        self.PortName = ko.observable();
        self.QuayName = ko.observable();
        self.QuayLength = ko.observable();
        self.BerthLength = ko.observable();
        self.BerthName = ko.observable();
        self.PortCode = ko.observable("").extend({ required: true }); //{message:'*Please Select Port Name'}});

        self.QuayCode = ko.observable("").extend({ required: true }); //{ message: '* Please Select the Quay Name' } });
        self.BerthCode = ko.observable("").extend({ required: true }); //{ message: '* Please Select the Berth Name' } });
        self.FromMeter = ko.observable("").extend({ required: true }); //{ message: '* From Meter is required' }});
        self.ToMeter = ko.observable("").extend({ required: true }); //{ message: '* To Meter is required' }});
        self.Continous = ko.observable('N');
        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.Description = ko.observable("");

        self.ContinousStatus = ko.observable(false);

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        ////////////////KendoUI Grid Sorting/////////

        self.BollardCodeSort;
        self.BollardCode.subscribe(function (value) {
            self.BollardCodeSort = value;
        });
        self.BollardNameSort;
        self.BollardName.subscribe(function (value) {
            self.BollardNameSort = value;
        });
        self.ShortNameSort;
        self.ShortName.subscribe(function (value) {
            self.ShortNameSort = value;
        });
        self.PortNameSort;
        self.PortName.subscribe(function (value) {
            self.PortNameSort = value;
        });
        self.QuayNameSort;
        self.QuayName.subscribe(function (value) {
            self.QuayNameSort = value;
        });
        self.BerthNameSort;
        self.BerthName.subscribe(function (value) {
            self.BerthNameSort = value;
        });
        self.FromMeterSort;
        self.FromMeter.subscribe(function (value) {
            self.FromMeterSort = parseInt(value);
        });
        self.ToMeterSort;
        self.ToMeter.subscribe(function (value) {
            self.ToMeterSort = parseInt(value);
        });

        ///////////////////////////////////////////////////



        self.cache = function () { };
        self.set(data);

    };
    IPMSROOT.BollardModel = BollardModel;
}(window.IPMSROOT));


function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}
IPMSROOT.BollardModel.prototype.set = function (data) {
    var self = this;
    //  self.BollardID(data ? (data.BollardID || 0) : 0);
    self.BollardCode(data ? (data.BollardCode || "") : "");
    self.BollardName(data ? (data.BollardName == 'NULL' ? "" : data.BollardName || "") : "");
    self.ShortName(data ? (data.ShortName == 'NULL' ? "" : data.ShortName || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
    self.QuayName(data ? (data.QuayName || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.QuayCode(data ? (data.QuayCode || "") : "");
    self.QuayLength(data ? (data.QuayLength || "") : "");
    self.BerthLength(data ? (data.BerthLength || "") : "");
    self.BerthCode(data ? (data.BerthCode || "") : "");
    self.FromMeter(data ? (data.FromMeter == 0 ? 0 : data.FromMeter || "") : "");
    self.ToMeter(data ? (data.ToMeter == 0 ? 0 : data.ToMeter || "") : "");
    self.Continous(data ? (data.Continous || "") : "");
    self.Description(data ? (data.Description || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.ContinousStatus(data ? (data.ContinousStatus || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.BollardModel.prototype.reset = function () {


    this.set(this.cache.latestData);
}




