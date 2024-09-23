using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models   
{
    public partial class ShiftingBerthingTaskExecution:EntityBase
    {
        public int BerthingTaskExecutionID { get; set; }
        public int ResourceAllocationID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string FromBerthPortCode { get; set; }
        public string FromBerthQuayCode { get; set; }
        public string FromBerthCode { get; set; }
        public string ToBerthPortCode { get; set; }
        public string ToBerthQuayCode { get; set; }
        public string ToBerthCode { get; set; }
        public string BerthingSide { get; set; }
        public string FromBollardPortCode { get; set; }
        public string FromBollardQuayCode { get; set; }
        public string FromBollardBerthCode { get; set; }
        public string FromBollardCode { get; set; }
        public string ToBollardPortCode { get; set; }
        public string ToBollardQuayCode { get; set; }
        public string ToBollardBerthCode { get; set; }
        public string ToBollardCode { get; set; }
        public string MooringBollardBowPortcode { get; set; }
        public string MooringBollardBowQuayCode { get; set; }
        public string MooringBollardBowBerthCode { get; set; }
        public string MooringBollardBowBollardCode { get; set; }
        public string MooringBollardStemPortcode { get; set; }
        public string MooringBollardStemQuayCode { get; set; }
        public string MooringBollardStemBerthCode { get; set; }
        public string MooringBollardStemBollardCode { get; set; }
        public Nullable<System.DateTime> FirstLineIn { get; set; }
        public Nullable<System.DateTime> LastLineIn { get; set; }
        public Nullable<System.DateTime> FirstLineOut { get; set; }
        public Nullable<System.DateTime> LastLineOut { get; set; }
        public string ForwardDraft { get; set; }
        public string AftDraft { get; set; }
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        public string DelayReason { get; set; }
        public string DelayOtherReason { get; set; }
        public string Remarks { get; set; }
        public string Deficiencies { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Berth Berth { get; set; }
        public virtual Berth Berth1 { get; set; }
        public virtual Bollard Bollard { get; set; }
        public virtual Bollard Bollard1 { get; set; }
        public virtual Bollard Bollard2 { get; set; }
        public virtual Bollard Bollard3 { get; set; }
        public virtual ResourceAllocation ResourceAllocation { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
 