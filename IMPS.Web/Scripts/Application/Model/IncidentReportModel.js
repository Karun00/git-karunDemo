(function (ipmsRoot) {

    //IncidentReportModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var IncidentReportModel = function (data) {
        var self = this;
        self.IncidentLocation = ko.observable("").extend({ required: { message: '* Incident Location is required' } });
        self.IncidentDescription = ko.observable("").extend({ required: { message: '* Incident Description is Required' } });
        self.IncidentID = ko.observable("");
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.PortCode = ko.observable("");
        self.IncidentNatureDetails = ko.observable();
        self.UploadedFiles = ko.observableArray([]);
        self.IncedentTypeArray = ko.observableArray([]);
        self.IncidentDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentDocuments, function (incidentDocument) {
            return new IncidentDocumnet(incidentDocument);
        }) : []);
        self.IncidentNatures = ko.observableArray(data ? ko.utils.arrayMap(data.IncidentNatures, function (incidentNature) {
            return new IncidentNature(incidentNature);
        }) : []);
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.IncidentLocationSort;
        self.IncidentLocation.subscribe(function (value) {
            self.IncidentLocationSort = value;
        });
        self.IncidentDescriptionSort;
        self.IncidentDescription.subscribe(function (value) {
            self.IncidentDescriptionSort = value;
        });
        self.IncidentNatureDetailsSort;
        self.IncidentNatureDetails.subscribe(function (value) {
            self.IncidentNatureDetailsSort = value;
        });
        self.RecordStatusSort;
        self.Statust.subscribe(function (value) {
            self.RecordStatusSort = value;
        });


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


    // Mobile IncidentType model
    var IncidentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    ipmsRoot.IncidentReportModel = IncidentReportModel;
    ipmsRoot.IncidentDocumnet = IncidentDocumnet;
    ipmsRoot.IncidentNature = IncidentNature;
    ipmsRoot.IncidentType = IncidentType;
}(window.IPMSROOT));

IPMSROOT.IncidentReportModel.prototype.set = function (data) {
    var self = this;
    self.IncidentLocation(data ? data.IncidentLocation : "");
    self.IncidentDescription(data ? data.IncidentDescription : "");
    self.IncidentID(data ? data.IncidentID : "");
    self.CreatedBy(data ? data.CreatedBy : "");
    self.CreatedDate(data ? data.CreatedDate : "");
    self.PortCode(data ? data.PortCode : "");
    self.RecordStatus(data ? data.RecordStatus : "");
    self.IncidentNatureDetails(data ? data.IncidentNatureDetails : "");
    self.IncedentTypeArray(data ? (data.IncedentTypeArray ? $.map(data.IncedentTypeArray, function (item) { return item }) : []) : []);
    self.cache.latestData = data;
}
IPMSROOT.IncidentReportModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}