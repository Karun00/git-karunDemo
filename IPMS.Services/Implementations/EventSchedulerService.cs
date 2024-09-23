using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using log4net;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Domain.DTOS;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EventSchedulerService : ServiceBase, IEventSchedulerService
    {
       
       // private readonly ILog log;
        private IEventSchedulerRepository _eventschedulerRepository;

     

        public EventSchedulerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventschedulerRepository = new EventSchedulerRepository(_unitOfWork);
            _UserId =GetUserIdByLoginname(_LoginName);
        }

        public EventSchedulerService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
           // log = LogManager.GetLogger(typeof(ElectronicNotificationsService));
            _eventschedulerRepository = new EventSchedulerRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                return _eventschedulerRepository.AddEventScheduler(eventSchedulerData, _UserId);
            });
        }

        public List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                return _eventschedulerRepository.ModifyEventScheduler(eventSchedulerData, _UserId);
            });
        }

        public List<EventScheduleVO> GetEventScheduler()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                return _eventschedulerRepository.GetEventScheduler();
            });
        }

        public string DeleteEventScheduler(EventScheduleVO eventSchedulerData)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                return _eventschedulerRepository.DeleteEventScheduler(eventSchedulerData, _UserId);
            });
        }

       

        public void Dispose()
        {
        }
    }
}
