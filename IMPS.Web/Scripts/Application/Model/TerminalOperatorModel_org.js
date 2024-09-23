(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
    var TerminalOperatorModel = function (data) {
        var self = this;
        self.TerminalOperatorID =ko.observable("");
        self.RegisteredName = ko.observable("").extend({ required: { message: '* RegisteredName is required' } });
        self.TradingName = ko.observable("").extend({ required: { message: '* TradingName is required' } });
        self.RegistrationNumber = ko.observable("").extend({ required: { message: '* RegistrationNumber is required' } });
        self.RegistrationDate = ko.observable("").extend({ required: { message: '* RegistrationDate is required' } });
        self.ValidityDate = ko.observable("").extend({ required: { message: '* ValidityDate is required' } });
        self.PremiseLocation = ko.observable("").extend({ required: { message: '* PremiseLocation is required' } });
        self.LicensedFor = ko.observable("").extend({ required: { message: '* LicensedFor is required' } });
        self.TelephoneNo1 = ko.observable("").extend({ required: { message: '* TelephoneNo1 is required' } });        //self.TelephoneNo2 = ko.observable();
        self.FaxNo = ko.observable("").extend({ required: { message: '* FaxNo is required' } });
        self.BusinessAddressID = ko.observable("");
        self.PostalAddressID = ko.observable("");

        self.AddressType = ko.observable("");
        self.AddressType1 = ko.observable("");
        self.NumStreet = ko.observable("").extend({ required: { message: '* Number OR Street  is required' } });
        self.Suburb = ko.observable("").extend({ required: { message: '* Suburb  is required' } });
        self.TownCity = ko.observable("").extend({ required: { message: '* Town/City is required' } });
        self.PostalCode = ko.observable("").extend({ required: { message: '* PostalCode is required' } });

        self.NumStreet2 = ko.observable("").extend({ required: { message: '* NumberStreet is required' } });
        self.Suburb2 = ko.observable("").extend({ required: { message: '* Suburb is required' } });
        self.TownCity2 = ko.observable("").extend({ required: { message: '* Town/City is required' } });
        self.PostalCode2 = ko.observable("").extend({ required: { message: '* PostalCode is required' } });
        //self.PostalCode2 = ko.observable("");
        self.PortCode = ko.observable("");
        self.QuayCode = ko.observable();
        self.selectedQuayCode = ko.observable("");
        self.QuayName = ko.observable("");
        self.BerthCode = ko.observable("");
        self.BerthName = ko.observable("");
        self.postalNumStreet = ko.observable("");
       
        self.SubCatCode = ko.observable("");

        self.RecordStatus = ko.observable('A');
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A'? "Active" : "In Active";
        });

        //CargoTypeCode
        self.CargoTypeCode = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");        
        self.isSubmitVisible = ko.observable(false);

        self.Quaynamevalues = ko.observableArray([]);
        self.TerminalOperatorBerths = ko.observableArray([]);
        self.TerminalOperatorCargoHandlings = ko.observableArray([]);
        self.PostalAddress = ko.observableArray([]);
        self.BusinessAddress = ko.observableArray([]);
        self.isSaveVisible = ko.observable(false);
        self.isSubmitVisible = ko.observable(false);
        self.isCancelVisible = ko.observable(true);
        self.isView = ko.observable(true);

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
        self.PremiseLocationSort;
        self.PremiseLocation.subscribe(function (value) {
            self.PremiseLocationSort = value;
        });
        self.LicensedForSort;
        self.LicensedFor.subscribe(function (value) {
            self.LicensedForSort = value;
        });
    }

    var Berth = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QueyCode = ko.observable(data ? data.QueyCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.computed(function () { self.QueyCode() + self.BerthCode() });
    }

    var Quay = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QueyCode = ko.observable(data ? data.QueyCode : "");
        self.QueyName = ko.observable(data ? data.QueyName : "");
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item) }) : []);

    }

    ipmsRoot.TerminalOperatorModel = TerminalOperatorModel;
    ipmsRoot.Berth = Berth;
    ipmsRoot.Quay = Quay;

}(window.IPMSROOT));


IPMSROOT.TerminalOperatorModel.prototype.set = function (data) {
    var self = this;

    self.TerminalOperatorID(data ? (data.TerminalOperatorID || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.TradingName(data ? (data.TradingName || "") : "");
    self.RegistrationNumber(data ? (data.RegistrationNumber || "") : "");
    self.RegistrationDate(data ? (data.RegistrationDate || "") : "");
    self.ValidityDate(data ? (data.ValidityDate || null) : null);
    self.PremiseLocation(data ? (data.PremiseLocation || null) : null);
    self.LicensedFor(data ? (data.LicensedFor || null) : null);
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.TelephoneNo1(data ? (data.TelephoneNo1 || "") : "");
    self.FaxNo(data ? (data.FaxNo || "") : "");
    self.selectedQuayCode(data ? (data.selectedQuayCode || "") : "");

    self.BusinessAddressID(data ? (data.BusinessAddressID || "") : "");
    self.PostalAddressID(data ? (data.PostalAddressID || "") : "");
    self.createdby(data ? (data.createdby || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy = (data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate = (data ? (data.ModifiedDate || "") : "");
  

    self.NumStreet(data ? (data.BusinessNumberStreet || "") : "");
    self.Suburb(data ? (data.BusinessSuburb || "") : "");
    self.TownCity(data ? (data.BusinessTownCity || "") : "");
    self.PostalCode(data ? (data.BusinessPostalCode || "") : "");

    self.NumStreet2(data ? (data.postalNumberStreet || "") : "");
    self.Suburb2(data ? (data.postalSuburb || "") : "");
    self.TownCity2(data ? (data.postalTownCity || "") : "");
    self.PostalCode2(data ? (data.postalPostalCode || "") : "");

   
    self.QuayCode(data ? (data.QuayCode || null) : null);
    self.QuayName(data ? (data.QuayName || "") : "");
    self.BerthCode(data ? (data.BerthCode || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.SubCatCode(data ? (data.SubCatCode || "") : "");
    self.cache.latestData = data;
}
IPMSROOT.TerminalOperatorModel.prototype.reset = function () {
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
//Getting Date time
////function GetDateTime() {
//    var date = new Date();
//    var month = date.getMonth() + 1;
//    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
//    return currentDate;
//}

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




//GetCheckedPortsValues: function () {
//    $(function () {
//        var myArray = GetDetails();
//        var recd = [];
//        $.each(myArray, function (key, value) {
//            // alert(value.port_id);
//            recd.push(value.port_id);
//        });
//        //  alert("okkkkk");

//        var myArray1 = GetResultPort();
//        var recd1 = [];
//        $.each(myArray1, function (key, value) {
//            // alert(value.port_id);
//            recd1.push(value.port_id);
//        });
//        for (var i = 0; i < recd.length; i++) {
//            for (var j = 0; j < recd1.length; j++) {
//                if (recd[i] == recd1[j])
//                {
//                    $('#chkPort_'+recd[i]).attr('checked','true')
//                }
//            }
//            // alert(recd[i]);
//        }
         
           
//    });}
$('#Quaynames').trigger('change');

