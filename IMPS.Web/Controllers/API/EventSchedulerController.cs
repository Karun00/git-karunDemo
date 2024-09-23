using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Controllers.API
{
    public class EventSchedulerController : ApiControllerBase
    {
    //    ILog log = log4net.LogManager.GetLogger(typeof(EventSchedulerController));
      //  IEventSchedulerService _eventscheduleservice;
          public EventSchedulerController()
        {
           // _eventscheduleservice = new EventSchedulerClient();
          
        }

        [Route("api/EventSchedulers")]        
        [HttpPost]
          public HttpResponseMessage PostEventScheduler(HttpRequestMessage request, EventScheduleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<EventScheduleVO> eventschedule = null;

                using (IEventSchedulerService ls = new EventSchedulerClient())
                {
                    eventschedule = ls.AddEventScheduler(value);
                }
                response = request.CreateResponse<List<EventScheduleVO>>(HttpStatusCode.OK, eventschedule);
                return response;
            });
        }

        [Route("api/EventSchedulers")]
        [HttpPut]
        public HttpResponseMessage PutEventScheduler(HttpRequestMessage request, EventScheduleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<EventScheduleVO> eventschedule = null;

                using (IEventSchedulerService ls = new EventSchedulerClient())
                {
                    eventschedule = ls.ModifyEventScheduler(value);
                }
                response = request.CreateResponse<List<EventScheduleVO>>(HttpStatusCode.OK, eventschedule);
                return response;
            });
        }

        [Route("api/EventSchedulers")]
        [HttpDelete]
        public HttpResponseMessage PutDeleteEventScheduler(HttpRequestMessage request, EventScheduleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string eventschedule = null;

                using (IEventSchedulerService ls = new EventSchedulerClient())
                {
                    eventschedule = ls.DeleteEventScheduler(value);
                }
                response = request.CreateResponse<string>(HttpStatusCode.OK, eventschedule);
                return response;
            });
        }

        [Route("api/EventSchedulers")]
        [HttpGet]
        public HttpResponseMessage GetEventScheduler(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<EventScheduleVO> eventschedulelist = null;

                using (IEventSchedulerService ls = new EventSchedulerClient())
                {
                    eventschedulelist = ls.GetEventScheduler();
                }
                response = request.CreateResponse<List<EventScheduleVO>>(HttpStatusCode.OK, eventschedulelist);
                return response;
            });
        }
    }
}
