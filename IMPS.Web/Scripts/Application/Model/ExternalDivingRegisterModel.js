(function (ipmsRoot) {

    var ReferenceData = function (data) {
        var self = this;

        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);       
    }

    var Berth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");        
        self.BerthKey = ko.observable(data ? data.BerthKey : "");        
    }


    var ExternalDivingRegisterModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.ExternalDivingRegisterID = ko.observable("");
        self.DivingLogDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select a Diving Log Date Time.' } });
        self.CompanyName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select a Name of Company.' } });
        self.BerthKey = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select a Location of Dive.' } });
        //self.VesselID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Name of Vessel.' } });
        self.VesselID = ko.observable("");
        self.PersonInCharge = ko.observable("").extend({ required: true });
        self.StartTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select a Start Time.' } });
        self.OnsiteSupervisorContNo = ko.observable("").extend({ required: true });
        self.OffsiteSupervisorContNo = ko.observable("").extend({ required: true });
        self.ClearanceNo = ko.observable("").extend({ required: true });
        self.NoOfDrivers = ko.observable("").extend({ required: true });
        self.PermissionObtained = ko.observable("");
        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.isPermissionObtained = ko.observable();
        self.CompanyNameDisplay = ko.observable("");
        self.BerthName = ko.observable("");
        self.VesselName = ko.observable("");     
                
        self.StopTime = ko.observable("");
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.PermissionObtainedt = ko.computed(function () {
            return self.PermissionObtained() == 'N' ? "No" : "Yes";
        });

        // --> Sorting fields
        //CompanyNameDisplay
        self.CompanyNameDisplaySort;
        self.CompanyNameDisplay.subscribe(function (value) {
            self.CompanyNameDisplaySort = value;
        });

        //DivingLogDateTime 
        self.DivingLogDateTimeSort;
        self.DivingLogDateTime.subscribe(function (value) {
            self.DivingLogDateTimeSort = value;
        });
        //LocationName 
        self.LocationNameSort;
        self.BerthName.subscribe(function (value) {
            self.LocationNameSort = value;
        });
        //VesselName  
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        //StartTime  
        self.StartTimeSort;
        self.StartTime.subscribe(function (value) {
            self.StartTimeSort = value;
        });
        //StopTime  
        self.StopTimeSort;
        self.StopTime.subscribe(function (value) {
            self.StopTimeSort = value;
        });
        // PersonInchargeSort  
        self.PersonInChargeSort;
        self.PersonInCharge.subscribe(function (value) {
            self.PersonInChargeSort = value;
        });
        //OnsiteSupervisorContNo  
        self.OnsiteSupervisorContNoSort;
        self.OnsiteSupervisorContNo.subscribe(function (value) {
            self.OnsiteSupervisorContNoSort = value;
        });
        //ClearanceNo  
        self.ClearanceNoSort;
        self.ClearanceNo.subscribe(function (value) {
            self.ClearanceNoSort = value;
        });
        //NoOfDrivers 
        self.NoOfDriversSort;
        self.NoOfDrivers.subscribe(function (value) {
            self.NoOfDriversSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.ExternalDivingRegisterModel = ExternalDivingRegisterModel;
    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.Berth = Berth;
}(window.IPMSROOT));

IPMSROOT.ExternalDivingRegisterModel.prototype.set = function (data) {
    var self = this;

    self.ExternalDivingRegisterID(data ? (data.ExternalDivingRegisterID || "") : "");   
    self.DivingLogDateTime(data ? (data.DivingLogDateTime != null ? moment(data.DivingLogDateTime).format('YYYY-MM-DD HH:mm') : new Date()) : new Date());
    self.CompanyName(data ? (data.CompanyName || "") : "");
    self.BerthKey(data ? (data.BerthKey || "") : "");
    self.VesselID(data ? (data.VesselID || "") : "");
    self.PersonInCharge(data ? (data.PersonInCharge || "") : "");
    self.StartTime(data ? (moment(data.StartTime).format('YYYY-MM-DD HH:mm') || null) : null);

    self.StopTime(data ? ((data.StopTime != null && data.StopTime != "") ? moment(data.StopTime).format('YYYY-MM-DD HH:mm') : null || null) : null);

    self.OnsiteSupervisorContNo(data ? (data.OnsiteSupervisorContNo || "") : "");
    self.OffsiteSupervisorContNo(data ? (data.OffsiteSupervisorContNo || "") : "");
    self.ClearanceNo(data ? (data.ClearanceNo || "") : "");
    self.NoOfDrivers(data ? (data.NoOfDrivers || "") : "");
    self.PermissionObtained(data ? (data.PermissionObtained || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.isPermissionObtained(data ? (data.isPermissionObtained || "") : "");
    self.CompanyNameDisplay(data ? (data.CompanyNameDisplay || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.ExternalDivingRegisterModel.prototype.reset = function () {
    this.set(this.cache.latestData);
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
    charcheck = /^[a-zA-Z0-9]/; ///^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}