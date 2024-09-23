(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })
    var Hour24ReportReferenceData = function (data) {
        var self = this;
        self.Naturetypes = ko.observableArray(data ? $.map(data.Naturetypes, function (item) { return new Nature(item); }) : []);
        self.RecordingofIncidents = ko.observableArray(data ? $.map(data.RecordingofIncident, function (item) { return new recordingofIncidentdetails(item); }) : []);
        self.GeneralAgencies = ko.observableArray(data ? $.map(data.GeneralAgencies, function (item) { return new GeneralAgenciesdetails(item); }) : []);
        self.OccupationalHygieneAgencies = ko.observableArray(data ? $.map(data.OccupationalHygieneAgencies, function (item) { return new OccupationalHygieneAgenciesdetails(item); }) : []);
        self.TypeofContact = ko.observableArray(data ? $.map(data.TypeofContact, function (item) { return new TypeofContactdetails(item); }) : []);
        self.StandardActs = ko.observableArray(data ? $.map(data.StandardActs, function (item) { return new StandardActsdetails(item); }) : []);
        self.SubstandardConditions = ko.observableArray(data ? $.map(data.SubstandardConditions, function (item) { return new SubstandardConditionsdetails(item); }) : []);
        self.PersonalFactors = ko.observableArray(data ? $.map(data.PersonalFactors, function (item) { return new PersonalFactorsdetails(item); }) : []);
        self.ControlStepsToPreventRecurrence = ko.observableArray(data ? $.map(data.ControlStepsToPreventRecurrence, function (item) { return new ControlStepsToPreventRecurrencedetails(item); }) : []);
        self.JobFactors = ko.observableArray(data ? $.map(data.JobFactors, function (item) { return new JobFactorsdetails(item); }) : []);
        self.FireDepartment = ko.observableArray(data ? $.map(data.FireDepartment, function (item) { return new FireDepartmentdetails(item); }) : []);
        self.IncidentClassification = ko.observableArray(data ? $.map(data.IncidentClassification, function (item) { return new IncidentClassificationtails(item); }) : []);
        self.DiscriptionofExposedRisk = ko.observableArray(data ? $.map(data.DiscriptionofExposedRisk, function (item) { return new DiscriptionofExposedRiskdetails(item); }) : []);
        self.Section_Record_Incident = ko.observableArray(data ? $.map(data.Section_Record_Incident, function (item) { return new Section_Record_Incidentdetails(item); }) : []);
        self.WeatherTypes = ko.observableArray(data ? $.map(data.WeatherTypes, function (item) { return new WeatherTypedetails(item); }) : []);
        self.Ports = ko.observableArray(data ? $.map(data.Ports, function (item) { return new Port(item); }) : []);
    }

    var Nature = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var recordingofIncidentdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var GeneralAgenciesdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var OccupationalHygieneAgenciesdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var TypeofContactdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var SubstandardConditionsdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var JobFactorsdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var StandardActsdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var PersonalFactorsdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var ControlStepsToPreventRecurrencedetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }

    var FireDepartmentdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var IncidentClassificationtails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var DiscriptionofExposedRiskdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var Section_Record_Incidentdetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var WeatherTypedetails = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

    }
    var Port = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }


    //secton625(G)
    var Section625GDetails = function (data) {
        var self = this;

        self.Section625GID = ko.observable(data ? data.Section625GID : "");
        self.Section625GID(data ? (data.Section625GID || 0) : 0);
        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : "");
        self.Section625ABCDID(data ? (data.Section625ABCDID || 0) : 0);
        self.IncidentDateTime = ko.observable(data ? data.IncidentDateTime : "");
        self.IncidentDateTime(data ? (moment(data.IncidentDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.TimeReported = ko.observable(data ? data.TimeReported : "");
        self.WIWitnessName1 = ko.observable(data ? data.WIWitnessName1 : "");
        self.WIContactNo1 = ko.observable(data ? data.WIContactNo1 : "");
        self.WIWitnessName2 = ko.observable(data ? data.WIWitnessName2 : "");
        self.WIContactNo2 = ko.observable(data ? data.WIContactNo2 : "");
        self.IncidentDescription = ko.observable(data ? data.IncidentDescription : "");
        self.IncidentExtent = ko.observable(data ? data.IncidentExtent : "");
        self.QuantityVolumeMaterial = ko.observable(data ? data.QuantityVolumeMaterial : "");
        self.EstimateDistanceNearestWaterway = ko.observable(data ? data.EstimateDistanceNearestWaterway : "");
        self.ActivityTypeIncident = ko.observable(data ? data.ActivityTypeIncident : "");
        self.IncidentIdentified = ko.observable(data ? data.IncidentIdentified : "");
        self.NameOfComplainant = ko.observable(data ? data.NameOfComplainant : "");
        self.ContactNoOfComplainant = ko.observable(data ? data.ContactNoOfComplainant : "");
        self.LIMinorEnvironmentalIncident = ko.observable(data ? data.LIMinorEnvironmentalIncident : "");
        self.LISignificantEnvironmentalIncident = ko.observable(data ? data.LISignificantEnvironmentalIncident : "");
        self.LIMajorEnvironmentalIncident = ko.observable(data ? data.LIMajorEnvironmentalIncident : "");
        self.ImmediateReleventActionsTaken = ko.observable(data ? data.ImmediateReleventActionsTaken : "");
        self.EnvironmentalImpactDescription = ko.observable(data ? data.EnvironmentalImpactDescription : "");
        self.ContributingFactorsCourses = ko.observable(data ? data.ContributingFactorsCourses : "");
        self.LikelyUnderlyingCauses = ko.observable(data ? data.LikelyUnderlyingCauses : "");

    }

    //secton625(G)Childtable
    var Section625GDetail2Details = function (data) {
        var self = this;
        self.Section625GDetail2ID = ko.observable(data ? data.Section625GDetail2ID : "");
        self.Section625GID = ko.observable(data ? data.Section625GID : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.ResponsiblePerson = ko.observable(data ? data.ResponsiblePerson : "");
        self.TargetCompletion = ko.observable(data ? data.TargetCompletion : "");
        self.TargetCompletion(data ? (moment(data.TargetCompletion).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.DateCompletion = ko.observable(data ? data.DateCompletion : "");
        self.DateCompletion(data ? (moment(data.DateCompletion).format('YYYY-MM-DD HH:mm:ss') || "") : "");

    }

    //secton625(E)
    var Section625EDetails = function (data) {
        var self = this;
        self.Section625EID = ko.observable(data ? data.Section625EID : "");
        self.Section625EID(data ? (data.Section625EID || 0) : 0);
        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : "");
        self.Section625ABCDID(data ? (data.Section625ABCDID || 0) : 0);
        self.IncidentDateTime = ko.observable(data ? data.IncidentDateTime : "");
        self.IncidentDateTime(data ? (moment(data.IncidentDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.TimeReported = ko.observable(data ? data.TimeReported : "");
        self.OwnerNameofStolenItem = ko.observable(data ? data.OwnerNameofStolenItem : "");
        self.OwnerAddress = ko.observable(data ? data.OwnerAddress : "");
        self.TelephoneNo = ko.observable(data ? data.TelephoneNo : "");
        self.MobileNo = ko.observable(data ? data.MobileNo : "");
        self.EmailID = ko.observable(data ? data.EmailID : "").extend({ required: { message: ' This field is required.' }, email: { message: 'Please enter a proper email address.' }, });
        self.IDWhenandWhereStolenDateTime = ko.observable(data ? data.IDWhenandWhereStolenDateTime : "");
        self.IDWhenandWhereStolenDateTime(data ? (moment(data.IDWhenandWhereStolenDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.IDWhenandWhereStolenLocation = ko.observable(data ? data.IDWhenandWhereStolenLocation : "");
        self.IDWhenWasDiscoveredDateTime = ko.observable(data ? data.IDWhenWasDiscoveredDateTime : "");
        self.IDWhenWasDiscoveredDateTime(data ? (moment(data.IDWhenWasDiscoveredDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.IDWhenWasDiscoveredLocation = ko.observable(data ? data.IDWhenWasDiscoveredLocation : "");
        self.TheftOccur = ko.observable(data ? data.TheftOccur : "");
        self.StolenFromBuilding = ko.observable(data ? data.StolenFromBuilding : "");
        self.ISPSBreach = ko.observable(data ? data.ISPSBreach : "");
        self.ProtectTheft = ko.observable(data ? data.ProtectTheft : "");
        self.Circumstances = ko.observable(data ? data.Circumstances : "");
        self.TheftAvoided = ko.observable(data ? data.TheftAvoided : "");
        self.PoliceAdviced = ko.observable(data ? data.PoliceAdviced : "N");
        self.SAPSOBNumber = ko.observable(data ? data.SAPSOBNumber : "");
        self.PoliceStationReportedTo = ko.observable(data ? data.PoliceStationReportedTo : "");

    }
    //secton625(e)Childtable
    var Section625EChildDetails = function (data) {
        var self = this;
        self.Section625EDetailID = ko.observable(data ? data.Section625EDetailID : "");
        self.Section625EID = ko.observable(data ? data.Section625EID : "");
        self.Item = ko.observable(data ? data.Item : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.ReplacementValue = ko.observable(data ? data.ReplacementValue : "");

    }

    //secton625(d)
    var Section625DDetails = function (data) {
        var self = this;
        self.Section625DID = ko.observable(data ? data.Section625DID : "");
        self.Section625DID(data ? (data.Section625DID || 0) : 0);
        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : "");
        self.Section625ABCDID(data ? (data.Section625ABCDID || 0) : 0);
        self.IncidentDateTime = ko.observable(data ? data.IncidentDateTime : "");
        self.IncidentDateTime(data ? (moment(data.IncidentDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.TimeReported = ko.observable(data ? data.TimeReported : "");
        self.SpecifyLocationOfFire = ko.observable(data ? data.SpecifyLocationOfFire : "");
        self.FireDepartmentAttend = ko.observable(data ? data.FireDepartmentAttend : "");
        self.OthersSpecify = ko.observable(data ? data.OthersSpecify : "");
        //self.OthersSpecify(data ? (data.OthersSpecify || null) : null);
        self.FICommercial = ko.observable(data ? data.FICommercial : "");
        self.FIStorage = ko.observable(data ? data.FIStorage : "");
        self.FIIndustry = ko.observable(data ? data.FIIndustry : "");
        self.FITransport = ko.observable(data ? data.FITransport : "");
        self.FIOthers = ko.observable(data ? data.FIOthers : "");
        self.FIMiscillaniousSpecify = ko.observable(data ? data.FIMiscillaniousSpecify : "");
        //self.FIMiscillaniousSpecify(data ? (data.FIMiscillaniousSpecify || null) : null);
        self.ICOthersSpecify = ko.observable(data ? data.ICOthersSpecify : "");
        //self.ICOthersSpecify(data ? (data.ICOthersSpecify || null) : null);
        self.DEROthersSpecify = ko.observable(data ? data.DEROthersSpecify : "");
        //self.DEROthersSpecify(data ? (data.DEROthersSpecify || null) : null);
        self.APDDamage = ko.observable(data ? data.APDDamage : "");
        self.APDMaximumEstimatedFinancialLoss = ko.observable(data ? data.APDMaximumEstimatedFinancialLoss : "");
        self.APDActualLoss = ko.observable(data ? data.APDActualLoss : "");
        self.MEByWhom = ko.observable(data ? data.MEByWhom : "");
        self.MEWithWhatBeforeFire = ko.observable(data ? data.MEWithWhatBeforeFire : "");
        self.MEWithWhatAfterFire = ko.observable(data ? data.MEWithWhatAfterFire : "");
        self.FurtherInformation = ko.observable(data ? data.FurtherInformation : "");
        self.WCWeather = ko.observable(data ? data.WCWeather : "");
        self.WCTemperature = ko.observable(data ? data.WCTemperature : "");
        self.WCWindSpeed = ko.observable(data ? data.WCWindSpeed : "");
        self.WCWindDirection = ko.observable(data ? data.WCWindDirection : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
    }


    //secton625(c)
    var Section625CDetails = function (data) {
        var self = this;
        self.Section625CID = ko.observable(data ? data.Section625CID : "");
        self.Section625CID(data ? (data.Section625CID || 0) : 0);
        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : "");
        self.Section625ABCDID(data ? (data.Section625ABCDID || 0) : 0);
        self.IDIncidentDateTime = ko.observable(data ? data.IDIncidentDateTime : "");
        self.IDIncidentDateTime(data ? (moment(data.IDIncidentDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.IDTimeReported = ko.observable(data ? data.IDTimeReported : "");
        self.IDIncidentSpecificLocation = ko.observable(data ? data.IDIncidentSpecificLocation : "");
        self.WIWitnessName1 = ko.observable(data ? data.WIWitnessName1 : "");
        self.WITelephoneNo1 = ko.observable(data ? data.WITelephoneNo1 : "");
        self.WIWitnessName2 = ko.observable(data ? data.WIWitnessName2 : "");
        self.WITelephoneNo2 = ko.observable(data ? data.WITelephoneNo2 : "");
        self.PIName = ko.observable(data ? data.PIName : "");
        self.PISurname = ko.observable(data ? data.PISurname : "");
        self.PIEmployeeNo = ko.observable(data ? data.PIEmployeeNo : "");
        self.PINoOfDaysLost = ko.observable(data ? data.PINoOfDaysLost : "");
        self.PIGender = ko.observable(data ? data.PIGender : "M");
        self.PIAge = ko.observable(data ? data.PIAge : "");
        self.PIGradePosition = ko.observable(data ? data.PIGradePosition : "");
        self.PIPartOfBody = ko.observable(data ? data.PIPartOfBody : "");
        self.IncidentDescription = ko.observable(data ? data.IncidentDescription : "");
        self.IIInvestigatorName = ko.observable(data ? data.IIInvestigatorName : "");
        self.IIDesignation = ko.observable(data ? data.IIDesignation : "");
        self.IIInvestigationDate = ko.observable(data ? data.IIInvestigationDate : "");
        self.IIInvestigationDate(data ? (moment(data.IIInvestigationDate).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.GAOthersSpecify = ko.observable(data ? data.GAOthersSpecify : "");
        self.GAOthersSpecify(data ? (data.GAOthersSpecify || null) : null);
        self.GAOHAOthersSpecify = ko.observable(data ? data.GAOHAOthersSpecify : "");
        self.GAOHAOthersSpecify(data ? (data.GAOHAOthersSpecify || null) : null);
        self.IDCOthersSpecify = ko.observable(data ? data.IDCOthersSpecify : "");
        self.CurrentControls = ko.observable(data ? data.CurrentControls : "");

    }

    //section625b addtable
    var Section625BUnionDetails = function (data) {
        var self = this;
        self.Section625BUnionID = ko.observable(data ? data.Section625BUnionID : "");
        self.Section625BID = ko.observable(data ? data.Section625BID : "");
        self.UnionName = ko.observable(data ? data.UnionName : "");
        self.TotalMembership = ko.observable(data ? data.TotalMembership : "");
        self.TotalRosteredForShift = ko.observable(data ? data.TotalRosteredForShift : "");
        self.TotalPresent = ko.observable(data ? data.TotalPresent : "");
        self.TotalStrike = ko.observable(data ? data.TotalStrike : "");
        self.TotalLeave = ko.observable(data ? data.TotalLeave : "");
        self.TotalSick = ko.observable(data ? data.TotalSick : "");
        self.ReplacementLeave = ko.observable(data ? data.ReplacementLeave : "");
    }

    //section625c addtable
    var Section625CPreventDetails = function (data) {
        var self = this;
        self.Section625CPreventID = ko.observable(data ? data.Section625CPreventID : "");
        self.Section625CID = ko.observable(data ? data.Section625CID : "");
        self.PreventStep = ko.observable(data ? data.PreventStep : "");
        self.TargetDateTime = ko.observable(data ? data.TargetDateTime : "");
        self.ActionBy = ko.observable(data ? data.ActionBy : "");
        self.ActionBy(data ? (moment(data.ActionBy).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.CompletedDate = ko.observable(data ? data.CompletedDate : "");
        self.TargetDateTime(data ? (moment(data.TargetDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.CompletedDate(data ? (moment(data.CompletedDate).format('YYYY-MM-DD HH:mm:ss') || "") : "");

    }

    //section625c addtable
    var Section625CRecommendedDetails = function (data) {
        var self = this;
        self.Section625CRecommendedID = ko.observable(data ? data.Section625CRecommendedID : "");
        self.Section625CID = ko.observable(data ? data.Section625CID : "");
        self.RecommendedStep = ko.observable(data ? data.RecommendedStep : "");
        self.TargetDateTime = ko.observable(data ? data.TargetDateTime : "");
        self.TargetDateTime(data ? (moment(data.TargetDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");//(data ? (moment(data.TargetDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.ActionBy = ko.observable(data ? data.ActionBy : "");
        self.ActionBy(data ? (moment(data.ActionBy).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.CompletedDate = ko.observable(data ? data.CompletedDate : "");
        self.CompletedDate(data ? (moment(data.CompletedDate).format('YYYY-MM-DD HH:mm:ss') || "") : "");
    }


    //  Section 65(B)
    var Section625BDetails = function (data) {
        var self = this;
        self.Section625BID = ko.observable(data ? data.Section625BID : "");
        self.Section625BID(data ? (data.Section625BID || 0) : 0);
        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : "");
        self.Section625ABCDID(data ? (data.Section625ABCDID || 0) : 0);
        self.IDIndustrialDisputeDateTime = ko.observable(data ? data.IDIndustrialDisputeDateTime : "");
        self.IDIndustrialDisputeDateTime(data ? (moment(data.IDIndustrialDisputeDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.IDTimeReported = ko.observable(data ? data.IDTimeReported : "");
        self.IDDisputeSpecificLocation = ko.observable(data ? data.IDDisputeSpecificLocation : "");
        self.IDTradeUnionName = ko.observable(data ? data.IDTradeUnionName : "");
        self.IDTotalNoOfEmployees = ko.observable(data ? data.IDTotalNoOfEmployees : "");
        self.IDStrikeStatuS = ko.observable(data ? data.IDStrikeStatuS : "");
        self.IDImpactOperations = ko.observable(data ? data.IDImpactOperations : "");
        self.IDViolencePresent = ko.observable(data ? data.IDViolencePresent : "");
        self.IndustrialDisputeDescription = ko.observable(data ? data.IndustrialDisputeDescription : "");
    }

    //  Section 65(A)
    var Section625ABCDDetails = function (data) {
        var self = this;

        self.Section625ABCDID = ko.observable(data ? data.Section625ABCDID : 0);
        self.OperatorName = ko.observable(data ? data.OperatorName : "");
        self.LincseNumber = ko.observable(data ? data.LincseNumber : "");
        self.TOMSLogEntryNo = ko.observable(data ? data.TOMSLogEntryNo : "");
        self.CompanyRegistrationNumber = ko.observable(data ? data.CompanyRegistrationNumber : "");
        self.SiteTerminal = ko.observable(data ? data.SiteTerminal : "");
        self.ChangeControlDateTime = ko.observable(data ? data.ChangeControlDateTime : "");
        self.ChangeControlDateTime(data ? (moment(data.ChangeControlDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
        self.CDName = ko.observable(data ? data.CDName : "");
        self.CDDesignation = ko.observable(data ? data.CDDesignation : "");
        self.CDContactNumber = ko.observable(data ? data.CDContactNumber : "");
        self.CDMobileNumber = ko.observable(data ? data.CDMobileNumber : "");
        self.CDEmailID = ko.observable(data ? data.CDEmailID : "").extend({ required: { message: ' This field is required.' }, email: { message: ' Please enter a proper email address.' }, });
        self.CDAddress = ko.observable(data ? data.CDAddress : "").extend({ required: true });
        self.ChangeControlLicensedOperator = ko.observable(data ? data.ChangeControlLicensedOperator : "");
        self.AnticipatedImpactOnBBBEERating = ko.observable(data ? data.AnticipatedImpactOnBBBEERating : "");

        //  $("#CDContactNumber1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        //$("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

    }

    // Main  65
    var Hour24AndSection625Model = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.Hour24Report625ID = ko.observable(data ? data.Hour24Report625ID : "").extend({ required: true });
        self.PortCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: ' This field is required.' } });
        self.OperatorName = ko.observable(data ? data.OperatorName : "").extend({ required: true });
        self.LincseNumber = ko.observable(data ? data.LincseNumber : "").extend({ required: true });
        self.CDName = ko.observable(data ? data.CDName : "").extend({ required: true });
        self.CDDesignation = ko.observable(data ? data.CDDesignation : "").extend({ required: true });
        self.CDContactNumber = ko.observable(data ? data.CDContactNumber : "").extend({ required: true });
        self.CDMobileNumber = ko.observable(data ? data.CDMobileNumber : "").extend({ required: true });
        self.CDEmailID = ko.observable(data ? data.CDEmailID : "").extend({ required: { message: ' This field is required.' }, email: { message: ' Please enter a proper email address.' }, });
        self.CDAddress = ko.observable(data ? data.CDAddress : "").extend({ required: true });
        self.Timeperiod = ko.observable(data ? data.Timeperiod : "").extend({ required: true });
        self.NatureofOccuranceName = ko.observable();
        self.IODOccuranceDateTime = ko.observable(data ? data.IODOccuranceDateTime : "").extend({ required: true });
        self.IODSpecificLocation = ko.observable(data ? data.IODSpecificLocation : "").extend({ required: true });
        self.IODOccuranceBriefDescription = ko.observable(data ? data.IODOccuranceBriefDescription : "").extend({ required: true });
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");


        self.selectedrecordingofIncidentdetails = ko.observableArray([]);
        self.selectedgeneralagenciesdetails = ko.observableArray([]);
        self.selectedOccupationalHygieneAgencies = ko.observableArray([]);
        self.selectedTypeofContact = ko.observableArray([]);
        self.selectedStandardAct = ko.observableArray([]);
        self.selectedSubstandardCondition = ko.observableArray([]);
        self.selectedPersonalFactors = ko.observableArray([]);
        self.selectedJobFactors = ko.observableArray([]);
        self.selectedControlStepsToPreventRecurrence = ko.observableArray([]);
        self.selectedFireDepartment = ko.observableArray([]);
        self.selectedIncidentClassification = ko.observableArray([]);
        self.selectedDiscriptionofExposedRisk = ko.observableArray([]);
        self.selectedRecordingofIncident = ko.observableArray([]);

        self.section625abcd = ko.observable();
        self.section625b = ko.observable();
        self.section625c = ko.observable();
        self.section625D = ko.observable();
        self.section625E = ko.observable();
        self.section625G = ko.observable();
        // self.section625abcd = ko.observableArray(data ? ko.utils.arrayMap(data.section625abcd, function (commodity) { return new Section625ABCDDetails(commodity); }) : []);
        self.Section625BUnion = ko.observableArray(data ? ko.utils.arrayMap(data.Section625BUnion, function (commodity) { return new Section625BUnionDetails(commodity); }) : []);
        self.Section625CPrevent = ko.observableArray(data ? ko.utils.arrayMap(data.Section625CPrevent, function (commodity) { return new Section625CPreventDetails(commodity); }) : []);
        self.Section625CRecommended = ko.observableArray(data ? ko.utils.arrayMap(data.Section625CRecommended, function (commodity) { return new Section625CRecommendedDetails(commodity); }) : []);
        self.Section625EDetail = ko.observableArray(data ? ko.utils.arrayMap(data.Section625EDetail, function (commodity) { return new Section625EChildDetails(commodity); }) : []);
        self.Section625GDetail2 = ko.observableArray(data ? ko.utils.arrayMap(data.Section625GDetail2, function (commodity) { return new Section625GDetail2Details(commodity); }) : []);
        self.ViewModeForTabs = ko.observable('notification1');
        self.NONatureCode = ko.observable("");
        // self.PortCode = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.OperatorNameSort;
        self.OperatorName.subscribe(function (value) {
            self.OperatorNameSort = value;
        });
        self.LincseNumberSort;
        self.LincseNumber.subscribe(function (value) {
            self.LincseNumberSort = value;
        });
        self.NatureofOccuranceNameSort;
        self.NatureofOccuranceName.subscribe(function (value) {
            self.NatureofOccuranceNameSort = value;
        });
        self.toggleAssociation = function (item) {
            self.NONatureCode(item.SubCatCode());
            return true;
        };


        $("#CDContactNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CDMobileNumber").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.Nature = Nature;
    ipmsRoot.Section625CDetails = Section625CDetails;
    ipmsRoot.Section625DDetails = Section625DDetails;
    ipmsRoot.Section625EDetails = Section625EDetails;
    ipmsRoot.Section625GDetails = Section625GDetails;
    ipmsRoot.Section625GDetail2Details = Section625GDetail2Details;
    ipmsRoot.Section625EChildDetails = Section625EChildDetails;
    ipmsRoot.recordingofIncidentdetails = recordingofIncidentdetails;
    ipmsRoot.GeneralAgenciesdetails = GeneralAgenciesdetails;
    ipmsRoot.OccupationalHygieneAgenciesdetails = OccupationalHygieneAgenciesdetails;
    ipmsRoot.TypeofContactdetails = TypeofContactdetails;
    ipmsRoot.SubstandardConditionsdetails = SubstandardConditionsdetails;
    ipmsRoot.JobFactorsdetails = JobFactorsdetails;
    ipmsRoot.DiscriptionofExposedRiskdetails = DiscriptionofExposedRiskdetails;
    ipmsRoot.IncidentClassificationtails = IncidentClassificationtails;
    ipmsRoot.FireDepartmentdetails = FireDepartmentdetails;
    ipmsRoot.StandardActsdetails = StandardActsdetails;
    ipmsRoot.PersonalFactorsdetails = PersonalFactorsdetails;
    ipmsRoot.ControlStepsToPreventRecurrencedetails = ControlStepsToPreventRecurrencedetails;
    ipmsRoot.Section625CRecommendedDetails = Section625CRecommendedDetails;
    ipmsRoot.Section625CPreventDetails = Section625CPreventDetails;
    ipmsRoot.Section625CDetails = Section625CDetails;
    ipmsRoot.Section625BUnionDetails = Section625BUnionDetails;
    ipmsRoot.Section625BDetails = Section625BDetails;
    ipmsRoot.Section625ABCDDetails = Section625ABCDDetails;
    ipmsRoot.Hour24ReportReferenceData = Hour24ReportReferenceData;
    ipmsRoot.Hour24AndSection625Model = Hour24AndSection625Model;
    ipmsRoot.Port = Port;
}(window.IPMSROOT));

IPMSROOT.Hour24AndSection625Model.prototype.set = function (data) {
    var self = this;

    self.Hour24Report625ID(data ? (data.Hour24Report625ID || 0) : 0);
    self.OperatorName(data ? (data.OperatorName || "") : "");
    self.LincseNumber(data ? (data.LincseNumber || "") : "");
    self.CDName(data ? (data.CDName || "") : "");
    self.CDDesignation(data ? (data.CDDesignation || "") : "");
    self.CDContactNumber(data ? (data.CDContactNumber || "") : "");
    self.CDMobileNumber(data ? (data.CDMobileNumber || "") : "");
    self.CDEmailID(data ? (data.CDEmailID || "") : "");
    self.CDAddress(data ? (data.CDAddress || "") : "");
    self.NONatureCode(data ? (data.NONatureCode || "") : "");
    self.NatureofOccuranceName(data ? (data.NatureofOccuranceName || null) : null);
    self.IODOccuranceDateTime(data ? (moment(data.IODOccuranceDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");
    self.IODSpecificLocation(data ? (data.IODSpecificLocation || "") : "");
    self.IODOccuranceBriefDescription(data ? (data.IODOccuranceBriefDescription || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || 'A') : 'A');
    self.PortCode(data ? (data.PortCode || "") : "");
    self.Timeperiod(data ? (data.Timeperiod || "") : "");
    self.section625abcd(data ? new IPMSROOT.Section625ABCDDetails(data.section625abcd) : new IPMSROOT.Section625ABCDDetails(undefined));

    self.section625b(data ? new IPMSROOT.Section625BDetails(data.section625b) : new IPMSROOT.Section625BDetails(undefined));
    self.section625c(data ? new IPMSROOT.Section625CDetails(data.section625c) : new IPMSROOT.Section625CDetails(undefined));
    self.section625D(data ? new IPMSROOT.Section625DDetails(data.section625D) : new IPMSROOT.Section625DDetails(undefined));
    self.section625E(data ? new IPMSROOT.Section625EDetails(data.section625E) : new IPMSROOT.Section625EDetails(undefined));
    self.section625G(data ? new IPMSROOT.Section625GDetails(data.section625G) : new IPMSROOT.Section625GDetails(undefined));


    self.selectedrecordingofIncidentdetails(data ? (data.selectedrecordingofIncidentdetails ? $.map(data.selectedrecordingofIncidentdetails, function (item) { return item }) : []) : []);
    self.selectedgeneralagenciesdetails(data ? (data.selectedgeneralagenciesdetails ? $.map(data.selectedgeneralagenciesdetails, function (item) { return item }) : []) : []);
    self.selectedOccupationalHygieneAgencies(data ? (data.selectedOccupationalHygieneAgencies ? $.map(data.selectedOccupationalHygieneAgencies, function (item) { return item }) : []) : []);
    self.selectedTypeofContact(data ? (data.selectedTypeofContact ? $.map(data.selectedTypeofContact, function (item) { return item }) : []) : []);
    self.selectedStandardAct(data ? (data.selectedStandardAct ? $.map(data.selectedStandardAct, function (item) { return item }) : []) : []);
    self.selectedSubstandardCondition(data ? (data.selectedSubstandardCondition ? $.map(data.selectedSubstandardCondition, function (item) { return item }) : []) : []);
    self.selectedPersonalFactors(data ? (data.selectedPersonalFactors ? $.map(data.selectedPersonalFactors, function (item) { return item }) : []) : []);
    self.selectedJobFactors(data ? (data.selectedJobFactors ? $.map(data.selectedJobFactors, function (item) { return item }) : []) : []);
    self.selectedControlStepsToPreventRecurrence(data ? (data.selectedControlStepsToPreventRecurrence ? $.map(data.selectedControlStepsToPreventRecurrence, function (item) { return item }) : []) : []);
    self.selectedFireDepartment(data ? (data.selectedFireDepartment ? $.map(data.selectedFireDepartment, function (item) { return item }) : []) : []);
    self.selectedIncidentClassification(data ? (data.selectedIncidentClassification ? $.map(data.selectedIncidentClassification, function (item) { return item }) : []) : []);
    self.selectedDiscriptionofExposedRisk(data ? (data.selectedDiscriptionofExposedRisk ? $.map(data.selectedDiscriptionofExposedRisk, function (item) { return item }) : []) : []);
    self.selectedRecordingofIncident(data ? (data.selectedRecordingofIncident ? $.map(data.selectedRecordingofIncident, function (item) { return item }) : []) : []);



    self.cache.latestData = data;
}
IPMSROOT.Hour24AndSection625Model.prototype.reset = function () {
    this.set(this.cache.latestData);
}
function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$^[]$:/;
    return charcheck.test(keychar);
}
self.ValidateAlphaNumeric = function (data, event) {

    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9]/;
    return charcheck.test(keychar);
}
self.ValidateAlphaNumericWithStarChracter = function (data, event) {

    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9*]/;
    return charcheck.test(keychar);
}

// Numeric 
self.ValidateNumeric = function (data, event) {
    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    //charcheck = /[0-9]/;
    charcheck = /[0-9.\b]/;
    return charcheck.test(keychar);
}

// Numeric 
//Author : Omprakash Kotha
//Dated  : 25th August 2014
//Reason : Added on key press event to check valid or not
self.ValidateNumeric_keypressEvent = function (data, event) {

    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9\b]/g;
    // var result = keychar.match(charcheck)
    return ((keychar.match(charcheck) == null) ? false : true);

}
//Author : Omprakash Kotha
//Dated  : 25th August 2014
//Reason : Added on key press event to check valid or not
// Accept only Alphabets and spaces
self.ValidateAlphabetsWithSpaces_keypressEvent = function (data, event) {

    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z \b]*$/;
    // var result = keychar.match(charcheck)
    return ((keychar.match(charcheck) == null) ? false : true);

}
//Author : Omprakash Kotha
//Dated  : 2nd September 2014
//Reason : Added on key down event to prevent backspace
// Prevent Backspace Button in text field
self.PreventBackspaces_keydownEvent = function (event) {
    CutPaste();
    var evt = event || window.event;
    if (evt) {
        var keyCode = evt.charCode || evt.keyCode;
        if (keyCode === 8 || keyCode === 46) {
            if (evt.preventDefault) {
                evt.preventDefault();
            } else {
                evt.returnValue = false;
            }
        }
    }
}


// Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z \b]*$/;
    return charcheck.test(keychar);
}
// AlphaNumeric with Spaces
self.ValidateAlphaNumericWithSpaces = function (data, event) {
    CutPaste();
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[A-Za-z\d\s\b]*$/;
    return charcheck.test(keychar);
}