(function (ipmsRoot) {

    var BerthMaintenanceCompModel = function (data, RefData) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.BerthMaintenanceCompletionID = ko.observable();
        self.BerthMaintenanceID = ko.observable("").extend({ required: { message: ' This field is required.' } });
        self.CompletionDateTime = ko.observable("").extend({ required: true }); // { message: '* Please select the Completion Date Time' } });
        self.observation = ko.observable("").extend({ required: true }); // { message: '* Please select the Observation' } });
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.BerthMaintData = ko.observableArray();
        self.PortCode = ko.observable();
        self.ProjectNo = ko.observable();
        self.MaintenanceTypeCode = ko.observable();
        self.MaintPortCode = ko.observable();
        self.MaintQuayCode = ko.observable();
        self.MaintBerthCode = ko.observable();
        self.FromPortCode = ko.observable();
        self.FromQuayCode = ko.observable();
        self.FromBerthCode = ko.observable();
        self.FromBollard = ko.observable();
        self.ToPortCode = ko.observable();
        self.ToQuayCode = ko.observable();
        self.ToBerthCode = ko.observable();
        self.ToBollard = ko.observable();
        self.PeriodFrom = ko.observable();
        self.PeriodTo = ko.observable();
        self.OccupationTypeCode = ko.observable();
        self.Precinct = ko.observable();
        self.DisciplineCode = ko.observable();
        self.SpecialConditions = ko.observable();
        self.Description = ko.observable();
        self.WorkFlowStatus = ko.observable();
        self.BerthMaintenanceNo = ko.observable();
        self.WorkflowInstanceId = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        //self.StatusSort;
        //self.Status.subscribe(function (value) {
        //    self.StatusSort = value;
        //});

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.OccupationTypeCode1     = ko.computed(function () {
            return self.OccupationTypeCode() == 'F' ? "Full" : "Partial";
        });

        self.CompletionDateTimeSort;
        self.CompletionDateTime.subscribe(function (value) { self.CompletionDateTimeSort = value; });

        self.ProjectNoSort;
        self.ProjectNo.subscribe(function (value) { self.ProjectNoSort = value; });

        self.MaintenanceTypeCodeSort;
        self.MaintenanceTypeCode.subscribe(function (value) { self.MaintenanceTypeCodeSort = value; });

        self.MaintBerthCodeSort;
        self.MaintBerthCode.subscribe(function (value) { self.MaintBerthCodeSort = value; });

        self.FromBollardSort;
        self.FromBollard.subscribe(function (value) { self.FromBollardSort = value; });

        self.ToBollardSort;
        self.ToBollard.subscribe(function (value) { self.ToBollardSort = value; });

        self.PeriodFromSort;
        self.PeriodFrom.subscribe(function (value) { self.PeriodFromSort = value; });

        self.PeriodToSort;
        self.PeriodTo.subscribe(function (value) { self.PeriodToSort = value; });

        self.OccupationTypeCodeSort;
        self.OccupationTypeCode.subscribe(function (value) { self.OccupationTypeCodeSort = value; });

        self.EditPending = ko.computed(function () {           
            if (self.WorkFlowStatus() == "Approve") {
                return false;
            }
            else if (self.WorkFlowStatus() == "Reject") {
                return false;
            }
            else {
                return true;
            }
        });

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


    var BerthMaintenance = function (data) {
        var self = this;
        self.BerthMaintenanceID = ko.observable(data ? data.BerthMaintenanceID : "");
        self.ProjectNo = ko.observable(data ? data.ProjectNo : "");
        self.MaintenanceTypeCode = ko.observable(data ? data.MaintenanceTypeCode : "");
        self.MaintBerthCode = ko.observable(data ? data.MaintBerthCode : "");
        self.FromBollard = ko.observable(data ? data.FromBollard : "");
        self.ToBollard = ko.observable(data ? data.ToBollard : "");
        self.PeriodFrom = ko.observable(data ? (moment(data.PeriodFrom).format('YYYY-MM-DD HH:mm')) : "");
        self.PeriodTo = ko.observable(data ? (moment(data.PeriodTo).format('YYYY-MM-DD HH:mm')) : "");
        self.OccupationTypeCode = ko.observable(data ? data.OccupationTypeCode : "");
        self.Precinct = ko.observable(data ? data.Precinct : "");
        self.DisciplineCode = ko.observable(data ? data.DisciplineCode : "");
        self.SpecialConditions = ko.observable(data ? data.SpecialConditions : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.BerthMaintenanceNo = ko.observable(data ? data.BerthMaintenanceNo : "");
        
    }

    ipmsRoot.BerthMaintenance = BerthMaintenance;
    ipmsRoot.BerthMaintenanceCompModel = BerthMaintenanceCompModel;


}(window.IPMSROOT));

IPMSROOT.BerthMaintenanceCompModel.prototype.set = function (data) {
    var self = this;
    self.BerthMaintenanceCompletionID(data ? (data.BerthMaintenanceCompletionID || "") : "");
    self.BerthMaintenanceID(data ? (data.BerthMaintenanceID || "") : "");   
    self.CompletionDateTime(data ? (moment(data.CompletionDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.observation(data ? (data.observation == 'NULL' ? "" : data.observation || "") : "");

    self.BerthMaintData(data ? new IPMSROOT.BerthMaintenance(data.BerthMaintenance) : "");
    self.PortCode(data ? (data.PortCode == 'NULL' ? "" : data.PortCode || "") : "");
    self.ProjectNo(data ? (data.ProjectNo == 'NULL' ? "" : data.ProjectNo || "") : "");
    self.MaintenanceTypeCode(data ? (data.MaintenanceTypeCode == 'NULL' ? "" : data.MaintenanceTypeCode || "") : "");
    self.MaintPortCode(data ? (data.MaintPortCode == 'NULL' ? "" : data.MaintPortCode || "") : "");
    self.MaintQuayCode(data ? (data.MaintQuayCode == 'NULL' ? "" : data.MaintQuayCode || "") : "");
    self.MaintBerthCode(data ? (data.MaintBerthCode == 'NULL' ? "" : data.MaintBerthCode || "") : "");
    self.FromPortCode(data ? (data.FromPortCode == 'NULL' ? "" : data.FromPortCode || "") : "");
    self.FromQuayCode(data ? (data.FromQuayCode == 'NULL' ? "" : data.FromQuayCode || "") : "");
    self.FromBerthCode(data ? (data.FromBerthCode == 'NULL' ? "" : data.FromBerthCode || "") : "");
    self.FromBollard(data ? (data.FromBollard == 'NULL' ? "" : data.FromBollard || "") : "");
    self.ToPortCode(data ? (data.ToPortCode == 'NULL' ? "" : data.ToPortCode || "") : "");
    self.ToQuayCode(data ? (data.ToQuayCode == 'NULL' ? "" : data.ToQuayCode || "") : "");
    self.ToBerthCode(data ? (data.ToBerthCode == 'NULL' ? "" : data.ToBerthCode || "") : "");
    self.ToBollard(data ? (data.ToBollard == 'NULL' ? "" : data.ToBollard || "") : "");
    self.PeriodFrom(data ? (moment(data.PeriodFrom).format('YYYY-MM-DD HH:mm') || "") : "");
    self.PeriodTo(data ? (moment(data.PeriodTo).format('YYYY-MM-DD HH:mm') || "") : "");
    self.OccupationTypeCode(data ? (data.OccupationTypeCode == 'NULL' ? "" : data.OccupationTypeCode || "") : "");
    self.Precinct(data ? (data.Precinct == 'NULL' ? "" : data.Precinct || "") : "");
    self.DisciplineCode(data ? (data.DisciplineCode == 'NULL' ? "" : data.DisciplineCode || "") : "");
    self.SpecialConditions(data ? (data.SpecialConditions == 'NULL' ? "" : data.SpecialConditions || "") : "");
    self.Description(data ? (data.Description == 'NULL' ? "" : data.Description || "") : "");    
    self.WorkFlowStatus(data ? (data.WorkFlowStatus || "") : "");
    self.BerthMaintenanceNo(data ? (data.BerthMaintenanceNo || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || null) : null);
    
    

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.BerthMaintenanceCompModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


