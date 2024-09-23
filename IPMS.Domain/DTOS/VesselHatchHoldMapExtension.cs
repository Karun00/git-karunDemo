using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;


namespace IPMS.Domain.DTOS
{
    public static class VesselHatchHoldMapExtension
    {
        public static VesselHatchHold MapToEntity(this VesselHatchHoldVO vo)
        {
            VesselHatchHold ve = new VesselHatchHold();
            if (vo != null)
            {
                ve.VesselHatchHoldID = vo.VesselHatchHoldID;
                ve.VesselID = vo.VesselID;
                ve.HatchHoldTypeM = vo.HatchHoldTypeM;
                ve.SafeWorkingLoad = vo.SafeWorkingLoad;
                ve.HoldCapacityCBM = vo.HoldCapacityCBM;
                ve.Description = vo.Description;
                ve.RecordStatus = vo.RecordStatus;
                ve.CreatedBy = vo.CreatedBy;
                ve.CreatedDate = vo.CreatedDate;
                ve.ModifiedBy = vo.ModifiedBy;
                ve.ModifiedDate = vo.ModifiedDate;
            }
            return ve;
        }
        public static VesselHatchHoldVO MapToDto(this VesselHatchHold data)
        {
            VesselHatchHoldVO VO = new VesselHatchHoldVO();
            if (data != null)
            {
                VO.VesselHatchHoldID = data.VesselHatchHoldID;
                VO.VesselID = data.VesselID;
                VO.HatchHoldTypeM = data.HatchHoldTypeM;
                VO.SafeWorkingLoad = data.SafeWorkingLoad;
                VO.HoldCapacityCBM = data.HoldCapacityCBM;
                VO.Description = data.Description;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static List<VesselHatchHold> MapToEntity(this List<VesselHatchHoldVO> vos)
        {
            List<VesselHatchHold> VesselEntities = new List<VesselHatchHold>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    if (vesselvo.HatchHoldTypeM > 0 && vesselvo.SafeWorkingLoad > 0 && vesselvo.HoldCapacityCBM > 0)
                        VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }
        public static List<VesselHatchHoldVO> MapToDto(this List<VesselHatchHold> data)
        {
            List<VesselHatchHoldVO> vos = new List<VesselHatchHoldVO>();
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
