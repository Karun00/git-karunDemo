using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class VehiclePermitMapExtension
    {
        public static List<VehiclePermit> MapToEntity(this IEnumerable<VehiclePermitVO> vos)
        {
            List<VehiclePermit> entities = new List<VehiclePermit>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<VehiclePermitVO> MapToDTO(this IEnumerable<VehiclePermit> entities)
        {
            List<VehiclePermitVO> vos = new List<VehiclePermitVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
        public static VehiclePermitVO MapToDTO(this VehiclePermit data)
        {
            VehiclePermitVO Vo = new VehiclePermitVO();
            Vo.VehiclePermitID = data.VehiclePermitID;
            Vo.PermitRequestID = data.PermitRequestID;
            Vo.VehicleMake = data.VehicleMake;
            Vo.VehicleRegnNo = data.VehicleRegnNo;
            Vo.PermitRequirementCode = data.PermitRequirementCode;
            Vo.Reason = data.Reason;
            Vo.RecordStatus = data.RecordStatus;
            Vo.CreatedBy = data.CreatedBy;
            Vo.CreatedDate = data.CreatedDate;
            Vo.ModifiedBy = data.ModifiedBy;
            Vo.CreatedBy = data.CreatedBy;
            Vo.ModifiedDate = data.ModifiedDate;
            return Vo;
        }
        public static VehiclePermit MapToEntity(this VehiclePermitVO VO)
        {
            VehiclePermit data = new VehiclePermit();
            data.VehiclePermitID = VO.VehiclePermitID;
            data.PermitRequestID = VO.PermitRequestID;
            data.VehicleMake = VO.VehicleMake;
            data.VehicleRegnNo = VO.VehicleRegnNo;
            data.PermitRequirementCode = VO.PermitRequirementCode;
            data.Reason = VO.Reason;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.CreatedBy = VO.CreatedBy;
            data.ModifiedDate = VO.ModifiedDate;
            return data;
        }

        public static VehiclePermitVO MapToDTOObj(this IEnumerable<VehiclePermit> VehiclePermit)
        {
            var VehiclePermitVOList = new VehiclePermitVO();
            foreach (var data in VehiclePermit)
            {
                VehiclePermitVOList.VehiclePermitID = data.VehiclePermitID;
                VehiclePermitVOList.PermitRequestID = data.PermitRequestID;
                VehiclePermitVOList.VehicleMake = data.VehicleMake;
                VehiclePermitVOList.VehicleRegnNo = data.VehicleRegnNo;
                VehiclePermitVOList.PermitRequirementCode = data.PermitRequirementCode;
                VehiclePermitVOList.Reason = data.Reason;
                VehiclePermitVOList.RecordStatus = data.RecordStatus;
                VehiclePermitVOList.CreatedBy = data.CreatedBy;
                VehiclePermitVOList.CreatedDate = data.CreatedDate;
                VehiclePermitVOList.ModifiedBy = data.ModifiedBy;
                VehiclePermitVOList.CreatedBy = data.CreatedBy;
                VehiclePermitVOList.ModifiedDate = data.ModifiedDate;

            }
            return VehiclePermitVOList;
        }

        public static List<string> MapToPermitRequirementCodeArray(this ICollection<VehiclePermitRequirementCode> VehiclePermitRequirementCodes)
        {
            List<string> selectedPermitRequirementCodes = new List<string>();
            if (VehiclePermitRequirementCodes != null)
            {

                foreach (var VehiclePermitRequirementCode in VehiclePermitRequirementCodes)
                {
                    selectedPermitRequirementCodes.Add(VehiclePermitRequirementCode.PermitRequirementCode);


                }
            }
            return selectedPermitRequirementCodes;
        }
    }
}
