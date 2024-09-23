using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class User : EntityBase
    {
        public User()
        {
            this.AuditTrails = new List<AuditTrail>();
            this.Addresses = new List<Address>();
            this.Addresses1 = new List<Address>();
            this.Agents = new List<Agent>();
            this.Agents1 = new List<Agent>();
            this.AgentPorts = new List<AgentPort>();
            this.AgentPorts1 = new List<AgentPort>();
            this.AgentPorts2 = new List<AgentPort>();
            this.AgentPorts3 = new List<AgentPort>();
            this.ArrivalReasons = new List<ArrivalReason>();
            this.ArrivalReasons1 = new List<ArrivalReason>();
            this.AuditTrailConfigs = new List<AuditTrailConfig>();
            this.ArrivalApprovals = new List<ArrivalApproval>();
            this.ArrivalApprovals1 = new List<ArrivalApproval>();
            this.ArrivalApprovals2 = new List<ArrivalApproval>();
            this.ArrivalCommodities = new List<ArrivalCommodity>();
            this.ArrivalCommodities1 = new List<ArrivalCommodity>();
            this.ArrivalDocuments = new List<ArrivalDocument>();
            this.ArrivalDocuments1 = new List<ArrivalDocument>();
            this.ArrivalIMDGTankers = new List<ArrivalIMDGTanker>();
            this.ArrivalIMDGTankers1 = new List<ArrivalIMDGTanker>();
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.ArrivalNotifications1 = new List<ArrivalNotification>();
            this.AuthorizedContactPersons = new List<AuthorizedContactPerson>();
            this.AuthorizedContactPersons1 = new List<AuthorizedContactPerson>();
            this.AutomatedSlotConfigurations = new List<AutomatedSlotConfiguration>();
            this.AutomatedSlotConfigurations1 = new List<AutomatedSlotConfiguration>();
            this.Berths = new List<Berth>();
            this.Berths1 = new List<Berth>();
            this.BerthCargoes = new List<BerthCargo>();
            this.BerthCargoes1 = new List<BerthCargo>();
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthMaintenances1 = new List<BerthMaintenance>();
            this.BerthMaintenanceApprovals = new List<BerthMaintenanceApproval>();
            this.BerthMaintenanceApprovals1 = new List<BerthMaintenanceApproval>();
            this.BerthMaintenanceApprovals2 = new List<BerthMaintenanceApproval>();
            this.BerthMaintenanceCompApprovals = new List<BerthMaintenanceCompApproval>();
            this.BerthMaintenanceCompApprovals1 = new List<BerthMaintenanceCompApproval>();
            this.BerthMaintenanceCompApprovals2 = new List<BerthMaintenanceCompApproval>();
            this.BerthMaintenanceCompletions = new List<BerthMaintenanceCompletion>();
            this.BerthMaintenanceCompletions1 = new List<BerthMaintenanceCompletion>();

            this.BerthOccupationDocuments = new List<BerthOccupationDocument>();
            this.BerthOccupationDocuments1 = new List<BerthOccupationDocument>();
            this.BerthPlanningConfigurations = new List<BerthPlanningConfigurations>();
            this.BerthPlanningConfigurations1 = new List<BerthPlanningConfigurations>();
            this.BerthReasonForVisits = new List<BerthReasonForVisit>();
            this.BerthReasonForVisits1 = new List<BerthReasonForVisit>();
            this.BerthVesselTypes = new List<BerthVesselType>();
            this.BerthVesselTypes1 = new List<BerthVesselType>();
            this.BerthingTaskExecutions = new List<BerthingTaskExecution>();
            this.BerthingTaskExecutions1 = new List<BerthingTaskExecution>();
            this.Bollards = new List<Bollard>();
            this.Bollards1 = new List<Bollard>();
            this.PortRegistries = new List<PortRegistry>();
            this.PortRegistries1 = new List<PortRegistry>();
            this.Codes = new List<Code>();
            this.Codes1 = new List<Code>();
            this.CodeDefinitions = new List<CodeDefinition>();
            this.CodeDefinitions1 = new List<CodeDefinition>();
            this.CodeDtls = new List<CodeDtl>();
            this.CodeDtls1 = new List<CodeDtl>();
            this.Bunkerings = new List<Bunkering>();
            this.Bunkerings1 = new List<Bunkering>();
            this.CargoManifest = new List<CargoManifest>();
            this.CargoManifest1 = new List<CargoManifest>();
            this.CargoManifestDtl = new List<CargoManifestDtl>();
            this.CargoManifestDtl1 = new List<CargoManifestDtl>();
            this.Crafts = new List<Craft>();
            this.Crafts1 = new List<Craft>();
            this.Conversations = new List<Conversation>();
            this.Conversations1 = new List<Conversation>();
            this.Conversations2 = new List<Conversation>();
            this.Conversations3 = new List<Conversation>();
            this.ConversationReplies = new List<ConversationReply>();
            this.ConversationReplies1 = new List<ConversationReply>();
            this.ConversationReplies2 = new List<ConversationReply>();
            this.CraftOutOfCommissions = new List<CraftOutOfCommission>();
            this.CraftOutOfCommissions1 = new List<CraftOutOfCommission>();
            this.CraftReminderConfigs = new List<CraftReminderConfig>();
            this.CraftReminderConfigs1 = new List<CraftReminderConfig>();
            this.Departments = new List<Department>();
            this.Departments1 = new List<Department>();
            this.DeploymentBudgets = new List<DeploymentBudget>();
            this.DeploymentBudgets1 = new List<DeploymentBudget>();
            this.DeploymentPlans = new List<DeploymentPlan>();
            this.DeploymentPlans1 = new List<DeploymentPlan>();
            this.Divings = new List<Diving>();
            this.Divings1 = new List<Diving>();
            this.IMDGInformations = new List<IMDGInformation>();
            this.IMDGInformations1 = new List<IMDGInformation>();    
            this.DivingOccupationApprovals = new List<DivingOccupationApproval>();
            this.DivingOccupationApprovals1 = new List<DivingOccupationApproval>();
            this.DivingOccupationApprovals2 = new List<DivingOccupationApproval>();
            this.DockingPlans = new List<DockingPlan>();
            this.DockingPlans1 = new List<DockingPlan>();          
            this.DredgingPriorities = new List<DredgingPriority>();
            this.DredgingPriorities1 = new List<DredgingPriority>();

            this.DredgingPriorityDocuments = new List<DredgingPriorityDocument>();
            this.DredgingPriorityDocuments1 = new List<DredgingPriorityDocument>();

            this.Employees = new List<Employee>();
            this.Employees1 = new List<Employee>();
            this.Entities = new List<Entity>();
            this.Entities1 = new List<Entity>();
            this.FireEquipments = new List<FireEquipment>();
            this.FireEquipments1 = new List<FireEquipment>();
            this.FireProtections = new List<FireProtection>();
            this.FireProtections1 = new List<FireProtection>();
            this.FloatingCranes = new List<FloatingCrane>();
            this.FloatingCranes1 = new List<FloatingCrane>();
            this.FloatingCraneTaskExecutions = new List<FloatingCraneTaskExecution>();
            this.FloatingCraneTaskExecutions1 = new List<FloatingCraneTaskExecution>();


            this.Drafts = new List<Draft>();
            this.Drafts1 = new List<Draft>();

            this.FuelConsumptionDailyLogs = new List<FuelConsumptionDailyLog>();
            this.FuelConsumptionDailyLogs1 = new List<FuelConsumptionDailyLog>();
            this.Hour24Report625 = new List<Hour24Report625>();
            this.Hour24Report6251 = new List<Hour24Report625>();

            this.Incidents = new List<Incident>();
            this.Incidents1 = new List<Incident>();
            this.IncidentNatures = new List<IncidentNature>();
            this.IncidentNatures1 = new List<IncidentNature>();          
            this.LicenseRequests = new List<LicenseRequest>();
            this.LicenseRequests1 = new List<LicenseRequest>();
            this.LicenseRequestPorts = new List<LicenseRequestPort>();
            this.LicenseRequestPorts1 = new List<LicenseRequestPort>();
            this.LicenseRequestPorts2 = new List<LicenseRequestPort>();
            this.LicenseRequestPorts3 = new List<LicenseRequestPort>();
            this.LicenseRequestDocuments = new List<LicenseRequestDocument>();
            this.LicenseRequestDocuments1 = new List<LicenseRequestDocument>();

            this.Modules = new List<Module>();
            this.Modules1 = new List<Module>();
            this.MovementResourceAllocations = new List<MovementResourceAllocation>();
            this.MovementResourceAllocations1 = new List<MovementResourceAllocation>();     
            this.ResourceAllocationConfigRules = new List<ResourceAllocationConfigRule>();
            this.ResourceAllocationConfigRules1 = new List<ResourceAllocationConfigRule>();
            this.News = new List<News>();
            this.News1 = new List<News>();
            this.NewsPort = new List<NewsPort>();
            this.NewsPort1 = new List<NewsPort>();                        
            this.Notifications = new List<Notification>();
            this.Notifications1 = new List<Notification>();
            this.NotificationPorts = new List<NotificationPort>();
            this.NotificationPorts1 = new List<NotificationPort>();
            this.NotificationRoles = new List<NotificationRole>();
            this.NotificationRoles1 = new List<NotificationRole>();
            this.NotificationTemplates = new List<NotificationTemplate>();
            this.OtherServiceRecordings = new List<OtherServiceRecording>();
            this.OtherServiceRecordings1 = new List<OtherServiceRecording>();
            this.PestControls = new List<PestControl>();
            this.PestControls1 = new List<PestControl>();
            this.Pilots = new List<Pilot>();
            this.Pilots1 = new List<Pilot>();
            this.PilotageServiceRecordings = new List<PilotageServiceRecording>();
            this.PilotageServiceRecordings1 = new List<PilotageServiceRecording>();
            this.PilotageTaskExecutions = new List<PilotageTaskExecution>();
            this.PilotageTaskExecutions1 = new List<PilotageTaskExecution>();
            this.PilotBoatTaskExecutions = new List<PilotBoatTaskExecution>();
            this.PilotBoatTaskExecutions1 = new List<PilotBoatTaskExecution>();
            this.PilotCertificates = new List<PilotCertificate>();
            this.PilotCertificates1 = new List<PilotCertificate>();
            this.PilotExemptionRequests = new List<PilotExemptionRequest>();
            this.PilotExemptionRequests1 = new List<PilotExemptionRequest>();
            this.PilotExemptionRequestDocuments = new List<PilotExemptionRequestDocument>();
            this.PilotExemptionRequestDocuments1 = new List<PilotExemptionRequestDocument>();
            this.PollutionControls = new List<PollutionControl>();
            this.PollutionControls1 = new List<PollutionControl>();
            this.Ports = new List<Port>();
            this.Ports1 = new List<Port>();
            this.Quays = new List<Quay>();
            this.Quays1 = new List<Quay>();
            this.ResourceAttendances = new List<ResourceAttendance>();
            this.ResourceAttendances1 = new List<ResourceAttendance>();
            this.ResourceAttendanceDtls = new List<ResourceAttendanceDtl>();
            this.ResourceAttendanceDtls1 = new List<ResourceAttendanceDtl>();

            this.ResourceAllocations = new List<ResourceAllocation>();
            this.ResourceAllocations1 = new List<ResourceAllocation>();
            this.ResourceAllocations2 = new List<ResourceAllocation>();
            this.RevenueStopLists = new List<RevenueStopList>();
            this.RevenueStopLists1 = new List<RevenueStopList>();
            this.RosterDtls = new List<RosterDtl>();
            this.RosterDtls1 = new List<RosterDtl>();
            this.Roles = new List<Role>();
            this.Roles1 = new List<Role>();

            this.SAPPostings = new List<SAPPosting>();
            this.SAPPostings1 = new List<SAPPosting>();
            this.ServiceTypes = new List<ServiceType>();
            this.ServiceTypes1 = new List<ServiceType>();
            this.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
            this.ShiftingBerthingTaskExecutions1 = new List<ShiftingBerthingTaskExecution>();
            this.ServiceRequests = new List<ServiceRequest>();
            this.ServiceRequests1 = new List<ServiceRequest>();
            this.ServiceRequestApprovals = new List<ServiceRequestApproval>();
            this.ServiceRequestApprovals1 = new List<ServiceRequestApproval>();
            this.ServiceRequestApprovals2 = new List<ServiceRequestApproval>();
            this.ServiceRequestDocuments = new List<ServiceRequestDocument>();
            this.ServiceRequestDocuments1 = new List<ServiceRequestDocument>();
            this.ServiceRequestSailings = new List<ServiceRequestSailing>();
            this.ServiceRequestSailings1 = new List<ServiceRequestSailing>();
            this.ServiceRequestShiftings = new List<ServiceRequestShifting>();
            this.ServiceRequestShiftings1 = new List<ServiceRequestShifting>();
            this.ServiceRequestWarpings = new List<ServiceRequestWarping>();
            this.ServiceRequestWarpings1 = new List<ServiceRequestWarping>();
            this.SystemNotifications = new List<SystemNotification>();
            this.StatementFacts = new List<StatementFact>();
            this.StatementFacts1 = new List<StatementFact>();
            this.StatementFactBunkers = new List<StatementFactBunker>();
            this.StatementFactBunkers1 = new List<StatementFactBunker>();
            this.StatementFactEvents = new List<StatementFactEvent>();
            this.StatementFactEvents1 = new List<StatementFactEvent>();
            this.Stevedores = new List<Stevedore>();
            this.Stevedores1 = new List<Stevedore>();
            this.TerminalOperators = new List<TerminalOperator>();
            this.TerminalOperators1 = new List<TerminalOperator>();
            this.TugWorkboatTaskExecutions = new List<TugWorkboatTaskExecution>();
            this.TugWorkboatTaskExecutions1 = new List<TugWorkboatTaskExecution>();
            this.UserPorts = new List<UserPort>();
            this.UserPorts1 = new List<UserPort>();
            this.UserPorts2 = new List<UserPort>();
            this.UserPorts3 = new List<UserPort>();
            this.UserPorts4 = new List<UserPort>();
            this.UserRoles = new List<UserRole>();
            this.Users1 = new List<User>();

            this.Users11 = new List<User>();
            this.Vessels = new List<Vessel>();
            this.Vessels1 = new List<Vessel>();
            this.VesselAgentChanges = new List<VesselAgentChange>();
            this.VesselAgentChanges1 = new List<VesselAgentChange>();
            this.VesselAgentChangeApprovals = new List<VesselAgentChangeApproval>();
            this.VesselAgentChangeApprovals1 = new List<VesselAgentChangeApproval>();
            this.VesselAgentChangeApprovals2 = new List<VesselAgentChangeApproval>();
            this.VesselAgentChangeApprovals3 = new List<VesselAgentChangeApproval>();
            this.VesselAgentChangeDocuments = new List<VesselAgentChangeDocument>();
            this.VesselAgentChangeDocuments1 = new List<VesselAgentChangeDocument>();
            this.VesselApprovals = new List<VesselApproval>();
            this.VesselApprovals1 = new List<VesselApproval>();
            this.VesselApprovals2 = new List<VesselApproval>();
            this.VesselCalls = new List<VesselCall>();
            this.VesselCalls1 = new List<VesselCall>();
            this.VesselCallAnchorages = new List<VesselCallAnchorage>();
            this.VesselCallAnchorages1 = new List<VesselCallAnchorage>();

            this.VesselCallMovements = new List<VesselCallMovement>();
            this.VesselCallMovements1 = new List<VesselCallMovement>();
            this.VesselEngines = new List<VesselEngine>();
            this.VesselEngines1 = new List<VesselEngine>();
            this.VesselETAChanges = new List<VesselETAChange>();
            this.VesselETAChanges1 = new List<VesselETAChange>();
            this.VesselGears = new List<VesselGear>();
            this.VesselGears1 = new List<VesselGear>();
            this.VesselGrabs = new List<VesselGrab>();
            this.VesselGrabs1 = new List<VesselGrab>();
            this.VesselHatchHolds = new List<VesselHatchHold>();
            this.VesselHatchHolds1 = new List<VesselHatchHold>();
            this.WaterServiceTaskExecutions = new List<WaterServiceTaskExecution>();
            this.WaterServiceTaskExecutions1 = new List<WaterServiceTaskExecution>();
            this.WorkflowInstances = new List<WorkflowInstance>();
            this.WorkflowInstances1 = new List<WorkflowInstance>();
            this.WorkflowProcess = new List<WorkflowProcess>();
            this.WorkflowProcess1 = new List<WorkflowProcess>();
            this.WorkflowTasks = new List<WorkflowTask>();
            this.WorkflowTasks1 = new List<WorkflowTask>();
            this.WorkflowTaskRoles = new List<WorkflowTaskRole>();
            this.WorkflowTaskRoles1 = new List<WorkflowTaskRole>();
            //-- Added By  Srinivas Malepati, on 08 july 2014, to add new feature - VesselArrestImmobilizationSAMSAStop
            this.VesselSAMSAStopDocuments = new List<VesselSAMSAStopDocument>();
            this.VesselSAMSAStopDocuments1 = new List<VesselSAMSAStopDocument>();
            this.VesselArrestDocuments = new List<VesselArrestDocument>();
            this.VesselArrestDocuments1 = new List<VesselArrestDocument>();
            this.VesselArrestImmobilizationSAMSAs = new List<VesselArrestImmobilizationSAMSA>();
            this.VesselArrestImmobilizationSAMSAs1 = new List<VesselArrestImmobilizationSAMSA>();
            this.VesselCertificateDetails = new List<VesselCertificateDetail>();
            this.VesselCertificateDetails1 = new List<VesselCertificateDetail>();
            this.DivingRequests = new List<DivingRequest>();
            this.DivingRequests1 = new List<DivingRequest>();
            this.Locations = new List<Location>();
            this.Locations1 = new List<Location>();
            this.Shifts = new List<Shift>();
            this.Shifts1 = new List<Shift>();

            this.FuelReceipts = new List<FuelReceipt>();
            this.FuelReceipts1 = new List<FuelReceipt>();


            this.FuelRequisitions = new List<FuelRequisition>();
            this.FuelRequisitions1 = new List<FuelRequisition>();

            this.FuelRequisitionApprovals = new List<FuelRequisitionApproval>();
            this.FuelRequisitionApprovals1 = new List<FuelRequisitionApproval>();
            this.FuelRequisitionApprovals2 = new List<FuelRequisitionApproval>();


            this.ResourceEmployeeGroups = new List<ResourceEmployeeGroup>();
            this.ResourceEmployeeGroups1 = new List<ResourceEmployeeGroup>();
            this.ResourceGroups = new List<ResourceGroup>();
            this.ResourceGroups1 = new List<ResourceGroup>();
            this.ResourceRosters = new List<ResourceRoster>();
            this.ResourceRosters1 = new List<ResourceRoster>();
            this.Rosters = new List<Roster>();
            this.Rosters1 = new List<Roster>();
            this.RosterGroups = new List<RosterGroup>();
            this.RosterGroups1 = new List<RosterGroup>();


            // -- Added by sandeep 29-07-2014

            this.DivingCheckLists = new List<DivingCheckList>();
            this.DivingCheckLists1 = new List<DivingCheckList>();
            this.DivingCheckListHazards = new List<DivingCheckListHazard>();
            this.DivingCheckListHazards1 = new List<DivingCheckListHazard>();
            this.DivingOccupationApprovals = new List<DivingOccupationApproval>();
            this.DivingOccupationApprovals1 = new List<DivingOccupationApproval>();
            this.DivingOccupationApprovals2 = new List<DivingOccupationApproval>();
            this.DivingRequestDivers = new List<DivingRequestDiver>();
            this.DivingRequestDivers1 = new List<DivingRequestDiver>();
            this.ExternalDivingRegisters = new List<ExternalDivingRegister>();
            this.ExternalDivingRegisters1 = new List<ExternalDivingRegister>();
            this.Locations = new List<Location>();
            this.Locations1 = new List<Location>();
            this.ReportBuilders = new List<ReportBuilder>();
            this.ReportBuilders1 = new List<ReportBuilder>();
            this.ReportQueryTemplate = new List<ReportQueryTemplate>();
            this.ReportQueryTemplate1 = new List<ReportQueryTemplate>();

            // -- end

            // -- Added by sandeep on 20-8-2014

            this.DockingUndockingTimes = new List<DockingUndockingTime>();
            this.DockingUndockingTimes1 = new List<DockingUndockingTime>();
            this.SuppDockUnDockTimes = new List<SuppDockUnDockTime>();
            this.SuppDockUnDockTimes1 = new List<SuppDockUnDockTime>();
            this.SuppDryDocks = new List<SuppDryDock>();
            this.SuppDryDocks1 = new List<SuppDryDock>();
            this.SuppDryDockDocuments = new List<SuppDryDockDocument>();
            this.SuppDryDockDocuments1 = new List<SuppDryDockDocument>();
            this.SuppMiscServices = new List<SuppMiscService>();
            this.SuppMiscServices1 = new List<SuppMiscService>();
            this.SuppServiceResourceAllocs = new List<SuppServiceResourceAlloc>();
            this.SuppServiceResourceAllocs1 = new List<SuppServiceResourceAlloc>();

            // -- end

            // -- Added by sandeep on 21-8-2014
            this.SuppServiceRequests = new List<SuppServiceRequest>();
            this.SuppServiceRequests1 = new List<SuppServiceRequest>();
            this.SuppFloatingCranes = new List<SuppFloatingCrane>();
            this.SuppFloatingCranes1 = new List<SuppFloatingCrane>();
            this.SuppHotColdWorkPermits = new List<SuppHotColdWorkPermit>();
            this.SuppHotColdWorkPermits1 = new List<SuppHotColdWorkPermit>();
            this.SuppHotColdWorkPermitDocuments = new List<SuppHotColdWorkPermitDocument>();
            this.SuppHotColdWorkPermitDocuments1 = new List<SuppHotColdWorkPermitDocument>();

            // -- end
            //-- Added by Srini

            this.SuppHotWorkInspections = new List<SuppHotWorkInspection>();
            this.SuppHotWorkInspections1 = new List<SuppHotWorkInspection>();
            //---

            this.Section625ABCD = new List<Section625ABCD>();
            this.Section625ABCD1 = new List<Section625ABCD>();
            this.Section625B = new List<Section625B>();
            this.Section625B1 = new List<Section625B>();
            this.Section625C = new List<Section625C>();
            this.Section625C1 = new List<Section625C>();
            this.Section625D = new List<Section625D>();
            this.Section625D1 = new List<Section625D>();
            this.Section625E = new List<Section625E>();
            this.Section625E1 = new List<Section625E>();
            this.Section625G = new List<Section625G>();
            this.Section625G1 = new List<Section625G>();
            // change pwd 
            this.ChangePasswordLogs = new List<ChangePasswordLog>();
            this.ChangePasswordLogs1 = new List<ChangePasswordLog>();
            this.ChangePasswordLogs2 = new List<ChangePasswordLog>();
            //

            // -- Added by sandeep on 27-09-2014
            this.ServiceTypeDesignations = new List<ServiceTypeDesignation>();
            this.ServiceTypeDesignations1 = new List<ServiceTypeDesignation>();
            // -- end
            this.TerminalOperatorBerths = new List<TerminalOperatorBerth>();
            this.TerminalOperatorBerths1 = new List<TerminalOperatorBerth>();
            this.TerminalOperatorCargoHandlings = new List<TerminalOperatorCargoHandling>();
            this.TerminalOperatorCargoHandlings1 = new List<TerminalOperatorCargoHandling>();


            this.EventSchedules = new List<EventSchedule>();
            this.EventSchedules1 = new List<EventSchedule>();
            this.EventScheduleTasks = new List<EventScheduleTask>();
            this.EventScheduleTasks1 = new List<EventScheduleTask>();

            this.PortGeneralConfigs = new List<PortGeneralConfig>();
            this.PortGeneralConfigs1 = new List<PortGeneralConfig>();

            //-- added by shankar on 07-Nov-14
            this.PortContents = new List<PortContent>();
            this.PortContents1 = new List<PortContent>();

            this.PortContentRoles = new List<PortContentRole>();
            this.PortContentRoles1 = new List<PortContentRole>();
            //-- end

            this.TerminalOperatorPorts1 = new List<TerminalOperatorPort>();
            this.TerminalOperatorPorts2 = new List<TerminalOperatorPort>();

            //Added By Santosh B on 17-Dec-2014
            this.FinancialYears = new List<FinancialYear>();
            this.FinancialYears1 = new List<FinancialYear>();
            this.BudgetedValues = new List<BudgetedValues>();
            this.BudgetedValues1 = new List<BudgetedValues>();
            //

            //// -- Added by sandeep on 29-12-2014
            this.DredgingOperations = new List<DredgingOperation>();
            this.DredgingOperations1 = new List<DredgingOperation>();
            //// -- end
            this.StatementCommodities = new List<StatementCommodity>();
            this.StatementCommodities1 = new List<StatementCommodity>();
            this.AutomatedSlotBlockings = new List<AutomatedSlotBlocking>();
            this.AutomatedSlotBlockings1 = new List<AutomatedSlotBlocking>();
            this.Marpols = new List<Marpol>();
            this.Marpols1 = new List<Marpol>();
            this.WasteDeclarations = new List<WasteDeclaration>();
            this.WasteDeclarations1 = new List<WasteDeclaration>();
            this.SlotOverRidingReasons = new List<SlotOverRidingReasons>();
            this.SlotOverRidingReasons1 = new List<SlotOverRidingReasons>();
            this.ContractorPermitApplicationDetails = new List<ContractorPermitApplicationDetails>();
            this.ContractorPermitApplicationDetails1 = new List<ContractorPermitApplicationDetails>();

           
            
        }

        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public int UserTypeID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public string PWD { get; set; }
        [DataMember]
        public string IsFirstTimeLogin { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PwdExpirtyDate { get; set; }
        [DataMember]
        public int IncorrectLogins { get; set; }
        [DataMember]
        public string DormantStatus { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocationConfigRule> ResourceAllocationConfigRules { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocationConfigRule> ResourceAllocationConfigRules1 { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LoginTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public  ICollection<Address> Addresses { get; set; }
        [DataMember]
        public  ICollection<Address> Addresses1 { get; set; }
        [DataMember]
        public  ICollection<AuditTrail> AuditTrails { get; set; }
        [DataMember]
        public  ICollection<Agent> Agents { get; set; }
        [DataMember]
        public  ICollection<Agent> Agents1 { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts1 { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts2 { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts3 { get; set; }
        [DataMember]
        public  ICollection<AuditTrailConfig> AuditTrailConfigs { get; set; }
        [DataMember]
        public  ICollection<ArrivalApproval> ArrivalApprovals { get; set; }
        [DataMember]
        public  ICollection<ArrivalApproval> ArrivalApprovals1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalApproval> ArrivalApprovals2 { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalDocument> ArrivalDocuments { get; set; }
        [DataMember]
        public  ICollection<ArrivalDocument> ArrivalDocuments1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalIMDGTanker> ArrivalIMDGTankers { get; set; }
        [DataMember]
        public  ICollection<ArrivalIMDGTanker> ArrivalIMDGTankers1 { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications1 { get; set; }
        [DataMember]
        public  ICollection<AuthorizedContactPerson> AuthorizedContactPersons { get; set; }
        [DataMember]
        public  ICollection<AuthorizedContactPerson> AuthorizedContactPersons1 { get; set; }
        [DataMember]
        public  ICollection<AutomatedSlotConfiguration> AutomatedSlotConfigurations { get; set; }
        [DataMember]
        public  ICollection<AutomatedSlotConfiguration> AutomatedSlotConfigurations1 { get; set; }
        [DataMember]
        public  ICollection<Berth> Berths { get; set; }
        [DataMember]
        public  ICollection<Berth> Berths1 { get; set; }
        [DataMember]
        public  ICollection<BerthCargo> BerthCargoes { get; set; }
        [DataMember]
        public  ICollection<BerthCargo> BerthCargoes1 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances1 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceApproval> BerthMaintenanceApprovals { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceApproval> BerthMaintenanceApprovals1 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceApproval> BerthMaintenanceApprovals2 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals1 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals2 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompletion> BerthMaintenanceCompletions { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompletion> BerthMaintenanceCompletions1 { get; set; }

        [DataMember]
        public  ICollection<BerthOccupationDocument> BerthOccupationDocuments { get; set; }
        [DataMember]
        public  ICollection<BerthOccupationDocument> BerthOccupationDocuments1 { get; set; }
        [DataMember]
        public  ICollection<BerthPlanningConfigurations> BerthPlanningConfigurations { get; set; }
        [DataMember]
        public  ICollection<BerthPlanningConfigurations> BerthPlanningConfigurations1 { get; set; }
        [DataMember]
        public  ICollection<BerthReasonForVisit> BerthReasonForVisits { get; set; }
        [DataMember]
        public  ICollection<BerthReasonForVisit> BerthReasonForVisits1 { get; set; }
        [DataMember]
        public  ICollection<BerthVesselType> BerthVesselTypes { get; set; }
        [DataMember]
        public  ICollection<BerthVesselType> BerthVesselTypes1 { get; set; }
        [DataMember]
        public  ICollection<BerthingTaskExecution> BerthingTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<BerthingTaskExecution> BerthingTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<Bollard> Bollards { get; set; }
        [DataMember]
        public  ICollection<Bollard> Bollards1 { get; set; }
        [DataMember]
        public  ICollection<Code> Codes { get; set; }
        [DataMember]
        public  ICollection<Code> Codes1 { get; set; }
        [DataMember]
        public  ICollection<CodeDefinition> CodeDefinitions { get; set; }
        [DataMember]
        public  ICollection<CodeDefinition> CodeDefinitions1 { get; set; }
        [DataMember]
        public  ICollection<CodeDtl> CodeDtls { get; set; }
        [DataMember]
        public  ICollection<CodeDtl> CodeDtls1 { get; set; }
        [DataMember]
        public  ICollection<Bunkering> Bunkerings { get; set; }
        [DataMember]
        public  ICollection<Bunkering> Bunkerings1 { get; set; }
        [DataMember]
        public  ICollection<CargoManifest> CargoManifest { get; set; }
        [DataMember]
        public  ICollection<CargoManifest> CargoManifest1 { get; set; }
        [DataMember]
        public  ICollection<CargoManifestDtl> CargoManifestDtl { get; set; }
        [DataMember]
        public  ICollection<CargoManifestDtl> CargoManifestDtl1 { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts { get; set; }
        [DataMember]
        public  ICollection<Craft> Crafts1 { get; set; }
        [DataMember]
        public  ICollection<CraftOutOfCommission> CraftOutOfCommissions { get; set; }
        [DataMember]
        public  ICollection<CraftOutOfCommission> CraftOutOfCommissions1 { get; set; }

        [DataMember]
        public  ICollection<Conversation> Conversations { get; set; }
        [DataMember]
        public  ICollection<Conversation> Conversations1 { get; set; }
        [DataMember]
        public  ICollection<Conversation> Conversations2 { get; set; }
        [DataMember]
        public  ICollection<Conversation> Conversations3 { get; set; }
        [DataMember]
        public  ICollection<ConversationReply> ConversationReplies { get; set; }
        [DataMember]
        public  ICollection<ConversationReply> ConversationReplies1 { get; set; }
        [DataMember]
        public  ICollection<ConversationReply> ConversationReplies2 { get; set; }     

        [DataMember]
        public  ICollection<IMDGInformation> IMDGInformations { get; set; }

        [DataMember]
        public  ICollection<IMDGInformation> IMDGInformations1 { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs { get; set; }
        [DataMember]
        public  ICollection<CraftReminderConfig> CraftReminderConfigs1 { get; set; }
        [DataMember]
        public  ICollection<Department> Departments { get; set; }
        [DataMember]
        public  ICollection<Department> Departments1 { get; set; }
        [DataMember]
        public  ICollection<DeploymentBudget> DeploymentBudgets { get; set; }
        [DataMember]
        public  ICollection<DeploymentBudget> DeploymentBudgets1 { get; set; }
        [DataMember]
        public  ICollection<DeploymentPlan> DeploymentPlans { get; set; }
        [DataMember]
        public  ICollection<DeploymentPlan> DeploymentPlans1 { get; set; }
        [DataMember]
        public  ICollection<Diving> Divings { get; set; }
        [DataMember]
        public  ICollection<Diving> Divings1 { get; set; }
        [DataMember]
        public  ICollection<Draft> Drafts { get; set; }
        [DataMember]
        public  ICollection<Draft> Drafts1 { get; set; } 
        [DataMember]
        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals { get; set; }
        [DataMember]
        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals1 { get; set; }
        [DataMember]
        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals2 { get; set; }
        [DataMember]
        public  ICollection<DockingPlan> DockingPlans { get; set; }
        [DataMember]
        public  ICollection<DockingPlan> DockingPlans1 { get; set; }
        [DataMember]
        public  ICollection<DredgingPriority> DredgingPriorities { get; set; }
        [DataMember]
        public  ICollection<DredgingPriority> DredgingPriorities1 { get; set; }

        [DataMember]
        public  ICollection<DredgingPriorityDocument> DredgingPriorityDocuments { get; set; }
        [DataMember]
        public  ICollection<DredgingPriorityDocument> DredgingPriorityDocuments1 { get; set; }

        [DataMember]
        public  ICollection<Employee> Employees { get; set; }
        [DataMember]
        public  ICollection<Employee> Employees1 { get; set; }
        [DataMember]
        public  ICollection<Entity> Entities { get; set; }
        [DataMember]
        public  ICollection<Entity> Entities1 { get; set; }
        [DataMember]
        public  ICollection<FireEquipment> FireEquipments { get; set; }
        [DataMember]
        public  ICollection<FireEquipment> FireEquipments1 { get; set; }
        [DataMember]
        public  ICollection<FireProtection> FireProtections { get; set; }
        [DataMember]
        public  ICollection<FireProtection> FireProtections1 { get; set; }
        [DataMember]
        public  ICollection<FloatingCrane> FloatingCranes { get; set; }
        [DataMember]
        public  ICollection<FloatingCrane> FloatingCranes1 { get; set; }
        [DataMember]
        public  ICollection<FloatingCraneTaskExecution> FloatingCraneTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<FloatingCraneTaskExecution> FloatingCraneTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<FuelConsumptionDailyLog> FuelConsumptionDailyLogs { get; set; }
        [DataMember]
        public  ICollection<FuelConsumptionDailyLog> FuelConsumptionDailyLogs1 { get; set; }
        [DataMember]
        public  ICollection<Hour24Report625> Hour24Report625 { get; set; }
        [DataMember]
        public  ICollection<Hour24Report625> Hour24Report6251 { get; set; }
        [DataMember]
        public  ICollection<Incident> Incidents { get; set; }
        [DataMember]
        public  ICollection<Incident> Incidents1 { get; set; }
        [DataMember]
        public  ICollection<IncidentNature> IncidentNatures { get; set; }
        [DataMember]
        public  ICollection<IncidentNature> IncidentNatures1 { get; set; }
        [DataMember]
        public  ICollection<LicenseRequest> LicenseRequests { get; set; }
        [DataMember]
        public  ICollection<LicenseRequest> LicenseRequests1 { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts1 { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts2 { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts3 { get; set; }

        [DataMember]
        public  ICollection<LicenseRequestDocument> LicenseRequestDocuments { get; set; }

        [DataMember]
        public  ICollection<LicenseRequestDocument> LicenseRequestDocuments1 { get; set; }

        [DataMember]
        public  ICollection<Module> Modules { get; set; }
        [DataMember]
        public  ICollection<Module> Modules1 { get; set; }
        [DataMember]
        public  ICollection<MovementResourceAllocation> MovementResourceAllocations { get; set; }
        [DataMember]
        public  ICollection<MovementResourceAllocation> MovementResourceAllocations1 { get; set; }  
        [DataMember]
        public  ICollection<News> News { get; set; }
        [DataMember]
        public  ICollection<News> News1 { get; set; }
        [DataMember]
        public ICollection<NewsPort> NewsPort { get; set; }
        [DataMember]
        public ICollection<NewsPort> NewsPort1 { get; set; }        
        [DataMember]
        public  ICollection<Notification> Notifications { get; set; }
        [DataMember]
        public  ICollection<Notification> Notifications1 { get; set; }
        [DataMember]
        public  ICollection<NotificationPort> NotificationPorts { get; set; }
        [DataMember]
        public  ICollection<NotificationPort> NotificationPorts1 { get; set; }
        [DataMember]
        public  ICollection<NotificationRole> NotificationRoles { get; set; }
        [DataMember]
        public  ICollection<NotificationRole> NotificationRoles1 { get; set; }
        [DataMember]
        public  ICollection<NotificationTemplate> NotificationTemplates { get; set; }
        [DataMember]
        public  ICollection<OtherServiceRecording> OtherServiceRecordings { get; set; }
        [DataMember]
        public  ICollection<OtherServiceRecording> OtherServiceRecordings1 { get; set; }
        [DataMember]
        public  ICollection<PilotageServiceRecording> PilotageServiceRecordings { get; set; }
        [DataMember]
        public  ICollection<PilotageServiceRecording> PilotageServiceRecordings1 { get; set; }
        [DataMember]
        public  ICollection<PestControl> PestControls { get; set; }
        [DataMember]
        public  ICollection<PestControl> PestControls1 { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots1 { get; set; }
        [DataMember]
        public  ICollection<PilotCertificate> PilotCertificates { get; set; }
        [DataMember]
        public  ICollection<PilotCertificate> PilotCertificates1 { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests1 { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequestDocument> PilotExemptionRequestDocuments { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequestDocument> PilotExemptionRequestDocuments1 { get; set; }
        [DataMember]
        public  ICollection<PilotageTaskExecution> PilotageTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<PilotageTaskExecution> PilotageTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<PilotBoatTaskExecution> PilotBoatTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<PilotBoatTaskExecution> PilotBoatTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<PollutionControl> PollutionControls { get; set; }
        [DataMember]
        public  ICollection<PollutionControl> PollutionControls1 { get; set; }

        [DataMember]
        public  ICollection<PermitRequest> PermitRequests { get; set; }
        [DataMember]
        public  ICollection<PermitRequest> PermitRequests1 { get; set; }
        [DataMember]
        public  ICollection<PermitRequestContractor> PermitRequestContractors { get; set; }
        [DataMember]
        public  ICollection<PermitRequestContractor> PermitRequestContractors1 { get; set; }
        [DataMember]
        public  ICollection<PermitRequestDocument> PermitRequestDocuments { get; set; }
        [DataMember]
        public  ICollection<PermitRequestDocument> PermitRequestDocuments1 { get; set; }
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits { get; set; }
        [DataMember]
        public  ICollection<PersonalPermit> PersonalPermits1 { get; set; }
        [DataMember]
        public  ICollection<VisitorPermit> VisitorPermits { get; set; }
        [DataMember]
        public  ICollection<VisitorPermit> VisitorPermits1 { get; set; }
        [DataMember]
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits { get; set; }
        [DataMember]
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits1 { get; set; }
        [DataMember]
        public  ICollection<VehiclePermit> VehiclePermits { get; set; }
        [DataMember]
        public  ICollection<VehiclePermit> VehiclePermits1 { get; set; }
        [DataMember]
        public  ICollection<Port> Ports { get; set; }
        [DataMember]
        public  ICollection<Port> Ports1 { get; set; }
        [DataMember]
        public  ICollection<Quay> Quays { get; set; }
        [DataMember]
        public  ICollection<Quay> Quays1 { get; set; }
        [DataMember]
        public  ICollection<Roster> Rosters { get; set; }
        [DataMember]
        public  ICollection<Roster> Rosters1 { get; set; }
        [DataMember]
        public  ICollection<RosterGroup> RosterGroups { get; set; }
        [DataMember]
        public  ICollection<RosterGroup> RosterGroups1 { get; set; }
        [DataMember]
        public  ICollection<RosterDtl> RosterDtls { get; set; }
        [DataMember]
        public  ICollection<RosterDtl> RosterDtls1 { get; set; }   
        [DataMember]
        public  ICollection<ResourceAttendance> ResourceAttendances { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendance> ResourceAttendances1 { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendanceDtl> ResourceAttendanceDtls { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendanceDtl> ResourceAttendanceDtls1 { get; set; }
        [DataMember]
        public  ICollection<RevenueStopList> RevenueStopLists { get; set; }
        [DataMember]
        public  ICollection<RevenueStopList> RevenueStopLists1 { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations1 { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocation> ResourceAllocations2 { get; set; }
        [DataMember]
        public  ICollection<Role> Roles { get; set; }
        [DataMember]
        public  ICollection<Role> Roles1 { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings1 { get; set; }
        [DataMember]
        public  ICollection<Section625ABCD> Section625ABCD { get; set; }
        [DataMember]
        public  ICollection<Section625ABCD> Section625ABCD1 { get; set; }
        [DataMember]
        public  ICollection<Section625B> Section625B { get; set; }
        [DataMember]
        public  ICollection<Section625B> Section625B1 { get; set; }
        [DataMember]
        public  ICollection<Section625C> Section625C { get; set; }
        [DataMember]
        public  ICollection<Section625C> Section625C1 { get; set; }
        [DataMember]
        public  ICollection<Section625D> Section625D { get; set; }
        [DataMember]
        public  ICollection<Section625D> Section625D1 { get; set; }
        [DataMember]
        public  ICollection<Section625E> Section625E { get; set; }
        [DataMember]
        public  ICollection<Section625E> Section625E1 { get; set; }
        [DataMember]
        public  ICollection<Section625G> Section625G { get; set; }
        [DataMember]
        public  ICollection<Section625G> Section625G1 { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<ServiceType> ServiceTypes { get; set; }
        [DataMember]
        public  ICollection<ServiceType> ServiceTypes1 { get; set; }
        [DataMember]
        public  ICollection<Stevedore> Stevedores { get; set; }
        [DataMember]
        public  ICollection<Stevedore> Stevedores1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestApproval> ServiceRequestApprovals { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestApproval> ServiceRequestApprovals1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestApproval> ServiceRequestApprovals2 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestDocument> ServiceRequestDocuments { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestDocument> ServiceRequestDocuments1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestSailing> ServiceRequestSailings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestSailing> ServiceRequestSailings1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings1 { get; set; }
        [DataMember]
        public  ICollection<StatementFact> StatementFacts { get; set; }
        [DataMember]
        public  ICollection<StatementFact> StatementFacts1 { get; set; }
        [DataMember]
        public  ICollection<StatementFactBunker> StatementFactBunkers { get; set; }
        [DataMember]
        public  ICollection<StatementFactBunker> StatementFactBunkers1 { get; set; }
        [DataMember]
        public  ICollection<StatementFactEvent> StatementFactEvents { get; set; }
        [DataMember]
        public  ICollection<StatementFactEvent> StatementFactEvents1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  ICollection<SystemNotification> SystemNotifications { get; set; }
        [DataMember]
        public  ICollection<TerminalOperator> TerminalOperators { get; set; }
        [DataMember]
        public  ICollection<TerminalOperator> TerminalOperators1 { get; set; }
        [DataMember]
        public  ICollection<TugWorkboatTaskExecution> TugWorkboatTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<TugWorkboatTaskExecution> TugWorkboatTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts1 { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts2 { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts3 { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts4 { get; set; }
        [DataMember]
        public  ICollection<UserRole> UserRoles { get; set; }
        [DataMember]
        public  ICollection<User> Users1 { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<User> Users11 { get; set; }
        [DataMember]
        public  User User2 { get; set; }
        [DataMember]
        public  UserPreference UserPreference { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels1 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges1 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals1 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals2 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals3 { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeDocument> VesselAgentChangeDocuments { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChangeDocument> VesselAgentChangeDocuments1 { get; set; }
        [DataMember]
        public  ICollection<VesselApproval> VesselApprovals { get; set; }
        [DataMember]
        public  ICollection<VesselApproval> VesselApprovals1 { get; set; }
        [DataMember]
        public  ICollection<VesselApproval> VesselApprovals2 { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls1 { get; set; }
        [DataMember]
        public  ICollection<VesselCallAnchorage> VesselCallAnchorages { get; set; }
        [DataMember]
        public  ICollection<VesselCallAnchorage> VesselCallAnchorages1 { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements1 { get; set; }
        [DataMember]
        public  ICollection<VesselEngine> VesselEngines { get; set; }
        [DataMember]
        public  ICollection<VesselEngine> VesselEngines1 { get; set; }
        [DataMember]
        public  ICollection<VesselETAChange> VesselETAChanges { get; set; }
        [DataMember]
        public  ICollection<VesselETAChange> VesselETAChanges1 { get; set; }
        [DataMember]
        public  ICollection<VesselGear> VesselGears { get; set; }
        [DataMember]
        public  ICollection<VesselGear> VesselGears1 { get; set; }
        [DataMember]
        public  ICollection<VesselGrab> VesselGrabs { get; set; }
        [DataMember]
        public  ICollection<VesselGrab> VesselGrabs1 { get; set; }
        [DataMember]
        public  ICollection<VesselHatchHold> VesselHatchHolds { get; set; }
        [DataMember]
        public  ICollection<VesselHatchHold> VesselHatchHolds1 { get; set; }
        [DataMember]
        public  ICollection<WaterServiceTaskExecution> WaterServiceTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<WaterServiceTaskExecution> WaterServiceTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowInstance> WorkflowInstances { get; set; }
        [DataMember]
        public  ICollection<WorkflowInstance> WorkflowInstances1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess { get; set; }
        [DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowTask> WorkflowTasks { get; set; }
        [DataMember]
        public  ICollection<WorkflowTask> WorkflowTasks1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowTaskRole> WorkflowTaskRoles { get; set; }
        [DataMember]
        public  ICollection<WorkflowTaskRole> WorkflowTaskRoles1 { get; set; }
        //-- Added By  Srinivas Malepati, on 08 july 2014, to add new feature - VesselArrestImmobilizationSAMSAStop
        [DataMember]
        public  ICollection<VesselSAMSAStopDocument> VesselSAMSAStopDocuments { get; set; }
        [DataMember]
        public  ICollection<VesselSAMSAStopDocument> VesselSAMSAStopDocuments1 { get; set; }
        [DataMember]
        public  ICollection<VesselArrestDocument> VesselArrestDocuments { get; set; }
        [DataMember]
        public  ICollection<VesselArrestDocument> VesselArrestDocuments1 { get; set; }
        [DataMember]
        public  ICollection<VesselArrestImmobilizationSAMSA> VesselArrestImmobilizationSAMSAs { get; set; }
        [DataMember]
        public  ICollection<VesselArrestImmobilizationSAMSA> VesselArrestImmobilizationSAMSAs1 { get; set; }
        [DataMember]
        public  ICollection<VesselCertificateDetail> VesselCertificateDetails { get; set; }
        [DataMember]
        public  ICollection<VesselCertificateDetail> VesselCertificateDetails1 { get; set; }
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests { get; set; }
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests1 { get; set; }
        [DataMember]
        public  ICollection<Location> Locations { get; set; }
        [DataMember]
        public  ICollection<Location> Locations1 { get; set; }
        [DataMember]
        public  ICollection<Shift> Shifts { get; set; }
        [DataMember]
        public  ICollection<Shift> Shifts1 { get; set; }
        [DataMember]
        public  ICollection<ResourceRoster> ResourceRosters { get; set; }
        [DataMember]
        public  ICollection<ResourceRoster> ResourceRosters1 { get; set; }
        [DataMember]
        public  ICollection<ResourceEmployeeGroup> ResourceEmployeeGroups { get; set; }
        [DataMember]
        public  ICollection<ResourceEmployeeGroup> ResourceEmployeeGroups1 { get; set; }
        [DataMember]
        public  ICollection<ResourceGroup> ResourceGroups { get; set; }
        [DataMember]
        public  ICollection<ResourceGroup> ResourceGroups1 { get; set; }

        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts { get; set; }
        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts1 { get; set; }


        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions { get; set; }
        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions1 { get; set; }


        [DataMember]
        public  ICollection<FuelRequisitionApproval> FuelRequisitionApprovals { get; set; }
        [DataMember]
        public  ICollection<FuelRequisitionApproval> FuelRequisitionApprovals1 { get; set; }
        [DataMember]
        public  ICollection<FuelRequisitionApproval> FuelRequisitionApprovals2 { get; set; }


        //-- Added By  Srinivas Malepati, on 23 july 2014, to add new feature
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }

        [DataMember]
        public string AnonymousUserYn { get; set; }      

        // Added by Sandeep on 29-07-2014
        [DataMember]
        public  ICollection<DivingCheckList> DivingCheckLists { get; set; }
        [DataMember]
        public  ICollection<DivingCheckList> DivingCheckLists1 { get; set; }
        [DataMember]
        public  ICollection<DivingCheckListHazard> DivingCheckListHazards { get; set; }
        [DataMember]
        public  ICollection<DivingCheckListHazard> DivingCheckListHazards1 { get; set; }
        [DataMember]
        public  ICollection<DivingRequestDiver> DivingRequestDivers { get; set; }
        [DataMember]
        public  ICollection<DivingRequestDiver> DivingRequestDivers1 { get; set; }

        [DataMember]
        public  ICollection<ExternalDivingRegister> ExternalDivingRegisters { get; set; }
        [DataMember]
        public  ICollection<ExternalDivingRegister> ExternalDivingRegisters1 { get; set; }
        // -- end
        [DataMember]
        public  ICollection<ReportBuilder> ReportBuilders { get; set; }
        [DataMember]
        public  ICollection<ReportBuilder> ReportBuilders1 { get; set; }
        [DataMember]
        public  ICollection<ReportQueryTemplate> ReportQueryTemplate { get; set; }
        [DataMember]
        public  ICollection<ReportQueryTemplate> ReportQueryTemplate1 { get; set; }

        // -- Added by sandeep on 20-8-2014

        [DataMember]
        public  ICollection<DockingUndockingTime> DockingUndockingTimes { get; set; }
        [DataMember]
        public  ICollection<DockingUndockingTime> DockingUndockingTimes1 { get; set; }
        [DataMember]
        public  ICollection<SuppDockUnDockTime> SuppDockUnDockTimes { get; set; }
        [DataMember]
        public  ICollection<SuppDockUnDockTime> SuppDockUnDockTimes1 { get; set; }
        [DataMember]
        public  ICollection<SuppDryDock> SuppDryDocks { get; set; }
        [DataMember]
        public  ICollection<SuppDryDock> SuppDryDocks1 { get; set; }
        [DataMember]
        public  ICollection<SuppDryDockDocument> SuppDryDockDocuments { get; set; }
        [DataMember]
        public  ICollection<SuppDryDockDocument> SuppDryDockDocuments1 { get; set; }
        [DataMember]
        public  ICollection<SuppMiscService> SuppMiscServices { get; set; }
        [DataMember]
        public  ICollection<SuppMiscService> SuppMiscServices1 { get; set; }
        [DataMember]
        public  ICollection<SuppServiceResourceAlloc> SuppServiceResourceAllocs { get; set; }
        [DataMember]
        public  ICollection<SuppServiceResourceAlloc> SuppServiceResourceAllocs1 { get; set; }

        // -- end

        // -- Added by sandeep on 21-8-2014
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests1 { get; set; }
        [DataMember]
        public  ICollection<SuppFloatingCrane> SuppFloatingCranes { get; set; }
        [DataMember]
        public  ICollection<SuppFloatingCrane> SuppFloatingCranes1 { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermit> SuppHotColdWorkPermits { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermit> SuppHotColdWorkPermits1 { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermitDocument> SuppHotColdWorkPermitDocuments { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermitDocument> SuppHotColdWorkPermitDocuments1 { get; set; }
        // -- end
        //-- Added by Srini

        [DataMember]
        public  ICollection<SuppHotWorkInspection> SuppHotWorkInspections { get; set; }
        [DataMember]
        public  ICollection<SuppHotWorkInspection> SuppHotWorkInspections1 { get; set; }
        //---


        //
        [DataMember]
        public  ICollection<ChangePasswordLog> ChangePasswordLogs { get; set; }
        [DataMember]
        public  ICollection<ChangePasswordLog> ChangePasswordLogs1 { get; set; }
        [DataMember]
        public  ICollection<ChangePasswordLog> ChangePasswordLogs2 { get; set; }

        //

        // -- Added by sandeep on 27-09-2014
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations { get; set; }
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations1 { get; set; }
        // -- end

        public  ICollection<TerminalOperatorBerth> TerminalOperatorBerths { get; set; }
        public  ICollection<TerminalOperatorBerth> TerminalOperatorBerths1 { get; set; }
        public  ICollection<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings { get; set; }
        public  ICollection<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings1 { get; set; }


        [DataMember]
        public  ICollection<EventSchedule> EventSchedules { get; set; }
        [DataMember]
        public  ICollection<EventSchedule> EventSchedules1 { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTask> EventScheduleTasks { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTask> EventScheduleTasks1 { get; set; }

        public  ICollection<PortGeneralConfig> PortGeneralConfigs { get; set; }
        public  ICollection<PortGeneralConfig> PortGeneralConfigs1 { get; set; }

        // -- Added by shankar on 07-Nov-14
        public  ICollection<PortContent> PortContents { get; set; }
        public  ICollection<PortContent> PortContents1 { get; set; }
        public  ICollection<PortContentRole> PortContentRoles { get; set; }
        public  ICollection<PortContentRole> PortContentRoles1 { get; set; }
        // -- end

        public  ICollection<TerminalOperatorPort> TerminalOperatorPorts1 { get; set; }
        public  ICollection<TerminalOperatorPort> TerminalOperatorPorts2 { get; set; }
        public  ICollection<PortRegistry> PortRegistries { get; set; }
        public  ICollection<PortRegistry> PortRegistries1 { get; set; }

        //Added By Santosh B on 17-Dec-2014
        public  ICollection<FinancialYear> FinancialYears { get; set; }
        public  ICollection<FinancialYear> FinancialYears1 { get; set; }
        public  ICollection<BudgetedValues> BudgetedValues { get; set; }
        public  ICollection<BudgetedValues> BudgetedValues1 { get; set; }
        //


        //Added By Omprakash k on 22nd Dec 2014
        [DataMember]
        public  ICollection<SuppDryDockExtension> SuppDryDockExtensions { get; set; }
        [DataMember]
        public  ICollection<SuppDryDockExtension> SuppDryDockExtension1 { get; set; }

        // -- Added by sandeep on 29-12-2014
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations1 { get; set; }
        // -- end
        // -- Added by Amala on 7-1-2015
        [DataMember]
        public  ICollection<RevenuePosting> RevenuePostings { get; set; }
        [DataMember]
        public  ICollection<RevenuePosting> RevenuePostings1 { get; set; }
        [DataMember]
        public  ICollection<AgentAccount> AgentAccounts { get; set; }
        [DataMember]
        public  ICollection<AgentAccount> AgentAccounts1 { get; set; }
        // -- end
        [DataMember]
        public  ICollection<ArrivalReason> ArrivalReasons { get; set; }
        [DataMember]
        public  ICollection<ArrivalReason> ArrivalReasons1 { get; set; }

        [DataMember]
        public string  ReasonForAccess { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ValidFromDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ValidToDate { get; set; }
        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities { get; set; }
        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities1 { get; set; }
        [DataMember]
        public ICollection<AutomatedSlotBlocking> AutomatedSlotBlockings { get; set; }
        [DataMember]
        public ICollection<AutomatedSlotBlocking> AutomatedSlotBlockings1 { get; set; }
        [DataMember]
        public ICollection<Marpol> Marpols { get; set; }
        [DataMember]
        public ICollection<Marpol> Marpols1 { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations1 { get; set; }
        [DataMember]
        public ICollection<SlotOverRidingReasons> SlotOverRidingReasons { get; set; }
        [DataMember]
        public ICollection<SlotOverRidingReasons> SlotOverRidingReasons1 { get; set; }
        [DataMember]
        public ICollection<ContractorPermitApplicationDetails> ContractorPermitApplicationDetails { get; set; }

        [DataMember]
        public ICollection<ContractorPermitApplicationDetails> ContractorPermitApplicationDetails1 { get; set; }

        
         
       
            
    }
}
