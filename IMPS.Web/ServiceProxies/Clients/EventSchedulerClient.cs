using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class EventSchedulerClient : UserClientBase<IEventSchedulerService>, IEventSchedulerService
    {
        public List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return WrapOperationWithException(() => Channel.AddEventScheduler(eventSchedulerData));
        }

        public List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return WrapOperationWithException(() => Channel.ModifyEventScheduler(eventSchedulerData));
        }

        public string DeleteEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return WrapOperationWithException(() => Channel.DeleteEventScheduler(eventSchedulerData));
        }
       
        
        public List<EventScheduleVO> GetEventScheduler()
        {
            return WrapOperationWithException(() => Channel.GetEventScheduler());
        }
      
    }
}