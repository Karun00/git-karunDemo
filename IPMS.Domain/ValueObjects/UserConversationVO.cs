using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserConversationVO
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int ConversationID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Reply { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public string IsRead { get; set; }
        [DataMember]
        public int ConversationReplyID { get; set; }
    }
}
