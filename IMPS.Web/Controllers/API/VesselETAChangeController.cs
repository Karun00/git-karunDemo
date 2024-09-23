using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Controllers.API
{
    public class VesselETAChangeController : ApiControllerBase
    {
        IVesselETAChangeService _etaChangeService;

        public VesselETAChangeController()
        {
            _etaChangeService = new VesselETAChangeClient();
        }

        #region GetChangeETADetails
        /// <summary>
        ///  To Get Change ETA Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ChangeETA/{vcn}/{vesselName}/{etafrom}/{etato}/{agentNameSearch}")]
        [HttpGet]
        public HttpResponseMessage GetChangeETADetails(HttpRequestMessage request, string vcn, string vesselName, string etafrom, string etato, string agentNameSearch)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselETAChangeVO> changeeta = _etaChangeService.ChangeEtaDetails(vcn, vesselName, etafrom, etato, agentNameSearch);
                response = request.CreateResponse<List<VesselETAChangeVO>>(HttpStatusCode.OK, changeeta);
                return response;
            });
        }
        #endregion

        #region GetzChangeETADetails
        /// <summary>
        ///  To Get Change ETA Details by vcn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetzChangeETADetails/{vcn}/{VesselETAChangeID}")]
        [HttpGet]
        public HttpResponseMessage GetzChangeETADetails(HttpRequestMessage request, string vcn, int? VesselETAChangeID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselETAChangeVO> changeeta = _etaChangeService.ChangezEtaDetails(vcn, VesselETAChangeID);
                response = request.CreateResponse<List<VesselETAChangeVO>>(HttpStatusCode.OK, changeeta);
                return response;
            });
        }
        #endregion

        #region GetVCNs
        /// <summary>
        ///   To Get VCN Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetVCNs")]
        [HttpGet]
        public HttpResponseMessage GetVCNs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselETAChangeVO> vcns = _etaChangeService.GetArrivalVcns();
                response = request.CreateResponse<List<VesselETAChangeVO>>(HttpStatusCode.OK, vcns);
                return response;
            });
        }
        #endregion

        #region GetVesselInfoByVCN
        /// <summary>
        ///  To Get Vessel Information By VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetVesselInfoByVCN/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetVesselInfoByVCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselETAChangeVO vesselInfo = _etaChangeService.GetVesselInfoByVcns(vcn);
                response = request.CreateResponse<VesselETAChangeVO>(HttpStatusCode.OK, vesselInfo);
                return response;
            });
        }
        #endregion

        #region PostVesselETAChange
        /// <summary>
        /// To Add Change ETA Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SaveChangeETA")]
        [HttpPost]
        public HttpResponseMessage PostVesselETAChange(HttpRequestMessage request, VesselETAChangeVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselETAChangeVO vesselinfo = _etaChangeService.PostVesselEtaChange(data);
                response = request.CreateResponse<VesselETAChangeVO>(HttpStatusCode.OK, vesselinfo);
                return response;
            });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _etaChangeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}