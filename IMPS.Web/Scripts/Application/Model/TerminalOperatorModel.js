/// <reference path="../ViewModel/ElectronicNotificationsViewModel.js" />
(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    var PortReferenceData = function (data) {
        var self = this;
        self.PortRequesttypes = ko.observableArray(data ? $.map(data, function (item) { return new PortRequesttype(item); }) : []);
    }

    // Countries list from Vessel Nationality
    var Countries = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var PortRequesttype = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }

    //LicensingRequestModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var TerminalOperatorModel = function (data, masterQuays, masterCargoTypes) {
        var self = this;

        self.TerminalOperatorID = ko.observable();
        self.RegisteredName = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.TradingName = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.RegistrationNumber = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.RegistrationDate = ko.observable("");
        self.ValidityDate = ko.observable("");
        self.PremiseLocation = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.TelephoneNo1 = ko.observable("").extend({ required: { message: '* This field is required.' } });
        self.FaxNo = ko.observable("").extend({ required: { message: '* This field is required.' } });

        self.LicenLicensedForString = ko.observable();
        self.TerminalOperatorPortsArr = ko.observableArray();
        self.TerminalOperatorPortsNameArr = ko.observableArray();
        self.TerminalOperatorServiceTypesArray = ko.observableArray([]);

        self.PortValue = ko.observable();
        self.PortCodeValue = ko.observable();
        self.PortsArray1 = ko.observableArray([]);
        self.BusinessAddress = ko.observable();
        self.PostalAddress = ko.observable();
        self.checkedValue = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.Createdby = ko.observable(0);
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable(0);
        self.ModifiedDate = ko.observable("");
        self.IsSameBusinessAdd = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.selectedQuay = ko.observable();
        self.selectedQuayKey = ko.observable();

        self.operationalBerths = ko.observableArray([]);
        self.operationalCargotypes = ko.observableArray([]);
        self.selectedCargotype = ko.observable("");

        self.selectedCargotype.subscribe(function (key) {
            if (key !== undefined) {
                var cargotypeSelected = masterCargoTypes.filter(function (item) {
                    return item.SubCatCode() === key;
                })[0];
                self.masterCargoTypes(ko.toJS(cargotypeSelected));
            }
        });

        self.TerminalOperatorIDSort;
        self.TerminalOperatorID.subscribe(function (value) {
            self.TerminalOperatorIDSort = value;
        });

        self.RegisteredNameSort;
        self.RegisteredName.subscribe(function (value) {
            self.RegisteredNameSort = value;
        });

        self.TradingNameSort;
        self.TradingName.subscribe(function (value) {
            self.TradingNameSort = value;
        });

        self.RegistrationNumberSort;
        self.RegistrationNumber.subscribe(function (value) {
            self.RegistrationNumberSort = value;
        });

        self.RegistrationDateSort;
        self.RegistrationDate.subscribe(function (value) {
            self.RegistrationDateSort = value;
        });

        self.ValidityDateSort;
        self.ValidityDate.subscribe(function (value) {
            self.ValidityDateSort = value;
        });

        self.LicenLicensedForStringSort;
        self.LicenLicensedForString.subscribe(function (value) {
            self.LicenLicensedForStringSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    //BerthModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var Berth = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QueyCode = ko.observable(data ? data.QueyCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
    }

    //QuayModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var Quay = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayName = ko.observable(data ? data.QuayName : "");
        self.QuayKey = ko.observable(data ? data.QuayKey : "");
        self.Berths = ko.observableArray(data ? (data.berthlist ? $.map(data.berthlist, function (item) { return new Berth(item) }) : []) : []);
    }

    //CargoModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var Cargo = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //AddressModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var Address = function (data, addressType) {
        var self = this;
        self.AddressID = ko.observable(data ? data.AddressID : 0);
        var typeOfAddress = "";
        if (addressType !== undefined) {
            typeOfAddress = addressType;
        }
        self.AddressType = ko.observable(data ? data.AddressType : typeOfAddress);
        self.NumberStreet = ko.observable(data ? data.NumberStreet : "");
        self.Suburb = ko.observable(data ? data.Suburb : "");
        self.TownCity = ko.observable(data ? data.TownCity : "");
        self.PostalCode = ko.observable(data ? data.PostalCode : "");
        self.CountryCode = ko.observable(data ? data.CountryCode : "");
    }

    ipmsRoot.PortReferenceData = PortReferenceData;
    ipmsRoot.PortRequesttype = PortRequesttype;
    ipmsRoot.TerminalOperatorModel = TerminalOperatorModel;
    ipmsRoot.Berth = Berth;
    ipmsRoot.Quay = Quay;
    ipmsRoot.Address = Address;
    ipmsRoot.Cargo = Cargo;
    ipmsRoot.Countries = Countries;

}(window.IPMSROOT));

//TerminalOperatorModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.TerminalOperatorModel.prototype.set = function (data) {
    var self = this;

    self.TerminalOperatorID(data ? (data.TerminalOperatorID || 0) : 0);
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.TradingName(data ? (data.TradingName || "") : "");
    self.RegistrationNumber(data ? (data.RegistrationNumber || "") : "");

    self.RegistrationDate(data ? (moment(data.RegistrationDate).format('YYYY-MM-DD') || "") : "");
    self.ValidityDate(data ? (moment(data.ValidityDate).format('YYYY-MM-DD') || "") : "");
    self.PremiseLocation(data ? (data.PremiseLocation || "") : "");
    self.TelephoneNo1(data ? (data.TelephoneNo1 || "") : "");
    self.FaxNo(data ? (data.FaxNo || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.Createdby(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy = (data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate = (data ? (data.ModifiedDate || "") : "");
    self.BusinessAddress(data ? new IPMSROOT.Address(data.BusinessAddress) : new IPMSROOT.Address(undefined, "ADPE"));
    self.PostalAddress(data ? new IPMSROOT.Address(data.PostalAddress) : new IPMSROOT.Address(undefined, "ADTE"));
    self.operationalBerths(data ? (data.OperationalBerths ? $.map(data.OperationalBerths, function (item) { return item }) : []) : []);
    self.operationalCargotypes(data ? (data.OperationalCargotypes ? $.map(data.OperationalCargotypes, function (item) { return item }) : []) : []);
    self.LicenLicensedForString(data ? (data.LicenLicensedForString || "") : "");
    self.TerminalOperatorServiceTypesArray(data ? (data.TerminalOperatorServiceTypesArray ? $.map(data.TerminalOperatorServiceTypesArray, function (item) { return item }) : []) : []);
    self.TerminalOperatorPortsArr(data ? (data.TerminalOperatorPortsArr ? $.map(data.TerminalOperatorPortsArr, function (item) { return item }) : []) : []);
    self.IsSameBusinessAdd(data ? data.IsSameBusinessAdd : '');
    self.cache.latestData = data;
}

// Reset Licensing Request Model Data
IPMSROOT.TerminalOperatorModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

//Accept validate dates
function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}





