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


namespace IPMS.Web.Api
{
    public class MarineRevenueController : ApiControllerBase
    {
        private IMarineRevenueService _marineRevenueService;

        public MarineRevenueController()
        {
            _marineRevenueService = new MarineRevenueClient();
        }


        [HttpGet]
        public HttpResponseMessage GetMarineRevenueList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<RevenuePostingVO> revenuePostingVOs = _marineRevenueService.GetMarineRevenueList();

                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, revenuePostingVOs);

                return response;
            });
        }

        [Route("api/MarineRevenueDetails/{vcnSearch}/{vesselName}/{frmdate}/{todate}")]
        [HttpGet]
        public HttpResponseMessage GetMarineRevenueDetails(HttpRequestMessage request, string vcnSearch, string vesselName, string frmdate, string todate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<RevenuePostingVO> revenuePostingVOs = _marineRevenueService.GetMarineRevenueDetails(vcnSearch, vesselName,frmdate,todate);

                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, revenuePostingVOs);

                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetVcnDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                string searchColumn = HttpContext.Current.Request.Params["columnName"];
                string param = HttpContext.Current.Request.Params["columnName1"];


                List<RevenuePostingVO> revenuePostingVOs = _marineRevenueService.GetVcnDetails(searchValue, searchColumn, param);

                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, revenuePostingVOs);

                return response;
            });
        }


        [HttpGet]
        public HttpResponseMessage GetVcnViewDetails(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                string searchColumn = "VC";
                string param = "VCN";

                List<RevenuePostingVO> revenuePostingVOs = _marineRevenueService.GetVcnDetails(vcn, searchColumn, param);

                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, revenuePostingVOs);

                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetRevenueSectionDetails(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenuePostingSectionsVO revenueSectionDetails = _marineRevenueService.GetRevenueSectionDetails(vcn);

                response = request.CreateResponse<RevenuePostingSectionsVO>(HttpStatusCode.OK, revenueSectionDetails);

                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetVcnAgents(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<AgentVO> agentDetails = _marineRevenueService.GetVcnAgents(vcn);

                response = request.CreateResponse<List<AgentVO>>(HttpStatusCode.OK, agentDetails);

                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetAgentAccountDetails(HttpRequestMessage request, int agentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<AgentAccountVO> agentAccountDetails = _marineRevenueService.GetAgentAccountDetails(agentId);

                response = request.CreateResponse<List<AgentAccountVO>>(HttpStatusCode.OK, agentAccountDetails);

                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage PostMarineRevenueDetails(HttpRequestMessage request, RevenuePostingSectionsVO revenuePostingDetails)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int revenuePostingID = _marineRevenueService.AddMarineRevenueDetails(revenuePostingDetails);
                response = request.CreateResponse<int>(HttpStatusCode.Created, revenuePostingID);
                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetRevenueSectionDetailsView(HttpRequestMessage request, int revenuePostingId, int agentId, int accountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenuePostingSectionsVO revenueDetails = _marineRevenueService.GetRevenueSectionDetailsView(revenuePostingId, agentId, accountId);

                response = request.CreateResponse<RevenuePostingSectionsVO>(HttpStatusCode.OK, revenueDetails);

                return response;
            });
        }

        /// <summary>
        /// VCN Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/RevenuePostingVcnDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage RevenuePostingVcnDetailsforAutocomplete(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenuePostingVO> arrivalcommodities = _marineRevenueService.RevenuePostingVcnDetailsforAutocomplete(searchValue);
                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, arrivalcommodities);
                return response;
            });
        }

        /// <summary>
        /// Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/RevenuePostingVesselDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage RevenuePostingVesselDetailsforAutocomplete(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<VesselVO> Vessels = _marineRevenueService.RevenuePostingVesselDetailsforAutocomplete(searchvalue);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }
    }
}
