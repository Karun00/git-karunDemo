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
    public class ShiftController : ApiControllerBase
    {
           IShiftService _Shiftservice;
      


        public ShiftController()
        {
            _Shiftservice = new ShiftClient();
          
        }

        /// <summary>
        /// To get all Shift Data 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/ShiftDetails")]
        [Authorize]
        public HttpResponseMessage GetShiftInfo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ShiftVO> shift = _Shiftservice.GetShiftList();
                response = request.CreateResponse<List<ShiftVO>>(HttpStatusCode.OK, shift);
                return response;
            });
        }

        /// <summary>
        /// To Add New Shift Record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/PostShift")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostShiftData(HttpRequestMessage request, ShiftVO value)
        {
            return GetHttpResponse(request, () =>
            {
               
                HttpResponseMessage response = null;
               
                ShiftVO shiftCreated = _Shiftservice.AddShift(value);
                response = request.CreateResponse<ShiftVO>(HttpStatusCode.Created, shiftCreated);
                return response;
            });
          
        }

        /// <summary>
        /// This method is used to Modify the data.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/PutShift")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage ModifyShift(HttpRequestMessage request, ShiftVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
             
                ShiftVO shiftCreated = _Shiftservice.ModifyShift(value);
                response = request.CreateResponse<ShiftVO>(HttpStatusCode.Created, shiftCreated);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Shiftservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
