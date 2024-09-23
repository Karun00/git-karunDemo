using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.ValueObjects;
using System.Globalization;


namespace IPMS.Web.Api
{
    public class MobileController : ApiControllerBase
    {
        IMobileService _mobileservice;
        IQuayService _quayService;
        // IFileService _fileservice;
        public MobileController()
        {
            _mobileservice = new MobileClient();
            _quayService = new QuayClient();
            // _fileservice = new FileClient();
        }

        /// <summary>
        /// To get the mobile module list
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of mobile modules</returns>
        public HttpResponseMessage GetModules(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<MobileModuleVO> modules = null;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    modules = _mobileservice.GetModulesForMobile();
                }

                response = request.CreateResponse<IEnumerable<MobileModuleVO>>(HttpStatusCode.OK, modules);
                return response;
            });
        }

        /// <summary>
        /// This function will return the list of new and old notifications
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the list of notifications</returns>
        public HttpResponseMessage GetNotifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<SystemNotificationVO> notifications = null;
                    using (IMobileService _mobileservice = new MobileClient())
                    {
                        notifications = _mobileservice.GetNotifications();
                    }
                    response = request.CreateResponse<IEnumerable<SystemNotificationVO>>(HttpStatusCode.OK, notifications);
                }

                return response;
            });
        }

        /// <summary>
        /// This function is used to change the notification status
        /// </summary>
        /// <param name="request"></param>
        /// <param name="notificationData"></param>
        /// <returns>returns NotificationId</returns>
        public HttpResponseMessage PutNotifications(HttpRequestMessage request, SystemNotification notificationData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int notificationId = 0;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    notificationId = _mobileservice.ModifyNotifications(notificationData);
                }
                response = request.CreateResponse<int>(HttpStatusCode.OK, notificationId);
                return response;
            });
        }

        /// <summary>
        /// This function is used to change the notification status
        /// </summary>
        /// <param name="request"></param>
        /// <param name="notificationData"></param>
        /// <returns>returns NotificationId</returns>
        public HttpResponseMessage PutNotificationsByID(HttpRequestMessage request, SystemNotificationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int notificationId = value.NotificationId;
                int returnVal = 0;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    returnVal = _mobileservice.ModifyNotificationsByID(notificationId.ToString(CultureInfo.InvariantCulture));
                }
                response = request.CreateResponse<int>(HttpStatusCode.OK, returnVal);
                return response;
            });
        }

        /// <summary>
        /// This function is used to return a features entity
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Features entity will return</returns>
        public HttpResponseMessage GetFeatures(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<EntityVO> entities = null;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    entities = _mobileservice.GetFeatures();
                }
                response = request.CreateResponse<IEnumerable<EntityVO>>(HttpStatusCode.OK, entities);
                return response;
            });
        }

        /// <summary>
        /// To get the quays with berth details
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns list of Quays with berhts</returns>
        [HttpGet]
        public HttpResponseMessage GetQuaynames(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> quaynames = null;
                using (IQuayService _quayService = new QuayClient())
                {
                    quaynames = _quayService.GetQuaysWithBerthsMobile();
                }

                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quaynames);
                return response;


            });
        }

        /// <summary>
        /// This function will return the list of new notifications
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the list of new notifications</returns>
        public HttpResponseMessage GetNewNotifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<SystemNotificationVO> newNotifications = null;
                    using (IMobileService _mobileservice = new MobileClient())
                    {
                        newNotifications = _mobileservice.GetNewNotifications();
                    }
                    response = request.CreateResponse<IEnumerable<SystemNotificationVO>>(HttpStatusCode.OK, newNotifications);
                }

                return response;
            });
        }


        public HttpResponseMessage GetPlannedMovements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<PlannedMovementsVO> plannedMovements = null;
                    using (IMobileService _mobileservice = new MobileClient())
                    {
                        plannedMovements = _mobileservice.GetPlannedMovements();
                    }
                    response = request.CreateResponse<IEnumerable<PlannedMovementsVO>>(HttpStatusCode.OK, plannedMovements);
                }

                return response;
            });
        }
        [AllowAnonymous]
        [Route("api/Mobile/GetPlannedMovementsForDesktop/{PortCode}")]
        [HttpGet]
        public HttpResponseMessage GetPlannedMovementsForDesktop(HttpRequestMessage request, string PortCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<PlannedMovementsVO> plannedMovements = null;
                    using (IMobileService _mobileservice = new MobileClient())
                    {
                        plannedMovements = _mobileservice.GetPlannedMovementsForDesktop(PortCode);
                    }
                    response = request.CreateResponse<IEnumerable<PlannedMovementsVO>>(HttpStatusCode.OK, plannedMovements);
                }

                return response;
            });
        }

        [Route("api/GetPlannedMovementsForAnonymous/{portCode}")]
        [HttpGet]
        public HttpResponseMessage GetPlannedMovementsForAnonymous(HttpRequestMessage request, string portCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<PlannedMovementsVO> plannedMovements = null;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    plannedMovements = _mobileservice.GetPlannedMovementsForAnonymous(portCode);
                }
                response = request.CreateResponse<IEnumerable<PlannedMovementsVO>>(HttpStatusCode.OK, plannedMovements);

                return response;
            });
        }


        [Route("api/GetVCNStatusForMobApp/{VCN}")]
        [HttpGet]
        public HttpResponseMessage GetVCNStatusForMobApp(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrvNotfMobileAppVo> arrmob = null;
                using (IMobileService _mobileservice = new MobileClient())
                {
                    arrmob = _mobileservice.GetVCNStatusForMobApp(VCN);
                }
                response = request.CreateResponse<List<ArrvNotfMobileAppVo>>(HttpStatusCode.OK, arrmob);

                return response;
            });
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mobileservice.Dispose();
                _quayService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
