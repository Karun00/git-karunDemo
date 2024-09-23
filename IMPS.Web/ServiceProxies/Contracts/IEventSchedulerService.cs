using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IEventSchedulerService : IDisposable
    {
        [OperationContract]
        List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData);

        [OperationContract]
        List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData);    
    
        [OperationContract]
        string DeleteEventScheduler(EventScheduleVO eventSchedulerData); 
        
        [OperationContract]
        List<EventScheduleVO> GetEventScheduler();
    }
}