using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMobileConversationService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetUsers(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ConversationVO AddConversation(ConversationVO value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserConversationVO> GetConversationUsers();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ConversationReplyVO> GetConversations(int conversationId, int userId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int AddConversationReply(ConversationReplyVO value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserConversationVO> GetNewMessages();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int modifyConversationReply(int ConversationID);
    }
}
