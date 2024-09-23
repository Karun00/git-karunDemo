(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    // MobileIncidentReportModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var MobileIncidentReportModel = function (data) {
        var self = this;
        self.incidentLocation = ko.observable("").extend({ required: { message: '* Incident Location is required' } });
        self.incidentDescription = ko.observable("").extend({ required: { message: '* Incident Description is Required' } });
        self.UploadedFiles = ko.observableArray([]);
        self.IncedentTypeArray = ko.observableArray([]);
        self.IncidentDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentDocuments, function (incidentDocument) {
            return new IncidentDocumnet(incidentDocument);
        }) : []);
        self.IncidentNatures = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentNatures, function (incidentNature) {
            return new IncidentNature(incidentNature);
        }) : []);
        self.cache = function () { };
        self.set(data);
    }

    // Mobile IncidentDocumnet model
    var IncidentDocumnet = function (data) {
        var self = this;
        self.IncidentID = ko.observable(data ? data.IncidentID : "");
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentType = ko.observable(data ? data.DocumentType : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    }

    //Mobile IncidentNature model
    var IncidentNature = function (data) {
        var self = this;
        self.IncidentNatureID = ko.observable(data ? data.IncidentNatureID : "");
        self.IncidentID = ko.observable(data ? data.IncidentID : "");
    }

    // IncidentReportReferenceData is used to get the different incident types
    var IncidentReportReferenceData = function (data) {
        var self = this;
        self.IncidentTypes = ko.observableArray(data ? $.map(data, function (item) { return new IncidentType(item); }) : []);
    }

    // Mobile IncidentType model
    var IncidentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    ipmsRoot.MobileIncidentReportModel = MobileIncidentReportModel;
    ipmsRoot.IncidentDocumnet = IncidentDocumnet;
    ipmsRoot.IncidentNature = IncidentNature;
    ipmsRoot.IncidentReportReferenceData = IncidentReportReferenceData;
}(window.IPMSROOT));

IPMSROOT.MobileIncidentReportModel.prototype.set = function (data) {
    var self = this;
    self.IncedentTypeArray(data ? (data.IncedentTypeArray ? $.map(data.IncedentTypeArray, function (item) { return item }) : []) : []);
    self.cache.latestData = data;
}
IPMSROOT.MobileIncidentReportModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}