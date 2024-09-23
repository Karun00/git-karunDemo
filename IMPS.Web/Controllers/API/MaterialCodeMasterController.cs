using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class MaterialCodeMasterController : ApiControllerBase
    {
        IMaterialCodeMasterService _MaterialCodeMasterService;
        public MaterialCodeMasterController()
        {
            _MaterialCodeMasterService = new MaterialCodeMasterClient();
        }

        /// <summary>
        /// To get Material Code Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/MaterialCodeMaster")]
        [HttpGet]
        public HttpResponseMessage GetMaterialCodeDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<MaterialCodeMasterVO> MaterialCodeList = _MaterialCodeMasterService.GetMaterialCodeDetails();
                response = request.CreateResponse<List<MaterialCodeMasterVO>>(HttpStatusCode.OK, MaterialCodeList);
                return response;
            });
        }

    }
}