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
    public static class ResourceAllocationMapExtension
    {
        public static ResourceAllocationVO MapToDTO(this ResourceAllocation data)
        {
            ResourceAllocationVO resourceallocationVo = new ResourceAllocationVO();
            if (data != null)
            {
                resourceallocationVo.ResourceAllocationID = data.ResourceAllocationID;
                resourceallocationVo.ServiceReferenceType = data.ServiceReferenceType;
                resourceallocationVo.ServiceReferenceID = data.ServiceReferenceID;
                resourceallocationVo.ServiceTypeCode = data.OperationType;
                resourceallocationVo.OperationType = data.OperationType;
                resourceallocationVo.ResourceID = data.ResourceID ?? null;
                resourceallocationVo.ResourceType = data.ResourceType;
                resourceallocationVo.StartTime = data.StartTime;
                //resourceallocationVo.ActualScheduledTime = data.ActualScheduledTime;
                resourceallocationVo.EndTime = data.EndTime;
                resourceallocationVo.TaskStatus = data.TaskStatus;
                resourceallocationVo.RecordStatus = data.RecordStatus;
                resourceallocationVo.CreatedBy = data.CreatedBy;
                resourceallocationVo.CreatedDate = data.CreatedDate;
                resourceallocationVo.ModifiedBy = data.ModifiedBy;
                resourceallocationVo.ModifiedDate = data.ModifiedDate;
                resourceallocationVo.AcknowledgeDate = data.AcknowledgeDate;
                resourceallocationVo.Remarks = data.Remarks;
                resourceallocationVo.ShiftingBerthingTaskExecution = data.ShiftingBerthingTaskExecutions.MapToDTOObj();
                resourceallocationVo.PilotageServiceRecording = data.PilotageServiceRecordings.MapToDTOObj();
                resourceallocationVo.OtherServiceRecording = data.OtherServiceRecordings.MapToDTOObj();
            }

            return resourceallocationVo;
        }

        public static ResourceAllocationVO MapToDTO(this ResourceAllocation data, string MovementType)
        {
            ResourceAllocationVO resourceallocationVo = new ResourceAllocationVO();
            if (data != null)
            {
                resourceallocationVo.ResourceAllocationID = data.ResourceAllocationID;
                resourceallocationVo.ServiceReferenceType = data.ServiceReferenceType;
                resourceallocationVo.ServiceReferenceID = data.ServiceReferenceID;
                resourceallocationVo.ServiceTypeCode = data.OperationType;
                resourceallocationVo.OperationType = data.OperationType;
                resourceallocationVo.ResourceID = data.ResourceID ?? null;
                resourceallocationVo.ResourceType = data.ResourceType;
                resourceallocationVo.StartTime = data.StartTime;
                //resourceallocationVo.ActualScheduledTime = data.ActualScheduledTime;
                resourceallocationVo.EndTime = data.EndTime;
                resourceallocationVo.TaskStatus = data.TaskStatus;
                resourceallocationVo.RecordStatus = data.RecordStatus;
                resourceallocationVo.CreatedBy = data.CreatedBy;
                resourceallocationVo.CreatedDate = data.CreatedDate;
                resourceallocationVo.ModifiedBy = data.ModifiedBy;
                resourceallocationVo.ModifiedDate = data.ModifiedDate;
                resourceallocationVo.AcknowledgeDate = data.AcknowledgeDate;
                resourceallocationVo.Remarks = data.Remarks;
                resourceallocationVo.MovementType = MovementType;
                resourceallocationVo.MovementDateTime = data.MovementDateTime;
                if (data.ShiftingBerthingTaskExecutions != null)
                {
                    foreach (var lst in data.ShiftingBerthingTaskExecutions)
                    {
                        resourceallocationVo.Remarks = lst.Remarks;
                        resourceallocationVo.Deficiencies = lst.Deficiencies;
                    }
                }

                if (data.PilotageServiceRecordings != null)
                {
                    foreach (var lst in data.PilotageServiceRecordings)
                    {
                        resourceallocationVo.Remarks = lst.Remarks;
                        resourceallocationVo.Deficiencies = lst.Deficiencies;
                    }
                }

                if (data.OtherServiceRecordings != null)
                {
                    foreach (var lst in data.OtherServiceRecordings)
                    {
                        resourceallocationVo.Remarks = lst.Remarks;
                        resourceallocationVo.Deficiencies = lst.Deficiencies;
                    }
                }

                resourceallocationVo.ShiftingBerthingTaskExecution = data.ShiftingBerthingTaskExecutions.MapToDTOObj();
                resourceallocationVo.PilotageServiceRecording = data.PilotageServiceRecordings.MapToDTOObj();
                resourceallocationVo.OtherServiceRecording = data.OtherServiceRecordings.MapToDTOObj();
            }

            return resourceallocationVo;
        }

        public static ResourceAllocationVO MapToDTOforVO(this ResourceAllocationVO data)
        {
            ResourceAllocationVO resourceallocationVo = new ResourceAllocationVO();
            if (data != null)
            {
                resourceallocationVo.ResourceAllocationID = data.ResourceAllocationID;
                resourceallocationVo.ServiceReferenceType = data.ServiceReferenceType;
                resourceallocationVo.ServiceReferenceID = data.ServiceReferenceID;
                resourceallocationVo.ServiceTypeCode = data.ServiceTypeCode;
                resourceallocationVo.ResourceID = data.ResourceID;
                resourceallocationVo.ResourceType = data.ResourceType;
                resourceallocationVo.StartTime = data.StartTime;
                //resourceallocationVo.ActualScheduledTime = data.ActualScheduledTime;
                resourceallocationVo.EndTime = data.EndTime;
                resourceallocationVo.TaskStatus = data.TaskStatus;
                resourceallocationVo.RecordStatus = data.RecordStatus;
                resourceallocationVo.CreatedBy = data.CreatedBy;
                resourceallocationVo.CreatedDate = data.CreatedDate;
                resourceallocationVo.ModifiedBy = data.ModifiedBy;
                resourceallocationVo.ModifiedDate = data.ModifiedDate;
                resourceallocationVo.AcknowledgeDate = data.AcknowledgeDate;
                resourceallocationVo.Remarks = data.Remarks;
                resourceallocationVo.ServiceReferenceTypeName = data.ServiceReferenceTypeName;
                resourceallocationVo.VCN = data.VCN;
                resourceallocationVo.OperationTypeName = data.OperationTypeName;
                resourceallocationVo.ResourceTypeName = data.ResourceTypeName;
                resourceallocationVo.TaskStatusName = data.TaskStatusName;
            }

            return resourceallocationVo;
        }

        public static ResourceAllocation MapToEntity(this ResourceAllocationVO vo)
        {
            ResourceAllocation resourceallocation = new ResourceAllocation();
            if (vo != null)
            {
                resourceallocation.ResourceAllocationID = vo.ResourceAllocationID;
                resourceallocation.ServiceReferenceType = vo.ServiceReferenceType;
                resourceallocation.ServiceReferenceID = vo.ServiceReferenceID;
                resourceallocation.OperationType = vo.OperationType;
                resourceallocation.ResourceID = vo.ResourceID;
                resourceallocation.ResourceType = vo.ResourceType;
                resourceallocation.StartTime = Convert.ToDateTime(vo.StartTime, CultureInfo.InvariantCulture);
                resourceallocation.ActualScheduledTime = Convert.ToDateTime(vo.ActualScheduledTime, CultureInfo.InvariantCulture);
                resourceallocation.EndTime = Convert.ToDateTime(vo.EndTime, CultureInfo.InvariantCulture);
                resourceallocation.TaskStatus = vo.TaskStatus;
                resourceallocation.RecordStatus = vo.RecordStatus;
                resourceallocation.CreatedBy = vo.CreatedBy;
                resourceallocation.CreatedDate = vo.CreatedDate;
                resourceallocation.ModifiedBy = vo.ModifiedBy;
                resourceallocation.ModifiedDate = vo.ModifiedDate;
                resourceallocation.AcknowledgeDate = vo.AcknowledgeDate;
                resourceallocation.Remarks = vo.Remarks;
                resourceallocation.MovementDateTime = vo.MovementDateTime;
                resourceallocation.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
                resourceallocation.ShiftingBerthingTaskExecutions.Add(vo.ShiftingBerthingTaskExecution.MapToEntity());
                resourceallocation.PilotageServiceRecordings = new List<PilotageServiceRecording>();
                resourceallocation.PilotageServiceRecordings.Add(vo.PilotageServiceRecording.MapToEntity());
                resourceallocation.OtherServiceRecordings = new List<OtherServiceRecording>();
                resourceallocation.OtherServiceRecordings.Add(vo.OtherServiceRecording.MapToEntity());
            }

            return resourceallocation;
        }

        public static List<ResourceAllocation> MapToEntity(this List<ResourceAllocationVO> vos)
        {
            List<ResourceAllocation> ResourceAllocationEntities = new List<ResourceAllocation>();
            if (vos != null)
            {
                foreach (var ResourceAllocationVO in vos)
                {
                    ResourceAllocationEntities.Add(ResourceAllocationVO.MapToEntity());
                }
            }
            return ResourceAllocationEntities;
        }

        public static List<ResourceAllocationVO> MapToDTO(this List<ResourceAllocation> data)
        {
            List<ResourceAllocationVO> ResourceAllocationVOS = new List<ResourceAllocationVO>();
            if (data != null)
            {
                foreach (var ResourceAllocationEntities in data)
                {
                    ResourceAllocationVOS.Add(ResourceAllocationEntities.MapToDTO());
                }
            }
            return ResourceAllocationVOS;
        }
    }
}
