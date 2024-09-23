using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IWorkFlowTaskRepository
    {
        List<WorkflowTask> GetWorkFlowTasks();
        WorkflowTask GetWorkflowTaskByEntity(string p_EntityCode);
        WorkflowTask GeCurrentTaskByEntityandReferance(string p_EntityCode, string p_ReferenceID);
        WorkflowTask GetNextStepTaskByEntityandReferance(string p_EntityCode, string p_ReferenceID);
        int GetRequestStatus(string p_entitycode, string p_referenceno);
        IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID, int UserID);
        EntityVO InsertOrUpdateWorkFlowTask(EntityVO entityvalue, int userid, string portcode, bool isUPdate);
        List<EntityVO> GetWorkFlowTask(string portcode);
        PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode);     
    }
}
