using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class MobileConversationClient : UserClientBase<IMobileConversationService>, IMobileConversationService
    {
        public List<UserMasterVO> GetUsers(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetUsers(searchValue));
        }

        public ConversationVO AddConversation(ConversationVO value)
        {
            return WrapOperationWithException(() => Channel.AddConversation(value));
        }

        public List<UserConversationVO> GetConversationUsers()
        {
            return WrapOperationWithException(() => Channel.GetConversationUsers());
        }

        public List<ConversationReplyVO> GetConversations(int conversationId, int userId)
        {
            return WrapOperationWithException(() => Channel.GetConversations(conversationId, userId));
        }

        public int AddConversationReply(ConversationReplyVO value)
        {
            return WrapOperationWithException(() => Channel.AddConversationReply(value));
        }

        public List<UserConversationVO> GetNewMessages()
        {
            return WrapOperationWithException(() => Channel.GetNewMessages());
        }

        public int modifyConversationReply(int ConversationID)
        {
            return WrapOperationWithException(() => Channel.modifyConversationReply(ConversationID));
        }
    }
}
