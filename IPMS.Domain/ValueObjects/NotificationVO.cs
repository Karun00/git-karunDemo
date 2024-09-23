using IPMS.Domain.Models;
using System;

namespace IPMS.Domain.ValueObjects
{
    public class NotificationVO
    {
        public int? NotificationId { get; set; }
        public int EntityID { get; set; }
        public string NotificationTemplateCode { get; set; }
        public DateTime DateTime { get; set; }
        public string Reference { get; set; }
        public string RecordStatus { get; set; }
        public string EmailStatus { get; set; }
        public string SMSStatus { get; set; }
        public string WorkflowTaskCode { get; set; }
        public string SystemNotificationStatus { get; set; }
        public int UserID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PortCode { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public string EmailAddress { get; set; }
        public string NotificationTemplateBase { get; set; }

    }
}
