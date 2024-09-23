using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ConversationMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="conversations"></param>
        /// <returns></returns>
        public static List<ConversationVO> MapToDTO(this List<Conversation> conversations)
        {
            List<ConversationVO> conversationVos = new List<ConversationVO>();
            if (conversations != null)
            {
                foreach (var item in conversations)
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
        public static List<Conversation> MapToEntity(this List<ConversationVO> conversationVos)
        {
            List<Conversation> conversations = new List<Conversation>();
            if (conversationVos != null)
            {
                foreach (var item in conversationVos)
                {
                    conversations.Add(item.MapToEntity());
                }
            }
            return conversations;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ConversationVO MapToDTO(this Conversation data)
        {
            ConversationVO conversationVO = new ConversationVO();
            if (data != null)
            {
                conversationVO.ConversationID = data.ConversationID;
                conversationVO.CreatedBy = data.CreatedBy;
                conversationVO.CreatedDate = data.CreatedDate;
                conversationVO.ModifiedBy = data.ModifiedBy;
                conversationVO.ModifiedDate = data.ModifiedDate;
                conversationVO.RecordStatus = data.RecordStatus;
                conversationVO.ConversationReplies = data.ConversationReplies.MapToDTO();
                conversationVO.IPAddress = data.IPAddress;
                conversationVO.User = data.User.MapToDTO();
                conversationVO.UserID1 = data.UserID1;
                conversationVO.UserID2 = data.UserID2;
            }
            return conversationVO;

        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Conversation MapToEntity(this ConversationVO vo)
        {
            Conversation conversation = new Conversation();
            if (vo != null)
            {
                conversation.ConversationID = vo.ConversationID;
                conversation.CreatedBy = vo.CreatedBy;
                conversation.CreatedDate = vo.CreatedDate;
                conversation.ModifiedBy = vo.ModifiedBy;
                conversation.ModifiedDate = vo.ModifiedDate;
                conversation.RecordStatus = vo.RecordStatus;
                conversation.IPAddress = vo.IPAddress;
                conversation.UserID1 = vo.UserID1;
                conversation.UserID2 = vo.UserID2;
            }
            return conversation;
        }


    }
}
