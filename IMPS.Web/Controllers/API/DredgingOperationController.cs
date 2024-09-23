using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.ServiceProxies.Contracts;
using IPMS.ServiceProxies.Clients;
using IPMS.Domain.ValueObjects;

namespace IPMS.Web.Api
{
    public class DredgingOperationController : ApiControllerBase
    {
        private IDredgingPriorityService _dredgingPriorityService;

        public DredgingOperationController()
        {
            _dredgingPriorityService = new DredgingPriorityClient();
        }

        [Route("api/BerthOccupation")]
        [HttpGet]
        public HttpResponseMessage GetBerthOccupationList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<DredgingOperationVO> dredgingPriorityVOs = _dredgingPriorityService.GetBerthOccupationList();

                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingPriorityVOs);

                return response;
            });
        }
        [HttpGet]
        public HttpResponseMessage GetBerthOccupationById(HttpRequestMessage request , int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<DredgingOperationVO> dredgingPriorityVOs = _dredgingPriorityService.GetBerthOccupationById(id);

                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingPriorityVOs);

                return response;
            });
        }

        #region Workflow Integrated Methods for Berth Occupation

        /// <summary>
        /// This method is used for Approve 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/BerthOccupation/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveBerthOccupation(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _dredgingPriorityService.ApproveBerthOccupation(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        

        /// <summary>
        /// This method is used for Reject 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/BerthOccupation/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectBerthOccupation(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _dredgingPriorityService.RejectBerthOccupation(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion


        #region Workflow Integrated Methods for Dredging Volume

        /// <summary>
        /// This method is used for Approve 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/DredgingVolume/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveDredgingVolume(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _dredgingPriorityService.ApproveDredgingVolume(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }



        /// <summary>
        /// This method is used for Reject 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
         [Route("api/DredgingVolume/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectDredgingVolume(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _dredgingPriorityService.RejectDredgingVolume(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion

        [Route("api/BerthOccupation")]
        [HttpPut]
        public HttpResponseMessage UpdateBerthOccupation(HttpRequestMessage request, DredgingOperationVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DredgingOperationVO dredgingPriorityVO = _dredgingPriorityService.UpdateBerthOccupation(data);

                response = request.CreateResponse<DredgingOperationVO>(HttpStatusCode.OK, dredgingPriorityVO);

                return response;
            });
        }

        [Route("api/DredgingVolume")]
        [HttpGet]
        public HttpResponseMessage GetDredgingVolumeList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<DredgingOperationVO> dredgingPriorityVOs = _dredgingPriorityService.GetDredgingVolumeList();

                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingPriorityVOs);

                return response;
            });
        }
        [HttpGet]
        public HttpResponseMessage GetDredgingVolumeById(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<DredgingOperationVO> dredgingPriorityVOs = _dredgingPriorityService.GetDredgingVolumeById(id);

                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingPriorityVOs);

                return response;
            });
        }
        [Route("api/DredgingVolume")]
        [HttpPut]
        public HttpResponseMessage UpdateDredgingVolume(HttpRequestMessage request, DredgingOperationVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DredgingOperationVO dredgingPriorityVO = _dredgingPriorityService.UpdateDredgingVolume(data);

                response = request.CreateResponse<DredgingOperationVO>(HttpStatusCode.OK, dredgingPriorityVO);

                return response;
            });
        }

        /// <summary>
        /// This method is used for Cancel
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingOperation/GridCancel")]
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, DredgingOperationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;

                List<DredgingOperationVO> DredgingOperationCreated = _dredgingPriorityService.Cancel(value);
                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.Created, DredgingOperationCreated);


                return response;
            });
        }
    }
}