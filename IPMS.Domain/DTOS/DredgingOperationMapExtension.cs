using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;
using System;

namespace IPMS.Domain.DTOS
{
    public static class DredgingOperationMapExtension
    {
        public static List<DredgingOperationVO> MapToDto(this List<DredgingOperation> dredgingOperations)
        {
            List<DredgingOperationVO> dredgingOperationVOs = new List<DredgingOperationVO>();
            if (dredgingOperations != null)
            {
                foreach (DredgingOperation obj in dredgingOperations)
                {
                    dredgingOperationVOs.Add(obj.MapToDto());
                }
            }

            return dredgingOperationVOs;
        }

        public static List<DredgingOperation> MapToEntity(this List<DredgingOperationVO> dredgingOperationsVO)
        {
            List<DredgingOperation> lstdredgingOperation = new List<DredgingOperation>();
            if (dredgingOperationsVO != null)
            {
                foreach (DredgingOperationVO dredgingOperationvo in dredgingOperationsVO)
                {
                    lstdredgingOperation.Add(dredgingOperationvo.MapToEntity());
                }
            }

            return lstdredgingOperation;
        }

        public static DredgingOperationVO MapToDto(this DredgingOperation data)
        {
            //return new DredgingOperationVO
            // {
            //     DredgingOperationID = data.DredgingOperationID,
            //     DredgingPriorityID = data.DredgingPriorityID,
            //     Priority = data.Priority,
            //     AreaLocationID = data.AreaLocationID,
            //     TypeCode = data.TypeCode,
            //     RequiredDate = data.RequiredDate,
            //     DesignDepth = data.DesignDepth,
            //     PromulgateDepth = data.PromulgateDepth,
            //     Requirement = data.Requirement,
            //     DPARemarks = data.DPARemarks,
            //     AreaType = data.AreaType,
            //     PortCode = data.PortCode,
            //     QuayCode = data.QuayCode,
            //     BerthCode = data.BerthCode,
            //     DPAWorkflowInstanceID = data.DPAWorkflowInstanceID,
            //     OccupationFrom = data.OccupationFrom != null ? data.OccupationFrom.ToString() : null,
            //     OccupationTo = data.OccupationTo != null ? data.OccupationTo.ToString() : null,
            //     OccupationDuration = data.OccupationDuration,
            //     DOWorkflowInstanceID = data.DOWorkflowInstanceID,
            //     Volume = data.Volume,
            //     CraftID = data.CraftID,
            //     DredgingTask = data.DredgingTask,
            //     DredgingDelay = data.DredgingDelay,
            //     DVRemarks = data.DVRemarks,
            //     DVWorkflowInstanceID = data.DVWorkflowInstanceID,
            //     DredgingStatus = data.DredgingStatus,
            //     IsDPAFinal = data.IsDPAFinal,
            //     IsDOFinal = data.IsDOFinal,
            //     IsDVFinal = data.IsDVFinal,
            //     FinancialYearID = data.FinancialYearID,
            //     RecordStatus = data.RecordStatus,
            //     CreatedBy = data.CreatedBy,
            //     CreatedDate = data.CreatedDate,
            //     ModifiedBy = data.ModifiedBy,
            //     ModifiedDate = data.ModifiedDate,
            //     DredgingTypeName = data.SubCategory2.SubCatName,
            //     AreaName = data.AreaType == "B" ? data.Berth.BerthName : data.Location.LocationName,
            //     FinancialYearDate = data.FinancialYear.StartDate.ToString("MMMM yyyy") + " to " + data.FinancialYear.EndDate.ToString("MMMM yyyy"),
            //     BerthOccupationDocumentVO = (data.BerthOccupationDocuments != null && data.BerthOccupationDocuments.Count > 0) ? data.BerthOccupationDocuments.MapToDTO() : null
            //};

            DredgingOperationVO vo = new DredgingOperationVO();
            if (data != null)
            {
                vo.DredgingOperationID = data.DredgingOperationID;
                vo.DredgingPriorityID = data.DredgingPriorityID;
                vo.Priority = data.Priority;
                vo.AreaLocationID = data.AreaLocationID != null ? data.AreaLocationID : null;
                vo.TypeCode = data.TypeCode;
                vo.RequiredDate = Convert.ToString(data.RequiredDate, CultureInfo.InvariantCulture);
                vo.DesignDepth = data.DesignDepth;
                vo.PromulgateDepth = data.PromulgateDepth;
                vo.Requirement = data.Requirement;
                vo.DPARemarks = data.DPARemarks;
                vo.AreaType = data.AreaType;
                vo.PortCode = data.PortCode != null ? data.PortCode : null;
                vo.QuayCode = data.QuayCode != null ? data.QuayCode : null;
                vo.BerthCode = data.BerthCode != null ? data.BerthCode : null;
                vo.DPAWorkflowInstanceID = data.DPAWorkflowInstanceID;
                vo.OccupationFrom = data.OccupationFrom != null ? data.OccupationFrom.ToString() : null;
                vo.OccupationTo = data.OccupationTo != null ? data.OccupationTo.ToString() : null;
                vo.OccupationDuration = data.OccupationDuration;
                vo.DOWorkflowInstanceID = data.DOWorkflowInstanceID;
                vo.Volume = data.Volume;
                vo.CraftID = data.CraftID;
                vo.DredgingTask = data.DredgingTask;
                vo.DredgingDelay = data.DredgingDelay;
                vo.DVRemarks = data.DVRemarks;
                vo.DVWorkflowInstanceID = data.DVWorkflowInstanceID;
                vo.DredgingStatus = data.DredgingStatus;
                vo.IsDPAFinal = data.IsDPAFinal;
                vo.IsDOFinal = data.IsDOFinal;
                vo.IsDVFinal = data.IsDVFinal;
                vo.FinancialYearID = data.FinancialYearID;
                vo.RecordStatus = data.RecordStatus;
                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = data.ModifiedDate;
                vo.RequireDate = data.RequireDate != null ? data.RequireDate : null;
                vo.DredgerName = data.DredgerName != null ? data.DredgerName : null;
                //chandrima
                vo.PortName = data.PortName != null ? data.PortName : null;
                vo.VolumeOccupationFrom = data.VolumeOccupationFrom != null ? data.VolumeOccupationFrom.ToString() : null;
                vo.VolumeOccupationTo = data.VolumeOccupationTo != null ? data.VolumeOccupationTo.ToString() : null;
                vo.VolumeOccupationDuration = data.VolumeOccupationDuration;
                vo.workflowRemarks = null;
                vo.TypeName = data.SubCategory2.SubCatName != null ? data.SubCategory2.SubCatName : null;
                vo.ToDate = Convert.ToString(data.DredgingPriority.ToDate, CultureInfo.InvariantCulture) != null ? Convert.ToString(data.DredgingPriority.ToDate, CultureInfo.InvariantCulture) : null;
                vo.FromDate = Convert.ToString(data.DredgingPriority.FromDate, CultureInfo.InvariantCulture) != null ? Convert.ToString(data.DredgingPriority.FromDate, CultureInfo.InvariantCulture) : null;
                vo.DredgingMaterial = data.SubCategory2.SubCatName != null ? data.SubCategory2.SubCatName : null;
                vo.AreaName = data.AreaType == "B" ? data.Berth.BerthName : data.Location.LocationName != null ? data.AreaType == "B" ? data.Berth.BerthName : data.Location.LocationName : null;
                vo.FinancialYearDate = data.FinancialYear.StartDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) + " to " + data.FinancialYear.EndDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) != null ? data.FinancialYear.StartDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) + " to " + data.FinancialYear.EndDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : null;
                //vo.DredgingTypeName = data.SubCategory2.SubCatName != null ? data.SubCategory2.SubCatName : null;
                //vo.AreaName = data.AreaType != null ? data.AreaType == "B" ? data.Berth.BerthName : data.Location.LocationName : null;
                //vo.FinancialYearDate = data.FinancialYear != null ? data.FinancialYear.StartDate.ToString("MMMM yyyy") + " to " + data.FinancialYear.EndDate.ToString("MMMM yyyy") : null;
                vo.BerthKey = data.BerthCode != null ? data.PortCode + "." + data.QuayCode + "." + data.BerthCode : null;
                vo.BerthOccupationDocumentVO = (data.BerthOccupationDocuments != null && data.BerthOccupationDocuments.Count > 0) ? data.BerthOccupationDocuments.MapToDto() : null;
                }
                return vo;
            
        }

        public static DredgingOperation MapToEntity(this DredgingOperationVO vo)
        {
            //return new DredgingOperation
            //{
            //    DredgingOperationID = vo.DredgingOperationID,
            //    DredgingPriorityID = vo.DredgingPriorityID,
            //    Priority = vo.Priority,
            //    AreaLocationID = vo.AreaLocationID,
            //    TypeCode = vo.TypeCode,
            //    RequiredDate = vo.RequiredDate,
            //    DesignDepth = vo.DesignDepth,
            //    PromulgateDepth = vo.PromulgateDepth,
            //    Requirement = vo.Requirement,
            //    DPARemarks = vo.DPARemarks,
            //    AreaType = vo.AreaType,
            //    PortCode =  vo.PortCode != "" ? vo.PortCode : null,
            //    QuayCode = vo.QuayCode != "" ? vo.QuayCode : null,
            //    BerthCode = vo.BerthCode != "" ? vo.BerthCode : null,
            //    DPAWorkflowInstanceID = vo.DPAWorkflowInstanceID,
            //    OccupationFrom = string.IsNullOrEmpty(vo.OccupationFrom) ? DateTime.MinValue : Convert.ToDateTime(vo.OccupationFrom),
            //    OccupationTo = string.IsNullOrEmpty(vo.OccupationTo) ? DateTime.MinValue : Convert.ToDateTime(vo.OccupationTo),
            //    OccupationDuration = vo.OccupationDuration,
            //    DOWorkflowInstanceID = vo.DOWorkflowInstanceID,
            //    Volume = vo.Volume,
            //    CraftID = vo.CraftID,
            //    DredgingTask = vo.DredgingTask,
            //    DredgingDelay = vo.DredgingDelay,
            //    DVRemarks = vo.DVRemarks,
            //    DVWorkflowInstanceID = vo.DVWorkflowInstanceID,
            //    DredgingStatus = vo.DredgingStatus,
            //    IsDPAFinal = vo.IsDPAFinal,
            //    IsDOFinal = vo.IsDOFinal,
            //    IsDVFinal = vo.IsDVFinal,
            //    FinancialYearID = vo.FinancialYearID,
            //    RecordStatus = vo.RecordStatus,
            //    CreatedBy = vo.CreatedBy,
            //    CreatedDate = vo.CreatedDate,
            //    ModifiedBy = vo.ModifiedBy,
            //    ModifiedDate = vo.ModifiedDate
            //    //BerthOccupationDocuments = vo.BerthOccupationDocumentVO.MapToEntity()
            //};



            DredgingOperation obj = new DredgingOperation();
            if (vo != null)
            {
                obj.DredgingOperationID = vo.DredgingOperationID;
                obj.DredgingPriorityID = vo.DredgingPriorityID;
                obj.Priority = vo.Priority;
                obj.AreaLocationID = vo.AreaLocationID;
                obj.TypeCode = vo.TypeCode;
                obj.RequiredDate = Convert.ToDateTime(vo.RequiredDate, CultureInfo.InvariantCulture);
                obj.DesignDepth = vo.DesignDepth;
                obj.PromulgateDepth = vo.PromulgateDepth;
                obj.Requirement = vo.Requirement;
                obj.DPARemarks = vo.DPARemarks;
                obj.AreaType = vo.AreaType;
                obj.PortCode = vo.PortCode != "" ? vo.PortCode : null;
                obj.QuayCode = vo.QuayCode != "" ? vo.QuayCode : null;
                obj.BerthCode = vo.BerthCode != "" ? vo.BerthCode : null;
                //obj.PortCode = string.IsNullOrEmpty(vo.PortCode) ? vo.PortCode : null;
                //obj.QuayCode = string.IsNullOrEmpty(vo.QuayCode)? vo.QuayCode : null;
                //obj.BerthCode = string.IsNullOrEmpty(vo.BerthCode) ? vo.BerthCode : null;
                obj.DPAWorkflowInstanceID = vo.DPAWorkflowInstanceID;
                obj.OccupationFrom = string.IsNullOrEmpty(vo.OccupationFrom) ? DateTime.MinValue : Convert.ToDateTime(vo.OccupationFrom, CultureInfo.InvariantCulture);
                obj.OccupationTo = string.IsNullOrEmpty(vo.OccupationTo) ? DateTime.MinValue : Convert.ToDateTime(vo.OccupationTo, CultureInfo.InvariantCulture);
                obj.OccupationDuration = vo.OccupationDuration;
                obj.DOWorkflowInstanceID = vo.DOWorkflowInstanceID;
                obj.Volume = vo.Volume;
                obj.CraftID = vo.CraftID;
                obj.DredgingTask = vo.DredgingTask;
                obj.DredgingDelay = vo.DredgingDelay;
                obj.DVRemarks = vo.DVRemarks;
                obj.DVWorkflowInstanceID = vo.DVWorkflowInstanceID;
                obj.DredgingStatus = vo.DredgingStatus;
                obj.IsDPAFinal = vo.IsDPAFinal;
                obj.IsDOFinal = vo.IsDOFinal;
                obj.IsDVFinal = vo.IsDVFinal;
                obj.FinancialYearID = vo.FinancialYearID;
                obj.RecordStatus = vo.RecordStatus;
                obj.CreatedBy = vo.CreatedBy;
                obj.CreatedDate = vo.CreatedDate;
                obj.ModifiedBy = vo.ModifiedBy;
                obj.ModifiedDate = vo.ModifiedDate;
                //BerthOccupationDocuments = vo.BerthOccupationDocumentVO.MapToEntity()
                obj.DredgingMaterial = vo.DredgingMaterial;
                obj.AreaName = vo.AreaName;
                obj.RequireDate = vo.RequireDate;
                obj.DredgerName = vo.DredgerName;
                obj.PortName = vo.PortName;
                obj.VolumeOccupationFrom = string.IsNullOrEmpty(vo.VolumeOccupationFrom) ? DateTime.MinValue : Convert.ToDateTime(vo.VolumeOccupationFrom, CultureInfo.InvariantCulture);
                obj.VolumeOccupationTo = string.IsNullOrEmpty(vo.VolumeOccupationTo) ? DateTime.MinValue : Convert.ToDateTime(vo.VolumeOccupationTo, CultureInfo.InvariantCulture);
                obj.VolumeOccupationDuration = vo.VolumeOccupationDuration;
            }
            return obj;





        }
    }
}
