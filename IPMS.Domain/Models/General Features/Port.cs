using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Port : EntityBase
    {

        public Port()
        {
            this.AgentPorts = new List<AgentPort>();
            this.ArrivalNotifications = new List<ArrivalNotification>();


            this.AutomatedSlotConfigurations = new List<AutomatedSlotConfiguration>();
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthPlanningConfigurations = new List<BerthPlanningConfigurations>();

            this.Codes = new List<Code>();
            this.DeploymentPlans = new List<DeploymentPlan>();
            this.DockingPlans = new List<DockingPlan>();

            this.NotificationPorts = new List<NotificationPort>();
            this.Incidents = new List<Incident>();
            this.LicenseRequestPorts = new List<LicenseRequestPort>();
            this.ResourceAllocationConfigRules = new List<ResourceAllocationConfigRule>();
            this.ResourceAllocationMovementTypeRules = new List<ResourceAllocationMovementTypeRule>();
            // this.TerminalOperators = new List<TerminalOperator>();
            //this.MovementSlotConfigs = new List<MovementSlotConfig>();
            this.Pilots = new List<Pilot>();
            this.Quays = new List<Quay>();
            this.ResourceGroups = new List<ResourceGroup>();
            this.Shifts = new List<Shift>();
            this.UserPorts = new List<UserPort>();
            //added by shankar
            //this.Vessels = new List<Vessel>();
            this.WorkflowInstances = new List<WorkflowInstance>();
            this.Notifications = new List<Notification>();
            this.SAPPostings = new List<SAPPosting>();
            this.CraftsPort = new List<Craft>();
            this.SystemNotifications = new List<SystemNotification>();
            // Added by sandeep on 04-08-2014
            this.Locations = new List<Location>();
            // -- end
            this.ResourceGroups = new List<ResourceGroup>();
            this.FuelConsumptionDailyLogs = new List<FuelConsumptionDailyLog>();
            this.FuelRequisitions = new List<FuelRequisition>();
            this.RevenueStopLists = new List<RevenueStopList>();

            // -- Added by sandeep on 27-09-2014
            this.ServiceTypeDesignations = new List<ServiceTypeDesignation>();
            // -- end
            this.WorkflowTask1 = new List<WorkflowTask>();

            this.WorkflowTaskRole1 = new List<WorkflowTaskRole>();

            this.PortGeneralConfigs = new List<PortGeneralConfig>();
            this.PermitRequests = new List<PermitRequest>();


            // this.SuppDryDocks = new List<SuppDryDock>();


            this.TerminalOperatorPorts = new List<TerminalOperatorPort>();

            // -- Added By Santosh on 05-12-2014
            this.DepartureNotices = new List<DepartureNotice>();
            // -- end

            // -- Added By Santosh on 17-12-2014
            this.BudgetedValues = new List<BudgetedValues>();
            // -- end
            this.MaterialCodePorts = new List<MaterialCodePort>();
            this.AgentAccounts = new List<AgentAccount>();
            this.RevenuePostings = new List<RevenuePosting>();
            this.Hour24Port = new List<Hour24Report625>();           
            this.NewsPorts = new List<NewsPort>();
            this.AutomatedSlotBlockings = new List<AutomatedSlotBlocking>();
        }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string InternationalCharacter { get; set; }
        [DataMember]
        public string GeographicLocation { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string Website { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }


        [DataMember]
        public  ICollection<AutomatedSlotConfiguration> AutomatedSlotConfigurations { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }

        [DataMember]
        public  ICollection<BerthPlanningConfigurations> BerthPlanningConfigurations { get; set; }
        [DataMember]
        public  ICollection<Craft> CraftsPort { get; set; }
        [DataMember]
        public  ICollection<PermitRequest> PermitRequests { get; set; }

        [DataMember]
        public  ICollection<Code> Codes { get; set; }
        [DataMember]
        public  ICollection<DeploymentPlan> DeploymentPlans { get; set; }
        [DataMember]
        public  ICollection<DockingPlan> DockingPlans { get; set; }

        [DataMember]
        public  ICollection<NotificationPort> NotificationPorts { get; set; }
        [DataMember]
        public  ICollection<Incident> Incidents { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts { get; set; }
        //[DataMember]
        //public  ICollection<MovementSlotConfig> MovementSlotConfigs { get; set; }


        [DataMember]
        public  ICollection<ResourceAllocationConfigRule> ResourceAllocationConfigRules { get; set; }
        [DataMember]
        public  ICollection<ResourceAllocationMovementTypeRule> ResourceAllocationMovementTypeRules { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots { get; set; }
        [DataMember]
        public  User CreatedUser { get; set; }
        [DataMember]
        public  User ModifiedUser { get; set; }
        [DataMember]
        public  ICollection<Quay> Quays { get; set; }
        //[DataMember]
        //public  ICollection<ResourceGroup> ResourceGroups { get; set; }
        [DataMember]
        public  ICollection<Shift> Shifts { get; set; }
        [DataMember]
        public  ICollection<UserPort> UserPorts { get; set; }
        // added by shankar
        //[DataMember]
        //public  ICollection<Vessel> Vessels { get; set; }
        [DataMember]
        public  ICollection<WorkflowInstance> WorkflowInstances { get; set; }
        [DataMember]
        public  PortConfiguration PortConfiguration { get; set; }
        [DataMember]
        public  ICollection<Notification> Notifications { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPPostings { get; set; }

        [DataMember]
        public  ICollection<SystemNotification> SystemNotifications { get; set; }

        // -- Added by sandeep
        public  ICollection<Location> Locations { get; set; }
        // -- end
        //  public  ICollection<SystemNotification> SystemNotification { get; set; }
        [DataMember]
        public  ICollection<ResourceGroup> ResourceGroups { get; set; }
        [DataMember]
        public  ICollection<FuelConsumptionDailyLog> FuelConsumptionDailyLogs { get; set; }
        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions { get; set; }
        [DataMember]
        public  ICollection<RevenueStopList> RevenueStopLists { get; set; }
        //   [DataMember]
        //   public  ICollection<TerminalOperator> TerminalOperators { get; set; }

        // -- Added by sandeep on 27-09-2014
        [DataMember]
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations { get; set; }
        // -- end
        [DataMember]
        public  ICollection<WorkflowTask> WorkflowTask1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowTaskRole> WorkflowTaskRole1 { get; set; }
        [DataMember]
        public  ICollection<PortGeneralConfig> PortGeneralConfigs { get; set; }

        // -- Sandeep Added on 06-11-2014
        //[DataMember]
        //public  ICollection<SuppDryDock> SuppDryDocks { get; set; }
        // -- end
        //-- added by shankar on 07-nov-14
        public  ICollection<PortContent> PortContents { get; set; }
        //-- end
        [DataMember]
        public  ICollection<TerminalOperatorPort> TerminalOperatorPorts { get; set; }


        // -- Added By Santosh on 05-12-2014
        [DataMember]
        public  ICollection<DepartureNotice> DepartureNotices { get; set; }
        // -- end

        // -- Added By Santosh on 17-12-2014
        [DataMember]
        public  ICollection<BudgetedValues> BudgetedValues { get; set; }
        // -- end

        // -- Added By Amala on 7-1-2015
        [DataMember]
        public  ICollection<MaterialCodePort> MaterialCodePorts { get; set; }
        [DataMember]
        public  ICollection<AgentAccount> AgentAccounts { get; set; }
        [DataMember]
        public  ICollection<RevenuePosting> RevenuePostings { get; set; }

        [DataMember]
        public  ICollection<Hour24Report625> Hour24Port { get; set; }
        
        [DataMember]
        public ICollection<NewsPort> NewsPorts { get; set; }
        [DataMember]
        public ICollection<AutomatedSlotBlocking> AutomatedSlotBlockings { get; set; }
    }
}
