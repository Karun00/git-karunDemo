using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{

    public static class DockingPlanMapExtension
    {
        public static List<DockingPlanVO> MapToDtoForDocking(this List<DockingPlan> dockingplans)
        {
            List<DockingPlanVO> dockingplanvos = new List<DockingPlanVO>();
            if (dockingplans != null)
            {
                foreach (var dockingplan in dockingplans)
                {
                    dockingplanvos.Add(dockingplan.MapToDto());
                }
            }
            return dockingplanvos;
        }

        public static DockingPlanVO MapToDto(this DockingPlan data)
        {
            DockingPlanVO VO = new DockingPlanVO();
            if (data != null)
            {
                VO.DockingPlanID = data.DockingPlanID;
                VO.VesselID = data.VesselID;
                //VO.DocumentID = data.DocumentID;
                // VO.WorkflowInstanceID = data.WorkflowInstanceID;
                VO.Remarks = data.Remarks;
                VO.VesselName = data.Vessel.VesselName;
                VO.IMONo = data.Vessel.IMONo;
                VO.VesselType = data.Vessel.SubCategory3.SubCatName;
                VO.LengthOverallInM = data.Vessel.LengthOverallInM;
                VO.BeamInM = data.Vessel.BeamInM;
                //VO.PortOfRegistry = data.Vessel.SubCategory1.SubCatName;
                //VO.PortOfRegistry = data.Vessel.Port.PortName;
                VO.PortOfRegistry = data.Vessel.PortRegistry.PortName;
                VO.DockingPlanStatus = data.WorkflowInstance.SubCategory.SubCatName;
                VO.DockingPlanNo = data.DockingPlanNo;
                VO.AgentName = data.VesselAgent;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.WorkflowInstanceID = data.WorkflowInstanceID;
                VO.IsFinal = data.IsFinal;
                VO.VesselGRT = data.Vessel.GrossRegisteredTonnageInMT;
                VO.DockingPlanDocumentsVO = data.DockingPlanDocuments.MapToDto();
            }

            return VO;
        }

        public static DockingPlan MapToEntity(this DockingPlanVO vo)
        {
            DockingPlan data = new DockingPlan();
            if (vo != null)
            {
                data.DockingPlanID = vo.DockingPlanID;
                data.ReferenceNo = vo.DockingPlanNo;
                data.VesselID = vo.VesselID;
                data.DockingPlanNo = vo.DockingPlanNo;
                // data.DocumentID = VO.DocumentID;
                //  data.WorkflowInstanceID = VO.WorkflowInstanceID;
                data.PortCode = vo.PortCode;
                data.Remarks = vo.Remarks;
                data.RecordStatus = vo.RecordStatus;
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
                data.VesselAgent = vo.AgentName;
                data.VesselName = vo.VesselName;
                data.ApplicationDateTime = vo.CreatedDate;
                data.WorkflowInstanceID = vo.WorkflowInstanceID;

                data.IsFinal = (String.IsNullOrEmpty(vo.IsFinal) ? null : vo.IsFinal);

                data.DockingPlanDocuments = vo.DockingPlanDocumentsVO != null ? vo.DockingPlanDocumentsVO.MapToEntity() : null;
            }
            return data;
        }


    }
}
