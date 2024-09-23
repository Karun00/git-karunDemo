using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class WorkFlowTaskClient : UserClientBase<IWorkFlowTaskService>, IWorkFlowTaskService
    {


        public WorkFlowTaskReferenceVO GetWorkFlowTaskReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowTaskReferenceVO());
        }

        //public Task<WorkFlowTaskReferenceVO> GetWorkFlowTaskReferenceVOAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkFlowTaskReferenceVOAsync());
        //}


        public List<WorkFlowTaskVO> GetWorkFlowTasks()
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowTasks());
        }

        //public Task<List<WorkFlowTaskVO>> GetWorkFlowTasksAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkFlowTasksAsync());
        //}


        public EntityVO AddWorkFlowTask(EntityVO value)
        {
            return WrapOperationWithException(() => Channel.AddWorkFlowTask(value));
        }

        //public Task<EntityVO> AddWorkFlowTaskAsync(EntityVO value)
        //{
        //    return WrapOperationWithException(() => Channel.AddWorkFlowTaskAsync(value));
        //}


        public EntityVO ModifyWorkFlowTask(EntityVO value)
        {
            return WrapOperationWithException(() => Channel.ModifyWorkFlowTask(value));
        }

        //public Task<EntityVO> ModifyWorkFlowTaskAsync(EntityVO value)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyWorkFlowTaskAsync(value));
        //}

        public IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID)
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowTaskAction(ReferenceID, WorkflowInstanceID));
        }

        //public Task<IEnumerable<PendingTaskVO>> GetWorkFlowTaskActionAsync(string ReferenceID, int WorkflowInstanceID)
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkFlowTaskActionAsync(ReferenceID, WorkflowInstanceID));
        //}


        public List<EntityVO> GetWorkFlowTask()
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowTask());
        }

        //public Task<List<EntityVO>> GetWorkFlowTaskAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkFlowTaskAsync());
        //}


        public PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode)
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowTaskStatus(ReferenceID, WorkflowInstanceId, TaskCode));
        }

        //public Task<PendingTaskVO> GetWorkFlowTaskStatusAsync(string ReferenceID, int WorkflowInstanceId, string TaskCode)
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkFlowTaskStatusAsync(ReferenceID, WorkflowInstanceId, TaskCode));
        //}


    }
}