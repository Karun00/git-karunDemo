using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMobileScheduledTasksService : IDisposable
    {
        [OperationContract]
        List<ScheduledTasksVO> GetScheduledTasks();

        [OperationContract]
        int ModifyScheduledTasks(ResourceAllocationVO data);

        [OperationContract]
        List<String> GetMobileScheduledTaskViewDetails(int id);

        [OperationContract]
        List<ScheduledTaskExecutionVO> GetMobileResourceAllowTaskExecution(int id);

        [OperationContract]
        int PostMobileScheduledTaskExecution(ScheduledTaskExecutionVO data);

        [OperationContract]
        int PostPilotageTaskExecution(PilotageServiceRecordingVO data);

        [OperationContract]
        List<SubCategoryCodeNameVO> GetBerthingSide();

        [OperationContract]
        int PostBerthingDetails(ShiftingBerthingTaskExecutionVO data);

        [OperationContract]
        int PostTugOrWorkBoatTaskExecution(OtherServiceRecordingVO data);

    }
}

