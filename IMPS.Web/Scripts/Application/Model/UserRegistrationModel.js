(function (ipmsRoot) {

    //UserRegistrationModel Fills from viewmodel to model and send data to view and Validates cshtml Controls data
    var UserRegistrationModel = function (data) {

        var self = this;

        self.UserType = ko.observable();
        self.UserTypeID = ko.observable();
        self.Name = ko.observable();
        self.ReferenceNo = ko.observable("").extend({ required: true }); //{ onlyIf: self.validationEnabled, message: '* SAP/Ref./Registration number is required' } });
        self.SubCatName = ko.observable();
        self.SubCatCode = ko.observable();
        self.Designation = ko.observable("").extend({ required: { onlyIf:self.validationEnabled, message: '* This field is required.' } });
        self.validationEnabled = ko.observable(false);
        self.FirstName = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* First Name is required' } });
        self.LastName = ko.observable();
        self.UserName = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* User Name is required' } });
        self.EmailID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' }, email: { message: '* Please enter valid Email ID' } });
self.ContactNo = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Contact No. is required' } });
        self.CreatedBy = ko.observable('1');
        self.PortCode = ko.observable();
        self.DormantStatus = ko.observable("N");
        self.PortList = ko.observableArray();
        self.UserPorts = ko.observableArray();
        self.AnonymousUserYn = ko.observable();
       
        self.ReasonForAccess = ko.observable();
        self.ValidFromDate = ko.observable('').extend({ required: true });
        self.ValidToDate = ko.observable('').extend({ required: true });


        self.cache = function () { };
                 
        self.set(data);                                                                 
    }

    ipmsRoot.UserRegistrationModel = UserRegistrationModel;

}(window.IPMSROOT));

//UserRegistrationModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.UserRegistrationModel.prototype.set = function (data) {

    var self = this;

    self.UserType(data ? data.UserType : "");
    self.UserTypeID(data ? (data.UserTypeID || null) : null);
    self.Name(data ? (data.Name || "") : "");
    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.Designation(data ? (data.Designation || "") : "");
    self.FirstName(data ? (data.FirstName || "") : "");
    self.LastName(data ? (data.LastName || "") : "");
    self.UserName(data ? (data.UserName || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.ContactNo(data ? (data.ContactNo || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.DormantStatus(data ? (data.DormantStatus || "N") : "N");
    self.AnonymousUserYn(data ? (data.AnonymousUserYn || "") : "");
   
    self.ReasonForAccess(data ? data.ReasonForAccess : "");
    self.ValidFromDate(data ? data.ValidFromDate : "");
    self.ValidToDate(data ? data.ValidToDate : "");

    self.cache.latestData = data;
}

//for reset
IPMSROOT.UserRegistrationModel.prototype.reset = function () {
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
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}