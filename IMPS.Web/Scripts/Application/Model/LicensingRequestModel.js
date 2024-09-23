(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })
    var validationMessageText = '* This field is Required';

    // Countries list from Vessel Nationality
    var Countries = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    // LicensingRequestReferenceData Used For Fills The All Dropdowns  those are Document Types and License request Types
    var LicensingRequestReferenceData = function (data) {
        var self = this;

        self.DocumentsTypes = ko.observableArray(data ? $.map(data.DocumentsTypes, function (item) { return new DocumentsType(item); }) : []);
        self.LicensingRequesttypes = ko.observableArray(data ? $.map(data.LicensingRequestTypes, function (item) { return new LicensingRequesttype(item); }) : []);
    }

    //DocumentsTypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var DocumentsType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //LicensingRequesttypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var LicensingRequesttype = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : undefined);
        self.SubCatName = ko.observable(data ? data.SubCatName : undefined);
    }

    //DivingsModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var DivingsDetails = function (data) {
        var self = this;
        self.DivingID = ko.observable(data ? data.DivingID : "");
        self.QualificationsCompetencies = ko.observable(data ? data.QualificationsCompetencies : "");
        self.ProvideDivingPorts = ko.observable(data ? data.ProvideDivingPorts : "N");
        self.YearsProvidingDiving = ko.observable(data ? data.YearsProvidingDiving : "");
        self.RegisteredDepartmentLabour = ko.observable(data ? data.RegisteredDepartmentLabour : "N");
        self.EquipmentPersProtClothing = ko.observable(data ? data.EquipmentPersProtClothing : "");
        self.EquipmentRegisterTestCert = ko.observable(data ? data.EquipmentRegisterTestCert : "N");
        self.EquipmentIncludeTwoRadioSets = ko.observable(data ? data.EquipmentIncludeTwoRadioSets : "N");
        self.QualifyPublLiabInsurance = ko.observable(data ? data.QualifyPublLiabInsurance : "N");
        self.BBBEE = ko.observable(data ? data.BBBEE : "");
    }

    //AddressModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var Address = function (data, addressType) {
        var self = this;
        self.AddressID = ko.observable(data ? data.AddressID : 0);
        var typeOfAddress = "";
        if (addressType !== undefined) {
            typeOfAddress = addressType;
        }
        self.AddressType = ko.observable(data ? data.AddressType : typeOfAddress);
        self.NumberStreet = ko.observable(data ? data.NumberStreet : "");
        self.Suburb = ko.observable(data ? data.Suburb : "");
        self.TownCity = ko.observable(data ? data.TownCity : "");
        self.CountryCode = ko.observable(data ? data.CountryCode : "");
        self.PostalCode = ko.observable(data ? data.PostalCode : "");
    }

    //BunkeringModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var BunkeringDetails = function (data) {
        var self = this;
        self.BunkeringID = ko.observable(data ? data.BunkeringID : "");
        self.ProvideBunkeringPorts = ko.observable(data ? data.ProvideBunkeringPorts : "N");
        self.YearsProvidingBunkering = ko.observable(data ? data.YearsProvidingBunkering : "");
        self.GenlHealthSafetyCertificate = ko.observable(data ? data.GenlHealthSafetyCertificate : "N");
        self.EmployeesSelfInflating = ko.observable(data ? data.EmployeesSelfInflating : "N");
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N");
    }

    //FireEquipmentModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var FireEquipmentDetails = function (data) {
        var self = this;
        self.FireEquipmentID = ko.observable(data ? data.FireEquipmentID : "");
        //self.MemberAssociationsBureaus = ko.observable(data ? data.MemberAssociationsBureaus : "");
        self.EquipmentTradersAssociation = ko.observable(data ? data.EquipmentTradersAssociation : "N");
        self.AutomaticSprinklerInspection = ko.observable(data ? data.AutomaticSprinklerInspection : "N");
        self.FireDetectionInstallers = ko.observable(data ? data.FireDetectionInstallers : "N");
        self.EquipInstallationMaintenance = ko.observable(data ? data.EquipInstallationMaintenance : "N");
        self.YearsProvidingEquipment = ko.observable(data ? data.YearsProvidingEquipment : "");
        self.FireMaintenanceCertificate = ko.observable(data ? data.FireMaintenanceCertificate : "N");
        self.SANS1475permit = ko.observable(data ? data.SANS1475permit : "N");
        self.DOFTASCertificate = ko.observable(data ? data.DOFTASCertificate : "N");
        self.GenlHealthSafetyCertificate = ko.observable(data ? data.GenlHealthSafetyCertificate : "N");
        self.FireDivisionRegistration = ko.observable(data ? data.FireDivisionRegistration : "N");
        self.EquipmentRegisterTestCerti = ko.observable(data ? data.EquipmentRegisterTestCerti : "N");
        self.HardHat = ko.observable(data ? data.HardHat : "N");
        self.SafetyShoes = ko.observable(data ? data.SafetyShoes : "N");
        self.ReflectiveJacket = ko.observable(data ? data.ReflectiveJacket : "N");
        self.SelfInflatingLifeJacket = ko.observable(data ? data.SelfInflatingLifeJacket : "N");
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N");
        self.RiskAssessmentReportDealing = ko.observable(data ? data.RiskAssessmentReportDealing : "N");
        self.CompiledPlanReducingRisk = ko.observable(data ? data.CompiledPlanReducingRisk : "N");
    }

    //FireProtectionModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var FireProtectionDetails = function (data) {
        var self = this;
        self.FireProtectionID = ko.observable(data ? data.FireProtectionID : "").extend({ required: { message: validationMessageText } });
        self.HighRiskLicense = ko.observable(data ? data.HighRiskLicense : "N").extend({ required: { message: validationMessageText } });
        self.YearsProvidingProtection = ko.observable(data ? data.YearsProvidingProtection : "").extend({ required: { message: validationMessageText } });
        self.EmployeesApplQualifications = ko.observable(data ? data.EmployeesApplQualifications : "").extend({ required: { message: validationMessageText } });
        self.SAQAAccreditedBody = ko.observable(data ? data.SAQAAccreditedBody : "N").extend({ required: { message: validationMessageText } });
        self.BasicMarineFireFightingCerti = ko.observable(data ? data.BasicMarineFireFightingCerti : "N").extend({ required: { message: validationMessageText } });
        self.Level1FirstAidCertificate = ko.observable(data ? data.Level1FirstAidCertificate : "N").extend({ required: { message: validationMessageText } });
        self.BreathingApparatusCertificate = ko.observable(data ? data.BreathingApparatusCertificate : "N").extend({ required: { message: validationMessageText } });
        self.GenlHealthSafetyCertificate = ko.observable(data ? data.GenlHealthSafetyCertificate : "N").extend({ required: { message: validationMessageText } });
        self.ApprenticeshipProgramme = ko.observable(data ? data.ApprenticeshipProgramme : "N").extend({ required: { message: validationMessageText } });
        self.EquipmentRegisterTestCerti = ko.observable(data ? data.EquipmentRegisterTestCerti : "N").extend({ required: { message: validationMessageText } });
        self.HardHat = ko.observable(data ? data.HardHat : "N").extend({ required: { message: validationMessageText } });
        self.SafetyShoes = ko.observable(data ? data.SafetyShoes : "N").extend({ required: { message: validationMessageText } });
        self.ReflectiveJacket = ko.observable(data ? data.ReflectiveJacket : "N").extend({ required: { message: validationMessageText } });
        self.SelfInflatingLifeJacket = ko.observable(data ? data.SelfInflatingLifeJacket : "N").extend({ required: { message: validationMessageText } });
        self.FireHelmet = ko.observable(data ? data.FireHelmet : "N").extend({ required: { message: validationMessageText } });
        self.FireCoat = ko.observable(data ? data.FireCoat : "N").extend({ required: { message: validationMessageText } });
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N").extend({ required: { message: validationMessageText } });
        self.CompiledRiskAssessment = ko.observable(data ? data.CompiledRiskAssessment : "N").extend({ required: { message: validationMessageText } });
        self.CompiledPlanReducingRisk = ko.observable(data ? data.CompiledPlanReducingRisk : "N").extend({ required: { message: validationMessageText } });
    }

    //FlootinCraneModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var FlootinCraneDetails = function (data) {
        var self = this;
        self.FloatingCraneID = ko.observable(data ? data.FloatingCraneID : "").extend({ required: { message: validationMessageText } });
        self.QualificationsCompetencies = ko.observable(data ? data.QualificationsCompetencies : "").extend({ required: { message: validationMessageText } });
        self.EmployQualifiedTrainedPers = ko.observable(data ? data.EmployQualifiedTrainedPers : "N").extend({ required: { message: validationMessageText } });
        self.HardHat = ko.observable(data ? data.HardHat : "N").extend({ required: { message: validationMessageText } });
        self.SafetyShoes = ko.observable(data ? data.SafetyShoes : "N").extend({ required: { message: validationMessageText } });
        self.ReflectiveJacket = ko.observable(data ? data.ReflectiveJacket : "N").extend({ required: { message: validationMessageText } });
        self.SelfInflatingLifeJacket = ko.observable(data ? data.SelfInflatingLifeJacket : "N").extend({ required: { message: validationMessageText } });
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N").extend({ required: { message: validationMessageText } });
    }

    //StevedoresModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var StevedoresDetails = function (data) {
        var self = this;
        self.StevedoreID = ko.observable(data ? data.StevedoreID : "");
        self.NASASAMember = ko.observable(data ? data.NASASAMember : "N");
        self.QualifiedFirstAiderPerShift = ko.observable(data ? data.QualifiedFirstAiderPerShift : "N");
        self.QualifiedFirstAiderPerShift50 = ko.observable(data ? data.QualifiedFirstAiderPerShift50 : "N");
        self.FireFightingEmployeesTrained = ko.observable(data ? data.FireFightingEmployeesTrained : "N");
        self.SafetyOfficers = ko.observable(data ? data.SafetyOfficers : "N");
        self.QualifiedExperiencedRiggers = ko.observable(data ? data.QualifiedExperiencedRiggers : "N");
        self.VehicleOperators = ko.observable(data ? data.VehicleOperators : "N");
        self.LiftingEquipmentInspectors = ko.observable(data ? data.LiftingEquipmentInspectors : "N");
        self.Electricians = ko.observable(data ? data.Electricians : "N");
        self.FlagSignalOperators = ko.observable(data ? data.FlagSignalOperators : "N");
        self.Lifting = ko.observable(data ? data.Lifting : "N");
        self.EquipmentInspectors = ko.observable(data ? data.EquipmentInspectors : "");
        self.HazardousCargoHandlers = ko.observable(data ? data.HazardousCargoHandlers : "N");
        self.SafetyHealthEnvironmentalRep = ko.observable(data ? data.SafetyHealthEnvironmentalRep : "N");
        self.OperationalManager = ko.observable(data ? data.OperationalManager : "N");
        self.LiftingEquipment = ko.observable(data ? data.LiftingEquipment : "N");
        self.MotorizedEquipment = ko.observable(data ? data.MotorizedEquipment : "N");
        self.ElectricalEquipment = ko.observable(data ? data.ElectricalEquipment : "N");
        self.PalletsLoadSupportingDevices = ko.observable(data ? data.PalletsLoadSupportingDevices : "N");
        self.HandProtectors = ko.observable(data ? data.HandProtectors : "N");
        self.HeadProtectors = ko.observable(data ? data.HeadProtectors : "N");
        self.EyeFaceProtection = ko.observable(data ? data.EyeFaceProtection : "N");
        self.Footwear = ko.observable(data ? data.Footwear : "N");
        self.ProtectiveClothing = ko.observable(data ? data.ProtectiveClothing : "N");
        self.HearingConservationEquipment = ko.observable(data ? data.HearingConservationEquipment : "N");
        self.RespiratoryEquipment = ko.observable(data ? data.RespiratoryEquipment : "N");
        self.SafetyHarnesses = ko.observable(data ? data.SafetyHarnesses : "N");
        self.OHSAAppointed = ko.observable(data ? data.OHSAAppointed : "N");
        self.SafetyHealthEnvIncidents = ko.observable(data ? data.SafetyHealthEnvIncidents : "N");
        self.OccuHealthSafetyTraining = ko.observable(data ? data.OccuHealthSafetyTraining : "N");
        self.AnnualRefresher = ko.observable(data ? data.AnnualRefresher : "N");
        self.FirstAidBoxesAvailable = ko.observable(data ? data.FirstAidBoxesAvailable : "N");
        self.FacilitiesTreatInjuries = ko.observable(data ? data.FacilitiesTreatInjuries : "N");
        self.EmergencyContactDetails = ko.observable(data ? data.EmergencyContactDetails : "N");
        self.EvacuationProceduresPracticed = ko.observable(data ? data.EvacuationProceduresPracticed : "N");
        self.HousekeepingFacilities = ko.observable(data ? data.HousekeepingFacilities : "N");
        self.WorkerFacilities = ko.observable(data ? data.WorkerFacilities : "N");
        self.StackingStorageFacilities = ko.observable(data ? data.StackingStorageFacilities : "N");
        self.InspectionFacilities = ko.observable(data ? data.InspectionFacilities : "N");
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N");
        self.CompiledRiskAssessment = ko.observable(data ? data.CompiledRiskAssessment : "N");
        self.CompiledPlanReducingRisk = ko.observable(data ? data.CompiledPlanReducingRisk : "N");
    }

    //PestControlModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PestControlDetails = function (data) {
        var self = this;
        self.PestControlID = ko.observable(data ? data.PestControlID : "").extend({ required: { message: validationMessageText } });
        self.AgricultureDeptrelevant = ko.observable(data ? data.AgricultureDeptrelevant : "N").extend({ required: { message: validationMessageText } });
        self.SAQACertificate = ko.observable(data ? data.SAQACertificate : "N").extend({ required: { message: validationMessageText } });
        self.EmployQualifiedTrainedPers = ko.observable(data ? data.EmployQualifiedTrainedPers : "N").extend({ required: { message: validationMessageText } });
        self.HardHat = ko.observable(data ? data.HardHat : "N").extend({ required: { message: validationMessageText } });
        self.SafetyShoes = ko.observable(data ? data.SafetyShoes : "N").extend({ required: { message: validationMessageText } });
        self.ReflectiveJacket = ko.observable(data ? data.ReflectiveJacket : "N").extend({ required: { message: validationMessageText } });
        self.SelfInflatingLifeJacket = ko.observable(data ? data.SelfInflatingLifeJacket : "N").extend({ required: { message: validationMessageText } });
        self.FaceMasks = ko.observable(data ? data.FaceMasks : "N").extend({ required: { message: validationMessageText } });
        self.ProtectiveGloves = ko.observable(data ? data.ProtectiveGloves : "N").extend({ required: { message: validationMessageText } });
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N").extend({ required: { message: validationMessageText } });
    }

    //PollutionControlsModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PollutionControlsDetails = function (data) {
        var self = this;
        self.PollutionControlID = ko.observable(data ? data.PollutionControlID : "");
        self.MemberInstituteWasteMgnt = ko.observable(data ? data.MemberInstituteWasteMgnt : "N");
        self.SAQACertificate = ko.observable(data ? data.SAQACertificate : "N");
        self.EmployQualifiedTrainedPers = ko.observable(data ? data.EmployQualifiedTrainedPers : "N");
        self.HardHat = ko.observable(data ? data.HardHat : "N");
        self.SafetyShoes = ko.observable(data ? data.SafetyShoes : "N");
        self.ReflectiveJacket = ko.observable(data ? data.ReflectiveJacket : "N");
        self.SelfInflatingLifeJacket = ko.observable(data ? data.SelfInflatingLifeJacket : "N");
        self.QualifyPublicLiabilityInsu = ko.observable(data ? data.QualifyPublicLiabilityInsu : "N");
    }

    //AuthorizedContactPersonModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var AuthorizedContactPerson = function (data, AuthorizedContactPersonType) {
        var self = this;
        self.AuthorizedContactPersonID = ko.observable(data ? data.AuthorizedContactPersonID : 0);
        if (AuthorizedContactPersonType !== undefined) {
            typeOfAuthorizedContactPerson = AuthorizedContactPersonType;
        }
        self.AuthorizedContactPersonType = ko.observable(data ? data.AuthorizedContactPersonType : typeOfAuthorizedContactPerson);
        self.FirstName = ko.observable(data ? data.FirstName : "").extend({ required: { message: '* This field is Required.' } });
        self.SurName = ko.observable(data ? data.SurName : "").extend({ required: { message: '* This field is Required.' } });
        self.IdentityNo = ko.observable(data ? data.IdentityNo : "").extend({ required: { message: '* This field is Required.' } });
        self.Designation = ko.observable(data ? data.Designation : "").extend({ required: { message: '* This field is Required.' } });
        self.CellularNo = ko.observable(data ? data.CellularNo : "").extend({ required: { message: '* This field is Required.' } });
        self.EmailID = ko.observable(data ? data.EmailID : "").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is Required.' }, email: { onlyIf: self.validationEnabled, message: '* Please enter valid email address.' }, });

    }

    //PortModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PortReferenceData = function (data) {
        var self = this;
        self.PortRequesttypes = ko.observableArray(data ? $.map(data, function (item) { return new PortRequesttype(item); }) : []);
    }

    var PortRequesttype = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }

    // Bind License Request Document Details
    var LicenseRequestDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.FileName = ko.observable(data ? data.FileName : '');
    }

    var pendingTask = function (data) {
        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* This field is Required.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    //  LicensingRequestModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var LicensingRequestModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.AddressCheckbox = ko.observable("");
        self.WorkflowInstanceID = ko.observable();
        self.licenseRequestPortsArr = ko.observableArray([]);
        self.BusinessAddress = ko.observable();
        self.PostalAddress = ko.observable();
        self.Bunkerings = ko.observable();
        self.FireEquipments = ko.observable();
        self.FireProtections = ko.observable();
        self.FloatingCranes = ko.observable();
        self.PestControls = ko.observable();
        self.Stevedores = ko.observable();
        self.PollutionControls = ko.observable();
        self.Divings = ko.observable();
        self.ReferenceWorkflowInstenceID = ko.observable();
        self.AuthorizedContactPerson = ko.observable();
        self.LicenseRequestID = ko.observable();
        self.LicenseRequestType = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is Required.' } });
        self.ReferenceNo = ko.observable();
        self.RegisteredName = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.TradingName = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.RegistrationNumber = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.VATNumber = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.IncomeTaxNumber = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.SkillsDevLevyNumber = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.TelephoneNo1 = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.FaxNo = ko.observable("").extend({ required: { message: '* This field is Required.' } });
        self.ValidTaxClearanceCertificate = ko.observable();
        self.BBBEEStatus = ko.observable();
        self.VerifiedBBBEEStatus = ko.observable();
        self.BBBEEExemptedMicroEnterprise = ko.observable();
        self.PublicLiabilityInsurance = ko.observable();
        self.ViewModeForTabs = ko.observable('registration');

        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.UploadedFiles = ko.observableArray([]);
        self.Workflowstatus = ko.observable();
        self.LicenseRequestTypeName = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.LicenseRequestDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.LicenseRequestDocuments,
            function (licenseRequestDocument) {
                return new LicenseRequestDocument(licenseRequestDocument);
            }) : []);

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.RecordStatus = ko.observable('A');
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.Workflowstatus = ko.observable('A');
        self.WorkflowStatust = ko.computed(function () {
            return self.Workflowstatus() == 'WFSA' ? "Approved" : "Not Approved";
        });
        self.fullBBBEEStatust = ko.computed(function () {
            return self.BBBEEStatus() == 'Y' ? "Yes" : "No";
        });

        // For Sorting the Grid
        self.LicenseRequestIDSort;
        self.LicenseRequestID.subscribe(function (value) {
            self.LicenseRequestIDSort = value;
        });
        self.LicenseRequestTypeNameSort;
        self.LicenseRequestTypeName.subscribe(function (value) {
            self.LicenseRequestTypeNameSort = value;
        });
        self.RegisteredNameSort;
        self.RegisteredName.subscribe(function (value) {
            self.RegisteredNameSort = value;
        });
        self.RegistrationNumberSort;
        self.RegistrationNumber.subscribe(function (value) {
            self.RegistrationNumberSort = value;
        });
        self.TradingNameSort;
        self.TradingName.subscribe(function (value) {
            self.TradingNameSort = value;
        });

        self.VATNumberSort;
        self.VATNumber.subscribe(function (value) {
            self.VATNumberSort = value;
        });

        self.IncomeTaxNumberSort;
        self.IncomeTaxNumber.subscribe(function (value) {
            self.IncomeTaxNumberSort = value;
        });
        self.SkillsDevLevyNumberSort;
        self.SkillsDevLevyNumber.subscribe(function (value) {
            self.SkillsDevLevyNumberSort = value;
        });
        self.BBBEEStatusSort;
        self.BBBEEStatus.subscribe(function (value) {
            self.BBBEEStatusSort = value;
        });

        $("#TelephoneNo1").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#FaxNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
        $("#CellularNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.LicensingRequestReferenceData = LicensingRequestReferenceData;

    ipmsRoot.LicensingRequesttype = LicensingRequesttype;
    ipmsRoot.DocumentsType = DocumentsType;
    ipmsRoot.Address = Address;
    ipmsRoot.BunkeringDetails = BunkeringDetails;
    ipmsRoot.FireEquipmentDetails = FireEquipmentDetails;
    ipmsRoot.FireProtectionDetails = FireProtectionDetails;
    ipmsRoot.FlootinCraneDetails = FlootinCraneDetails;
    ipmsRoot.PestControlDetails = PestControlDetails;
    ipmsRoot.StevedoresDetails = StevedoresDetails;
    ipmsRoot.DivingsDetails = DivingsDetails;
    ipmsRoot.PollutionControlsDetails = PollutionControlsDetails;
    ipmsRoot.PortReferenceData = PortReferenceData;
    ipmsRoot.AuthorizedContactPerson = AuthorizedContactPerson;
    ipmsRoot.PortRequesttype = PortRequesttype;
    ipmsRoot.LicenseRequestDocument = LicenseRequestDocument;
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.LicensingRequestModel = LicensingRequestModel;
    ipmsRoot.Countries = Countries;

}(window.IPMSROOT));

//LicensingRequestModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.LicensingRequestModel.prototype.set = function (data) {

    var self = this;
    self.LicenseRequestID(data ? (data.LicenseRequestID || "") : "");
    self.LicenseRequestType(data ? (data.LicenseRequestType || "") : "");
    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.ReferenceWorkflowInstenceID(data ? (data.ReferenceWorkflowInstenceID || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.TradingName(data ? (data.TradingName || "") : "");
    self.RegistrationNumber(data ? (data.RegistrationNumber || "") : "");
    self.VATNumber(data ? (data.VATNumber || "") : "");
    self.IncomeTaxNumber(data ? (data.IncomeTaxNumber || "") : "");
    self.SkillsDevLevyNumber(data ? (data.SkillsDevLevyNumber || "") : "");
    self.TelephoneNo1(data ? (data.TelephoneNo1 || "") : "");
    self.FaxNo(data ? (data.FaxNo || "") : "");
    self.AddressCheckbox(data ? (data.AddressCheckbox || false) : false);
    self.ValidTaxClearanceCertificate(data ? (data.ValidTaxClearanceCertificate || "N") : "N");
    self.BBBEEStatus(data ? (data.BBBEEStatus || "") : "");
    self.VerifiedBBBEEStatus(data ? (data.VerifiedBBBEEStatus || "N") : "N");
    self.BBBEEExemptedMicroEnterprise(data ? (data.BBBEEExemptedMicroEnterprise || "N") : "N");
    self.PublicLiabilityInsurance(data ? (data.PublicLiabilityInsurance || "N") : "N");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate || "") : "");
    self.Workflowstatus(data ? (data.Workflowstatus || "") : "");
    self.LicenseRequestTypeName(data ? (data.LicenseRequestTypeName || "") : "");
    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || "") : "");

    // Request Type data Binding
    self.BusinessAddress(data ? new IPMSROOT.Address(data.BusinessAddress) : new IPMSROOT.Address(undefined, "ADPE"));
    self.PostalAddress(data ? new IPMSROOT.Address(data.PostalAddress) : new IPMSROOT.Address(undefined, "ADTE"));
    self.AuthorizedContactPerson(data ? new IPMSROOT.AuthorizedContactPerson(data.AuthorizedContactPerson) : new IPMSROOT.AuthorizedContactPerson(undefined, "CPT1"));
    self.Bunkerings(data ? new IPMSROOT.BunkeringDetails(data.Bunkerings) : new IPMSROOT.BunkeringDetails(undefined));
    self.FireEquipments(data ? new IPMSROOT.FireEquipmentDetails(data.FireEquipments) : new IPMSROOT.FireEquipmentDetails(undefined));
    self.FireProtections(data ? new IPMSROOT.FireProtectionDetails(data.FireProtections) : new IPMSROOT.FireProtectionDetails(undefined));
    self.FloatingCranes(data ? new IPMSROOT.FlootinCraneDetails(data.FloatingCranes) : new IPMSROOT.FlootinCraneDetails(undefined));
    self.PestControls(data ? new IPMSROOT.PestControlDetails(data.PestControls) : new IPMSROOT.PestControlDetails(undefined));
    self.Stevedores(data ? new IPMSROOT.StevedoresDetails(data.Stevedores) : new IPMSROOT.StevedoresDetails(undefined));
    self.PollutionControls(data ? new IPMSROOT.PollutionControlsDetails(data.PollutionControls) : new IPMSROOT.PollutionControlsDetails(undefined));
    self.Divings(data ? new IPMSROOT.DivingsDetails(data.Divings) : new IPMSROOT.DivingsDetails(undefined));

    // For Requested Ports data 
    self.licenseRequestPortsArr(data ? (data.LicenseRequestPortsArr ? $.map(data.LicenseRequestPortsArr, function (item) { return item }) : []) : []);
    self.cache.latestData = data;
}

// Reset Licensing Request Model Data
IPMSROOT.LicensingRequestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

