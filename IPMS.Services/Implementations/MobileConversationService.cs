using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MobileConversationService : ServiceBase, IMobileConversationService
    {

        public MobileConversationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public MobileConversationService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// To get the list of users
        /// </summary>
        /// <returns> List<UserMasterVO> list of users </returns>
        public List<UserMasterVO> GetUsers(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userid = new SqlParameter("@userid", _UserId);
                var portCode = new SqlParameter("@portcode", _PortCode);

                var users = _unitOfWork.SqlQuery<UserMasterVO>("dbo.usp_GetUsers @userid, @portcode", userid, portCode).Where(x => x.FirstName.ToUpperInvariant().Contains(searchValue.ToUpperInvariant())).ToList();

                return users;
            });
        }

        /// <summary>
        /// To get the list of conversation users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> List<UserConversationVO> list of conversation users</returns>
        public List<UserConversationVO> GetConversationUsers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userid = new SqlParameter("@userid", _UserId);

                var conversations = _unitOfWork.SqlQuery<UserConversationVO>("dbo.usp_GetConversationUsers  @userid", userid).ToList();


                return conversations;
            });
        }


        /// <summary>
        /// To get the list of conversations
        /// </summary>
        /// <param name="ConversationID"></param>
        /// <param name="UserID"></param>
        /// <returns> List<ConversationReplyVO> list of conversations</returns>
        public List<ConversationReplyVO> GetConversations(int conversationId, int userId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                _unitOfWork.ExecuteSqlCommand(" update dbo.ConversationReply SET IsRead = 'Y' WHERE IsRead='N' and ConversationID = @p0 and  UserID = @p1 ", conversationId, userId);

                var conversations = (from c in _unitOfWork.Repository<ConversationReply>().Query().Include(u => u.User2).Select()
                                     where c.ConversationID == conversationId
                                     orderby c.ConversationReplyID
                                     select c).ToList();

                if (conversations.Count == 0)
                {

                    List<ConversationReply> rvo = new List<ConversationReply>();


                    rvo.Add(new ConversationReply()
                    {
                        ConversationID = conversationId
                    });

                    conversations = rvo;
                }

                return conversations.MapToDTO();
            });
        }

        /// <summary>
        /// To add a conversation 
        /// </summary>
        /// <param name="value"></param>
        /// <returns> ConversationVO </returns>
        public ConversationVO AddConversation(ConversationVO value)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                value.CreatedBy = _UserId;
                value.ModifiedBy = _UserId;
                value.UserID1 = _UserId;
                Conversation conversation = null;
                conversation = value.MapToEntity();
                conversation.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Conversation>().Insert(conversation);
                _unitOfWork.SaveChanges();

                return value;
            });
        }


        /// <summary>
        /// To add a conversation reply
        /// </summary>
        /// <param name="value"></param>
        /// <returns>it will return id</returns>
        public int AddConversationReply(ConversationReplyVO value)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                value.CreatedBy = _UserId;
                value.ModifiedBy = _UserId;
                value.UserID = _UserId;
                ConversationReply conversationReply = null;
                conversationReply = value.MapToEntity();

                conversationReply.ObjectState = ObjectState.Added;
                conversationReply.IsRead = "N";
                _unitOfWork.Repository<ConversationReply>().Insert(conversationReply);
                _unitOfWork.SaveChanges();

                return value.ConversationReplyID;
            });
        }

        /// <summary>
        ///  To get the list of new messages
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserConversationVO</returns>
        public List<UserConversationVO> GetNewMessages()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userid = new SqlParameter("@userid", _UserId);

                var messages = _unitOfWork.SqlQuery<UserConversationVO>("dbo.usp_GetNewMessages  @userid", userid).ToList();

                return messages;
            });
        }

        /// <summary>
        /// To modify the conversation reply
        /// </summary>
        /// <param name="ConversationID"></param>
        /// <param name="UserID"></param>
        /// <returns>it will return a conversation id</returns>
        public int modifyConversationReply(int ConversationID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                _unitOfWork.ExecuteSqlCommand(" update dbo.ConversationReply SET IsRead = 'Y' WHERE IsRead='N' and ConversationID = @p0 and  UserID != @p1 ", ConversationID, _UserId);
                return ConversationID;
            });
        }
    }

}
