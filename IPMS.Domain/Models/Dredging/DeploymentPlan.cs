using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class DeploymentPlan : EntityBase
    {
        public DeploymentPlan()
        {
            this.DeploymentBudgets = new List<DeploymentBudget>();
            this.DredgingPriorities = new List<DredgingPriority>();
        }

       
        public int DeploymentPlanID { get; set; }
        public string PortCode { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
        public Nullable<int> FinancialYearID { get; set; }
        public  ICollection<DeploymentBudget> DeploymentBudgets { get; set; }
        public  User User { get; set; }
        public  FinancialYear FinancialYear { get; set; }
        public  User User1 { get; set; }
        public  Port Port { get; set; }
        public  ICollection<DredgingPriority> DredgingPriorities { get; set; }
    }
}
