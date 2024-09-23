using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMobileScheduledTasksService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ScheduledTasksVO> GetScheduledTasks();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ModifyScheduledTasks(ResourceAllocationVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<String> GetMobileScheduledTaskViewDetails(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ScheduledTaskExecutionVO> GetMobileResourceAllowTaskExecution(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int PostMobileScheduledTaskExecution(ScheduledTaskExecutionVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int PostPilotageTaskExecution(PilotageServiceRecordingVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameVO> GetBerthingSide();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int PostBerthingDetails(ShiftingBerthingTaskExecutionVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int PostTugOrWorkBoatTaskExecution(OtherServiceRecordingVO data);
    }
}
