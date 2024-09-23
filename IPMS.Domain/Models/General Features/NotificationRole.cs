using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class NotificationRole : EntityBase
    {
        [DataMember]
        public int NotificationRoleID { get; set; }
        [DataMember]
        public string NotificationTemplateCode { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual NotificationTemplate NotificationTemplate { get; set; }
        [DataMember]
        public virtual Role Role { get; set; }
    }
}
