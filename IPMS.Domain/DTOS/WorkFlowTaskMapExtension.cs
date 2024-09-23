using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class WorkFlowTaskMapExtension
    {
        #region MapToDTO
        public static List<WorkFlowTaskVO> MapToDTO(this List<WorkflowTask> wftasks)
        {
            List<WorkFlowTaskVO> wftVos = new List<WorkFlowTaskVO>();
            foreach (var serviceRequest in wftasks)
            {
                wftVos.Add(serviceRequest.MapToDTO());

            }
            return wftVos;
        }

        public static WorkFlowTaskVO MapToDTO(this WorkflowTask data)
        {
            WorkFlowTaskVO workflowtaskVO = new WorkFlowTaskVO();

            workflowtaskVO.EntityID = data.EntityID;
            workflowtaskVO.CreatedBy = data.CreatedBy;
            workflowtaskVO.CreatedDate = data.CreatedDate;
            workflowtaskVO.ModifiedBy = data.ModifiedBy;
            workflowtaskVO.ModifiedDate = data.ModifiedDate;
            workflowtaskVO.RecordStatus = data.RecordStatus;

            workflowtaskVO.WorkflowTaskCode = data.WorkflowTaskCode;
            workflowtaskVO.Step = data.Step;
            workflowtaskVO.NextStep = data.NextStep;
            workflowtaskVO.ValidityPeriod = data.ValidityPeriod;
            workflowtaskVO.HasNotification = data.HasNotification;
            workflowtaskVO.HasRemarks = data.HasRemarks;
            workflowtaskVO.APIUrl = data.APIUrl;

            return workflowtaskVO;
        }
        #endregion

        #region MapToEntity
        public static WorkflowTask MapToEntity(this WorkFlowTaskVO vo)
        {
            WorkflowTask workflowtask = new WorkflowTask();

            workflowtask.EntityID = vo.EntityID;
            workflowtask.CreatedBy = vo.CreatedBy;
            workflowtask.CreatedDate = vo.CreatedDate;
            workflowtask.ModifiedBy = vo.ModifiedBy;
            workflowtask.ModifiedDate = vo.ModifiedDate;
            workflowtask.RecordStatus = vo.RecordStatus;

            workflowtask.WorkflowTaskCode = vo.WorkflowTaskCode;
            workflowtask.Step = vo.Step;
            workflowtask.NextStep = vo.NextStep;
            workflowtask.ValidityPeriod = vo.ValidityPeriod;
            workflowtask.HasNotification = vo.HasNotification;
            workflowtask.HasRemarks = vo.HasRemarks;
            workflowtask.APIUrl = vo.APIUrl;

            return workflowtask;
        }

        public static List<WorkflowTask> MapToEntity(this List<WorkFlowTaskVO> WFTList)
        {
            List<WorkflowTask> wftList = new List<WorkflowTask>();
            if (WFTList != null)
                foreach (var data in WFTList)
                {
                    wftList.Add(data.MapToEntity());
                }
            return wftList;
        }
        #endregion

        #region MapToEntityForSetWFTaskRoles
        public static WorkFlowTaskUpdateVO MapToEntityForSetWFTaskRoles(this WorkFlowTaskVO vo)
        {
            WorkFlowTaskUpdateVO workflowtask = new WorkFlowTaskUpdateVO();

            workflowtask.EntityID = vo.EntityID;
            workflowtask.WorkflowTaskCode = vo.WorkflowTaskCode;
            workflowtask.Step = vo.Step;
            workflowtask.NextStep = vo.NextStep;
            workflowtask.ValidityPeriod = vo.ValidityPeriod;
            workflowtask.HasNotification = vo.HasNotification;
            workflowtask.HasRemarks = vo.HasRemarks;
            workflowtask.APIUrl = vo.APIUrl;
            workflowtask.RoleID = vo.RoleID;
            workflowtask.PortCode = vo.PortCode;

            workflowtask.CreatedBy = vo.CreatedBy;
            workflowtask.CreatedDate = vo.CreatedDate;
            workflowtask.ModifiedBy = vo.ModifiedBy;
            workflowtask.ModifiedDate = vo.ModifiedDate;
            workflowtask.RecordStatus = vo.RecordStatus;

            workflowtask.arrayRoles = vo.arrayRoles;

            if (vo.arrayRoles.Count() > 0 && vo.arrayRoles.Count() != null)
            {
                var hasNoValue = false;

                foreach (var item in vo.arrayRoles)
                {
                    int val = int.Parse(item, CultureInfo.InvariantCulture);
                    if (val == 0)
                    {
                        hasNoValue = true;
                    }
                }

                if (!hasNoValue)
                {
                    workflowtask.CommaSeperatedRoleIDs = String.Join(",", vo.arrayRoles);
                }
            }
            return workflowtask;
        }

        public static List<WorkFlowTaskUpdateVO> MapToEntityForSetWFTaskRoles(this List<WorkFlowTaskVO> WFTList)
        {
            List<WorkFlowTaskUpdateVO> wftList = new List<WorkFlowTaskUpdateVO>();
            if (WFTList != null)
            {
                foreach (var data in WFTList)
                {
                    wftList.Add(data.MapToEntityForSetWFTaskRoles());
                }
            }
            return wftList;
        }
        #endregion
    }
}
