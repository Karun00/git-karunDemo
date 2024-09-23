using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class VesselSAPPostingController : ApiControllerBase
    {
        IVesselSAPPostingService _VesselSAPPostingService;
        public VesselSAPPostingController()
        {
            _VesselSAPPostingService = new VesselSAPPostingClient();
        }

        /// <summary>
        ///  To Get SAP Posting Details By ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetSAPPostVessels")]
        [HttpGet]
        public HttpResponseMessage GetSAPVesselPostGrid(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SAPPostingVO> sappostings = _VesselSAPPostingService.GetSAPVesselPostGrid();
                response = request.CreateResponse<List<SAPPostingVO>>(HttpStatusCode.OK, sappostings);
                return response;
            });
        }


        /// <summary>
        ///  To Get SAP Posting Details By ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/VesselsList")]
        [HttpGet]
        public HttpResponseMessage GetVesselsList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string SearchColumn = HttpContext.Current.Request.Params["columnName"];
                string SearchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                HttpResponseMessage response = null;
                List<VesselSAPPostingVO> sappostings = _VesselSAPPostingService.GetVesselsList(SearchColumn, SearchValue);
                response = request.CreateResponse<List<VesselSAPPostingVO>>(HttpStatusCode.OK, sappostings);
                return response;
            });
        }

        [Authorize]
        [Route("api/VesselSAPPosting/SAPVesselData/{columnName}/{SearchValue}")]
        [HttpGet]
        public HttpResponseMessage SAPVesselData(HttpRequestMessage request, string columnName, string SearchValue)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselSAPPostingVO> sappostings = _VesselSAPPostingService.GetVesselsList(columnName, SearchValue);
                response = request.CreateResponse<List<VesselSAPPostingVO>>(HttpStatusCode.OK, sappostings);
                return response;
            });
        }



        /// <summary>
        /// To Post Vessel SAP Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/VesselSAPPosting")]
        [HttpPost]
        public HttpResponseMessage PostVesselSAP(HttpRequestMessage request, VesselSAPPostingVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselSAPPostingVO sapCreated = _VesselSAPPostingService.PostVesselSAP(value);
                response = request.CreateResponse<VesselSAPPostingVO>(HttpStatusCode.Created, sapCreated);
                return response;
            });
        }
    }
}