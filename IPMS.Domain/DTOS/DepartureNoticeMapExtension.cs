using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class DepartureNoticeMapExtension
    {
        public static List<DepartureNoticeVO> MapToDto(this List<DepartureNotice> departureNotices)
        {
            List<DepartureNoticeVO> departureNoticeVO = new List<DepartureNoticeVO>();
            if (departureNotices != null)
            {
                foreach (var departureNotice in departureNotices)
                {
                    departureNoticeVO.Add(departureNotice.MapToDto());
                }
            }
            return departureNoticeVO;
        }

        public static DepartureNoticeVO MapToDto(this DepartureNotice DN)
        {
            DepartureNoticeVO DNVO = new DepartureNoticeVO();
            if (DN != null)
            {
                DNVO.DepartureID = DN.DepartureID;
                DNVO.AgentID = DN.AgentID;
                DNVO.CurrentBerth = DN.CurrentBerth;
                DNVO.DaylightRestriction = DN.DaylightRestriction;
                DNVO.PortCode = DN.PortCode;
                DNVO.EstimatedDatetimeOfSR = Convert.ToDateTime(DN.EstimatedDatetimeOfSR,CultureInfo.InvariantCulture);
                DNVO.EstimatedDatetimeOfSRConverted = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", DN.EstimatedDatetimeOfSR);
                DNVO.IsVesselDoubleBank = DN.IsVesselDoubleBank;
                DNVO.NoMainEngine = DN.NoMainEngine;
                DNVO.SideAlongSideCode = DN.SideAlongSideCode;
                DNVO.Tidal = DN.Tidal;
                DNVO.TowingDetails = DN.TowingDetails;
                DNVO.VCN = DN.VCN;
                DNVO.VesselID = DN.VesselID;
                DNVO.WillSheBeUnderTow = DN.WillSheBeUnderTow;
                DNVO.WorkflowInstanceId = DN.WorkflowInstanceId;
                DNVO.workflowRemarks = null;

                if (DN.ArrivalNotification != null)
                {
                    DNVO.VesselName = DN.ArrivalNotification.Vessel != null ? DN.ArrivalNotification.Vessel.VesselName : "";
                }

                DNVO.RecordStatus = DN.RecordStatus;
                DNVO.CreatedBy = DN.CreatedBy;
                DNVO.CreatedDate = DN.CreatedDate;
                DNVO.ModifiedBy = DN.ModifiedBy;
                DNVO.ModifiedDate = DN.ModifiedDate;
            }
            return DNVO;
        }

        public static DepartureNotice MapToEntity(this DepartureNoticeVO data)
        {
            if (data == null)
                return new DepartureNotice();

            DepartureNotice DN = new DepartureNotice
            {
                DepartureID = data.DepartureID.GetValueOrDefault(),
                VesselID = data.VesselID,
                AgentID = data.AgentID,
                PortCode = data.PortCode,
                Tidal = data.Tidal,
                DaylightRestriction = data.DaylightRestriction,
                NoMainEngine = data.NoMainEngine,
                WillSheBeUnderTow = data.WillSheBeUnderTow,
                TowingDetails = data.TowingDetails,
                CurrentBerth = data.CurrentBerth,
                SideAlongSideCode = data.SideAlongSideCode,
                IsVesselDoubleBank = data.IsVesselDoubleBank,
                EstimatedDatetimeOfSR = DateTime.Parse(Convert.ToDateTime(data.EstimatedDatetimeOfSRConverted, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                WorkflowInstanceId = data.WorkflowInstanceId,
                RecordStatus = data.RecordStatus,
                IsFinal = data.IsFinal,
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault(),

                //Workflow Tokens binding
                VCN = data.VCN,
                VesselName = data.VesselName,
                VesselType = data.VesselType,
                AgentName = data.AgentName,
                SubmissionDate = data.CreatedDate.GetValueOrDefault()
            };

            return DN;
        }
    }
}
