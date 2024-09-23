using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Conversation
    /// </summary>
    public class ConversationVO
    {
        public int ConversationID { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public string IPAddress { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public UserMasterVO User { get; set; }
        public List<ConversationReplyVO> ConversationReplies { get; set; }
    }
}
