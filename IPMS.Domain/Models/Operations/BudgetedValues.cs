using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class BudgetedValues : EntityBase
    {
        public int BudgetedValuesID { get; set; }
        public int FinancialYearID { get; set; }
        public string PortCode { get; set; }
        public Nullable<decimal> VolumesContainers { get; set; }
        public Nullable<decimal> VolumesRBCT { get; set; }
        public Nullable<decimal> VolumesDryBulk { get; set; }
        public Nullable<decimal> VolumesBreakBulk { get; set; }
        public Nullable<decimal> MovementsContainers { get; set; }
        public Nullable<decimal> MovementsRBCT { get; set; }
        public Nullable<decimal> MovementsDryBulk { get; set; }
        public Nullable<decimal> MovementsBreakBulk { get; set; }
        public Nullable<decimal> STATContainers { get; set; }
        public Nullable<decimal> STATRBCT { get; set; }
        public Nullable<decimal> STATDryBulk { get; set; }
        public Nullable<decimal> STATBreakBulk { get; set; }

        //Newly Added as Per report format
        public Nullable<decimal> TotalArrivals { get; set; }
        public Nullable<decimal> TotalGT { get; set; }
        public Nullable<decimal> TotalPilotDelays { get; set; }
        public Nullable<decimal> TotalTugDelays { get; set; }
        public Nullable<decimal> TotalBerthingDelays { get; set; }
        public Nullable<decimal> TotalTugAvailability { get; set; }
        public Nullable<decimal> TotalTugUtilization { get; set; }

        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }
        public virtual FinancialYear FinancialYear { get; set; }
        public virtual User User1 { get; set; }
        public virtual Port Port { get; set; }
    }
}
