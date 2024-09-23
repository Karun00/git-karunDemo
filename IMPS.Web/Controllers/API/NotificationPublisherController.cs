using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Domain.Models;

namespace IPMS.Web.API
{
    public class NotificationPublisherController : ApiControllerBase
    {
        INotificationPublisherService _NotificationPublisher;



        public NotificationPublisherController()
        {
            _NotificationPublisher = new NotificationPublisherClient();

        }

        /// <summary>
        /// To get all Shift Data 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PushMessage")]
      //  [Authorize]
        [HttpPost]
        public HttpResponseMessage PushMessage(HttpRequestMessage request, NotificationVO notificationVO)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                 int entityId = notificationVO.EntityID;
                string reference = notificationVO.Reference;
                int userid =  notificationVO.UserID;
                string portcode = notificationVO.PortCode;
                string workFlowTaskCode = notificationVO.WorkflowTaskCode;
                CompanyVO company = new CompanyVO();
                company.UserType = notificationVO.UserType;
                company.UserTypeId = notificationVO.UserTypeId;

                bool retvalue = _NotificationPublisher.PushMessageToQueue(entityId, reference, userid, company, portcode, workFlowTaskCode);
                response = request.CreateResponse<bool>(HttpStatusCode.OK, retvalue);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _NotificationPublisher.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
