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
    public static class VesselArrestImmobilizationSAMSAStopMapExtension
    {
        public static List<VesselArrestImmobilizationSAMSAStopVO> MapToDTO(this List<VesselArrestImmobilizationSAMSA> data)
        {
            List<VesselArrestImmobilizationSAMSAStopVO> vos = new List<VesselArrestImmobilizationSAMSAStopVO>();
            foreach (var vessel in data)
            {
                vos.Add(vessel.MapToDTO());
            }
            return vos;
        }

        public static VesselArrestImmobilizationSAMSAStopVO MapToDTO(this VesselArrestImmobilizationSAMSA data)
        {
            VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSAMSAStopVo = new VesselArrestImmobilizationSAMSAStopVO();
            vesselArrestImmobilizationSAMSAStopVo.VCN = data.VCN;
            vesselArrestImmobilizationSAMSAStopVo.VAISID = data.VAISID;
            vesselArrestImmobilizationSAMSAStopVo.VesselArrested = data.VesselArrested;
            vesselArrestImmobilizationSAMSAStopVo.ArrestedDate = Convert.ToString(data.ArrestedDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.ArrestedRemarks = data.ArrestedRemarks;
            vesselArrestImmobilizationSAMSAStopVo.VesselReleased = data.VesselReleased;
            vesselArrestImmobilizationSAMSAStopVo.ReleasedDate = Convert.ToString(data.ReleasedDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.ReleasedRemarks = data.ReleasedRemarks;
            vesselArrestImmobilizationSAMSAStopVo.Immobilization = data.Immobilization;
            vesselArrestImmobilizationSAMSAStopVo.ImmobilizationStartDate = Convert.ToString(data.ImmobilizationStartDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.ImmobilizationEndDate = Convert.ToString(data.ImmobilizationEndDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.ExactWorkProposed = data.ExactWorkProposed;
            vesselArrestImmobilizationSAMSAStopVo.PollutionPrecautionTaken = data.PollutionPrecautionTaken;
            vesselArrestImmobilizationSAMSAStopVo.ApprovedDate = Convert.ToString(data.ApprovedDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.SAMSAStop = data.SAMSAStop;
            vesselArrestImmobilizationSAMSAStopVo.SAMSAStopDate = Convert.ToString(data.SAMSAStopDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.SAMSAStopRemarks = data.SAMSAStopRemarks;
            vesselArrestImmobilizationSAMSAStopVo.SAMSACleared = data.SAMSACleared;
            vesselArrestImmobilizationSAMSAStopVo.SAMSAClearedDate = Convert.ToString(data.SAMSAClearedDate, CultureInfo.InvariantCulture);
            vesselArrestImmobilizationSAMSAStopVo.SAMSAClearedRemarks = data.SAMSAClearedRemarks;
            vesselArrestImmobilizationSAMSAStopVo.RecordStatus = data.RecordStatus;
            vesselArrestImmobilizationSAMSAStopVo.CreatedBy = data.CreatedBy;
            vesselArrestImmobilizationSAMSAStopVo.CreatedDate = data.CreatedDate;
            vesselArrestImmobilizationSAMSAStopVo.ModifiedBy = data.ModifiedBy;
            vesselArrestImmobilizationSAMSAStopVo.ModifiedDate = data.ModifiedDate;
            vesselArrestImmobilizationSAMSAStopVo.Vessel = data.ArrivalNotification.Vessel.MapToDto();
            vesselArrestImmobilizationSAMSAStopVo.VesselReleasedStatus = data.VesselReleased == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.VesselArrestedStatus = data.VesselArrested == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.ImmobilizationStatus = data.Immobilization == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.PollutionPrecautionTakenStatus = data.PollutionPrecautionTaken == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.SAMSAStopStatus = data.SAMSAStop == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.SAMSAClearedStatus = data.SAMSACleared == "Y" ? true : false;
            vesselArrestImmobilizationSAMSAStopVo.Agent = data.ArrivalNotification.Agent.MapToDTO();
            vesselArrestImmobilizationSAMSAStopVo.ETA = data.ArrivalNotification.ETA;
            vesselArrestImmobilizationSAMSAStopVo.CurrentBerth = data.ArrivalNotification.PreferredBerthCode;

            vesselArrestImmobilizationSAMSAStopVo.AnyDangerousGoods = data.ArrivalNotification.AnyDangerousGoodsonBoard;
             
            if (data.VesselArrestDocuments != null)
            {
                vesselArrestImmobilizationSAMSAStopVo.VesselArrestDocuments = data.VesselArrestDocuments.Count != 0 ? data.VesselArrestDocuments.ToList().MapToDTO() : null;
            }

            if (data.VesselSAMSAStopDocuments != null)
            {
                vesselArrestImmobilizationSAMSAStopVo.VesselSAMSAStopDocuments = data.VesselSAMSAStopDocuments.Count != 0 ? data.VesselSAMSAStopDocuments.ToList().MapToDTO() : null;
            }

            return vesselArrestImmobilizationSAMSAStopVo;
        }

        public static VesselArrestImmobilizationSAMSA MapToEntity(this VesselArrestImmobilizationSAMSAStopVO vo)
        {
            VesselArrestImmobilizationSAMSA vesselArrestImmobilizationSAMSA = new VesselArrestImmobilizationSAMSA();
            vesselArrestImmobilizationSAMSA.VCN = vo.VCN;
            vesselArrestImmobilizationSAMSA.VAISID = vo.VAISID;

            if (vo.ArrestedDate != null)
            {
                if (vo.ArrestedDate != "")
                {
                    vesselArrestImmobilizationSAMSA.ArrestedDate = DateTime.Parse(vo.ArrestedDate, CultureInfo.InvariantCulture);
                }
            }

            vesselArrestImmobilizationSAMSA.ArrestedRemarks = vo.ArrestedRemarks;

            if (vo.ReleasedDate != null)
            {
                if (vo.ReleasedDate != "")
                {
                    vesselArrestImmobilizationSAMSA.ReleasedDate = DateTime.Parse(vo.ReleasedDate, CultureInfo.InvariantCulture);
                }
            }

            vesselArrestImmobilizationSAMSA.ReleasedRemarks = vo.ReleasedRemarks;

            if (vo.ImmobilizationStartDate != null)
            {
                if (vo.ImmobilizationStartDate != "")
                {
                    vesselArrestImmobilizationSAMSA.ImmobilizationStartDate = DateTime.Parse(vo.ImmobilizationStartDate, CultureInfo.InvariantCulture);
                }
            }

            if (vo.ImmobilizationEndDate != null)
            {
                if (vo.ImmobilizationEndDate != "")
                {
                    vesselArrestImmobilizationSAMSA.ImmobilizationEndDate = DateTime.Parse(vo.ImmobilizationEndDate, CultureInfo.InvariantCulture);
                }
            }

            vesselArrestImmobilizationSAMSA.ExactWorkProposed = vo.ExactWorkProposed;

            if (vo.ApprovedDate != null)
            {
                if (vo.ApprovedDate != "")
                {
                    vesselArrestImmobilizationSAMSA.ApprovedDate = DateTime.Parse(vo.ApprovedDate, CultureInfo.InvariantCulture);
                }
            }
            if (vo.SAMSAStopDate != null)
            {
                if (vo.SAMSAStopDate != "")
                {
                    vesselArrestImmobilizationSAMSA.SAMSAStopDate = DateTime.Parse(vo.SAMSAStopDate, CultureInfo.InvariantCulture);
                }
            }

            vesselArrestImmobilizationSAMSA.SAMSAStopRemarks = vo.SAMSAStopRemarks;

            if (vo.SAMSAClearedDate != null)
            {
                if (vo.SAMSAClearedDate != "")
                {
                    vesselArrestImmobilizationSAMSA.SAMSAClearedDate = DateTime.Parse(vo.SAMSAClearedDate, CultureInfo.InvariantCulture);
                }
            }

            vesselArrestImmobilizationSAMSA.SAMSAClearedRemarks = vo.SAMSAClearedRemarks;
            vesselArrestImmobilizationSAMSA.RecordStatus = vo.RecordStatus;
            vesselArrestImmobilizationSAMSA.CreatedBy = vo.CreatedBy;
            vesselArrestImmobilizationSAMSA.CreatedDate = vo.CreatedDate;
            vesselArrestImmobilizationSAMSA.ModifiedBy = vo.ModifiedBy;
            vesselArrestImmobilizationSAMSA.ModifiedDate = vo.ModifiedDate;

            vesselArrestImmobilizationSAMSA.VesselReleased = vo.VesselReleasedStatus == true ? "Y" : "N";
            vesselArrestImmobilizationSAMSA.VesselArrested = vo.VesselArrestedStatus == true ? "Y" : "N";
            vesselArrestImmobilizationSAMSA.Immobilization = vo.ImmobilizationStatus == true ? "Y" : "N";
            vesselArrestImmobilizationSAMSA.PollutionPrecautionTaken = vo.PollutionPrecautionTakenStatus == true ? "Y" : "N";
            vesselArrestImmobilizationSAMSA.SAMSAStop = vo.SAMSAStopStatus == true ? "Y" : "N";
            vesselArrestImmobilizationSAMSA.SAMSACleared = vo.SAMSAClearedStatus == true ? "Y" : "N";

            vesselArrestImmobilizationSAMSA.VesselArrestDocuments = vo.VesselArrestDocuments.MapToEntity();
            vesselArrestImmobilizationSAMSA.VesselSAMSAStopDocuments = vo.VesselSAMSAStopDocuments.MapToEntity();

            return vesselArrestImmobilizationSAMSA;
        }
    }
}
