(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
    var validationMessageText = '* This field is required.';

    // PortReferenceData Used For bind and send data  from view model controls
    var PortReferenceData = function (data) {
        var self = this;
        self.Ports = ko.observableArray(data ? $.map(data.Ports, function (item) { return new PortRequesttype(item); }) : []);
        self.PilotNationalitys = ko.observableArray(data ? $.map(data.Pilot_Nationality, function (item) { return new PilotNationality(item); }) : []);
        self.PilotRoles = ko.observableArray(data ? $.map(data.PilotRoleCode, function (item) { return new PilotroleCode(item); }) : []);
       // self.Vesseldetails = ko.observableArray(data ? $.map(data.VesselDetails, function (item) { return new vesselData(item); }) : []);
        self.MovementDetails = ko.observableArray(data ? $.map(data.MomentTypes, function (item) { return new movementtype(item); }) : []);
        self.DocumentsTypes = ko.observableArray(data ? $.map(data.Doctypes, function (item) { return new DocumentType(item); }) : []);
    }

    // PilotNationality Used For bind and send data  from view model controls
    var PortRequesttype = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }

    // PilotNationality Used For bind and send data  from view model controls
    var PilotNationality = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    // PilotroleCode Used For bind and send data  from view model controls
    var PilotroleCode = function (data) {
        var self = this;

        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    // vesselData Used For bind and send data  from view model controls
    //var vesselData = function (data) {
    //    var self = this;
    //    self.VesselID = ko.observable(data ? data.VesselID : "");
    //    self.VesselName = ko.observable(data ? data.VesselName : "");
    //}

    // movementtype Used For bind and send data  from view model controls
    var movementtype = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    // DocumentType Used For bind and send data  from view model controls
    var DocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    // Address Used For bind and send data  from view model controls
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

    // AddPilotExeptiontable Used For bind and send data  from view model controls
    var AddPilotExeptiontable = function (data) {
        var self = this;
        self.PilotID = ko.observable(data ? data.ArrivalCommodityID : "");
        self.PilotExemptionRequestID = ko.observable(data ? data.PilotExemptionRequestID : "");
        self.MovementTypeCode = ko.observable(data ? data.MovementTypeCode : "");
        self.PilotRoleCode = ko.observable(data ? data.PilotRoleCode : "");
        self.MovementDate = ko.observable(data ? moment(data.MovementDate).format('YYYY-MM-DD') : "");
        self.VesselID = ko.observable(data ? data.VesselID : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
    }

    // PilotExemptionRequestDocuments Used For bind The upload controls from view model
    var PilotExemptionRequestDocuments = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    var pendingTask = function (data) {
        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* This field is required.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    // PilotExemptionRequestModel Used For bind The All controls from view model
    var PilotExemptionRequestModel = function (data) {
        var self = this;

        self.FirstName = ko.observable(data ? data.FirstName : "").extend({ required: { message: validationMessageText } });
        self.Surname = ko.observable(data ? data.Surname : "").extend({ required: { message: validationMessageText } });
        self.DateofBirth = ko.observable("").extend({ required: { message: validationMessageText } });
        self.AddressCheckbox = ko.observable();
        self.IDNo = ko.observable(data ? data.IDNo : "").extend({ required: { message: validationMessageText } });

        self.IssuedDate = ko.observable(data ? data.IssuedDate : undefined).extend({ required: { message: validationMessageText } });
        self.IssueDate = ko.observable(data ? data.IssueDate : undefined);
        self.RenewalDate = ko.observable(data ? data.RenewalDate : "").extend({ required: { message: validationMessageText } });
        self.RenewDate = ko.observable(data ? data.RenewDate : undefined);
        self.CellNo = ko.observable(data ? data.CellNo : "");
        self.EmailID = ko.observable(data ? data.EmailID : "").extend({ required: { message: validationMessageText }, email: { onlyIf: self.validationEnabled, message: '* Please enter valid email address' }, });
        self.IssuingAuthority = ko.observable(data ? data.IssuingAuthority : "TNPA").extend({ required: { message: validationMessageText } });
        self.InvoiceRecipient = ko.observable(data ? data.InvoiceRecipient : "").extend({ required: { message: validationMessageText } });
        self.LicenseRecipient = ko.observable(data ? data.LicenseRecipient : "").extend({ required: { message: validationMessageText } });
        self.ContactNo = ko.observable(data ? data.ContactNo : "").extend({ required: { message: validationMessageText } });
        self.Certificate_of_Competency = ko.observable(data ? data.Certificate_of_Competency : "").extend({ required: { message: validationMessageText } });
        self.RecordStatus = ko.observable('A');
        self.PortCode = ko.observable(data ? data.PortCode : undefined).extend({ required: { message: validationMessageText } });
        self.NationalityCode = ko.observable(data ? data.NationalityCode : undefined).extend({ required: { message: validationMessageText } });
    //    self.vesselData = ko.observable(data ? data.vesselData : "");
        self.PortRequesttype = ko.observable(data ? data.PortRequesttype : "");
        self.PilotNationality = ko.observable(data ? data.PilotNationality : "");
        self.PilotRoleCode = ko.observable(data ? data.PilotRoleCode : "");
        self.movementtype = ko.observable(data ? data.movementtype : "");
        self.ResidentialAddress = ko.observable(data ? data.ResidentialAddress : "");
        self.PostalAddress = ko.observable(data ? data.PostalAddress : "");
        self.UploadedFiles = ko.observableArray([]);
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.PilotID = ko.observable(data ? data.PilotID : "");
        self.DocumentType = ko.observable(data ? data.DocumentType : undefined);
        self.PilotExemptionRequest = ko.observableArray(data ? ko.utils.arrayMap(data.PilotExemptionRequest, function (commodity) { return new AddPilotExeptiontable(commodity); }) : []);
        self.PilotExemptionRequestDocument = ko.observableArray(data ? ko.utils.arrayMap(data.PilotExemptionRequestdocument, function (pilotexemptionrequestDocument) {
            return new PilotExemptionRequestDocuments(pilotexemptionrequestDocument);
        }) : []);

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.Address = Address;
    ipmsRoot.DocumentType = DocumentType;
    ipmsRoot.PortReferenceData = PortReferenceData;
    ipmsRoot.PortRequesttype = PortRequesttype;
    ipmsRoot.PilotNationality = PilotNationality;
    ipmsRoot.PilotroleCode = PilotroleCode;
    ipmsRoot.AddPilotExeptiontable = AddPilotExeptiontable;
    ipmsRoot.movementtype = movementtype;
    ipmsRoot.PilotExemptionRequestDocuments = PilotExemptionRequestDocuments;
   // ipmsRoot.vesselData = vesselData;
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.PilotExemptionRequestModel = PilotExemptionRequestModel;
}(window.IPMSROOT));

// PilotExemptionRequestModel Used For get data from view model and bind The All controls in thevew
IPMSROOT.PilotExemptionRequestModel.prototype.set = function (data) {
    var self = this;

    self.FirstName(data ? (data.FirstName || "") : "");
    self.AddressCheckbox(data ? (data.AddressCheckbox || "") : "");
    self.Surname(data ? (data.Surname || "") : "");
    self.DateofBirth(data ? (moment(data.DateofBirth).format('YYYY-MM-DD') || "") : "");
    self.IDNo(data ? (data.IDNo || "") : "");
    self.IssuedDate(data ? (moment(data.IssuedDate).format('YYYY-MM-DD') || "") : "");
    self.IssueDate(data ? (data.IssueDate || "") : "");
    self.RenewalDate(data ? (moment(data.RenewalDate).format('YYYY-MM-DD') || "") : "");
    self.RenewDate(data ? (data.RenewDate || "") : "");
    self.CellNo(data ? (data.CellNo || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.Certificate_of_Competency(data ? (data.Certificate_of_Competency || "") : "");
    self.IssuingAuthority(data ? (data.IssuingAuthority || "TNPA") : "TNPA");
    self.InvoiceRecipient(data ? (data.InvoiceRecipient || "") : "");
    self.LicenseRecipient(data ? (data.LicenseRecipient || "") : "");
    self.ContactNo(data ? (data.ContactNo || "") : "");
    self.PilotRoleCode(data ? (data.PilotRoleCode || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || "") : "");
    self.PilotID(data ? (data.PilotID || "0") : "0");
    self.ResidentialAddress(data ? new IPMSROOT.Address(data.ResidentialAddress) : new IPMSROOT.Address(undefined, "ADPE"));
    self.PostalAddress(data ? new IPMSROOT.Address(data.PostalAddress) : new IPMSROOT.Address(undefined, "ADTE"));

    self.cache.latestData = data;
}

IPMSROOT.PilotExemptionRequestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
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