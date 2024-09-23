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
        //self.Vesseldetails = ko.observableArray(data ? $.map(data.VesselDetails, function (item) { return new vesselData(item); }) : []);
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
        self.NumberStreet = ko.observable(data ? data.NumberStreet : "").extend({ required: { message: validationMessageText } });
        self.Suburb = ko.observable(data ? data.Suburb : "").extend({ required: { message: validationMessageText } });
        self.TownCity = ko.observable(data ? data.TownCity : "").extend({ required: { message: validationMessageText } });
        self.PostalCode = ko.observable(data ? data.PostalCode : "").extend({ required: { message: validationMessageText } });
        self.CountryCode = ko.observable(data ? data.CountryCode : "").extend({ required: { message: validationMessageText } });
    }

    // AddPilotExeptiontable Used For bind and send data  from view model controls
    var AddPilotExeptiontable = function (data) {
        var self = this;
        self.PilotID = ko.observable(data ? data.PilotID : "");
        self.PilotExemptionRequestID = ko.observable(data ? data.PilotExemptionRequestID : "");
        self.MovementTypeCode = ko.observable(data ? data.MovementTypeCode : "");
        self.PilotRoleCode = ko.observable(data ? data.PilotRoleCode : "");
        self.MovementDate = ko.observable(data ? (data.MovementDate != null ? moment(data.MovementDate).format('YYYY-MM-DD') : "") : '');
        self.VesselID = ko.observable(data ? data.VesselID : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
    }

    var PilotCertificate = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.CertificateFileName = ko.observable(data ? data.CertificateFileName : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
    }

    // PilotExemptionRequestDocuments Used For bind The upload controls from view model
    var PilotExemptionRequestDocuments = function (data) {
        var self = this;
        self.PilotID = ko.observable(data ? data.PilotID : "");
        self.PilotExemptionRequestDocumentID = ko.observable(data ? data.PilotExemptionRequestDocumentID : "");
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    // PilotExemptionRequestModel Used For bind The All controls from view model
    var PilotExemptionRequestListViewUpdateModel = function (data) {
        var self = this;

        self.FirstName = ko.observable("").extend({ required: { message: validationMessageText } });
        self.LastName = ko.observable("").extend({ required: { message: validationMessageText } });
        self.Surname = ko.observable("").extend({ required: { message: validationMessageText } });
        self.DateofBirth = ko.observable("").extend({ date: true, message: validationMessageText });
        self.IDNo = ko.observable("").extend({ required: { message: validationMessageText } });
        self.IssuedDate = ko.observable("").extend({ date: true, message: validationMessageText });
        self.Certificate_of_Competency = ko.observable("").extend({ required: { message: validationMessageText } });
        self.PilotID = ko.observable();
        self.ExpiryDate = ko.observable();
        self.WorkflowInstanceId = ko.observable();
        self.RenewalDate = ko.observable("").extend({ date: true, message: validationMessageText });
        self.CellNo = ko.observable("").extend({ required: { message: validationMessageText } });
        self.EmailID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessageText }, email: { onlyIf: self.validationEnabled, message: '* Please enter valid email address.' }, });
        self.IssuingAuthority = ko.observable("TNPA").extend({ required: { message: validationMessageText } });
        self.InvoiceRecipient = ko.observable("").extend({ required: { message: validationMessageText } });
        self.LicenseRecipient = ko.observable("").extend({ required: { message: validationMessageText } });
        self.ContactNo = ko.observable("").extend({ required: { message: validationMessageText } });
        self.RecordStatus = ko.observable('A');
        self.PortCode = ko.observable();
        self.AddressCheckbox = ko.observable();
        self.NationalityCode = ko.observable(data ? data.NationalityCode : undefined).extend({ required: { message: validationMessageText } });
        self.vesselData = ko.observable("").extend({ required: { message: validationMessageText } });
        self.PortRequesttype = ko.observable("").extend({ required: { message: validationMessageText } });
        self.PilotNationality = ko.observable("").extend({ required: { message: validationMessageText } });
        self.PilotRoleCode = ko.observable("").extend({ required: { message: validationMessageText } });
        self.movementtype = ko.observable("").extend({ required: { message: validationMessageText } });
        //self.ResidentialAddress = ko.observable("").extend({ required: { message: validationMessageText } });
        //self.PostalAddress = ko.observable("").extend({ required: { message: validationMessageText } });
        self.ResidentialAddress = ko.observable(data ? data.ResidentialAddress : "");
        self.PostalAddress = ko.observable(data ? data.PostalAddress : "");
        self.UploadedFiles = ko.observableArray([]);
        self.DocumentType = ko.observable(data ? data.DocumentType : "").extend({ required: { message: validationMessageText } });
        self.WorkflowTaskCode = ko.observable(data ? data.Workflowinstance.WorkflowTaskCode : "").extend({ required: { message: validationMessageText } });
        self.PilotExemptionRequest = ko.observableArray(data ? ko.utils.arrayMap(data.PilotExemptionRequest, function (commodity) { return new AddPilotExeptiontable(commodity); }) : []);
        self.PilotExemptionRequestDocument = ko.observableArray(data ? ko.utils.arrayMap(data.PilotExemptionRequestdocument, function (pilotExemptionRequestDocuments) {
            return new PilotExemptionRequestDocuments(pilotExemptionRequestDocuments);
        }) : []);

        self.Statust = ko.computed(function () {
            return self.WorkflowTaskCode() == 'WFSA' ? "Approved" : " Not Approved";
        });

        self.IssuedApprovedDate = ko.observable();
        self.FullName = ko.observable();

        self.FirstNameSort;
        self.FirstName.subscribe(function (value) {
            self.FirstNameSort = value;
        });

        self.IssuedDateSort;
        self.IssuedApprovedDate.subscribe(function (value) {
            self.IssuedDateSort = value;
        });
        self.ExpiryDateSort;
        self.ExpiryDate.subscribe(function (value) {
            self.ExpiryDateSort = value;
        });

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
  //  ipmsRoot.vesselData = vesselData;
    ipmsRoot.PilotExemptionRequestListViewUpdateModel = PilotExemptionRequestListViewUpdateModel;

    ipmsRoot.PilotExemptionRequestDocuments = PilotExemptionRequestDocuments;

}(window.IPMSROOT));

// PilotExemptionRequestModel Used For get data from view model and bind The All controls in the view
IPMSROOT.PilotExemptionRequestListViewUpdateModel.prototype.set = function (data) {

    var self = this;
    self.FirstName(data ? (data.FirstName || "") : "");
    self.Surname(data ? (data.Surname || "") : "");
    self.ExpiryDate(data ? (data.ExpiryDate != null ? moment(data.ExpiryDate).format('YYYY-MM-DD') : "") : '');
    self.DateofBirth(data ? (data.DateofBirth != null ? moment(data.DateofBirth).format('YYYY-MM-DD') : "") : '');
    self.IDNo(data ? (data.IDNo || "") : "");
    self.IssuedDate(data ? (data.IssuedDate != null ? moment(data.IssuedDate).format('YYYY-MM-DD') : "") : '');
    self.IssuedApprovedDate(data ? (data.IssuedApprovedDate != null ? moment(data.IssuedApprovedDate).format('YYYY-MM-DD') : "") : '');
    self.RenewalDate(data ? (data.RenewalDate != null ? moment(data.RenewalDate).format('YYYY-MM-DD') : "") : '');
    self.Certificate_of_Competency(data ? (data.Certificate_of_Competency || "") : "");
    self.CellNo(data ? (data.CellNo || "") : "");
    self.AddressCheckbox(data ? (data.AddressCheckbox || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.PilotID(data ? (data.PilotID || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.NationalityCode(data ? (data.NationalityCode || "") : "");
    self.WorkflowTaskCode(data ? (data.Workflowinstance.WorkflowTaskCode || "") : "");
    self.IssuingAuthority(data ? (data.IssuingAuthority || "TNPA") : "TNPA");
    self.InvoiceRecipient(data ? (data.InvoiceRecipient || "") : "");
    self.LicenseRecipient(data ? (data.LicenseRecipient || "") : "");
    self.ContactNo(data ? (data.ContactNo || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.PilotRoleCode(data ? (data.PilotRoleCode || "") : "");
    self.LastName(data ? (data.LastName || "") : "");
    self.ResidentialAddress(data ? new IPMSROOT.Address(data.ResidentialAddress) : new IPMSROOT.Address(undefined, "ADPE"));
    self.PostalAddress(data ? new IPMSROOT.Address(data.PostalAddress) : new IPMSROOT.Address(undefined, "ADTE"));

    self.cache.latestData = data;
}

IPMSROOT.PilotExemptionRequestListViewUpdateModel.prototype.reset = function () {
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