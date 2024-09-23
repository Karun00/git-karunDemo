using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class OtherServiceRecordingVO
    {
        [DataMember]
        public int OtherServiceRecordingID { get; set; }
        [DataMember]
        public int ValPKID { get; set; }
        [DataMember]
        public int ResourceAllocationID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LineUp { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LineDown { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PilotOn { get; set; }
        [DataMember]
        public Nullable<decimal> OpeningMeterReading { get; set; }
        [DataMember]
        public Nullable<decimal> ClosingMeterReading { get; set; }
        [DataMember]
        public Nullable<decimal> TotalDispensed { get; set; }
        [DataMember]
        public string MeterSerialNo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstSwing { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastSwing { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeAlongSide { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Deficiencies { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BackToQuay { get; set; }
        [DataMember]
        public bool Extend { get; set; }
        [DataMember]
        public string BerthKey { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string DelayReason { get; set; }
        [DataMember]
        public string DelayOtherReason { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        [DataMember]
        public bool IsCompleted { get; set; }
        [DataMember]
        public string MeterNo { get; set; }
        [DataMember]
        public bool IsTop { get; set; }
        [DataMember]
        public string action { get; set; }
    }
}
