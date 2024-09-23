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
using System.Globalization;
using System.Threading;


namespace IPMS.Web.API
{
    public class AutomatedResourceSchedulingController : ApiControllerBase
    {
        IAutomatedResourceSchedulingService _automatedResourceScheduling;
        IAccountService _accountService;

        public AutomatedResourceSchedulingController()
        {
            _automatedResourceScheduling = new AutomatedResourceSchedulingClient();
            _accountService = new AccountClient();
        }

        [Authorize]
        [Route("api/ConfirmedServiceReq")]
        [HttpGet]
        public HttpResponseMessage GetMovementTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselCallMovementVO> servicereq = _automatedResourceScheduling.GetPendingMovementsForAllocation();
                response = request.CreateResponse<List<VesselCallMovementVO>>(HttpStatusCode.OK, servicereq);

                return response;
            });
        }


        /// <summary>
        /// Get ResourceAllocation Slot Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ResourceAllocationDetails/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetResourceAllocationDetails(HttpRequestMessage request, DateTime slotDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                List<MovementResourceAllocationVO> vessel = _automatedResourceScheduling.GetResourceAllocations(slotDate);
                responce = request.CreateResponse<List<MovementResourceAllocationVO>>(HttpStatusCode.OK, vessel);
                return responce;
            });
        }

        [Authorize]
        [Route("api/GetSearchCraft/{ServiceTypeCode}/{SlotNumber}/{AllocationDate}")]
        [HttpGet]
        public HttpResponseMessage GetSearchCraft(HttpRequestMessage request, string ServiceTypeCode, string SlotNumber, DateTime AllocationDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceSlotVO resourceSlot = new ResourceSlotVO();
                resourceSlot.ServiceTypeCode = ServiceTypeCode;
                resourceSlot.SlotNumber = Convert.ToInt32(SlotNumber, CultureInfo.InvariantCulture);
                resourceSlot.AllocationDate = AllocationDate;
                //string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<IdNameVO> crafts = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    //crafts = ls.GetSearchCraft(resourceSlot).Where(x => x.Name.StartsWith(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    crafts = ls.GetSearchCraft(resourceSlot);
                }

                response = request.CreateResponse<List<IdNameVO>>(HttpStatusCode.OK, crafts);

                return response;
            });
        }

        [Route("api/GetServiceTypes")]
        [HttpGet]
        public HttpResponseMessage GetServiceTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceTypeVO> ServiceTypes = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    ServiceTypes = ls.GetServiceTypes();
                }

                response = request.CreateResponse<List<ServiceTypeVO>>(HttpStatusCode.OK, ServiceTypes);

                return response;
            });
        }

        //[Authorize]
        //[Route("api/ScheduleResource")]
        //[HttpPost]
        //public HttpResponseMessage PostScheduleResource(HttpRequestMessage request, List<VesselCallMovementVO> value)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        List<MovementResourceAllocationVO> movemtvo = _automatedResourceScheduling.ScheduleMovements(value);
        //        response = request.CreateResponse<List<MovementResourceAllocationVO>>(HttpStatusCode.Created, movemtvo);
        //        return response;
        //    });
        //}

        [Authorize]
        [Route("api/ScheduleResource")]
        [HttpPost]
        public HttpResponseMessage PostScheduleResource(HttpRequestMessage request, VesselCallMovementVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //-- changed by sandeep on 02-02-2015
                //List<MovementResourceAllocationVO> movemtvo = _automatedResourceScheduling.ScheduleMovements(value);
                //response = request.CreateResponse<List<MovementResourceAllocationVO>>(HttpStatusCode.Created, movemtvo);

                string movemtvo = _automatedResourceScheduling.ScheduleMovements(value);
                response = request.CreateResponse<string>(HttpStatusCode.Created, movemtvo);
                //-- end

                return response;
            });
        }

        //[Authorize]
        //[Route("api/ResourceAllocations")]
        [HttpPost]
        public HttpResponseMessage PostResourceAllocations(HttpRequestMessage request, MovementResourceAllocationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<MovementResourceAllocationVO> movemtvo = _automatedResourceScheduling.SaveResourceAllocations(value);
                response = request.CreateResponse<List<MovementResourceAllocationVO>>(HttpStatusCode.Created, movemtvo);
                return response;
            });
        }

        [Route("api/GetAllShiftTypes")]
        [HttpGet]
        public HttpResponseMessage GetAllShiftTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ShiftVO> ShiftTypes = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    ShiftTypes = ls.GetShiftDetails();
                }

                response = request.CreateResponse<List<ShiftVO>>(HttpStatusCode.OK, ShiftTypes);

                return response;
            });
        }

        [Route("api/GetResourceCalendarDetails")]
        [HttpPost]
        public HttpResponseMessage GetResourceCalendarDetails(HttpRequestMessage request, ResourceCalendarSearchVO objResourceCalendarSearchVO)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ResourceCalendarVO> ResourceCalendar = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    ResourceCalendar = ls.GetResourceCalendarDetails(objResourceCalendarSearchVO);
                }

                response = request.CreateResponse<List<ResourceCalendarVO>>(HttpStatusCode.OK, ResourceCalendar);

                return response;
            });
        }

        [Route("api/GetCraftCalendarDetails")]
        [HttpPost]
        public HttpResponseMessage GetCraftCalendarDetails(HttpRequestMessage request, ResourceCalendarSearchVO objResourceCalendarSearchVO)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ResourceCalendarVO> ResourceCalendar = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    ResourceCalendar = ls.GetCraftCalendarDetails(objResourceCalendarSearchVO);
                }

                response = request.CreateResponse<List<ResourceCalendarVO>>(HttpStatusCode.OK, ResourceCalendar);

                return response;
            });
        }

        [Route("api/GetCraftAvailabilityServiceTypes")]
        [HttpGet]
        public HttpResponseMessage GetCraftAvailabilityServiceTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceTypeVO> ServiceTypes = null;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    ServiceTypes = ls.GetCraftAvailabilityServiceTypes();
                }

                response = request.CreateResponse<List<ServiceTypeVO>>(HttpStatusCode.OK, ServiceTypes);

                return response;
            });
        }

        [Route("api/VerifyMovementIsActiveByVCNAndServiceRequestID/{vcn}/{servicerequestid}")]
        [HttpGet]
        public HttpResponseMessage VerifyMovementIsActiveByVCNAndServiceRequestID(HttpRequestMessage request, string vcn, string servicerequestid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                bool status = false;
                using (IAutomatedResourceSchedulingService ls = new AutomatedResourceSchedulingClient())
                {
                    status = ls.VerifyMovementIsActiveByVCNAndServiceRequestId(vcn, Convert.ToInt32(servicerequestid, CultureInfo.InvariantCulture));
                }

                response = request.CreateResponse<bool>(HttpStatusCode.OK, status);

                return response;
            });
        }

        [Authorize]
        [Route("api/GetAutomatedResourceSchedulingPrivileges/{controllerName}")]
        [HttpGet]
        public HttpResponseMessage GetAutomatedResourceSchedulingPrivileges(HttpRequestMessage request, string controllerName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                bool result = false;
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = this.GetType().Name;
                    controllername = controllername.Replace("Controller", "");
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                    if (!string.IsNullOrEmpty(privilege.Privileges))
                    {
                        if (privilege.HasAddPrivilege || privilege.HasEditPrivilege)
                            result = true;
                    }
                }

                response = request.CreateResponse<bool>(HttpStatusCode.OK, result);
                return response;
            });
        }
    }
}
