using IPMS.Domain.Models;
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
    public class ResourceAttendanceController : ApiControllerBase
    {
        IResourceAttendanceService _resourceAttendanceService;

        public ResourceAttendanceController()
        {
            _resourceAttendanceService = new ResourceAttendanceClient();
        }

        /// <summary>
        /// To get designation details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [Route("api/ResourceDesignationDetails")]
        [HttpGet]
        public HttpResponseMessage GetDesignations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<SubCategory> designations = _resourceAttendanceService.GetDesignations();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, designations);

                return response;
            });
        }

        /// <summary>
        /// To get the shift details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ResourceShiftDetails")]
        [HttpPost]
        public HttpResponseMessage ShiftDetails(HttpRequestMessage request, ResourceAttendanceVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ShiftVO> shifts = null;

                using (IResourceAttendanceService _resourceAttendance = new ResourceAttendanceClient())
                {
                    shifts = _resourceAttendance.GetShiftDetails(value);
                }

                response = request.CreateResponse<List<ShiftVO>>(HttpStatusCode.OK, shifts);

                return response;
            });
        }

        /// <summary>
        /// To get the resource attendance details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ResourceAttendanceDetails")]
        [HttpPost]
        public HttpResponseMessage ResourceAttendanceDetails(HttpRequestMessage request, ResourceAttendanceVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<EmployeeMasterDetails> employees = null;

                using (IResourceAttendanceService _resourceAttendance = new ResourceAttendanceClient())
                {
                    employees = _resourceAttendance.GetResourceAttendanceDetails(value);
                }

                response = request.CreateResponse<List<EmployeeMasterDetails>>(HttpStatusCode.OK, employees);

                return response;
            });
        }

        /// <summary>
        ///  To add a resource attendance data to the data base
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        [Authorize]
        [Route("api/SaveResourceAttendance")]
        [HttpPost]
        public HttpResponseMessage PostResourceAttendanceDetails(HttpRequestMessage request, ResourceAttendanceVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int resourceID = 0;
                using (IResourceAttendanceService _resourceAttendance = new ResourceAttendanceClient())
                {
                    resourceID = _resourceAttendance.AddResourceAttendanceDetails(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, resourceID);

                return response;
            });
        }
    }
}
