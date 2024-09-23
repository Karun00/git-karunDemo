using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class ElectronicNotificationsController : ApiControllerBase
    {

        IElectronicNotificationsService _electronicnotificationservice;
       
//        ILog log = log4net.LogManager.GetLogger(typeof(ElectronicNotificationsController));

        public ElectronicNotificationsController()
        {
            _electronicnotificationservice = new ElectronicNotificationsClient();
          
        }
        [Route("api/ElectronicNotifications")]        
        [HttpPost]
        public HttpResponseMessage PostNotificationsData(HttpRequestMessage request, NotificationTemplate value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    //var userId = 1;
                    //if (userId == -1)
                    //    return response;
                    //value.CreatedDate = DateTime.Now;
                    //value.CreatedBy = userId;
                    //value.ModifiedBy = userId;
                    //value.ModifiedDate = DateTime.Now;
                    string notificationCreated = _electronicnotificationservice.AddNotification(value);
                    response = request.CreateResponse<string>(HttpStatusCode.Created, notificationCreated);
                }
                return response;
            });
        }
        [Route("api/GetEntities")]
        [HttpGet]
        public HttpResponseMessage GetEntities(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Entity> entities = _electronicnotificationservice.GetEntityDetails();
                response = request.CreateResponse<List<Entity>>(HttpStatusCode.OK, entities);
                return response;
            });
        }
        [Route("api/GetTokens")]
        [HttpGet]
        public HttpResponseMessage GetTokens(HttpRequestMessage request, int entityID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<string> tokens = _electronicnotificationservice.GetTokens(entityID);
                response = request.CreateResponse<List<string>>(HttpStatusCode.OK, tokens);
                return response;
            });
        }
        [Route("api/GetRoles")]
        [HttpGet]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Role> roles = _electronicnotificationservice.GetRolesDetails();
                response = request.CreateResponse<List<Role>>(HttpStatusCode.OK, roles);
                return response;
            });
        }
        [Route("api/GetPorts")]
        [HttpGet]
        public HttpResponseMessage GetPorts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Port> ports = _electronicnotificationservice.GetPortsDetails();
                response = request.CreateResponse<List<Port>>(HttpStatusCode.OK, ports);
                return response;
            });
        }
        [Route("api/ElectronicNotifications")]   
        [HttpGet]
        public HttpResponseMessage GetAllNotifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<NotificationDetails> notifications = _electronicnotificationservice.GetNotifications();
                response = request.CreateResponse<List<NotificationDetails>>(HttpStatusCode.OK, notifications);
                return response;
            });
        }

        [Route("api/GetElectronicNotifications")]
        [HttpGet]
        public HttpResponseMessage GetNotifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<NotificationTemplateVO> notifications = _electronicnotificationservice.GetAllNotifications();
                response = request.CreateResponse<List<NotificationTemplateVO>>(HttpStatusCode.OK, notifications);
                return response;
            });
        }

        [Route("api/ElectronicNotifications")]
        [HttpDelete]
        public HttpResponseMessage PutDeleteNotification(HttpRequestMessage request, NotificationTemplate value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;
                string notification = _electronicnotificationservice.DeleteNotification(value);
                response = request.CreateResponse<string>(HttpStatusCode.OK, notification);
                return response;
            });
        }

        [Route("api/ElectronicNotifications")]
        [HttpPut]
        public HttpResponseMessage ModifyNotificationData(HttpRequestMessage request, NotificationTemplate value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //value.CreatedDate = DateTime.Now;
                //value.CreatedBy = 1;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;
             
                string notificationModified = _electronicnotificationservice.ModifyNotification(value);
                response = request.CreateResponse<string>(HttpStatusCode.Created, notificationModified);
                return response;
            });
        }
        [Route("api/GetWorkflowStatus")]
        [HttpGet]
        public HttpResponseMessage GetWorkflowStatus(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> CargoHanding = _electronicnotificationservice.GetWorkflowStatus();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, CargoHanding);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _electronicnotificationservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
