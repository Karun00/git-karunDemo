(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });


    var PortEntryPassApplicationReferenceData = function (data) {
        var self = this;
        self.Ports = ko.observableArray(data ? $.map(data.Ports, function (item) { return new PortRequesttype(item); }) : []);
        self.ApplicationCategory = ko.observableArray(data ? $.map(data.ApplicationCategory, function (item) { return new appcategory(item); }) : []);
        self.PermitTypes = ko.observableArray(data ? $.map(data.PermitTypes, function (item) { return new PermitType(item); }) : []);
        self.AreaOfConstruction = ko.observableArray(data ? $.map(data.AreaOfConstruction, function (item) { return new areaofconstruction(item); }) : []);
        self.TemporaryPermits = ko.observableArray(data ? $.map(data.TemporaryPermits, function (item) { return new TemporaryPermit(item); }) : []);
        self.AdhocPermits = ko.observableArray(data ? $.map(data.AdhocPermits, function (item) { return new AdhocPermit(item); }) : []);
        self.OperatedTerminals = ko.observableArray(data ? $.map(data.OperatedTerminals, function (item) { return new OperatedTerminal(item); }) : []);
        self.PermitDocumentTypes = ko.observableArray(data ? $.map(data.PermitDocumentTypes, function (item) { return new PermitDocumentType(item); }) : []);
        self.SecurityDocumentTypes = ko.observableArray(data ? $.map(data.SecurityDocumentTypes, function (item) { return new SecurityDocumentType(item); }) : []);
        self.PermitRequeirements = ko.observableArray(data ? $.map(data.PermitRequeirements, function (item) { return new PermitRequeirement(item); }) : []);
        self.AccessGates = ko.observableArray(data ? $.map(data.AccessGates, function (item) { return new AccessGate(item); }) : []);
        self.PermitRequeirementstypes = ko.observableArray(data ? $.map(data.PermitRequeirementstypes, function (item) { return new PermitRequeirementstype(item); }) : []);
        self.PermitRequeirementsDuration = ko.observableArray(data ? $.map(data.PermitRequeirementsDuration, function (item) { return new PermitRequeirementsDurationS(item); }) : []);
        self.PermitCodes = ko.observableArray(data ? $.map(data.PermitCodes, function (item) { return new Permitcode(item); }) : []);
        self.PermitStatus = ko.observableArray(data ? $.map(data.PermitStatus, function (item) { return new PermitStatusS(item); }) : []);
        self.IndividualTemporaryPermits = ko.observableArray(data ? $.map(data.IndividualTemporaryPermits, function (item) { return new IndividualTemporaryPermit(item); }) : []);
        self.ContractorTemporaryPermits = ko.observableArray(data ? $.map(data.ContractorTemporaryPermits, function (item) { return new ContractorTemporaryPermit(item); }) : []);
        self.ContractorPermanentPermits = ko.observableArray(data ? $.map(data.ContractorPermanentPermits, function (item) { return new ContractorPermanentPermit(item); }) : []);
        self.IndividualPermanentPermits = ko.observableArray(data ? $.map(data.IndividualPermanentPermits, function (item) { return new IndividualPermanentPermit(item); }) : []);
        self.AccessAreasForRBs = ko.observableArray(data ? $.map(data.AccessAreasForRBs, function (item) { return new AccessAreasForRB(item); }) : []);
        self.AccessAreasForCTs = ko.observableArray(data ? $.map(data.AccessAreasForCTs, function (item) { return new AccessAreasForCT(item); }) : []);
        self.AccessAreasForDBs = ko.observableArray(data ? $.map(data.AccessAreasForDBs, function (item) { return new AccessAreasForDB(item); }) : []);
        self.AccessAreasForELs = ko.observableArray(data ? $.map(data.AccessAreasForELs, function (item) { return new AccessAreasForEL(item); }) : []);
        self.AccessAreasForNGs = ko.observableArray(data ? $.map(data.AccessAreasForNGs, function (item) { return new AccessAreasForNG(item); }) : []);
        self.AccessAreasForMBs = ko.observableArray(data ? $.map(data.AccessAreasForMBs, function (item) { return new AccessAreasForMB(item); }) : []);
        self.AccessAreasForSBs = ko.observableArray(data ? $.map(data.AccessAreasForSBs, function (item) { return new AccessAreasForSB(item); }) : []);
        self.AccessAreasForPEs = ko.observableArray(data ? $.map(data.AccessAreasForPEs, function (item) { return new AccessAreasForPE(item); }) : []);
        self.ReasonForPermits = ko.observableArray(data ? $.map(data.ReasonForPermits, function (item) { return new ReasonForPermit(item); }) : []);
        self.ContractorReasonForPermits = ko.observableArray(data ? $.map(data.ContractorReasonForPermits, function (item) { return new ContractorReasonForPermit(item); }) : []);
        self.ddlVesselNationality = ko.observableArray(data ? $.map(data.Country, function (item) { return new CountryOfOrigin(item); }) : []);

        // self.VehiclePermitDtos = ko.observableArray();
    }
    var CountryOfOrigin = function (data) {
        var self = this;
        self.VNName = ko.observable(data ? data.SubCatName : "");
        self.VNCode = ko.observable(data ? data.SubCatCode : "");

    }

    var AdvnceSearchmodel = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselID = ko.observable();
        //self.MovementType = ko.observable();

        self.RequestFrom = ko.observable();
        self.RequestTo = ko.observable();
        self.set(data);
    }

    var PortRequesttype = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }
    var appcategory = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var areaofconstruction = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var TemporaryPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AdhocPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var OperatedTerminal = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitDocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var SecurityDocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirement = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessGate = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirementstype = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirementsDurationS = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var Permitcode = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitStatusS = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var IndividualTemporaryPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var IndividualPermanentPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForRB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var AccessAreasForCT = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForDB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForEL = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForNG = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForMB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForSB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessAreasForPE = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var ContractorTemporaryPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var ContractorPermanentPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var ReasonForPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var ContractorReasonForPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var SubAccessAreasForRB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode + '_' + data.SupCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        self.SupCatCode = ko.observable(data ? data.SupCatCode : "");
    }
    var ViewSubAccessAreasForRB = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        self.SupCatCode = ko.observable(data ? data.SupCatCode : "");
    }

    //****************************************ChildModels***********************************************************************************
    // PermitRequestDocument Used For bind The upload controls from view modelPermitcodes
    var PermitRequestDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
        self.deletevisable = ko.observable(false);

    }
    var PermitRequestArea = function (data) {
        var self = this;
        self.PermitRequestAreaCode = ko.observableArray([]);
        self.PermitRequestAreaCode = ko.observable(data ? data.PermitRequestAreaCode : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitRequestAreaID = ko.observable(data ? data.PermitRequestAreaID : "");
    }
    var PermitReason = function (data) {
        var self = this;
        self.ReasonCode = ko.observableArray([]);
        self.ReasonCode = ko.observable(data ? data.ReasonCode : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select a Reason' } });
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitReasonID = ko.observable(data ? data.PermitReasonID : "");
    }
    var PermitRequestContractor = function (data) {
        var self = this;
        self.PermitRequestContractorID = ko.observable(data ? data.PermitRequestContractorID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.CompanyName = ko.observable(data ? data.CompanyName : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Company Name' } });
        self.DepartmentName = ko.observable(data ? data.DepartmentName : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Department Name' } });
        self.CompanyManagerName = ko.observable(data ? data.CompanyManagerName : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Contractor Company Manager Name' } });
        self.TelephoneNumber = ko.observable(data ? data.TelephoneNumber : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Telephone NUmber' } });
        self.SubContractorManager = ko.observable(data ? data.SubContractorManager : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Sub contractor Manager Name' } });
        self.SubContManagerTelephone = ko.observable(data ? data.SubContManagerTelephone : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Sub contractor Telephone Number' } });
        self.ContractNo = ko.observable(data ? data.ContractNo : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Contract No.' } });
        self.ContractManagerName = ko.observable(data ? data.ContractManagerName : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Contract Manager Name' } });
        self.ContractDuration = ko.observable(data ? data.ContractDuration : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Contract Duration' } });
        self.ServiceCompanyName = ko.observable(data ? data.ServiceCompanyName : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Service Company Name' } });
        self.ResponsibleManager = ko.observable(data ? data.ResponsibleManager : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Responsible Manager' } });
        self.ContactNo = ko.observable(data ? data.ContactNo : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Contact No.' } });
        self.MobileNo = ko.observable(data ? data.MobileNo : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Mobile No.' } });

    }
    var VehiclePermit = function (data) {
        var self = this;
        self.VehiclePermitID = ko.observable(data ? data.VehiclePermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.VehicleMake = ko.observable(data ? data.VehicleMake : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Make' } });
        self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Registration No.' } });
        self.VehicleModel = ko.observable(data ? data.VehicleModel : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Model' } });
        // self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Regn No.' } });
        //self.VehicleModel = ko.observable(data ? data.VehicleModel : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Model' } });
        // self.Chassis_VinNo = ko.observable(data ? data.Chassis_VinNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Chassis or Vin No.' } });
        // self.Colour = ko.observable(data ? data.Colour : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Colour.' } });
        //self.PermitRequirementCode = ko.observable(data ? data.PermitRequirementCode : "");
        self.Reason = ko.observable(data ? data.Reason : "");

    }

    //var IndividualVehiclePermit = function (data) {
    //    var self = this;
    //    self.IndividualVehiclePermitID = ko.observable(data ? data.IndividualVehiclePermitID : "");
    //    self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
    //    self.VehicleMake = ko.observable(data ? data.VehicleMake : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Make' } });
    //    self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Registration No.' } });
    //    self.VehicleModel = ko.observable(data ? data.VehicleModel : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Model' } });       
    //    self.Chassis_VinNo = ko.observable(data ? data.Chassis_VinNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Chassis or Vin No.' } });
    //    self.Colour = ko.observable(data ? data.Colour : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Colour.' } });


    //}
    var IndividualVehiclePermit = function (data) {
        var self = this;
        self.IndividualVehiclePermitID = ko.observable(data ? data.IndividualVehiclePermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.VehicleMake = ko.observable(data ? data.VehicleMake : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Make' } });
        self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Registration No.' } });
        self.VehicleModel = ko.observable(data ? data.VehicleModel : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Model' } });
        self.Chassis_VinNo = ko.observable(data ? data.Chassis_VinNo : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Chassis or Vin No.' } });
        self.Colour = ko.observable(data ? data.Colour : "");//.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Colour.' } });


    }
    var VisitorPermit = function (data) {
        var self = this;

        self.VisitorPermitID = ko.observable(data ? data.VisitorPermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.CompanyName = ko.observable(data ? data.CompanyName : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Company Name' } });
        self.Reason = ko.observable(data ? data.Reason : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Reason' } });
        self.AuthorizedPersonName = ko.observable(data ? data.AuthorizedPersonName : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Authorized Person Name' } });
        self.Division = ko.observable(data ? data.Division : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Division' } });
        self.PositionHeld = ko.observable(data ? data.PositionHeld : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Position Held' } });
        self.EscortName = ko.observable(data ? data.EscortName : "");   //.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Escort Name' } });
        self.TelephoneNo = ko.observable(data ? data.TelephoneNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Telephone No.' } });
        self.PermitNo = ko.observable(data ? data.PermitNo : "");  //.extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Permit No.' } });
        self.PermitCode = ko.observable(data ? data.PermitCode : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Permit Code' } });
        /////////////////
        self.secion56licenseNo = ko.observable(data ? data.secion56licenseNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter section 56/57 license No.' } });
        self.portLeaseAggrement = ko.observable(data ? data.secion56licenseNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Port Lease Aggrement' } });
        self.NewEmp = ko.observable(data ? data.VisitorPermitID : "");
        self.PrtAccPermitRenewal = ko.observable(data ? data.PrtAccPermitRenewal : "");
        //////////////
    }
    var WharfVehiclePermit = function (data) {
        var self = this;

        self.WharfVehiclePermitID = ko.observable(data ? data.WharfVehiclePermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.VehicleMake = ko.observable(data ? data.VehicleMake : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Make' } });
        self.Hometelephone = ko.observable(data ? data.Hometelephone : "");
        self.VehicleModel = ko.observable(data ? data.VehicleModel : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Model' } });
        self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Regn No.' } });
        self.VehicleDescription = ko.observable(data ? data.VehicleDescription : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Description' } });
        self.VehicleRegisterd = ko.observable(data ? data.VehicleRegisterd : "N").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Registerd' } });
        self.VehiclePointed = ko.observable(data ? data.VehiclePointed : "N").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Vehicle Pointed' } });
        self.Reason = ko.observable(data ? data.Reason : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Reason' } });
        self.MobileNo = ko.observable(data ? data.MobileNo : "");
        self.TelephoneNo = ko.observable(data ? data.TelephoneNo : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Telephone No.' } });
        self.ContractDuration = ko.observable(data ? data.ContractDuration : "");
        self.PermitRequirement = ko.observable(data ? data.PermitRequirement : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter a Permit Requirement' } });
        self.ContractorNo = ko.observable(data ? data.ContractorNo : "");
        self.TemporaryPermits = ko.observable(data ? data.TemporaryPermits : "");


        // self.AccessGates = ko.observable(data ? data.AccessGates : "");
        self.OtherSpecify = ko.observable(data ? data.OtherSpecify : "");

    }

    var IndividualPermitApplicationDetail = function (data) {
        var self = this;
        self.IndividualApplicationID = ko.observable(data ? data.IndividualApplicationID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.Initial = ko.observable(data ? data.Initial : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Initial' } });
        self.SACitizen = ko.observable(data ? data.SACitizen : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select  SA Citizen' } });
        self.Gender = ko.observable(data ? data.Gender : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please Enter Gender' } });
        self.Suburb = ko.observable(data ? data.Suburb : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Subrub' } });
        self.City = ko.observable(data ? data.City : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a City' } });
        self.PostalCode = ko.observable(data ? data.PostalCode : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Postal Code' } });
        self.CountryOfOrigin = ko.observable(data ? data.CountryOfOrigin : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Select Country Of Origin' } });
        self.DepartmentManager = ko.observable(data ? data.DepartmentManager : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Department Manager' } });
        // self.CompnyPhyAddress = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Company Physical Address' } });
        self.EmployeeNo = ko.observable(data ? data.EmployeeNo : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Employee No.' } });
        self.JobTitle = ko.observable(data ? data.JobTitle : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Job Title' } });
        self.Current_Permit_Exists = ko.observable(data ? data.Current_Permit_Exists : "").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select weather current permit exists' } });
        self.Reason_Reapplication = ko.observable(data ? data.Reason_Reapplication : "");
        self.Port_Induction_Training = ko.observable(data ? data.Port_Induction_Training : "");
        self.Training_Date = ko.observable(data ? (moment(data.Training_Date).format('YYYY-MM-DD HH:mm') || "") : "");
        self.Criminal_Bckground = ko.observable(data ? data.Criminal_Bckground : "");
        self.EmployeeNo = ko.observable(data ? data.EmployeeNo : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter EmployeeNo' } });
        self.Signature = ko.observable(data ? data.Signature : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Signature of the Applicant' } });
        self.Date = ko.observable(data ? (moment(data.Date).format('YYYY-MM-DD HH:mm') || "") : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Date of the Applicant' } });
        // self.ETA(data ? (moment(data.ETA).format('YYYY-MM-DD HH:mm') || "") : "");
        self.EmailAddress = ko.observable(data ? data.EmailAddress : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter authorised Email' }, email: { message: '* Please enter valid Email ID' } });
    }


    var ContractorPermitEmployeeDetail = function (data) {
        var self = this;
        self.ContractorPermitEmployeeID = ko.observable(data ? data.ContractorPermitEmployeeID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.EmployeeName = ko.observable(data ? data.EmployeeName : "");
        self.IDNumber = ko.observable(data ? data.IDNumber : "");
        self.JobTitle = ko.observable(data ? data.JobTitle : "");
        self.CriminalRecord = ko.observable(data ? data.CriminalRecord : "");
        self.EmpSignature = ko.observable(data ? data.EmpSignature : "");
        self.RecordStatus = ko.observable();
    }

    var IndividualPersonalPermit = function (data) {
        var self = this;
        self.IndividualPersonalPermitID = ko.observable(data ? data.IndividualPersonalPermitID : "");

        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PerCode = ko.observable(data ? data.PerCode : "");
        self.TempCode = ko.observable(data ? data.TempCode : "");
        self.permittype = ko.observable(data ? data.permittype : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Select permit type' } });
        self.IndividualTemporaryPermits = ko.observable(data ? data.IndividualTemporaryPermits : "");
        self.ContractorTemporaryPermits = ko.observable(data ? data.ContractorTemporaryPermits : "");
        self.ContractorPermanentPermits = ko.observable(data ? data.ContractorPermanentPermits : "");
        self.IndividualPermanentPermits = ko.observable(data ? data.IndividualPermanentPermits : "");

        self.TempFromDate = ko.observable(data ? (moment(data.TempFromDate).format('YYYY-MM-DD') || "") : "");

        self.TempToDate = ko.observable(data ? (moment(data.TempToDate).format('YYYY-MM-DD') || "") : "");
        self.PerFromDate = ko.observable(data ? (moment(data.PerFromDate).format('YYYY-MM-DD') || "") : "");
        self.PerToDate = ko.observable(data ? (moment(data.PerToDate).format('YYYY-MM-DD') || "") : "");
        self.IsTools = ko.observable(data ? data.IsTools : "");
        self.IsCamera = ko.observable(data ? data.IsCamera : "");
        self.IsSpclEquipment = ko.observable(data ? data.IsSpclEquipment : "");
        self.ToolsDetails = ko.observable(data ? data.ToolsDetails : "");//ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Details of the tools' } });
        self.CameraDetails = ko.observable(data ? data.CameraDetails : ""); //ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Details of camera' } });
        self.SpclEquipmentDetails = ko.observable(data ? data.SpclEquipmentDetails : "");// ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Details of special Equipments' } });
        self.AuthorisedSurname = ko.observable(data ? data.AuthorisedSurname : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Intial and surname' } });
        self.TelephoneWork = ko.observable(data ? data.TelephoneWork : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Telephone Work' } });
        self.AuthorisedMobile = ko.observable(data ? data.AuthorisedMobile : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter mobile number' } });
        self.AuthorisedIdentityNumber = ko.observable(data ? data.AuthorisedIdentityNumber : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter authorised Identity Number' } });
        // self.EmailID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' }, email: { message: '* Please enter valid Email ID' } });
        self.AuthorisedEmail = ko.observable(data ? data.AuthorisedEmail : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter authorised Email' }, email: { message: '* Please enter valid Email ID' } });
        self.AuthorisedSignature = ko.observable(data ? data.AuthorisedSignature : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter authorised Signature' } });
        self.SignatoryDate = ko.observable(data ? (moment(data.SignatoryDate).format('YYYY-MM-DD') || "") : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter authorised Date Signature' } });
        self.ContractorTemporaryPermits = ko.observable(data ? data.ContractorTemporaryPermits : "");
        self.ContractorPermanentPermits = ko.observable(data ? data.ContractorPermanentPermits : "");
        self.ContractorTempFromDate = ko.observable(data ? (moment(data.ContractorTempFromDate).format('YYYY-MM-DD') || "") : "");
        self.ContractorTempToDate = ko.observable(data ? (moment(data.ContractorTempToDate).format('YYYY-MM-DD') || "") : "");
        self.ContractorPerFromDate = ko.observable(data ? (moment(data.ContractorPerFromDate).format('YYYY-MM-DD') || "") : "");
        self.ContractorPerToDate = ko.observable(data ? (moment(data.ContractorPerToDate).format('YYYY-MM-DD') || "") : "");


    }

    var PersonalPermit = function (data) {

        var self = this;
        self.PersonalPermitID = ko.observable(data ? data.PersonalPermitID : "");

        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitCategoryCode = ko.observable(data ? data.PermitCategoryCode : "");
        self.AllNPASites = ko.observable(data ? data.AllNPASites : "");
        self.SpecificNPASites = ko.observable(data ? data.SpecificNPASites : "");
        self.SpecifyArea = ko.observable(data ? data.SpecifyArea : "");
        self.LeaseholdSite = ko.observable(data ? data.LeaseholdSite : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Specify Name And Specific Location of Lease Holding Site' } });
        self.PhysicalAddress = ko.observable(data ? data.PhysicalAddress : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Physical Address' } });
        self.AdhocPermits = ko.observable(data ? data.AdhocPermits : "");
        self.TemporaryPermits = ko.observable(data ? data.TemporaryPermits : "");
        self.AllPorts = ko.observable(data ? data.AllPorts : "");
        self.ConstructionArea = ko.observable(data ? data.ConstructionArea : "");
        self.PermanentPermits = ko.observable(data ? data.PermanentPermits : "");
        self.Reason = ko.observable(data ? data.Reason : "");//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Reason' } });
        self.permittype = ko.observable(data ? data.permittype : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Select permit type' } });
    }
    var PermitRequestDetailsverifyedbySSA = function (data) {
        var self = this;
        self.PermitRequestverifyedID = ko.observable(data ? data.PermitRequestverifyedID : "");
        self.permitrRequestID = ko.observable(data ? data.permitrRequestID : "");
        self.CreminalCheck = ko.observable(data ? data.CreminalCheck : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.verifyedUserID = ko.observable(data ? data.verifyedUserID : "");
        self.verifyedDate = ko.observable(data ? data.verifyedDate : "");
    }
    var PermitRequestDetailsverifyedbySAPS = function (data) {
        var self = this;
        self.PermitRequestverifyedID = ko.observable(data ? data.PermitRequestverifyedID : "");
        self.permitrRequestID = ko.observable(data ? data.permitrRequestID : "");
        self.CreminalCheck = ko.observable(data ? data.CreminalCheck : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.verifyedUserID = ko.observable(data ? data.verifyedUserID : "");
        self.verifyedDate = ko.observable(data ? data.verifyedDate : "");
    }

    var PermitRequestverifyedbySSADocument = function (data) {
        var self = this;

        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    var PermitRequestverifyedbySAPSDocument = function (data) {
        var self = this;

        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    var PermitRequestSubArea = function (data) {
        var self = this;
        self.PermitRequestSubAreaCode = ko.observableArray([]);
        self.PermitRequestSubAreaCode = ko.observable(data ? data.PermitRequestSubAreaCode : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitRequestSubAreaID = ko.observable(data ? data.PermitRequestSubAreaID : "");
        self.PermitRequestAreaCode = ko.observable(data ? data.PermitRequestAreaCode : "");
    }
    var PermitRequestSubAreaDis = function (data) {
        var self = this;
        self.PermitRequestSubAreaCode = ko.observableArray([]);
        self.PermitRequestSubAreaCode = ko.observable(data ? data.PermitRequestSubAreaCode : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitRequestSubAreaID = ko.observable(data ? data.PermitRequestSubAreaID : "");
        self.PermitRequestAreaCode = ko.observable(data ? data.PermitRequestAreaCode : "");
    }

    var ContractorPermitApplicationDetail = function (data) {
        var self = this;
        self.ContractorPermitApplicationID = ko.observable(data ? data.ContractorPermitApplicationID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.ContractorCompanyName = ko.observable(data ? data.ContractorCompanyName : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Contractor Company Name' } })
        self.ContractorCompanyManager = ko.observable(data ? data.ContractorCompanyManager : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Contractor Company Manager' } })
        self.Department = ko.observable(data ? data.Department : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Department' } })
        self.TelephoneNumber = ko.observable(data ? data.TelephoneNumber : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Telephone Number' } })
        self.SubContractorCompanyName = ko.observable(data ? data.SubContractorCompanyName : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Sub contractor Company Name' } });
        self.SubContractorTelephoneNumber = ko.observable(data ? data.SubContractorTelephoneNumber : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter Sub contractor Telephone Number' } });
    }


    //****************************************MainModel***********************************************************************************
    var PortEntryPassApplicationModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(false);

        self.PermitRequestID = ko.observable();
        self.PortCode = ko.observable(data ? data.PortCode : "").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select a Port' } });
        self.PermitRequestTypeCode = ko.observable();
        self.PermitRequestTypeCodeName = ko.observable();
        self.Email = ko.observable(); //ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Email.' }, email: { message: '* Please enter a proper email address.' }, });
        self.MobileNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Mobile No.' } });
        self.CompanyName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Company Name' } });
        self.DepartmentName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Department Name' } });
        self.ApplicantFullName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Applicant Full Name' } });
        self.ApplicantSurName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Applicant Surname' } });
        self.PensionEmployeeNo = ko.observable();//ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Pension/Employee No.' } });
        self.IDPassportNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a ID/Passport No.' } });
        self.Occupation = ko.observable();//ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Occupation' } });
        self.HomeAddress = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Home Address' } });
        self.CompanyAddress = ko.observable();//ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Company Address' } });
        self.CompanyTelephoneNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Company Telephone No.' } });
        self.CompanyFaxNo = ko.observable();//ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please Enter a Company Fax No.' } });
        self.RecordStatus = ko.observable();
        self.Flag = ko.observable();
        self.PortName = ko.observable();
        self.CreatedDate = ko.observable();
        self.permitStatusName = ko.observable();
        self.permitStatus = ko.observable();
        self.ReferenceNo = ko.observable();
        self.AppealBoardRemarks = ko.observable();
        self.PSOremarkes = ko.observable();
        self.AppealRemarks = ko.observable();
        self.Status = ko.observable();
        self.FlagSAPS = ko.observable();
        self.FlagPSSA = ko.observable();

        self.documentupload = ko.observable(false);
        self.selectedPermitRequirementCode = ko.observableArray([]);
        self.selectedAccessGates = ko.observableArray([]);


        self.isResubmisionVisible = ko.computed(function () {

            if (self.permitStatus() != null && self.permitStatus() != '') {
                if (self.permitStatus() == "PERN" || self.permitStatus() == "PRSN") {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isAppealsubmitVisible = ko.computed(function () {

            if (self.permitStatus() != null && self.permitStatus() != '') {
                if (self.permitStatus() == "PRRN") {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isforwordtosecuritycheckVisible = ko.computed(function () {

            if (self.permitStatus() == "PERN" || self.permitStatus() == "PRSN") {
                //if (self.permitStatus()== "PSCV") {
                return true;
            }
            else {
                return false;
            }

        }, this);

        self.isviewApplealapproved = ko.computed(function () {
            if (self.permitStatus() == "PAAD") {
                //if (self.permitStatus()== "PSCV") {
                return true;
            }
            else {
                return false;
            }
        }, this);

        self.isApproveVisible = ko.computed(function () {

            if (self.permitStatus() != null && self.permitStatus() != '') {
                if (self.permitStatus() == "PSCV") {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.AfterIssueandClosePermit = ko.computed(function () {
            if (self.permitStatus() == "PRIC") {
                return true;
            }
            else {
                return false;
            }
        }, this);

        self.Forissuepermit = ko.computed(function () {

            if (self.permitStatus() == "PAAD")
            { return true; }
            else if (self.permitStatus() == "PSOA")
            { return true; }
            else if (self.permitStatus() == "IEPA")
            { return true; }
            else { return false; }
        }, this);


        self.isinetrnalEmpApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.PermitRequestTypeCode() == "APCG" || self.permitStatus() == 'IEPA') {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isApprovalVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.permitStatus() == 'PSOA') {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isAppealApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.permitStatus() == 'PAAD') {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isissueissableAppealApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.permitStatus() == 'PAAD') {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isSecurityClearenceRemarksVisible = ko.computed(function () {

            if (self.permitStatus() == "PAAD" || self.permitStatus() == "PERR" || self.permitStatus() == "PRIC" || self.permitStatus() == "PRRN" || self.permitStatus() == "PSOA" || self.permitStatus() == "PSCV") {
                return true;
            }
            else {
                return false;
            }

        }, this);

        //--------Arrays--------------------------------------
        self.selectedDocumentsType = ko.observableArray([]);
        self.UploadedFiles = ko.observableArray([]);
        self.PermitRequestAreas = ko.observableArray([]);
        self.PermitReasons = ko.observableArray([]);
        self.PermitRequestSubAreas = ko.observableArray([]);
        self.ContractorPermitApplicationDetails = ko.observableArray([]);


        //------------------------------------------------------

        self.toggleAssociation = function (item) {
            self.PermitRequestTypeCode(item.SubCatCode());
            self.documentupload(true);
            return true;
            //if(self.PermitRequestTypeCode=='APCA')

        };
        self.toggleAssociationrediobutton11 = function (item) {

            self.PersonalPermits().PermitCategoryCode(item.SubCatCode());
            self.documentupload(true);
            return true;
        };
        self.toggleAssociationrediobutton12 = function (item) {
            self.PersonalPermits().AdhocPermits(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton13 = function (item) {
            self.PersonalPermits().TemporaryPermits(item.SubCatCode());

            return true;
        };
        self.toggleAssociationrediobutton14 = function (item) {
            self.PersonalPermits().ConstructionArea(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton15 = function (item) {
            self.VehiclePermits().PermitRequirementCode(item.SubCatCode());
            return true;
        };

        self.toggleAssociationrediobutton16 = function (item) {
            self.WharfVehiclePermits().PermitRequirement(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton17 = function (item) {
            self.WharfVehiclePermits().TemporaryPermits(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton18 = function (item) {
            self.WharfVehiclePermits().AccessGates(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton19 = function (item) {
            self.VisitorPermits().PermitCode(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton20 = function (item) {
            self.IndividualPersonalPermits().IndividualTemporaryPermits(item.SubCatCode());

            return true;
        };
        self.toggleAssociationrediobutton21 = function (item) {
            self.IndividualPersonalPermits().ContractorTemporaryPermits(item.SubCatCode());

            return true;
        };
        self.toggleAssociationrediobutton22 = function (item) {
            self.IndividualPersonalPermits().ContractorPermanentPermits(item.SubCatCode());

            return true;
        };

        self.toggleAssociationrediobutton23 = function (item) {
            self.IndividualPersonalPermits().IndividualPermanentPermits(item.SubCatCode());

            return true;
        };
        //----------------------------------------------------------------------------------------------------------------------------------------
        self.ApplicantFullNameSort;
        self.ApplicantFullName.subscribe(function (value) {
            self.ApplicantFullNameSort = value;
        });
        self.ReferenceNoSort;
        self.ReferenceNo.subscribe(function (value) {
            self.ReferenceNoSort = value;
        });
        self.PermitRequestTypeCodeNameSort;
        self.PermitRequestTypeCodeName.subscribe(function (value) {
            self.PermitRequestTypeCodeNameSort = value;
        });
        self.CompanyNameSort;
        self.CompanyName.subscribe(function (value) {
            self.CompanyNameSort = value;
        });
        self.IDPassportNoSort;
        self.IDPassportNo.subscribe(function (value) {
            self.IDPassportNoSort = value;
        });
        self.permitStatusNameSort;
        self.permitStatusName.subscribe(function (value) {
            self.permitStatusNameSort = value;
        });
        self.CreatedDateSort;
        self.CreatedDate.subscribe(function (value) {
            self.CreatedDateSort = value;
        });
        self.PensionEmployeeNoSort;
        self.PensionEmployeeNo.subscribe(function (value) {
            self.PensionEmployeeNoSort = value;
        });



        //-----------------------------------------------------------------------------------------------------------------------------------------
        //self.PermitRequestDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestDocuments, function (PortEntryPass) {
        //    return new PermitRequestDocument(PortEntryPass);
        //}) : []);
        self.PermitRequestDocuments = ko.observableArray([]);

        self.PermitRequestAreas = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestAreas, function (PortEntryPass) {
            return new PermitRequestArea(PortEntryPass);
        }) : []);
        self.PermitRequestSubAreas = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestSubAreas, function (PortEntryPass) {
            return new PermitRequestSubArea(PortEntryPass);
        }) : []);

        self.PermitRequestSubAreaDiss = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestSubAreaDiss, function (PortEntryPass) {
            return new PermitRequestSubAreaDis(PortEntryPass);
        }) : []);



        self.PermitReasons = ko.observableArray(data ? ko.utils.arrayMap(data.PermitReasons, function (PortEntryPass) {
            return new PermitReason(PortEntryPass);
        }) : []);

        self.ContractorPermitEmployeeDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ContractorPermitEmployeeDetails, function (PortEntryPass) {
            return new ContractorPermitEmployeeDetail(PortEntryPass);
        }) : []);
        self.PermitRequestContractors = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestContractors, function (PortEntryPass) {
            return new PermitRequestContractor(PortEntryPass);
        }) : []);
        self.VehiclePermits = ko.observableArray(data ? ko.utils.arrayMap(data.VehiclePermits, function (PortEntryPass) {
            return new VehiclePermit(PortEntryPass);
        }) : []);
        self.VisitorPermits = ko.observableArray(data ? ko.utils.arrayMap(data.VisitorPermits, function (PortEntryPass) {
            return new VisitorPermit(PortEntryPass);
        }) : []);
        self.WharfVehiclePermits = ko.observableArray(data ? ko.utils.arrayMap(data.WharfVehiclePermits, function (PortEntryPass) {
            return new WharfVehiclePermit(PortEntryPass);
        }) : []);
        self.PersonalPermits = ko.observableArray(data ? ko.utils.arrayMap(data.PersonalPermits, function (PortEntryPass) {
            return new PersonalPermit(PortEntryPass);
        }) : []);

        self.IndividualPersonalPermits = ko.observableArray(data ? ko.utils.arrayMap(data.IndividualPersonalPermits, function (PortEntryPass) {
            return new IndividualPersonalPermit(PortEntryPass);
        }) : []);
        self.PermitRequestVerifyedDetailsverifyedbySSA = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA, function (PortEntryPass) {
            return new PermitRequestDetailsverifyedbySSA(PortEntryPass);
        }) : []);
        self.PermitRequestVerifyedDetailsverifyedbySAPS = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS, function (PortEntryPass) {
            return new PermitRequestDetailsverifyedbySAPS(PortEntryPass);
        }) : []);

        self.IndividualPermitApplicationDetails = ko.observableArray(data ? ko.utils.arrayMap(data.IndividualPermitApplicationDetails, function (PortEntryPass) {
            return new IndividualPermitApplicationDetail(PortEntryPass);
        }) : []);

        self.IndividualVehiclePermits = ko.observableArray(data ? ko.utils.arrayMap(data.IndividualVehiclePermits, function (PortEntryPass) {
            return new IndividualVehiclePermit(PortEntryPass);
        }) : []);

        self.ContractorPermitApplicationDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ContractorPermitApplicationDetails, function (PortEntryPass) {
            return new ContractorPermitApplicationDetail(PortEntryPass);
        }) : []);
        //if (data.PermitRequestVerifyedDetailsverifyedbySSA().PermitRequestverifyedbySSADocuments != null) {

        self.PermitRequestverifyedbySSADocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA.PermitRequestverifyedbySSADocuments, function (PortEntryPass) {
            return new PermitRequestverifyedbySSADocument(PortEntryPass);
        }) : []);
        //}
        //if (data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments != null) {
        self.PermitRequestverifyedbySAPSDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments, function (PortEntryPass) {
            return new PermitRequestverifyedbySAPSDocument(PortEntryPass);
        }) : []);
        //}

        //-----------------------------------------------------------------------------------------------------------------------------------------

        self.cache = function () { };
        self.set(data);
    }
    //****************************************MainModel END***********************************************************************************
    ipmsRoot.PermitRequestverifyedbySSADocument = PermitRequestverifyedbySSADocument;
    ipmsRoot.PermitRequestverifyedbySAPSDocument = PermitRequestverifyedbySAPSDocument;

    ipmsRoot.PermitRequestDetailsverifyedbySSA = PermitRequestDetailsverifyedbySSA;
    ipmsRoot.PermitRequestDetailsverifyedbySAPS = PermitRequestDetailsverifyedbySAPS;

    ipmsRoot.Permitcode = Permitcode;
    ipmsRoot.PermitRequeirement = PermitRequeirement;
    ipmsRoot.AccessGate = AccessGate;
    ipmsRoot.PermitRequeirementstype = PermitRequeirementstype;
    ipmsRoot.PermitRequeirementsDurationS = PermitRequeirementsDurationS;
    ipmsRoot.appcategory = appcategory;
    ipmsRoot.PermitType = PermitType;
    ipmsRoot.areaofconstruction = areaofconstruction;
    ipmsRoot.TemporaryPermit = TemporaryPermit;
    ipmsRoot.AdhocPermit = AdhocPermit;
    ipmsRoot.OperatedTerminal = OperatedTerminal;
    ipmsRoot.PermitRequestDocument = PermitRequestDocument;
    ipmsRoot.IndividualTemporaryPermit = IndividualTemporaryPermit;
    ipmsRoot.ContractorTemporaryPermit = ContractorTemporaryPermit;
    ipmsRoot.ContractorPermanentPermit = ContractorPermanentPermit;
    ipmsRoot.IndividualPermanentPermit = IndividualPermanentPermit;
    ipmsRoot.AccessAreasForRB = AccessAreasForRB;
    ipmsRoot.ReasonForPermit = ReasonForPermit;
    ipmsRoot.ContractorReasonForPermit = ContractorReasonForPermit;
    // ipmsRoot.PermitRequestArea = PermitRequestArea;
    ipmsRoot.PermitRequestContractor = PermitRequestContractor;
    ipmsRoot.VehiclePermit = VehiclePermit;
    ipmsRoot.IndividualVehiclePermit = IndividualVehiclePermit;

    ipmsRoot.VisitorPermit = VisitorPermit;
    ipmsRoot.WharfVehiclePermit = WharfVehiclePermit;
    ipmsRoot.PersonalPermit = PersonalPermit;
    ipmsRoot.IndividualPersonalPermit = IndividualPersonalPermit;
    // ipmsRoot.IndividualVehiclePermit = IndividualVehiclePermit;
    ipmsRoot.IndividualPermitApplicationDetail = IndividualPermitApplicationDetail;
    ipmsRoot.PortEntryPassApplicationReferenceData = PortEntryPassApplicationReferenceData;
    ipmsRoot.PortEntryPassApplicationModel = PortEntryPassApplicationModel;
    ipmsRoot.SubAccessAreasForRB = SubAccessAreasForRB;
    ipmsRoot.ViewSubAccessAreasForRB = ViewSubAccessAreasForRB;
    ipmsRoot.PermitRequestSubArea = PermitRequestSubArea;
    ipmsRoot.PermitRequestSubAreaDis = PermitRequestSubAreaDis;
    ipmsRoot.PermitReason = PermitReason;
    ipmsRoot.ContractorPermitEmployeeDetail = ContractorPermitEmployeeDetail;
    ipmsRoot.ContractorPermitApplicationDetail = ContractorPermitApplicationDetail;
    ipmsRoot.AdvnceSearchmodel = AdvnceSearchmodel;

}(window.IPMSROOT));


var DateFormat = 'YYYY-MM-DD HH:mm';

IPMSROOT.AdvnceSearchmodel.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    
    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate());
    fromdate.setDate(fromdate.getDate() - 1);
    self.RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
}



IPMSROOT.PortEntryPassApplicationModel.prototype.set = function (data) {
    var self = this;
    self.Status(data ? (data.Status || "") : "");
    self.FlagSAPS(data ? (data.FlagSAPS || "") : "");
    self.FlagPSSA(data ? (data.FlagPSSA || "") : "");

    self.AppealBoardRemarks(data ? (data.AppealBoardRemarks || "") : "");
    self.PSOremarkes(data ? (data.PSOremarkes || "") : "");
    self.AppealRemarks(data ? (data.AppealRemarks || "") : "");
    self.permitStatusName(data ? (data.permitStatusName || "") : "");
    self.permitStatus(data ? (data.permitStatus || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.documentupload(data ? (data.documentupload || "") : "");
    self.PermitRequestID(data ? (data.PermitRequestID || "") : "");
    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.Email(data ? (data.Email || "") : "");
    self.MobileNo(data ? (data.MobileNo || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PermitRequestTypeCodeName(data ? (data.PermitRequestTypeCodeName || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
    self.PermitRequestTypeCode(data ? (data.PermitRequestTypeCode || "") : "");
    self.CompanyName(data ? (data.CompanyName || "") : "");
    self.DepartmentName(data ? (data.DepartmentName || "") : "");
    self.ApplicantFullName(data ? (data.ApplicantFullName || "") : "");
    self.ApplicantSurName(data ? (data.ApplicantSurName || "") : "");
    self.PensionEmployeeNo(data ? (data.PensionEmployeeNo == 'NULL' ? "" : data.PensionEmployeeNo || "") : "");
    self.IDPassportNo(data ? (data.IDPassportNo || "") : "");
    self.Occupation(data ? (data.Occupation || "") : "");
    self.HomeAddress(data ? (data.HomeAddress || "") : "");
    self.CompanyAddress(data ? (data.CompanyAddress || "") : "");
    self.CompanyTelephoneNo(data ? (data.CompanyTelephoneNo || "") : "");
    //self.CompanyFaxNo(data ? (data.CompanyFaxNo || "") : "");
    self.Flag(data ? (data.Flag || "") : "");
    self.CreatedDate(data ? (moment(data.CreatedDate).format('YYYY-MM-DD HH:mm') || "") : "");

    //
    //self.Initial(data ? (data.Initial || "") : "");
    //
    self.PermitRequestContractors(data ? new IPMSROOT.PermitRequestContractor(data.PermitRequestContractors) : new IPMSROOT.PermitRequestContractor(undefined));
    // self.PermitRequestAreas(data ? new IPMSROOT.PermitRequestArea(data.PermitRequestAreas) : new IPMSROOT.PermitRequestArea(undefined));
    self.VehiclePermits(data ? new IPMSROOT.VehiclePermit(data.VehiclePermits) : new IPMSROOT.VehiclePermit(undefined));
    self.VisitorPermits(data ? new IPMSROOT.VisitorPermit(data.VisitorPermits) : new IPMSROOT.VisitorPermit(undefined));
    self.WharfVehiclePermits(data ? new IPMSROOT.WharfVehiclePermit(data.WharfVehiclePermits) : new IPMSROOT.WharfVehiclePermit(undefined));
    self.PersonalPermits(data ? new IPMSROOT.PersonalPermit(data.PersonalPermits) : new IPMSROOT.PersonalPermit(undefined));
    //  self.IndividualVehiclePermits(data ? new IPMSROOT.IndividualVehiclePermit(data.IndividualVehiclePermits) : new IPMSROOT.IndividualVehiclePermit(undefined));
    self.IndividualPersonalPermits(data ? new IPMSROOT.IndividualPersonalPermit(data.IndividualPersonalPermits) : new IPMSROOT.IndividualPersonalPermit(undefined));
    self.ContractorPermitApplicationDetails(data ? new IPMSROOT.ContractorPermitApplicationDetail(data.ContractorPermitApplicationDetails) : new IPMSROOT.ContractorPermitApplicationDetail(undefined));

    //////////
    //self.IndividualVehiclePermits(data ? new IPMSROOT.IndividualVehiclePermit(data.IndividualVehiclePermits) : new IPMSROOT.IndividualVehiclePermit(undefined));        
    self.IndividualPermitApplicationDetails(data ? new IPMSROOT.IndividualPermitApplicationDetail(data.IndividualPermitApplicationDetails) : new IPMSROOT.IndividualPermitApplicationDetail(undefined));
    //////
    self.PermitRequestVerifyedDetailsverifyedbySSA(data ? new IPMSROOT.PermitRequestDetailsverifyedbySSA(data.PermitRequestVerifyedDetailsverifyedbySSA) : new IPMSROOT.PermitRequestDetailsverifyedbySSA(undefined));
    self.PermitRequestVerifyedDetailsverifyedbySAPS(data ? new IPMSROOT.PermitRequestDetailsverifyedbySAPS(data.PermitRequestVerifyedDetailsverifyedbySAPS) : new IPMSROOT.PermitRequestDetailsverifyedbySAPS(undefined));
    //self.PermitRequestDocuments(data ? new IPMSROOT.PermitRequestDocument(data.PermitRequestDocuments) : new IPMSROOT.PermitRequestDocument(undefined));
    self.PermitRequestAreas(data ? (data.PermitRequestAreas ? $.map(data.PermitRequestAreas, function (item) { return item }) : []) : []);
    self.PermitRequestSubAreas(data ? (data.PermitRequestSubAreas ? $.map(data.PermitRequestSubAreas, function (item) { return item }) : []) : []);
    self.PermitRequestSubAreaDiss(data ? (data.PermitRequestSubAreaDiss ? $.map(data.PermitRequestSubAreaDiss, function (item) { return item }) : []) : []);

    //self.PermitRequestSubAreas(data ? ko.utils.arrayMap(data.PermitRequestSubAreas, function (PortEntryPass) {
    //    return new PermitRequestSubArea(PortEntryPass);
    //}) : []);
    self.PermitReasons(data ? (data.PermitReasons ? $.map(data.PermitReasons, function (item) { return item }) : []) : []);
    //self.SubPermitRequestAreas(data ? new IPMSROOT.SubPermitRequestArea(data.SubPermitRequestAreas) : new IPMSROOT.SubPermitRequestArea(undefined));
    self.PermitRequestDocuments(data ? ko.utils.arrayMap(data.PermitRequestDocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestDocument(PortEntryPass);
    }) : []);

    self.selectedPermitRequirementCode(data ? (data.selectedPermitRequirementCode ? $.map(data.selectedPermitRequirementCode, function (item) { return item }) : []) : []);
    self.selectedAccessGates(data ? (data.selectedAccessGates ? $.map(data.selectedAccessGates, function (item) { return item }) : []) : []);

    self.PermitRequestverifyedbySSADocuments(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA.PermitRequestverifyedbySSADocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestverifyedbySSADocument(PortEntryPass);
    }) : []);

    self.PermitRequestverifyedbySAPSDocuments(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestverifyedbySAPSDocument(PortEntryPass);
    }) : []);
    //self.ContractorPermitApplicationDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ContractorPermitApplicationDetails, function (PortEntryPass) {
    //    return new ContractorPermitApplicationDetail(PortEntryPass);
    //}) : []);

    // self.VehiclePermitDtos(data ? ko.utils.arrayMap(data.VehiclePermitDtos, function (vehiclepermitDto) { return new IPMSROOT.VehiclePermitDto(vehiclepermitDto); }) : []);
    self.cache.latestData = data;
}

IPMSROOT.PortEntryPassApplicationModel.prototype.reset = function () {
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

//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
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
//Accept Alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    // charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    charcheck = /^([a-zA-Z0-9].*)$/;
    return charcheck.test(keychar);
}

