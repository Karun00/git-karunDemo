using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using IPMS.Web.Api;
using IPMS.Domain.ValueObjects;
using System.Linq;
using System.Web;
using System.Threading;

namespace IPMS.Web.API
{
    public class AutomatedSlottingController : ApiControllerBase
    {
        IAutomatedSlottingService _automatedService;
        IAccountService _accountService;

        public AutomatedSlottingController()
        {
            _automatedService = new AutomatedSlottingClient();
            _accountService = new AccountClient();
        }

        [Route("api/VesselMovementTypes")]
        [HttpGet]
        public HttpResponseMessage GetMovementTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> movementtypes = _automatedService.GetMovementTypes();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, movementtypes);

                return response;
            });
        }

        /// <summary>
        /// Get unplanned vessels
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("api/UnPlannedVesselsDet/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetUnPlannedVesselDet(HttpRequestMessage request, DateTime slotDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                List<VesselCallMovementVO> vessel = _automatedService.GetUnPlannedVesselDet(slotDate);
                responce = request.CreateResponse<List<VesselCallMovementVO>>(HttpStatusCode.OK, vessel);

                return responce;
            });
        }
        /// <summary>
        /// Updated planned vessel slot details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Plannedmovements"></param>
        /// <returns></returns>

        //[Route("api/UpdateVesselSoltDtl/{Plannedmovements},{ExtendYn}")]
        [Route("api/UpdateVesselSoltDtl")]
        [HttpPut]
        // [AcceptVerbs("GET", "PUT")]
        // [ActionName("UpdateVesselSoltDtl")]
        public HttpResponseMessage UpdateVesselSoltDtl(HttpRequestMessage request, List<VesselCallMovementVO> Plannedmovements)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage responce = null;
               int result = _automatedService.UpdateVesselSlotDetails(Plannedmovements);
               Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
               bHub.Show();
               responce = request.CreateResponse<int>(HttpStatusCode.Created, result);
               return responce;
           });

        }

        /// <summary>
        /// Get planned vessel slots
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/AutomatedSlots/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetPlannedVesselDet(HttpRequestMessage request, DateTime slotDate)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                List<VesselCallMovementVO> vessel = _automatedService.GetPlannedVesselDetails(slotDate);
                responce = request.CreateResponse<List<VesselCallMovementVO>>(HttpStatusCode.OK, vessel);
                return responce;
            });
        }

        [Route("api/ConfigurationDetails/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetAutomatedConfigurationDetails(HttpRequestMessage request, DateTime slotDate)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                AutomatedSlotConfigurationVO configvo = _automatedService.GetAutomatedConfigurationDetails(slotDate);
                responce = request.CreateResponse<AutomatedSlotConfigurationVO>(HttpStatusCode.OK, configvo);
                return responce;
            });
        }

        [Route("api/GettingExtendableYesNo/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetExtendableYesNo(HttpRequestMessage request, DateTime slotDate)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                AutomatedSlotConfigurationVO vessel = _automatedService.GetExtendableYesNo(slotDate);
                responce = request.CreateResponse<AutomatedSlotConfigurationVO>(HttpStatusCode.OK, vessel);
                return responce;
            });
        }

        [Route("api/GetPrivilegesByUserIDAndEntityCode/{entitycode}")]
        [HttpGet]
        public HttpResponseMessage GetPrivilegesByUserIDAndEntityCode(HttpRequestMessage request, string entitycode)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                bool status = _automatedService.GetPrivilegesByUserIdAndEntityCode(entitycode);
                responce = request.CreateResponse<bool>(HttpStatusCode.OK, status);
                return responce;
            });
        }

        [Route("api/UpdateSingleVesselSlotDetails")]
        [HttpPut]
        public HttpResponseMessage UpdateSingleVesselSlotDetails(HttpRequestMessage request, VesselCallMovementVO slotDetails)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                bool status = _automatedService.UpdateSingleVesselSlotDetails(slotDetails);
                responce = request.CreateResponse<bool>(HttpStatusCode.OK, status);
                return responce;
            });
        }

        [Route("api/GetActiveSlots")]
        [HttpGet]
        public HttpResponseMessage GetActiveSlots(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                var activeSlots = _automatedService.GetActiveSlots();
                responce = request.CreateResponse<List<string>>(HttpStatusCode.OK, activeSlots);
                return responce;
            });
        }

        [Route("api/GetBlockedSlots/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetBlockedSlots(HttpRequestMessage request, DateTime slotDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                var blockedSlots = _automatedService.GetBlockedSlots(slotDate);
                responce = request.CreateResponse<List<AutomatedSlotBlockingVO>>(HttpStatusCode.OK, blockedSlots);
                return responce;
            });
        }

       
        [Route("api/GetRolePrivileges/{controllerName}")]
        [HttpGet]
        public HttpResponseMessage GetRolePrivileges(HttpRequestMessage request, string controllerName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                string result = "false";

                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = this.GetType().Name;
                    controllername = controllername.Replace("Controller", "");
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                    if (!string.IsNullOrEmpty(privilege.Privileges))
                        result = "true";
                }

                response = request.CreateResponse<string>(HttpStatusCode.OK, result);
                return response;
            });
        }

        /// <summary>
        /// To Get Reason types 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ReasonTypes")]
        [HttpGet]
        public HttpResponseMessage GetReasonTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> subCategoryDocTypeDetails = _automatedService.GetReasonTypes();
                response = request.CreateResponse(HttpStatusCode.OK, subCategoryDocTypeDetails);
                return response;
            });
        }

        
    }
}