using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ShiftingBerthingTaskExecutionVO
    {
        [DataMember]
        public int BerthingTaskExecutionID { get; set; }
        [DataMember]
        public int ResourceAllocationID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }
        [DataMember]
        public string FromBerthPortCode { get; set; }
        [DataMember]
        public string FromBerthQuayCode { get; set; }
        [DataMember]
        public string FromBerthCode { get; set; }
        [DataMember]
        public string ToBerthPortCode { get; set; }
        [DataMember]
        public string ToBerthQuayCode { get; set; }
        [DataMember]
        public string ToBerthCode { get; set; }
        [DataMember]
        public string BerthingSide { get; set; }
        [DataMember]
        public string FromBollardPortCode { get; set; }
        [DataMember]
        public string FromBollardQuayCode { get; set; }
        [DataMember]
        public string FromBollardBerthCode { get; set; }
        [DataMember]
        public string FromBollardCode { get; set; }
        [DataMember]
        public string ToBollardPortCode { get; set; }
        [DataMember]
        public string ToBollardQuayCode { get; set; }
        [DataMember]
        public string ToBollardBerthCode { get; set; }
        [DataMember]
        public string ToBollardCode { get; set; }
        [DataMember]
        public string MomentType { get; set; }
        [DataMember]
        public string MooringBollardBowPortcode { get; set; }
        [DataMember]
        public string MooringBollardBowQuayCode { get; set; }
        [DataMember]
        public string MooringBollardBowBerthCode { get; set; }
        [DataMember]
        public string MooringBollardBowBollardCode { get; set; }
        [DataMember]
        public string MooringBollardStemPortcode { get; set; }
        [DataMember]
        public string MooringBollardStemQuayCode { get; set; }
        [DataMember]
        public string MooringBollardStemBerthCode { get; set; }
        [DataMember]
        public string MooringBollardStemBollardCode { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstLineIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastLineIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstLineOut { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastLineOut { get; set; }
        [DataMember]
        public string ForwardDraft { get; set; }
        [DataMember]
        public string AftDraft { get; set; }
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
        //public  Berth Berth { get; set; }
        //public Berth Berth1 { get; set; }
        //public Bollard Bollard { get; set; }
        //public Bollard Bollard1 { get; set; }
        //public Bollard Bollard2 { get; set; }
        //public Bollard Bollard3 { get; set; } 
        [DataMember]
        public int ValPKID { get; set; }
        [DataMember]
        public string FromBerthKey { get; set; }
        [DataMember]
        public string ToBerthKey { get; set; }
        [DataMember]
        public string FromBolardKey { get; set; }
        [DataMember]
        public string ToBolardKey { get; set; }
        [DataMember]
        public string MooringBolardBowKey { get; set; }
        [DataMember]
        public string MooringBolardStemKey { get; set; }

        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string DelayReason { get; set; }
        [DataMember]
        public string DelayOtherReason { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
    }
}
