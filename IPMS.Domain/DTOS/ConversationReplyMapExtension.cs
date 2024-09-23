using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ConversationReplyMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="conversationReplies"></param>
        /// <returns></returns>
        public static List<ConversationReplyVO> MapToDTO(this ICollection<ConversationReply> conversationReplies)
        {
            List<ConversationReplyVO> conversationVos = new List<ConversationReplyVO>();
            if (conversationReplies != null)
            {
                foreach (var item in conversationReplies)
                {
                    conversationVos.Add(item.MapToDTO());
                }
            }
            return conversationVos;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity 
        /// </summary>
        /// <param name="conversationVos"></param>
        /// <returns></returns>
        public static List<ConversationReply> MapToEntity(this List<ConversationReplyVO> conversationVos)
        {
            List<ConversationReply> conversationReplies = new List<ConversationReply>();
            if (conversationVos != null)
            {
                foreach (var item in conversationVos)
                {
                    conversationReplies.Add(item.MapToEntity());
                }
            }

            return conversationReplies;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ConversationReplyVO MapToDTO(this ConversationReply data)
        {
            ConversationReplyVO conversationReplyVO = new ConversationReplyVO();
            if (data != null)
            {
                conversationReplyVO.ConversationID = data.ConversationID;
                conversationReplyVO.ConversationReplyID = data.ConversationReplyID;
                conversationReplyVO.CreatedBy = data.CreatedBy;
                conversationReplyVO.CreatedDate = data.CreatedDate;
                conversationReplyVO.IPAddress = data.IPAddress;
                conversationReplyVO.ModifiedBy = data.ModifiedBy;
                conversationReplyVO.ModifiedDate = data.ModifiedDate;
                conversationReplyVO.RecordStatus = data.RecordStatus;
                conversationReplyVO.Reply = data.Reply;
                conversationReplyVO.IsRead = data.IsRead;

                conversationReplyVO.User = data.User != null ? data.User.MapToDTO() : null;
                conversationReplyVO.UserName = data.User2 != null ? data.User2.MapToDTO().UserName : null;
                conversationReplyVO.UserID = data.UserID;
                conversationReplyVO.FirstName = data.User2 != null
                    ? data.User2.MapToDTO().UserName + '-' + data.User2.MapToDTO().FirstName + ' ' +
                      data.User2.MapToDTO().LastName
                    : null;
            }
            return conversationReplyVO;

        }

        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static ConversationReply MapToEntity(this ConversationReplyVO vo)
        {
            ConversationReply conversation = new ConversationReply();
            if (vo != null)
            {
                conversation.ConversationID = vo.ConversationID;
                conversation.ConversationReplyID = vo.ConversationReplyID;
                conversation.CreatedBy = vo.CreatedBy;
                conversation.CreatedDate = vo.CreatedDate;
                conversation.IPAddress = vo.IPAddress;
                conversation.ModifiedBy = vo.ModifiedBy;
                conversation.ModifiedDate = vo.ModifiedDate;
                conversation.RecordStatus = vo.RecordStatus;
                conversation.Reply = vo.Reply;
                conversation.UserID = vo.UserID;
            }
            return conversation;
        }


    }
}
