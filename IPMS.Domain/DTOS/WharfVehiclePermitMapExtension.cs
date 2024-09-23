using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class WharfVehiclePermitMapExtension
    {            
        public static List<WharfVehiclePermit> MapToEntity(this IEnumerable<WharfVehiclePermitVO> vos)
        {
            List<WharfVehiclePermit> entities = new List<WharfVehiclePermit>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<WharfVehiclePermitVO> MapToDTO(this IEnumerable<WharfVehiclePermit> entities)
        {
            List<WharfVehiclePermitVO> vos = new List<WharfVehiclePermitVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
        public static WharfVehiclePermitVO MapToDTO(this WharfVehiclePermit data)
        {
            WharfVehiclePermitVO Vo = new WharfVehiclePermitVO();
            Vo.WharfVehiclePermitID = data.WharfVehiclePermitID;
            Vo.PermitRequestID = data.PermitRequestID;
            Vo.VehicleMake = data.VehicleMake;         
              Vo.VehicleModel = data.VehicleModel;
            Vo.VehicleRegnNo = data.VehicleRegnNo;
            Vo.VehicleDescription = data.VehicleDescription;   
              Vo.VehicleRegisterd = data.VehicleRegisterd;
              Vo.Hometelephone = data.Hometelephone;
            Vo.VehiclePointed = data.VehiclePointed;
            Vo.TelephoneNo = data.TelephoneNo;
            Vo.ContractDuration = data.ContractDuration;
            Vo.MobileNo = data.MobileNo;
            Vo.PermitRequirement = data.PermitRequirement;
            Vo.ContractorNo = data.ContractorNo;
            Vo.TemporaryPermits = data.TemporaryPermits;
            Vo.AccessGates = data.AccessGates;
            Vo.OtherSpecify = data.OtherSpecify;

            Vo.Reason = data.Reason;   
            return Vo;
        }
        public static WharfVehiclePermit MapToEntity(this WharfVehiclePermitVO VO)
        {
            WharfVehiclePermit data = new WharfVehiclePermit();
              data.WharfVehiclePermitID = VO.WharfVehiclePermitID;
            data.PermitRequestID = VO.PermitRequestID;
            data.VehicleMake = VO.VehicleMake;         
              data.VehicleModel = VO.VehicleModel;
            data.VehicleRegnNo = VO.VehicleRegnNo;
            data.VehicleDescription = VO.VehicleDescription;
            data.Hometelephone = VO.Hometelephone;
              data.VehicleRegisterd = VO.VehicleRegisterd;
            data.VehiclePointed = VO.VehiclePointed;
            data.TelephoneNo = VO.TelephoneNo;
            data.ContractDuration = VO.ContractDuration;
            data.MobileNo = VO.MobileNo;
            data.PermitRequirement = VO.PermitRequirement;
            data.ContractorNo = VO.ContractorNo;
            data.TemporaryPermits = VO.TemporaryPermits;
            data.AccessGates = VO.AccessGates;
            data.OtherSpecify = VO.OtherSpecify;
            data.Reason = VO.Reason;            
            return data;
        }
        public static WharfVehiclePermitVO MapToDTOObj(this IEnumerable<WharfVehiclePermit> WharfVehiclePermit)
        {
            var WharfVehiclePermitVOList = new WharfVehiclePermitVO();
            foreach (var data in WharfVehiclePermit)
            { WharfVehiclePermitVOList.WharfVehiclePermitID= data.WharfVehiclePermitID;
            WharfVehiclePermitVOList.PermitRequestID = data.PermitRequestID;
            WharfVehiclePermitVOList.VehicleMake = data.VehicleMake;
            WharfVehiclePermitVOList.VehicleModel = data.VehicleModel;
            WharfVehiclePermitVOList.VehicleRegnNo = data.VehicleRegnNo;
            WharfVehiclePermitVOList.Hometelephone = data.Hometelephone;
            WharfVehiclePermitVOList.VehicleDescription = data.VehicleDescription;
            WharfVehiclePermitVOList.VehicleRegisterd = data.VehicleRegisterd;
            WharfVehiclePermitVOList.VehiclePointed = data.VehiclePointed;
            WharfVehiclePermitVOList.Reason = data.Reason;
            WharfVehiclePermitVOList.MobileNo = data.MobileNo;
            WharfVehiclePermitVOList.PermitRequirement = data.PermitRequirement;
            WharfVehiclePermitVOList.ContractorNo = data.ContractorNo;
            WharfVehiclePermitVOList.TemporaryPermits = data.TemporaryPermits;
            WharfVehiclePermitVOList.AccessGates = data.AccessGates;
            WharfVehiclePermitVOList.OtherSpecify = data.OtherSpecify;
            WharfVehiclePermitVOList.RecordStatus = data.RecordStatus;
            WharfVehiclePermitVOList.TelephoneNo = data.TelephoneNo;
            WharfVehiclePermitVOList.ContractDuration = data.ContractDuration;
            }
            return WharfVehiclePermitVOList;
        }

        public static List<string> MapToAccessGatesArray(this ICollection<PermitRequestAccessGates> PermitRequestAccessGates)
        {
            List<string> selectedAccessGates = new List<string>();
            if (PermitRequestAccessGates != null)
            {

                foreach (var PermitRequestAccessGate in PermitRequestAccessGates)
                {
                    selectedAccessGates.Add(PermitRequestAccessGate.AccessGates);
                  

                }
            }
            return selectedAccessGates;
        }
    }
    }

