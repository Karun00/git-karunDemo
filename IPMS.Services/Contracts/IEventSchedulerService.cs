using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IEventSchedulerService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EventScheduleVO> GetEventScheduler();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData);
       
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string DeleteEventScheduler(EventScheduleVO eventSchedulerData);
        
    }
}
