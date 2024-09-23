(function (ipmsRoot) {
    var BerthModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);

        self.BerthCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.BerthName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.ShortName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.PortCode = ko.observable("").extend({ required: true });
        self.PortName = ko.observable("");
        self.QuayCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.QuayName = ko.observable("");
        self.QuayLength = ko.observable();

        self.BerthType = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.FromMeter = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.ToMeter = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.Lengthm = ko.observable("").extend({ number: true });
        self.Draftm = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.TidalDraft = ko.observable("");

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.CargoType = ko.observableArray([]);
        self.VesselType = ko.observableArray([]);
        self.ReasonForVisitType = ko.observableArray([]);      
        
        self.IsLength = ko.observable("");

        //self.IsLength = ko.computed(function () {
        //    if (parseInt(self.ToMeter()) > 0 && parseInt(self.FromMeter()) >= 0)
        //        return parseInt(self.ToMeter()) - parseInt(self.FromMeter());
        //});

        self.IsLength = ko.computed(function () {
            if (parseFloat(self.ToMeter()) > 0 && parseFloat(self.FromMeter()) >= 0)
                return parseFloat(self.ToMeter()) - parseFloat(self.FromMeter());
        });

        self.SubCatCode = ko.observable();
        self.SubCatName = ko.observable("");
        self.BerthTypeName = ko.observable("");
        
        self.BerthCodeSort;
        self.BerthCode.subscribe(function (value) {
            self.BerthCodeSort = value;
        });

        self.BerthNameSort;
        self.BerthName.subscribe(function (value) {
            self.BerthNameSort = value;
        });

        self.BerthTypeSort;
        self.BerthTypeName.subscribe(function (value) {
            self.BerthTypeSort = value;
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

        self.FromMeterSort;
        self.FromMeter.subscribe(function (value) {

            self.FromMeterSort = value.toString();

        });

        self.ToMeterSort;
        self.ToMeter.subscribe(function (value) {
            self.ToMeterSort = value.toString();
        });

        self.LengthMSort;
        self.Lengthm.subscribe(function (value) {
            self.LengthMSort = value.toString();
        });

        self.DraftMSort;
        self.Draftm.subscribe(function (value) {
            self.DraftMSort = value.toString();
        });

        self.cache = function () { };
        self.set(data);

    }
    ipmsRoot.BerthModel = BerthModel;
}(window.IPMSROOT));

IPMSROOT.BerthModel.prototype.set = function (data) {
    var self = this;

    self.BerthCode(data ? (data.BerthCode || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.ShortName(data ? (data.ShortName || "") : "");
    self.PortName(data ? (data.PortName || "") : null);
    self.QuayName(data ? (data.QuayName || "") : null);
    self.PortCode(data ? (data.PortCode || "") : "");
    self.QuayCode(data ? (data.QuayCode || "") : "");
    self.BerthType(data ? (data.BerthType || "") : "");

    self.FromMeter(data ? (data.FromMeter == 0 ? 0 : data.FromMeter || "") : "");
    self.ToMeter(data ? (data.ToMeter == 0 ? 0 : data.ToMeter || "") : "");
    self.Lengthm(data ? (data.Lengthm == 0 ? 0 : data.Lengthm || "") : "");
    self.Draftm(data ? (data.Draftm == 0 ? 0 : data.Draftm || "") : "");
    self.BerthTypeName(data ? (data.BerthTypeName || "") : null);
    self.QuayLength(data ? (data.QuayLength || "") : "");

    self.TidalDraft(data ? (data.TidalDraft == 0 ? 0 : data.TidalDraft || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");

    self.CargoType(data ? (data.CargoType || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.ReasonForVisitType(data ? (data.ReasonForVisitType || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.BerthModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

function ValidateNumericDecimal(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.]/;

    return charcheck.test(keychar);
}




