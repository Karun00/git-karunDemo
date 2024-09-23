using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMobileConversationService : IDisposable
    {
        [OperationContract]
        List<UserMasterVO> GetUsers(string searchValue);

        [OperationContract]
        ConversationVO AddConversation(ConversationVO value);

        [OperationContract]
        List<UserConversationVO> GetConversationUsers();

        [OperationContract]
        List<ConversationReplyVO> GetConversations(int conversationId, int userId);

        [OperationContract]
        int AddConversationReply(ConversationReplyVO value);

        [OperationContract]
        List<UserConversationVO> GetNewMessages();

        [OperationContract]
        int modifyConversationReply(int ConversationID);
    }
}

