(function (ipmsRoot) {

    var VesselArrestImmobilizationSAMSAStopModel = function (data) {

        var self = this;

        self.VesselName = ko.observable();
        self.LOA = ko.observable();
        self.GRT = ko.observable();
        self.PortOfRegistry = ko.observable();
        self.AgentName = ko.observable();
        self.VesselAgent = ko.observable();
        self.AnyDangerousGoods = ko.observable();
        self.TelephoneNo1 = ko.observable();
        self.VAISID = ko.observable();
        self.VCN = ko.observable("").extend({ required: { message: '* Please select the VCN No.' } });
        self.ArrestedDate = ko.observable();
        self.ArrestedRemarks = ko.observable();
        self.ReleasedDate = ko.observable();
        self.ReleasedRemarks = ko.observable();
        self.ImmobilizationStartDate = ko.observable();
        self.ImmobilizationEndDate = ko.observable();
        self.ExactWorkProposed = ko.observable();
        self.ApprovedDate = ko.observable();
        self.SAMSAStopDate = ko.observable();
        self.SAMSAStopRemarks = ko.observable();
        self.SAMSAClearedDate = ko.observable();
        self.SAMSAClearedRemarks = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.PeriodofImmobilization = ko.observable();
        self.DocumentName = ko.observable();
        self.UploadedFiles = ko.observableArray([]);
        self.VesselArrestedStatus = ko.observable();
        self.VesselReleasedStatus = ko.observable();
        self.ImmobilizationStatus = ko.observable();
        self.PollutionPrecautionTakenStatus = ko.observable();
        self.SAMSAStopStatus = ko.observable();
        self.SAMSAClearedStatus = ko.observable();
        self.ETA = ko.observable();

        self.CurrentBerth = ko.observable();
        //////// KendoUI Grid Sorting ////////

        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.ArrestedSort;
        self.VesselArrestedStatus.subscribe(function (value) {
            self.ArrestedSort = value;
        });

        self.ImmobilizationSort;
        self.ImmobilizationStatus.subscribe(function (value) {
            self.ImmobilizationSort = value;
        });

        self.VesselArrestDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.VesselArrestDocuments, function (vesselDocument) {
            return new VesselArrestDocument(vesselDocument);
        }) : []);

        self.VesselSAMSAStopDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.VesselSAMSAStopDocuments, function (Document) {
            return new VesselSAMSAStopDocument(Document);
        }) : []);

        self.SAMSAStopSort;
        self.SAMSAStopStatus.subscribe(function (value) {
            self.SAMSAStopSort = value;
        });

        self.cache = function () { };
        self.set(data);

    }

    var VesselArrestDocument = function (data) {
        var self = this;
        self.VAISID = ko.observable(data ? data.VAISID : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.Document.DocumentName : '');
        self.FileName = ko.observable(data ? data.Document.FileName : '');
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "'A");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');

    }

    var VesselSAMSAStopDocument = function (data) {
        var self = this;
        self.VAISID = ko.observable(data ? data.VAISID : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.Document.DocumentName : '');
        self.FileName = ko.observable(data ? data.Document.FileName : '');
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "A");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');

    }

    ipmsRoot.VesselArrestDocument = VesselArrestDocument;
    ipmsRoot.VesselSAMSAStopDocument = VesselSAMSAStopDocument;
    ipmsRoot.VesselArrestImmobilizationSAMSAStopModel = VesselArrestImmobilizationSAMSAStopModel;


}(window.IPMSROOT));

// Sets the default data, which is used in Reset functionality
IPMSROOT.VesselArrestImmobilizationSAMSAStopModel.prototype.set = function (data) {

    var self = this;

    self.VCN(data ? (data.VCN || "") : "");
    self.VesselName(data ? (data.Vessel.VesselName || "") : "");
    self.AnyDangerousGoods(data ? data.AnyDangerousGoods : "I");
    self.VAISID(data ? (data.VAISID || "") : "");
    self.GRT(data ? (data.Vessel.GrossRegisteredTonnageInMT || "") : "");
    self.LOA(data ? (data.Vessel.LengthOverallInM || "") : "");
    self.PortOfRegistry(data ? (data.Vessel.PortOfRegistry || "") : "");

    self.VesselAgent(data ? (data.Agent.RegisteredName || "") : "");
    self.AgentName(data ? (data.Agent.RegisteredName || "") : "");
    self.TelephoneNo1(data ? (data.Agent.TelephoneNo1 || "") : "");
    self.ArrestedDate(data ? ((data.ArrestedDate != null && data.ArrestedDate != "") ? moment(data.ArrestedDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.ArrestedRemarks(data ? (data.ArrestedRemarks || "") : "");
    self.ReleasedDate(data ? ((data.ReleasedDate != null && data.ReleasedDate != "") ? moment(data.ReleasedDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.ReleasedRemarks(data ? (data.ReleasedRemarks || "") : "");
    self.ImmobilizationStartDate(data ? ((data.ImmobilizationStartDate != null && data.ImmobilizationStartDate != "") ? moment(data.ImmobilizationStartDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.ImmobilizationEndDate(data ? ((data.ImmobilizationEndDate != null && data.ImmobilizationEndDate != "") ? moment(data.ImmobilizationEndDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.ExactWorkProposed(data ? (data.ExactWorkProposed || "") : "");
    self.ApprovedDate(data ? ((data.ApprovedDate != null && data.ApprovedDate != "") ? moment(data.ApprovedDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.SAMSAStopDate(data ? ((data.SAMSAStopDate != null && data.SAMSAStopDate != "") ? moment(data.SAMSAStopDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.SAMSAStopRemarks(data ? (data.SAMSAStopRemarks || "") : "");
    self.SAMSAClearedDate(data ? ((data.SAMSAClearedDate != null && data.SAMSAClearedDate != "") ? moment(data.SAMSAClearedDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.SAMSAClearedRemarks(data ? (data.SAMSAClearedRemarks || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || 'A') : 'A');
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy = ko.observable(data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate = ko.observable(data ? (data.ModifiedDate || "") : "");
    self.DocumentName = ko.observable(data ? (data.DocumentName || "") : "");
    self.PeriodofImmobilization = ko.observable(data ? (data.PeriodofImmobilization || "") : "");
    self.VesselArrestedStatus(data ? (data.VesselArrestedStatus || "") : "");
    self.VesselReleasedStatus(data ? (data.VesselReleasedStatus || "") : "");
    self.ImmobilizationStatus(data ? (data.ImmobilizationStatus || "") : "");
    self.PollutionPrecautionTakenStatus(data ? (data.PollutionPrecautionTakenStatus || "") : "");
    self.SAMSAStopStatus(data ? (data.SAMSAStopStatus || "") : "");
    self.SAMSAClearedStatus(data ? (data.SAMSAClearedStatus || "") : "");
    self.ETA(data ? (data.ETA || "") : "");
    self.CurrentBerth(data ? (data.CurrentBerth || "N/A") : "N/A");

    self.cache.latestData = data;
}

IPMSROOT.VesselArrestImmobilizationSAMSAStopModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}