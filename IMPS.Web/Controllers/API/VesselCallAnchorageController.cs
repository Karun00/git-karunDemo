using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace IPMS.Web.API
{

    public class VesselCallAnchorageController : ApiControllerBase
    {
        //
        // GET: /VesselCallAnchorage/
        IVesselCallAnchorageService _VesselCallAnchorageService;

        public VesselCallAnchorageController()
        {
            _VesselCallAnchorageService = new VesselCallAnchorageClient();
        }

        /// <summary>
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Getting Summary of Vessel Call Anchorage from Database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/VesselCallAnchorages/{vcn}/{vesselName}/{etaFrom}/{etaTo}")]
        public HttpResponseMessage GetAnchorageRecordingList(HttpRequestMessage request, string vcn, string vesselName, string etaFrom, string etaTo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselCallVO> Vesselcallanchorage = _VesselCallAnchorageService.GetAnchorageRecordingList(vcn, vesselName, etaFrom, etaTo);
                response = request.CreateResponse<List<VesselCallVO>>(HttpStatusCode.OK, Vesselcallanchorage);
                return response;
            });
        }

        /// <summary>
        /// 
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Getting all the Anchorage Details List from Database Based on VCN number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn">Selected vcn number</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]  
        [Route("api/VesselCallAnchorages/{vcn?}")]
        public HttpResponseMessage GetzAnchorageRecordingList(HttpRequestMessage request,string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselCallVO> Vesselcallanchorage = _VesselCallAnchorageService.GetzAnchorageRecordingList(vcn);
                response = request.CreateResponse<List<VesselCallVO>>(HttpStatusCode.OK, Vesselcallanchorage);
                return response;
            });
        }

        /// <summary>
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Getting all Reasons from the Database to bind Dropdown list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        // [Route("api/VesselCallAnchorageReasons")]
        public HttpResponseMessage GetReasons(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> Reasons = _VesselCallAnchorageService.GetReasons();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, Reasons);
                return response;
            });
        }

        /// <summary>
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Getting ConfigValue
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/VesselCallAnchorage/GetGeneralConfigs")]
        public HttpResponseMessage GetGeneralConfigs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string GeneralConfigs = _VesselCallAnchorageService.GetGeneralConfigs();
                response = request.CreateResponse<string>(HttpStatusCode.OK, GeneralConfigs);
                return response;
            });
        }

        /// <summary>
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Updating the Vessel Call Anchorage Data
        /// </summary>
        /// <param name="request">request details </param>
        /// <param name="value"> VesselCallVO Object Values</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("api/VesselCallAnchorages")]
        public HttpResponseMessage ModifyVesselCallAnchorageData(HttpRequestMessage request, VesselCallVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<VesselCallVO> Vesselcallanchorage = _VesselCallAnchorageService.ModifyVesselCallAnchorageData(value);
                response = request.CreateResponse<List<VesselCallVO>>(HttpStatusCode.Created, Vesselcallanchorage);
                return response;
            });
        }



        /// <summary>
        /// VCN Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselCallVcnDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage VesselCallVcnDetailsforAutocomplete(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenuePostingVO> arrivalcommodities = _VesselCallAnchorageService.VesselCallVcnDetailsforAutocomplete(searchValue);
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
        [Route("api/VesselCallVesselDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage VesselCallVesselDetailsforAutocomplete(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<VesselVO> Vessels = _VesselCallAnchorageService.VesselCallVesselDetailsforAutocomplete(searchvalue);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }


        [Authorize]
        [HttpGet]
        [Route("api/VcnClose/{vcn?}")]
        public HttpResponseMessage VcnClose(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VcnCloseVO VcnInfo = _VesselCallAnchorageService.VcnClose(vcn);
                response = request.CreateResponse<VcnCloseVO>(HttpStatusCode.OK, VcnInfo);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _VesselCallAnchorageService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}