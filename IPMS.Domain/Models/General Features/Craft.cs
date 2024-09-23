using Core.Repository;
using System;
using System.Collections.Generic;

namespace IPMS.Domain.Models
{
    public partial class Craft : EntityBase
    {
        public Craft()
        {
            this.CraftOutOfCommissions = new List<CraftOutOfCommission>();
            this.CraftReminderConfigs = new List<CraftReminderConfig>();
            this.DeploymentBudgets = new List<DeploymentBudget>();
            this.DeploymentBudgets1 = new List<DeploymentBudget>();
            this.DeploymentBudgets2 = new List<DeploymentBudget>();
            this.DeploymentBudgets3 = new List<DeploymentBudget>();
            this.DeploymentBudgets4 = new List<DeploymentBudget>();
            this.DeploymentBudgets5 = new List<DeploymentBudget>();
            this.DeploymentBudgets6 = new List<DeploymentBudget>();
            this.DeploymentBudgets7 = new List<DeploymentBudget>();
            this.DeploymentBudgets8 = new List<DeploymentBudget>();
            this.DeploymentBudgets9 = new List<DeploymentBudget>();
            this.DeploymentBudgets10 = new List<DeploymentBudget>();
            this.DeploymentBudgets11 = new List<DeploymentBudget>();
            this.FuelConsumptionDailyLogs = new List<FuelConsumptionDailyLog>();
            this.FuelRequisitions = new List<FuelRequisition>();

            // -- Added by sandeep on 29-09-2014
            this.ResourceAllocations = new List<ResourceAllocation>();
            this.DredgingOperations = new List<DredgingOperation>();
            // -- end
        }

        public int CraftID { get; set; }
        public string CraftCode { get; set; }
        public string CraftName { get; set; }
        public string IMONo { get; set; }
        public string CallSign { get; set; }
        public string ExCallSign { get; set; }
        public string CraftType { get; set; }
        public Nullable<System.DateTime> CraftBuildDate { get; set; }
        public Nullable<System.DateTime> DateOfDelivery { get; set; }
        public string CraftNationality { get; set; }
        public string ClassificationSociety { get; set; }
        public Nullable<System.DateTime> CommissionDate { get; set; }
        public decimal AFCInMetricTon { get; set; }
        public string PortCode { get; set; }
        public string FuelType { get; set; }
        public string PortOfRegistry { get; set; }
        public Nullable<int> EnginePower { get; set; }
        public string EngineType { get; set; }
        public string PropulsionType { get; set; }
        public Nullable<int> NoOfPropellers { get; set; }
        public Nullable<int> MaxManeuveringSpeed { get; set; }
        public long BeamM { get; set; }
        public long RegisteredLengthM { get; set; }
        public long ForwardDraftM { get; set; }
        public long AftDraftM { get; set; }
        public long GrossRegisteredTonnageMT { get; set; }
        public long NetRegisteredTonnageMT { get; set; }
        public Nullable<long> DeadWeightTonnageMT { get; set; }
        public Nullable<long> BollardPullMT { get; set; }
        public string OwnersName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public Nullable<long> InitialFuelQuantityMT { get; set; }
        public Nullable<long> LOROBMT { get; set; }
        public Nullable<long> HYDROBMT { get; set; }
        public Nullable<long> FreshwaterROBMT { get; set; }
        public string CraftCommissionStatus { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        // -- Added by santosh on 09-01-2015
        public string DredgerColorCode { get; set; }
        // -- end

        public  SubCategory SubCategory { get; set; }
        public  SubCategory SubCategory1 { get; set; }
        public  SubCategory SubCategory2 { get; set; }
        public  SubCategory SubCategory3 { get; set; }
        public  User User { get; set; }
        public  SubCategory SubCategory4 { get; set; }
        public  Port Port1 { get; set; }
        public  SubCategory SubCategory5 { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory6 { get; set; }
        public  SubCategory SubCategory7 { get; set; }

        public  ICollection<CraftOutOfCommission> CraftOutOfCommissions { get; set; }
        public  ICollection<CraftReminderConfig> CraftReminderConfigs { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets1 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets2 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets3 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets4 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets5 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets6 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets7 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets8 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets9 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets10 { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets11 { get; set; }
        public  ICollection<FuelConsumptionDailyLog> FuelConsumptionDailyLogs { get; set; }
        public  ICollection<FuelRequisition> FuelRequisitions { get; set; }

        // -- Added by sandeep on 29-09-2014
        public  ICollection<ResourceAllocation> ResourceAllocations { get; set; }
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        // -- end        
    }
}
