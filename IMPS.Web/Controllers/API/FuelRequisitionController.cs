using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class FuelRequisitionController : ApiControllerBase
    {
        IFuelRequisitionService _FuelRequisitionService;
        public FuelRequisitionController()
        {
            _FuelRequisitionService = new FuelRequisitionClient();
        }

        /// <summary>
        ///  To Get Fuel Requisition Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisition")]
        [HttpGet]
        public HttpResponseMessage GetFuelRequisitionDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> fuels = _FuelRequisitionService.FuelRequisitionDetails();
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, fuels);
                return response;
            });
        }



        /// <summary>
        /// To Fuel Requisition Reference data While initialization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisitionReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetFuelRequisitionReferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelRequisitionVO fuelRequisitionDetails = _FuelRequisitionService.GetFuelRequisitionReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, fuelRequisitionDetails);

                return response;
            });
        }

        /// <summary>
        ///   To Get Craft Name Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisitionCraft")]
        [HttpGet]
        public HttpResponseMessage GetCraftNames(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> crafts = _FuelRequisitionService.GetCraftNames();
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, crafts);
                return response;
            });
        }

        /// <summary>
        ///  To Get Craft Details By CraftID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisitionCraftInfo/{CraftID}")]
        [HttpGet]
        public HttpResponseMessage GetCraftsByID(HttpRequestMessage request, int CraftID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelRequisitionVO craftinfo = _FuelRequisitionService.GetCraftsByID(CraftID);
                response = request.CreateResponse<FuelRequisitionVO>(HttpStatusCode.OK, craftinfo);
                return response;
            });
        }

        /// <summary>
        /// To Add Fuel Requisition Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisition")]
        [HttpPost]
        public HttpResponseMessage PostFuelRequsitionData(HttpRequestMessage request, FuelRequisitionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelRequisitionVO fuelreqCreated = _FuelRequisitionService.AddFuelRequisition(value);
                response = request.CreateResponse<FuelRequisitionVO>(HttpStatusCode.Created, fuelreqCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Fuel Requisition Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisition")]
        [HttpPut]
        public HttpResponseMessage ModifyFuelRequisition(HttpRequestMessage request, FuelRequisitionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelRequisitionVO fuelreqCreated = _FuelRequisitionService.ModifyFuelRequisition(value);
                response = request.CreateResponse<FuelRequisitionVO>(HttpStatusCode.Created, fuelreqCreated);
                return response;
            });
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve Fuel Requisition 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/FuelRequisition/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveFuelRequisition(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _FuelRequisitionService.ApproveFuelRequisition(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Reject Fuel Requisition 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/FuelRequisition/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectFuelRequisition(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _FuelRequisitionService.RejectFuelRequisition(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        ///  To get Fuel Requisition based on FuelRequisitionID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fuelrequisitionid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelRequisition/{fuelrequisitionid}")]
        [HttpGet]
        public HttpResponseMessage GetFuelRequisition(HttpRequestMessage request, int fuelrequisitionid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> fuelrequisitiontype = _FuelRequisitionService.GetFuelRequisition(fuelrequisitionid);
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, fuelrequisitiontype);
                return response;
            });
        }
       
        #endregion






    }
}