using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace IPMS.Domain.DTOS
{
    public static class WorkFlowInstanceMapExtensions
    {
        public static WorkFlowInstanceVO MapToDTO(this WorkflowInstance data)
        {
            WorkFlowInstanceVO workflowInstancevo = new WorkFlowInstanceVO();
            workflowInstancevo.WorkflowInstanceId = data.WorkflowInstanceId;
            workflowInstancevo.EntityID = data.EntityID;
            workflowInstancevo.PortCode = data.PortCode;
            workflowInstancevo.ReferenceID = data.ReferenceID;
            workflowInstancevo.WorkflowTaskCode = data.WorkflowTaskCode;
            workflowInstancevo.RecordStatus = data.RecordStatus;
        
            workflowInstancevo.CreatedBy = data.CreatedBy;
            workflowInstancevo.CreatedDate = data.CreatedDate;
            workflowInstancevo.ModifiedBy = data.ModifiedBy;
            workflowInstancevo.ModifiedDate = data.ModifiedDate;
            return workflowInstancevo;
        }
        public static WorkflowInstance MapToEntity(this WorkFlowInstanceVO vo)
        {
            WorkflowInstance workflowInstance = new WorkflowInstance();
            workflowInstance.WorkflowInstanceId = vo.WorkflowInstanceId;
            workflowInstance.EntityID = vo.EntityID;
            workflowInstance.PortCode = vo.PortCode;
            workflowInstance.ReferenceID = vo.ReferenceID;
            workflowInstance.WorkflowTaskCode = vo.WorkflowTaskCode;
            workflowInstance.RecordStatus = vo.RecordStatus;
            workflowInstance.CreatedBy = vo.CreatedBy;
            workflowInstance.CreatedDate = vo.CreatedDate;
            workflowInstance.ModifiedBy = vo.ModifiedBy;
            workflowInstance.ModifiedDate = vo.ModifiedDate;
            return workflowInstance;
        }
    }
}
