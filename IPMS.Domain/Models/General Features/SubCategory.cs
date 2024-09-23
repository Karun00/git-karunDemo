using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SubCategory : EntityBase
    {
        public SubCategory()
        {

            this.Addresses = new List<Address>();
            this.Addresses1 = new List<Address>();
            this.ArvDocument = new List<ArrivalDocument>();
            this.AgentPorts = new List<AgentPort>();
            this.ArrivalApprovals = new List<ArrivalApproval>();
            this.ArrivalCommodities = new List<ArrivalCommodity>();
            this.ArrivalCommodities1 = new List<ArrivalCommodity>();
            this.ArrivalCommodities2 = new List<ArrivalCommodity>();
            this.ArrivalIMDGTankers = new List<ArrivalIMDGTanker>();
            this.ArrivalIMDGTankers1 = new List<ArrivalIMDGTanker>();
            this.ArrivalIMDGInformatin = new List<IMDGInformation>();
            this.AuthorizedContactPersons = new List<AuthorizedContactPerson>();
            this.Berths = new List<Berth>();            
            this.ArrivalNotifications12 = new List<ArrivalNotification>();
            this.ArrivalNotifications13 = new List<ArrivalNotification>();
            this.ArrivalNotifications1 = new List<ArrivalNotification>();
            this.ArrivalNotifications2 = new List<ArrivalNotification>();
            this.ArrivalNotifications3 = new List<ArrivalNotification>();
            this.ArrivalReasons = new List<ArrivalReason>();
            this.BerthCargoes = new List<BerthCargo>();
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthMaintenances1 = new List<BerthMaintenance>();
            this.IMDGInformations = new List<IMDGInformation>();
            this.IMDGInformations1 = new List<IMDGInformation>();            
            this.ResourceAllocationConfigRules = new List<ResourceAllocationConfigRule>();
            this.ResourceAllocationMovementTypeRules = new List<ResourceAllocationMovementTypeRule>();
            this.BerthMaintenanceApprovals = new List<BerthMaintenanceApproval>();
            this.BerthMaintenanceCompApprovals = new List<BerthMaintenanceCompApproval>();

            this.BerthReasonForVisits = new List<BerthReasonForVisit>();
            this.BerthVesselTypes = new List<BerthVesselType>();
            this.CargoManifest = new List<CargoManifest>();
            this.CargoManifestDtl = new List<CargoManifestDtl>();
            this.CargoManifestDtl1 = new List<CargoManifestDtl>();
            this.Crafts8 = new List<Craft>();
            this.Crafts1 = new List<Craft>();
            this.Crafts2 = new List<Craft>();
            this.Crafts3 = new List<Craft>();
            this.Crafts4 = new List<Craft>();
            this.Crafts5 = new List<Craft>();
            this.Crafts6 = new List<Craft>();
            this.Crafts7 = new List<Craft>();
            this.Pilots = new List<Pilot>();            

            this.DeploymentBudgets = new List<DeploymentBudget>();          
            this.Documents = new List<Document>();
            this.Documents1 = new List<Document>();

            this.Employees = new List<Employee>();
            this.Employees1 = new List<Employee>();
            this.Employees2 = new List<Employee>();
            this.Employees3 = new List<Employee>();
            this.Employees4 = new List<Employee>();
            this.Employees5 = new List<Employee>();
            this.Employees6 = new List<Employee>();
            this.Employees7 = new List<Employee>();
            this.Employees8 = new List<Employee>();
            this.EntityPrivileges = new List<EntityPrivilege>();
            this.NotificationTemplates = new List<NotificationTemplate>();
            this.IncidentNatures = new List<IncidentNature>();
            this.LicenseRequests = new List<LicenseRequest>();
            this.LicenseRequestPorts = new List<LicenseRequestPort>();
            this.Hour24Report625 = new List<Hour24Report625>();          

            this.ResourceAllocations = new List<ResourceAllocation>();
            this.ResourceAllocations1 = new List<ResourceAllocation>();
            this.ResourceAllocations2 = new List<ResourceAllocation>();
            this.ResourceAllocations3 = new List<ResourceAllocation>();

            this.ResourceAttendances = new List<ResourceAttendance>();
            this.SAPPostings = new List<SAPPosting>();
            this.SAPPostings1 = new List<SAPPosting>();
            this.SAPPostings2 = new List<SAPPosting>();
            this.ServiceRequests = new List<ServiceRequest>();
            this.ServiceRequests1 = new List<ServiceRequest>();
            this.ServiceRequestApprovals = new List<ServiceRequestApproval>();            
            this.Section625GDetail1 = new List<Section625GDetail1>();
            this.Section625CDetail = new List<Section625CDetail>();
            this.Section625DDetail = new List<Section625DDetail>();
            this.StatementFacts = new List<StatementFact>();
            this.StatementFactEvents = new List<StatementFactEvent>();
            this.ServiceRequestDocuments = new List<ServiceRequestDocument>();
            this.TerminalOperators = new List<TerminalOperator>();
            this.TerminalOperatorCargoHandlings = new List<TerminalOperatorCargoHandling>();
            this.UserPorts = new List<UserPort>();
            this.Users = new List<User>();
            this.Vessels = new List<Vessel>();            
            this.Vessels2 = new List<Vessel>();
            this.Vessels3 = new List<Vessel>();
            this.VesselAgentChangeApprovals = new List<VesselAgentChangeApproval>();
            this.VesselApprovals = new List<VesselApproval>();
            this.VesselCallAnchorages = new List<VesselCallAnchorage>();
            this.VesselCallMovements = new List<VesselCallMovement>();
            this.VesselCallMovements1 = new List<VesselCallMovement>();
            this.VesselCallMovementsMovementType = new List<VesselCallMovement>();
            this.VesselEngines = new List<VesselEngine>();
            this.VesselEngines1 = new List<VesselEngine>();
            this.WorkflowInstances = new List<WorkflowInstance>();
            this.WorkflowProcess = new List<WorkflowProcess>();
            this.WorkflowProcess1 = new List<WorkflowProcess>();
            this.WorkflowTasks = new List<WorkflowTask>();

            this.PortConfigurations = new List<PortConfiguration>();
            this.PilotExemptionRequests = new List<PilotExemptionRequest>();
            this.PilotExemptionRequests1 = new List<PilotExemptionRequest>();


            this.PortConfigurations1 = new List<PortConfiguration>();
            this.PortConfigurations2 = new List<PortConfiguration>();
            this.PortConfigurations3 = new List<PortConfiguration>();
            this.VesselAgentChanges = new List<VesselAgentChange>();
            this.VesselCertificateDetails = new List<VesselCertificateDetail>();
            this.ResourceGroups = new List<ResourceGroup>();
            this.ResourceRosters = new List<ResourceRoster>();
            this.Rosters = new List<Roster>();
            // -- Added by Sandeep on 29-07-2014

            this.DivingOccupationApprovals = new List<DivingOccupationApproval>();

            // -- end

            // -- Added by sandeep on 20-8-2014
            this.SlotPriorityConfigurations = new List<SlotPriorityConfiguration>();
            this.SuppMiscServices = new List<SuppMiscService>();         
            this.SuppServiceRequests = new List<SuppServiceRequest>();
            // -- end

            this.FuelReceipts = new List<FuelReceipt>();
            this.FuelReceipts1 = new List<FuelReceipt>();
            this.FuelRequisitionApprovals = new List<FuelRequisitionApproval>();
            this.FuelRequisitions = new List<FuelRequisition>();
            this.FuelRequisitions1 = new List<FuelRequisition>();
            this.FuelRequisitions2 = new List<FuelRequisition>();
            this.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
            this.RevenueAccountStatus = new List<RevenueAccountStatus>();

            this.ServiceTypes = new List<ServiceType>();
            // -- Added by sandeep on 27-09-2014
            this.ServiceTypeDesignations = new List<ServiceTypeDesignation>();
            this.ServiceTypeDesignations1 = new List<ServiceTypeDesignation>();
            // -- end
            this.PermitRequests = new List<PermitRequest>();
            this.PermitRequests1 = new List<PermitRequest>();

            this.PermitRequestAreas = new List<PermitRequestArea>();
            this.PermitRequestSubAreas = new List<PermitRequestSubArea>();
            
            this.PersonalPermits = new List<PersonalPermit>();
            this.PersonalPermits1 = new List<PersonalPermit>();
            this.PersonalPermits2 = new List<PersonalPermit>();
            this.PersonalPermits3 = new List<PersonalPermit>();
            this.VehiclePermits = new List<VehiclePermit>();
            this.VisitorPermits = new List<VisitorPermit>();
            this.WharfVehiclePermits = new List<WharfVehiclePermit>();
            this.WharfVehiclePermits1 = new List<WharfVehiclePermit>();
            this.WharfVehiclePermits2 = new List<WharfVehiclePermit>();
            this.VehiclePermitRequirementCodes = new List<VehiclePermitRequirementCode>();
            this.PermitRequestAccessGates = new List<PermitRequestAccessGates>();
            // -- Added by sandeep on 29-12-2014
            this.DredgingOperations = new List<DredgingOperation>();           
            this.DredgingOperations2 = new List<DredgingOperation>();
            // -- end
            this.ServiceRequestWarpings3 = new List<ServiceRequestWarping>();

            //-- Added by sandeep on 10-03-2015
            this.DivingRequests = new List<DivingRequest>();
            //-- end
            this.PilotageServiceRecordings = new List<PilotageServiceRecording>();
            this.StatementCommodities = new List<StatementCommodity>();
            this.StatementCommodities1 = new List<StatementCommodity>();
            this.StatementCommodities2 = new List<StatementCommodity>();
            this.StatementCommodities3 = new List<StatementCommodity>();
            this.AutomatedSlotBlockings = new List<AutomatedSlotBlocking>();
            this.Marpols = new List<Marpol>();
            this.WasteDeclarations = new List<WasteDeclaration>();
            this.SlotOverRidingReasons = new List<SlotOverRidingReasons>();
            this.IndividualPersonalPermits = new List<IndividualPersonalPermit>();
            this.IndividualPersonalPermits1 = new List<IndividualPersonalPermit>();
            this.IndividualPersonalPermits2 = new List<IndividualPersonalPermit>();
            this.IndividualPersonalPermits3 = new List<IndividualPersonalPermit>();
        }
        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string SupCatCode { get; set; }
        [DataMember]
        public string SubCatName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public  ICollection<Address> Addresses { get; set; }
        [DataMember]
        public  ICollection<Address> Addresses1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalDocument> ArvDocument { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts { get; set; }
        [DataMember]
        public  ICollection<ArrivalApproval> ArrivalApprovals { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities2 { get; set; }
        [DataMember]
        public  ICollection<ArrivalIMDGTanker> ArrivalIMDGTankers { get; set; }
        [DataMember]
        public  ICollection<ArrivalIMDGTanker> ArrivalIMDGTankers1 { get; set; }
        [DataMember]
        public  ICollection<IMDGInformation> ArrivalIMDGInformatin { get; set; }
       
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications12 { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications13 { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications2 { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications3 { get; set; }
        [DataMember]
        public  ICollection<IMDGInformation> IMDGInformations { get; set; }
        [DataMember]
        public  ICollection<IMDGInformation> IMDGInformations1 { get; set; }
        [DataMember]
        public  ICollection<AuthorizedContactPerson> AuthorizedContactPersons { get; set; }
        [DataMember]
        public  ICollection<Berth> Berths { get; set; }
        [DataMember]
        public  ICollection<BerthCargo> BerthCargoes { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots { get; set; }
        [DataMember]
        public  ICollection<RevenueAccountStatus> RevenueAccountStatus { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances1 { get; set; }       
        [DataMember]
        public  ICollection<BerthMaintenanceApproval> BerthMaintenanceApprovals { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals { get; set; }

        [DataMember]
        public  ICollection<BerthReasonForVisit> BerthReasonForVisits { get; set; }
        [DataMember]
        public  ICollection<BerthVesselType> BerthVesselTypes { get; set; }
        [DataMember]
        public  ICollection<CargoManifest> CargoManifest { get; set; }
        [DataMember]
        public  ICollection<CargoManifestDtl> CargoManifestDtl { get; set; }
        [DataMember]
        public  ICollection<CargoManifestDtl> CargoManifestDtl1 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts8 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts1 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts2 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts3 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts4 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts5 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts6 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts7 { get; set; }
        [DataMember]
        public  ICollection<CraftOutOfCommission> CraftOutOfCommissions { get; set; }
        [DataMember]
        public  ICollection<CraftOutOfCommission> CraftOutOfCommissions1 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs1 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs2 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs3 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs4 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs5 { get; set; }
        [DataMember]
        public  ICollection<DeploymentBudget> DeploymentBudgets { get; set; }        

        [DataMember]
        public  ICollection<Document> Documents { get; set; }
        [DataMember]
        public  ICollection<Document> Documents1 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees1 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees2 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees3 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees4 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees5 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees6 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees7 { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees8 { get; set; }
        [DataMember]
        public  ICollection<EntityPrivilege> EntityPrivileges { get; set; }
        [DataMember]
        public  ICollection<Hour24Report625> Hour24Report625 { get; set; }
        [DataMember]
        public  ICollection<IncidentNature> IncidentNatures { get; set; }
        [DataMember]
        public  ICollection<LicenseRequest> LicenseRequests { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts { get; set; }
        [DataMember]
        public  ICollection<MovementResourceAllocation> MovementResourceAllocations { get; set; }
        [DataMember]
        public  ICollection<Section625CDetail> Section625CDetail { get; set; }
        [DataMember]
        public  ICollection<Section625DDetail> Section625DDetail { get; set; }
        [DataMember]
        public  ICollection<Section625GDetail1> Section625GDetail1 { get; set; }       
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations1 { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations2 { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations3 { get; set; }
        [DataMember]
        public  ICollection<SlotPriorityConfiguration> SlotPriorityConfigurations { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestApproval> ServiceRequestApprovals { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }

        [DataMember]
        public  ICollection<StatementFact> StatementFacts { get; set; }
        [DataMember]
        public  ICollection<StatementFactEvent> StatementFactEvents { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestDocument> ServiceRequestDocuments { get; set; }
        [DataMember]
        public  ICollection<NotificationTemplate> NotificationTemplates { get; set; }
        [DataMember]
        public  ICollection<TerminalOperator> TerminalOperators { get; set; }
        [DataMember]
        public  ICollection<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts { get; set; }
        [DataMember]
        public  ICollection<User> Users { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels { get; set; }        
        [DataMember]
        public  ICollection<Vessel> Vessels2 { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels3 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals { get; set; }
        [DataMember]
        public  ICollection<VesselApproval> VesselApprovals { get; set; }
        [DataMember]
        public  ICollection<VesselCallAnchorage> VesselCallAnchorages { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements1 { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovementsMovementType { get; set; }


        [DataMember]
        public  ICollection<VesselEngine> VesselEngines { get; set; }
        [DataMember]
        public  ICollection<VesselEngine> VesselEngines1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowInstance> WorkflowInstances { get; set; }
        [DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess { get; set; }
        [DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowTask> WorkflowTasks { get; set; }
        [DataMember]
        public  ICollection<PortConfiguration> PortConfigurations { get; set; }
        [DataMember]
        public  ICollection<PortConfiguration> PortConfigurations1 { get; set; }
        [DataMember]
        public  ICollection<PortConfiguration> PortConfigurations2 { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests1 { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests { get; set; }
        [DataMember]
        public  ICollection<PortConfiguration> PortConfigurations3 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges { get; set; }
        [DataMember]
        public  ICollection<VesselCertificateDetail> VesselCertificateDetails { get; set; }
        [DataMember]
        public  ICollection<ResourceGroup> ResourceGroups { get; set; }
        [DataMember]
        public  ICollection<ResourceRoster> ResourceRosters { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendance> ResourceAttendances { get; set; }
        [DataMember]
        public  ICollection<Roster> Rosters { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings1 { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings2 { get; set; }

       

        // -- Added by sandeep on 29-07-2014

        [DataMember]
        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals { get; set; }
        // -- end

        // -- Added by sandeep on 20-8-2014
        [DataMember]
        public  ICollection<ResourceAllocationConfigRule> ResourceAllocationConfigRules { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocationMovementTypeRule> ResourceAllocationMovementTypeRules { get; set; }

        [DataMember]
        public  ICollection<SuppMiscService> SuppMiscServices { get; set; }    
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        // -- end

        //-- Added by Srini

        [DataMember]
        public  ICollection<SuppHotWorkInspection> SuppHotWorkInspections { get; set; }
        //---- 


        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts { get; set; }
        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts1 { get; set; }
        [DataMember]
        public  ICollection<FuelRequisitionApproval> FuelRequisitionApprovals { get; set; }
        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions { get; set; }
        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions1 { get; set; }
        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions2 { get; set; }

        [DataMember]
        public  ICollection<ServiceType> ServiceTypes { get; set; }
        // -- Added by sandeep on 27-09-2014
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations { get; set; }
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations1 { get; set; }
        // -- end
        //Added by shankar on 07-Nov-14
        public  ICollection<PortContentRole> PortContentRoles { get; set; }
        //end

        [DataMember]
        public  ICollection<PermitRequest> PermitRequests { get; set; }
        [DataMember]
        public  ICollection<PermitRequest> PermitRequests1 { get; set; }


        [DataMember]
        public  ICollection<PermitRequestArea> PermitRequestAreas { get; set; }
        [DataMember]
        public ICollection<PermitRequestSubArea> PermitRequestSubAreas { get; set; }
      
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits { get; set; }
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits1 { get; set; }
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits2 { get; set; }
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits3 { get; set; }
        [DataMember]
        public  ICollection<VehiclePermit> VehiclePermits { get; set; }
        [DataMember]
        public  ICollection<VisitorPermit> VisitorPermits { get; set; }
        [DataMember]
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits { get; set; }
        [DataMember]
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits1 { get; set; }
        [DataMember]
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits2 { get; set; }
        [DataMember]
        public  ICollection<PermitRequestAccessGates> PermitRequestAccessGates { get; set; }
        [DataMember]
        public  ICollection<VehiclePermitRequirementCode> VehiclePermitRequirementCodes { get; set; }
        // -- Added by sandeep on 29-12-2014
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }      
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations2 { get; set; }
        // -- end
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings3 { get; set; }
        [DataMember]
        public  ICollection<ArrivalReason> ArrivalReasons { get; set; }

        //-- Added by sandeep on 10-03-2015
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests { get; set; }
        //-- end
        [DataMember]
        public ICollection<PilotageServiceRecording> PilotageServiceRecordings { get; set; }

        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities { get; set; }
        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities1 { get; set; }
        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities2 { get; set; }
        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities3 { get; set; }
        [DataMember]
        public ICollection<AutomatedSlotBlocking> AutomatedSlotBlockings { get; set; }
        [DataMember]
        public ICollection<Marpol> Marpols { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations { get; set; }
        [DataMember]
        public ICollection<SlotOverRidingReasons> SlotOverRidingReasons { get; set; }

        [DataMember]
        public ICollection<PermitReason> PermitReasons { get; set; }

        [DataMember]
        public ICollection<IndividualPersonalPermit> IndividualPersonalPermits { get; set; }
        [DataMember]
        public ICollection<IndividualPersonalPermit> IndividualPersonalPermits1 { get; set; }
        [DataMember]
        public ICollection<IndividualPersonalPermit> IndividualPersonalPermits2 { get; set; }
        [DataMember]
        public ICollection<IndividualPersonalPermit> IndividualPersonalPermits3 { get; set; }

        
    }
}
