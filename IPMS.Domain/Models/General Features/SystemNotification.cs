using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SystemNotification : EntityBase
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int NotificationId { get; set; }
        [DataMember]
        public string NotificationText { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string IsRead { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public virtual Notification Notification { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }

    }
}
