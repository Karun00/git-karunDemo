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
    public static class VesselAgentChangeMapExtenstion
    {
        public static VesselAgentChange MapToEntity(this VesselAgentChangeVO vo)
        {
            VesselAgentChange vessel = new VesselAgentChange();
            vessel.VesselAgentChangeID = vo.VesselAgentChangeID;
            vessel.VCN = vo.VCN;
            vessel.ProposedAgent = vo.ProposedAgent;
            vessel.RecordStatus = vo.RecordStatus;
            vessel.CreatedBy = vo.CreatedBy;
            vessel.CreatedDate = vo.CreatedDate;
            vessel.ModifiedBy = vo.ModifiedBy;
            vessel.ModifiedDate = vo.ModifiedDate;
            vessel.ReasonForTransferCode = vo.ReasonForTransferCode;
            vessel.EffectiveDateTime = Convert.ToDateTime(vo.EffectiveDateTime,CultureInfo.InvariantCulture);
            vessel.WorkflowInstanceId = vo.WorkflowInstanceId;
            vessel.CurrentAgentID = vo.CurrentAgentID;
            vessel.ProposedAgentName = vo.ProposedAgentName;
            vessel.RequestedAgentName = vo.RequestedAgentName;
            vessel.VesselAgentChangeDocuments = vo.VesselAgentChangeDocuments.MapToEntity();
            //vessel.ArrivalNotification = vo.ArrivalNotification.MapToEntity();
            //vessel.Agent = vo.Agent.MapToEntity();

            return vessel;
        }
        public static VesselAgentChangeVO MapToDto(this VesselAgentChange data)
        {
            VesselAgentChangeVO VO = new VesselAgentChangeVO();
            VO.VesselAgentChangeID = data.VesselAgentChangeID;
            VO.VCN = data.VCN;
            VO.ProposedAgent = data.ProposedAgent;
            VO.RecordStatus = data.RecordStatus;
            VO.CreatedBy = data.CreatedBy;
            VO.CreatedDate = data.CreatedDate;
            VO.ModifiedBy = data.ModifiedBy;
            VO.ModifiedDate = data.ModifiedDate;
            VO.ReasonForTransferCode = data.ReasonForTransferCode;
            VO.WorkflowInstanceId = data.WorkflowInstanceId;
            VO.CurrentAgentID = data.CurrentAgentID;
            VO.EffectiveDateTime = Convert.ToString(data.EffectiveDateTime,CultureInfo.InvariantCulture);
            VO.ArrivalNotification = data.ArrivalNotification.MapToDto();
            VO.WorkFlowStatus = data.WorkflowInstatnce.SubCategory != null ? ((data.WorkflowInstatnce.SubCategory.SubCatName == "Confirm" || data.WorkflowInstatnce.SubCategory.SubCatName == "Verify") ? "Confirmed" : data.WorkflowInstatnce.SubCategory.SubCatName == "Approve" ? "Approved" : data.WorkflowInstatnce.SubCategory.SubCatName == "Reject" ? "Rejected" : data.WorkflowInstatnce.SubCategory.SubCatName == "Request Reject" ? "Request Rejected" : data.WorkflowInstatnce.SubCategory.SubCatName) : "";
            VO.ProposedAgentName = data.ProposedAgentName;
            VO.RequestedAgentName = data.RequestedAgentName;
            VO.Agent = data.Agent.MapToDTO();
            VO.VesselAgentChangeDocuments = data.VesselAgentChangeDocuments.MapToDto();
            // VO.ReasonForVisit = data.ArrivalNotification != null ? (data.ArrivalNotification.SubCategory3 != null ? data.ArrivalNotification.SubCategory3.SubCatName : string.Empty) : string.Empty;


            if (data.ArrivalNotification != null)
            {
                if (data.ArrivalNotification.ArrivalReasons.Count > 0)
                {
                    foreach (ArrivalReason ar in data.ArrivalNotification.ArrivalReasons)
                    {

                        if (VO.ReasonForVisitName == null)
                            VO.ReasonForVisitName = ar.SubCategory.SubCatName;
                        else
                            VO.ReasonForVisitName = VO.ReasonForVisitName + "," + ar.SubCategory.SubCatName;
                    }

                }
            }


           
            VO.WorkflowInstanceId = data.WorkflowInstatnce != null ? data.WorkflowInstatnce.WorkflowInstanceId : default(int);

            return VO;
        }
        public static List<VesselAgentChange> MapToEntity(this List<VesselAgentChangeVO> vos)
        {
            List<VesselAgentChange> VesselEntities = new List<VesselAgentChange>();
            foreach (var vesselvo in vos)
            {
                VesselEntities.Add(vesselvo.MapToEntity());
            }
            return VesselEntities;
        }
        public static List<VesselAgentChangeVO> MapToDTO(this List<VesselAgentChange> data)
        {
            List<VesselAgentChangeVO> vos = new List<VesselAgentChangeVO>();
            foreach (var vessel in data)
            {
                vos.Add(vessel.MapToDto());
            }
            return vos;
        }
    }
}
