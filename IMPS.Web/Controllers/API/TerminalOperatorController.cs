using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace IPMS.Web.Controllers
{
    public class TerminalOperatorMasterController : ApiControllerBase
    {
        ITerminalOperatorService _terminaloparetorservice;

        IQuayService _quayService;

        public TerminalOperatorMasterController()
        {
            _terminaloparetorservice = new TerminalOperatorClient();
            _quayService = new QuayClient();
        }

        #region GetTerminalOperatorlist
        [Authorize]
        [Route("api/TerminalOperatorMaster")]
        [HttpGet]
        public HttpResponseMessage GetTerminalOperatorlist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = this.GetType().Name;
                    controllername = controllername.Replace("Controller", "");
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }
               
                if (privilege.Privileges.Contains("VIEW"))
                {
                    List<TerminalOperatorVO> TerminalOperator = _terminaloparetorservice.GetTerminalOperatorList();
                    response = request.CreateResponse<List<TerminalOperatorVO>>(HttpStatusCode.OK, TerminalOperator);
                }
                else
                {
                    //user = null;
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                }                                            
                return response;
            });
        }
        #endregion

        #region PostTerminalOperatorData
        [Authorize]
        [HttpPost]
        [Route("api/TerminalOperatorMaster")]
        public HttpResponseMessage PostTerminalOperatorData(HttpRequestMessage request, TerminalOperatorVO applicant)
        {
            return GetHttpResponse(request, () =>
            {
                bool isUpdate = false;
                if (applicant.TerminalOperatorID > 0)
                {
                    isUpdate = true;
                }

                HttpResponseMessage response = null;
                TerminalOperatorVO termincalOperatorId = null;
                if (User.Identity.IsAuthenticated && isUpdate == false)
                {
                }
                else
                {
                    applicant.CreatedBy = 1;//This id has to be set to anonymous user id.
                }
                applicant.TelephoneNo1 = applicant.TelephoneNo1.Replace("(", "").Replace(")", "").Replace("-", "");
                applicant.FaxNo = applicant.FaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                using (ITerminalOperatorService _terminaloparetorservice = new TerminalOperatorClient())
                {
                    termincalOperatorId = _terminaloparetorservice.AddTerminalOperator(applicant);
                }
            
                response = request.CreateResponse<TerminalOperatorVO>(HttpStatusCode.Created, termincalOperatorId);
                return response;
            });
        }
        #endregion

        #region GetQuaynames
        [Authorize]
        [Route("api/GetQuays")]
        [HttpGet]
        public HttpResponseMessage GetQuaynames(HttpRequestMessage request, string portCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> quaynames = _quayService.GetQuaysWithBerths(portCode);
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quaynames);
                return response;
            });
        }
        #endregion

        #region GetCargoTypes
        [Authorize]
        [HttpGet]
        [Route("api/CargoTypes")]
        public HttpResponseMessage GetCargoTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> CargoHanding = _terminaloparetorservice.GetCargoTypes();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, CargoHanding);
                return response;
            });
        }
        #endregion

        #region PutTerminalOperatorData
        [Authorize]
        [HttpPut]
        [Route("api/TerminalOperatorMaster")]
        public HttpResponseMessage PutTerminalOperatorData(HttpRequestMessage request, TerminalOperatorVO applicant)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                TerminalOperatorVO termincalOperatorId = null;
                if (User.Identity.IsAuthenticated == false)
                {
                }
                else
                {
                    applicant.CreatedBy = 1;//This id has to be set to anonymous user id.
                }

                applicant.TelephoneNo1 = applicant.TelephoneNo1.Replace("(", "").Replace(")", "").Replace("-", "");
                applicant.FaxNo = applicant.FaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                using (ITerminalOperatorService _terminaloparetorservice = new TerminalOperatorClient())
                {

                    termincalOperatorId = _terminaloparetorservice.ModifyTerminalOperator(applicant);
                }

                response = request.CreateResponse<TerminalOperatorVO>(HttpStatusCode.Created, termincalOperatorId);
                return response;
            });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _terminaloparetorservice.Dispose();
                _quayService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
