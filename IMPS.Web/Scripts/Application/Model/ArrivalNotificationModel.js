(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })    

    // ArrivalNotification ReferenceData Used For Fills The All Dropdowns in arrival notification form
    var ArrivalNotificationReferenceData = function (data) {
        var self = this;

        self.agent = ko.observable();        
        self.Docks = ko.observableArray(data ? $.map(data.Docks, function (item) { return new Dock(item); }) : []);
        self.CargoTypes = ko.observableArray(data ? $.map(data.CargoTypes, function (item) { return new Cargo(item); }) : []);
        self.Purposes = ko.observableArray(data ? $.map(data.Purpose, function (item) { return new Purpose(item); }) : []);
        self.Uoms = ko.observableArray(data ? $.map(data.Uoms, function (item) { return new Uoml(item); }) : []);
        self.DangerousGoods = ko.observableArray(data ? $.map(data.DangerousGoods, function (item) { return new DangerousGood(item); }) : []);
        self.Doctypes = ko.observableArray(data ? $.map(data.Doctypes, function (item) { return new Doctype(item); }) : []);
        self.Tankers = ko.observableArray(data ? $.map(data.Tankers, function (item) { return new Tanker(item); }) : []);        
        self.ReasonVisists = ko.observableArray(data ? $.map(data.ReasonForVisit, function (item) { return new ReasonVisist(item); }) : []);
        self.Commoditys = ko.observableArray(data ? $.map(data.Commoditys, function (item) { return new Commodity(item); }) : []);        
        self.Pilots = ko.observableArray(data ? $.map(data.Pilots, function (item) { return new Pilot(item); }) : []);
        self.DryDocBerths = ko.observableArray(data ? $.map(data.DryDocBerths, function (item) { return new DryDocBerth(item); }) : []);
        
        self.PortDetails = ko.observableArray(data ? $.map(data.PortDetails, function (item) { return new PortDetail(item); }) : []);
        self.BirthTos = ko.observableArray(data ? $.map(data.BirthTos, function (item) { return new BirthTo(item); }) : []);
        self.BunkersDetails = ko.observableArray(data ? $.map(data.BunkersDetails, function (item) { return new BunkersDetail(item); }) : []);
        self.BunkersRequiredDetails = ko.observableArray(data ? $.map(data.BunkersRequiredDetails, function (item) { return new BunkersRequiredDetail(item); }) : []);
        self.BunkersMethodDetails = ko.observableArray(data ? $.map(data.BunkersMethodDetails, function (item) { return new BunkersMethodDetail(item); }) : []);
        
        self.UserDetails = ko.observableArray(data ? $.map(data.UserDetails, function (item) { return new UserDetail(item); }) : []);

        self.WasteDclServiceProviders = ko.observableArray(data ? $.map(data.WasteDclServiceProvider, function (item) { return new WasteDclServiceProvider(item); }) : []);      
        self.Marpols = ko.observableArray(data ? $.map(data.Marpol, function (item) { return new Marpol(item); }) : []);
    }


    
    var WasteDclServiceProvider = function (data) {
        var self = this;        
        self.LicenseRequestID = ko.observable(data ? data.LicenseRequestID : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
    }

    
    var WasteDclClass = function (data) {
        var self = this;
        self.ClassCode = ko.observable(data ? data.ClassCode : "");
        self.ClassName = ko.observable(data ? data.ClassName : "");
    }

    
    var Marpol = function (data) {
        var self = this;
        self.MarpolCode = ko.observable(data ? data.MarpolCode : "");
        self.MarpolName = ko.observable(data ? data.MarpolName : "");        
        self.MarpolDetails = ko.observableArray(data ? $.map(data.MarpolDetails, function (item) { return new WasteDclClass(item); }) : []);
    }


    // ArrivalNotification ReferenceData Used For Fills The All Dropdowns in arrival notification form
    var ArrivalNotificationReferenceBirthData = function (data) {
        var self = this;
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);

    }
    // ArrivalNotification ReferenceData Used For Fills The All Dropdowns in arrival notification form

    var ArrivalNotificationReferenceDraftData = function (data) {
        var self = this;
        self.DraftDetails = ko.observableArray(data ? $.map(data.DraftDetails, function (item) { return new DraftDetail(item); }) : []);
    }


    var UserDetail = function (data) {
        
        var self = this;
        self.UserName = ko.observable(data ? data.FirstName + ' ' + data.LastName : "");
        self.ContactNo = ko.observable(data ? data.ContactNo : "");
        self.ArrivalCreatedAgent = ko.observable(data ? data.ArrivalCreatedAgent : "");
    }

    //ArrivalNotification Draft Details Model
    var BirthTo = function (data) {
        var self = this;
        self.TerminalOperatorid = ko.observable(data ? data.TerminalOperatorID : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
    }

    //ArrivalNotification Draft Details Model
    var DraftDetail = function (data) {
        var self = this;
        self.VCNName = ko.observable(data ? data.VCNdraftDisplay : "");
        self.VCNKey = ko.observable(data ? data.VCN : "");
    }

    //ArrivalNotification Berth Model
    var DryDocBerth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
    }


    var PortDetail = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }

    var BunkersDetail = function (data) {
        var self = this;
        self.LicenseRequestID = ko.observable(data ? data.LicenseRequestID : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
    }

    var BunkersRequiredDetail = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var BunkersMethodDetail = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    //ArrivalNotification Berth Model
    var Berth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.Draftm = ko.observable(data ? data.Draftm : "");
        self.CargoDetails = ko.observable(data ? data.CargoDetails : "");
    }
    //ArrivalNotificationTanker Model
    var Dock = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotification CargoType Model
    var Cargo = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotification Purpose Model
    var Purpose = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotification  Uoml Model
    var Uoml = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotification DangerousGood Model
    var DangerousGood = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotification Documenttype Model
    var Doctype = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ArrivalNotificationTanker Model
    var Tanker = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    
    //ArrivalNotificationPilot Model
    var Pilot = function (data) {

        var self = this;
        self.SubCatCode = ko.observable(data ? data.PilotID : "");
        self.SubCatName = ko.observable(data ? data.IDNo + ' - ' + data.FirstName : "");
    }
    //ArrivalNotificationReasonVisistModel
    var ReasonVisist = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    //ArrivalNotificationCommodityModel
    var Commodity = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    //VesselModel
    var Vessel = function (data) {
        var self = this;
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.VesselID = ko.observable(data ? data.VesselID : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.VesselTypeName = ko.observable(data ? data.VesselTypeName : "");
        self.LengthOverallInM = ko.observable(data ? data.LengthOverallInM : "");
        self.BeamInM = ko.observable(data ? data.BeamInM : "");
        self.CallSign = ko.observable(data ? data.CallSign : "");
        self.VesselNationality = ko.observable(data ? data.VesselNationality : "");
        self.GrossRegisteredTonnageInMT = ko.observable(data ? data.GrossRegisteredTonnageInMT : "");
        self.DeadWeightTonnageInMT = ko.observable(data ? data.DeadWeightTonnageInMT : "");
        self.DateOfIssue = ko.observable(data ? data.DateOfIssue : "");

    
        self.DockingPlanNo = ko.observable(data ? data.DockingPlanNo : "");

        self.DockIsFinal = ko.observable(data ? data.DockIsFinal : "");
        self.DockStatus = ko.observable(data ? data.DockStatus : "");

        self.DockingPlanID = ko.observable(data ? data.DockingPlanID : "");
        self.SubmissionDate = ko.observable(data ? data.SubmissionDate : "");
      
    }

    var ArrivalNotificationModelSearchGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VCNSERCH = ko.observable();
        self.VesselName = ko.observable();
        self.VesselID = ko.observable();
        self.IsserchIMDG = ko.observable();
        self.IsserchISPS = ko.observable();

        self.IsserchIMDGClear = ko.observable();
        self.IsserchISPSClear = ko.observable();
        self.IsserchPHOClear = ko.observable();

        self.ETAFrom = ko.observable();
        self.ETATo = ko.observable();
        self.set(data);
    }

    var ArrivalNotificationModelGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselType = ko.observable();
        self.ReasonforvisitName = ko.observable();        

        
        self.ETA = ko.observable();
        self.ETD = ko.observable();
        self.NominationDate = ko.observable();
        self.wfStatus = ko.observable();
        self.isEditVisible = ko.observable();
        self.isViewVisible = ko.observable();

        self.ReasonForVisit = ko.observable();
        self.AnyDangerousGoodsonBoard = ko.observable();


        self.TerminalOperatorID = ko.observable('0');
        self.IsTerminalOperator = ko.observable('I');


        self.UserName = ko.observable("");
        self.ContactNo = ko.observable("");
        self.IsANFinal = ko.observable();
        self.IsISPSANFinal = ko.observable();
        self.IsPHANFinal = ko.observable();
        self.IsIMDGANFinal = ko.observable();
        self.IsSamsaArrested = ko.observable();

        self.IsArrivaStatus = ko.observable();
        self.IsPHANStatus = ko.observable();
        self.IsISPSANStatus = ko.observable();
        self.IsIMDGANStatus = ko.observable();
        self.RecordStatus = ko.observable();

        self.ArrvwfRemarks = ko.observable();
        self.PHOwfRemarks = ko.observable();
        self.ISPSwfRemarks = ko.observable();
        self.IMDGwfRemarks = ko.observable();
        self.ArvStatus = ko.observable();
        self.IsPrimary = ko.observable();
        self.CancelRemarks = ko.observable();
        
              

        // Srini
        self.ETAFrom = ko.observable("");
        self.ETATo = ko.observable("");
        // Srini End

        self.VCNSortGrid;
        self.VCN.subscribe(function (value) {
            self.VCNSortGrid = value;
        });

        self.VesselNameSortGrid;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSortGrid = value;
        });

        self.VesselTypeSortGrid;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSortGrid = value;
        });

        self.ReasonforvisitSortGrid;
        self.ReasonforvisitName.subscribe(function (value) {
            self.ReasonforvisitSortGrid = value;
        });

        self.ArvStatusSortGrid;
        self.ArvStatus.subscribe(function (value) {
            self.ArvStatusSortGrid = value;
        });

        self.ETASortGrid;
        self.ETA.subscribe(function (value) {
            self.ETASortGrid = value;
        });

        self.ETDSortGrid;
        self.ETD.subscribe(function (value) {
            self.ETDSortGrid = value;
        });

        self.NominationDateSortGrid;
        self.NominationDate.subscribe(function (value) {
            self.NominationDateSortGrid = value;
        });

        self.cache = function () { };
        self.set(data);


    }



    //ArrivalNotificationModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ArrivalNotificationModel = function (data, referenceData) {
        var self = this;
        self.ArrivaReasonArray = ko.observableArray([]);
        self.AppliedForISPSChanged = ko.observable(false);
        self.VCN = ko.observable("");
        self.CancelRemarks = ko.observable("");

        

        self.WokflowFlag = ko.observable("");

        self.WFStatus = ko.observable(data ? data.WFStatus : "");
        self.WFCode = ko.observable(data ? data.WFCode : "");

        //By Mahesh : For Cancellation:
        self.SecondaryAgentID1 = ko.observable(data ? data.SecondaryAgentID1 : "");
        self.SecondaryAgentID2 = ko.observable(data ? data.SecondaryAgentID2 : "");

        self.SecondaryAgent1Name = ko.observable(data ? data.SecondaryAgent1Name : "");
        self.SecondaryAgent2Name = ko.observable(data ? data.SecondaryAgent2Name : "");     

        self.IsANFinal = ko.observable(data ? data.IsANFinal : "");
        self.IsISPSANFinal = ko.observable(data ? data.IsISPSANFinal : "");
        self.IsPHANFinal = ko.observable(data ? data.IsPHANFinal : "");
        self.IsIMDGANFinal = ko.observable(data ? data.IsIMDGANFinal : "");
        self.GRT = ko.observable(data ? data.GRT : "");

        self.isEditVisible = ko.computed(function () {
            if (self.IsANFinal() == "Y" && self.IsPHANFinal() == "Y" && (self.IsIMDGANFinal() == "NA" || self.IsIMDGANFinal() == "Y") && (self.IsISPSANFinal() == "NA" || self.IsISPSANFinal() == "Y")) {
                return false;
            }
            else {
                return true;
            }
        }, this);       


        self.Tempid = ko.observable("");
        self.WorkflowInstanceId = ko.observable();
        self.PortCode = ko.observable("");
        self.AgentID = ko.observable("");
        self.VesselID = ko.observable("");

        self.TerminalOperatorid = ko.observable("");
        self.RegisteredName = ko.observable("");

        // For workflow cancellation remarks
        self.workflowRemarks = ko.observable();
             
        self.VesselName = ko.observable("").extend({ required: true });
        self.VesselType = ko.observable("");
        self.VesselData = ko.observable();
        self.IMONo = ko.observable("");
        self.DockingPlanNo = ko.observable("");
        self.DockingPlanID = ko.observable("");
        self.DockIsFinal = ko.observable("");
        self.DockStatus = ko.observable("");

        self.SubmissionDate = ko.observable("");

        self.UserType = ko.observable("");
        self.VoyageIn = ko.observable("").extend({ required: true });
        self.UserType = ko.observable("");
        self.UserName = ko.observable("");
        self.ContactNo = ko.observable("");
        // Srini
        self.ETAFrom = ko.observable("");

        self.ETATo = ko.observable("");
        // Srini End

        self.VoyageOut = ko.observable("").extend({ required: true });
        
        self.ETA = ko.observable("").extend({ required: true });

        
        self.ETD = ko.observable("").extend({ required: true });

        
        self.ArrDraft = ko.observable("").extend({ required: true });        
        self.SpecifyReason = ko.observable("");
        
        self.DepDraft = ko.observable("").extend({ required: true });

        self.ReasonForVisit = ko.observable().extend({ required: true });
        self.ReasonforvisitName = ko.observable();


        self.ReasonForVisitList = ko.observableArray([]);
        self.IsTerminalOperator = ko.observable("I");
        self.TerminalOperatorID = ko.observable();        
        self.LastPortOfCall = ko.observable("").extend({ required: true });
        self.VslSrchOn = ko.observable("");        
        self.NextPortOfCall = ko.observable("").extend({ required: true });
        self.NominationDate = ko.observable("");
        self.PreferedBerthKey = ko.observable("").extend({ required: true });
        self.PilotExemptionChecked = ko.observable(false);
        self.DraftKey = ko.observable("");        
        self.AlternateBerthKey = ko.observable("");
        self.IsSamsaArrested = ko.observable("");
        self.DryDockBerthKey = ko.observable("");
        self.AppliedForISPS = ko.observable(data ? data.AppliedForISPS : "I");
        self.ViewModeForTabs = ko.observable('notification1');

        self.bunkersVisible = ko.observable(false);
        self.layByVisble = ko.observable(false);
        self.AppliedDate = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.AppliedForISPS() === "A"); }
            }
        });
        self.Clearance = ko.observable(data ? data.Clearance : "I");
        self.ISPSReferenceNo = ko.observable("");

        self.TerminalOperatorID = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.IsTerminalOperator() === "A"); }
            }
        });


        self.ExemptionPilotID = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.PilotExemptionChecked() === true); }
            }
        });     





        self.PilotExemption = ko.observable("");
        self.MasterName = ko.observable("I");

        self.PreferredBerthCode = ko.observable();
        self.AlternateBerthCode = ko.observable("");        
        self.PreferredSideDock = ko.observable().extend({ required: true });

                    
        self.PreferredSideAlternateBirth = ko.observable("");


        self.ReasonAlternateBirth = ko.observable("");
        self.Tidal = ko.observable(data ? data.Tidal : "I");
        self.BallastWater = ko.observable(data ? data.BallastWater : "I");
        self.WasteDeclaration = ko.observable(data ? data.WasteDeclaration : "I");
        self.DaylightRestriction = ko.observable(data ? data.DaylightRestriction : "I");
        self.IsSpecialNature = ko.observable(data ? data.IsSpecialNature : "I");
        

        self.ExceedPortLimitations = ko.observable(data ? data.ExceedPortLimitations : "I");
        self.ExceedSpecifyReason = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.ExceedPortLimitations() === "A"); }
            }
        });
        self.DaylightSpecifyReason = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.DaylightRestriction() === "A"); }
            }
        });

        self.SpecialNatureReason = ko.observable("").extend({
            required: {
                message: 'This field is required',
                onlyIf: function () { return (self.IsSpecialNature() === "A"); }
            }
        });

     

        self.AnyAdditionalInfo = ko.observable("");
        self.PlanDateTimeOfBerth = ko.observable("").extend({ required: true });
        self.PlanDateTimeToVacateBerth = ko.observable("").extend({ required: true });
        self.PlanDateTimeToStartCargo = ko.observable("").extend({ required: true });
        self.PlanDateTimeToCompleteCargo = ko.observable("").extend({ required: true });
        self.Daycnt = ko.observable("");
        self.AnyDangerousGoodsonBoard = ko.observable(data ? data.AnyDangerousGoodsonBoard : "I");       
            


        self.UNNo = ko.observable("");
        self.DangerousGoodsClass = ko.observable("");
        self.CargoTypeClass = ko.observable("");        



        self.IMDGNetQty = ko.observable("");       


        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.VesselTypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSort = value;
        });

        self.ReasonforvisitNameSort;
        self.ReasonforvisitName.subscribe(function (value) {
            self.ReasonforvisitNameSort = value;
        });

        self.ETASort;
        self.ETA.subscribe(function (value) {
            self.ETASort = value;
        });

        self.ETDSort;
        self.ETD.subscribe(function (value) {
            self.ETDSort = value;
        });

        self.NominationDateSort;
        self.NominationDate.subscribe(function (value) {
            self.NominationDateSort = value;
        });

        self.LoadDischargeDate = ko.observable("");
        self.DischargeDate = ko.observable("");

        self.CargoDescription = ko.observable("");
        self.CellNo = ko.observable("");

        $("#CellNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });     


        // Waste Declaration Information
        self.Marpol = ko.observable("");
        self.Class = ko.observable("");
        self.ServiceProvider = ko.observable("");
        self.DeclarationName = ko.observable("");
        self.WasteQuantity = ko.observable("");
        self.LastPortWasteDelivered = ko.observable("");
        self.NextPortWasteDelivery = ko.observable("");
        self.DateLastWasteDelivered = ko.observable("");


        // Lay By / Repair
        self.PlannedDurationDate = ko.observable("");
        self.PlannedDurationToDate = ko.observable("");
        self.ReasonForLayup = ko.observable("");       


        // Bunkers Information  
        self.BunkersRequired = ko.observable("");
        self.BunkersMethod = ko.observable("");
        self.BunkerService = ko.observable("");
        self.DistanceFromStern = ko.observable("");
        self.TonsMT = ko.observable("");
        self.AnyImpInfo = ko.observable("");        

        self.RecordStatus = ko.observable('A');

        self.Status = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "In Active";
        });

        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable("");

        self.ArrivalCommodities = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalCommodities, function (commodity) {
            return new ArrivalCommodity(commodity);
        }) : []);

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.ArrivalIMDGTankers = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalIMDGTankers, function (arrivalIMDGTanker) {
            return new ArrivalIMDGTanker(arrivalIMDGTanker);
        }) : []);


        self.IMDGInformations = ko.observableArray(data ? ko.utils.arrayMap(data.IMDGInformations, function (arrivalIMDGTanker) {
            return new IMDGContainerInformationdetails(arrivalIMDGTanker);
        }) : []);

        self.WasteDeclarations = ko.observableArray(data ? ko.utils.arrayMap(data.WasteDeclarations, function (item) {
            return new WasteDeclarationDetails(item, referenceData);
        }) : []);

        

        self.ArrivalDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalDocuments, function (arrivalDocument) {

            return new ArrivalDocument(arrivalDocument);
        }) : []);       

        self.TerminalData = ko.observableArray();
        self.AgencyData = ko.observable();

        self.PortName = ko.observable();
        self.isView = ko.observable(true);
        self.DocumentCategoryList = ko.observableArray();
        self.DocumentTypeCode = ko.observable();
        self.UploadedFiles = ko.observableArray([]);
        self.BerthsList = ko.observableArray();
        self.CargoTypesList = ko.observableArray();
        self.DocksList = ko.observableArray();
        self.PurposeList = ko.observableArray();
        self.UomsList = ko.observableArray();
        self.DangerousGoodsList = ko.observableArray();
        self.DocumentCategoryList = ko.observableArray();
        self.TankersList = ko.observableArray();
        self.PilotsList = ko.observableArray();
        self.VesselList = ko.observableArray();
        self.BunkersList = ko.observableArray();
        self.MasterName = ko.observable();
        self.selectedChoice = ko.observable();
        self.VesselDisplay = ko.observable();
        self.DocumentList = ko.observableArray([]);
        
        self.CommoditiesQuantitiesList = ko.observableArray();
        self.EnableDisableAddNew = ko.observable(true);
        self.EnableDisableAddNewIMDG = ko.observable(true);

        
        self.Url = ko.observable(data ? '/FileDownload/Download/' + data.ArrivalDocuments.DocumentID : '/FileDownload/Download/');
        

        // Added by sandeep on 15-09-2015
        self.ArrivalCreatedAgent = ko.observable();
        //-- end

        self.cache = function () { };
        self.set(data);
    }
    //ArrivalCommodityModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ArrivalCommodity = function (data) {
        var self = this;
        self.ArrivalCommodityID = ko.observable(data ? data.ArrivalCommodityID : "");
        self.CommodityBerthKey = ko.observable(data ? data.CommodityBerthKey : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");
        self.Commodity = ko.observable(data ? data.Commodity : "" );
        self.Package = ko.observable(data ? data.Package : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');
    }


    //IMDG Container InformationModel Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var IMDGContainerInformationdetails = function (data) {
        var self = this;
        self.IMDGInformationID = ko.observable(data ? data.IMDGInformationID : 0);
        self.VCN = ko.observable(data ? data.VCN : '');
        self.Purpose = ko.observable(data ? data.Purpose : '');
        self.ClassCode = ko.observable(data ? data.ClassCode : '');
        self.CargoCode = ko.observable(data ? data.CargoCode : "");
        self.UNNo = ko.observable(data ? data.UNNo : '');
        self.Quantity = ko.observable(data ? data.Quantity : '');
        self.NoofContainer = ko.observable(data ? data.NoofContainer : '');
        self.Others = ko.observable(data ? data.Others : '');
        self.RecordStatus = ko.observable(data ? data.RecordStatus : '');
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');
    }

    var WasteDeclarationDetails = function (data, referenceData) {
        var self = this;
        self.WasteDeclarationID = ko.observable(data ? data.WasteDeclarationID : 0);
        self.VCN = ko.observable(data ? data.VCN : '');
        self.MarpolCode = ko.observable(data ? data.MarpolCode : '');
        self.ClassCode = ko.observable(data ? data.ClassCode : '');
        self.LicenseRequestID = ko.observable(data ? data.LicenseRequestID : "");
        self.Quantity = ko.observable(data ? data.Quantity : '');
        self.DeclarationName = ko.observable(data ? data.DeclarationName : '');
        self.Others = ko.observable(data ? data.Others : '');        
        self.RecordStatus = ko.observable(data ? data.RecordStatus : '');
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');

        self.MarpolTypes = ko.computed(function () {
            debugger;
            if (self.MarpolCode() == "" || self.MarpolCode() == undefined) {
                return [];
            }
            else {
                var marpolgroups = referenceData.Marpols();
                var selectedMarpolGroup = $.grep(marpolgroups, function (c) {
                    return c.MarpolCode() === self.MarpolCode();
                });
                //To sort javascript arrays
                selectedMarpolGroup[0].MarpolDetails().sort(function (a, b) {
                    var nameA = a.ClassName().toLowerCase(), nameB = b.ClassName().toLowerCase()
                    if (nameA < nameB)
                        return -1
                    if (nameA > nameB)
                        return 1
                    return 0
                })
                return selectedMarpolGroup[0].MarpolDetails();
            }
        });
    }


    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.EntityCode = ko.observable(data ? data.EntityCode : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");


    }

    //ArrivalIMDGTankerModel Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ArrivalIMDGTanker = function (data) {
        var self = this;
        self.ArrivalIMDGTankerID = ko.observable(data ? data.ArrivalIMDGTankerID : 0);
        self.Purpose = ko.observable(data ? data.Purpose : '');
        self.Commodity = ko.observable(data ? data.Commodity : '');        
        self.Quantity = ko.observable(data ? data.Quantity : '');
        self.FromTank = ko.observable(data ? data.FromTank : '');
        self.RecordStatusncha = ko.observable(data ? data.RecordStatusncha : '');
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');
    }
    //ArrivalDocument Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ArrivalDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.DocumentCode = ko.observable(data ? data.DocumentCode : '');
        self.FileName = ko.observable(data ? data.FileName : '');
    }

    ipmsRoot.ArrivalNotificationReferenceData = ArrivalNotificationReferenceData;
    ipmsRoot.ArrivalNotificationReferenceBirthData = ArrivalNotificationReferenceBirthData;
    ipmsRoot.ArrivalNotificationReferenceDraftData = ArrivalNotificationReferenceDraftData;



    ipmsRoot.ArrivalNotificationModel = ArrivalNotificationModel;
    ipmsRoot.ArrivalNotificationModelGrid = ArrivalNotificationModelGrid;
    ipmsRoot.ArrivalNotificationModelSearchGrid = ArrivalNotificationModelSearchGrid;

    ipmsRoot.ArrivalCommodity = ArrivalCommodity;
    ipmsRoot.ArrivalDocument = ArrivalDocument;
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.ArrivalIMDGTanker = ArrivalIMDGTanker;
    ipmsRoot.IMDGContainerInformationdetails = IMDGContainerInformationdetails;
    ipmsRoot.Berth = Berth;
    ipmsRoot.Dock = Dock;
    ipmsRoot.Cargo = Cargo;
    ipmsRoot.Commodity = Commodity
    ipmsRoot.Purpose = Purpose;
    ipmsRoot.Uoml = Uoml;
    ipmsRoot.DangerousGood = DangerousGood;
    ipmsRoot.Doctype = Doctype;
    ipmsRoot.Tanker;    
    ipmsRoot.Pilot = Pilot;
    ipmsRoot.BirthTo = BirthTo;
    ipmsRoot.Vessel = Vessel;
    ipmsRoot.BunkersDetail = BunkersDetail;
    ipmsRoot.BunkersRequiredDetail = BunkersRequiredDetail;
    ipmsRoot.BunkersMethodDetail = BunkersMethodDetail;
    ipmsRoot.UserDetail = UserDetail;
    ipmsRoot.WasteDclServiceProvider = WasteDclServiceProvider;
    ipmsRoot.WasteDclClass = WasteDclClass;
    ipmsRoot.Marpol = Marpol;
    ipmsRoot.WasteDeclarationDetails = WasteDeclarationDetails;

    ipmsRoot.PortDetail = PortDetail;
    ipmsRoot.DraftDetail = DraftDetail;
}(window.IPMSROOT));


IPMSROOT.ArrivalNotificationModelSearchGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VCNSERCH(data ? data.VCNSERCH : "");
    self.VesselName(data ? data.VesselName : "");
    self.VesselID(data ? data.VesselID : "");
    self.IsserchIMDG(data ? data.IsserchIMDG : "All");
    self.IsserchISPS(data ? data.IsserchISPS : "All");

    self.IsserchIMDGClear(data ? data.IsserchIMDGClear : false);
    self.IsserchISPSClear(data ? data.IsserchISPSClear : false);
    self.IsserchPHOClear(data ? data.IsserchPHOClear : false);


    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate() + 30);
    fromdate.setDate(fromdate.getDate() - 30);
    self.ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
}


IPMSROOT.ArrivalNotificationModelGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    self.VesselType(data ? data.VesselType : "");
    self.ReasonforvisitName(data ? data.ReasonforvisitName : "");
    self.ETA(data ? (moment(data.ETA).format('YYYY-MM-DD HH:mm') || "") : "");
    self.ETD(data ? (moment(data.ETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.NominationDate(data ? (moment(data.NominationDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.wfStatus(data ? data.wfStatus : "");
    self.isEditVisible(data ? data.isEditVisible : "");
    self.isViewVisible(data ? data.isViewVisible : "");
    self.ReasonForVisit(data ? data.ReasonForVisit : "");
    self.workflowRemarks = ko.observable();
    self.IsANFinal(data ? data.IsANFinal : "");
    self.IsISPSANFinal(data ? data.IsISPSANFinal : "");
    self.IsPHANFinal(data ? data.IsPHANFinal : "");
    self.IsIMDGANFinal(data ? data.IsIMDGANFinal : "");
    self.AnyDangerousGoodsonBoard(data ? data.AnyDangerousGoodsonBoard : "");
    self.IsSamsaArrested(data ? data.IsSamsaArrested : "");
    self.IsArrivaStatus(data ? data.IsArrivaStatus : "");
    self.IsPHANStatus(data ? data.IsPHANStatus : "");
    self.IsISPSANStatus(data ? data.IsISPSANStatus : "");
    self.IsIMDGANStatus(data ? data.IsIMDGANStatus : "");
    self.RecordStatus(data ? data.RecordStatus : "");
    self.ArrvwfRemarks(data ? data.ArrvwfRemarks : "");
    self.PHOwfRemarks(data ? data.PHOwfRemarks : "");
    self.ISPSwfRemarks(data ? data.ISPSwfRemarks : "");
    self.IMDGwfRemarks(data ? data.IMDGwfRemarks : "");
    self.ArvStatus(data ? data.ArvStatus : "");
    self.IsPrimary(data ? data.IsPrimary : "");
    self.CancelRemarks(data ? data.CancelRemarks : "");

    
}

//ArrivalNotificationModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.ArrivalNotificationModel.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselType(data ? data.VesselType : "");
    self.ArrivaReasonArray(data ? (data.ArrivaReasonArray || "") : "");

    self.WorkflowInstanceId(data ? data.WorkflowInstanceId : "");
    self.PortCode(data ? data.PortCode : "");
    self.AgentID(data ? data.AgentID : "");
    self.WFStatus(data ? data.WFStatus : "");

    self.VesselData(data ? new IPMSROOT.Vessel(data.Vessel) : "");
    self.VesselID(data ? data.VesselID : "");
    self.VesselName(data ? data.Vessel.VesselName : "");
    
    self.CancelRemarks(data ? data.CancelRemarks : "");

    self.SecondaryAgentID1(data ? data.SecondaryAgentID1 : "");
    self.SecondaryAgentID2(data ? data.SecondaryAgentID2 : "");
    self.SecondaryAgent1Name(data ? data.SecondaryAgent1Name : "");
    self.SecondaryAgent2Name(data ? data.SecondaryAgent2Name : "");

    

    self.TerminalOperatorid(data ? data.TerminalOperatorid : "");
    self.RegisteredName(data ? data.RegisteredName : "");


    self.ContactNo(data ? data.ContactNo : "");
    self.UserName(data ? data.UserName : "");
    self.VoyageIn(data ? data.VoyageIn : "");
    self.UserType(data ? data.UserType : "");
    self.VoyageOut(data ? data.VoyageOut : "");    
    self.ETA(data ? (moment(data.ETA).format('YYYY-MM-DD HH:mm') || "") : "");    
    self.ETD(data ? (moment(data.ETD).format('YYYY-MM-DD HH:mm') || "") : "");
    self.ArrDraft(data ? data.ArrDraft : "");    
    
    self.SpecifyReason(data ? data.SpecifyReason : "");


    self.DepDraft(data ? data.DepDraft : "");
    self.ReasonForVisit(data ? data.ReasonForVisit : '');
    self.ReasonforvisitName(data ? data.ReasonforvisitName : '');


    self.IsTerminalOperator(data ? data.IsTerminalOperator : "I");
    self.TerminalOperatorID(data ? data.TerminalOperatorID : "");

    self.LastPortOfCall(data ? data.LastPortOfCall : "");
    self.NextPortOfCall(data ? data.NextPortOfCall : "");
    self.NominationDate(data ? (moment(data.NominationDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.PreferedBerthKey(data ? data.PreferedBerthKey : undefined);
    self.DraftKey("");

    self.IsSamsaArrested(data ? data.IsSamsaArrested : "");

    self.AlternateBerthKey(data ? data.AlternateBerthKey : "");
    self.DryDockBerthKey(data ? data.DryDockBerthKey : "");

    self.AppliedForISPS(data ? data.AppliedForISPS : "I");


    self.AppliedDate(data ? data.AppliedDate : "");
    self.Clearance(data ? data.Clearance : "I");
    self.ISPSReferenceNo(data ? data.ISPSReferenceNo : "");
    self.PilotExemptionChecked(data ? data.PilotExemptionChecked : false);
    self.PilotExemption(data ? data.PilotExemption : "I");
    self.MasterName("I");
    self.ExemptionPilotID(data ? data.ExemptionPilotID : "");

    self.PreferredBerthCode(data ? data.PreferredBerthCode : "");
    self.AlternateBerthCode(data ? data.AlternateBerthCode : "");
    self.PreferredSideDock(data ? data.PreferredSideDock : null);
    self.PreferredSideAlternateBirth(data ? data.PreferredSideAlternateBirth : "");
    self.ReasonAlternateBirth(data ? data.ReasonAlternateBirth : "");
    self.Tidal(data ? data.Tidal : "I");
    self.BallastWater(data ? data.BallastWater : "I");
    self.WasteDeclaration(data ? data.WasteDeclaration : "I");
    self.DaylightRestriction(data ? data.DaylightRestriction : "I");
    self.IsSpecialNature(data ? data.IsSpecialNature : "I");



    self.ExceedPortLimitations(data ? data.ExceedPortLimitations : "I");
    self.ExceedSpecifyReason(data ? data.ExceedSpecifyReason : "");
    self.DaylightSpecifyReason(data ? data.DaylightSpecifyReason : "");
    self.SpecialNatureReason(data ? data.SpecialNatureReason : "");

    
    self.ExceedSpecifyReason(data ? data.ExceedSpecifyReason : "");
    self.AnyAdditionalInfo(data ? data.AnyAdditionalInfo : "");  

    self.PlanDateTimeOfBerth(data ? data.PlanDateTimeOfBerth ? (moment(data.PlanDateTimeOfBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToVacateBerth(data ? data.PlanDateTimeToVacateBerth ? (moment(data.PlanDateTimeToVacateBerth).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToStartCargo(data ? data.PlanDateTimeToStartCargo ? (moment(data.PlanDateTimeToStartCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");
    self.PlanDateTimeToCompleteCargo(data ? data.PlanDateTimeToCompleteCargo ? (moment(data.PlanDateTimeToCompleteCargo).format('YYYY-MM-DD HH:mm') || "") : "" : "");


    self.LastPortWasteDelivered(data ? data.LastPortWasteDelivered  : "");
    self.NextPortWasteDelivery(data ? data.NextPortWasteDelivery : "");
    self.DateLastWasteDelivered(data ? data.DateLastWasteDelivered ? (moment(data.DateLastWasteDelivered).format('YYYY-MM-DD HH:mm') || "") : "" : "");

    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    // Tab 2 
    self.AnyDangerousGoodsonBoard(data ? data.AnyDangerousGoodsonBoard : "I");
    self.CargoTypeClass(data ? data.CargoTypeClass : "");
    self.DangerousGoodsClass(data ? data.DangerousGoodsClass : "");

    self.UNNo(data ? data.UNNo : "");
    self.LoadDischargeDate(data ? data.LoadDischargeDate : "");
    self.DischargeDate(data ? data.DischargeDate : "");


    self.IMDGNetQty(data ? data.IMDGNetQty : "");
    self.CellNo(data ? data.CellNo : "");
    self.CargoDescription(data ? data.CargoDescription : "");
    self.PlannedDurationDate(data ? data.PlannedDurationDate : "");
    self.Daycnt(data ? data.Daycnt : "");

    self.ReasonForLayup(data ? data.ReasonForLayup : "");
    self.PlannedDurationToDate(data ? data.PlannedDurationToDate : "");
    // Bunkers Information
    self.BunkersRequired(data ? data.BunkersRequired : "");
    self.BunkersMethod(data ? data.BunkersMethod : "");
    self.BunkerService(data ? data.BunkerService : "");
    self.DistanceFromStern(data ? data.DistanceFromStern : "");
    self.TonsMT(data ? data.TonsMT : "");
    self.AnyImpInfo(data ? data.AnyImpInfo : "");
    self.RecordStatus(data ? data.AnyImpInfo : "A");
    self.CreatedBy(data ? data.CreatedBy : "");
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : "");
    self.ModifiedDate(data ? data.ModifiedDate : "");

    self.IsANFinal(data ? data.IsANFinal : "");
    self.IsISPSANFinal(data ? data.IsISPSANFinal : "");
    self.IsPHANFinal(data ? data.IsPHANFinal : "");
    self.IsIMDGANFinal(data ? data.IsIMDGANFinal : "");

    // Added by sandeep on 15-09-2015
    self.ArrivalCreatedAgent(data ? data.ArrivalCreatedAgent : "");
    //-- end


    self.cache.latestData = data;
}
IPMSROOT.ArrivalNotificationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


function ValidatenumericwithDigit(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.\b]/;

    return charcheck.test(keychar);
}

function allowOnlyTwoPositiveDigts(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function allowOnlyTwoPositiveDigtsOnly(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    if (dotPos == -1 && charCode != 46 && number[0].length > 1) {
        return false;
    }
    if (number[0].length > 1 && dotPos == 2 && caratPos <= dotPos) {
        return false;
    }
    return true;
}

//thanks: http://javascript.nwbox.com/cursor_position/
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}




////To validate numeric
function ValidateNumericonly(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9\b]/;
    return charcheck.test(keychar);
}


