using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class EventScheduleMapExtension
    {
        public static EventScheduleVO MapToDTO(this EventSchedule data)
        {
            EventScheduleVO EventScheduleVo = new EventScheduleVO();
            if (data != null)
            {
                EventScheduleVo.EventScheduleID = data.EventScheduleID;
                EventScheduleVo.EventScheduleType = data.EventScheduleType;
                EventScheduleVo.EventScheduleName = data.EventScheduleName;
                EventScheduleVo.EventScheduleStartDate = data.EventScheduleStartDate;
                EventScheduleVo.EventScheduleTime = data.EventScheduleTime;
                EventScheduleVo.ExecutionPlan = data.ExecutionPlan;
                EventScheduleVo.NextExecutionDateTime = data.NextExecutionDateTime;
                EventScheduleVo.EventScheduleEndDateTime = data.EventScheduleEndDateTime;
                EventScheduleVo.ExecutionCount = data.ExecutionCount;
                EventScheduleVo.LastExecutionDateTime = data.LastExecutionDateTime;
                EventScheduleVo.RecordStatus = data.RecordStatus;
                EventScheduleVo.CreatedBy = data.CreatedBy;
                EventScheduleVo.CreatedDate = data.CreatedDate;
                EventScheduleVo.ModifiedBy = data.ModifiedBy;
                EventScheduleVo.ModifiedDate = data.ModifiedDate;
                EventScheduleVo.EntityID = data.EntityID;
                if (data.Entity != null)
                {
                    EventScheduleVo.Entity = data.Entity.MapToDto();
                }
            }
            return EventScheduleVo;
        }
        public static EventSchedule MapToEntity(this EventScheduleVO vo)
        {
            EventSchedule EventSchedule = new EventSchedule();
            if (vo != null)
            {
                EventSchedule.EventScheduleID = vo.EventScheduleID;
                EventSchedule.EventScheduleType = vo.EventScheduleType;
                EventSchedule.EventScheduleName = vo.EventScheduleName;
                EventSchedule.EventScheduleStartDate = vo.EventScheduleStartDate;
                EventSchedule.EventScheduleTime = vo.EventScheduleTime;
                EventSchedule.ExecutionPlan = vo.ExecutionPlan;
                EventSchedule.NextExecutionDateTime = vo.NextExecutionDateTime;
                EventSchedule.EventScheduleEndDateTime = vo.EventScheduleEndDateTime;
                EventSchedule.ExecutionCount = vo.ExecutionCount;
                EventSchedule.LastExecutionDateTime = vo.LastExecutionDateTime;
                EventSchedule.RecordStatus = vo.RecordStatus;
                EventSchedule.CreatedBy = vo.CreatedBy;
                EventSchedule.CreatedDate = vo.CreatedDate;
                EventSchedule.ModifiedBy = vo.ModifiedBy;
                EventSchedule.ModifiedDate = vo.ModifiedDate;
                EventSchedule.EntityID = vo.EntityID;
                if (vo.Entity != null)
                {
                    EventSchedule.Entity = vo.Entity.MapToEntity();
                }
            }
            return EventSchedule;
        }

        public static List<EventSchedule> MapToEntity(this List<EventScheduleVO> vos)
        {
            List<EventSchedule> EventScheduleEntities = new List<EventSchedule>();
            if (vos != null)
            {
                foreach (var EventSchedulevo in vos)
                {
                    if (EventSchedulevo != null)
                    {
                        EventScheduleEntities.Add(EventSchedulevo.MapToEntity());
                    }
                }
            }
            return EventScheduleEntities;
        }



        public static List<EventScheduleVO> MapToDTO(this List<EventSchedule> EventSchedules)
        {

            List<EventScheduleVO> EventScheduleVos = new List<EventScheduleVO>();
            foreach (var EventSchedule in EventSchedules)
            {
                EventScheduleVos.Add(EventSchedule.MapToDTO());

            }
            return EventScheduleVos;
          
        }
    }
}
