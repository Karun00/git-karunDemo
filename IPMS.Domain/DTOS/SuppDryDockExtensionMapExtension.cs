using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;


namespace IPMS.Domain.DTOS
{
    public  static class SuppDryDockExtensionMapExtension
    {
        public static List<SuppDryDockExtensionVO> MapToDto(this List<SuppDryDockExtension> suppDryDock)
        {
            List<SuppDryDockExtensionVO> SuppDryDockExtsVOs = new List<SuppDryDockExtensionVO>();

            if (suppDryDock != null)
            {
                foreach (SuppDryDockExtension obj in suppDryDock)
                {
                    SuppDryDockExtsVOs.Add(obj.MapToDTO());
                }
            }

            return SuppDryDockExtsVOs;
        }
        public static SuppDryDockExtensionVO MapToDTO(this SuppDryDockExtension data)
        {
            SuppDryDockExtensionVO obj = new SuppDryDockExtensionVO();
            if (data != null)
            {
                obj.SuppDryDockID = data.SuppDryDockID;
                obj.ExtensionDateTime = (data.ExtensionDateTime != null ? data.ExtensionDateTime.Value.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "");
                obj.Remarks = data.Remarks;
                obj.TermsandConditions = data.TermsandConditions == "Y" ? true : false;
                obj.WorkflowInstanceID = data.WorkflowInstanceID;
                obj.RecordStatus = data.RecordStatus;
                obj.CreatedBy = data.CreatedBy;
                obj.CreatedDate = data.CreatedDate;
                obj.ModifiedBy = data.ModifiedBy;
                obj.ModifiedDate = data.ModifiedDate;
                obj.SuppDryDockDocuments = data.SuppDryDock.SuppDryDockDocuments.Count > 0 ? data.SuppDryDock.SuppDryDockDocuments.MapToDto() : null;
                obj.WFStatus = data.WorkflowInstance != null ? (data.WorkflowInstance.SubCategory != null ? data.WorkflowInstance.SubCategory.SubCatName : string.Empty) : string.Empty;

                if (data.ExtensionDateTime != null)
                {
                    obj.EnteredDockDateTime = data.ExtensionDateTime;
                }

                obj.ScheduleFromDate = (data.ScheduleFromDate != null ? data.ScheduleFromDate.Value.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "");// data.ScheduleFromDate != null ? data.ScheduleFromDate : null;
                obj.ScheduleToDate = (data.ScheduleToDate != null ? data.ScheduleToDate.Value.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "");// data.ScheduleToDate != null ? data.ScheduleToDate : null;
                obj.ScheduleStatus = data.ScheduleStatus != null ? data.ScheduleStatus : null;
            }            

            return obj;
        }

        public static SuppDryDockExtension MapToEntity(this SuppDryDockExtensionVO vo)
        {

            SuppDryDockExtension suppDryDockExtns = new SuppDryDockExtension();
            if (vo != null)
            {
                suppDryDockExtns.SuppDryDockID = vo.SuppDryDockID;
                suppDryDockExtns.SuppDryDockExtensionID = vo.SuppDryDockExtensionID;


                suppDryDockExtns.ExtensionDateTime = DateTime.Parse(vo.ExtensionDateTime, CultureInfo.InvariantCulture);
                suppDryDockExtns.Remarks = vo.Remarks;
                suppDryDockExtns.TermsandConditions = vo.TermsandConditions == true ? "Y" : "N";
                suppDryDockExtns.WorkflowInstanceID = vo.WorkflowInstanceID;
                suppDryDockExtns.RecordStatus = vo.RecordStatus;
                suppDryDockExtns.CreatedBy = vo.CreatedBy;
                suppDryDockExtns.CreatedDate = vo.CreatedDate;
                suppDryDockExtns.ModifiedBy = vo.ModifiedBy;
                suppDryDockExtns.ModifiedDate = vo.ModifiedDate;
                //suppDryDockExtns.SuppDryDock.SuppDryDockDocuments = vo.SuppDryDockDocuments != null ? vo.SuppDryDockDocuments.MapToEntity() : null;
                if (vo.EnteredDockDateTime != null)
                {
                    suppDryDockExtns.ExtensionDateTime = vo.EnteredDockDateTime;
                }

                suppDryDockExtns.ScheduleFromDate = DateTime.Parse(vo.ScheduleFromDate, CultureInfo.InvariantCulture);
                suppDryDockExtns.ScheduleToDate = DateTime.Parse(vo.ScheduleToDate, CultureInfo.InvariantCulture);
                suppDryDockExtns.ScheduleStatus = vo.ScheduleStatus;              
            }
            return suppDryDockExtns;


        }
    }
}
