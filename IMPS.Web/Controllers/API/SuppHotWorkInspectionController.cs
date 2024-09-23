using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;

namespace IPMS.Web.Controllers.API
{
    public class SuppHotWorkInspectionController : ApiControllerBase
    {
        ISuppHotWorkInspectionService _SuppHotWorkInspectionservice;
        ISupplymentaryServiceRequestService _supplymentaryServiceRequestService;

        public SuppHotWorkInspectionController()
        {
            _SuppHotWorkInspectionservice = new SuppHotWorkInspectionClient();
            _supplymentaryServiceRequestService = new SupplymentaryServiceRequestClient();
        }

        /// <summary>
        /// Gets Supp Hot Work Inspection list
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppHotWorkInspections")]
        [HttpGet]
        public HttpResponseMessage AllSuppHotWorkInspectionDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //List<SuppHotWorkInspectionVO> AllSuppHotWorkInspections = _SuppHotWorkInspectionservice.AllSuppHotWorkInspectionDetails();
                //response = request.CreateResponse<List<SuppHotWorkInspectionVO>>(HttpStatusCode.OK, AllSuppHotWorkInspections);

                List<SuppServiceRequestVO> AllSuppHotWorkInspections = _supplymentaryServiceRequestService.AllSuppHotWorkInspectionDetails();
                response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, AllSuppHotWorkInspections);

                return response;
            });
        }




        /// <summary>
        /// adds / inserts the Supp Hot Work Inspection details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppHotWorkInspections")]
        [HttpPost]
        public HttpResponseMessage PostSuppHotWorkInspectionData(HttpRequestMessage request, SuppHotWorkInspectionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppHotWorkInspectionVO SuppHotWorkInspectionCreated = _SuppHotWorkInspectionservice.AddSuppHotWorkInspection(value);
                response = request.CreateResponse<SuppHotWorkInspectionVO>(HttpStatusCode.Created, SuppHotWorkInspectionCreated);
                return response;
            });
        }
        /// <summary>
        /// Modifies / update the Supp Hot Work Inspection details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppHotWorkInspections")]
        [HttpPut]
        //public HttpResponseMessage ModifySuppHotWorkInspection(HttpRequestMessage request, SuppHotWorkInspectionVO value)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        SuppHotWorkInspectionVO SuppHotWorkInspectionCreated = _SuppHotWorkInspectionservice.ModifySuppHotWorkInspection(value);
        //        response = request.CreateResponse<SuppHotWorkInspectionVO>(HttpStatusCode.Created, SuppHotWorkInspectionCreated);
        //        return response;
        //    });
        //}

        public HttpResponseMessage ModifySuppHotWorkInspection(HttpRequestMessage request, SuppServiceRequestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppServiceRequestVO SuppHotWorkInspectionCreated = _SuppHotWorkInspectionservice.ModifySuppHotWorkInspection(value);
                response = request.CreateResponse<SuppServiceRequestVO>(HttpStatusCode.Created, SuppHotWorkInspectionCreated);
                return response;
            });
        }

        /// <summary>
        /// Deletes Supp Hot Work Inspection - not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostDeleteSuppHotWorkInspectionData(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppHotWorkInspectionVO SuppHotWorkInspectionCreated = _SuppHotWorkInspectionservice.DeleteSuppHotWorkInspection(id);
                response = request.CreateResponse<SuppHotWorkInspectionVO>(HttpStatusCode.Created, SuppHotWorkInspectionCreated);
                return response;
            });
        }

        [Authorize]
       // [Route("api/ModifyHotWorkInspectionPermitStatus")]
        [HttpPost]
        public HttpResponseMessage PostModifyHotWorkInspectionPermitStatus(HttpRequestMessage request, SuppServiceRequestVO value) 
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                long id = value.SuppHotWorkInspectionVO.SuppHotWorkInspectionID;
                 string Result = null;

                using (ISuppHotWorkInspectionService ls = new SuppHotWorkInspectionClient())
                {
                    Result = ls.ModifyHotWorkInspectionPermitStatus(id);
                }
                //string Result =   _SuppHotWorkInspectionservice.ModifyHotWorkInspectionPermitStatus(id);
                response = request.CreateResponse<string>(HttpStatusCode.Created, Result);
                return response;
            });
        }
    }
}
