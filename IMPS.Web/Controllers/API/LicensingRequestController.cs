using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class LicensingRequestController : ApiControllerBase
    {
        ILicensingRequestService _licensingRequestSerive;

        public LicensingRequestController()
        {
            _licensingRequestSerive = new LicensingRequestClient();

        }

        [Route("api/LicensingRequestList")]
        public HttpResponseMessage GetLicensingRequestList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LicenseRequestVO> licensingRequestlist = _licensingRequestSerive.GetLicensingRequestlist();
                response = request.CreateResponse<List<LicenseRequestVO>>(HttpStatusCode.OK, licensingRequestlist);
                return response;
            });
        }

        public HttpResponseMessage GetLicensingRequest(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LicenseRequestVO licensingRequest = _licensingRequestSerive.GetLicensingRequest(id);
                response = request.CreateResponse<LicenseRequestVO>(HttpStatusCode.OK, licensingRequest);
                return response;
            });
        }

        public HttpResponseMessage GetLicensingRequestbyreferenceid(HttpRequestMessage request, string id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LicenseRequestVO licensingRequest = _licensingRequestSerive.GetLicensingRequestbyreference(id);
                response = request.CreateResponse<LicenseRequestVO>(HttpStatusCode.OK, licensingRequest);
                return response;
            });
        }

        [HttpPost]
        [Route("api/LicensingRequest")]
        public HttpResponseMessage PostAddLicensingRequest(HttpRequestMessage request, LicenseRequestVO licensingrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LicenseRequestVO licensingRequest = null;
                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //LicensingRequestData.ModifiedBy = userId; 
                    //LicensingRequestData.CreatedBy = userId;
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    licensingrequestdata.CreatedBy = anonymousUserId; // 1;
                    licensingrequestdata.ModifiedBy = anonymousUserId; // 1;
                }

                using (ILicensingRequestService licensingRequestSerive = new LicensingRequestClient())
                {
                    licensingRequest = licensingRequestSerive.AddLicensingRequest(licensingrequestdata);
                }


                response = request.CreateResponse<LicenseRequestVO>(HttpStatusCode.OK, licensingRequest);
                return response;
            });
        }

        [Authorize]
        [Route("api/LicensingRequest")]
        [HttpPut]
        public HttpResponseMessage PutModifyLicensingRequest(HttpRequestMessage request, LicenseRequestVO licensingrequestdata)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                LicenseRequestVO licensingRequest = null;


                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //LicensingRequestData.ModifiedBy = userId;
                }

                using (ILicensingRequestService licensingRequestSerive = new LicensingRequestClient())
                {
                    licensingRequest = licensingRequestSerive.ModifyLicensingRequest(licensingrequestdata);
                }
                response = request.CreateResponse<LicenseRequestVO>(HttpStatusCode.OK, licensingRequest);
                return response;
            });
        }

        [Route("api/LicensingRequestTypes")]
        [HttpGet]
        public HttpResponseMessage GetLicensingRequestTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LicenseRequestReferenceVO licenserequestDetails = _licensingRequestSerive.GetLicenseRequestReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, licenserequestDetails);

                return response;
            });
        }

        [Route("api/CheckReferenceNoExists")]
        [HttpGet]
        public HttpResponseMessage CheckReferenceNoExists(HttpRequestMessage request, string referenceno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool licensingRequest = _licensingRequestSerive.CheckReferenceNoExists(referenceno);
                response = request.CreateResponse(HttpStatusCode.OK, licensingRequest);
                return response;
            });
        }

        #region Workflow Integrated Methods
        [HttpPost]
        public HttpResponseMessage ApproveLicenseRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ILicensingRequestService ls = new LicensingRequestClient())
                {
                    ls.ApproveLicenseRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage VerifyLicenseRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ILicensingRequestService ls = new LicensingRequestClient())
                {
                    ls.VerifyLicenseRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage RejectLicenseRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ILicensingRequestService ls = new LicensingRequestClient())
                {
                    ls.RejectLicenseRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _licensingRequestSerive.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
