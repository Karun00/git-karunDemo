using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class VesselGearMapExtension
    {
        public static VesselGear MapToEntity(this VesselGearVO vo)
        {
            VesselGear ve = new VesselGear();
            if (vo != null)
            {
                ve.VesselGearID = vo.VesselGearID;
                ve.VesselID = vo.VesselID;
                ve.GearTypeM = vo.GearTypeM;
                ve.SafeWorkingLoad = vo.SafeWorkingLoad;
                ve.GearCapacityCBM = vo.GearCapacityCBM;
                ve.Description = vo.Description;
                ve.RecordStatus = vo.RecordStatus;
                ve.CreatedBy = vo.CreatedBy;
                ve.CreatedDate = vo.CreatedDate;
                ve.ModifiedBy = vo.ModifiedBy;
                ve.ModifiedDate = vo.ModifiedDate;
            }
            return ve;
        }
        public static VesselGearVO MapToDto(this VesselGear data)
        {
            VesselGearVO VO = new VesselGearVO();
            if (data != null)
            {
                VO.VesselGearID = data.VesselGearID;
                VO.VesselID = data.VesselID;
                VO.GearTypeM = data.GearTypeM;
                VO.SafeWorkingLoad = data.SafeWorkingLoad;
                VO.GearCapacityCBM = data.GearCapacityCBM;
                VO.Description = data.Description;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static List<VesselGear> MapToEntity(this List<VesselGearVO> vos)
        {
            List<VesselGear> VesselEntities = new List<VesselGear>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    if (vesselvo.GearTypeM > 0 && vesselvo.GearCapacityCBM > 0)
                        VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }
        public static List<VesselGearVO> MapToDto(this List<VesselGear> data)
        {
            List<VesselGearVO> vos = new List<VesselGearVO>();
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
