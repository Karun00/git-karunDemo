(function (ipmsRoot) {

    //ShiftModel Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ShiftModel = function (data) {
        var self = this;
        self.ShiftID = ko.observable();
        self.validationEnabled = ko.observable(false);
        self.PortCode = ko.observable();
        self.ShiftName = ko.observable("").extend({ required: true});// { onlyIf: self.validationEnabled, message: '* Shift Name is required' } });
        //self.StartTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Select the Start Time' } });
        //self.EndTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Select the End Time' } });
        self.StartTime = ko.observable();
        self.EndTime = ko.observable();
        self.IsShiftOff = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.ShiftOff = ko.observable();

        // -- Added by sandeep on 07-01-2005
        self.FirstShiftID = ko.observable();
        self.SecondShiftID = ko.observable();
        self.RollOverOn = ko.observable();
        
        self.IsContinuousShift = ko.observable();
        // -- end

        self.ShiftNameSort;
        self.ShiftName.subscribe(function (value) {
            self.ShiftNameSort = value;
        });

        self.StartTimeSort;
        self.StartTime.subscribe(function (value) {
            self.StartTimeSort = value;
        });
        self.EndTimeSort;
        self.EndTime.subscribe(function (value) {
            self.EndTimeSort = value;
        });
        self.IsShiftOffSort;
        self.ShiftOff.subscribe(function (value) {
            self.IsShiftOffSort = value;
        });
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        //self.IsShiftOff = ko.computed(function () {
        //    return self.IsShiftOff() == "Y" ? true : false;
        //});

        self.cache = function () { };
        self.set(data);
    };
    ipmsRoot.ShiftModel = ShiftModel;
}(window.IPMSROOT));

//ShiftModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.ShiftModel.prototype.set = function (data) {
   
    var self = this;
    self.ShiftID(data ? (data.ShiftID || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.ShiftName(data ? (data.ShiftName || "") : "");
    self.StartTime(data ? (data.StartTime || "") : "");
    self.EndTime(data ? (data.EndTime || "") : "");
    self.ShiftOff(data ? data.IsShiftOff : "N");
    self.IsShiftOff(data ? data.IsShiftOff == "Y" ? true : false : false);
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    // -- Added by sandeep on 07-01-2005
    self.FirstShiftID(data ? (data.FirstShiftID == 'NULL' ? "" : data.FirstShiftID || "") : "");
    self.SecondShiftID(data ? (data.SecondShiftID == 'NULL' ? "" : data.SecondShiftID || "") : "");
    self.RollOverOn(data ? (data.RollOverOn == 'NULL' ? "" : data.RollOverOn || "") : "");

    
    self.IsContinuousShift(data ? data.IsContinuousShift == "Y" ? true : false : false);
    // -- end

    self.cache.latestData = data;
}

IPMSROOT.ShiftModel.prototype.reset = function () {
   
    this.set(this.cache.latestData);
}

// This is used to validate Start time and End time
function ValidateTime(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}

// AlphaNumeric with Spaces
function ValidateAlphaNumericWithSpaces(data, event) {
    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[A-Za-z\d\s]*$/;
    return charcheck.test(keychar);
}

