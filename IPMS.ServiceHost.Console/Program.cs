using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using System.Security.Principal;
using System.Timers;
using Core.Repository;
using System.ServiceModel;
using IPMS.Services;
using System.Globalization;




namespace IPMS.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {

            SM.ServiceHost hostPortService = new SM.ServiceHost(typeof(PortService));
            SM.ServiceHost hostBollardService = new SM.ServiceHost(typeof(BollardService));
            SM.ServiceHost hostQuayService = new SM.ServiceHost(typeof(QuayService));
            SM.ServiceHost hostAgentService = new SM.ServiceHost(typeof(AgentService));
            SM.ServiceHost hostAccountService = new SM.ServiceHost(typeof(AccountService));
            SM.ServiceHost hostAuditLogService = new SM.ServiceHost(typeof(AuditLogService));
            SM.ServiceHost hostBerthService = new SM.ServiceHost(typeof(BerthService));
            SM.ServiceHost hostEmailService = new SM.ServiceHost(typeof(EmailService));
            SM.ServiceHost hostUserService = new SM.ServiceHost(typeof(UserService));
            SM.ServiceHost hostTerminalOperatorService = new SM.ServiceHost(typeof(TerminalOperatorService));
            SM.ServiceHost hostElectronicNotificationsService = new SM.ServiceHost(typeof(ElectronicNotificationsService));
            SM.ServiceHost hostEmployeeService = new SM.ServiceHost(typeof(EmployeeService));
            SM.ServiceHost hostCommonService = new SM.ServiceHost(typeof(CommonService));
            SM.ServiceHost hostServiceRequest = new SM.ServiceHost(typeof(ServiceRequestService));
            SM.ServiceHost hostArrivalNotificationRequest = new SM.ServiceHost(typeof(ArrivalNotificationService));
            SM.ServiceHost hostSuperCategoryRequest = new SM.ServiceHost(typeof(SuperCategoryService));
            SM.ServiceHost hostVesselETAChangeService = new SM.ServiceHost(typeof(VesselETAChangeService));
            SM.ServiceHost hostSubCategoryRequest = new SM.ServiceHost(typeof(SubCategoryService));
            SM.ServiceHost hostNewsServiceRequest = new SM.ServiceHost(typeof(NewsService));
            SM.ServiceHost hostModuleRequest = new SM.ServiceHost(typeof(ModuleService));
            SM.ServiceHost hostCraftServiceRequest = new SM.ServiceHost(typeof(CraftService));
            SM.ServiceHost hostVesselAgentChangeRequest = new SM.ServiceHost(typeof(VesselAgentChangeService));
            SM.ServiceHost hostLicensingRequestService = new SM.ServiceHost(typeof(LicensingRequestService));
            SM.ServiceHost hostVesselCallAnchorageService = new SM.ServiceHost(typeof(VesselCallAnchorageService));
            SM.ServiceHost hostVesselArrestImmobilizationSAMSAStopService = new SM.ServiceHost(typeof(VesselArrestImmobilizationSAMSAStopService));
            SM.ServiceHost hostVesselRegistrationService = new SM.ServiceHost(typeof(VesselRegistrationService));
            SM.ServiceHost hostRolePrivilegeRequest = new SM.ServiceHost(typeof(RolePrivilegeService));
            SM.ServiceHost hostEntityRequest = new SM.ServiceHost(typeof(EntityService));
            SM.ServiceHost hostBerthPlanningService = new SM.ServiceHost(typeof(BerthPlanningService));
            SM.ServiceHost hostFileService = new SM.ServiceHost(typeof(FileService));
            SM.ServiceHost BerthMainCompService = new SM.ServiceHost(typeof(BerthMaintenanceCompletionService));
            SM.ServiceHost hostMobileService = new SM.ServiceHost(typeof(MobileService));
            SM.ServiceHost hostPilotExemptionRequestService = new SM.ServiceHost(typeof(PilotExemptionRequestService));
            SM.ServiceHost hostBerthMaintenanceRequest = new SM.ServiceHost(typeof(BerthMaintenanceService));
            SM.ServiceHost hostVoyageMonitoringService = new SM.ServiceHost(typeof(VoyageMonitoringService));
            SM.ServiceHost hostDivingRequestService = new SM.ServiceHost(typeof(DivingRequestService));
            SM.ServiceHost hostWorkFlowTaskService = new SM.ServiceHost(typeof(WorkFlowTaskService));
            SM.ServiceHost hostMobileIncidentReportService = new SM.ServiceHost(typeof(MobileIncidentReportService));
            SM.ServiceHost hostDashBoardService = new SM.ServiceHost(typeof(DashBoardService));
            SM.ServiceHost hostShiftService = new SM.ServiceHost(typeof(ShiftService));
            SM.ServiceHost hostMobileConversationService = new SM.ServiceHost(typeof(MobileConversationService));
            SM.ServiceHost hostResourceGroupService = new SM.ServiceHost(typeof(ResourceGroupService));
            SM.ServiceHost hostReportBuilderService = new SM.ServiceHost(typeof(ReportBuilderService));
            SM.ServiceHost hostSupplymentaryServiceRequestService = new SM.ServiceHost(typeof(SupplymentaryServiceRequestService));
            SM.ServiceHost hostBerthPreSchedulingService = new SM.ServiceHost(typeof(BerthPreSchedulingService));
            SM.ServiceHost hostAutomatedSlottingService = new SM.ServiceHost(typeof(AutomatedSlottingService));
            SM.ServiceHost hostResourceAttendanceService = new SM.ServiceHost(typeof(ResourceAttendanceService));
            SM.ServiceHost hostExternalDivingRegisterService = new SM.ServiceHost(typeof(ExternalDivingRegisterService));
            SM.ServiceHost hostRosterService = new SM.ServiceHost(typeof(RosterService));
            //SM.ServiceHost hostRosterGroupService = new SM.ServiceHost(typeof(RosterGroupService));
            SM.ServiceHost hostSuppHotWorkInspectionService = new SM.ServiceHost(typeof(SuppHotWorkInspectionService));
            SM.ServiceHost hostAutomatedResourceSchedulingService = new SM.ServiceHost(typeof(AutomatedResourceSchedulingService));
            //
            SM.ServiceHost hostSuppDockUnDockTimeService = new SM.ServiceHost(typeof(SuppDockUnDockTimeService));
            SM.ServiceHost hostFuelConsumptionDailyLogService = new SM.ServiceHost(typeof(FuelConsumptionDailyLogService));
            SM.ServiceHost hostStatementFactService = new SM.ServiceHost(typeof(StatementFactService));
            SM.ServiceHost hostCargoManifestService = new SM.ServiceHost(typeof(CargoManifestService));
            SM.ServiceHost hostAutomatedSlotConfigurationService = new SM.ServiceHost(typeof(AutomatedSlotConfigurationService));
            SM.ServiceHost hostSuppServiceResourceAllocService = new SM.ServiceHost(typeof(SuppServiceResourceAllocService));
            SM.ServiceHost hostResourceAllocationService = new SM.ServiceHost(typeof(ResourceAllocationService));
            SM.ServiceHost hostBerthPlanningConfigurationsService = new SM.ServiceHost(typeof(BerthPlanningConfigurationsService));
            SM.ServiceHost hostResourceAllocationConfigRuleService = new SM.ServiceHost(typeof(ResourceAllocationConfigRuleService));
            SM.ServiceHost hostMobileScheduledTasksService = new SM.ServiceHost(typeof(MobileScheduledTasksService));
            SM.ServiceHost hostFuelRequisitionRequest = new SM.ServiceHost(typeof(FuelRequisitionService));
            SM.ServiceHost hostCraftOutOfCommissionService = new SM.ServiceHost(typeof(CraftOutOfCommissionService));
            SM.ServiceHost hostFuelReceiptRequest = new SM.ServiceHost(typeof(FuelReceiptService));
            SM.ServiceHost hostHour24Report625Service = new SM.ServiceHost(typeof(Hour24Report625Service));
            SM.ServiceHost hostEventSchedulerService = new SM.ServiceHost(typeof(EventSchedulerService));
            SM.ServiceHost hostPortGeneralConfigsService = new SM.ServiceHost(typeof(PortGeneralConfigsService));
            SM.ServiceHost hostCraftReminderConfigService = new SM.ServiceHost(typeof(CraftReminderConfigService));
            SM.ServiceHost hostDeploymentPlanService = new SM.ServiceHost(typeof(DeploymentPlanService));
            SM.ServiceHost hostPortInformationService = new SM.ServiceHost(typeof(PortInformationService));
            SM.ServiceHost hostDockingPlanService = new SM.ServiceHost(typeof(DockingPlanService));

            SM.ServiceHost hostDredgingPriorityService = new SM.ServiceHost(typeof(DredgingPriorityService));
            SM.ServiceHost hostPortEntryPassApplicationService = new SM.ServiceHost(typeof(PortEntryPassApplicationService));
            SM.ServiceHost hostSuppDryDockService = new SM.ServiceHost(typeof(SuppDryDockService));

            SM.ServiceHost hostUserPreferenceService = new SM.ServiceHost(typeof(UserPreferenceService));
            SM.ServiceHost hostSuppMiscServiceRecordingService = new SM.ServiceHost(typeof(SuppMiscServiceRecordingService));
            SM.ServiceHost hostLocationService = new SM.ServiceHost(typeof(LocationService));
            SM.ServiceHost hostDepartureNoticeService = new SM.ServiceHost(typeof(DepartureNoticeService));
            SM.ServiceHost hostServiceTypeService = new SM.ServiceHost(typeof(ServiceTypeService));
            SM.ServiceHost hostServiceTypeDesignationService = new SM.ServiceHost(typeof(ServiceTypeDesignationService));
            SM.ServiceHost hostBudgetedValuesService = new SM.ServiceHost(typeof(BudgetedValuesService));
            SM.ServiceHost hostDryDockSchedulerService = new SM.ServiceHost(typeof(DryDockSchedulerService));

            SM.ServiceHost hostSuppDryDockExtensionService = new SM.ServiceHost(typeof(SuppDryDockExtensionService));
            SM.ServiceHost hostMarineRevenueService = new SM.ServiceHost(typeof(MarineRevenueService));

            SM.ServiceHost hostRevenueStopListService = new SM.ServiceHost(typeof(RevenueStopListService));
            SM.ServiceHost hostMaterialCodeMasterService = new SM.ServiceHost(typeof(MaterialCodeMasterService));
            SM.ServiceHost hostSAPPostingService = new SM.ServiceHost(typeof(SAPPostingService));
            SM.ServiceHost hostVesselSAPPostingService = new SM.ServiceHost(typeof(VesselSAPPostingService));
            SM.ServiceHost hostNotificationPublisherService = new SM.ServiceHost(typeof(NotificationPublisherService));
            SM.ServiceHost hostAutomatedSlotBlockingService = new SM.ServiceHost(typeof(AutomatedSlotBlockingService));
            SM.ServiceHost hostmarpolService = new SM.ServiceHost(typeof(MarpolService));
           

            StartService(hostPortEntryPassApplicationService, "PortEntryPassApplicationService");
            StartService(hostPortService, "PortService");
            StartService(hostBollardService, "BollardService");
            StartService(hostQuayService, "Quayservice");
            StartService(hostAccountService, "AccountService");
            StartService(hostAgentService, "AgentService");
            StartService(hostAuditLogService, "AuditLogService");
            StartService(hostBerthService, "BerthService");
            StartService(hostEmailService, "EmailService");
            StartService(hostUserService, "UserService");
            StartService(hostTerminalOperatorService, "TerminalOperatorService");
            StartService(hostElectronicNotificationsService, "ElectronicNotificationsService");
            StartService(hostEmployeeService, "EmployeeService");
            StartService(hostCommonService, "CommonService");
            StartService(hostServiceRequest, "ServiceRequest");
            StartService(hostArrivalNotificationRequest, "ArrivalNotificationRequestService");
            StartService(hostNewsServiceRequest, "NewsService");
            StartService(hostSuperCategoryRequest, "SuperCategoryService");
            StartService(hostSubCategoryRequest, "SubCategoryService");
            StartService(hostVesselETAChangeService, "VesselETAChangeService");
            StartService(hostModuleRequest, "Moduleservice");
            StartService(hostCraftServiceRequest, "CraftService");
            StartService(hostLicensingRequestService, "LicensingRequestService");
            StartService(hostVesselAgentChangeRequest, "VesselAgentChangeService");
            StartService(hostVesselCallAnchorageService, "VesselCallAnchorageService");
            StartService(hostVesselArrestImmobilizationSAMSAStopService, "VesselArrestImmobilizationSAMSAStopService");
            StartService(hostVesselRegistrationService, "VesselRegistrationService");
            StartService(hostRolePrivilegeRequest, "RolePrivilegeService");
            StartService(hostEntityRequest, "EntityService");
            StartService(hostBerthPlanningService, "BerthPlanningService");
            StartService(hostFileService, "FileService");
            StartService(hostPilotExemptionRequestService, "PilotExemptionRequestService");
            StartService(BerthMainCompService, "BerthMaintenanceCompletionService");
            StartService(hostMobileService, "MobileService");
            StartService(hostBerthMaintenanceRequest, "BerthMaintenanceService");
            StartService(hostVoyageMonitoringService, "VoyageMonitoringService");
            StartService(hostDivingRequestService, "DivingRequestService");
            StartService(hostMobileIncidentReportService, "MobileIncidentReportService");
            StartService(hostShiftService, "ShiftService");
            StartService(hostWorkFlowTaskService, "WorkFlowTaskService");
            StartService(hostDashBoardService, "WorkFlowTaskService");
            StartService(hostMobileConversationService, "MobileConversationService");
            StartService(hostResourceGroupService, "ResourceGroupService");
            StartService(hostReportBuilderService, "ReportBuilderService");
            StartService(hostSupplymentaryServiceRequestService, "SupplymentaryServiceRequestService");
            //
            StartService(hostExternalDivingRegisterService, "ExternalDivingRegisterService");
            StartService(hostBerthPreSchedulingService, "BerthPreSchedulingService");
            StartService(hostAutomatedSlottingService, "AutomatedSlottingService");
            StartService(hostResourceAttendanceService, "ResourceAttendanceService");
            StartService(hostAutomatedResourceSchedulingService, "AutomatedResourceSchedulingService");

            StartService(hostRosterService, "RosterService");
            //StartService(hostRosterGroupService, "RosterGroupService");
            StartService(hostSuppHotWorkInspectionService, "SuppHotWorkInspectionService");

            StartService(hostSuppDockUnDockTimeService, "SuppDockUnDockTimeService");
            StartService(hostStatementFactService, "StatementFactService");
            StartService(hostCargoManifestService, "CargoManifestService");
            StartService(hostFuelConsumptionDailyLogService, "FuelConsumptionDailyLogService");
            StartService(hostAutomatedSlotConfigurationService, "AutomatedSlotConfigurationService");
            StartService(hostSuppServiceResourceAllocService, "SuppServiceResourceAllocService");
            StartService(hostResourceAllocationService, "ResourceAllocationService");
            StartService(hostBerthPlanningConfigurationsService, "BerthPlanningConfigurationsService");
            StartService(hostFuelRequisitionRequest, "FuelRequisitionService");
            StartService(hostCraftOutOfCommissionService, "CraftOutOfCommissionService");
            StartService(hostFuelReceiptRequest, "FuelReceiptService");

            StartService(hostMobileScheduledTasksService, "MobileScheduledTasksService");
            StartService(hostHour24Report625Service, "Hour24Report625Service");
            StartService(hostEventSchedulerService, "EventSchedulerService");
            StartService(hostCraftReminderConfigService, "CraftReminderConfigService");
            StartService(hostPortGeneralConfigsService, "PortGeneralConfigsService");
            StartService(hostResourceAllocationConfigRuleService, "ResourceAllocationConfigRuleService");
            StartService(hostDeploymentPlanService, "DeploymentPlanService");
            StartService(hostSuppDryDockService, "SuppDryDockService");
            StartService(hostDockingPlanService, "DockingPlanService");
            StartService(hostSuppDryDockExtensionService, "SuppDryDockExtensionService");



            StartService(hostLocationService, "LocationService");
            StartService(hostDredgingPriorityService, "DredgingPriorityService");

            StartService(hostUserPreferenceService, "UserPreferenceService");
            StartService(hostSuppMiscServiceRecordingService, "SuppMiscServiceRecordingService");
            StartService(hostDepartureNoticeService, "DepartureNoticeService");
            StartService(hostServiceTypeService, "ServiceTypeService");
            StartService(hostServiceTypeDesignationService, "ServiceTypeDesignationService");
            StartService(hostPortInformationService, "PortInformationService");
            StartService(hostBudgetedValuesService, "BudgetedValuesService");
            StartService(hostDryDockSchedulerService, "DryDockSchedulerService");
            StartService(hostMarineRevenueService, "MarineRevenueService");
            StartService(hostRevenueStopListService, "RevenueStopListService");
            StartService(hostMaterialCodeMasterService, "MaterialCodeMasterService");
            StartService(hostSAPPostingService, "SAPPostingService");
            StartService(hostVesselSAPPostingService, "VesselSAPPostingService");
            StartService(hostNotificationPublisherService, "NotificationPublisherService");
            StartService(hostAutomatedSlotBlockingService, "AutomatedSlotBlockingService");
            StartService(hostmarpolService, "MarpolService");
            
            // StartService(hostTptDocumentUploadService, "TptDocumentUploadService");


            Console.WriteLine("PortEntryPassApplicationService is started");
            Console.WriteLine("Port Service is started");
            Console.WriteLine("Bollard Service is started");
            Console.WriteLine("Quay service is started");
            Console.WriteLine("Account Service is started");
            Console.WriteLine("Agent Service is started");
            Console.WriteLine("AuditLog Service is started");
            Console.WriteLine("Berth Service is started");
            Console.WriteLine("User Service is started");
            Console.WriteLine("TerminalOperator Service is started");
            Console.WriteLine("ElectronicNotifications Service is started");
            Console.WriteLine("Employee Service is started");
            Console.WriteLine("Common Service is started");
            Console.WriteLine("ServiceRequest Service is started");
            Console.WriteLine("ArrivalNotificationRequest is started");
            Console.WriteLine("NewsService is started");
            Console.WriteLine("SuperCategory Service is started");
            Console.WriteLine("VesselETAChangeService is started");
            Console.WriteLine("ModuleService is started");
            Console.WriteLine("SubCategoryRequestService is started");
            Console.WriteLine("VesselAgentChangeRequest is started");
            Console.WriteLine("Craftservice is started");
            Console.WriteLine("LicensingRequest Service is started");
            Console.WriteLine("VesselCallAnchorage Service is started");
            Console.WriteLine("VesselArrestImmobilizationSAMSAStop Service is started");
            Console.WriteLine("Role Privilege Service is started");
            Console.WriteLine("Entity Service is started");
            Console.WriteLine("Vessel Registration Service is started");
            Console.WriteLine("BerthPlanning Service is started");
            Console.WriteLine("PilotExemptionRequestService Service is started");
            Console.WriteLine("File Service is started");
            Console.WriteLine("Berth Maintenance Completion Service is started");
            Console.WriteLine("Mobile Service is started");
            Console.WriteLine("BerthMaintenance Service is started");
            Console.WriteLine("Voyage Monitoring Service  is started");
            Console.WriteLine("Diving Request Service is started");
            Console.WriteLine("WorkFlow Task Service is started");
            Console.WriteLine("MobileIncidentReport Service is started");
            Console.WriteLine("DashBoard Service is started");
            Console.WriteLine("Shift Service is started");
            Console.WriteLine("MobileConversation Service is started");
            Console.WriteLine("Resource Group Service  is started");
            Console.WriteLine("Report Builder Service  is started");
            Console.WriteLine("Supplymentary Service Request Service is started");
            Console.WriteLine("External DivingRegister Service is started");
            Console.WriteLine("Berth Pre-Scheduling Service  is started");
            Console.WriteLine("Automated Slotting Service  is started");
            Console.WriteLine("Resource Attendance Service  is started");
            Console.WriteLine("RosterService  is started");

            Console.WriteLine("Automated Slotting Service  is started");
            Console.WriteLine("Statement Fact Service  is started");
            Console.WriteLine("RosterGroupService  is started");
            Console.WriteLine("Supp Hot Work Inspection Service is started");

            Console.WriteLine("Supp Dock UnDock Time Service is started");
            Console.WriteLine("Cargo Manifest Service  is started");

            Console.WriteLine("Fuel Consumption Daily Log Service  is started.");
            Console.WriteLine("Automated Slot Configuration Service is started.");
            Console.WriteLine("Automated Resource Scheduling Service is started");

            Console.WriteLine("Fuel Requisition Service is started");

            Console.WriteLine("SuppServiceResourceAlloc Service is started");
            Console.WriteLine("ResourceAllocationService is started");
            Console.WriteLine("Berth Planning Configurations Service is started");
            Console.WriteLine("Mobile ScheduledTasks Service is started");

            Console.WriteLine("Resource Allocation ConfigRule Service is started");
            Console.WriteLine("Craft Out Of Commission Service is started");
            Console.WriteLine("Hour24Report625Service is started");
            Console.WriteLine("Fuel Receipt Service is started");
            Console.WriteLine("Event Scheduler Service is started");
            Console.WriteLine("CraftReminderConfig Service is started");
            Console.WriteLine("Port General Configs Service is started");
            Console.WriteLine("Deployment Plan Service is started");
            Console.WriteLine("Docking Plan Service is started");
            Console.WriteLine("Supplementary Dry Dock Service is started");
            Console.WriteLine("UserPreferenceService is started");
            Console.WriteLine("SuppMiscServiceRecordingService is started");
            Console.WriteLine("DepartureNotice Service is started");
            Console.WriteLine("ServiceType Service is started");
            Console.WriteLine("ServiceTypeDesignation Service is started");
            Console.WriteLine("BudgetedValuesService Service is started");
            Console.WriteLine("MarineRevenueService Service is started");
            Console.WriteLine("RevenueStopListService is started");
            Console.WriteLine("MaterialCodeMasterService is started");
            Console.WriteLine("SAPPostingService is started");
            Console.WriteLine("VesselSAPPostingService is started");
            Console.WriteLine("NotificationPublisherService is started");
            Console.WriteLine("AutomatedSlotBlockingService is started");
            Console.WriteLine("MarpolService is started");           

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            Console.WriteLine("PortEntryPassApplicationService stopped.");
            Console.WriteLine("Resource Allocation ConfigRule stopped.");
            Console.WriteLine("Port Service monitor stopped.");
            Console.WriteLine("Bollard Service monitor stopped.");
            Console.WriteLine("Quay Service monitor stopped.");
            Console.WriteLine("Agent Service monitor stopped.");
            Console.WriteLine("Account Service monitor stopped.");
            Console.WriteLine("Auditlog Service monitor stopped.");
            Console.WriteLine("Berth Service monitor stopped.");
            Console.WriteLine("User Service monitor stopped.");
            Console.WriteLine("TerminalOperator Service monitor stopped.");
            Console.WriteLine("ElectronicNotifications Service monitor stopped.");
            Console.WriteLine("Employee Service monitor stopped.");
            Console.WriteLine("Common Service monitor stopped.");
            Console.WriteLine("ServiceRequest Service monitor stopped.");
            Console.WriteLine("NewsService monitor stopped.");
            Console.WriteLine("SuperCategory Service monitor stopped.");
            Console.WriteLine("Craftservice Service monitor stopped.");
            Console.WriteLine("LicensingRequest Service monitor stopped.");
            Console.WriteLine("VesselETAChange Service monitor stopped.");
            Console.WriteLine("Module Service monitor stopped.");
            Console.WriteLine("SubCategoryRequest Service monitor stopped.");
            Console.WriteLine("VesselAgentChangeRequest Service monitor stopped.");
            Console.WriteLine("VesselCallAnchorage Service monitor stopped.");
            Console.WriteLine("MobileConversation Service monitor stopped.");

            Console.WriteLine("VesselArrestImmobilizationSAMSAStop Service monitor stopped.");
            Console.WriteLine("Vessel Registration Service is stoped");
            Console.WriteLine("BerthPlanning Service monitor stopped.");
            Console.WriteLine("Role Privilege Service monitor stopped.");
            Console.WriteLine("Entity Service monitor stopped.");
            Console.WriteLine("PilotExemptionRequestService monitor stopped.");
            Console.WriteLine("File Service monitor stopped.");
            Console.WriteLine("Berth Maintenance Completion Service monitor stopped.");
            Console.WriteLine("Mobile Service monitor stopped.");
            Console.WriteLine("Voyage Monitoring Service  is stopped");
            Console.WriteLine("BerthMaintenance Service monitor stopped.");
            Console.WriteLine("Diving Request Service monitor stopped.");
            Console.WriteLine("WorkFlow Task Service stopped.");
            Console.WriteLine("MobileIncidentReport Service monitor stopped.");
            Console.WriteLine("Shift Service monitor stopped.");
            Console.WriteLine("DashBoard Service stopped.");
            Console.WriteLine("ResourceGroup Service is stopped.");
            Console.WriteLine("Report Builder Service is stopped.");
            Console.WriteLine("Supplymentary Service Request Service is stopped.");

            Console.WriteLine("External Diving Register Service is stopped.");
            Console.WriteLine("Berth Pre-Scheduling is stopped.");
            Console.WriteLine("Automated Slotting Service  is stopped.");
            Console.WriteLine("Resource Attendance Service  is stopped.");
            Console.WriteLine("Statement Fact Service  is stopped.");
            Console.WriteLine("Cargo Manifest Service  is stopped.");
            Console.WriteLine("Fuel Consumption Daily Log Service  is stopped.");

            Console.WriteLine("RosterService is stopped.");
            Console.WriteLine("RosterGroupService  is stopped.");


            Console.WriteLine("Supp Hot Work Inspection Service  is stopped.");
            Console.WriteLine("Supp Dock UnDock Time Service  is stopped.");
            Console.WriteLine("Automated Slot Config Service is stopped.");

            Console.WriteLine("Automated Resource Scheduling Service  is stopped.");
            Console.WriteLine("SuppServiceResourceAlloc Service is stopped.");
            Console.WriteLine("ResourceAllocationService is stopped");
            Console.WriteLine("Berth Planning Configurations Service is stopped");
            Console.WriteLine("Mobile ScheduledTasks Service is stopped");
            Console.WriteLine("Fuel Requisition Service is stopped");
            Console.WriteLine("Craft Out Of Commission Service is stopped");
            Console.WriteLine("Hour24Report625Service monitor stopped.");
            Console.WriteLine("Fuel Receipt Service is stopped");
            Console.WriteLine("Event Scheduler Service is stopped");
            Console.WriteLine("CraftReminderConfig Service is stopped");
            Console.WriteLine("Port General Configs Service is stopped");
            Console.WriteLine("Deployment Plan Service is stopped");
            Console.WriteLine("Docking Plan Service is stopped");
            Console.WriteLine("Supplementary Dry Dock Service is stopped");

            Console.WriteLine("Dredging Volume Service is stopped");
            Console.WriteLine("Dredging Priority Service is stopped");
            Console.WriteLine("UserPreferenceService is stopped");
            Console.WriteLine("SuppMiscServiceRecordingService is stopped");
            Console.WriteLine("DepartureNotice Service is stopped");
            Console.WriteLine("ServiceType Service is stopped");
            Console.WriteLine("ServiceTypeDesignation Service is stopped");
            Console.WriteLine("BudgetedValuesService Service is stopped");

            Console.WriteLine("DryDockSchedulerService is stopped");
            Console.WriteLine("MarineRevenueService is stopped");
            Console.WriteLine("RevenueStopListService stopped.");
            Console.WriteLine("MaterialCodeMasterService stopped.");
            Console.WriteLine("SAPPostingService stopped.");
            Console.WriteLine("NotificationPublisherService is stopped.");
            Console.WriteLine("AutomatedSlotBlockingService is stopped.");
            Console.WriteLine("MarpolService stopped.");

            StopService(hostPortEntryPassApplicationService, "PortEntryPassApplicationService");
            StopService(hostResourceAllocationConfigRuleService, "Resource Allocation ConfigRule");
            StopService(hostPortService, "PortService");
            StopService(hostBollardService, "BollardService");
            StopService(hostQuayService, "QuayService");
            StopService(hostBollardService, "BollardService");
            StopService(hostAccountService, "AccountService");
            StopService(hostAgentService, "AgentService");
            StopService(hostAuditLogService, "AuditLogService");
            StopService(hostBerthService, "BerthService");
            StopService(hostEmailService, "EmailService");
            StopService(hostEmailService, "UserService");
            StopService(hostEmailService, "TerminalOperator");
            StopService(hostUserService, "UserService");
            StopService(hostElectronicNotificationsService, "ElectronicNotificationsService");
            StopService(hostEmployeeService, "EmployeeService");
            StopService(hostCommonService, "CommonService");
            StopService(hostServiceRequest, "ServiceRequest");
            StopService(hostArrivalNotificationRequest, "ServiceRequest");
            StopService(hostNewsServiceRequest, "NewsService");
            StopService(hostModuleRequest, "ModuleService");
            StopService(hostCraftServiceRequest, "CraftService");
            StopService(hostLicensingRequestService, "LicensingRequestService");
            StopService(hostSuperCategoryRequest, "SuperCategoryService");
            StopService(hostSubCategoryRequest, "SubCategoryService");
            StopService(hostVesselAgentChangeRequest, "VesselAgentChangeRequest");

            StopService(hostVesselETAChangeService, "VesselETAChangeService");
            StopService(hostSubCategoryRequest, "SubCategoryRequest");
            StopService(hostVesselCallAnchorageService, "VesselCallAnchorageService");
            StopService(hostVesselArrestImmobilizationSAMSAStopService, "VesselArrestImmobilizationSAMSAStopService");
            StopService(hostRolePrivilegeRequest, "RolePrivilegeService");
            StopService(hostEntityRequest, "EntityService");
            StopService(hostBerthPlanningService, "BerthPlanningService");
            StopService(hostPilotExemptionRequestService, "PilotExemptionRequestService");
            StopService(hostFileService, "FileService");
            StopService(BerthMainCompService, "BerthMaintenanceCompletionService");
            StopService(hostMobileService, "MobileService");
            StopService(hostVoyageMonitoringService, "VoyageMonitoringService");
            StopService(hostBerthMaintenanceRequest, "BerthMaintenanceService");
            StopService(hostDivingRequestService, "DivingRequestService");
            StopService(hostWorkFlowTaskService, "WorkFlowTaskService");
            StopService(hostMobileIncidentReportService, "MobileIncidentReportService");
            StopService(hostDashBoardService, "VDashBoardService");
            StopService(hostShiftService, "ShiftService");
            StopService(hostMobileConversationService, "MobileConversationService");
            //StopService(hostVesselRegistrationService, "VesselRegistrationService");
            StopService(hostVesselRegistrationService, "VesselRegistrationService");
            StopService(hostResourceGroupService, "ResourceGroupService");
            StopService(hostReportBuilderService, "ReportBuilderService");
            StopService(hostSupplymentaryServiceRequestService, "SupplymentaryServiceRequestService");
            StopService(hostBerthPreSchedulingService, "BerthPreScheduling");
            StopService(hostAutomatedSlottingService, "AutomatedSlottingService");
            StopService(hostExternalDivingRegisterService, "ExternalDivingRegisterService");
            StopService(hostResourceAttendanceService, "ResourceAttendanceService");
            StopService(hostSuppHotWorkInspectionService, "SuppHotWorkInspectionService");

            StopService(hostSuppDockUnDockTimeService, "SuppDockUnDockTimeService");
            StopService(hostFuelConsumptionDailyLogService, "FuelConsumptionDailyLogService");
            StopService(hostRosterService, "RosterService");
            StopService(hostStatementFactService, "StatementFactService");
            //StopService(hostRosterGroupService, "RosterGroupService");
            StopService(hostAutomatedSlotConfigurationService, "AutomatedSlotConfigurationService");
            StopService(hostExternalDivingRegisterService, "ExternalDivingRegisterService");
            StopService(hostAutomatedResourceSchedulingService, "AutomatedResourceSchedulingService");

            StopService(hostSuppServiceResourceAllocService, "SuppServiceResourceAllocService");
            StopService(hostResourceAllocationService, "ResourceAllocationService");
            StopService(hostBerthPlanningConfigurationsService, "BerthPlanningConfigurationsService");
            StopService(hostMobileScheduledTasksService, "MobileScheduledTasksService");
            StopService(hostFuelRequisitionRequest, "FuelRequisitionService");
            StopService(hostCraftOutOfCommissionService, "CraftOutOfCommissionService");
            StopService(hostHour24Report625Service, "Hour24Report625Service");
            StopService(hostFuelReceiptRequest, "FuelReceiptService");
            StopService(hostEventSchedulerService, "EventSchedulerService");
            StopService(hostCraftReminderConfigService, "CraftReminderConfigService");

            StopService(hostPortGeneralConfigsService, " PortGeneralConfigsService");
            StopService(hostDeploymentPlanService, "DeploymentPlanService");
            StopService(hostPortInformationService, "PortInformationService");
            StopService(hostDockingPlanService, "DockingPlanService");

            StopService(hostDredgingPriorityService, "DredgingPriorityService");
            StopService(hostSuppDryDockService, "SuppDryDockService");

            StopService(hostUserPreferenceService, "UserPreferenceService");
            StopService(hostSuppMiscServiceRecordingService, "SuppMiscServiceRecordingService");
            StopService(hostLocationService, "LocationService");
            StopService(hostDepartureNoticeService, "DepartureNoticeService");
            StopService(hostServiceTypeService, "ServiceTypeService");
            StopService(hostServiceTypeDesignationService, "ServiceTypeDesignationService");
            StopService(hostBudgetedValuesService, "BudgetedValuesService");
            StopService(hostDryDockSchedulerService, "DryDockSchedulerService");

            StopService(hostSuppDryDockExtensionService, "SuppDryDockExtensionService");
            StopService(hostMarineRevenueService, "MarineRevenueService");
            StopService(hostRevenueStopListService, "RevenueStopListService");
            StopService(hostMaterialCodeMasterService, "MaterialCodeMasterService");
            StopService(hostSAPPostingService, "SAPPostingService");
            StopService(hostVesselSAPPostingService, "VesselSAPPostingService");
            StopService(hostNotificationPublisherService, "NotificationPublisherService");
            StopService(hostAutomatedSlotBlockingService, "AutomatedSlotBlockingService");
            StopService(hostmarpolService, "MarpolService");             
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service {0} started.", serviceDescription);


            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Listening on endpoint:"), CultureInfo.InvariantCulture);
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Address: {0}", endpoint.Address.Uri.ToString()), CultureInfo.InvariantCulture);
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,"Binding: {0}", endpoint.Binding.Name), CultureInfo.InvariantCulture);
                Console.WriteLine(string.Format( CultureInfo.CurrentCulture,"Contract: {0}", endpoint.Contract.ConfigurationName), CultureInfo.CurrentCulture);
            }

            Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service {0} stopped.", serviceDescription);

        }

        public static SM.ServiceHost hostArrivalNotificationRequest { get; set; }
    }
}
