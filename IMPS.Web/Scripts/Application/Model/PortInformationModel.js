(function (ipmsRoot) {

    var PortInformationModel = function (data, ContentRoleTypes, UploadedFiles) {

        var self = this;
        self.validationEnabled = ko.observable(true);
        self.PortContentID = ko.observable();
        self.PortCode = ko.observable("");
        self.ContentType = ko.observable("");
        self.ContentName = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.LinkVisibility = ko.observable("");
        self.LinkType = ko.observable("");
        self.LinkContent = ko.observable("");
        self.DocumentID = ko.observable(null);
        self.DocumentName = ko.observable("");
        self.ParentPortContentID = ko.observable("");
        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable('1');
        self.CreatedDate = ko.observable(GetDateTime());
        self.UserType = ko.observable("EMP");
        self.UploadedFiles = ko.observableArray([]);
        self.PortContentRole = ko.observableArray();
        self.Document = ko.observable("");
        self.PortContent1 = ko.observable();

        self.cache = function () { };
        self.set(data);
    }

    var UploadedFiles = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
    }

    var Roles = function (data) {
        var self = this;
        self.RoleName = ko.observable(data ? data.RoleName : "");
        self.RoleID = ko.observable(data ? data.RoleID : "");
    }

    ipmsRoot.Roles = Roles;
    ipmsRoot.UploadedFiles = UploadedFiles;
    ipmsRoot.PortInformationModel = PortInformationModel;

}(window.IPMSROOT));

IPMSROOT.PortInformationModel.prototype.set = function (data) {
    var self = this;

    self.PortContentID(data ? (data.PortContentID) || 0 : 0);
    self.PortCode(data ? (data.PortCode || "") : "");
    self.ContentType(data ? (data.ContentType || "") : "Y");
    self.ContentName(data ? (data.ContentName || "") : "");
    self.LinkVisibility(data ? (data.LinkVisibility || "") : "U");
    self.LinkType(data ? (data.LinkType || "D") : "C");
    self.LinkContent(data ? (data.LinkContent || "") : "");
    self.DocumentID(data ? (data.DocumentID || null) : null);
    self.DocumentName(data ? (data.DocumentName || "") : "");
    self.ParentPortContentID(data ? (data.ParentPortContentID || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || '') : '');
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.Document(data ? (data.Document || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.PortInformationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

