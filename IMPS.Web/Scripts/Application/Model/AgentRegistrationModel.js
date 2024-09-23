(function (IPMSRoot) {

    // Countries list from Vessel Nationality
    var Countries = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var agentModel = function (data) {
        var self = this;
        var validationMessageText = '* This field is required.';
        self.validationEnabled = ko.observable(false);
        var rnd = Math.floor((Math.random() * 1000) + 1);

        self.AgentID = ko.observable();
        self.ReferenceNo = ko.observable();
        self.AnonymousUserYn = ko.observable();
        self.RegisteredName = ko.observable('').extend({ required: { message: validationMessageText } });
        self.TradingName = ko.observable('').extend({ required: { message: validationMessageText } });
        self.RegistrationNumber = ko.observable('').extend({ required: { message: validationMessageText } });
        self.VATNumber = ko.observable('').extend({ required: { message: validationMessageText } });
        self.IncomeTaxNumber = ko.observable('').extend({ required: { message: validationMessageText } });
        self.SkillsDevLevyNumber = ko.observable('').extend({ required: { message: validationMessageText } });

        /*newly added*/
        self.BusinessAddressID = ko.observable();
        self.PostalAddressID = ko.observable();
        self.TelephoneNo1 = ko.observable("").extend({ required: { message: validationMessageText } });
        self.TelephoneNo2 = ko.observable();
        self.FaxNo = ko.observable("").extend({ required: { message: validationMessageText } });
        self.AuthorizedContactPersonID = ko.observable();
        /**/

        self.SARSTaxClearance = ko.observable("N");
        self.SAASOA = ko.observable("N");
        self.QualifyBBBEECodes = ko.observable("N");
        self.BBBEEStatus = ko.observable(0);
        self.BBBEEStatuses = ko.observableArray([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
        self.VerifyBBBEEStatus = ko.observable("N");
        self.RecordStatus = ko.observable('');
        self.CreatedBy = ko.observable('1');
        self.validDate = ko.observable();

        self.CreatedDate = ko.observable(GetDateTime());
        self.ModifiedBy = ko.observable(null);
        self.ModifiedDate = ko.observable(null);

        self.NumberStreet = ko.observable('').extend({ required: { message: validationMessageText } });
        self.Suburb = ko.observable('').extend({ required: { message: validationMessageText } });
        self.TownCity = ko.observable('').extend({ required: { message: validationMessageText } });
        self.CountryCode = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: validationMessageText } });
        self.PostalCode = ko.observable('').extend({ required: { message: validationMessageText } });

        self.NumStreet2 = ko.observable('').extend({ required: { message: validationMessageText } });
        self.Suburb2 = ko.observable('').extend({ required: { message: validationMessageText } });
        self.TownCity2 = ko.observable('').extend({ required: { message: validationMessageText } });
        self.CountryCode2 = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessageText } });
        self.PostalCode2 = ko.observable('').extend({ required: { message: validationMessageText } });

        self.AuthorizedContactPersonID = ko.observable();
        self.AuthorizedContactPersonType = ko.observable();
        self.FirstName = ko.observable('').extend({ required: { message: validationMessageText } });
        self.SurName = ko.observable('').extend({ required: { message: validationMessageText } });
        self.IdentityNo = ko.observable('').extend({ required: { message: validationMessageText } });
        self.Designation = ko.observable('').extend({ required: { message: validationMessageText } });
        self.CellularNo = ko.observable('').extend({ required: { message: validationMessageText } });
        self.EmailID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: validationMessageText }, email: { message: '* Please enter valid email address.' } });

        self.AuthorizedContactPerson = ko.observableArray([]);
        self.AgentPorts = ko.observableArray([]);
        self.Address = ko.observableArray([]);
        self.Address1 = ko.observableArray([]);
        self.AgentDocuments = ko.observableArray([]);
        self.PortData = ko.observableArray([]);
        self.termsConditions = ko.observable(false);

        self.DocumentTypes = ko.observableArray();
        self.DocumentTypeCode = ko.observable();
        self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);

        //add pending task
        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);
        // end

        self.AgentDocumentsVO = ko.observableArray(data ? ko.utils.arrayMap(data.AgentDocumentsVO, function (document) {
            return new AgentDocuments(document);
        }) : []);
    }

    var pendingTask = function (data) {
        var self = this;
        var validationMessageText = '* This field is required';

        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: validationMessageText } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
    }

    var AgentDocuments = function (data) {
        var self = this;
        self.Data = ko.observable(data ? data.Data : '');
        self.DocumentName = ko.observable(data ? data.CategoryName : '');
        self.DocumentType = ko.observable(data ? data.CategoryCode : '');
        self.DocumentPath = ko.observable(data ? data.FileName : '');
        self.CategoryName = ko.observable(data ? data.CategoryName : '');
        self.CategoryCode = ko.observable(data ? data.CategoryCode : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.FileName = ko.observable(data ? data.FileName : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.FileType = ko.observable(data ? data.FileType : "");
    }

    IPMSRoot.AgentModel = agentModel;
    IPMSRoot.AgentDocuments = AgentDocuments;
    IPMSRoot.Countries = Countries;

}(window.IPMSROOT));

IPMSROOT.AgentModel.prototype.set = function (data) {
    var self = this;

    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.AnonymousUserYn(data ? (data.AnonymousUserYn || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.TradingName(data ? (data.TradingName || "") : "");
    self.RegistrationNumber(data ? (data.RegistrationNumber || "") : "");
    self.VATNumber(data ? (data.VATNumber || "") : "");
    self.IncomeTaxNumber(data ? (data.IncomeTaxNumber || "") : "");
    self.SkillsDevLevyNumber(data ? (data.SkillsDevLevyNumber || "") : "");
    self.SARSTaxClearance(data ? (data.SARSTaxClearance || "") : "N");
    self.SAASOA(data ? (data.SAASOA || "") : "N");
    self.QualifyBBBEECodes(data ? (data.QualifyBBBEECodes || "") : "N");
    self.BBBEEStatus(data ? (data.BBBEEStatus || "") : "");
    self.VerifyBBBEEStatus(data ? (data.VerifyBBBEEStatus || "") : "N");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.NumberStreet(data ? (data.NumberStreet || "") : "");
    self.Suburb(data ? (data.Suburb || "") : "");
    self.TownCity(data ? (data.TownCity || "") : "");
    self.CountryCode(data ? (data.CountryCode || "") : "");
    self.PostalCode(data ? (data.PostalCode || "") : "");
    self.TelephoneNo1(data ? (data.TelephoneNo1 || "") : "");
    self.FaxNo(data ? (data.FaxNo || "") : "");
    self.NumStreet2(data ? (data.NumStreet2 || "") : "");
    self.Suburb2(data ? (data.Suburb2 || "") : "");
    self.TownCity2(data ? (data.TownCity2 || "") : "");
    self.CountryCode2(data ? (data.CountryCode2 || "") : "");
    self.PostalCode2(data ? (data.PostalCode2 || "") : "");
    self.FirstName(data ? (data.FirstName || "") : "");
    self.SurName(data ? (data.SurName || "") : "");
    self.IdentityNo(data ? (data.IdentityNo || "") : "");
    self.Designation(data ? (data.Designation || "") : "");
    self.CellularNo(data ? (data.CellularNo || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.AgentPorts(data ? (data.AgentPorts || "") : "");
    self.Address(data ? (data.Address || "") : "");
    self.Address1(data ? (data.Address1 || "") : "");
    self.AuthorizedContactPerson(data ? (data.AuthorizedContactPerson || "") : "");

    self.validDate(data ? (data.validDate || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.AgentModel.prototype.reset = function () {
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