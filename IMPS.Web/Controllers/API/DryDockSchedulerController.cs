using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class DryDockSchedulerController : ApiControllerBase
    {
        IDryDockSchedulerService _dryDockSchedulerService;
        public DryDockSchedulerController()
        {
            _dryDockSchedulerService = new DryDockSchedulerClient();
        }

        [Authorize]
        [Route("api/PendingVesselForDryDock")]
        [HttpGet]
        public HttpResponseMessage GetPendingVesselForDryDock(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppDryDockVO> vcnDet = _dryDockSchedulerService.GetPendingVesselForDryDock();
                response = request.CreateResponse(HttpStatusCode.OK, vcnDet);

                return response;
            });
        }

        [Authorize]
        [Route("api/GetDockList")]
        [HttpGet]
        public HttpResponseMessage GetDockList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> vcnDet = _dryDockSchedulerService.GetDockList();
                response = request.CreateResponse(HttpStatusCode.OK, vcnDet);

                return response;
            });
        }


        [Route("api/ScheduleDryDock")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PutScheduleDryDock(HttpRequestMessage request, SuppDryDockVO suppDryDock)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppDryDock suppDoc = _dryDockSchedulerService.AddScheduleDryDock(suppDryDock);
                response = request.CreateResponse<SuppDryDock>(HttpStatusCode.Created, suppDoc);
                return response;
            });
        }

        [Route("api/ConfirmedScheduleDryDocks")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage ConfirmedScheduleDryDock(HttpRequestMessage request,List<SuppScheduledDryDockVO> suppDryDock)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppScheduledDryDockVO suppDoc = _dryDockSchedulerService.ConfirmedScheduleDryDock(suppDryDock);
                response = request.CreateResponse<SuppScheduledDryDockVO>(HttpStatusCode.Created, suppDoc);
                return response;
            });
        }



        [Route("api/UnPlannedScheduleDryDock")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage UnPlannedScheduleDryDock(HttpRequestMessage request, SuppDryDockVO suppDryDock)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppDryDock suppDoc = _dryDockSchedulerService.UnPlannedScheduleDryDock(suppDryDock);
                response = request.CreateResponse<SuppDryDock>(HttpStatusCode.Created, suppDoc);
                return response;
            });
        }

        //[Authorize]
        //[Route("api/ScheduledVesselForDryDock")]
        //[HttpGet]
        //public HttpResponseMessage GetScheduledVesselForDryDock(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        List<SuppDryDockVO> vcnDet = _dryDockSchedulerService.GetScheduledVesselForDryDock();
        //        response = request.CreateResponse(HttpStatusCode.OK, vcnDet);

        //        return response;
        //    });
        //}

        [Authorize]
        [Route("api/ScheduledVesselForDryDock/{dockcode}/{quaycode}")]
        [HttpGet]
        public HttpResponseMessage GetScheduledVesselForDryDock(HttpRequestMessage request, string dockcode, string quaycode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppDryDockVO> vcnDet = _dryDockSchedulerService.GetScheduledVesselForDryDock(dockcode, quaycode);
                response = request.CreateResponse(HttpStatusCode.OK, vcnDet);

                return response;
            });
        }


    }
}
