using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IWorkFlowTaskService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        WorkFlowTaskReferenceVO GetWorkFlowTaskReferenceVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<WorkFlowTaskVO> GetWorkFlowTasks();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        EntityVO AddWorkFlowTask(EntityVO value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        EntityVO ModifyWorkFlowTask(EntityVO value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EntityVO> GetWorkFlowTask();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode);     
    }
}
