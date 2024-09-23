using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class EventScheduleVO
    {      
        [DataMember]
        public int EventScheduleID { get; set; }
        [DataMember]
        public Nullable<int> EntityID { get; set; }
        [DataMember]
        public string EventScheduleName { get; set; }
        [DataMember]
        public string EventScheduleType { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EventScheduleStartDate { get; set; }
        [DataMember]
        public string EventScheduleTime { get; set; }
        [DataMember]
        public string ExecutionPlan { get; set; }
        [DataMember]
        public Nullable<System.DateTime> NextExecutionDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EventScheduleEndDateTime { get; set; }
        [DataMember]
        public Nullable<int> ExecutionCount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastExecutionDateTime { get; set; }
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
        public EntityVO Entity { get; set; }
       


    }
}
