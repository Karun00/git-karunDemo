using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class CraftOutOfCommissionController : ApiControllerBase
    {
        ICraftOutOfCommissionService _CraftOutOfCommissionService;

        public CraftOutOfCommissionController()
        {
            _CraftOutOfCommissionService = new CraftOutOfCommissionClient();
        }

        [Authorize]
        [Route("api/CraftOutOfCommissionDetails")]
        [HttpGet]
        public HttpResponseMessage CraftOutOfCommissionDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftOutOfCommissionVO> CraftOutOfCommission = _CraftOutOfCommissionService.CraftOutOfCommissionDetails();
                response = request.CreateResponse<List<CraftOutOfCommissionVO>>(HttpStatusCode.OK, CraftOutOfCommission);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftInCommissionDetails")]
        [HttpGet]
        public HttpResponseMessage CraftInCommissionDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftOutOfCommissionVO> CraftInCommission = _CraftOutOfCommissionService.CraftInCommissionDetails();
                response = request.CreateResponse<List<CraftOutOfCommissionVO>>(HttpStatusCode.OK, CraftInCommission);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftOutOfCommission")]
        [HttpPost]
        //To save Data
        public HttpResponseMessage PostCraftOutOfCommData(HttpRequestMessage request, CraftOutOfCommissionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CraftOutOfCommissionVO craftoutofcommCreated = _CraftOutOfCommissionService.AddCraftOutOfCommission(value);
                response = request.CreateResponse<CraftOutOfCommissionVO>(HttpStatusCode.Created, craftoutofcommCreated);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftOutOfCommission")]
        [HttpPut]
        //To update CraftOutOfComm Data
        public HttpResponseMessage ModifyCraftOutOfCommData(HttpRequestMessage request, CraftOutOfCommissionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CraftOutOfCommissionVO craftoutofcommModified = _CraftOutOfCommissionService.ModifyCraftOutOfCommission(value);
                response = request.CreateResponse<CraftOutOfCommissionVO>(HttpStatusCode.Created, craftoutofcommModified);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftInCommission")]
        [HttpPut]
        //To update CraftInComm Data
        public HttpResponseMessage ModifyCraftInCommData(HttpRequestMessage request, CraftOutOfCommissionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CraftOutOfCommissionVO craftincommModified = _CraftOutOfCommissionService.ModifyCraftInCommission(value);
                response = request.CreateResponse<CraftOutOfCommissionVO>(HttpStatusCode.Created, craftincommModified);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftDetailsForOutofComm")]
        [HttpGet]
        public HttpResponseMessage GetCraftDetailsForOutofComm(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftVO> Crafts = _CraftOutOfCommissionService.CraftsDetails();
                response = request.CreateResponse<List<CraftVO>>(HttpStatusCode.OK, Crafts);
                return response;
            });
        }

        [Authorize]
        [Route("api/CraftDetailsForOutofComm/{CraftID}")]
        [HttpGet]
        public HttpResponseMessage GetCraftDetailsWithCraftID(HttpRequestMessage request, int craftId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftVO> Crafts = _CraftOutOfCommissionService.CraftsDetailsWithCraftId(craftId);
                response = request.CreateResponse<List<CraftVO>>(HttpStatusCode.OK, Crafts);
                return response;
            });
        }

        [Authorize]
        [Route("api/GetReasonforOutofCommDetails/{ReasonCode}")]
        [HttpGet]
        public HttpResponseMessage GetReasonforOutofCommDetails(HttpRequestMessage request, string reasonCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> ReasonforOutofComm = _CraftOutOfCommissionService.ReasonForOutOfCommissionDetails(reasonCode);
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, ReasonforOutofComm);
                return response;
            });
        }

        [Authorize]
        [Route("api/GetCommStatusDetails/{Status}")]
        [HttpGet]
        public HttpResponseMessage GetCommStatusDetails(HttpRequestMessage request, string Status)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> ReasonforOutofComm = _CraftOutOfCommissionService.CommissionStatusDetails(Status);
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, ReasonforOutofComm);
                return response;
            });
        }
    }
}