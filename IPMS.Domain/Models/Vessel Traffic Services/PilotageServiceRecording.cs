using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models   
{
    public partial class PilotageServiceRecording:EntityBase
    {
        public int PilotageServiceRecordingID { get; set; }
        public int ResourceAllocationID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> ActualScheduledTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> PilotOnBoard { get; set; }
        public Nullable<System.DateTime> PilotOff { get; set; }
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        public string AdditionalTugs { get; set; }
        public string OffSteam { get; set; }
        public string MarineRevenueCleared { get; set; }
        public string DelayReason { get; set; }
        public string DelayOtherReason { get; set; }
        public string Remarks { get; set; }
        public string Deficiencies { get; set; }
        public string RecordStatus { get; set; }
        public string MOPSDelay { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ResourceAllocation ResourceAllocation { get; set; }
    }
}
