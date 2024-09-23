using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ConversationReply : EntityBase
    {
        [DataMember]
        public int ConversationReplyID { get; set; }
        [DataMember]
        public int ConversationID { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public string Reply { get; set; }
        [DataMember]
        public string IsRead { get; set; }
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
        public virtual Conversation Conversation { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual User User2 { get; set; }
    }
}
