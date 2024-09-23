using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class FuelReceiptController : ApiControllerBase
    {
        IFuelReceiptService _FuelReceiptService;
        public FuelReceiptController()
        {
            _FuelReceiptService = new FuelReceiptClient();
        }

        /// <summary>
        ///  To Get Fuel Receipt Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelReceipt")]
        [HttpGet]
        public HttpResponseMessage GetFuelReceiptDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> fuelreceipts = _FuelReceiptService.FuelReceiptDetails();
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, fuelreceipts);
                return response;
            });
        }

        /// <summary>
        /// To Fuel Receipt Reference data While initialization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelReceiptReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetFuelReceiptReferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelReceiptVO fuelReceiptDetails = _FuelReceiptService.GetFuelReceiptReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, fuelReceiptDetails);

                return response;
            });
        }

        /// <summary>
        /// To Add Fuel Receipt Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelReceipt")]
        [HttpPost]
        public HttpResponseMessage PostFuelRequsitionData(HttpRequestMessage request, FuelReceiptVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FuelReceiptVO fuelreceiptCreated = _FuelReceiptService.AddFuelReceipt(value);
                response = request.CreateResponse<FuelReceiptVO>(HttpStatusCode.Created, fuelreceiptCreated);
                return response;
            });
        }

        /// <summary>
        ///  To get Fuel Receipt based on Fuelrequestionid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fuelrequestionid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelReceipt/{fuelrequestionid}")]
        [HttpGet]
        public HttpResponseMessage GetFuelReceipt(HttpRequestMessage request, int fuelrequestionid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> fuelreceipttype = _FuelReceiptService.GetFuelReceipt(fuelrequestionid);
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, fuelreceipttype);
                return response;
            });
        }

        /// <summary>
        ///  To get Fuel Receipt based on fuelreceiptid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fuelreceiptid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FuelReceiptFuelID/{fuelreceiptid}")]
        [HttpGet]
        public HttpResponseMessage GetFuelReceiptFuelID(HttpRequestMessage request, int fuelreceiptid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelRequisitionVO> fuelreceipttype = _FuelReceiptService.GetFuelReceiptFuelId(fuelreceiptid);
                response = request.CreateResponse<List<FuelRequisitionVO>>(HttpStatusCode.OK, fuelreceipttype);
                return response;
            });
        }


        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve Fuel Receipt
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/FuelReceipt/Acknowledge")]
        [HttpPost]
        public HttpResponseMessage ApproveFuelReceipt(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _FuelReceiptService.ApproveFuelReceipt(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

    }
        
}