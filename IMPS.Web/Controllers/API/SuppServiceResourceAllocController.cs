using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Linq;
using System;

namespace IPMS.Web.Api
{
    public class SuppServiceResourceAllocController : ApiControllerBase
    {
        ISuppServiceResourceAllocService _suppServiceResourceAllocService = null;
        //IResourceAllocationService _resourceAllocation = null;

        public SuppServiceResourceAllocController()
        {
            _suppServiceResourceAllocService = new SuppServiceResourceAllocClient();
            //_resourceAllocation = new ResourceAllocationServiceClient();
        }

        [Authorize]
        //[Route("api/SuppServiceResourceAllocation/{slotDate}/{vcn}")]
        [Route("api/ResourceAllocation/{slotDate}/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetSupplementaryResourceAllocationByDate(HttpRequestMessage request, DateTime slotDate, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceAllocationSlotVO> resourceAllocationSlotVOs = _suppServiceResourceAllocService.GetSupplementaryResourceAllocationByDate(slotDate, vcn);
                response = request.CreateResponse<List<ResourceAllocationSlotVO>>(HttpStatusCode.OK, resourceAllocationSlotVOs);

                return response;
            });
        }

        [Authorize]
        //[Route("api/SuppServiceResourceAllocation")]
        [Route("api/ResourceAllocation")]
        [HttpPut]
        public HttpResponseMessage PostSupplementaryResourceAllocation(HttpRequestMessage request, List<ResourceAllocationSlotVO> ResourceAllocationSlotVOs)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceAllocationSlotVO> lst = _suppServiceResourceAllocService.PostSupplementaryResourceAllocation(ResourceAllocationSlotVOs);

                response = request.CreateResponse<List<ResourceAllocationSlotVO>>(HttpStatusCode.OK, lst);

                return response;

            });
        }

        [Authorize]
        [Route("api/GetSearchResource/{ServiceTypeCode}/{ServiceReferenceID}/{SlotNumber}/{AllocationDate}")]
        [HttpGet]
        public HttpResponseMessage GetSearchResource(HttpRequestMessage request, string ServiceTypeCode, int ServiceReferenceID, int SlotNumber, DateTime AllocationDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<IdNameVO> users = null;
                ResourceSlotVO data = new ResourceSlotVO();
                data.ServiceTypeCode = ServiceTypeCode;
                data.ServiceReferenceID = ServiceReferenceID;
                data.SlotNumber = SlotNumber;
                data.AllocationDate = AllocationDate;
                //string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                //List<ServiceRequestVCNDetails> VCNDtls = _servicerequestService.GetVCNDetails().Where(x => x.VCN.StartsWith(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
                using (ISuppServiceResourceAllocService ls = new SuppServiceResourceAllocClient())
                {
                    //users = ls.GetSearchResource(value).Where(x => x.Name.StartsWith(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    users = ls.GetSearchResource(data);
                }

                response = request.CreateResponse<List<IdNameVO>>(HttpStatusCode.OK, users);

                return response;
            });
        }

        [Authorize]
        [Route("api/GetSlotConfiguration/{slotDate}")]
        [HttpGet]
        public HttpResponseMessage GetSlotConfiguration(HttpRequestMessage request, DateTime slotDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceSlotVO> ResourceSlotVO = _suppServiceResourceAllocService.GetSlotConfiguration(slotDate);

                response = request.CreateResponse<List<ResourceSlotVO>>(HttpStatusCode.OK, ResourceSlotVO);

                return response;
            });
        }

        [Authorize]
        [Route("api/GetPortNameByPortCode")]
        [HttpGet]
        public HttpResponseMessage GetPortNameByPortCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string portName = _suppServiceResourceAllocService.GetPortNameByPortCode();

                response = request.CreateResponse<string>(HttpStatusCode.OK, portName);

                return response;
            });
        }

        [Authorize]
        [Route("api/GetResourceAllocationVCNDetails/{date}")]
        [HttpGet]
        public HttpResponseMessage GetVCNDetails(HttpRequestMessage request, DateTime date)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<string> VCNs = _suppServiceResourceAllocService.GetVCNDetails(date);

                response = request.CreateResponse<List<string>>(HttpStatusCode.OK, VCNs);

                return response;
            });
        }

        [Authorize]
        [Route("api/UpdateResourceAllocation")]
        [HttpPut]
        public HttpResponseMessage UpdateResourceAllocation(HttpRequestMessage request, ResourceAllocationSlotVO resourceAllocationSlotData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ResourceAllocationSlotVO resourceAllocationSlotVO = _suppServiceResourceAllocService.UpdateResourceAllocation(resourceAllocationSlotData);

                response = request.CreateResponse<ResourceAllocationSlotVO>(HttpStatusCode.OK, resourceAllocationSlotVO);

                return response;

            });
        }

        [Authorize]
        [Route("api/GetActiveResourceSlot")]
        [HttpGet]
        public HttpResponseMessage GetActiveResourceSlots(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceSlotVO> resourceSlotVOs = _suppServiceResourceAllocService.GetActiveResourceSlots();

                response = request.CreateResponse<List<ResourceSlotVO>>(HttpStatusCode.OK, resourceSlotVOs);

                return response;

            });
        }

        [Authorize]
        //[Route("api/SuppServiceResourceAllocation")]
        [Route("api/UpdateSlotById/{resourceAllocationId}")]
        [HttpPut]
        public HttpResponseMessage UpdateResourceAllocationSlotById(HttpRequestMessage request, string resourceAllocationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string result = _suppServiceResourceAllocService.UpdateResourceAllocationSlotById(resourceAllocationId);

                response = request.CreateResponse<string>(HttpStatusCode.OK, result);

                return response;

            });
        }
    }
}
