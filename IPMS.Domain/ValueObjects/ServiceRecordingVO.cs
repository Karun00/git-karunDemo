using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class ServiceRecordingVO
    {
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public int AgentID { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> PilotOnBoard { get; set; }
        public Nullable<System.DateTime> PilotOff { get; set; }
        public Nullable<System.DateTime> FirstLineIn { get; set; }
        public Nullable<System.DateTime> LastLineIn { get; set; }
        
        public Nullable<System.DateTime> FirstLineOut { get; set; }
        public Nullable<System.DateTime> LastLineOut { get; set; }
        public string MovementType { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        public Nullable<System.DateTime> EndTime1 { get; set; }
        public Nullable<System.DateTime> WaitingStartTime1 { get; set; }
        public Nullable<System.DateTime> WaitingEndTime1 { get; set; }
    }
}
