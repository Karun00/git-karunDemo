(function (ipmsRoot) {

    //DivingRequest DivingRequestDriver Model
    var DivingRequestDrivers = function (data) {

        var self = this;

        self.DivingRequestDiverID = ko.observable(data ? data.DivingRequestDiverID : "");
        self.DivingRequestID = ko.observable(data ? data.DivingRequestID : "");
        self.DiverName = ko.observable(data ? data.DiverName : "");
        self.TimeArrivedSurface = ko.observable(data ? (data.TimeArrivedSurface != "" ? moment(data.TimeArrivedSurface).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.TimeLeftSurface = ko.observable(data ? (data.TimeLeftSurface != "" ? moment(data.TimeLeftSurface).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.DiverType = ko.observable(data ? data.DiverType : "");
    }

    //DivingRequest DivingCheckList Model
    var DivingCheckLists = function (data) {

        var self = this;

        self.DivingCheckListID = ko.observable(data ? data.DivingCheckListID : "");
        self.DivingRequestID = ko.observable(data ? data.DivingRequestID : "");
        self.DiveReferenceNo = ko.observable(data ? data.DiveReferenceNo : "");
        self.DivingSupervisorName = ko.observable(data ? data.DivingSupervisorName : "");

        self.Date = ko.observable(data ? (data.Date != null ? moment(data.Date).format('YYYY-MM-DD HH:mm') : "" || "") : "");

        self.WBPSDDEDivingStatus = ko.observable(data ? data.WBPSDDEDivingStatus : "");
        self.WBPSheetPileInspectionStatus = ko.observable(data ? data.WBPSheetPileInspectionStatus : "");
        self.WBPLiftingOperationsStatus = ko.observable(data ? data.WBPLiftingOperationsStatus : "");
        self.WBPBouyInspectionStatus = ko.observable(data ? data.WBPBouyInspectionStatus : "");
        self.WBPQuayWallInspectionStatus = ko.observable(data ? data.WBPQuayWallInspectionStatus : "");
        self.WBPConcretePileInspectionStatus = ko.observable(data ? data.WBPConcretePileInspectionStatus : "");
        self.WBPObjectRecoveryStatus = ko.observable(data ? data.WBPObjectRecoveryStatus : "");
        self.WBPDockyardInspectionStatus = ko.observable(data ? data.WBPDockyardInspectionStatus : "");
        self.WBPCordlessComsScubaStatus = ko.observable(data ? data.WBPCordlessComsScubaStatus : "");
        self.WBPCraftInspectionStatus = ko.observable(data ? data.WBPCraftInspectionStatus : "");
        self.WBPHotWorkStatus = ko.observable(data ? data.WBPHotWorkStatus : "");
        self.WBPOtherStatus = ko.observable(data ? data.WBPOtherStatus : "");
        self.WBPOtherDescription = ko.observable(data ? data.WBPOtherDescription : "");
        self.PPEHardHotStatus = ko.observable(data ? data.PPEHardHotStatus : "");
        self.PPEReflectiveVestsStatus = ko.observable(data ? data.PPEReflectiveVestsStatus : "");
        self.PPESafetyGlossesStatus = ko.observable(data ? data.PPESafetyGlossesStatus : "");
        self.PPEGlovesStatus = ko.observable(data ? data.PPEGlovesStatus : "");
        self.PPEOverallStatus = ko.observable(data ? data.PPEOverallStatus : "");
        self.PPESafetyShoesStatus = ko.observable(data ? data.PPESafetyShoesStatus : "");
        self.PPELifeJacketStatus = ko.observable(data ? data.PPELifeJacketStatus : "");
        self.PPEOtherStatus = ko.observable(data ? data.PPEOtherStatus : "");
        self.PPEOtherDescription = ko.observable(data ? data.PPEOtherDescription : "");
        self.EQPUsedCorrectlyStatus = ko.observable(data ? data.EQPUsedCorrectlyStatus : "");
        self.EQPCompetentToUseEquipmentStatus = ko.observable(data ? data.EQPCompetentToUseEquipmentStatus : "");
        self.EQPGoodConditionStatus = ko.observable(data ? data.EQPGoodConditionStatus : "");
        self.EQPInDateStatus = ko.observable(data ? data.EQPInDateStatus : "");
        self.EQPSecuredStatus = ko.observable(data ? data.EQPSecuredStatus : "");
        self.EQPSafetyDevicesInPlaceStatus = ko.observable(data ? data.EQPSafetyDevicesInPlaceStatus : "");
        self.EQPDailyChecksCompletedStatus = ko.observable(data ? data.EQPDailyChecksCompletedStatus : "");
        self.EQPOtherStatus = ko.observable(data ? data.EQPOtherStatus : "");
        self.EQPOtherDescription = ko.observable(data ? data.EQPOtherDescription : "");
        self.PRADivePermitStatus = ko.observable(data ? data.PRADivePermitStatus : "");
        self.PRALockOutProcStatus = ko.observable(data ? data.PRALockOutProcStatus : "");
        self.PRAFlogAlphaStatus = ko.observable(data ? data.PRAFlogAlphaStatus : "");
        self.PRACommunicationNetworkCompletedStatus = ko.observable(data ? data.PRACommunicationNetworkCompletedStatus : "");
        self.PRAWorkPlaceTidyStatus = ko.observable(data ? data.PRAWorkPlaceTidyStatus : "");
        self.PRORequiredStatus = ko.observable(data ? data.PRORequiredStatus : "");
        self.PROSuppliedStatus = ko.observable(data ? data.PROSuppliedStatus : "");
        self.PRORiskAssessmentStatus = ko.observable(data ? data.PRORiskAssessmentStatus : "");
        self.PROTaskKnownUnderstoodStatus = ko.observable(data ? data.PROTaskKnownUnderstoodStatus : "");
        self.PROOnsiteHazardIDStatus = ko.observable(data ? data.PROOnsiteHazardIDStatus : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");

        self.DivingCheckListHazard = ko.observableArray(data ? ko.utils.arrayMap(data.DivingCheckListHazard, function (hazards) {
            return new DivingCheckListHazards(hazards);
        }) : []);

    }

    //DivingCheckList DivingCheckList Model
    var DivingCheckListHazards = function (data) {

        var self = this;

        this.DivingCheckListHazardID = ko.observable(data ? data.DivingCheckListHazardID : "");
        this.DivingCheckListID = ko.observable(data ? data.DivingCheckListID : "");
        this.Hazard = ko.observable(data ? data.Hazard : "");
        this.Cause = ko.observable(data ? data.Cause : "");
        this.Action = ko.observable(data ? data.Action : "");
    }

    //DivingTaskExecutionModel Fills from viewmodel to model and send data to view and Validates cshtml Controls data
    var DivingTaskExecutionModel = function (data) {

        var self = this;

        //Diving Request

        self.DivingRequestID = ko.observable();
        self.DRN = ko.observable();
        self.FromPortCode = ko.observable();
        self.FromQuayCode = ko.observable();
        self.FromQuayName = ko.observable();
        self.FromBerthCode = ko.observable();
        self.FromBerthName = ko.observable();
        self.FromBollardCode = ko.observable();
        self.FromBollardName = ko.observable();
        self.ToPortCode = ko.observable();
        self.ToQuayCode = ko.observable();
        self.ToQuayName = ko.observable();
        self.ToBerthCode = ko.observable();
        self.ToBerthName = ko.observable();
        self.ToBollardCode = ko.observable();
        self.ToBollardName = ko.observable();
        self.RequiredByDate = ko.observable();
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.OtherLocation = ko.observable();
        self.OcupationFromDate = ko.observable();
        self.OcupationToDate = ko.observable();
        self.HoursOfOccupation1 = ko.observable();
        self.StartTime = ko.observable();
        self.StopTime = ko.observable();
        self.HoursOfOccupation2 = ko.observable();
        self.validationEnabled = ko.observable(false);
        self.DivingReferenceNo = ko.observable("");
        self.ChangeLocation = ko.observable();
        self.SupervisorName = ko.observable("");
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
        self.ViewModeForTabs = ko.observable('taskexecution1');
        self.LocationType = ko.observable();
        self.QuayLocation = ko.observable();
        self.MainGas = ko.observable();
        self.Schedule = ko.observable();
        self.LocationName = ko.observable();
        self.ClearanceNo = ko.observable("");

        self.DivingCheckList = ko.observable();

        self.OccupationReasonName = ko.observable();
        self.OccupationReason = ko.observable();


        //self.DivingRequestDivers1 = ko.observableArray(data ? ko.utils.arrayMap(data.DivingRequestDivers1, function (divers) {
        //    return new DivingRequestDrivers(divers);
        //}) : []);

        //self.DivingRequestDivers2 = ko.observableArray(data ? ko.utils.arrayMap(data.DivingRequestDivers2, function (divers) {
        //    return new DivingRequestDrivers(divers);
        //}) : []);

        //self.DivingRequestDivers3 = ko.observableArray(data ? ko.utils.arrayMap(data.DivingRequestDivers3, function (divers) {
        //    return new DivingRequestDrivers(divers);
        //}) : []);

        self.DivingRequestDivers1 = ko.observableArray();

        self.DivingRequestDivers2 = ko.observableArray();

        self.DivingRequestDivers3 = ko.observableArray();

        self.Status = ko.computed(function () {
            if (self.StopTime() != "") {
                return "Completed";
            }
            else {
                return "Pending";
            }
        });

        self.Execute = ko.computed(function () {
            if (self.StopTime() != "") {
                return false;
            }
            else {
                return true;
            }
        });

        self.LocationNameSort;
        self.LocationName.subscribe(function (value) {
            self.LocationNameSort = value;
        });

        self.FromBerthCodeSort;
        self.FromBerthCode.subscribe(function (value) {
            self.FromBerthCodeSort = value;
        });

        self.FromBollardNameSort;
        self.FromBollardName.subscribe(function (value) {
            self.FromBollardNameSort = value;
        });

        self.ToBollardNameSort;
        self.ToBollardName.subscribe(function (value) {
            self.ToBollardNameSort = value;
        });

        self.OcupationFromDateSort;
        self.OcupationFromDate.subscribe(function (value) {
            self.OcupationFromDateSort = value;
        });

        self.OcupationToDateSort;
        self.OcupationToDate.subscribe(function (value) {
            self.OcupationToDateSort = value;
        });

        //self.RemarksSort;
        //self.Remarks.subscribe(function (value) {
        //    self.RemarksSort = value;
        //});
        self.ReasonSort;
        self.OccupationReasonName.subscribe(function (value) {
            self.ReasonSort = value;
        });

        self.StatusSort;
        self.Status.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.DRNSort;
        self.DRN.subscribe(function (value) {
            self.DRNSort = value;
        });

        self.cache = function () { }
        self.set(data);
    }

    ipmsRoot.DivingTaskExecutionModel = DivingTaskExecutionModel;
    ipmsRoot.DivingCheckLists = DivingCheckLists;
    ipmsRoot.DivingCheckListHazards = DivingCheckListHazards;
    ipmsRoot.DivingRequestDrivers = DivingRequestDrivers;

}(window.IPMSROOT));

//DivingTaskExecutionModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.DivingTaskExecutionModel.prototype.set = function (data) {

    var self = this;

    //Diving Request
    self.DivingRequestID(data ? (data.DivingRequestID || "") : "");
    self.DRN(data ? data.DRN : "");
    self.FromPortCode(data ? (data.FromPortCode || "") : "NA");
    self.FromQuayCode(data ? (data.FromQuayCode || "") : "NA");
    self.FromQuayName(data ? (data.FromQuayName || "") : "NA");
    self.FromBerthCode(data ? (data.FromBerthCode || "") : "NA");
    self.FromBerthName(data ? (data.FromBerthName || "") : "NA");
    self.FromBollardCode(data ? (data.FromBollardCode || "") : "NA");
    self.FromBollardName(data ? (data.FromBollardName || "") : "NA");
    self.ToPortCode(data ? (data.ToPortCode || "") : "NA");
    self.ToQuayCode(data ? (data.ToQuayCode || "") : "NA");
    self.ToQuayName(data ? (data.ToQuayName || "") : "NA");
    self.ToBerthCode(data ? (data.ToBerthCode || "") : "NA");
    self.ToBerthName(data ? (data.ToBerthName || "") : "NA");
    self.ToBollardCode(data ? (data.ToBollardCode || "") : "NA");
    self.ToBollardName(data ? (data.ToBollardName || "") : "NA");
    self.RequiredByDate(data ? (data.RequiredByDate || "") : "NA");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.RecordStatus(data ? data.RecordStatus : "A");
    self.CreatedBy(data ? data.CreatedBy : "");
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : "");
    self.ModifiedDate(data ? data.ModifiedDate : "");
    self.OtherLocation(data ? (data.OtherLocation || "") : "NA");
    self.OcupationFromDate(data ? (data.OcupationFromDate != null ? moment(data.OcupationFromDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.OcupationToDate(data ? (data.OcupationToDate != null ? moment(data.OcupationToDate).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.HoursOfOccupation1(data ? (data.HoursOfOccupation1 || "") : "");

    self.StartTime(data ? (data.StartTime != null ? moment(data.StartTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.StopTime(data ? (data.StopTime != null ? moment(data.StopTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.HoursOfOccupation2(data ? (data.HoursOfOccupation2 || '') : '');
    self.DivingReferenceNo(data ? (data.DivingReferenceNo || "") : "");
    self.ChangeLocation(data ? (data.ChangeLocation || "") : "");
    self.SupervisorName(data ? (data.SupervisorName || "") : "");
    self.DiveTenders(data ? (data.DiveTenders || "") : "");

    self.LoggedDiveTimeFrom(data ? (data.LoggedDiveTimeFrom != null ? moment(data.LoggedDiveTimeFrom).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.LoggedDiveTimeTo(data ? (data.LoggedDiveTimeTo != null ? moment(data.LoggedDiveTimeTo).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.TimeDiveOperationCancelled(data ? (data.TimeDiveOperationCancelled != null ? moment(data.TimeDiveOperationCancelled).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.DiveNature(data ? (data.DiveNature || "") : "");
    self.DiverDepth(data ? (data.DiverDepth || "") : "");
    self.BreathingMixture(data ? (data.BreathingMixture || "") : "");
    self.CompressedAir(data ? (data.CompressedAir || "") : "");
    self.DivingEquipmentUsed1(data ? (data.DivingEquipmentUsed1 || "") : "");
    self.DivingEquipmentUsed2(data ? (data.DivingEquipmentUsed2 || "") : "");

    self.TimeLeftWorkshop(data ? (data.TimeLeftWorkshop != null ? moment(data.TimeLeftWorkshop).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.TimeLeftSite(data ? (data.TimeLeftSite != null ? moment(data.TimeLeftSite).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.TimeArrivedWorkshop(data ? (data.TimeArrivedWorkshop != null ? moment(data.TimeArrivedWorkshop).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.TimeArrivedSite(data ? (data.TimeArrivedSite != null ? moment(data.TimeArrivedSite).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.DecompressionTables(data ? (data.DecompressionTables || "") : "");
    self.CommsCheck(data ? (data.CommsCheck || false) : false);
    self.BoilOut(data ? (data.BoilOut || false) : false);
    self.MainGas(data ? (data.MainGas || false) : false);
    self.Schedule(data ? (data.Schedule || "") : "");
    self.Visibility(data ? (data.Visibility || "") : "");
    self.SeaCondition(data ? (data.SeaCondition || "") : "");
    self.UnderWaterCurrents(data ? (data.UnderWaterCurrents || "") : "");
    self.ContaminatedWater(data ? (data.ContaminatedWater || "") : "");
    self.WaterTemperature(data ? (data.WaterTemperature || "") : "");
    self.OccupationReasonName(data ? data.OccupationReasonName : "");
    self.OccupationReason(data ? data.OccupationReason : "");

    self.LostDiveTime(data ? (data.LostDiveTime != null ? moment(data.LostDiveTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");

    self.RepetiveDiveDesignation(data ? (data.RepetiveDiveDesignation || "") : "");
    self.SkiBoat(data ? (data.DivingCheckListID || false) : false);
    self.LDV(data ? (data.LDV || false) : false);
    self.Trailer(data ? (data.Trailer || false) : false);
    self.LocationType(data ? data.LocationType : "");
    self.QuayLocation(data ? data.QuayLocation : "");
    //self.LocationName(data ? data.LocationName != null ? data.LocationName : "NA" : "NA");
    self.LocationName(data ? data.LocationName != null ? data.LocationName : data.QuayLocation : "NA");
    self.ClearanceNo(data ? data.ClearanceNo : "");

    self.DivingCheckList(data ? new IPMSROOT.DivingCheckLists(data.DivingCheckList) : new IPMSROOT.DivingCheckLists());
    self.DivingRequestDivers1(data ? ko.utils.arrayMap(data.DivingRequestDivers1, function (divers) {
        return new IPMSROOT.DivingRequestDrivers(divers);
    }) : []);

    self.DivingRequestDivers2(data ? ko.utils.arrayMap(data.DivingRequestDivers2, function (divers) {
        return new IPMSROOT.DivingRequestDrivers(divers);
    }) : []);

    self.DivingRequestDivers3(data ? ko.utils.arrayMap(data.DivingRequestDivers3, function (divers) {
        return new IPMSROOT.DivingRequestDrivers(divers);
    }) : []);

    self.cache.latestdata = data;
}

IPMSROOT.DivingTaskExecutionModel.prototype.reset = function (data) {
    this.set(this.cache.latestdata);
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}

function ValidateNumeric(event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.]/;

    return charcheck.test(keychar);
}
