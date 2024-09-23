using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IPMS.Data.Mapping;
using IPMS.Domain.Models;
using Core.Repository.Providers.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using IPMS.Domain.DTOS;
using IPMS.Data;


namespace IPMS.Data.Context
{

    public partial class TnpaContext : DataContext
    {
        static TnpaContext()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

            Database.SetInitializer<TnpaContext>(null);
        }

        public TnpaContext()
            : base("Name=TnpaContext")
        {

            Database.SetInitializer<TnpaContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<TnpaContext, TnpaContextMigrationConfiguration>());
        }


        //Generic set.
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<AuditTrailConfig> AuditTrailConfigs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentDocument> AgentDocuments { get; set; }
        public DbSet<AgentPort> AgentPorts { get; set; }
        public DbSet<ArrivalApproval> ArrivalApprovals { get; set; }
        public DbSet<ArrivalCommodity> ArrivalCommodities { get; set; }
        public DbSet<ArrivalDocument> ArrivalDocuments { get; set; }
        public DbSet<ArrivalIMDGTanker> ArrivalIMDGTankers { get; set; }
        public DbSet<ArrivalReason> ArrivalReasons { get; set; }
        public DbSet<ArrivalAgent> ArrivalAgents { get; set; }
        public DbSet<ArrivalNotification> ArrivalNotifications { get; set; }
        public DbSet<IMDGInformation> IMDGInformations { get; set; }
        public DbSet<AuthorizedContactPerson> AuthorizedContactPersons { get; set; }
        public DbSet<AutomatedSlotConfiguration> AutomatedSlotConfigurations { get; set; }
        public DbSet<Berth> Berths { get; set; }
        public DbSet<BerthCargo> BerthCargoes { get; set; }
        public DbSet<BerthMaintenance> BerthMaintenances { get; set; }
        public DbSet<BerthMaintenanceApproval> BerthMaintenanceApprovals { get; set; }
        public DbSet<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals { get; set; }
        public DbSet<BerthMaintenanceCompletion> BerthMaintenanceCompletions { get; set; }

        public DbSet<BerthOccupationDocument> BerthOccupationDocuments { get; set; }
        public DbSet<BerthPlanningConfigurations> BerthPlanningConfigurations { get; set; }
        public DbSet<BerthReasonForVisit> BerthReasonForVisits { get; set; }
        public DbSet<BerthVesselType> BerthVesselTypes { get; set; }
        public DbSet<BerthingTaskExecution> BerthingTaskExecutions { get; set; }
        public DbSet<Bollard> Bollards { get; set; }
        public DbSet<CargoManifest> CargoManifests { get; set; }
        public DbSet<CargoManifestDtl> CargoManifestDtls { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<CodeDefinition> CodeDefinitions { get; set; }
        public DbSet<CodeDtl> CodeDtls { get; set; }
        public DbSet<Bunkering> Bunkerings { get; set; }
        public DbSet<Craft> Crafts { get; set; }
        public DbSet<CraftOutOfCommission> CraftOutOfCommissions { get; set; }
        public DbSet<CraftReminderConfig> CraftReminderConfigs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeploymentBudget> DeploymentBudgets { get; set; }
        public DbSet<DeploymentPlan> DeploymentPlans { get; set; }
        public DbSet<Diving> Divings { get; set; }
        public DbSet<DivingRequest> DivingRequests { get; set; }
        public DbSet<DivingCheckList> DivingCheckLists { get; set; }
        public DbSet<DivingCheckListHazard> DivingCheckListHazards { get; set; } // -- sandeep added     
        public DbSet<DivingOccupationApproval> DivingOccupationApprovals { get; set; }
        public DbSet<DockingPlan> DockingPlans { get; set; }
        public DbSet<DockingPlanDocument> DockingPlanDocuments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DredgingPriority> DredgingPriorities { get; set; }

        public DbSet<DredgingPriorityDocument> DredgingPriorityDocuments { get; set; }


        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityPrivilege> EntityPrivileges { get; set; }
        public DbSet<FireEquipment> FireEquipments { get; set; }
        public DbSet<FireProtection> FireProtections { get; set; }
        public DbSet<FloatingCrane> FloatingCranes { get; set; }
        public DbSet<FloatingCraneTaskExecution> FloatingCraneTaskExecutions { get; set; }
        public DbSet<FuelRequisition> FuelRequisitions { get; set; }
        public DbSet<FuelRequisitionApproval> FuelRequisitionApprovals { get; set; }
        public DbSet<FuelReceipt> FuelReceipts { get; set; }

        public DbSet<FuelConsumptionDailyLog> FuelConsumptionDailyLogs { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentDocument> IncidentDocuments { get; set; }
        public DbSet<IncidentNature> IncidentNatures { get; set; }        
        public DbSet<LicenseRequest> LicenseRequests { get; set; }
        public DbSet<LicenseRequestPort> LicenseRequestPorts { get; set; }
        public DbSet<LicenseRequestDocument> LicenseRequestDocuments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<MovementResourceAllocation> MovementResourceAllocations { get; set; }   
        public DbSet<News> News { get; set; }
        public DbSet<NewsPort> NewsPorts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationPort> NotificationPorts { get; set; }
        public DbSet<NotificationRole> NotificationRoles { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<OtherServiceRecording> OtherServiceRecordings { get; set; }
        public DbSet<PestControl> PestControls { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<PilotCertificate> PilotCertificates { get; set; }
        public DbSet<PilotExemptionRequest> PilotExemptionRequests { get; set; }
        public DbSet<PilotExemptionRequestDocument> PilotExemptionRequestDocuments { get; set; }
        public DbSet<PilotageTaskExecution> PilotageTaskExecutions { get; set; }
        public DbSet<PilotBoatTaskExecution> PilotBoatTaskExecutions { get; set; }
        public DbSet<PilotageServiceRecording> PilotageServiceRecordings { get; set; }        
        public DbSet<Port> Ports { get; set; }
        public DbSet<PortRegistry> PortRegistries { get; set; }
        public DbSet<PortConfiguration> PortConfigurations { get; set; }
        public DbSet<Quay> Quays { get; set; }        
        public DbSet<ResourceEmployeeGroup> ResourceEmployeeGroups { get; set; }
        public DbSet<ResourceGroup> ResourceGroups { get; set; }
        public DbSet<ResourceRoster> ResourceRosters { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<RosterGroup> RosterGroups { get; set; }
        public DbSet<RosterDtl> RosterDtls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ResourceAllocation> ResourceAllocations { get; set; }
        public DbSet<ResourceAttendance> ResourceAttendances { get; set; }
        public DbSet<ResourceAttendanceDtl> ResourceAttendanceDtls { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<RevenueAccountStatus> RevenueAccountStatus { get; set; }
        public DbSet<RevenueStopList> RevenueStopLists { get; set; }
        public DbSet<SAPPosting> SAPPostings { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestApproval> ServiceRequestApprovals { get; set; }
        public DbSet<ServiceRequestDocument> ServiceRequestDocuments { get; set; }
        public DbSet<ServiceRequestSailing> ServiceRequestSailings { get; set; }
        public DbSet<ServiceRequestShifting> ServiceRequestShiftings { get; set; }
        public DbSet<ServiceRequestWarping> ServiceRequestWarpings { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }
        public DbSet<SlotPriorityConfiguration> SlotPriorityConfigurations { get; set; }
        public DbSet<StatementFact> StatementFacts { get; set; }
        public DbSet<StatementFactBunker> StatementFactBunkers { get; set; }
        public DbSet<StatementFactEvent> StatementFactEvents { get; set; }
        public DbSet<StatementCommodity> StatementCommodities { get; set; }
        public DbSet<Stevedore> Stevedores { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SuperCategory> SuperCategories { get; set; }
        public DbSet<SystemNotification> SystemNotifications { get; set; }
        public DbSet<TerminalOperator> TerminalOperators { get; set; }
        public DbSet<TerminalOperatorBerth> TerminalOperatorBerths { get; set; }
        public DbSet<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings { get; set; }
        public DbSet<TugWorkboatTaskExecution> TugWorkboatTaskExecutions { get; set; }
        public DbSet<UserPort> UserPorts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<VesselAgentChange> VesselAgentChanges { get; set; }
        public DbSet<VesselAgentChangeApproval> VesselAgentChangeApprovals { get; set; }
        public DbSet<VesselAgentChangeDocument> VesselAgentChangeDocuments { get; set; }
        public DbSet<VesselApproval> VesselApprovals { get; set; }
        public DbSet<VesselCall> VesselCalls { get; set; }
        public DbSet<VesselCallAnchorage> VesselCallAnchorages { get; set; }
        public DbSet<VesselCallMovement> VesselCallMovements { get; set; }
        public DbSet<VesselEngine> VesselEngines { get; set; }
        public DbSet<VesselETAChange> VesselETAChanges { get; set; }
        public DbSet<VesselGear> VesselGears { get; set; }
        public DbSet<VesselGrab> VesselGrabs { get; set; }
        public DbSet<VesselHatchHold> VesselHatchHolds { get; set; }
        public DbSet<WaterServiceTaskExecution> WaterServiceTaskExecutions { get; set; }
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; }
        public DbSet<WorkflowProcess> WorkflowProcess { get; set; }
        public DbSet<WorkflowTask> WorkflowTasks { get; set; }
        public DbSet<WorkflowTaskRole> WorkflowTaskRoles { get; set; }
        //-- Added by Srini Malepati, on 8th july 2014
        public DbSet<VesselArrestDocument> VesselArrestDocuments { get; set; }
        public DbSet<VesselArrestImmobilizationSAMSA> VesselArrestImmobilizationSAMSAStops { get; set; }
        public DbSet<VesselSAMSAStopDocument> VesselSAMSAStopDocuments { get; set; }
        public DbSet<VesselCertificateDetail> VesselCertificateDetail { get; set; }
        public DbSet<ResourceAllocationConfigRule> ResourceAllocationConfigRules { get; set; }
        public DbSet<ResourceAllocationMovementTypeRule> ResourceAllocationMovementTypeRules { get; set; }


        public DbSet<ResourceGangConfig> ResourceGangConfigs { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }       

        // Added by sandeep on 04-08-2014

        public DbSet<Location> Locations { get; set; }
        public DbSet<ExternalDivingRegister> ExternalDivingRegisters { get; set; }

        // -- Added by sandeep on 08-08-2014

        public DbSet<DivingRequestDiver> DivingRequestDivers { get; set; }

        // -- end
        //----------------------------------------------------------------------------------
        public DbSet<PermitRequest> PermitRequests { get; set; }
        public DbSet<PermitRequestArea> PermitRequestAreas { get; set; }
        public DbSet<PermitRequestContractor> PermitRequestContractors { get; set; }
        public DbSet<PermitRequestDocument> PermitRequestDocuments { get; set; }
        public DbSet<PersonalPermit> PersonalPermits { get; set; }
        public DbSet<VehiclePermit> VehiclePermits { get; set; }
        public DbSet<VisitorPermit> VisitorPermits { get; set; }
        public DbSet<WharfVehiclePermit> WharfVehiclePermits { get; set; }
        public DbSet<PermitRequestVerifyedDetail> PermitRequestVerifyedDetails { get; set; }
        public DbSet<PermitRequestVerifyedDocument> PermitRequestVerifyedDocuments { get; set; }
        public DbSet<VehiclePermitRequirementCode> VehiclePermitRequirementCodes { get; set; }
        public DbSet<PermitRequestAccessGates> PermitRequestAccessGates { get; set; }
        public DbSet<IndividualPermitApplicationDetails> IndividualPermitApplicationDetails { get; set; }
        public DbSet<IndividualVehiclePermit> IndividualVehiclePermits { get; set; }
        public DbSet<IndividualPersonalPermit> IndividualPersonalPermits { get; set; }
        public DbSet<PermitReason> PermitReasons { get; set; }
        public DbSet<PermitRequestSubArea> PermitRequestSubArea { get; set; }
        public DbSet<ContractorPermitApplicationDetails> ContractorPermitApplicationDetails { get; set; }
        public DbSet<ContractorPermitEmployeeDetails> ContractorPermitEmployeeDetails { get; set; }
    
        //--------------------------------------------------------------------------------
        public DbSet<Hour24Report625> Hour24Report625 { get; set; }
        public DbSet<Section625ABCD> Section625ABCD { get; set; }
        public DbSet<Section625B> Section625B { get; set; }
        public DbSet<Section625BUnion> Section625BUnion { get; set; }
        public DbSet<Section625C> Section625C { get; set; }
        public DbSet<Section625CDetail> Section625CDetail { get; set; }
        public DbSet<Section625CPrevent> Section625CPrevent { get; set; }
        public DbSet<Section625CRecommended> Section625CRecommended { get; set; }
        public DbSet<Section625D> Section625D { get; set; }
        public DbSet<Section625DDetail> Section625DDetail { get; set; }
        public DbSet<Section625E> Section625E { get; set; }
        public DbSet<Section625EDetail> Section625EDetail { get; set; }
        public DbSet<Section625G> Section625G { get; set; }
        public DbSet<Section625GDetail1> Section625GDetail1 { get; set; }
        public DbSet<Section625GDetail2> Section625GDetail2 { get; set; }


        public DbSet<ReportBuilder> ReportBuilders { get; set; }
        public DbSet<ReportQueryOperator> ReportQueryOperators { get; set; }
        public DbSet<ReportQueryTemplate> ReportQueryTemplate { get; set; }
        public DbSet<ReportQueryDataTypeOperator> ReportQueryDataTypeOperators { get; set; }
        public DbSet<ReportQueryLookup> ReportQueryLookup { get; set; }
        // -- Added by sandeep on 21-08-2014

        public DbSet<SuppServiceRequest> SuppServiceRequests { get; set; }
        public DbSet<SuppFloatingCrane> SuppFloatingCranes { get; set; }
        public DbSet<SuppHotColdWorkPermit> SuppHotColdWorkPermits { get; set; }
        public DbSet<SuppHotColdWorkPermitDocument> SuppHotColdWorkPermitDocuments { get; set; }

        // -- end

        // -- Added by Srini on 27-8-2014

        public DbSet<SuppHotWorkInspection> SuppHotWorkInspections { get; set; }
        public DbSet<SuppDockUnDockTime> SuppDockUnDockTimes { get; set; }
        public DbSet<SuppDryDock> SuppDryDocks { get; set; }
        // -- end


        //
        public DbSet<ChangePasswordLog> ChangePasswordLogs { get; set; }
        //

        // -- Added by sandeep on 27-09-2014
        public DbSet<ServiceTypeDesignation> ServiceTypeDesignations { get; set; }
        // -- end


        public DbSet<EventSchedule> EventSchedules { get; set; }
        public DbSet<EventScheduleTask> EventScheduleTasks { get; set; }
        public DbSet<EventScheduleTrack> EventScheduleTracks { get; set; }
        public DbSet<PortGeneralConfig> PortGeneralConfigs { get; set; }

        // -- Added by sandeep on 10-11-2014
        public DbSet<SuppDryDockDocument> SuppDryDockDocuments { get; set; }
        // -- end

        public DbSet<SuppMiscService> SuppMiscServices { get; set; }

        public DbSet<PortContent> PortContents { get; set; }
        public DbSet<PortContentRole> PortContentRoles { get; set; }

        public DbSet<TerminalOperatorPort> TerminalOperatorPorts { get; set; }

        // -- Added by Santosh B on 10-11-2014
        public DbSet<DepartureNotice> DepartureNotices { get; set; }

        //-- Added by Santosh B on 17-11-2014
        public DbSet<BudgetedValues> BudgetedValues { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<SuppDryDockExtension> SuppDryDockExtensions { get; set; }

        // -- Added by sandeep on 30-12-2014
        public DbSet<DredgingOperation> DredgingOperations { get; set; }
        // -- end

        // -- Added by Amala on 7-1-2015
        public DbSet<MaterialCodeMaster> MaterialCodeMasters { get; set; }
        public DbSet<MaterialCodePort> MaterialCodePorts { get; set; }
        public DbSet<RevenuePosting> RevenuePostings { get; set; }
        public DbSet<RevenuePostingDtl> RevenuePostingDtls { get; set; }
        public DbSet<AgentAccount> AgentAccounts { get; set; }
        // -- end
        //for SAP: invoice response posting to invoice table.
        public DbSet<SAPInvoiceItem> SAPInvoiceItems { get; set; }

        public DbSet<AutomatedSlotBlocking> AutomatedSlotBlockings { get; set; }
        public DbSet<Marpol> Marpols { get; set; }
        public DbSet<WasteDeclaration> WasteDeclarations { get; set; }
        public DbSet<SlotOverRidingReasons> SlotOverRidingReasons { get; set; }

        //anusha 28-09-2024
        public DbSet<ResetPasswordLogs> ResetPasswordLogss { get; set; }

        public DbSet<CommonAllData> CommonAllDatas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                modelBuilder.Configurations.Add(new ArrivalAgentMap());
                modelBuilder.Configurations.Add(new AuditTrailMap());
                modelBuilder.Configurations.Add(new AuditTrailConfigMap());
                modelBuilder.Configurations.Add(new AddressMap());
                modelBuilder.Configurations.Add(new AgentMap());
                modelBuilder.Configurations.Add(new AgentDocumentMap());
                modelBuilder.Configurations.Add(new AgentPortMap());
                modelBuilder.Configurations.Add(new ArrivalApprovalMap());
                modelBuilder.Configurations.Add(new ArrivalCommodityMap());
                modelBuilder.Configurations.Add(new ArrivalDocumentMap());
                modelBuilder.Configurations.Add(new ArrivalIMDGTankerMap());
                modelBuilder.Configurations.Add(new IMDGInformationMap());
                modelBuilder.Configurations.Add(new ArrivalNotificationMap());
                modelBuilder.Configurations.Add(new AuthorizedContactPersonMap());
                modelBuilder.Configurations.Add(new AutomatedSlotConfigurationMap());
                modelBuilder.Configurations.Add(new BerthMap());
                modelBuilder.Configurations.Add(new BerthCargoMap());
                modelBuilder.Configurations.Add(new BerthMaintenanceMap());
                modelBuilder.Configurations.Add(new BerthMaintenanceApprovalMap());
                modelBuilder.Configurations.Add(new BerthMaintenanceCompApprovalMap());
                modelBuilder.Configurations.Add(new BerthMaintenanceCompletionMap());

                modelBuilder.Configurations.Add(new BerthOccupationDocumentMap());
                modelBuilder.Configurations.Add(new BerthPlanningConfigurationsMap());
                modelBuilder.Configurations.Add(new BerthReasonForVisitMap());
                modelBuilder.Configurations.Add(new BerthVesselTypeMap());
                modelBuilder.Configurations.Add(new BerthingTaskExecutionMap());
                modelBuilder.Configurations.Add(new BollardMap());
                modelBuilder.Configurations.Add(new CargoManifestMap());
                modelBuilder.Configurations.Add(new CargoManifestDtlMap());
                modelBuilder.Configurations.Add(new CodeMap());
                modelBuilder.Configurations.Add(new CodeDefinitionMap());
                modelBuilder.Configurations.Add(new CodeDtlMap());
                modelBuilder.Configurations.Add(new BunkeringMap());
                modelBuilder.Configurations.Add(new CraftMap());
                modelBuilder.Configurations.Add(new ConversationMap());
                modelBuilder.Configurations.Add(new ConversationReplyMap());
                modelBuilder.Configurations.Add(new CraftOutOfCommissionMap());
                modelBuilder.Configurations.Add(new CraftReminderConfigMap());
                modelBuilder.Configurations.Add(new DepartmentMap());
                modelBuilder.Configurations.Add(new DeploymentBudgetMap());
                modelBuilder.Configurations.Add(new DeploymentPlanMap());
                modelBuilder.Configurations.Add(new DivingMap());
                modelBuilder.Configurations.Add(new DivingCheckListMap());                
                modelBuilder.Configurations.Add(new IncidentNatureMap());
                modelBuilder.Configurations.Add(new DivingOccupationApprovalMap());
                modelBuilder.Configurations.Add(new DockingPlanMap());
                modelBuilder.Configurations.Add(new DockingPlanDocumentMap());
                modelBuilder.Configurations.Add(new DocumentMap());
                modelBuilder.Configurations.Add(new DredgingPriorityMap());

                modelBuilder.Configurations.Add(new DredgingPriorityDocumentMap());

                modelBuilder.Configurations.Add(new DraftMap());
                modelBuilder.Configurations.Add(new EmployeeMap());
                modelBuilder.Configurations.Add(new EntityMap());
                modelBuilder.Configurations.Add(new EntityPrivilegeMap());
                modelBuilder.Configurations.Add(new FireEquipmentMap());
                modelBuilder.Configurations.Add(new FireProtectionMap());
                modelBuilder.Configurations.Add(new FloatingCraneMap());
                modelBuilder.Configurations.Add(new FloatingCraneTaskExecutionMap());
                modelBuilder.Configurations.Add(new FuelConsumptionDailyLogMap());
                modelBuilder.Configurations.Add(new FuelRequisitionMap());
                modelBuilder.Configurations.Add(new FuelRequisitionApprovalMap());
                modelBuilder.Configurations.Add(new FuelReceiptMap());

                modelBuilder.Configurations.Add(new Hour24Report625Map());
                modelBuilder.Configurations.Add(new Section625ABCDMap());
                modelBuilder.Configurations.Add(new Section625BMap());
                modelBuilder.Configurations.Add(new Section625BUnionMap());
                modelBuilder.Configurations.Add(new Section625CMap());
                modelBuilder.Configurations.Add(new Section625CDetailMap());
                modelBuilder.Configurations.Add(new Section625CPreventMap());
                modelBuilder.Configurations.Add(new Section625CRecommendedMap());
                modelBuilder.Configurations.Add(new Section625DMap());
                modelBuilder.Configurations.Add(new Section625DDetailMap());
                modelBuilder.Configurations.Add(new Section625EMap());
                modelBuilder.Configurations.Add(new Section625EDetailMap());
                modelBuilder.Configurations.Add(new Section625GMap());
                modelBuilder.Configurations.Add(new Section625GDetail1Map());
                modelBuilder.Configurations.Add(new Section625GDetail2Map());


                modelBuilder.Configurations.Add(new IncidentMap());
                modelBuilder.Configurations.Add(new IncidentDocumentMap());                
                modelBuilder.Configurations.Add(new LicenseRequestMap());
                modelBuilder.Configurations.Add(new LicenseRequestPortMap());
                modelBuilder.Configurations.Add(new LicenseRequestDocumentMap());
                modelBuilder.Configurations.Add(new ModuleMap());
                modelBuilder.Configurations.Add(new MovementResourceAllocationMap());                
                modelBuilder.Configurations.Add(new NewsMap());
                modelBuilder.Configurations.Add(new NewsPortMap());
                modelBuilder.Configurations.Add(new NotificationMap());
                modelBuilder.Configurations.Add(new NotificationPortMap());
                modelBuilder.Configurations.Add(new NotificationRoleMap());
                modelBuilder.Configurations.Add(new NotificationTemplateMap());
                modelBuilder.Configurations.Add(new OtherServiceRecordingMap());
                modelBuilder.Configurations.Add(new PestControlMap());
                modelBuilder.Configurations.Add(new PilotMap());
                modelBuilder.Configurations.Add(new PilotCertificateMap());
                modelBuilder.Configurations.Add(new PilotExemptionRequestMap());
                modelBuilder.Configurations.Add(new PilotExemptionRequestDocumentMap());
                modelBuilder.Configurations.Add(new PilotageTaskExecutionMap());
                modelBuilder.Configurations.Add(new PilotBoatTaskExecutionMap());
                modelBuilder.Configurations.Add(new PollutionControlMap());
                modelBuilder.Configurations.Add(new PortMap());
                modelBuilder.Configurations.Add(new PortRegistryMap());
                modelBuilder.Configurations.Add(new PilotageServiceRecordingMap());
                modelBuilder.Configurations.Add(new PortConfigurationMap());
                modelBuilder.Configurations.Add(new QuayMap());
                modelBuilder.Configurations.Add(new RevenueAccountStatusMap());
                modelBuilder.Configurations.Add(new RevenueStopListMap());
                modelBuilder.Configurations.Add(new SAPPostingMap());              
                modelBuilder.Configurations.Add(new ResourceAllocationConfigRuleMap());
                modelBuilder.Configurations.Add(new ResourceAllocationMovementTypeRuleMap());
                modelBuilder.Configurations.Add(new ResourceGangConfigMap());
                modelBuilder.Configurations.Add(new ResourceAttendanceMap());
                modelBuilder.Configurations.Add(new ResourceAttendanceDtlMap());
                modelBuilder.Configurations.Add(new RoleMap());
                modelBuilder.Configurations.Add(new RolePrivilegeMap());
                modelBuilder.Configurations.Add(new ServiceRequestMap());
                modelBuilder.Configurations.Add(new ServiceRequestApprovalMap());
                modelBuilder.Configurations.Add(new ServiceRequestDocumentMap());
                modelBuilder.Configurations.Add(new ServiceRequestSailingMap());
                modelBuilder.Configurations.Add(new ServiceRequestShiftingMap());
                modelBuilder.Configurations.Add(new ServiceRequestWarpingMap());
                modelBuilder.Configurations.Add(new SlotPriorityConfigurationMap());                
                modelBuilder.Configurations.Add(new ResourceAllocationMap());
                modelBuilder.Configurations.Add(new RosterMap());
                modelBuilder.Configurations.Add(new RosterGroupMap());
                modelBuilder.Configurations.Add(new RosterDtlMap());
                modelBuilder.Configurations.Add(new ShiftingBerthingTaskExecutionMap());
                modelBuilder.Configurations.Add(new StatementFactMap());
                modelBuilder.Configurations.Add(new StatementFactBunkerMap());
                modelBuilder.Configurations.Add(new StatementFactEventMap());
                modelBuilder.Configurations.Add(new StatementCommodityMap());
                modelBuilder.Configurations.Add(new StevedoreMap());
                modelBuilder.Configurations.Add(new SubCategoryMap());
                modelBuilder.Configurations.Add(new SuperCategoryMap());
                modelBuilder.Configurations.Add(new ServiceTypeMap());
                modelBuilder.Configurations.Add(new SystemNotificationMap());
                modelBuilder.Configurations.Add(new TerminalOperatorMap());
                modelBuilder.Configurations.Add(new TerminalOperatorBerthMap());
                modelBuilder.Configurations.Add(new TerminalOperatorCargoHandlingMap());
                modelBuilder.Configurations.Add(new TugWorkboatTaskExecutionMap());
                modelBuilder.Configurations.Add(new UserPortMap());
                modelBuilder.Configurations.Add(new UserRoleMap());
                modelBuilder.Configurations.Add(new UserMap());
                modelBuilder.Configurations.Add(new UserPreferenceMap());
                modelBuilder.Configurations.Add(new VesselMap());
                modelBuilder.Configurations.Add(new VesselAgentChangeMap());
                modelBuilder.Configurations.Add(new VesselAgentChangeApprovalMap());
                modelBuilder.Configurations.Add(new VesselAgentChangeDocumentMap());
                modelBuilder.Configurations.Add(new VesselApprovalMap());
                modelBuilder.Configurations.Add(new VesselCallMap());
                modelBuilder.Configurations.Add(new VesselCallAnchorageMap());
                modelBuilder.Configurations.Add(new VesselCallMovementMap());
                modelBuilder.Configurations.Add(new VesselEngineMap());
                modelBuilder.Configurations.Add(new VesselETAChangeMap());
                modelBuilder.Configurations.Add(new VesselGearMap());
                modelBuilder.Configurations.Add(new VesselGrabMap());
                modelBuilder.Configurations.Add(new VesselHatchHoldMap());
                modelBuilder.Configurations.Add(new WaterServiceTaskExecutionMap());
                modelBuilder.Configurations.Add(new WorkflowInstanceMap());
                modelBuilder.Configurations.Add(new WorkflowProcessMap());
                modelBuilder.Configurations.Add(new WorkflowTaskMap());
                modelBuilder.Configurations.Add(new WorkflowTaskRoleMap());
                //-- Added by Srini Malepati, on 8th july 2014
                modelBuilder.Configurations.Add(new VesselArrestDocumentMap());
                modelBuilder.Configurations.Add(new VesselArrestImmobilizationSAMSAMap());
                modelBuilder.Configurations.Add(new VesselSAMSAStopDocumentMap());
                modelBuilder.Configurations.Add(new VesselCertificateDetailMap());
                //added by avinash
                modelBuilder.Configurations.Add(new DivingRequestMap());
                modelBuilder.Configurations.Add(new DivingCheckListHazardMap()); // -- sandeep added

                // -- Added by sandeep on 04-08-2014

                modelBuilder.Configurations.Add(new LocationMap());
                modelBuilder.Configurations.Add(new ExternalDivingRegisterMap());

                modelBuilder.Configurations.Add(new ShiftMap());               

                // -- Added by sandeep on 08-08-2014

                modelBuilder.Configurations.Add(new DivingRequestDiverMap());

                // -- end
                //-- Added by Suresh P, on 6th AUG 2014
                modelBuilder.Configurations.Add(new ResourceGroupMap());
                modelBuilder.Configurations.Add(new ResourceEmployeeGroupMap());
                modelBuilder.Configurations.Add(new ResourceRostersMap());

                modelBuilder.Configurations.Add(new ReportBuilderMap());
                modelBuilder.Configurations.Add(new ReportQueryOperatorMap());
                modelBuilder.Configurations.Add(new ReportQueryTemplateMap());
                modelBuilder.Configurations.Add(new ReportQueryDataTypeOperatorMap());
                modelBuilder.Configurations.Add(new ReportQueryLookupMap());

                // -- Added by sandeep on 21-08-2014
                modelBuilder.Configurations.Add(new SuppServiceRequestMap());
                modelBuilder.Configurations.Add(new SuppFloatingCraneMap());
                modelBuilder.Configurations.Add(new SuppHotColdWorkPermitMap());
                modelBuilder.Configurations.Add(new SuppHotColdWorkPermitDocumentMap());

                // -- end
                // -- Added by Srini

                modelBuilder.Configurations.Add(new SuppHotWorkInspectionMap());
                modelBuilder.Configurations.Add(new SuppDryDockMap());
                modelBuilder.Configurations.Add(new SuppDockUnDockTimeMap());
                // -- End

                //
                modelBuilder.Configurations.Add(new ChangePasswordLogMap());
                //

                // -- Added by sandeep on 27-09-2014
                modelBuilder.Configurations.Add(new ServiceTypeDesignationMap());
                // -- end

                modelBuilder.Configurations.Add(new EventScheduleMap());
                modelBuilder.Configurations.Add(new EventScheduleTaskMap());
                modelBuilder.Configurations.Add(new EventScheduleTrackMap());
                modelBuilder.Configurations.Add(new PortGeneralConfigMap());
                modelBuilder.Configurations.Add(new PermitRequestVerifyedDetailMap());
                modelBuilder.Configurations.Add(new PermitRequestVerifyedDocumentMap());
                modelBuilder.Configurations.Add(new PermitRequestMap());
                modelBuilder.Configurations.Add(new PermitRequestAreaMap());
                modelBuilder.Configurations.Add(new PermitRequestContractorMap());
                modelBuilder.Configurations.Add(new PermitRequestDocumentMap());
                modelBuilder.Configurations.Add(new PersonalPermitMap());
                modelBuilder.Configurations.Add(new VehiclePermitMap());
                modelBuilder.Configurations.Add(new VisitorPermitMap());
                modelBuilder.Configurations.Add(new WharfVehiclePermitMap());
                modelBuilder.Configurations.Add(new PermitRequestAccessGateMap());
                modelBuilder.Configurations.Add(new VehiclePermitRequirementCodeMap());
                modelBuilder.Configurations.Add(new IndividualPermitApplicationDetailsMap());
                modelBuilder.Configurations.Add(new IndividualVehiclePermitMap());
                modelBuilder.Configurations.Add(new IndividualPersonalPermitMap());
                modelBuilder.Configurations.Add(new PermitReasonMap());
                modelBuilder.Configurations.Add(new PermitRequestSubAreaMap());
                modelBuilder.Configurations.Add(new ContractorPermitApplicationDetailsMap());
                modelBuilder.Configurations.Add(new ContractorPermitEmployeeDetailsMap());


                ///////

                // -- Added by sandeep on 10-11-2014
                modelBuilder.Configurations.Add(new SuppDryDockDocumentMap());
                // -- end
                // -- Added by shankar on 07-nov-14
                modelBuilder.Configurations.Add(new PortContentMap());
                modelBuilder.Configurations.Add(new PortContentRoleMap());
                // -- end
                modelBuilder.Configurations.Add(new SuppMiscServiceMap());

                modelBuilder.Configurations.Add(new TerminalOperatorPortMap());


                // -- Added by Santosh B on 10-11-2014
                modelBuilder.Configurations.Add(new DepartureNoticeMap());
                // -- end

                //-- Added by Santosh B on 17-11-2014
                modelBuilder.Configurations.Add(new BudgetedValuesMap());
                modelBuilder.Configurations.Add(new FinancialYearMap());

                modelBuilder.Configurations.Add(new SuppDryDockExtensionMap());

                // -- Added by sandeep on 30-12-2014
                modelBuilder.Configurations.Add(new DredgingOperationMap());
                // -- end

                // -- Added by Amala on 7-1-2015
                modelBuilder.Configurations.Add(new MaterialCodeMasterMap());
                modelBuilder.Configurations.Add(new MaterialCodePortMap());
                modelBuilder.Configurations.Add(new RevenuePostingMap());
                modelBuilder.Configurations.Add(new RevenuePostingDtlMap());
                modelBuilder.Configurations.Add(new AgentAccountMap());
                // end
                modelBuilder.Configurations.Add(new ArrivalReasonMap());

                modelBuilder.Configurations.Add(new SAPInvoiceItemMap());
                modelBuilder.Configurations.Add(new AutomatedSlotBlockingMap());
                modelBuilder.Configurations.Add(new MarpolMap());
                modelBuilder.Configurations.Add(new WasteDeclarationMap());
                modelBuilder.Configurations.Add(new SlotOverRidingReasonsMap());

                //anusha28-09-2024
                modelBuilder.Configurations.Add(new ResetPasswordLogsMap());
                modelBuilder.Configurations.Add(new CommonAllDataMap());
            }
        }
    }
}
