using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class VesselGrabMapExtension
    {
        public static VesselGrab MapToEntity(this VesselGrabVO vo)
        {
            VesselGrab vg = new VesselGrab();
            if (vo != null)
            {
                vg.VesselGrabID = vo.VesselGrabID;
                vg.VesselID = vo.VesselID;
                vg.GrabTypeM = vo.GrabTypeM;
                vg.SafeWorkingLoad = vo.SafeWorkingLoad;
                vg.GrabCapacityCBM = vo.GrabCapacityCBM;
                vg.Description = vo.Description;
                vg.RecordStatus = vo.RecordStatus;
                vg.CreatedBy = vo.CreatedBy;
                vg.CreatedDate = vo.CreatedDate;
                vg.ModifiedBy = vo.ModifiedBy;
                vg.ModifiedDate = vo.ModifiedDate;
            }
            return vg;
        }
        public static VesselGrabVO MapToDto(this VesselGrab data)
        {
            VesselGrabVO VO = new VesselGrabVO();
            if (data != null)
            {
                VO.VesselGrabID = data.VesselGrabID;
                VO.VesselID = data.VesselID;
                VO.GrabTypeM = data.GrabTypeM;
                VO.SafeWorkingLoad = data.SafeWorkingLoad;
                VO.GrabCapacityCBM = data.GrabCapacityCBM;
                VO.Description = data.Description;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static List<VesselGrab> MapToEntity(this List<VesselGrabVO> vos)
        {
            List<VesselGrab> VesselEntities = new List<VesselGrab>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    if (vesselvo.GrabTypeM > 0 && vesselvo.GrabCapacityCBM > 0)
                        VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }
        public static List<VesselGrabVO> MapToDto(this List<VesselGrab> data)
        {
            List<VesselGrabVO> vos = new List<VesselGrabVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDto());
                }
            }
            return vos;
        }

    }
}
