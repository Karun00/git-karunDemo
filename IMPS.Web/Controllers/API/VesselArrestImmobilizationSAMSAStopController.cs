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
    public class VesselArrestImmobilizationSAMSAStopController : ApiControllerBase
    {
        IVesselArrestImmobilizationSAMSAStopService _VesselArrestImmobilizationSAMSAStopService;
        IVesselETAChangeService _etaChangeService;

        public VesselArrestImmobilizationSAMSAStopController()
        {
            _VesselArrestImmobilizationSAMSAStopService = new VesselArrestImmobilizationSAMSAStopClient();
            _etaChangeService = new VesselETAChangeClient();
        }

        #region GetVesselArrestImmobilizationSAMSAStopList
        /// <summary>
        /// Commented On  :  14th August 2014
        /// Description   :  Getting All VCN details with Top listed filtering
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/GetVesselArrestImmobilizationSAMSAStopList")]
        public HttpResponseMessage GetVesselArrestImmobilizationSAMSAStopList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselArrestImmobilizationSAMSAStopVO> vesselArrestImmobilizationSAMSAStop = _VesselArrestImmobilizationSAMSAStopService.GetVesselArrestImmobilizationSamsaStopList();
                response = request.CreateResponse<List<VesselArrestImmobilizationSAMSAStopVO>>(HttpStatusCode.OK, vesselArrestImmobilizationSAMSAStop);
                return response;
            });
        }
        #endregion

        #region PostVesselArrestImmobilizationSAMSAStopData
        /// <summary>
        /// Commented On  :  14th August 2014
        /// Description   :  Adding the Vessel Arrest Immobilization SAMSA Stop
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/VesselArrestImmobilizationSAMSAStop")]
        public HttpResponseMessage PostVesselArrestImmobilizationSAMSAStopData(HttpRequestMessage request, VesselArrestImmobilizationSAMSAStopVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselArrestImmobilizationSAMSAStopVO VesselArrestImmobilizationSAMSAStopCreated = _VesselArrestImmobilizationSAMSAStopService.AddVesselArrestImmobilizationSamsaStop(value);
                response = request.CreateResponse<VesselArrestImmobilizationSAMSAStopVO>(HttpStatusCode.Created, VesselArrestImmobilizationSAMSAStopCreated);
                return response;
            });
        }
        #endregion

        #region ModifyVesselArrestImmobilizationSAMSAStop
        /// <summary>
        /// Commented On  :  14th August 2014
        /// Description   :  Updating the Vessel Arrest Immobilization SAMSA Stop
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("api/VesselArrestImmobilizationSAMSAStop")]
        public HttpResponseMessage ModifyVesselArrestImmobilizationSAMSAStop(HttpRequestMessage request, VesselArrestImmobilizationSAMSAStopVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselArrestImmobilizationSAMSAStopVO VesselArrestImmobilizationSAMSAStopCreated = _VesselArrestImmobilizationSAMSAStopService.ModifyVesselArrestImmobilizationSamsaStop(value);
                response = request.CreateResponse<VesselArrestImmobilizationSAMSAStopVO>(HttpStatusCode.Created, VesselArrestImmobilizationSAMSAStopCreated);
                return response;
            });
        }
        #endregion

        #region GetVcnDetails
        /// <summary>
        /// 
        /// Commented On  :  14th August 2014
        /// Description   :  Getting All VCN details without any condition
        /// </summary>
        /// <param name="disposing"></param>
        [Authorize]
        [HttpGet]
        [Route("api/GetVcnDetails")]
        public HttpResponseMessage GetVcnDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalNotificationVO> VCNDtls = _VesselArrestImmobilizationSAMSAStopService.GetVcnDetails();
                response = request.CreateResponse<List<ArrivalNotificationVO>>(HttpStatusCode.OK, VCNDtls);
                return response;
            });
        }
        #endregion

        #region GetVesselInfoByVCN
        /// <summary>
        /// Commented On  :  14th August 2014
        /// Description   :  Getting Vessel Information Based on given VCN number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/GetVesselInfoByVCN")]
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _VesselArrestImmobilizationSAMSAStopService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
