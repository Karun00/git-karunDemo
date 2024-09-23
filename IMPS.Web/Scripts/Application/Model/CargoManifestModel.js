(function (ipmsRoot) {

    var CargoManifestModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.FirstMoveDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select the First Move Date Time.' } });
        self.LastMoveDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select the Last Move Date Time.' } });
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.CargoCommodity = ko.observable();
        self.VCN = ko.observable("");
        self.CargoManifestID = ko.observable("");
        self.VesselName = ko.observable("");
        self.VesselType = ko.observable("");
        self.Agent = ko.observable("");
        self.LengthOverallInM = ko.observable("");
        self.MaxDraft = ko.observable("");
        self.IMDG = ko.observable("");
        self.NominationDate = ko.observable("");
        self.ETA = ko.observable("");
        self.ETD = ko.observable("");
        self.PreferredBerth = ko.observable("");
        self.AlternateBerth = ko.observable("");
        self.ReasonforVisit = ko.observable("");
        self.ETB = ko.observable("");
        self.ETUB = ko.observable("");
        self.Berth = ko.observable("");
        self.ATA = ko.observable();
        self.ATD = ko.observable();
        self.CargoManifests = ko.observableArray([]);

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.Status = ko.computed(function () {
            if (self.CargoManifestID() > 0) {
                return "Completed";
            }
            else {
                return "Pending";
            }
        });

        self.VCN;
        self.VCN.subscribe(function (value) {
            self.VCN = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.VesselTypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSort = value;
        });

        self.AgentSort;
        self.Agent.subscribe(function (value) {
            self.AgentSort = value;
        });

        self.LOASort;
        self.LengthOverallInM.subscribe(function (value) {
            self.LOASort = parseInt(value);
        });

        self.MaxDraftSort;
        self.MaxDraft.subscribe(function (value) {
            self.MaxDraftSort = parseInt(value);
        });

        self.IMDGt = ko.computed(function () {
            var IMDG = '';
            if (self.IMDG() == 'I') {
                IMDG = "No";
            }
            else if (self.IMDG() == 'A') {
                IMDG = "Yes";
            }
            return self.IMDGt = IMDG;
        });
        self.IMDGSort;
        self.IMDGt.subscribe(function (value) {
            self.IMDGSort = value;
        });

        self.IDReceivSort;
        self.NominationDate.subscribe(function (value) {
            self.IDReceivSort = kendo.parseDate(value);
        });

        self.ETASort;
        self.ETA.subscribe(function (value) {
            self.ETASort = kendo.parseDate(value);
        });

        self.ETDSort;
        self.ETD.subscribe(function (value) {
            self.ETDSort = kendo.parseDate(value);
        });

        self.PrefferedBerthSort;
        self.PreferredBerth.subscribe(function (value) {
            self.PrefferedBerthSort = value;
        });

        self.AltBerthSort;
        self.AlternateBerth.subscribe(function (value) {
            self.AltBerthSort = value;
        });

        self.ReasonvisitSort;
        self.ReasonforVisit.subscribe(function (value) {
            self.ReasonvisitSort = value;
        });

        self.ETBSort;
        self.ETB.subscribe(function (value) {
            self.ETBSort = kendo.parseDate(value);
        });

        self.ETUBSort;
        self.ETUB.subscribe(function (value) {
            self.ETUBSort = kendo.parseDate(value);
        });

        self.BerthSort;
        self.Berth.subscribe(function (value) {
            self.BerthSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    var CargoCommodity = function (data) {
        var self = this;
        self.VCN = ko.observable(data ? data.VCN : "");
        self.CargoTypeCode = ko.observable(data ? data.CargoTypeCode : "");
        self.CargoTypeName = ko.observable(data ? data.CargoTypeName : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.UOMCode = ko.observable(data ? data.UOMCode : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.OutTurn = ko.observable(data ? data.OutTurn : "");
        self.CargoManifestDtlID = ko.observable(data ? data.CargoManifestDtlID : "");
        self.CargoManifestID = ko.observable(data ? data.CargoManifestID : "");
        self.RecordStatus = ko.observable(data ? (data.RecordStatus || 'A') : 'A');
        self.CreatedBy = ko.observable(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
        self.CreatedDate = ko.observable(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
        self.ModifiedBy = ko.observable(data ? (data.ModifiedBy == 'NULL' ? "" : data.ModifiedBy || "") : "");
        self.ModifiedDate = ko.observable(data ? (data.ModifiedDate == 'NULL' ? "" : data.ModifiedDate || "") : "");
        self.cache = function () { };
    }

    ipmsRoot.CargoCommodity = CargoCommodity;
    ipmsRoot.CargoManifestModel = CargoManifestModel;
}(window.IPMSROOT));

IPMSROOT.CargoManifestModel.prototype.set = function (data) {
    var self = this;
    self.FirstMoveDateTime(data ? (moment(data.FirstMoveDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
    self.LastMoveDateTime(data ? (moment(data.LastMoveDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
    self.VCN(data ? (data.VCN || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.Agent(data ? (data.Agent || "") : "");
    self.LengthOverallInM(data ? (data.LengthOverallInM || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.MaxDraft(data ? (data.MaxDraft || "") : "");
    self.IMDG(data ? (data.IMDG || "") : "");
    self.NominationDate(data ? (data.NominationDate == 'NULL' ? "" : data.NominationDate || "") : "");
    self.ETA(data ? (data.ETA || "") : "");
    self.ETD(data ? (data.ETD || "") : "");
    self.PreferredBerth(data ? (data.PreferredBerth || "") : "");
    self.AlternateBerth(data ? (data.AlternateBerth || "") : "");
    self.ReasonforVisit(data ? (data.ReasonforVisit || "") : "");

    self.ETB(data ? (moment(data.ETB).format('YYYY-MM-DD HH:mm') || "") : "");
    if (self.ETB() == 'Invalid date')
        self.ETB('');
    self.ETUB(data ? (moment(data.ETUB).format('YYYY-MM-DD HH:mm') || "") : "");
    if (self.ETUB() == 'Invalid date')
        self.ETUB('');
    self.Berth(data ? (data.Berth || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CargoManifestID(data ? (data.CargoManifestID || "") : "");
    self.ATA(data ? (data.ATA || "") : "");
    self.ATD(data ? (data.ATD || "") : "");
    self.CargoManifests(data ? (data.CargoManifests ? $.map(data.CargoManifests, function (item) { return new IPMSROOT.CargoCommodity(item) }) : []) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.CargoManifestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}

//Accept Alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}

//Allow onlu Two Positive Digits
function ValidateDecimalValue(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/;
    return charcheck.test(keychar);
}