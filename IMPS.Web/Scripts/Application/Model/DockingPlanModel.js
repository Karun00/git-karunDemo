(function (ipmsRoot) {


    var DockingPlanDocument = function (data) {
        var self = this;

        self.DockingPlanDocumentID = ko.observable(data ? data.DockingPlanDocumentID : '');
        self.DockingPlanID = ko.observable(data ? data.DockingPlanID : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.FileName = ko.observable(data ? data.FileName : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");

        self.DocumentName = ko.observable(data ? data.CategoryName : '');
        self.DocumentType = ko.observable(data ? data.CategoryCode : '');
        self.DocumentPath = ko.observable(data ? data.FileName : '');
        self.CategoryName = ko.observable(data ? data.CategoryName : '');
        self.CategoryCode = ko.observable(data ? data.CategoryCode : '');
        self.DocumentTypeName = ko.observable(data ? data.DocumentTypeName : '');
    }


    var DockingPlanModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.DockingPlanDocumentID = ko.observable("");
        self.DockingPlanID = ko.observable("");
        self.VesselName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '*Please Select Vessel Name' } });
        self.VslSrchOn = ko.observable("");
        self.DocumentID = ko.observable("");
        self.WorkflowInstanceID = ko.observable("");
        self.Remarks = ko.observable("");

        self.DockingPlanStatus = ko.observable('');
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.workflowRemarks = ko.observable();
        self.IMONo = ko.observable("");
        self.VesselID = ko.observable("");
        self.VesselType = ko.observable("");
        self.LengthOverallInM = ko.observable("");
        self.BeamInM = ko.observable("");
        self.PortOfRegistry = ko.observable("");
        self.DockingPlanNo = ko.observable("");
        self.VesselGRT = ko.observable("");

        self.DockingPlanVO = ko.observable();
        self.WorkFlowRemarks = ko.observable();

        self.EditVisible = ko.observable("");

        self.DocumentTypes = ko.observableArray();
        self.DocumentTypeCode = ko.observable();

        //self.DockingPlanDocumentsVO = ko.observableArray(data ? ko.utils.arrayMap(data.DockingPlanDocumentsVO, function (document) {
        //    return new DockingPlanDocument(document);
        //}) : []);

        self.DockingPlanDocumentsVO = ko.observableArray();

        self.DockingStatus = ko.computed(function () {
            return self.DockingPlanStatus() == 'Approve' ? "Approved" : self.DockingPlanStatus() == 'Reject' ? "Rejected" : self.DockingPlanStatus() == 'Update Request' ? "Update Request" : self.DockingPlanStatus() == 'Cancel' ? "Cancelled" : "New Request";
        });

        // self.Url = ko.observable(data ? '/FileDownload/Download/' + data.DockingPlanDocumentsVO.DocumentID : '/FileDownload/Download/');

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "In Active";
        });

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.DockingPlanIDSort;
        self.DockingPlanNo.subscribe(function (value) {
            self.DockingPlanIDSort = value;
        });
        self.VesselSort;
        self.VesselName.subscribe(function (value) {
            self.VesselSort = value;
        });
        self.IMOSort;
        self.IMONo.subscribe(function (value) {
            self.IMOSort = value;
        });

        self.VesselTypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSort = value;
        });
        self.LOASort;
        self.LengthOverallInM.subscribe(function (value) {
            self.LOASort = value;
        });
        self.BeamSort;
        self.BeamInM.subscribe(function (value) {
            self.BeamSort = value;
        });
        self.CountrySort;
        self.PortOfRegistry.subscribe(function (value) {
            self.CountrySort = value;
        });
        self.SubmissionSort;
        self.CreatedDate.subscribe(function (value) {
            self.SubmissionSort = value;
        });




        self.EditPending = ko.computed(function () {

            if (self.EditVisible() == true) {
                if (self.DockingPlanStatus() == "New Request") {
                    return true;
                }
                if (self.DockingPlanStatus() == "Update Request") {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

        });

        self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);
    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Please enter remarks.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");

    }

    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.DockingPlanModel = DockingPlanModel;
    ipmsRoot.DockingPlanDocument = DockingPlanDocument;
}(window.IPMSROOT));

IPMSROOT.DockingPlanModel.prototype.set = function (data) {
    var self = this;

    self.DockingPlanID(data ? (data.DockingPlanID || "") : "");
    self.DockingPlanDocumentID(data ? (data.DockingPlanDocumentID || "") : "");
    self.VesselID(data ? (data.VesselID == 'NULL' ? "" : data.VesselID || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.LengthOverallInM(data ? (data.LengthOverallInM || "") : "");
    self.BeamInM(data ? (data.BeamInM || "") : "");
    self.PortOfRegistry(data ? (data.PortOfRegistry || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.DockingPlanStatus(data ? (data.DockingPlanStatus || "") : "");

    self.DocumentID(data ? (data.DocumentID || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (moment(data.CreatedDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || null) : null);
    self.VesselGRT(data ? (data.VesselGRT || "") : "");
    self.EditVisible(data ? (data.EditVisible || "") : "");   

    self.DockingPlanNo(data ? (data.DockingPlanNo || "") : "");
    self.DockingPlanVO(data ? data.DockingPlanVO : "");
    self.DockingPlanDocumentsVO(data ? ko.utils.arrayMap(data.DockingPlanDocumentsVO, function (document) {
        return new IPMSROOT.DockingPlanDocument(document);
    }) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.DockingPlanModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



