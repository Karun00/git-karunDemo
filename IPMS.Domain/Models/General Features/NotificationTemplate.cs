using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class NotificationTemplate : EntityBase
    {
        public NotificationTemplate()
        {
            
            this.Notifications = new List<Notification>();
            this.NotificationRoles = new List<NotificationRole>();
            this.NotificationPorts = new List<NotificationPort>();
            this.SAPNotifications = new List<SAPPosting>();
        }

        [DataMember]
        public string NotificationTemplateCode { get; set; }
        [DataMember]
        public string NotificationTemplateName { get; set; }
        [DataMember]
        public string NotificationTemplateBase { get; set; }
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public string IsEmail { get; set; }
        [DataMember]
        public string EmailSubject { get; set; }
        [DataMember]
        public string EmailTemplate { get; set; }
        [DataMember]
        public string IsSMS { get; set; }
        [DataMember]
        public string SMSTemplate { get; set; }
        [DataMember]
        public string IsSysMessage { get; set; }
        [DataMember]
        public string SysMessageTemplate { get; set; }
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
        public  ICollection<Notification> Notifications { get; set; }
        [DataMember]
        public  ICollection<NotificationPort> NotificationPorts { get; set; }
        [DataMember]
        public  ICollection<NotificationRole> NotificationRoles { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  ICollection<SAPPosting> SAPNotifications { get; set; }
 
        
    }
}
