using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class ResourceAllocationController : ApiControllerBase
    {
        IResourceAllocationService _resourceallocationservice;
          IArrivalNotificationService _arrivalnotificationservice;



        public ResourceAllocationController()
        {
            _resourceallocationservice = new ResourceAllocationServiceClient();
            _arrivalnotificationservice = new ArrivalNotificationClient();
        }

        /// <summary>
        /// To Get Agent Registration details in view screens for Pending Task View Screens of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/GetResourceallocationList/{vcn}/{vesselName}/{resourceName}")]
        public HttpResponseMessage GetResourceallocationList(HttpRequestMessage request, string vcn, string vesselName, string resourceName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceAllocationVO> resourceallocationlist = _resourceallocationservice.GetResourceAllocations(vcn, vesselName, resourceName);

                response = request.CreateResponse<List<ResourceAllocationVO>>(HttpStatusCode.OK, resourceallocationlist);

                return response;
            });
        }


      
        
        [Authorize]
        [HttpGet]
        [Route("api/GetWaterDetailsList/{resourceAllocationID}/{act}")]
        public HttpResponseMessage GetWaterDetailsList(HttpRequestMessage request,  string resourceAllocationID,string act)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<OtherServiceRecordingVO> resourceallocationlist = _resourceallocationservice.GetWaterDetailsList(resourceAllocationID,act);

                response = request.CreateResponse<List<OtherServiceRecordingVO>>(HttpStatusCode.OK, resourceallocationlist);

                return response;
            });
        }

      

        [Authorize]
        [Route("api/GetFormDetails")]
        [HttpPost]
        public HttpResponseMessage GetResourceAloocationFormDetails(HttpRequestMessage request, ResourceAllocationVO resource)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationVO resourceallocationdetails = _resourceallocationservice.GetResourceAllocationformDetails(resource);
                response = request.CreateResponse<ResourceAllocationVO>(HttpStatusCode.Created, resourceallocationdetails);
                return response;
            });
        }

        [Authorize]
        [Route("api/ResourceallocationReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetResourceallocationReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationReferenceDataVO arrivalnotificationDetails = _resourceallocationservice.GetResourceAllocationReferenceDataVO();
                response = request.CreateResponse(HttpStatusCode.OK, arrivalnotificationDetails);

                return response;
            });
        }

        [Route("api/GetBollardsInBerthsDetails")]
        [HttpGet]
        public HttpResponseMessage GetBollardsInBerthsDetails(HttpRequestMessage request, string Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BollardVO> resourceallocationdetails = _resourceallocationservice.GetBollardsInBerths(Id);


                response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.Created, resourceallocationdetails);

                return response;
            });
        }

        [Authorize]
        [HttpPost]
        [Route("api/PostResourceAloocationDetails")]
        public HttpResponseMessage PostResourceAloocationDetails(HttpRequestMessage request, ResourceAllocationVO resource)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationVO resourceallocationdetails = _resourceallocationservice.UpdateResourceAllocationformDetails(resource);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<ResourceAllocationVO>(HttpStatusCode.Created, resourceallocationdetails);

                return response;
            });
        }       
        [Authorize]
        [HttpPost]
        [Route("api/SaveWaterAllocationDetails")]
        public HttpResponseMessage SaveWaterAllocationDetails(HttpRequestMessage request, ResourceAllocationVO resource)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationVO resourceallocationdetails = _resourceallocationservice.SaveWaterAllocationDetails(resource);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<ResourceAllocationVO>(HttpStatusCode.Created, resourceallocationdetails);

                return response;
            });
        }

        [Authorize]
        [HttpPut]
        [Route("api/PutResourceAloocationDetails")]
        public HttpResponseMessage PutResourceAloocationDetails(HttpRequestMessage request, ResourceAllocationVO resource)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationVO resourceallocationdetails = _resourceallocationservice.ModifyResourceAllocationformDetails(resource);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<ResourceAllocationVO>(HttpStatusCode.Created, resourceallocationdetails);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _resourceallocationservice.Dispose();

            }
            base.Dispose(disposing);
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetResourcealocationByResourceAllocID")]
        public HttpResponseMessage GetresourceAllocation_ResourceAllocID(HttpRequestMessage request, string Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ResourceAllocationVO lstResourceAllocationVO = _resourceallocationservice.GetresourceAllocationByResourceAllocId(Id);
                response = request.CreateResponse<ResourceAllocationVO>(HttpStatusCode.OK, lstResourceAllocationVO);
                return response;
            });
        }
        [Route("api/CheckMeterNoExists/{meterno}/{resourceAllocationID}")]
        [HttpGet]
        public HttpResponseMessage CheckMeterNoExists(HttpRequestMessage request, string meterno, int resourceAllocationID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _resourceallocationservice.CheckMeterNoExists(meterno, resourceAllocationID);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }



        [Authorize]
        [HttpGet]
        [Route("api/VerifyResourceAllocationDetails/{OperationType}/{MovementType}/{ResourceAllocationID}")]
        public HttpResponseMessage VerifyResourceAllocationformDetails(HttpRequestMessage request, string OperationType, string MovementType, string ResourceAllocationID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string resourceallocationdetails = _resourceallocationservice.VerifyResourceAllocationDetails(OperationType, MovementType, ResourceAllocationID);
                response = request.CreateResponse<string>(HttpStatusCode.Created, resourceallocationdetails);
                return response;
            });
        }


        /// <summary>
        /// VCN Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/ServiceRecordingVcnDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage GetArrivalVcnDetailsforAutocomplete(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenuePostingVO> arrivalcommodities = _resourceallocationservice.ServiceRecordingVcnDetailsforAutocomplete(searchValue);
                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, arrivalcommodities);
                return response;
            });
        }

        /// <summary>
        /// Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
         [Route("api/ServiceRecordingVesselDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage ServiceRecordingVesselDetailsforAutocomplete(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<VesselVO> Vessels = _resourceallocationservice.ServiceRecordingVesselDetailsforAutocomplete(searchvalue);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }

        //ServiceRecordingResourceDetailsforAutocomplete
         /// <summary>
         /// Vessel / IMO No. Autocomplete for Search
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
         /// 
         [Route("api/ServiceRecordingResourceDetailsforAutocomplete")]
         [HttpGet]
         public HttpResponseMessage ServiceRecordingResourceDetailsforAutocomplete(HttpRequestMessage request)
         {

             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                 string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                 List<UserMasterVO> Vessels = _resourceallocationservice.ServiceRecordingResourceDetailsforAutocomplete(searchvalue);
                 response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, Vessels);
                 return response;
             });
         }
    }
}
