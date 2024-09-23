using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class SuppDryDockMapExtension
    {
        public static List<SuppDryDockVO> MapToDto(this List<SuppDryDock> suppDryDock)
        {
            List<SuppDryDockVO> SuppDryDockVOs = new List<SuppDryDockVO>();

            if (suppDryDock != null)
            {
                foreach (SuppDryDock obj in suppDryDock)
                {
                    SuppDryDockVOs.Add(obj.MapToDto());
                }
            }

            return SuppDryDockVOs;
        }

        public static List<SuppDryDockVO> MapToDtoSuppMisc(this List<SuppDryDock> suppMiscRequests)
        {
            List<SuppDryDockVO> lstSuppMisc = new List<SuppDryDockVO>();

            if (suppMiscRequests != null)
            {
                foreach (SuppDryDock suppMiscRequest in suppMiscRequests)
                {
                    lstSuppMisc.Add(suppMiscRequest.MapToDtoSuppMisc());
                }
            }

            return lstSuppMisc;
        }


        public static SuppDryDockVO MapToDto(this SuppDryDock data)
        {
            SuppDryDockVO obj = new SuppDryDockVO();
            if (data != null)
            {
            obj.SuppDryDockID = data.SuppDryDockID;
            obj.VCN = data.VCN;
            // obj.PortCode = data.PortCode;
                obj.FromDate = data.FromDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                obj.ToDate = data.ToDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            obj.BarkeelCode = data.BarkeelCode;
            obj.CargoTons = data.CargoTons;
            obj.Ballast = data.Ballast;
            obj.Bunkers = data.Bunkers;
            obj.ExtensionDateTime = data.ExtensionDateTime != null ? data.ExtensionDateTime : null;
            obj.Remarks = data.Remarks;
            obj.TermsandConditions = data.TermsandConditions == "Y" ? true : false;
            obj.WorkflowInstanceID = data.WorkflowInstanceID;
            obj.RecordStatus = data.RecordStatus;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.ArrivalNotification = data.ArrivalNotification.MapToDto();
            List<VesselCall> lstVesselCall = data.ArrivalNotification.VesselCalls as List<VesselCall>;
            if (lstVesselCall.Count > 0)
            {
                obj.Agent = lstVesselCall[0].Agent.MapToDTO();
                obj.Agent.TelephoneNo2 = lstVesselCall[0].Agent.AuthorizedContactPerson.CellularNo;

            }
            obj.VesselName = data.ArrivalNotification.Vessel.VesselName;

            //-- Added by sandeep on 21-02-2015
            obj.AnyDangerousGoods = data.ArrivalNotification.AnyDangerousGoodsonBoard;
            //-- end

                obj.SuppDryDockDocuments = data.SuppDryDockDocuments.Count > 0 ? data.SuppDryDockDocuments.MapToDto() : null;
            obj.VesselAgent = data.ArrivalNotification != null ? (data.ArrivalNotification.Agent != null ? data.ArrivalNotification.Agent.RegisteredName : string.Empty) : string.Empty;

            // -- changed by sandeep on 23-01-2015
            //obj.ApplicationDateTime = Convert.ToString(data.CreatedDate);
                obj.ApplicationDateTime = Convert.ToString(data.ModifiedDate, CultureInfo.InvariantCulture);
            // -- end

            obj.WFStatus = data.WorkflowInstance != null ? (data.WorkflowInstance.SubCategory != null ? data.WorkflowInstance.SubCategory.SubCatName : string.Empty) : string.Empty;
            obj.WorkflowTaskCode = data.WorkflowInstance != null ? (data.WorkflowInstance.WorkflowTaskCode != null ? data.WorkflowInstance.WorkflowTaskCode : string.Empty) : string.Empty;

            obj.Chamber = data.Chamber;
            if (data.EnteredDockDateTime != null)
            {
                    obj.EnteredDockDateTime = Convert.ToString(data.EnteredDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.OnBlocksDateTime != null)
            {
                    obj.OnBlocksDateTime = Convert.ToString(data.OnBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (data.DryDockDateTime != null)
            {
                    obj.DryDockDateTime = Convert.ToString(data.DryDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.FinishedDockDateTime != null)
            {
                    obj.FinishedDockDateTime = Convert.ToString(data.FinishedDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.OffBlocksDateTime != null)
            {
                    obj.OffBlocksDateTime = Convert.ToString(data.OffBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (data.LeftDockDateTime != null)
            {
                    obj.LeftDockDateTime = Convert.ToString(data.LeftDockDateTime, CultureInfo.InvariantCulture);
            }
            obj.DockPortCode = data.DockPortCode;
            obj.DockBerthCode = data.DockBerthCode != null ? data.DockBerthCode : null;
            obj.DockQuayCode = data.DockQuayCode != null ? data.DockQuayCode : null;
            obj.ScheduleFromDate = data.ScheduleFromDate != null ? data.ScheduleFromDate : null;
            obj.ScheduleToDate = data.ScheduleToDate != null ? data.ScheduleToDate : null;
            obj.ScheduleStatus = data.ScheduleStatus != null ? data.ScheduleStatus : null;
            //  obj.EnteredDockDateTime = data.EnteredDockDateTime != null ? data.EnteredDockDateTime : DateTime.MinValue;
            //  obj.FinishedDockDateTime = data.FinishedDockDateTime != null ? data.FinishedDockDateTime : DateTime.MinValue;
            obj.LengthOverallInM = data.ArrivalNotification != null ? (data.ArrivalNotification.Vessel != null
                ? data.ArrivalNotification.Vessel.LengthOverallInM : default(decimal)) : default(decimal);

            obj.ArrDraft = data.ArrivalNotification != null ? data.ArrivalNotification.ArrDraft : string.Empty;
            obj.WFStatus = data.WorkflowInstance != null ? (data.WorkflowInstance.SubCategory != null ? data.WorkflowInstance.SubCategory.SubCatName : string.Empty) : string.Empty;
            obj.BerthName = data.DockBerthCode != null ? data.Berth.BerthName : data.ArrivalNotification.Berth1.BerthName;
            }
            return obj;
        }

        public static SuppDryDockVO MapToDtoSuppMisc(this SuppDryDock data)
        {
            SuppDryDockVO obj = new SuppDryDockVO();
            if (data != null)
            {
            obj.SuppDryDockID = data.SuppDryDockID;
            obj.VCN = data.VCN;
            obj.VesselName = data.ArrivalNotification != null ? (data.ArrivalNotification.Vessel != null ? data.ArrivalNotification.Vessel.VesselName : string.Empty) : string.Empty;
            obj.VesselAgent = data.ArrivalNotification != null ? (data.ArrivalNotification.Agent != null ? data.ArrivalNotification.Agent.RegisteredName : string.Empty) : string.Empty;
                obj.FromDate = data.FromDate.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
                obj.ToDate = data.ToDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            obj.ExtensionDateTime = data.ExtensionDateTime != null ? data.ExtensionDateTime : null;
            obj.RecordStatus = data.RecordStatus;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.ScheduleFromDate = data.ScheduleFromDate != null ? data.ScheduleFromDate : null;
            obj.ScheduleToDate = data.ScheduleToDate != null ? data.ScheduleToDate : null;
            if (data.EnteredDockDateTime != null)
            {
                    obj.EnteredDockDateTime = Convert.ToString(data.EnteredDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.OnBlocksDateTime != null)
            {
                    obj.OnBlocksDateTime = Convert.ToString(data.OnBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (data.DryDockDateTime != null)
            {
                    obj.DryDockDateTime = Convert.ToString(data.DryDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.FinishedDockDateTime != null)
            {
                    obj.FinishedDockDateTime = Convert.ToString(data.FinishedDockDateTime, CultureInfo.InvariantCulture);
            }
            if (data.OffBlocksDateTime != null)
            {
                    obj.OffBlocksDateTime = Convert.ToString(data.OffBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (data.LeftDockDateTime != null)
            {
                    obj.LeftDockDateTime = Convert.ToString(data.LeftDockDateTime, CultureInfo.InvariantCulture);
            }
            else
            {
                obj.LeftDockDateTime = "NA";
            }
            obj.BerthName = data.ArrivalNotification != null ? data.ArrivalNotification.Berth1.BerthName : "NA";
            if (data.SuppMiscServices != null)
            {
                foreach (var item in data.SuppMiscServices)
                {

                    obj.SuppMiscServiceID = item.SuppMiscServiceID;
                    obj.ServiceTypeID = item.ServiceTypeID;
                    obj.Phase = item.Phase;
                        obj.FromDateTime = item.FromDateTime.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
                        obj.ToDateTime = item.ToDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                    obj.Quantity = item.Quantity;
                    //   obj.UOMCode = item.UOMCode;
                    obj.Remarks = item.Remarks;
                }
            }
            }
            return obj;
        }


        public static SuppDryDock MapToEntity(this SuppDryDockVO vo)
        {

            SuppDryDock suppDryDock = new SuppDryDock();
            if (vo != null)
            {
            suppDryDock.SuppDryDockID = vo.SuppDryDockID;
            suppDryDock.VCN = vo.VCN;
            suppDryDock.DockPortCode = vo.DockPortCode;

            // -- changed by sandeep on 29-01-2014
            //suppDryDock.FromDate = DateTime.Parse(vo.FromDate).Date;
                suppDryDock.FromDate = DateTime.Parse(vo.FromDate, CultureInfo.InvariantCulture);
            // -- end

                suppDryDock.ToDate = DateTime.Parse(vo.ToDate, CultureInfo.InvariantCulture);
            suppDryDock.BarkeelCode = vo.BarkeelCode;
            suppDryDock.CargoTons = vo.CargoTons;
            suppDryDock.Ballast = vo.Ballast;
            suppDryDock.Bunkers = vo.Bunkers;
            suppDryDock.ExtensionDateTime = vo.ExtensionDateTime;
            suppDryDock.Remarks = vo.Remarks;
            suppDryDock.TermsandConditions = vo.TermsandConditions == true ? "Y" : "N";
            suppDryDock.WorkflowInstanceID = vo.WorkflowInstanceID;
            suppDryDock.RecordStatus = vo.RecordStatus;
            suppDryDock.CreatedBy = vo.CreatedBy;
            suppDryDock.CreatedDate = vo.CreatedDate;
            suppDryDock.ModifiedBy = vo.ModifiedBy;
            suppDryDock.ModifiedDate = vo.ModifiedDate;
            suppDryDock.VesselName = vo.VesselName;
            suppDryDock.VesselAgent = vo.VesselAgent;
            suppDryDock.SuppDryDockDocuments = vo.SuppDryDockDocuments != null ? vo.SuppDryDockDocuments.MapToEntity() : null;


            suppDryDock.Chamber = vo.Chamber;
            if (vo.EnteredDockDateTime != null)
            {
                    suppDryDock.EnteredDockDateTime = Convert.ToDateTime(vo.EnteredDockDateTime, CultureInfo.InvariantCulture);
            }
            if (vo.OnBlocksDateTime != null)
            {
                    suppDryDock.OnBlocksDateTime = Convert.ToDateTime(vo.OnBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (vo.DryDockDateTime != null)
            {
                    suppDryDock.DryDockDateTime = Convert.ToDateTime(vo.DryDockDateTime, CultureInfo.InvariantCulture);
            }
            if (vo.FinishedDockDateTime != null)
            {
                    suppDryDock.FinishedDockDateTime = Convert.ToDateTime(vo.FinishedDockDateTime, CultureInfo.InvariantCulture);
            }
            if (vo.OffBlocksDateTime != null)
            {
                    suppDryDock.OffBlocksDateTime = Convert.ToDateTime(vo.OffBlocksDateTime, CultureInfo.InvariantCulture);
            }
            if (vo.LeftDockDateTime != null)
            {
                    suppDryDock.LeftDockDateTime = Convert.ToDateTime(vo.LeftDockDateTime, CultureInfo.InvariantCulture);
            }

            suppDryDock.DockBerthCode = vo.DockBerthCode != null ? vo.DockBerthCode : null;
            suppDryDock.DockQuayCode = vo.DockQuayCode != null ? vo.DockQuayCode : null;

            if (vo.ScheduleFromDate != null)
                suppDryDock.ScheduleFromDate = vo.ScheduleFromDate;

            if (vo.ScheduleToDate != null)
                suppDryDock.ScheduleToDate = vo.ScheduleToDate;

            suppDryDock.ScheduleStatus = vo.ScheduleStatus != null ? vo.ScheduleStatus : null;
            }
            return suppDryDock;


        }


        //public static SuppDryDock MapToEntityDock(this SuppDockUnDockTimeVO suppDockUnDockTimeVO)
        //{

        //    SuppDryDock suppDockUnDockTime = new SuppDryDock();

        //    suppDockUnDockTime.SuppDryDockID = suppDockUnDockTimeVO.SuppDryDockID;
        //    suppDockUnDockTime.Chamber = suppDockUnDockTimeVO.Chamber;
        //    suppDockUnDockTime.EnteredDockDateTime = suppDockUnDockTimeVO.EnteredDockDateTime;
        //    suppDockUnDockTime.OnBlocksDateTime = suppDockUnDockTimeVO.OnBlocksDateTime;
        //    suppDockUnDockTime.DryDockDateTime = suppDockUnDockTimeVO.DryDockDateTime;
        //    suppDockUnDockTime.FinishedDockDateTime = suppDockUnDockTimeVO.FinishedDockDateTime;
        //    suppDockUnDockTime.OffBlocksDateTime = suppDockUnDockTimeVO.OffBlocksDateTime;
        //    suppDockUnDockTime.LeftDockDateTime = suppDockUnDockTimeVO.LeftDockDateTime;

        //    suppDockUnDockTime.BarkeelCode = suppDockUnDockTimeVO.BarkeelCode;
        //    suppDockUnDockTime.TermsandConditions = suppDockUnDockTimeVO.TermsandConditionStatus;
        //    suppDockUnDockTime.RecordStatus = suppDockUnDockTimeVO.RecordStatus;
        //    suppDockUnDockTime.CreatedBy = suppDockUnDockTimeVO.CreatedBy;
        //    suppDockUnDockTime.CreatedDate = suppDockUnDockTimeVO.CreatedDate;
        //    suppDockUnDockTime.ModifiedBy = suppDockUnDockTimeVO.ModifiedBy;
        //    suppDockUnDockTime.ModifiedDate = suppDockUnDockTimeVO.ModifiedDate;
        //    suppDockUnDockTime.VCN = suppDockUnDockTimeVO.VCN;
        //    suppDockUnDockTime.FromDate = Convert.ToDateTime(suppDockUnDockTimeVO.FromDate);
        //    suppDockUnDockTime.ToDate = Convert.ToDateTime(suppDockUnDockTimeVO.ToDate);

        //    return suppDockUnDockTime;

        //}
    }
}
