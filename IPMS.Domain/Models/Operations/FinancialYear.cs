using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class FinancialYear : EntityBase
    {
        public FinancialYear()
        {
            this.BudgetedValues = new List<BudgetedValues>();
            this.DeploymentPlans = new List<DeploymentPlan>();
            this.DredgingPriorities = new List<DredgingPriority>();

            // -- Added by sandeep on 29-12-2014
            this.DredgingOperations = new List<DredgingOperation>();
            // -- end
        }

        public int FinancialYearID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string IsCurrentFinancialYear { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  ICollection<BudgetedValues> BudgetedValues { get; set; }
        public  ICollection<DeploymentPlan> DeploymentPlans { get; set; }
        public  ICollection<DredgingPriority> DredgingPriorities { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }

        // -- Added by sandeep on 29-12-2014
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        // -- end
    }
}
