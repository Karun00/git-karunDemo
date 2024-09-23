using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class SupplementaryServiceExtensionController : ApiControllerBase
    {
       // ISupplymentaryServiceRequestService _supplymentaryServiceRequestService;

        //public SupplementaryServiceExtensionController()
        //{
        //    _supplymentaryServiceRequestService = new SupplymentaryServiceRequestClient();
           
        //}

        //[HttpGet]
        //[Route("api/SupplementaryServiceExtensions")]
        //public HttpResponseMessage GetSupplymentaryServiceRequestList(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        List<SuppServiceRequestVO> lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetSupplymentaryServiceRequestList();
        //        response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, lstSuppServiceRequestVO);
        //        return response;
        //    });
        //}

    }
}
