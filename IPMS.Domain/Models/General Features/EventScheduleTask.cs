using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class EventScheduleTask : EntityBase
    {
        public EventScheduleTask()
        {
            this.EventScheduleTracks = new List<EventScheduleTrack>();
        }
        [DataMember]
        public int EventScheduleTaskID { get; set; }
        [DataMember]
        public int EventScheduleID { get; set; }
        [DataMember]
        public int SequenceID { get; set; }
        [DataMember]
        public string EventScheduleTaskName { get; set; }
        [DataMember]
        public string EventScheduleTaskDescription { get; set; }
        [DataMember]
        public string EventScheduleParameter { get; set; }
        [DataMember]
        public string EventScheduleParameterValues { get; set; }
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
        public  EventSchedule EventSchedule { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTrack> EventScheduleTracks { get; set; }
    }
}
