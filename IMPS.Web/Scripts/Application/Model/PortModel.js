(function (ipmsRoot) {
    var PortModel = function (data) {
        var self = this;

        var validationMessage = '* This field is required.';
        self.validationEnabled = ko.observable(true);
        self.PortCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage } });
        self.PortName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage } });
        self.InternationalCharacter = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage } });
        self.GeographicLocation = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage } });
        self.ContactNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage } });
        self.Email = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessage }, email: { onlyIf: self.validationEnabled, message: '* Please enter valid email address.' }, });
        self.Fax = ko.observable();

        self.Website = ko.observable("").extend({
            pattern: {
                onlyIf: self.validationEnabled,
                message: '* Please enter valid website url ',
                params: /^\s*www\.[a-z\d\-]{1,255}\.[a-z]{2,6}\s*$/,
            }
        });

        self.Description = ko.observable();
        self.CreatedBy = ko.observable('1');
        self.CreatedDate = ko.observable(GetDateTime());
        self.RecordStatus = ko.observable('A');

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.PortCodeSort;
        self.PortCode.subscribe(function (value) {
            self.PortCodeSort = value;
        });

        self.PortNameSort;
        self.PortName.subscribe(function (value) {
            self.PortNameSort = value;
        });

        self.InternationalCharacterSort;
        self.InternationalCharacter.subscribe(function (value) {
            self.InternationalCharacterSort = value;
        });

        self.GeographicLocationSort;
        self.GeographicLocation.subscribe(function (value) {
            self.GeographicLocationSort = value;
        });

        self.ContactNoSort;
        self.ContactNo.subscribe(function (value) {
            self.ContactNoSort = value;
        });

        self.EmailSort;
        self.Email.subscribe(function (value) {
            self.EmailSort = value;
        });

        self.FaxSort;
        self.Fax.subscribe(function (value) {
            self.FaxSort = value;
        });

        self.StatusSort;
        self.RecordStatus.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.cache = function () { };
        self.set(data);
    };

    ipmsRoot.PortModel = PortModel;
}(window.IPMSROOT));

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

IPMSROOT.PortModel.prototype.set = function (data) {
    var self = this;

    self.PortCode(data ? (data.PortCode || "") : "");
    self.PortName(data ? (data.PortName == 'NULL' ? "" : data.PortName || "") : "");
    self.InternationalCharacter(data ? (data.InternationalCharacter == 'NULL' ? "" : data.InternationalCharacter || "") : "");
    self.GeographicLocation(data ? (data.GeographicLocation == 'NULL' ? "" : data.GeographicLocation || "") : "");
    self.ContactNo(data ? (data.ContactNo || "") : "");
    //self.ContactNo(data ? (data.ContactNo == 'NULL' ? "" : data.ContactNo || "") : "");
    self.Email(data ? (data.Email == 'NULL' ? "" : data.Email || "") : "");
    self.Fax(data ? (data.Fax || "") : "");
    self.Website(data ? (data.Website == 'NULL' ? "" : data.Website || "") : "");
    self.Description(data ? (data.Description == 'NULL' ? "" : data.Description || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.PortModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

function ValidateWebsite(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9a-zA-Z.]/;
    return charcheck.test(keychar);
}



