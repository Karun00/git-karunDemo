(function (ipmsRoot) {

    var BerthMaintenanceReferenceData = function (data) {
        var self = this;
        self.MaintenanceTypes = ko.observableArray(data ? $.map(data.MaintenanceTypes, function (item) { return new MaintenanceType(item); }) : []);
        self.DepartmentTypes = ko.observableArray(data ? $.map(data.DepartmentTypes, function (item) { return new DepartmentType(item); }) : []);
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);

    }

    var MaintenanceType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var DepartmentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var Berth = function (data) {
        var self = this;
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");

    }

    var BerthMaintenanceModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable();
        self.BerthMaintenanceID = ko.observable("");
        self.PortCode = ko.observable("");
        self.ProjectNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.MaintenanceTypeCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.MaintenanceType = ko.observable("");
        self.BerthName = ko.observable();
        self.BollardsFrom = ko.observable();
        self.BollardsTo = ko.observable();
        // Berth 
        self.MaintPortCode = ko.observable("");
        self.MaintQuayCode = ko.observable("");
        self.MaintBerthCode = ko.observable("");

        // Bolloard From
        self.FromPortCode = ko.observable("");
        self.FromQuayCode = ko.observable("");
        self.FromBerthCode = ko.observable("");
        self.FromBollard = ko.observable("");
        // Bollard To
        self.ToPortCode = ko.observable("");
        self.ToQuayCode = ko.observable("");
        self.ToBerthCode = ko.observable("");
        self.ToBollard = ko.observable("");

        self.BerthKey = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.BollardName = ko.observable();
        self.FromBollardKey = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.ToBollardKey = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });

        self.PeriodFrom = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please Select the Period From Date & time' } });
        self.PeriodTo = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please Select the Period To Date & time' } });
        self.OccupationTypeCode = ko.observable(data ? data.OccupationTypeCode : "F");
        self.Precinct = ko.observable("");
        self.DisciplineCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.SpecialConditions = ko.observable("");
        self.Description = ko.observable("");
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.OccupTypes = ko.observableArray([]);
        self.startDate = ko.observable();
        self.Status = ko.observable("");
        self.BerthMaintenanceNo = ko.observable();
        self.WorkflowInstanceId = ko.observable();

        //self.Statust = ko.computed(function () {
        //    return self.RecordStatus() == 'A' ? "Active" : "In Active";
        //});


        self.WorkFlowRemarks = ko.observable();

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);


        self.StatusSort;
        self.Status.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.OccupationTypeCodet = ko.computed(function () {
            return self.OccupationTypeCode() == 'F' ? "Full" : "Partial";
        });

        self.ReferenceNoSort;
        self.BerthMaintenanceNo.subscribe(function (value) {
            self.ReferenceNoSort = value;
        });
        self.ProjectNoSort;
        self.ProjectNo.subscribe(function (value) {
            self.ProjectNoSort = value;
        });
        self.MaintenanceTypeCodeSort;
        self.MaintenanceType.subscribe(function (value) {
            self.MaintenanceTypeCodeSort = value;
        });
        self.MaintBerthCodeSort;
        self.BerthName.subscribe(function (value) {
            self.MaintBerthCodeSort = value;
        });
        self.FromBollardSort;
        self.BollardsFrom.subscribe(function (value) {
            self.FromBollardSort = value;
        });
        self.ToBollardSort;
        self.BollardsTo.subscribe(function (value) {
            self.ToBollardSort = value;
        });

        self.PeriodFromSort;
        self.PeriodFrom.subscribe(function (value) {
            self.PeriodFromSort = value;
        });
        self.PeriodToSort;
        self.PeriodTo.subscribe(function (value) {
            self.PeriodToSort = value;
        });
        self.OccupationTypeCodeSort;
        self.OccupationTypeCode.subscribe(function (value) {
            self.OccupationTypeCodeSort = value;
        });

        self.EditPending = ko.computed(function () {
            if (self.Status() == "Approve") {
                return false;
            }
            else if (self.Status() == "Reject") {
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

    ipmsRoot.BerthMaintenanceModel = BerthMaintenanceModel;
    ipmsRoot.BerthMaintenanceReferenceData = BerthMaintenanceReferenceData;
    ipmsRoot.Berth = Berth;
    ipmsRoot.MaintenanceType = MaintenanceType;
    ipmsRoot.DepartmentType = DepartmentType;

}(window.IPMSROOT));

IPMSROOT.BerthMaintenanceModel.prototype.set = function (data) {
    var self = this;
    self.BerthMaintenanceID(data ? (data.BerthMaintenanceID || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.ProjectNo(data ? (data.ProjectNo || "") : "");
    self.MaintenanceTypeCode(data ? (data.MaintenanceTypeCode || "") : "");
    self.MaintenanceType(data ? (data.MaintenanceType || "") : "");
    //  self.Status(data ? (data.Status || "") : "");
    self.Status(data ? (data.Status || "") : "");

    self.MaintPortCode(data ? (data.MaintPortCode || "") : "");
    self.MaintQuayCode(data ? (data.MaintQuayCode || "") : "");
    self.MaintBerthCode(data ? (data.MaintBerthCode || "") : "");
    self.BerthName(data ? (data.BerthName || "") : "");

    self.FromPortCode(data ? (data.FromPortCode || "") : "");
    self.FromQuayCode(data ? (data.FromQuayCode || "") : "");
    self.FromBerthCode(data ? (data.FromBerthCode || "") : "");
    self.FromBollard(data ? (data.FromBollard || "") : "");
    self.BollardsFrom(data ? (data.BollardsFrom || "") : "");
    self.BollardsTo(data ? (data.BollardsTo || "") : "");


    self.ToPortCode(data ? (data.ToPortCode || "") : "");
    self.ToQuayCode(data ? (data.ToQuayCode || "") : "");
    self.ToBerthCode(data ? (data.ToBerthCode || "") : "");
    self.ToBollard(data ? (data.ToBollard || "") : "");


    self.PeriodFrom(data ? (moment(data.PeriodFrom).format('YYYY-MM-DD HH:mm') || "") : "");
    self.PeriodTo(data ? (moment(data.PeriodTo).format('YYYY-MM-DD HH:mm') || "") : "");

    self.DisciplineCode(data ? (data.DisciplineCode || "") : "");
    self.OccupationTypeCode(data ? data.OccupationTypeCode : "F");
    self.Precinct(data ? (data.Precinct || "") : "");
    self.SpecialConditions(data ? (data.SpecialConditions || "") : "");
    self.Description(data ? (data.Description || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.BerthKey(data ? (data.BerthKey || "") : "");
    self.FromBollardKey(data ? (data.FromBollardKey || "") : "");
    self.ToBollardKey(data ? (data.ToBollardKey || "") : "");
    self.BerthMaintenanceNo(data ? (data.BerthMaintenanceNo || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || "") : "");
    self.BollardName(data ? (data.BollardName || "") : "");


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.BerthMaintenanceModel.prototype.reset = function () {
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