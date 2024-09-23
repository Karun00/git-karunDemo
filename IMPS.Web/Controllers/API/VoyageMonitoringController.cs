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
    public class VoyageMonitoringController : ApiControllerBase
    {
        IVoyageMonitoringService _VoyageMonitoringService;
        ISupplymentaryServiceRequestService _supplymentaryServiceRequestService;
        IResourceAllocationService _resourceAllocationService;

        public VoyageMonitoringController()
        {
            _VoyageMonitoringService = new VoyageMonitoringClient();
            _supplymentaryServiceRequestService = new SupplymentaryServiceRequestClient();
            _resourceAllocationService = new ResourceAllocationServiceClient();
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetVCNDetailsVoyage")]
        public HttpResponseMessage GetVCNDetailsVoyage(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];

                List<VoyageMonitoringVO> VCNDtls = _VoyageMonitoringService.GetVcnDetailsVoyage(searchValue);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, VCNDtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetVCNDetailsVoyage_VCN")]
        public HttpResponseMessage GetVCNDetailsVoyage_VCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> vcnDtls = _VoyageMonitoringService.GetVcnDetailsVoyage_vcn(vcn);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, vcnDtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetServiceRequestDetailss")]
        public HttpResponseMessage GetServiceRequestDetailss(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> serviceReqDtls = _VoyageMonitoringService.GetServiceRequestDetails(VCN);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, serviceReqDtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetChangeATAandATDDetails")]
        public HttpResponseMessage GetChangeATAandATDDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> changeEtaDtls = _VoyageMonitoringService.GetChangeAtaAndAtdDetails(VCN);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, changeEtaDtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetPortAndBreakLimitDetails")]
        public HttpResponseMessage GetPortAndBreakLimitDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> Dtls = _VoyageMonitoringService.GetPortAndBreakLimitDetails(VCN);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, Dtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetAnchorageDetails")]
        public HttpResponseMessage GetAnchorageDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> anchorageDtls = _VoyageMonitoringService.GetAnchorageDetails(VCN);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, anchorageDtls);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetSupplymentaryServiceRequestList_VCN")]
        public HttpResponseMessage GetSupplymentaryServiceRequestList_VCN(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppServiceRequestVO> lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetSupplymentaryServiceRequestListVcn(VCN);
                response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, lstSuppServiceRequestVO);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetresourceAllocationdetails_VCN")]
        public HttpResponseMessage GetresourceAllocationdetails_VCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ResourceAllocationVO> lstResourceAllocationVO = _resourceAllocationService.GetresourceAllocationdetailsByVCN(vcn);
                response = request.CreateResponse<List<ResourceAllocationVO>>(HttpStatusCode.OK, lstResourceAllocationVO);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetBerthDetails")]
        public HttpResponseMessage GetBerthDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VoyageMonitoringVO> AnchorageDtls = _VoyageMonitoringService.GetBerthDetails(VCN);
                response = request.CreateResponse<List<VoyageMonitoringVO>>(HttpStatusCode.OK, AnchorageDtls);
                return response;
            });
        }

    }
}