using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public interface IEventSchedulerRepository
    {
        List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData, int userId);
        List<EventScheduleVO> GetEventScheduler();
        List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData, int userId);
        string DeleteEventScheduler(EventScheduleVO eventSchedulerData, int userId);

        
    }
}
