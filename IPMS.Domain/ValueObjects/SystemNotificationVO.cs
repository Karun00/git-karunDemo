using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SystemNotificationVO
    {
        //--By Mahesh : To get system notifications in Header...................


        [DataMember]
        public string NotificationText { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string IsRead { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public string CreatedDate { get; set; }
        [DataMember]
        public int NotificationId { get; set; }


    }
}
