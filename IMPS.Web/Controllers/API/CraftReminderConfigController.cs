using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Controllers.API
{
    public class CraftReminderConfigController : ApiControllerBase
    {
        ICraftReminderConfigService _craftreminderconfigService;

        public CraftReminderConfigController()
        {
            _craftreminderconfigService = new CraftReminderConfigClient();
           
        }
        //This method is used for get the data to fill grid.
        [Route("api/CraftsReminderConfig/{CraftID}")]
        public HttpResponseMessage GetCraftReminderConfigDetails(HttpRequestMessage request,int craftID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
               List<CraftReminderConfigVO> CraftConfiglist = _craftreminderconfigService.GetCraftReminderConfigDetails(craftID);
               response = request.CreateResponse<List<CraftReminderConfigVO>>(HttpStatusCode.OK, CraftConfiglist);
                return response;
            });
        }

        [Route("api/Crafts/{craftreminderconfigID}")]
        [HttpGet]
        public HttpResponseMessage GetCraftReminderConfigByID(HttpRequestMessage request, int craftreminderconfigID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftVO> Configlist = _craftreminderconfigService.GetCraftReminderConfigById(craftreminderconfigID);
                response = request.CreateResponse<List<CraftVO>>(HttpStatusCode.OK, Configlist);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetCraftreferencedata")]
        //This method is used for get the data to fill alldropdown.
        public HttpResponseMessage GetCraftReminderReferences(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CraftReferenceVO craftReferences = _craftreminderconfigService.GetCraftReminderReferences();
                response = request.CreateResponse(HttpStatusCode.OK, craftReferences);

                return response;
            });
        }

        [Authorize]
        [HttpPost]
        [Route("api/CraftsReminderConfig")]
        //This method is used for insert the data into backend.
        public HttpResponseMessage AddCraftReminderConfig(HttpRequestMessage request, CraftReminderConfigVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CraftVO configCreated = _craftreminderconfigService.AddCraftReminderConfig(value);
                response = request.CreateResponse<CraftVO>(HttpStatusCode.Created, configCreated);
                return response;
            });
        }


        [Authorize]
        [HttpPut]
        [Route("api/CraftsReminderConfig")]
        //This method is used for Update the data into backend.
        public HttpResponseMessage ModifyCraftReminderConfig(HttpRequestMessage request, CraftReminderConfigVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CraftReminderConfigVO configUpdated = _craftreminderconfigService.ModifyCraftReminderConfig(value);
                response = request.CreateResponse<CraftReminderConfigVO>(HttpStatusCode.Created, configUpdated);
                return response;
            });
        }

        [Route("api/CraftsReminderConfig/Acknowledge")]
        [HttpPost]
        public HttpResponseMessage Acknowledge(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _craftreminderconfigService.AcknowledgeCraftReminderConfig(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _craftreminderconfigService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
