using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Conversation : EntityBase
    {
        public Conversation()
        {
            this.ConversationReplies = new List<ConversationReply>();
        }
        [DataMember]
        public int ConversationID { get; set; }
        [DataMember]
        public int UserID1 { get; set; }
        [DataMember]
        public int UserID2 { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  User User2 { get; set; }
        [DataMember]
        public  User User3 { get; set; }
        [DataMember]
        public  ICollection<ConversationReply> ConversationReplies { get; set; }
    }
}
