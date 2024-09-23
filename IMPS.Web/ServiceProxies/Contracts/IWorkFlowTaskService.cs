using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IWorkFlowTaskService : IDisposable
    {

        [OperationContract]
        WorkFlowTaskReferenceVO GetWorkFlowTaskReferenceVO();

        //[OperationContract]
        //Task<WorkFlowTaskReferenceVO> GetWorkFlowTaskReferenceVOAsync();


        [OperationContract]
        List<WorkFlowTaskVO> GetWorkFlowTasks();

        //[OperationContract]
        //Task<List<WorkFlowTaskVO>> GetWorkFlowTasksAsync();


        [OperationContract]
        EntityVO AddWorkFlowTask(EntityVO value);

        //[OperationContract]
        //Task<EntityVO> AddWorkFlowTaskAsync(EntityVO value);


        [OperationContract]
        EntityVO ModifyWorkFlowTask(EntityVO value);

        //[OperationContract]
        //Task<EntityVO> ModifyWorkFlowTaskAsync(EntityVO value);
          
        [OperationContract]
        IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID);

        //[OperationContract]
        //Task<IEnumerable<PendingTaskVO>> GetWorkFlowTaskActionAsync(string ReferenceID, int WorkflowInstanceID);

         [OperationContract]
         List<EntityVO> GetWorkFlowTask();

         //[OperationContract]
         //Task<List<EntityVO>> GetWorkFlowTaskAsync();

         [OperationContract]
         PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode);

         //[OperationContract]
         //Task<PendingTaskVO> GetWorkFlowTaskStatusAsync(string ReferenceID, int WorkflowInstanceId, string TaskCode);

    }
}
