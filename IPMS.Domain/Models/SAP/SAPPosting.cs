using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SAPPosting : EntityBase
    {
        //public SAPPosting()
        //{
        //    this.SAPSystemNotifications = new List<SystemNotification>();
        //}

        [DataMember]
        public int SAPPostingID { get; set; }
        [DataMember]
        public string MessageType { get; set; }
        [DataMember]
        public string NotificationTemplateCode { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public string PostingStatus { get; set; }
        [DataMember]
        public string TransmitData { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string SAPReferenceNo { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string EmailStatus { get; set; }
        [DataMember]
        public string SMSStatus { get; set; }
        [DataMember]
        public string SystemNotificationStatus { get; set; }
        [DataMember]
        public Nullable<int> UserTypeId { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public virtual NotificationTemplate SAPNotificationTemplate { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory1 { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory2 { get; set; }

        [DataMember]
        public string RevinueAccountNo { get; set; }

        [DataMember]
        public string RevenueAgentAccNo { get; set; }
        

         [DataMember]
        public int MarinePostingId { get; set; }
        

    }
}
