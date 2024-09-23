using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Notification : EntityBase
    {
        public Notification()
        {
            this.SystemNotifications = new List<SystemNotification>();
            this.EventScheduleTracks = new List<EventScheduleTrack>();
           
        }

        [DataMember]
        public int NotificationId { get; set; }
        [DataMember]
        public string NotificationTemplateCode { get; set; }
        [DataMember]
        public System.DateTime DateTime { get; set; }
        [DataMember]
        public string Reference { get; set; }       
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string EmailStatus { get; set; }
        [DataMember]
        public string SMSStatus { get; set; }
        [DataMember]
        public string SystemNotificationStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  NotificationTemplate NotificationTemplate { get; set; }
        [DataMember]
        public  ICollection<SystemNotification> SystemNotifications { get; set; }
        [DataMember]
        public string PortCode { get; set; }

        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public int UserTypeId { get; set; }

        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTrack> EventScheduleTracks { get; set; }
        [DataMember]
        public string NotificationTemplateBase { get; set; }
         [DataMember]
        public string EmailAddress { get; set; }
       
    }
}
