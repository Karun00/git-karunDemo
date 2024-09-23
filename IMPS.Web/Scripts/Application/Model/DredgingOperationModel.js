(function (ipmsROOT) {

    var DredgingOperationModel = function (data) {
        var self = this;

        self.DredgingOperationID = ko.observable();
        self.DredgingPriorityID = ko.observable();
        self.Priority = ko.observable();
        self.AreaLocationID = ko.observable();
        self.TypeCode = ko.observable();
        self.RequiredDate = ko.observable();
        self.DesignDepth = ko.observable();
        self.PromulgateDepth = ko.observable();
        self.Requirement = ko.observable();
        self.DPARemarks = ko.observable();
        self.AreaType = ko.observable();
        self.PortCode = ko.observable();
        self.QuayCode = ko.observable();
        self.BerthCode = ko.observable();
        self.DPAWorkflowInstanceID = ko.observable();
        self.OccupationFrom = ko.observable("").extend({ required: { message: ' Please Select Occupation From' } });
        self.OccupationTo = ko.observable("").extend({ required: { message: ' Please Select Occupation To' } });
        self.OccupationDuration = ko.observable();
        self.VolumeOccupationFrom = ko.observable("");
        self.VolumeOccupationTo = ko.observable("");
        self.VolumeOccupationDuration = ko.observable();
        self.DOWorkflowInstanceID = ko.observable();
        self.Volume = ko.observable("");
        self.CraftID = ko.observable();
        self.DredgingTask = ko.observable();
        self.DredgingDelay = ko.observable("");
        self.DVRemarks = ko.observable();
        self.DVWorkflowInstanceID = ko.observable();
        self.DredgingStatus = ko.observable();
        self.IsDPAFinal = ko.observable();
        self.IsDOFinal = ko.observable();
        self.IsDVFinal = ko.observable();
        self.FinancialYearID = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.FinancialYearDate = ko.observable();
        self.DredgingMaterial = ko.observable();
        self.AreaName = ko.observable();
        self.ToDate = ko.observable();
        self.FromDate = ko.observable();
        self.UploadedFiles = ko.observableArray([]);
        self.BerthOccupationDocumentVO = ko.observableArray([]);
        self.DredgerName = ko.observable("");
        self.RequireDate = ko.observable("");
        self.workflowRemarks = ko.observable();

        self.WrkFlowRemark = ko.observable("");
        //self.EditPending = ko.computed(function () {
        //    if (self.IsDOFinal() == "Y ") {
        //        return true;
        //    }
        //    else if (self.IsDOFinal() == "R ") {
        //        return true;
        //    }
        //    else {
        //        return false;
        //    }

        //});



        // Sorting
        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        
        self.FinancialYearDateSort;
        self.FinancialYearDate.subscribe(function (value) {
            self.FinancialYearDateSort = value;
        });
        
        self.DredgingTypeNameSort;
        self.DredgingMaterial.subscribe(function (value) {
            self.DredgingTypeNameSort = value;
        });
        
        self.AreaNameSort;
        self.AreaName.subscribe(function (value) {
            self.AreaNameSort = value;
        });
        
        self.RequiredDateSort;
        self.RequiredDate.subscribe(function (value) {
            self.RequiredDateSort = value;
        });

        
        self.OccupationFromSort;
        self.OccupationFrom.subscribe(function (value) {
            self.OccupationFromSort = value;
        });

        self.OccupationToSort;
        self.OccupationTo.subscribe(function (value) {
            self.OccupationToSort = value;
        });
        self.VolumeOccupationFromSort;
        self.VolumeOccupationFrom.subscribe(function (value) {
            self.VolumeOccupationFromSort = value;
        });

        self.VolumeOccupationToSort;
        self.VolumeOccupationTo.subscribe(function (value) {
            self.OccupationToSort = value;
        });
        
        self.IsDOFinals = ko.computed(function () {
            debugger;
            if (self.IsDOFinal() == "N ") {
                var R = "New";
                return R;
            }
            if (self.IsDOFinal() == null) {
                var R = "New";
                return R;
            }
            if (self.IsDOFinal() == "Y "&&self.IsDVFinal() == "R ") {
                var R = "Completed";
                return R;
            }
            if (self.IsDOFinal() == "R "&&self.IsDVFinal() == "Y ") {
                var R = "Completed";
                return R;
            }
            if (self.IsDOFinal() == "Y " && self.IsDVFinal() == "Y ") {
                var R = "Completed";
                return R;
            }
            if (self.IsDOFinal() == "R " && self.IsDVFinal() == "R ") {
                var R = "Completed";
                return R;
            }
            if (self.IsDOFinal() == "Y ") {
                var R = "Approved";
                return R;
            }
           
            if (self.IsDOFinal() == "R ") {
                var R = "Rejected";
                return R;
            }
            if (self.IsDOFinal() == "C ") {
                var R = "Cancelled";
                return R;
            }
            
        });
        self.EditPendingBO = ko.computed(function () {
            debugger;
            //var Today = new Date();
            var Today = (moment(new Date()).format('YYYY-MM-DD'));
            var RDate = (moment(self.RequiredDate()).format('YYYY-MM-DD'));
            if (Today < RDate) {
                if (self.IsDOFinal() == "Y " && self.IsDVFinal() == "Y ") {
                    return false;
                }
                if (self.IsDOFinal() == "Y " && self.IsDVFinal() == "R ") {
                    return false;
                }
                if (self.IsDOFinal() == "Y ") {
                    return true;
                }
                
                if (self.IsDOFinal() == "R ") {
                    return false;
                }
                if (self.IsDOFinal() == "C ") {
                    return false;
                }
                if (self.IsDOFinal() == "N ") {
                    return true;
                }
                if (self.IsDOFinal() == null) {
                    return true;
                }
            }
            else {
                return false;

            }

        });
        //self.IsDOFinals = ko.computed(function () {
        //    return self.IsDOFinal() == "N " ? "New" : self.IsDOFinal() == null ? "New" : self.IsDOFinal() == "Y " ? "Occupation Approved" : self.IsDOFinal() == "R " ? "Occupation Rejected" : self.IsDVFinal() == "Y " ? "Completed" : self.IsDVFinal() == "R " ? "Completed" : "";
        //});
        self.IsDVFinals = ko.computed(function () {
            return self.IsDVFinal() == "N " ? "New" : self.IsDVFinal() == null ? "New" : self.IsDVFinal() == "Y " ? "Approved" : self.IsDVFinal() == "R " ? "Rejected" : "";
        });
       
        
        self.EditPendingDV = ko.computed(function () {
            if (self.IsDVFinal() == "Y ") {
                return false;
            }
            if (self.IsDVFinal() == "R ") {
                return false;
            }
            else if (self.IsDVFinal() == "N ") {
                return true;
            }
            else if (self.IsDVFinal() == null) {
                return true;
            }

        });


        self.updatePendingBO = ko.computed(function () {
            if (self.IsDOFinal() == "Y ") {
                return false;
            }
            if (self.IsDOFinal() == "R ") {
                return false;
            }
            if (self.IsDOFinal() == "C ") {
                return false;
            }
            else if (self.IsDOFinal() == "N ") {
                return true;
            }
            else if (self.IsDOFinal() == null) {
                return true;
            }

        });
        // End Sorting fileds
        
        self.cache = function () { };
        self.set(data);
    }

    var BerthOccupationDocument = function (data) {
        var self = this;

        self.BerthOccupationDocumentID = ko.observable(data?data.BerthOccupationDocumentID : 0);
        self.DredgingOperationID = ko.observable(data ? data.DredgingOperationID : 0);
        self.DocumentID = ko.observable(data ? data.DocumentID : 0);
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "A");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    //var DredgingPriorityModel = function (data) {
    //    var self = this;

    //    self.DredgingPriorityID = ko.observable();
    //    self.DeploymentPlanID = ko.observable();
    //    self.FromDate = ko.observable();
    //    self.ToDate = ko.observable();
    //    self.RecordStatus = ko.observable();
    //    self.CreatedBy = ko.observable();
    //    self.CreatedDate = ko.observable();
    //    self.ModifiedBy = ko.observable();
    //    self.ModifiedDate = ko.observable();
    //    self.FinancialYearID = ko.observable();
    //    self.BerthCode = ko.observable();
    //    self.BerthName = ko.observable();
    //    self.QuayCode = ko.observable();
    //    self.PortCode = ko.observable();
    //    self.LocationName = ko.observable();
    //    self.FinancialYearDate = ko.observable();
    //    self.LocationID = ko.observable();
    //    self.FinancialYears = ko.observable();
    //    self.DredgingOperationVO = ko.observable();
    //    self.OccupationFrom = ko.observable();
    //    self.OccupationTo = ko.observable();
    //    self.DredgingTypeName = ko.observable();
    //    self.AreaName = ko.observable();
    //    self.RequiredDate = ko.observable();

    //    self.cache = function () { };

    //    self.set(data);
    //}
    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }
    ipmsROOT.pendingTask = pendingTask;
    ipmsROOT.BerthOccupationDocument = BerthOccupationDocument;
    ipmsROOT.DredgingOperationModel = DredgingOperationModel;

}(window.IPMSROOT));

var DateFormat = 'YYYY-MM-DD HH:mm';

IPMSROOT.DredgingOperationModel.prototype.set = function (data) {
    var self = this;

    self.DredgingOperationID(data ? data.DredgingOperationID : 0);
    self.DredgingPriorityID(data ? data.DredgingPriorityID : 0);
    self.Priority(data ? data.Priority : 0);
    self.AreaLocationID(data ? data.AreaLocationID : 0);
    self.TypeCode(data ? data.TypeCode : "");
    self.RequiredDate(data ? (data.RequiredDate != null ? moment(data.RequiredDate).format('YYYY-MM-DD') : "") : "");
    self.DesignDepth(data ? data.DesignDepth : "");
    self.PromulgateDepth(data ? data.PromulgateDepth : "");
    self.Requirement(data ? data.Requirement : "");
    self.DPARemarks(data ? data.DPARemarks : "");
    self.AreaType(data ? data.AreaType : "");
    self.PortCode(data ? data.PortCode : "");
    self.QuayCode(data ? data.QuayCode : "");
    self.BerthCode(data ? data.BerthCode : "");
    self.DPAWorkflowInstanceID(data ? data.DPAWorkflowInstanceID : "");
    self.OccupationFrom(data ? (data.OccupationFrom != null ? moment(data.OccupationFrom).format(DateFormat) : "") : "");
    self.OccupationTo(data ? (data.OccupationTo != null ? moment(data.OccupationTo).format(DateFormat) : "") : "");
    self.OccupationDuration(data ? data.OccupationDuration : "");
    self.DOWorkflowInstanceID(data ? data.DOWorkflowInstanceID : "");
    self.Volume(data ? data.Volume : "");
    self.CraftID(data ? data.CraftID : "");
    self.DredgingTask(data ? data.DredgingTask : null);
    self.DredgingDelay(data ? data.DredgingDelay : "");
    self.DVRemarks(data ? data.DVRemarks : "");
    self.DVWorkflowInstanceID(data ? data.DVWorkflowInstanceID : "");
    self.DredgingStatus(data ? data.DredgingStatus : "");
    self.IsDPAFinal(data ? data.IsDPAFinal : "");
    self.IsDOFinal(data ? data.IsDOFinal : "");
    self.IsDVFinal(data ? data.IsDVFinal : "");
    self.FinancialYearID(data ? data.FinancialYearID : "");
    self.RecordStatus(data ? data.RecordStatus : "");
    self.CreatedBy(data ? data.CreatedBy : "");
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : "");
    self.ModifiedDate(data ? data.ModifiedDate : "");
    self.FinancialYearDate(data ? data.FinancialYearDate : "");
    self.DredgingMaterial(data ? data.DredgingMaterial : "");
    self.AreaName(data ? data.AreaName : "");
    self.ToDate(data ? data.ToDate : "");
    self.FromDate(data ? data.FromDate : "");
    self.DredgerName(data ? data.DredgerName : "");
    self.RequireDate(data ? data.RequireDate : "");
    self.VolumeOccupationFrom(data ? (data.VolumeOccupationFrom != null ? moment(data.VolumeOccupationFrom).format(DateFormat) : "") : "");
    self.VolumeOccupationTo(data ? (data.VolumeOccupationTo != null ? moment(data.VolumeOccupationTo).format(DateFormat) : "") : "");
    self.VolumeOccupationDuration(data ? data.VolumeOccupationDuration : "");
    self.BerthOccupationDocumentVO(data ? ko.utils.arrayMap(data.BerthOccupationDocumentVO, function (document) {
        return new IPMSROOT.BerthOccupationDocument(document);
    }) : []);

    self.cache.latestData = data;
}

IPMSROOT.DredgingOperationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

