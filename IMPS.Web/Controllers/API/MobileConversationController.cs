using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Linq;

namespace IPMS.Web.Api
{
    public class MobileConversationController : ApiControllerBase
    {


        /// <summary>
        /// To get the list of users
        /// </summary>
        /// <param name="request"></param>
        /// <returns> returnse a response</returns>
        [HttpGet]
        public HttpResponseMessage GetUsers(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {


                HttpResponseMessage response = null;

                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];

                List<UserMasterVO> users = null;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    users = _mobileservice.GetUsers(searchValue);
                }
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, users);
                return response;

                //HttpResponseMessage response = null;
                //List<UserMasterVO> users = null;
                //using (IMobileConversationService _mobileservice = new MobileConversationClient())
                //{
                //    users = _mobileservice.GetUsers();
                //}

                //response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, users);
                //return response;            

            });
        }

        /// <summary>
        /// To get the list of conversation users
        /// </summary>
        /// <param name="request"></param>
        /// <returns>returnse a response</returns>
        [HttpGet]
        public HttpResponseMessage GetConversationUsers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<UserConversationVO> users = null;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    users = _mobileservice.GetConversationUsers();
                }

                response = request.CreateResponse<List<UserConversationVO>>(HttpStatusCode.OK, users);
                return response;
            });
        }

        /// <summary>
        /// To get the list of conversations
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ConversationID"></param>
        /// <param name="UserID"></param>
        /// <returns>returnse a response</returns>
        [HttpGet]
        public HttpResponseMessage GetConversations(HttpRequestMessage request, int ConversationID, int UserID)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ConversationReplyVO> conversations = null;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    conversations = _mobileservice.GetConversations(ConversationID, UserID);
                }

                response = request.CreateResponse<List<ConversationReplyVO>>(HttpStatusCode.OK, conversations);
                return response;
            });
        }

        /// <summary>
        /// To add a conversation 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns>returnse a response</returns>
        [HttpPost]
        public HttpResponseMessage AddConversation(HttpRequestMessage request, ConversationVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.RecordStatus = "A";
                ConversationVO conversationCreated = null;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    conversationCreated = _mobileservice.AddConversation(value);
                }

                response = request.CreateResponse<ConversationVO>(HttpStatusCode.OK, conversationCreated);
                return response;
            });
        }

        /// <summary>
        ///  To add a conversation reply
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns>returnse a response</returns>
        [HttpPost]
        public HttpResponseMessage AddConversationReply(HttpRequestMessage request, ConversationReplyVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.RecordStatus = "A";
                int conversationID = 0;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    conversationID = _mobileservice.AddConversationReply(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, conversationID);
                return response;
            });
        }

        /// <summary>
        /// To get the list of new messages
        /// </summary>
        /// <param name="request"></param>
        /// <returns>returnse a response</returns>
        [HttpGet]
        public HttpResponseMessage GetNewMessages(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserConversationVO> messages = null;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    messages = _mobileservice.GetNewMessages();
                }

                response = request.CreateResponse<List<UserConversationVO>>(HttpStatusCode.OK, messages);
                return response;
            });
        }

        /// <summary>
        /// To modify the conversation reply
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ConversationID"></param>
        /// <returns>returnse a response</returns>
        [HttpGet]
        public HttpResponseMessage GetConversationReply(HttpRequestMessage request, int ConversationID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int conversationId = 0;
                using (IMobileConversationService _mobileservice = new MobileConversationClient())
                {
                    conversationId = _mobileservice.modifyConversationReply(ConversationID);
                }
                response = request.CreateResponse<int>(HttpStatusCode.OK, conversationId);
                return response;
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
