using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class RevenueStopListController : ApiControllerBase
    {
        IRevenueStopListService _revenuestoplistservice;
        public RevenueStopListController()
        {
            _revenuestoplistservice = new RevenueStopListService();
        }
        [Route("api/RevenueStoplistReference")]
        public HttpResponseMessage GetRevenueStoplistReferencedata(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenueStopReferenceDataVO ResourceAllocationConfigRuleReferences = _revenuestoplistservice.GetRevennueStopReferenceVO();

                response = request.CreateResponse<RevenueStopReferenceDataVO>(HttpStatusCode.OK, ResourceAllocationConfigRuleReferences);

                return response;
            });
        }

        [Route("api/Getagentdetails")]
        [HttpGet]
        public HttpResponseMessage Getallagentdetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenueStopListVO> allagentdetails = _revenuestoplistservice.GetAgentdetails(searchValue);

                response = request.CreateResponse<List<RevenueStopListVO>>(HttpStatusCode.OK, allagentdetails);

                return response;
            });
        }

        [Route("api/GetAllagentdetails")]
        [HttpGet]
        public HttpResponseMessage Getallgridagentdetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;      
                List<RevenueStopListVO> allagentdetails = _revenuestoplistservice.GetAllAgentsforgrid();
                response = request.CreateResponse<List<RevenueStopListVO>>(HttpStatusCode.OK, allagentdetails);

                return response;
            });
        }

        [Route("api/searchrevenuestopdata")]
        public HttpResponseMessage Getsearchrevenuestopdata(HttpRequestMessage request, string Agentid, string AgentName, string Accountno, string Accountstatus)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<RevenueStopListVO> searchrevenuestopdata = _revenuestoplistservice.SearchRevennueStop(Agentid, AgentName, Accountno, Accountstatus);

                response = request.CreateResponse<List<RevenueStopListVO>>(HttpStatusCode.OK, searchrevenuestopdata);

                return response;
            });
        }




        [Authorize]
        [Route("api/UpdateRevenueStop")]
        [HttpPost]
        public HttpResponseMessage PutRevenuestoplist(HttpRequestMessage request, RevenueStopListVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RevenueStopListVO revenue = _revenuestoplistservice.ModifyRevenueStop(data);
                response = request.CreateResponse<RevenueStopListVO>(HttpStatusCode.OK, revenue);

                return response;
            });
        }


        [Authorize]
        [Route("api/SaveRevenueStop")]
        [HttpPost]
        public HttpResponseMessage PostRevenuestoplist(HttpRequestMessage request, RevenueStopListVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RevenueStopListVO revenue = _revenuestoplistservice.AddRevenueStop(data);
                response = request.CreateResponse<RevenueStopListVO>(HttpStatusCode.OK, revenue);

                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _revenuestoplistservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

