using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class CargoManifestController : ApiControllerBase
    {
        ICargoManifestService _CargoManifestService;
        public CargoManifestController()
        {
            _CargoManifestService = new CargoManifestClient();
        }

        [Authorize]
        [Route("api/CargoManifest")]
        [HttpGet]
        public HttpResponseMessage CargoManifestDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VCNData> CargoManifest = _CargoManifestService.CargoManifestDetails();
                response = request.CreateResponse<List<VCNData>>(HttpStatusCode.OK, CargoManifest);
                return response;
            });
        }

        [Authorize]
        [Route("api/ArrivalCommodityDetails")]
        [HttpGet]
        public HttpResponseMessage ArrivalCommodityDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalCommoditiesList> ArrivalCommodities = _CargoManifestService.ArrivalCommodityDetails(VCN);
                response = request.CreateResponse<List<ArrivalCommoditiesList>>(HttpStatusCode.OK, ArrivalCommodities);
                return response;
            });
        }

        [Authorize]
        [Route("api/CargoManifest")]
        [HttpPost]
        public HttpResponseMessage PostCargoManifData(HttpRequestMessage request, CargoManifestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CargoManifestVO cargomanifestCreated = _CargoManifestService.AddCargoManifest(value);
                response = request.CreateResponse<CargoManifestVO>(HttpStatusCode.Created, cargomanifestCreated);
                return response;
            });
        }

        [Authorize]
        [Route("api/CargoManifest")]
        [HttpPut]
        public HttpResponseMessage ModifyCargoManifestData(HttpRequestMessage request, CargoManifestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CargoManifestVO cargomanifestModified = _CargoManifestService.ModifyCargoManifest(value);
                response = request.CreateResponse<CargoManifestVO>(HttpStatusCode.Created, cargomanifestModified);
                return response;
            });
        }
    }
}