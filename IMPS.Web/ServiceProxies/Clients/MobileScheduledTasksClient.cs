using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class MobileScheduledTasksClient : UserClientBase<IMobileScheduledTasksService>, IMobileScheduledTasksService
    {
        public List<ScheduledTasksVO> GetScheduledTasks()
        {
            return WrapOperationWithException(() => Channel.GetScheduledTasks());
        }

        public int ModifyScheduledTasks(ResourceAllocationVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyScheduledTasks(data));
        }

        public List<String> GetMobileScheduledTaskViewDetails(int id)
        {
            return WrapOperationWithException(() => Channel.GetMobileScheduledTaskViewDetails(id));
        }

        public List<ScheduledTaskExecutionVO> GetMobileResourceAllowTaskExecution(int id)
        {
            return WrapOperationWithException(() => Channel.GetMobileResourceAllowTaskExecution(id));
        }

        public int PostMobileScheduledTaskExecution(ScheduledTaskExecutionVO data)
        {
            return WrapOperationWithException(() => Channel.PostMobileScheduledTaskExecution(data));
        }

        public int PostPilotageTaskExecution(PilotageServiceRecordingVO data)
        {
            return WrapOperationWithException(() => Channel.PostPilotageTaskExecution(data));
        }

        public List<SubCategoryCodeNameVO> GetBerthingSide()
        {
            return WrapOperationWithException(() => Channel.GetBerthingSide());
        }

        public int PostBerthingDetails(ShiftingBerthingTaskExecutionVO data)
        {
            return WrapOperationWithException(() => Channel.PostBerthingDetails(data));
        }

        public int PostTugOrWorkBoatTaskExecution(OtherServiceRecordingVO data)
        {
            return WrapOperationWithException(() => Channel.PostTugOrWorkBoatTaskExecution(data));
        }
    }
}
