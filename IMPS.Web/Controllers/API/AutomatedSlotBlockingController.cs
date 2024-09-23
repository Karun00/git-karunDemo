using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class AutomatedSlotBlockingController : ApiControllerBase
    {
        IAutomatedSlotBlockingService _automatedSlotBlockingService;

        public AutomatedSlotBlockingController()
        {
            _automatedSlotBlockingService = new AutomatedSlotBlockingClient();
        }


        #region GetAutomatedSlotBlockings
        /// <summary>
        /// To Get AutomatedSlot Blockings
        /// </summary>
        /// <returns></returns>        [
        [Authorize]
        [Route("api/AutomatedSlotBlocking")]
        [HttpGet]
        public HttpResponseMessage GetAutomatedSlotBlockings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AutomatedSlotBlockingVO> slotBlockings = _automatedSlotBlockingService.GetAutomatedSlotBlockings();
                response = request.CreateResponse<List<AutomatedSlotBlockingVO>>(HttpStatusCode.OK, slotBlockings);
                return response;
            });
        }
        #endregion

        [Route("api/GetAutomatedReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetAutomatedReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AutomatedSlotBlockingVO referenceData = _automatedSlotBlockingService.GetAutomatedReferenceData();
                response = request.CreateResponse<AutomatedSlotBlockingVO>(HttpStatusCode.OK, referenceData);
                return response;
            });
        }

        /// <summary>
        /// To Add AutomatedSlot Blockings
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/AutomatedSlotBlocking")]
        [HttpPost]
        public HttpResponseMessage PostAutomatedSlotBlocking(HttpRequestMessage request, AutomatedSlotBlockingVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AutomatedSlotBlockingVO autoSlotBlock = _automatedSlotBlockingService.SaveAutomatedSlotBlocking(value);
                response = request.CreateResponse<AutomatedSlotBlockingVO>(HttpStatusCode.Created, autoSlotBlock);
                return response;
            });
        }

        /// <summary>
        /// To Modify AutomatedSlot Blockings
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/AutomatedSlotBlocking")]
        [HttpPut]
        public HttpResponseMessage ModifyAutomatedSlotBlocking(HttpRequestMessage request, AutomatedSlotBlockingVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AutomatedSlotBlockingVO autoSlotBlock = _automatedSlotBlockingService.ModifyAutomatedSlotBlocking(value);
                response = request.CreateResponse<AutomatedSlotBlockingVO>(HttpStatusCode.Created, autoSlotBlock);
                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _automatedSlotBlockingService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}