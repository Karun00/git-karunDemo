using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using IPMS.Web.Adapters;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using WebMatrix.WebData;
using System.Linq;
using System.Web;
using IPMS.ServiceProxies;
using System.Configuration;
using log4net;

namespace IPMS.Web.API
{

    public class PortEntryPassApplicationController : ApiControllerBase
    {
        IPortEntryPassApplicationService _portentrypassapplicationservice;
        //ISecurityAdapter _SecurityAdapter;
        ILog log = log4net.LogManager.GetLogger(typeof(PortEntryPassApplicationController));
        public PortEntryPassApplicationController()
        {
            _portentrypassapplicationservice = new PortEntryPassApplicationServiceClient();

        }


        [Route("api/PortEntryPassReferenceData")]
        public HttpResponseMessage GetPortEntryPassReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PortEntryPassApplicationReferenceVO Referencedata = _portentrypassapplicationservice.GetPortEntryPassReferenceData();

                response = request.CreateResponse<PortEntryPassApplicationReferenceVO>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }



        [Route("api/ApprovedPermitrequestlist")]
        public HttpResponseMessage GetApprovedPermitrequestlist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<PermitRequestVO> Referencedata = _portentrypassapplicationservice.GetApprovedPermitrequestlist();

                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }


        [HttpPost]
        [Route("api/ApprovedPermitrequestlistSearch")]
        public HttpResponseMessage GetApprovedPermitrequestlistSearch(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                
                List<PermitRequestVO> Referencedata = _portentrypassapplicationservice.GetApprovedPermitrequestlistSearch(Searchmdl);

                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }

        [Route("api/InternalEmployeePermitrequestlist")]
        public HttpResponseMessage GetInternalEmployeePermitrequestlist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<PermitRequestVO> Referencedata = _portentrypassapplicationservice.GetInternalEmployeePermitlist();

                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }
        [Route("api/PortEntryPassApplication/GetSubAccessAreasForRB/{supCatCode}")]
        [HttpGet]
        public HttpResponseMessage GetSubAccessAreasForRB(HttpRequestMessage request, string supCatCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<SubCategoryCodeNameWithSupCatCodeVO> Referencedata = _portentrypassapplicationservice.GetSubAccessAreasForRB(supCatCode);

                response = request.CreateResponse<List<SubCategoryCodeNameWithSupCatCodeVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }

        //Areas for portentrypass

        [Route("api/GetAreas/{supCatCode}")]
        [HttpGet]
        public HttpResponseMessage GetAreas(HttpRequestMessage request, string supCatCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<SubCategoryCodeNameWithSupCatCodeVO> Referencedata = _portentrypassapplicationservice.GetAreas(supCatCode);

                response = request.CreateResponse<List<SubCategoryCodeNameWithSupCatCodeVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }


        [HttpPost]
        [Route("api/PermitRequest")]
        public HttpResponseMessage PostAddPortEntryPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            log.Debug("Controller Started");
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                    log.Debug("if is authenticated");
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //PilotExemptionRequestData.ModifiedBy = userId;
                    //PilotExemptionRequestData.CreatedBy = userId;
                }
                else
                {
                    log.Debug("if is not authenticated");
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);
                    log.Debug("anonymousUserId Test" + anonymousUserId);

                    log.Debug("anonymousUserId Test1" + anonymousUserId);
                    permitrequestdata.ModifiedBy = anonymousUserId; // 1;
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                log.Debug("calling service start");
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.AddPortEntryPass(permitrequestdata);
                log.Debug("service end in controller");
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpPut]
        [Route("api/PermitRequest")]
        public HttpResponseMessage PutEditPortEntryPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //PilotExemptionRequestData.ModifiedBy = userId;
                    //PilotExemptionRequestData.CreatedBy = userId;
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    permitrequestdata.ModifiedBy = anonymousUserId; // 1; 
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.EditPortEntryPass(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpGet]
        [Route("api/PermitRequestList")]
        public HttpResponseMessage PortEntryPassRequestList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlist();
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }
        [HttpPost]
        [Route("api/GetPortEntryPassRequestlistSearch")]
        public HttpResponseMessage PortEntryPassRequestList(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlistSearch(Searchmdl);
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpPost]
        [Route("api/InternalEmployeePermitrequestlistSearch")]
        public HttpResponseMessage GetInternalEmployeePermitrequestlistSearch(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<PermitRequestVO> Referencedata = _portentrypassapplicationservice.GetInternalEmployeePermitlistSearch(Searchmdl);

                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }


        [HttpGet]
        [Route("api/InternalEmployeetobeapprovedPermitlist")]
        public HttpResponseMessage GetInternalEmployeetobeapprovedPermitlist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetInternalEmployeetobeapprovedPermitlist();
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpGet]
        [Route("api/PermitRequestListForSSA")]
        public HttpResponseMessage PortEntryPassRequestListForSsa(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlistForSsa();
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpPost]
        [Route("api/InternalEmployeetobeapprovedPermitlistSearch")]
        public HttpResponseMessage GetInternalEmployeetobeapprovedPermitlistSearch(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetInternalEmployeetobeapprovedPermitlistSearch(Searchmdl);
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpPost]
        [Route("api/PermitRequestListForSSASearch")]
        public HttpResponseMessage PortEntryPassRequestListForSsaSearch(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlistForSsaSearch(Searchmdl);
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpPost]
        [Route("api/PermitRequestListForSAPSSearch")]
        public HttpResponseMessage PortEntryPassRequestListForSapsSearch(HttpRequestMessage request, PermitRequestSearchVO Searchmdl)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlistForSapsSearch(Searchmdl);
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpGet]
        [Route("api/PermitRequestListForSAPS")]
        public HttpResponseMessage PortEntryPassRequestListForSaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<PermitRequestVO> PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequestlistForSaps();
                response = request.CreateResponse<List<PermitRequestVO>>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpGet]
        [Route("api/PermitRequestbyid")]
        public HttpResponseMessage GetPortEntryPassRequest(HttpRequestMessage request, string refrenceNumber, int flag, string portcode)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.GetPortEntryPassRequest(refrenceNumber, flag, portcode);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpGet]
        [Route("api/SSAandSAPSStatusbyid")]
        public HttpResponseMessage GetvalidatePortEntryPassRequestforSsasaps(HttpRequestMessage request, int id, string flag)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int status = _portentrypassapplicationservice.GetvalidatePortEntryPassRequestforSsasaps(id, flag);
                response = request.CreateResponse<int>(HttpStatusCode.OK, status);
                return response;
            });
        }


        [HttpPost]
        [Route("api/PermitRequestupdate")]
        public HttpResponseMessage PostPortEntryPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.UpdateRecodeWithComments(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }
        [HttpPost]
        [Route("api/PermitRequestforword")]
        public HttpResponseMessage PostforwordPortEntryPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.ForwordRecodeWithComments(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        [HttpPost]
        [Route("api/ADDPermitRequest")]
        public HttpResponseMessage PostAddSsaportentrypass(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    permitrequestdata.ModifiedBy = anonymousUserId; // 1;
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.AddverificationDetails(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpPost]
        [Route("api/ApprovePermitRequest")]
        public HttpResponseMessage PostApprovePortEntryPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //PilotExemptionRequestData.ModifiedBy = userId;
                    //PilotExemptionRequestData.CreatedBy = userId;
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    permitrequestdata.ModifiedBy = anonymousUserId; // 1;
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.ApprovalDenyPortEntryPass(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }


        [HttpPost]
        [Route("api/AppealApprovePermitRequest")]
        public HttpResponseMessage PostAppealApproveDenyPassRequest(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.AppealApproveDenyPortEntryPass(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }



        [HttpPost]
        [Route("api/AddInternalEmployeePermit")]
        public HttpResponseMessage PostInternalEmployeePermit(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.AddInternalEmployeePermitDetails(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }
        [HttpPost]
        [Route("api/IssuePortEntryPass")]
        public HttpResponseMessage PostIssuePortEntryPass(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    permitrequestdata.ModifiedBy = anonymousUserId; // 1;
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.IssuePortEntryPass(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }
        [HttpPost]
        [Route("api/ApproveInternalEmployeePermit")]
        public HttpResponseMessage PostApproveInternalEmployeePermit(HttpRequestMessage request, PermitRequestVO permitrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (User.Identity.IsAuthenticated)
                {
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    permitrequestdata.ModifiedBy = anonymousUserId; // 1;
                    permitrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PermitRequestVO PermitRequest = _portentrypassapplicationservice.ApproveInternalEmployeePermitDetails(permitrequestdata);
                response = request.CreateResponse<PermitRequestVO>(HttpStatusCode.OK, PermitRequest);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _portentrypassapplicationservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
