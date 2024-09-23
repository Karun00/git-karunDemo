using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class OtherServiceRecording : EntityBase
    {
        public int OtherServiceRecordingID { get; set; }
        public int ResourceAllocationID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> LineUp { get; set; }
        public Nullable<System.DateTime> LineDown { get; set; }
        public Nullable<System.DateTime> PilotOn { get; set; }
        public string MeterSerialNo { get; set; }
        public Nullable<decimal> OpeningMeterReading { get; set; }
        public Nullable<decimal> ClosingMeterReading { get; set; }
        public Nullable<decimal> TotalDispensed { get; set; }
        public Nullable<System.DateTime> FirstSwing { get; set; }
        public Nullable<System.DateTime> LastSwing { get; set; }
        public Nullable<System.DateTime> TimeAlongSide { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
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
        public Nullable<System.DateTime> BackToQuay { get; set; }
        public string Extend { get; set; }
        public virtual Berth Berth { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ResourceAllocation ResourceAllocation { get; set; }
        public string IsCompleted { get; set; }
        public string MeterNo { get; set; }
    }
}
