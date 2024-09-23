using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;


namespace IPMS.Domain.DTOS
{
    public static class VesselEngineMapExtension
    {
        public static VesselEngine MapToEntity(this VesselEngineVO vo)
        {
            VesselEngine ve = new VesselEngine();

            if (vo != null)
            {
                ve.VesselEngineID = vo.VesselEngineID;
                ve.VesselID = vo.VesselID;
                ve.EnginePower = vo.EnginePower;
                ve.EngineType = vo.EngineType;
                ve.PropulsionType = vo.PropulsionType;
                ve.NoOfPropellers = vo.NoOfPropellers;
                ve.MaxSpeed = vo.MaxSpeed;
                ve.RecordStatus = vo.RecordStatus;
                ve.CreatedBy = vo.CreatedBy;
                ve.CreatedDate = vo.CreatedDate;
                ve.ModifiedBy = vo.ModifiedBy;
                ve.ModifiedDate = vo.ModifiedDate;
            }
            return ve;
        }
        public static VesselEngineVO MapToDTO(this VesselEngine data)
        {
            VesselEngineVO VO = new VesselEngineVO();
            if (data != null)
            {
                VO.VesselEngineID = data.VesselEngineID;
                VO.VesselID = data.VesselID;
                VO.EnginePower = data.EnginePower;
                VO.EngineType = data.EngineType;
                VO.PropulsionType = data.PropulsionType;
                VO.NoOfPropellers = data.NoOfPropellers;
                VO.MaxSpeed = data.MaxSpeed;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static List<VesselEngine> MapToEntity(this List<VesselEngineVO> vos)
        {
            List<VesselEngine> VesselEntities = new List<VesselEngine>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    if (vesselvo.EnginePower > 0 && !string.IsNullOrWhiteSpace(vesselvo.EngineType) && !string.IsNullOrWhiteSpace(vesselvo.PropulsionType))
                        VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }
        public static List<VesselEngineVO> MapToDto(this List<VesselEngine> data)
        {
            List<VesselEngineVO> vos = new List<VesselEngineVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDTO());
                }
            }
            return vos;
        }
    }
}
