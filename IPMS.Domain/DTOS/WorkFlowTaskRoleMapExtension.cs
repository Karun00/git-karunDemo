using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class WorkFlowTaskRoleMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO 
        /// </summary>
        /// <param name="workflowtaskrole"></param>
        /// <returns></returns>
        public static List<WorkFlowTaskRoleVO> MapToDTO(this IEnumerable<WorkflowTaskRole> workflowtaskrole)
        {
            var workflowtaskroleVOList = new List<WorkFlowTaskRoleVO>();
            foreach (var item in workflowtaskrole)
            {
                workflowtaskroleVOList.Add(item.MapToDTO());
            }
            return workflowtaskroleVOList;
        }

        /// <summary>
        ///  Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="workflowtaskroleVOList"></param>
        /// <returns></returns>
        public static List<WorkflowTaskRole> MapToEntity(this IEnumerable<WorkFlowTaskRoleVO> workflowtaskroleVOList)
        {

            var workflowtaskroles = new List<WorkflowTaskRole>();
            foreach (var item in workflowtaskroleVOList)
            {
                workflowtaskroles.Add(item.MapToEntity());
            }
            return workflowtaskroles;
        }

        /// <summary>
        ///  Data Transfer from Entity to DTO        
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static WorkFlowTaskRoleVO MapToDTO(this WorkflowTaskRole data)
        {
            return new WorkFlowTaskRoleVO
            {
                EntityID = data.EntityID,
                Step = data.Step,
                RoleID = data.RoleID,
                RecordStatus = data.RecordStatus,
                PortCode = data.PortCode,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static WorkflowTaskRole MapToEntity(this WorkFlowTaskRoleVO data)
        {
            return new WorkflowTaskRole
            {
                EntityID = data.EntityID,
                Step = data.Step,
                RoleID = data.RoleID,
                RecordStatus = data.RecordStatus,
                PortCode = data.PortCode,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }
    }
}
