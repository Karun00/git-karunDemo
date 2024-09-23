using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class EventScheduleTrack : EntityBase
    {
        [DataMember]
        public int EventScheduleTrackID { get; set; }
        [DataMember]
        public Nullable<int> EventScheduleTaskID { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public Nullable<int> NotificationId { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public Nullable<int> WorkflowProcessId { get; set; }
        [DataMember]
        public virtual EventScheduleTask EventScheduleTask { get; set; }
        [DataMember]
        public virtual Notification Notification { get; set; }
        [DataMember]
        public virtual WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public virtual WorkflowProcess WorkflowProcess { get; set; }
    }
}
