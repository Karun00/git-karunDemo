(function (ipmsRoot) {
    var DredgingPriorityReferenceData = function (data) {
        var self = this;
        debugger;
       // self.FinancialYears = ko.observableArray(data ? $.map(data.FinancialYears, function (item) { return new FinancialYear(item); }) : []);
      //  self.DocumentType = ko.observableArray(data ? $.map(data.getDocumentTypes, function (item) { return new DocumentTypes(item); }) : []);
        self.DredgingTypes = ko.observableArray(data ? $.map(data.DredgingTypes, function (item) { return new DredgingType(item); }) : []);
        self.BerthTypes = ko.observableArray(data ? $.map(data.BerthTypes, function (item) { return new BerthType(item); }) : []);
        self.LocationTypes = ko.observableArray(data ? $.map(data.LocationTypes, function (item) { return new LocationType(item); }) : []);

    }
    //var FinancialYear = function (data) {
    //    var self = this;
    //    self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
    //    self.SubCatName = ko.observable(data ? data.SubCatName : "");
    //}
    var DredgingType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var BerthType = function (data) {
        var self = this;
       self.PortCode = ko.observable(data ? data.PortCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
    }
    var LocationType = function (data) {
        var self = this;
        self.LocationID = ko.observable(data ? data.LocationID : "");
        self.LocationName = ko.observable(data ? data.LocationName : "");
    }
    var KeyEvent = function (data) {
        var self = this;
        self.DredgingType = ko.observable(data ? data.DredgingType : "");
        self.DredgPlan = ko.observable(data ? (data.DredgPlan == 0 ? 0 : data.DredgPlan || "") : "");
        self.Volume = ko.observable(data ? (data.Volume == 0 ? 0 : data.Volume || "") : "");
        // self.Volume(data ? (data.Volume == 0 ? 0 : data.Volume || "") : "");
        //self.Budget = ko.observable(data ? data.Budget : "");
        //self.Volume = ko.observable(data ? data.Volume : "");
        self.Remaining = ko.dependentObservable(function () {
            return parseInt(self.DredgPlan()) - parseInt(self.Volume());
            self.DeploymentPlanID = ko.observable(data ? data.DeploymentPlanID : "");
        });
        
    }
    //var DocumentTypes = function (data) {
    //    debugger;
    //    var self = this;
    //    self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
    //    self.SubCatName = ko.observable(data ? data.SubCatName : "");
    //}
    var dredgingdocument = function (data) {
        var self = this;
       
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.FileName = ko.observable(data ? data.FileName : '');
        self.DredgingPriorityDocumentID = ko.observable(data ? data.DredgingPriorityDocumentID : '');
        self.DredgingPriorityID = ko.observable(data ? data.DredgingPriorityID : '');
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
    }
   
    var DredgingPriorityModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.DredgingPriorityID = ko.observable();
       // self.FinancialYearCode = ko.observable("").extend({ CodeUnique: self.FinancialYearCode, required: { onlyIf: self.validationEnabled, message: '* Please Select the Financial Year' } });;
        self.DeploymentPlanID = ko.observable();
        self.FromDate = ko.observable("").extend({ CodeUnique: self.FromDate, required: { onlyIf: self.validationEnabled, message: '* Please Select From Period' } });;
        self.ToDate = ko.observable("").extend({ CodeUnique: self.ToDate, required: { onlyIf: self.validationEnabled, message: '* Please Select To Period' } });;
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
       // self.TotalTime = ko.observable();
        self.FinancialYearDate = ko.observable("");
        self.FinancialYearID = ko.observable("");//.extend({ CodeUnique: self.FinancialYearID, required: { onlyIf: self.validationEnabled, message: '* Please Select Financial Year' } });;
        self.RecordStatus = ko.observable('A');
        self.DocumentID = ko.observable("");
        self.FileName = ko.observable("");
        self.DredgingPriorityDocumentID = ko.observable("");
        self.DredgingType = ko.observable("");
        // self.TypeName = ko.observable("");
        self.AreaName = ko.observable("");
        self.Month = ko.observable("");
        self.RequireDate = ko.observable("");
        
        self.Priority = ko.observable("").extend({ CodeUnique: self.Priority, required: { onlyIf: self.validationEnabled, message: '* Please enter the Priority' } });;
       // self.Priority = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please enter the priority' } });
        self.TypeCode = ko.observable("").extend({ CodeUnique: self.TypeCode, required: { onlyIf: self.validationEnabled, message: '* Please Select the Dredging Material' } });;
        self.RequiredDate = ko.observable("").extend({ CodeUnique: self.RequiredDate, required: { onlyIf: self.validationEnabled, message: '* Please Select the Required Date' } });;
        self.PromulgateDepth = ko.observable("");
        // self.DredgingDepth = ko.observable("");
        self.Requirement = ko.observable("").extend({ CodeUnique: self.Requirement, required: { onlyIf: self.validationEnabled, message: '* Please enter the Requirement' } });;
        self.DPARemarks = ko.observable("");
        self.AreaType = ko.observable("0").extend({ CodeUnique: self.AreaType, required: { onlyIf: self.validationEnabled, message: '* Please Select the Area Type' } });;

        self.AreaLocationID = ko.observable("");
        self.DesignDepth = ko.observable("");
        self.PortCode = ko.observable("");
        self.PortName = ko.observable("");
        self.QuayCode = ko.observable("");
        self.BerthCode = ko.observable("");
        self.BerthKey = ko.observable("");
        
        self.DesignDepth = ko.observable("");
        self.OccupationFrom = ko.observable();
        self.OccupationTo = ko.observable();
        self.DPAWorkflowInstanceID = ko.observable("");
        self.DredgingOperationID = ko.observable("");
        self.DredgingMaterial = ko.observable("");
        self.IsDPAFinal = ko.observable("");
        self.MonthValue = ko.observable("");
        self.BerthName = ko.observable("");
        self.LocationName = ko.observable("");

        self.UploadedFiles = ko.observableArray([]);
        self.StartDate = ko.observable();
        self.FinancialYear = ko.observable("");
        self.WorkFlowRemarks = ko.observable();

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.EditPending = ko.computed(function () {
            if (self.IsDPAFinal() == "Y ") {
                return false;
            }
            else if (self.IsDPAFinal() == "N ") {
                return true;
            }
            
        });
        self.IsDPAFinals = ko.computed(function () {
            //debugger;
            if (self.IsDPAFinal() == "N ") {
                var R = "New";
                return R;
            }
            if (self.IsDPAFinal() == null) {
                var R = "New";
                return R;
            }
           
            if (self.IsDPAFinal() == "Y ") {
                var R = "Approved";
                return R;
            }
            if (self.IsDPAFinal() == "R ") {
                var R = "Rejected";
                return R;
            }

        });
        self.AreaTypeSt = ko.computed(function () {
            return self.AreaType() == 'B' ? "Berth Type" : "Location";
        });

        self.DredgingOperationsVO = ko.observableArray(data ? ko.utils.arrayMap(data.DredgingOperationsVO, function (document) {
            return new DredgingPriorityAreaModel(document);
        }) : []);
       
        self.FinancialYearDateSort;
        self.FinancialYearDate.subscribe(function (value) {
            self.FinancialYearDateSort = value;
        });
        self.FromDateSort;
        self.Month.subscribe(function (value) {
            self.FromDateSort = value;
        });
       
        //self.ToDateSort;
        //self.ToDate.subscribe(function (value) {
        //    self.ToDateSort = value;
        //});

        //-------------DredgingPriorityAreaSort

        self.PrioritySort;
        self.Priority.subscribe(function (value) {
            self.PrioritySort = value;
        });

        self.AreaTypeSort;
        self.AreaType.subscribe(function (value) {
            self.AreaTypeSort = value;
        });
        self.BerthSort;
        self.BerthName.subscribe(function (value) {
            self.BerthSort = value;
        });
        self.AreaLocationSort;
        self.LocationName.subscribe(function (value) {
            self.AreaLocationSort = value;
        });
        self.TypeSort;
        self.TypeCode.subscribe(function (value) {
            self.TypeSort = value;
        });
        self.RequiredDateSort;
        self.RequiredDate.subscribe(function (value) {
            self.RequiredDateSort = value;
        });
        self.DesignDepthSort;
        self.DesignDepth.subscribe(function (value) {
            self.DesignDepthSort = value;
        });
        self.PromulgateDepthSort;
        self.PromulgateDepth.subscribe(function (value) {
            self.PromulgateDepthSort = value;
        });
        self.RequirementSort;
        self.Requirement.subscribe(function (value) {
            self.RequirementSort = value;
        });
        self.RemarksSort;
        self.DPARemarks.subscribe(function (value) {
            self.RemarksSort = value;
        });
        self.DredgingPriorityDocumentsVO = ko.observableArray(data ? (data.DredgingPriorityDocumentsVO || null) : null);
       // self.DredgingPriorityAreasVO = ko.observableArray(data ? (data.DredgingPriorityAreasVO || null) : null);
        self.cache = function () { };
        self.set(data);

    }
    var DredgingPriorityAreaModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
       // self.Priority = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please enter the priority' } });
        self.Priority = ko.observable("").extend({ CodeUnique: self.Priority, required: { onlyIf: self.validationEnabled, message: '* Please enter the Priority' } });;
       // self.Priority = ko.observable("").extend({ required: true });
        self.AreaLocationID = ko.observable("");
        self.TypeCode = ko.observable("").extend({ CodeUnique: self.TypeCode, required: { onlyIf: self.validationEnabled, message: '* Please Select the Dredging Material' } });;
        self.RequiredDate = ko.observable("").extend({ CodeUnique: self.RequiredDate, required: { onlyIf: self.validationEnabled, message: '* Please Select the Required Date' } });;
        self.PromulgateDepth = ko.observable("");
       // self.DredgingDepth = ko.observable("");
        self.Requirement = ko.observable("").extend({ CodeUnique: self.Requirement, required: { onlyIf: self.validationEnabled, message: '* Please enter the Requirement' } });;
        self.DPARemarks = ko.observable("");
        self.AreaType = ko.observable("").extend({ CodeUnique: self.AreaType, required: { onlyIf: self.validationEnabled, message: '* Please Select the Area Type' } });;
        self.PortCode = ko.observable("");
        self.PortName = ko.observable("");
        self.QuayCode = ko.observable("");
        self.BerthCode = ko.observable("");
        self.BerthKey = ko.observable("");
        self.DesignDepth = ko.observable("");
        self.IsDPAFinal = ko.observable("");
        self.AreaName = ko.observable("");
        self.Month = ko.observable("");
        self.RequireDate = ko.observable("");
        self.BerthName = ko.observable("");
        self.LocationName = ko.observable("");
       // self.Name = ko.observable("");
        self.IsBerthEnable = ko.observable(true);
        self.IsLocationEnable = ko.observable(false);
        self.DredgingPriorityDocumentsVO = ko.observableArray(data ? (data.DredgingPriorityDocumentsVO || null) : null);

        self.OccupationFrom = ko.observable();
        self.OccupationTo = ko.observable();
        self.DredgingPriorityID = ko.observable("");
        self.DeploymentPlanID = ko.observable("");
        self.DPAWorkflowInstanceID = ko.observable("");
        self.DredgingOperationID = ko.observable(data ? data.DredgingOperationID : "");
        self.DredgingMaterial = ko.observable("");
        self.DredgingType = ko.observable("");

        self.DredgingOperationsVO = ko.observableArray(data ? ko.utils.arrayMap(data.DredgingOperationsVO, function (document) {
            return new DredgingPriorityAreaModel(document);
        }) : []);
       // self.DredgingPriorityAreasVO = ko.observableArray(data ? (data.DredgingPriorityAreasVO || null) : null);
        self.FromDate = ko.observable("");
        self.ToDate = ko.observable("");
        self.FinancialYearID = ko.observable("");
        self.cache = function () { };
        self.set(data);
    }
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
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.DredgingPriorityModel = DredgingPriorityModel;
    ipmsRoot.DredgingPriorityReferenceData = DredgingPriorityReferenceData;
    ipmsRoot.BerthType = BerthType;
    ipmsRoot.LocationType = LocationType
    ipmsRoot.KeyEvent = KeyEvent;
    ipmsRoot.DredgingPriorityAreaModel = DredgingPriorityAreaModel;
    ipmsRoot.dredgingdocument = dredgingdocument;


}(window.IPMSROOT));

IPMSROOT.DredgingPriorityModel.prototype.set = function (data) {
    var self = this;

   // self.FinancialYearCode(data ? (data.FinancialYearCode == 'NULL' ? "" : data.FinancialYearCode || "") : "");
    self.DredgingPriorityID(data ? data.DredgingPriorityID : 0);
    self.DredgingOperationID(data ? data.DredgingOperationID : 0);
    
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.FromDate(data ? (moment(data.FromDate).format('YYYY-MM-DD') || "") : "");
    self.ToDate(data ? (moment(data.ToDate).format('YYYY-MM-DD') || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.FinancialYearID(data ? (data.FinancialYearID || "") : "");
    self.FinancialYearDate(data ? (data.FinancialYearDate || "") : "");
    self.FinancialYear(data ? (data.FinancialYear || "") : "");
    self.TypeCode(data ? (data.TypeCode || "") : "");
    self.Requirement(data ? (data.Requirement || "") : "");
   // self.DredgingPriorityID(data ? (data.DredgingPriorityID || "") : "");
    self.DeploymentPlanID(data ? (data.DeploymentPlanID || "") : "");
    self.DocumentID(data ? (data.DocumentID || "") : "");
    self.FileName(data ? (data.FileName || "") : "");
    self.DredgingPriorityDocumentID(data ? (data.DredgingPriorityDocumentID || "") : "");
    self.DredgingType(data ? (data.DredgingType || "") : "");
    self.IsDPAFinal(data ? (data.IsDPAFinal || "") : "");
    self.Priority(data ? (data.Priority || "") : "");
    self.AreaLocationID(data ? (data.AreaLocationID || "") : "");
    self.AreaName(data ? (data.AreaName || "") : "");
    self.Month(data ? (data.Month || "") : "");
    self.RequiredDate(data ? (moment(data.RequiredDate).format('YYYY-MM-DD') || "") : "");
    self.PromulgateDepth(data ? (data.PromulgateDepth || "") : "");
   // self.DredgingDepth(data ? (data.DredgingDepth || "") : "");
    self.Requirement(data ? (data.Requirement || "") : "");
    self.DPARemarks(data ? (data.DPARemarks || "") : "");
    self.AreaType(data ? (data.AreaType || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
    self.QuayCode(data ? data.QuayCode : null);
    self.BerthCode(data ? data.BerthCode : null);
    self.BerthKey(data ? data.BerthKey : null);
    self.DesignDepth(data ? (data.DesignDepth || "") : "");
    self.DPAWorkflowInstanceID(data ? (data.DPAWorkflowInstanceID || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.LocationName(data ? (data.LocationName || "") : "");

    self.DredgingMaterial(data ? (data.DredgingMaterial || "") : "");
    self.RequireDate(data ? (data.RequireDate || "") : "");
    self.MonthValue(data ? (data.MonthValue || "") : "");
    self.OccupationFrom(data ? data.OccupationFrom : null);
    self.OccupationTo(data ? data.OccupationTo : null);
  //  self.DredgingPriorityAreasVO(data ? ko.utils.arrayMap(data.DredgingPriorityAreasVO, function (AddPrioritysData) { return new IPMSROOT.AddPriorityData(AddPrioritysData); }) : []);
    self.DredgingPriorityDocumentsVO(data ? ko.utils.arrayMap(data.DredgingPriorityDocumentsVO, function (document) { return new IPMSROOT.dredgingdocument(document); }) : []);
    self.cache.latestData = data;
}
IPMSROOT.DredgingPriorityModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}
IPMSROOT.DredgingPriorityAreaModel.prototype.set = function (data) {
    var self = this;
    self.TypeCode(data ? (data.TypeCode || "") : "");
    
    self.AreaLocationID(data ? (data.AreaLocationID || "") : "");
    self.AreaName(data ? (data.AreaName || "") : "");
    self.AreaType(data ? (data.AreaType || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");
    self.QuayCode(data ? data.QuayCode : null);
    self.BerthCode(data ? data.BerthCode : null);
    self.BerthKey(data ? data.BerthKey : null);
    self.LocationName(data ? (data.LocationName || "") : "");

    self.DredgingType(data ? (data.DredgingType || "") : "");
    self.DredgingMaterial(data ? (data.DredgingMaterial || "") : "");

    self.RequiredDate(data ? (moment(data.RequiredDate).format('YYYY-MM-DD') || "") : "");
    self.Requirement(data ? (data.Requirement || "") : "");
    self.Priority(data ? (data.Priority || "") : "");
    self.PromulgateDepth(data ? (data.PromulgateDepth || "") : "");
    self.DPARemarks(data ? (data.DPARemarks || "") : "");
    self.DesignDepth(data ? (data.DesignDepth || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.DredgingPriorityAreaModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



