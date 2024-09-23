using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace IPMS.Web.Api
{
    public class BerthPreSchedulingController : ApiControllerBase
    {

         IBerthPreSchedulingService _BerthPreSchedulingService;
         IBollardService _bollardService;

         public BerthPreSchedulingController()
        {
            _BerthPreSchedulingService = new BerthPreSchedulingClient();
            _bollardService = new BollardClient();
        }

         /// <summary>
         /// To Get Berth Pre-Scheduling Reference Data
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
         [Authorize]
         [Route("api/BerthPreSchedulingReferenceData")]
         [HttpGet]
         public HttpResponseMessage GetBerthPreSchedulingReferenceData(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 BerthPreSchedulingReferenceVO berthpreschedulingDetails = _BerthPreSchedulingService.GetBerthPreSchedulingReferenceVO();
                 response = request.CreateResponse(HttpStatusCode.OK, berthpreschedulingDetails);

                 return response;
             });
         }

         [Authorize]
         [Route("api/GetVCMList/{AgentID}/{ETA}/{ETD}/{VesselType}/{ReasonforVisit}/{CargoType}/{MovementStatus}")]
         [HttpGet]
         public HttpResponseMessage GetVCMData(HttpRequestMessage request, string AgentID, string ETA, string ETD,string VesselType, string ReasonforVisit, string CargoType, string MovementStatus)
         {
             return GetHttpResponse(request, () => 
             {

                 HttpResponseMessage response = null;
                 List<VCMData> VCMList = _BerthPreSchedulingService.GetVesselCallMovements(AgentID, ETA, ETD, VesselType, ReasonforVisit, CargoType, MovementStatus);
                 response = request.CreateResponse(HttpStatusCode.OK, VCMList);

                 return response;

             });         
         
         }

         [Authorize]
         [Route("api/GetSuitableBerths")]
         [HttpGet]
         public HttpResponseMessage GetSuitableBerths(HttpRequestMessage request, string VCN, string ETB, string ETUB, string VesselCallMovementID)
         {
             return GetHttpResponse(request, () =>
             {

                 HttpResponseMessage response = null;
                 List<SuitableBerthVO> VCMList = _BerthPreSchedulingService.GetSuitableBerths(VCN, ETB, ETUB, VesselCallMovementID);
              // //  response = request.CreateResponse<SuitableBerthVO>(HttpStatusCode.OK, VCMList);
                 response = request.CreateResponse <List<SuitableBerthVO>>(HttpStatusCode.OK, VCMList);
                 return response;

             });


         }

         [Authorize]
         [HttpPut]
         [Route("api/BerthPreScheduling")]
         //This method is used for Update the data into backend.
         public HttpResponseMessage PutBerthPreSchedulingData(HttpRequestMessage request, BerthPreSchedulingVO data)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 string datass = _BerthPreSchedulingService.ModifyBerthPreScheduling(data);
                 Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                 bHub.Show();
                 response = request.CreateResponse<string>(HttpStatusCode.Created, datass);
                 return response;
             });
         }

         /// <summary>
         /// To get Bollards based on port, quay and berth
         /// </summary>
         /// <param name="request"></param>
         /// <param name="portId"></param>
         /// <param name="quayId"></param>
         /// <param name="berthId"></param>
         /// <returns></returns>
         [Authorize]
         [Route("api/BollardsInBerth/{portId}/{quayId}/{berthId}")]
         [HttpGet]
         public HttpResponseMessage GetBollardsInBerth(HttpRequestMessage request, string portId, string quayId, string berthId)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 List<BollardVO> bollards = _bollardService.GetBollardsInBerths(portId, quayId, berthId).OrderBy(a => a.FromMeter).ToList();
                 response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, bollards);
                 return response;
             });
         }

    }
}
