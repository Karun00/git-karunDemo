(function (ipmsRoot) {

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
    ko.validation.registerExtenders();

    var Reasons = function (data) {

        var self = this;

        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //DivingRequestModel Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var DivingRequestModel = function (data) {

        var self = this;
        self.validationHelper = new IPMSROOT.validationHelper();

        self.validationEnabled = ko.observable(true);
        self.DRN = ko.observable("");
        //self.OccupationReason = ko.observable("").extend({ required: true }); //{ onlyIf: self.validationEnabled, message: '* Please enter Reason' } });;
        self.OccupationReason = ko.observable();
        self.Port = ko.observable("");
        self.LocationorQuay = ko.observable("");

        self.Raisedby = ko.observable("");
        self.Berth = ko.observable("");
        self.DivingRequestID = ko.observable();
        self.OtherLocation = ko.observable("");
        self.QuayLocation = ko.observable("");
        self.FromBerthCode = ko.observable("");
        self.FromBerthName = ko.observable();
        self.FromBollardCode = ko.observable("");
        self.FromBollardName = ko.observable();
        self.ToBollardCode = ko.observable("");
        self.ToBollardName = ko.observable();
        self.Remarks = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Remarks' } });
        self.RequiredByDate = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please Select Required By Date' } });

        self.LocationType = ko.observable("");
        self.FromQuayCode = ko.observable("");
        self.FromQuayName = ko.observable();
        self.FromPortCode = ko.observable();
        self.ToBerthCode = ko.observable();
        self.ToBerthName = ko.observable();
        self.ToQuayCode = ko.observable();
        self.ToQuayName = ko.observable();
        self.ToPortCode = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

        //Added by Srini
        self.OcupationFromDate = ko.observable("");
        self.OcupationToDate = ko.observable("");
        self.HoursOfOccupation1 = ko.observable();
        self.WorkflowInstanceID = ko.observable();

        // Added by Sandeep
        self.StartTime = ko.observable();
        self.StopTime = ko.observable();
        self.HoursOfOccupation2 = ko.observable();
        self.DivingReferenceNo = ko.observable();
        self.ChangeLocation = ko.observable();
        self.SupervisorName = ko.observable();
        self.DiveTenders = ko.observable();
        self.LoggedDiveTimeFrom = ko.observable();
        self.LoggedDiveTimeTo = ko.observable();
        self.TimeDiveOperationCancelled = ko.observable();
        self.DiveNature = ko.observable();
        self.DiverDepth = ko.observable();
        self.BreathingMixture = ko.observable();
        self.CompressedAir = ko.observable();
        self.DivingEquipmentUsed1 = ko.observable();
        self.DivingEquipmentUsed2 = ko.observable();
        self.TimeLeftWorkshop = ko.observable();
        self.TimeLeftSite = ko.observable();
        self.TimeArrivedWorkshop = ko.observable();
        self.TimeArrivedSite = ko.observable();
        self.DecompressionTables = ko.observable();
        self.CommsCheck = ko.observable();
        self.BoilOut = ko.observable();
        self.Visibility = ko.observable();
        self.SeaCondition = ko.observable();
        self.UnderWaterCurrents = ko.observable();
        self.ContaminatedWater = ko.observable();
        self.WaterTemperature = ko.observable();
        self.LostDiveTime = ko.observable();
        self.RepetiveDiveDesignation = ko.observable();
        self.SkiBoat = ko.observable();
        self.LDV = ko.observable();
        self.Trailer = ko.observable();
        self.DivingCheckList = ko.observableArray([]);

        self.LocationTypeName = ko.observable();
        self.OtherLocationName = ko.observable();
        self.QuayLocationName = ko.observable();
        self.Reason = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.DRNSort;
        self.DRN.subscribe(function (value) {
            self.DRNSort = value;
        });

        self.ReasonsSort;
        self.Reason.subscribe(function (value) {
            self.ReasonsSort = value;
        });

        self.ReasonSort;
        self.OccupationReason.subscribe(function (value) {
            self.ReasonSort = value;
        });

        self.QuayLocationSort;
        self.QuayLocation.subscribe(function (value) {
            self.QuayLocationSort = value;
        });

        self.LocationTypeSort;
        self.LocationType.subscribe(function (value) {
            self.LocationTypeSort = value;
        });

        self.FromBerthNameSort;
        self.FromBerthName.subscribe(function (value) {
            self.FromBerthNameSort = value;
        });

        self.FromBollardNameSort;
        self.FromBollardName.subscribe(function (value) {
            self.FromBollardNameSort = value;
        });

        self.ToBollardNameSort;
        self.ToBollardName.subscribe(function (value) {
            self.ToBollardNameSort = value;
        });

        self.RemarksSort;
        self.Remarks.subscribe(function (value) {
            self.RemarksSort = value;
        });

        self.RequiredByDateSort;
        self.RequiredByDate.subscribe(function (value) {
            self.RequiredByDateSort = value;
        });

        self.OcupationFromDateSort = ko.computed(function () {
            if (self.OcupationFromDate() != "") {
                return moment(self.OcupationFromDate()).format('YYYY-MM-DD hh:mm');
            }
            else {
                return "";
            }
        });

        self.OcupationToDateSort = ko.computed(function () {
            if (self.OcupationToDate() != "") {
                return moment(self.OcupationToDate()).format('YYYY-MM-DD hh:mm');
            }
            else {
                return "";
            }
        });

        self.HoursOfOccupation1Sort;
        self.HoursOfOccupation1.subscribe(function (value) {
            self.HoursOfOccupation1Sort = value;
        });

        self.Statust = ko.computed(function () {
            if (self.OcupationToDate() != "") {
                self.Statustt = ko.observable("Completed");
                return "Completed";
            }
            else {
                self.Statustt = ko.observable("Pending");
                return "Pending";
            }
        });

        self.StatusSort;
        self.Statust.subscribe(function (value) {
            self.StatusSort = value;
        });

        //self.StatusSort;
        //self.StatusSort.subscribe(function (value) {
        //    self.Statust = value;
        //});


        //add pending task

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        // end


        self.Raise = ko.computed(function () {
            if (self.OcupationToDate() != "") {
                return false;
            }
            else {
                return true;
            }
        });

        self.cache = function () { };
        self.set(data);
    };



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
    ipmsRoot.Reasons = Reasons;


    ipmsRoot.DivingRequestModel = DivingRequestModel;
}(window.IPMSROOT));

//DivingRequestModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.DivingRequestModel.prototype.set = function (data) {
    var self = this;

    self.DRN(data ? (data.DRN || "") : "");
    self.OccupationReason(data ? data.OccupationReason : "");

    self.OtherLocation(data ? (data.OtherLocation || "") : "");
    self.QuayLocation(data ? (data.QuayLocation || "") : "");
    self.LocationType(data ? (data.LocationType || "Q") : "Q");
    self.FromQuayCode(data ? (data.FromQuayCode || "") : "");
    self.FromQuayName(data ? (data.FromQuayName || "") : "");
    self.FromBerthCode(data ? (data.FromBerthCode || "") : "");
    self.FromBerthName(data ? (data.FromBerthName || "") : "");
    self.FromBollardCode(data ? (data.FromBollardCode || "") : "");
    self.FromBollardName(data ? (data.FromBollardName || "") : "");
    self.ToBollardCode(data ? (data.ToBollardCode || "") : "");
    self.ToBollardName(data ? (data.ToBollardName || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.RequiredByDate(data ? (data.RequiredByDate || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");

    // Added by Srini
    self.Port(data ? (data.Port || "") : "");

    self.Raisedby(data ? (data.Raisedby || "") : "");
    self.LocationorQuay(data ? (data.LocationorQuay || "") : "");
    self.Berth(data ? (data.Berth || "") : "");

    self.FromPortCode(data ? (data.FromPortCode || "") : "");
    self.ToBerthCode(data ? (data.ToBerthCode || "") : "");
    self.ToBerthName(data ? (data.ToBerthName || "") : "");
    self.ToQuayCode(data ? (data.ToQuayCode || "") : "");
    self.ToQuayName(data ? (data.ToQuayName || "") : "");
    self.ToPortCode(data ? (data.ToPortCode || "") : "");
    self.DivingRequestID(data ? (data.DivingRequestID || "") : "");
    self.OcupationFromDate(data ? (data.OcupationFromDate || "") : "");
    self.OcupationToDate(data ? (data.OcupationToDate || "") : "");
    self.HoursOfOccupation1(data ? (data.HoursOfOccupation1 || '') : '');

    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || '') : '');
    // Added by Sandeep
    self.StartTime(data ? (data.StartTime != null ? data.StartTime : "") : "");
    self.StopTime(data ? (data.StopTime != null ? data.StopTime : "") : "");
    self.HoursOfOccupation2(data ? (data.HoursOfOccupation2 || "") : "");
    self.DivingReferenceNo(data ? (data.DivingReferenceNo || "") : "");
    self.ChangeLocation(data ? (data.ChangeLocation || "") : "");
    self.SupervisorName(data ? (data.SupervisorName || "") : "");
    self.DiveTenders(data ? (data.DiveTenders || "") : "");
    self.LoggedDiveTimeFrom(data ? (data.LoggedDiveTimeFrom || "") : "");
    self.LoggedDiveTimeTo(data ? (data.LoggedDiveTimeTo || "") : "");
    self.TimeDiveOperationCancelled(data ? (data.TimeDiveOperationCancelled || "") : "");
    self.DiveNature(data ? (data.DiveNature || "") : "");
    self.DiverDepth(data ? (data.DiverDepth || "") : "");
    self.BreathingMixture(data ? (data.BreathingMixture || "") : "");
    self.CompressedAir(data ? (data.CompressedAir || "") : "");
    self.DivingEquipmentUsed1(data ? (data.DivingEquipmentUsed1 || "") : "");
    self.DivingEquipmentUsed2(data ? (data.DivingEquipmentUsed2 || "") : "");
    self.TimeLeftWorkshop(data ? (data.TimeLeftWorkshop || "") : "");
    self.TimeLeftSite(data ? (data.TimeLeftSite || "") : "");
    self.TimeArrivedWorkshop(data ? (data.TimeArrivedWorkshop || "") : "");
    self.TimeArrivedSite(data ? (data.TimeArrivedSite || "") : "");
    self.DecompressionTables(data ? (data.DecompressionTables || "") : "");
    self.CommsCheck(data ? (data.CommsCheck || "") : "");
    self.BoilOut(data ? (data.BoilOut || "") : "");
    self.Visibility(data ? (data.Visibility || "") : "");
    self.SeaCondition(data ? (data.SeaCondition || "") : "");
    self.UnderWaterCurrents(data ? (data.UnderWaterCurrents || "") : "");
    self.ContaminatedWater(data ? (data.ContaminatedWater || "") : "");
    self.WaterTemperature(data ? (data.WaterTemperature || "") : "");
    self.LostDiveTime(data ? (data.LostDiveTime || "") : "");
    self.RepetiveDiveDesignation(data ? (data.RepetiveDiveDesignation || "") : "");
    self.SkiBoat(data ? (data.DivingCheckListID || "") : "");
    self.LDV(data ? (data.LDV || "") : "");
    self.Trailer(data ? (data.Trailer || "") : "");
    self.LocationTypeName(data ? (data.Trailer || "") : "");
    self.OtherLocationName(data ? (data.Trailer || "") : "");
    self.QuayLocationName(data ? (data.Trailer || "") : "");
    self.Reason(data ? data.Reason : "");

    self.cache.latestData = data;
}

IPMSROOT.DivingRequestModel.prototype.reset = function () {
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



