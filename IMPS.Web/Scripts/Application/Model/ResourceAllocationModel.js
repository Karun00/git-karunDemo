(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })   
    var ResourceDateFormat = 'YYYY-MM-DD HH:mm';

    // ArrivalNotification ReferenceData Used For Fills The All Dropdowns in arrival notification form
    var ReferenceData = function (data) {
        var self = this;
        self.agent = ko.observable();
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);  //It will have berths list under this port.  Only active berths.
        self.MopsDelays = ko.observableArray(data ? $.map(data.MopsDelays, function (item) { return new MopsDelay(item); }) : []);
        self.DelayReasons = ko.observableArray(data ? $.map(data.DelayReasons, function (item) { return new DelayReason(item); }) : []); //Delayreasons code
        self.FloatingResources = ko.observableArray(data ? $.map(data.FloatingResources, function (item) { return new FloatingResource(item); }) : []);
        self.WaterResources = ko.observableArray(data ? $.map(data.WaterResources, function (item) { return new WaterResource(item); }) : []);
        
    }

    var MopsDelay = function(data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //Delay Reasons Code
    var DelayReason = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var FloatingResource = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : "");
        self.UserName = ko.observable(data ? data.UserName : "");
    }

    var WaterResource = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : "");
        self.UserName = ko.observable(data ? data.UserName : "");
    }

    //ArrivalNotification Berth Model
    var Berth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
    }


    var FromblordreferenceData = function (data) {
        var self = this;
        self.FromBollards = ko.observableArray(data ? $.map(data, function (item) { return new FromBollard(item); }) : []);  //It will have berths list under this port.  Only active berths.
        //   self.FromMooringBollards = ko.observableArray(data ? $.map(data, function (item) { return new FromMooringBollard(item); }) : []);  //It will have berths list under this port.  Only active berths.

    }

    var FromBollard = function (data) {
        var self = this;
        self.BollardName = ko.observable(data ? data.BollardName : "");
        self.ToBollardKey = ko.observable(data ? data.BolardKey : "");
        self.FromMeter = ko.observable(data ? data.FromMeter : "");
        self.ToMeter = ko.observable(data ? data.ToMeter : "");
    }
    
    var ToblordreferenceData = function (data) {
        var self = this;
        self.ToBollards = ko.observableArray(data ? $.map(data, function (item) { return new ToBollard(item); }) : []);  //It will have berths list under this port.  Only active berths.

    }
    var ToBollard = function (data) {
        var self = this;
        self.BollardName = ko.observable(data ? data.BollardName : "");
        self.ToBollardKey = ko.observable(data ? data.BolardKey : "");
        self.FromMeter = ko.observable(data ? data.FromMeter : "");
        self.ToMeter = ko.observable(data ? data.ToMeter : "");
    }

    var ShiftingBerthingTaskExecutionDetails = function (data) {
        var self = this;

        self.IsValidationEnabled = ko.observable(false);
        self.BerthingTaskExecutionID = ko.observable(data ? data.BerthingTaskExecutionID : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.StartTime = ko.observable(data ? (data.StartTime ? moment(data.StartTime).format(ResourceDateFormat) : "") : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter start time.' } });
        self.EndTime = ko.observable(data ? (moment(data.EndTime).format(ResourceDateFormat)) : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter end time.' } });
        self.WaitingStartTime = ko.observable(data ? (data.WaitingStartTime ? moment(data.WaitingStartTime).format(ResourceDateFormat) : "") : "");
        self.WaitingEndTime = ko.observable(data ? (data.WaitingEndTime ? moment(data.WaitingEndTime).format(ResourceDateFormat) : "") : "");
        self.DelayReason = ko.observable(data ? data.DelayReason : "");
        self.DelayOtherReason = ko.observable(data ? data.DelayOtherReason : "");
        self.FromBerthPortCode = ko.observable(data ? data.FromBerthPortCode : "");
        self.FromBerthQuayCode = ko.observable(data ? data.FromBerthQuayCode : "");
        self.FromBerthCode = ko.observable(data ? data.FromBerthCode : "");
        self.ToBerthPortCode = ko.observable(data ? data.ToBerthPortCode : "");
        self.ToBerthQuayCode = ko.observable(data ? data.ToBerthQuayCode : "");
        self.ToBerthCode = ko.observable(data ? data.ToBerthCode : "");
        self.BerthingSide = ko.observable(data ? data.BerthingSide : "");
        self.FromBollardPortCode = ko.observable(data ? data.FromBollardPortCode : "");
        self.FromBollardQuayCode = ko.observable(data ? data.FromBollardQuayCode : "");
        self.FromBollardBerthCode = ko.observable(data ? data.FromBollardBerthCode : "");
        self.FromBollardCode = ko.observable(data ? data.FromBollardCode : "");
        self.ToBollardPortCode = ko.observable(data ? data.ToBollardPortCode : "");
        self.ToBollardQuayCode = ko.observable(data ? data.ToBollardQuayCode : "");
        self.ToBollardBerthCode = ko.observable(data ? data.ToBollardBerthCode : "");
        self.ToBollardCode = ko.observable(data ? data.ToBollardCode : "");
        self.MooringBollardBowPortcode = ko.observable(data ? data.MooringBollardBowPortcode : "");
        self.MooringBollardBowQuayCode = ko.observable(data ? data.MooringBollardBowQuayCode : "");
        self.MooringBollardBowBerthCode = ko.observable(data ? data.MooringBollardBowBerthCode : "");
        self.MooringBollardBowBollardCode = ko.observable(data ? data.MooringBollardBowBollardCode : "");
        self.MooringBollardStemPortcode = ko.observable(data ? data.MooringBollardStemPortcode : "");
        self.MooringBollardStemQuayCode = ko.observable(data ? data.MooringBollardStemQuayCode : "");
        self.MooringBollardStemBerthCode = ko.observable(data ? data.MooringBollardStemBerthCode : "");
        self.MooringBollardStemBollardCode = ko.observable(data ? data.MooringBollardStemBollardCode : "");
        self.FirstLineIn = ko.observable(data ? (data.FirstLineIn ? moment(data.FirstLineIn).format(ResourceDateFormat) : "") : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter First Line In.' } });
        self.LastLineIn = ko.observable(data ? (data.LastLineIn ? moment(data.LastLineIn).format(ResourceDateFormat) : "") : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Last Line In.' } });
        self.FirstLineOut = ko.observable(data ? (data.FirstLineOut ? moment(data.FirstLineOut).format(ResourceDateFormat) : "") : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter First Line Off.' } });
        self.LastLineOut = ko.observable(data ? (data.LastLineOut ? moment(data.LastLineOut).format(ResourceDateFormat) : "") : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Last Line Off.' } });
        self.ForwardDraft = ko.observable(data ? data.ForwardDraft : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Forward Draft.' } });
        self.AftDraft = ko.observable(data ? data.AftDraft : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Aft Draft.' } });
        self.Remarks = ko.observable(data ? ((data.Remarks != "" && data.Remarks != null && data.Remarks != undefined && data.Remarks != 'null') ? data.Remarks : "") : "");
        self.Deficiencies = ko.observable(data ? ((data.Deficiencies != "" && data.Deficiencies != null && data.Deficiencies != undefined && data.Deficiencies != 'null') ? data.Deficiencies : "") : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.FromBerthKey = ko.observable(data ? data.FromBerthKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter From Berth.' } });
        self.FromBolardKey = ko.observable(data ? data.FromBolardKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter From Bollard.' } });
        self.ToBerthKey = ko.observable(data ? data.ToBerthKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter To Berth.' } });
        self.ToBolardKey = ko.observable(data ? data.ToBolardKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter To Bollard (Stern).' } });
        self.MooringBolardBowKey = ko.observable(data ? data.MooringBolardBowKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Mooring Bollard(BOW).' } });
        self.MooringBolardStemKey = ko.observable(data ? data.MooringBolardStemKey : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter Mooring Bollard(stern).' } });
        self.VesselLength = ko.observable(data ? (data.VesselLength != null ? data.VesselLength : "") : "");
    }

    var PilotageServiceRecordingDetails = function (data) {
        var self = this;
        self.IsValidationEnabled = ko.observable(false);
        self.PilotageServiceRecordingID = ko.observable(data ? data.PilotageServiceRecordingID : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.StartTime = ko.observable(data ? (moment(data.StartTime).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.ActualScheduledTime = ko.observable(data ? (moment(data.ActualScheduledTime).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.EndTime = ko.observable(data ? (moment(data.EndTime).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.PilotOnBoard = ko.observable(data ? (moment(data.PilotOnBoard).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.PilotOff = ko.observable(data ? (moment(data.PilotOff).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.WaitingStartTime = ko.observable(data ? (data.WaitingStartTime ? moment(data.WaitingStartTime).format(ResourceDateFormat) : "") : "");
        self.WaitingEndTime = ko.observable(data ? (data.WaitingEndTime ? moment(data.WaitingEndTime).format(ResourceDateFormat) : "") : "");
        self.AdditionalTugs = ko.observable(data ? data.AdditionalTugs : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.OffSteam = ko.observable(data ? data.OffSteam : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.MarineRevenueCleared = ko.observable(data ? data.MarineRevenueCleared : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.Remarks = ko.observable(data ? ((data.Remarks != "" && data.Remarks != null && data.Remarks != undefined && data.Remarks != 'null') ? data.Remarks : "") : "");
        self.Deficiencies = ko.observable(data ? ((data.Deficiencies != "" && data.Deficiencies != null && data.Deficiencies != undefined && data.Deficiencies != 'null') ? data.Deficiencies : "") : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.DelayReason = ko.observable(data ? data.DelayReason : "");
        //self.MOPSDelay = ko.observable(data ? data.MOPSDelay : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.DelayOtherReason = ko.observable(data ? data.DelayOtherReason : "");
        //anusha 28/05/2024
        self.MOPSDelay = ko.observable(data ? ((data.MOPSDelay != "" && data.MOPSDelay != null && data.MOPSDelay != undefined && data.MOPSDelay != 'null') ? data.MOPSDelay : "") : "");
        //end
    }

    var OtherServiceRecordingDeatils = function (data) { 
        var self = this;
        self.IsValidationEnabled = ko.observable(false);
        self.OtherServiceRecordingID = ko.observable(data ? data.OtherServiceRecordingID : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.StartTime = ko.observable(data ? (moment(data.StartTime).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.EndTime = ko.observable(data ? (moment(data.EndTime).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.LineUp = ko.observable(data ? (moment(data.LineUp).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.LineDown = ko.observable(data ? (moment(data.LineDown).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.PilotOn = ko.observable(data ? (moment(data.PilotOn).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.OpeningMeterReading = ko.observable(data ? data.OpeningMeterReading : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.ClosingMeterReading = ko.observable(data ? data.ClosingMeterReading : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        //self.TotalDispensed = ko.observable(data ? data.TotalDispensed : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.TotalDispensed = ko.observable(data ? data.TotalDispensed : "");
        self.FirstSwing = ko.observable(data ? (moment(data.FirstSwing).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.LastSwing = ko.observable(data ? (moment(data.LastSwing).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.TimeAlongSide = ko.observable(data ? (moment(data.TimeAlongSide).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.BackToQuay = ko.observable(data ? (moment(data.BackToQuay).format(ResourceDateFormat) || "") : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.Extend = ko.observable(data ? data.Extend : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.WaitingStartTime = ko.observable(data ? (data.WaitingStartTime ? moment(data.WaitingStartTime).format(ResourceDateFormat) : "") : "");
        self.WaitingEndTime = ko.observable(data ? (data.WaitingEndTime ? moment(data.WaitingEndTime).format(ResourceDateFormat) : "") : "");
        self.DelayReason = ko.observable(data ? data.DelayReason : "");
        self.DelayOtherReason = ko.observable(data ? data.DelayOtherReason : "");
        self.IsCompleted = ko.observable(data ? data.IsCompleted == true ? "Yes" : "No" : "No");
        self.MeterNo = ko.observable(data ? data.MeterNo : "").extend({ required: { onlyIf: self.IsValidationEnabled } });
        self.WaterStartTime = ko.observable(data ? data.WaterStartTime : "");
        self.WaterEndTime = ko.observable(data ? data.WaterEndTisme : "");
        self.IsTop = ko.observable(data ? data.IsTop : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.Deficiencies = ko.observable(data ? data.Deficiencies : "");
        self.action = ko.observable(data ? data.action : "");


        self.isEditVisible = ko.observable();
        self.isEditVisible = ko.computed(function () {
            if (self.IsTop() == true) {
                return true;
            }
            else {
                return false;
            }
        }, this);

        self.isViewVisible = ko.observable();
        self.isViewVisible = ko.computed(function () {
            if (self.IsTop() == true) {
                return false;
            }
            else {
                return true;
            }
        }, this);     

    }


    var ResourceAllocationModel = function (data) {
        var self = this;
        self.IsValidationEnabled = ko.observable(true);
        self.ResourceAllocationID = ko.observable('');
        self.ServiceReferenceID = ko.observable('');
        self.ServiceReferenceType = ko.observable('');
        self.ResourceID = ko.observable('');
        self.ResourceType = ko.observable('');
        self.OperationType = ko.observable('');
        self.OperationName = ko.observable('');
        self.MovementType = ko.observable('');
        self.MovementTypeName = ko.observable();
        self.Remarks = ko.observable('');
        self.Deficiencies = ko.observable('');
        self.ServiceReferenceTypeName = ko.observable('');
        self.FirstName = ko.observable('');

        self.WaterStartTime = ko.observable('');
        self.WaterEndTime = ko.observable('');
        self.WaitingStartTime = ko.observable('');
        self.WaitingEndTime = ko.observable('');
        self.OpeningMeterReading = ko.observable('');
        self.ClosingMeterReading = ko.observable('');
        self.TotalDispensed = ko.observable('');
        self.DelayReason = ko.observable('');
        self.IsTop = ko.observable('');
        self.MovementDateTime = ko.observable('');
  



        self.VCN = ko.observable('');
        self.OperationTypeName = ko.observable('');
        self.ResourceTypeName = ko.observable('');
        self.TaskStatusName = ko.observable('');

        self.StartTime = ko.observable('').extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter resource start.' } });
        self.ActualScheduledTime = ko.observable('').extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please enter resource start.' } });
        self.EndTime = ko.observable('');
        self.TaskStatus = ko.observable('');
        self.RecordStatus = ko.observable('');

        self.ShiftingBerthingTaskExecution = ko.observable();
        self.PilotageServiceRecording = ko.observable();
        self.OtherServiceRecording = ko.observable();
        self.VesselName = ko.observable();
        self.VesselLength = ko.observable();

        self.StartTimeETAFrom = ko.observable(new Date());
        self.StartTimeTo = ko.observable(new Date());
        self.ResourceName = ko.observable('');
        self.VesselNameSearch = ko.observable('');
        self.VCNSearch = ko.observable('');
        self.BerthName = ko.observable();
        self.BerthKey = ko.observable();
        self.VCNSelected = ko.observable('');
        self.VesselSelected = ko.observable('');
        self.ResourceSelected = ko.observable('');
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
       

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.ServiceReferenceTypeSort;
        self.ServiceReferenceType.subscribe(function (value) {
            self.ServiceReferenceTypeSort = value;
        });

        self.ResourceTypeSort;
        self.FirstName.subscribe(function (value) {
            self.ResourceTypeSort = value;
        });

        self.StartTimeSort;
        self.StartTime.subscribe(function (value) {
            self.StartTimeSort = value;
        });

        self.OperationTypeSort;
        self.OperationType.subscribe(function (value) {
            self.OperationTypeSort = value;
        });

        self.EndTimeSort;
        self.EndTime.subscribe(function (value) {
            self.EndTimeSort = value;
        });

        self.TaskStatusNameSort;
        self.TaskStatusName.subscribe(function (value) {
            self.TaskStatusNameSort = value;
        });

        self.MovementTypeNameSort;
        self.MovementTypeName.subscribe(function (value) {
            self.MovementTypeNameSort = value;
        });

       
        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.FromblordreferenceData = FromblordreferenceData;
    ipmsRoot.ToblordreferenceData = ToblordreferenceData;
    ipmsRoot.Berth = Berth;
    ipmsRoot.FromBollard = FromBollard;
    ipmsRoot.ToBollard = ToBollard;
    ipmsRoot.MopsDelay = MopsDelay;
    // Delay reasons code
    ipmsRoot.DelayReason = DelayReason;
    ipmsRoot.OtherServiceRecordingDeatils = OtherServiceRecordingDeatils;
    ipmsRoot.PilotageServiceRecordingDeatils = PilotageServiceRecordingDetails;
    ipmsRoot.ShiftingBerthingTaskExecutionDetails = ShiftingBerthingTaskExecutionDetails;
    ipmsRoot.ResourceAllocationModel = ResourceAllocationModel;
    ipmsRoot.FloatingResource = FloatingResource;
    ipmsRoot.WaterResource = WaterResource;
    

}(window.IPMSROOT))

IPMSROOT.ResourceAllocationModel.prototype.set = function (data) {
    var self = this;
    self.ResourceAllocationID(data ? (data.ResourceAllocationID || "") : "");
    self.ServiceReferenceType(data ? (data.ServiceReferenceType || "") : "");
    self.ServiceReferenceID(data ? (data.ServiceReferenceID || "") : "");
    self.ResourceID(data ? (data.ResourceID || "") : "");
    self.OperationType(data ? (data.OperationType || "") : "");    
    self.MovementDateTime(data ? (data.MovementDateTime != null ? (moment(data.MovementDateTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");

    self.ServiceReferenceTypeName(data ? (data.ServiceReferenceTypeName || "") : "");
    self.FirstName(data ? (data.FirstName || "") : "");
    self.VCN(data ? (data.VCN || "") : ""); 
    self.OperationTypeName(data ? (data.OperationTypeName || "") : "");
    self.ResourceTypeName(data ? (data.ResourceTypeName || "") : "");
    self.MovementType(data ? (data.MovementType || "") : "");
    self.MovementTypeName(data ? data.MovementTypeName : "");
    self.TaskStatusName(data ? (data.TaskStatus == 'VERF' ? 'Verified' : (data.TaskStatusName || "")) : "");
    self.Remarks(data ? (((data.Remarks != "" && data.Remarks != null && data.Remarks != undefined && data.Remarks != 'null') ? data.Remarks : "") || "") : "");
    self.Deficiencies(data ? (((data.Deficiencies != "" && data.Deficiencies != null && data.Deficiencies != undefined && data.Deficiencies != 'null') ? data.Deficiencies : "") || "") : "");
    self.ResourceType(data ? (data.ResourceType || "") : "");
    //self.StartTime(data ? (data.StartTime != null ? (moment(data.StartTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    //self.ActualScheduledTime(data ? (data.ActualScheduledTime != null ? (moment(data.ActualScheduledTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    //self.EndTime(data ? (data.EndTime != null ? (moment(data.EndTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");

    //anusha 28/05/2024

    //self.StartTime(data ? (data.StartTime != null ? (moment(data.StartTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.StartTime(data ? (data.StartTime != null && data.StartTime != undefined && data.StartTime != 'null' ? (moment(data.StartTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.ActualScheduledTime(data ? (data.ActualScheduledTime != null ? (moment(data.ActualScheduledTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    //self.EndTime(data ? (data.EndTime != null ? (moment(data.EndTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.EndTime(data ? (data.EndTime != null && data.EndTime != undefined && data.EndTime != 'null' ? (moment(data.EndTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");

    // end




    self.TaskStatus(data ? (data.TaskStatus || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.VesselName(data ? data.VesselName : "");
    self.BerthName(data ? data.BerthName : "");
    self.BerthKey(data ? data.BerthKey : "");
    self.VesselLength(data ? data.VesselLength : "");
    self.VCNSelected(data ? data.VCNSelected : "");

    self.WaterStartTime(data ? (data.WaterStartTime != null ? (moment(data.WaterStartTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.WaterEndTime(data ? (data.WaterEndTime != null ? (moment(data.WaterEndTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.WaitingStartTime(data ? (data.WaitingStartTime != null ? (moment(data.WaitingStartTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.WaitingEndTime(data ? (data.WaitingEndTime != null ? (moment(data.WaitingEndTime).format('YYYY-MM-DD HH:mm') || "") : "") : "");
    self.OpeningMeterReading(data ? data.OpeningMeterReading : "");
    self.ClosingMeterReading(data ? data.ClosingMeterReading : "");
    self.TotalDispensed(data ? data.TotalDispensed : "");
    self.DelayReason(data ? data.DelayReason : "");
    self.IsTop(data ? data.IsTop : "");
    self.MeterNo = ko.observable(data ? data.MeterNo : 0);

    self.VesselSelected(data ? data.VesselSelected : "");
    self.ResourceSelected(data ? data.ResourceSelected : "");
    self.ResourceName(data ? data.ResourceName : "");
    self.VesselNameSearch(data ? data.VesselNameSearch : "");
    self.VCNSearch(data ? data.VCNSearch : "");
    self.PilotageServiceRecording(data ? new IPMSROOT.PilotageServiceRecordingDeatils(data.PilotageServiceRecording) : new IPMSROOT.PilotageServiceRecordingDeatils(undefined));
    self.ShiftingBerthingTaskExecution(data ? new IPMSROOT.ShiftingBerthingTaskExecutionDetails(data.ShiftingBerthingTaskExecution) : new IPMSROOT.ShiftingBerthingTaskExecutionDetails(undefined));
    self.OtherServiceRecording(data ? new IPMSROOT.OtherServiceRecordingDeatils(data.OtherServiceRecording) : new IPMSROOT.OtherServiceRecordingDeatils(undefined));
    
    self.cache.latestData = data;
}

IPMSROOT.ResourceAllocationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


//Accept only numeric 
function ValidateNumeric(data, event) {
    if (event.which == 9 || event.which == 0)
        return true;
    else {
        if (window.event) // IE
            keynum = event.keyCode;
        else if (event.which) // Netscape/Firefox/Opera
            keynum = event.which;
        keychar = String.fromCharCode(keynum);
        charcheck = /[0-9\b]/;
        return charcheck.test(keychar);
    }
}
