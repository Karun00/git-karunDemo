
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for ConversationReply
    /// </summary>
    public class ConversationReplyVO
    {
        public int ConversationReplyID { get; set; }
        public int ConversationID { get; set; }
        public int UserID { get; set; }
        public string IPAddress { get; set; }
        public string Reply { get; set; }
        public string IsRead { get; set; }
        public string RecordStatus { get; set; }
        public string FirstName { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public ConversationVO Conversation { get; set; }
        public UserMasterVO User { get; set; }
    }
}
