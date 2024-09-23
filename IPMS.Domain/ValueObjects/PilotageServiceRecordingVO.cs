using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class PilotageServiceRecordingVO
    {
        [DataMember]
        public int PilotageServiceRecordingID { get; set; }
        [DataMember]
        public int ValPKID { get; set; }
        [DataMember]
        public int ResourceAllocationID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ActualScheduledTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PilotOnBoard { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PilotOff { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        [DataMember]
        public string AdditionalTugs { get; set; }
        [DataMember]
        public bool OffSteam { get; set; }
        [DataMember]
        public bool MarineRevenueCleared { get; set; }
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
        public string DelayReason { get; set; }
        [DataMember]
        public string MOPSDelay { get; set; }
        [DataMember]
        public string DelayOtherReason { get; set; }

    }
}
