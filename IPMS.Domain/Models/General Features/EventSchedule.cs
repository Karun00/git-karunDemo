using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class EventSchedule : EntityBase
    {

        public EventSchedule()
        {
            this.EventScheduleTasks = new List<EventScheduleTask>();
        }
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
        public  Entity Entity { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTask> EventScheduleTasks { get; set; }


    }
}
