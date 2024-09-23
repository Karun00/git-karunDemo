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
    public class MobileScheduledTasksController : ApiControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetScheduledTasks(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ScheduledTasksVO> scheduledtasks = null;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    scheduledtasks = _scheduleservice.GetScheduledTasks();
                }

                response = request.CreateResponse<List<ScheduledTasksVO>>(HttpStatusCode.OK, scheduledtasks);
                return response;
            });
        }



        [Authorize]
        [HttpPut]
        public HttpResponseMessage PutScheduledTasks(HttpRequestMessage request, ResourceAllocationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int resourceAllocationID = 0;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    resourceAllocationID = _scheduleservice.ModifyScheduledTasks(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, resourceAllocationID);
                return response;
            });
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetMobileScheduledTaskViewDetails(HttpRequestMessage request, int id)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<String> scheduledtasks = null;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    scheduledtasks = _scheduleservice.GetMobileScheduledTaskViewDetails(id);
                }

                response = request.CreateResponse<List<String>>(HttpStatusCode.OK, scheduledtasks);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetMobileResourceAllowTaskExecution(HttpRequestMessage request, int id)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ScheduledTaskExecutionVO> scheduledtaskexecution = null;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    scheduledtaskexecution = _scheduleservice.GetMobileResourceAllowTaskExecution(id);
                }

                response = request.CreateResponse<List<ScheduledTaskExecutionVO>>(HttpStatusCode.OK, scheduledtaskexecution);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostMobileScheduledTaskExecution(HttpRequestMessage request, ScheduledTaskExecutionVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int id = 0;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    id = _scheduleservice.PostMobileScheduledTaskExecution(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, id);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostPilotageTaskExecution(HttpRequestMessage request, PilotageServiceRecordingVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int id = 0;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    id = _scheduleservice.PostPilotageTaskExecution(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, id);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostBerthingDetails(HttpRequestMessage request, ShiftingBerthingTaskExecutionVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int id = 0;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    id = _scheduleservice.PostBerthingDetails(value);
                }
                //Mahesh: For Planned Movements auto refresh on desktop
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                //End
                response = request.CreateResponse<int>(HttpStatusCode.OK, id);
                return response;
            });
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetBerthswithBollards(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berthsnbollards = null;
                using (IBerthService _berthService = new BerthClient())
                {
                    berthsnbollards = _berthService.GetBerthsWithBollards(); ;
                }

                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berthsnbollards);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetBerthingSide(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryCodeNameVO> berthingSide = null;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    berthingSide = _scheduleservice.GetBerthingSide();
                }

                response = request.CreateResponse<List<SubCategoryCodeNameVO>>(HttpStatusCode.OK, berthingSide);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostTugOrWorkBoatTaskExecution(HttpRequestMessage request, OtherServiceRecordingVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int id = 0;
                using (IMobileScheduledTasksService _scheduleservice = new MobileScheduledTasksClient())
                {
                    id = _scheduleservice.PostTugOrWorkBoatTaskExecution(value);
                }

                response = request.CreateResponse<int>(HttpStatusCode.OK, id);
                return response;
            });
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetBerths(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berths = null;
                using (IBerthService _berthService = new BerthClient())
                {
                    berths = _berthService.GetBerthsWithPortCode(); ;
                }

                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
