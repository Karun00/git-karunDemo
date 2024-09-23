using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.ServiceProxies.Clients;

namespace IPMS.Web.API
{
    public class AutomatedSlotConfigurationController : ApiControllerBase
    {
         IAutomatedSlotConfigurationService _Automatedslotconfigservice;


         public AutomatedSlotConfigurationController()
        {
            _Automatedslotconfigservice = new AutomatedSlotConfigurationClient();          
        }

         [Authorize]
         //[Route("api/Berths")]
         [HttpGet]
         public HttpResponseMessage GetAutomatedSlotconfigList(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 List<AutomatedSlotConfigurationVO> AutomatedSlotConfigurationlist = _Automatedslotconfigservice.GetAutomatedSlotConfigList();
                 response = request.CreateResponse<List<AutomatedSlotConfigurationVO>>(HttpStatusCode.OK, AutomatedSlotConfigurationlist);
                 return response;
             });
         }

         public HttpResponseMessage GetSlotpriorityconfigList(HttpRequestMessage request,int ID)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 List<SlotPriorityConfigurationVO> SlotPriorityConfigList = _Automatedslotconfigservice.GetSlotPriorityConfigList(ID);
                 response = request.CreateResponse<List<SlotPriorityConfigurationVO>>(HttpStatusCode.OK, SlotPriorityConfigList);
                 return response;
             });
         }

         [Authorize]
         [Route("api/AutomatedSlotConfigReferenceData")]
         [HttpGet]
         public HttpResponseMessage GetAutomatedSlotConfigReferenceData(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 AutomatedSlotConfigurationReferenceDataVO AutomatedSlotConfigurationDetails = _Automatedslotconfigservice.GetAutomatedSlotConfigurationReferenceVO();
                 response = request.CreateResponse(HttpStatusCode.OK, AutomatedSlotConfigurationDetails);

                 return response;
             });
         }

         [Authorize]
         [Route("api/AutomatedSlotConfigaretions")]
         [HttpPost]
         public HttpResponseMessage PostAutomatedSlotConfigaretion(HttpRequestMessage request, AutomatedSlotConfigurationVO value)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;

                 AutomatedSlotConfigurationVO AutomatedSlotConfigurationCreated = _Automatedslotconfigservice.SaveAutomatedSlotConfigDetails(value);
                 response = request.CreateResponse<AutomatedSlotConfigurationVO>(HttpStatusCode.Created, AutomatedSlotConfigurationCreated);
                 return response;
             });
         }
         [Authorize]
         [Route("api/AutomatedSlotConfigaretions")]
         [HttpPut]
         public HttpResponseMessage PutAutomatedSlotConfigaretion(HttpRequestMessage request, AutomatedSlotConfigurationVO value)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;

                 AutomatedSlotConfigurationVO AutomatedSlotConfigurationUpdated = _Automatedslotconfigservice.UpdateAutomatedSlotConfigDetails(value);
                 response = request.CreateResponse<AutomatedSlotConfigurationVO>(HttpStatusCode.Created, AutomatedSlotConfigurationUpdated);
                 return response;
             });
         }
    
    }
}
