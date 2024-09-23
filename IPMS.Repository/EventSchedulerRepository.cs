using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using IPMS.Domain.DTOS;
using IPMS.Domain.ValueObjects;
using System.Globalization;


namespace IPMS.Repository
{
    public class EventSchedulerRepository : IEventSchedulerRepository
    {
        private IUnitOfWork _unitOfWork;
      //  private readonly ILog log;

        public EventSchedulerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
          //  log = LogManager.GetLogger(typeof(EventSchedulerRepository));

        }

        public List<EventScheduleVO> GetEventScheduler()
        {
            var eventscheduler = new List<EventScheduleVO>();

            //try
            //{
                 eventscheduler = (from es in _unitOfWork.Repository<EventSchedule>().Query().Include(e => e.Entity).Select()
                                      where es.RecordStatus == "A"
                                      select es).ToList().MapToDTO();
                
            //}
            //catch (Exception ex)
            //{
                //log.Error("Exception " + ex.Message);
                
            //}

            return eventscheduler;
        }

        public List<EventScheduleVO> AddEventScheduler(EventScheduleVO eventSchedulerData, int userId)
        {
            if (eventSchedulerData != null)
            {
            //try
            //{
           
                EventSchedule esdata = null;
                esdata = eventSchedulerData.MapToEntity();
                esdata.EventScheduleTime = Convert.ToDateTime(eventSchedulerData.EventScheduleTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture);
                esdata.EventScheduleStartDate = Convert.ToDateTime(eventSchedulerData.EventScheduleStartDate, CultureInfo.InvariantCulture).Date;
                esdata.ObjectState = ObjectState.Added;
                esdata.CreatedBy = userId;
                esdata.ModifiedBy = userId;
                esdata.CreatedDate = DateTime.Now;
                esdata.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<EventSchedule>().Insert(esdata);
                _unitOfWork.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Exception " + ex.Message);

                //}
            }
            return GetEventScheduler();
        }

        public List<EventScheduleVO> ModifyEventScheduler(EventScheduleVO eventSchedulerData, int userId)
        {
            if (eventSchedulerData != null)
            {
                //try
                //{
                EventSchedule esdata = null;
                esdata = eventSchedulerData.MapToEntity();
                esdata.EventScheduleStartDate = Convert.ToDateTime(eventSchedulerData.EventScheduleStartDate, CultureInfo.InvariantCulture).Date;
                esdata.EventScheduleTime = Convert.ToDateTime(eventSchedulerData.EventScheduleTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture);
                esdata.ObjectState = ObjectState.Modified;
                //  esdata.CreatedBy = userid;
                esdata.ModifiedBy = userId;
                //  esdata.CreatedDate = DateTime.Now;
                esdata.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<EventSchedule>().Update(esdata);
                _unitOfWork.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Exception " + ex.Message);

                //}
            }
            return GetEventScheduler();
        }

        public string DeleteEventScheduler(EventScheduleVO eventSchedulerData, int userId)
        {
            if (eventSchedulerData != null)
            {
                //try
                //{
                EventSchedule esdata = null;
                esdata = eventSchedulerData.MapToEntity();

                esdata.ObjectState = ObjectState.Modified;
                esdata.RecordStatus = "I";
                esdata.ModifiedBy = userId;
                esdata.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<EventSchedule>().Update(esdata);
                _unitOfWork.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Exception " + ex.Message);

                //}
            }
            return eventSchedulerData.EventScheduleName;
        }

    }
}
