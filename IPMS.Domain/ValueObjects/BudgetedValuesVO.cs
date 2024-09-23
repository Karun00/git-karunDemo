using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class BudgetedValuesVO
    {
        [DataMember]
        public Nullable<int> BudgetedValuesID { get; set; }
        [DataMember]
        public Nullable<int> FinancialYearID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DataMember]
        public string IsCurrentFinancialYear { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public Nullable<decimal> VolumesContainers { get; set; }
        [DataMember]
        public Nullable<decimal> VolumesRBCT { get; set; }
        [DataMember]
        public Nullable<decimal> VolumesDryBulk { get; set; }
        [DataMember]
        public Nullable<decimal> VolumesBreakBulk { get; set; }
        [DataMember]
        public Nullable<decimal> MovementsContainers { get; set; }
        [DataMember]
        public Nullable<decimal> MovementsRBCT { get; set; }
        [DataMember]
        public Nullable<decimal> MovementsDryBulk { get; set; }
        [DataMember]
        public Nullable<decimal> MovementsBreakBulk { get; set; }
        [DataMember]
        public Nullable<decimal> STATContainers { get; set; }
        [DataMember]
        public Nullable<decimal> STATRBCT { get; set; }
        [DataMember]
        public Nullable<decimal> STATDryBulk { get; set; }
        [DataMember]
        public Nullable<decimal> STATBreakBulk { get; set; }

        //Newly Added as Per Report Format
        [DataMember]
        public Nullable<decimal> TotalArrivals { get; set; }
        [DataMember]
        public Nullable<decimal> TotalGT { get; set; }
        [DataMember]
        public Nullable<decimal> TotalPilotDelays { get; set; }
        [DataMember]
        public Nullable<decimal> TotalTugDelays { get; set; }
        [DataMember]
        public Nullable<decimal> TotalBerthingDelays { get; set; }
        [DataMember]
        public Nullable<decimal> TotalTugAvailability { get; set; }
        [DataMember]
        public Nullable<decimal> TotalTugUtilization { get; set; }

        [DataMember]
        public string BudgetedValuesFYDescription { get; set; }
        [DataMember]
        public string FinancialYear { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
