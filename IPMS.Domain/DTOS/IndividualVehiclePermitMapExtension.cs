using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class IndividualVehiclePermitMapExtension
    {
        public static List<IndividualVehiclePermit> MapToEntity(this IEnumerable<IndividualVehiclePermitVO> vos)
        {
            List<IndividualVehiclePermit> entities = new List<IndividualVehiclePermit>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<IndividualVehiclePermitVO> MapToDTO(this IEnumerable<IndividualVehiclePermit> entities)
        {
            List<IndividualVehiclePermitVO> vos = new List<IndividualVehiclePermitVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
        public static IndividualVehiclePermitVO MapToDTO(this IndividualVehiclePermit data)
        {
            IndividualVehiclePermitVO Vo = new IndividualVehiclePermitVO();
            Vo.IndividualVehiclePermitID = data.IndividualVehiclePermitID;
            Vo.PermitRequestID = data.PermitRequestID;
            Vo.VehicleMake = data.VehicleMake;
            Vo.VehicleRegnNo = data.VehicleRegnNo;
            Vo.Chassis_VinNo = data.Chassis_VinNo;
            Vo.VehicleModel = data.VehicleModel;
            Vo.Colour = data.Colour;
            return Vo;
        }
        public static IndividualVehiclePermit MapToEntity(this IndividualVehiclePermitVO VO)
        {
            IndividualVehiclePermit data = new IndividualVehiclePermit();
            data.IndividualVehiclePermitID = VO.IndividualVehiclePermitID;
            data.PermitRequestID = VO.PermitRequestID;
            data.VehicleMake = VO.VehicleMake;
            data.VehicleRegnNo = VO.VehicleRegnNo;
            data.Chassis_VinNo = VO.Chassis_VinNo;
            data.VehicleModel = VO.VehicleModel;
            data.Colour = VO.Colour;
            return data;
        }

        public static IndividualVehiclePermitVO MapToDTOObj(this IEnumerable<IndividualVehiclePermit> IndividualVehiclePermit)
        {
            var IndividualVehiclePermitVOList = new IndividualVehiclePermitVO();
            foreach (var data in IndividualVehiclePermit)
            {
                IndividualVehiclePermitVOList.IndividualVehiclePermitID = data.IndividualVehiclePermitID;
                IndividualVehiclePermitVOList.PermitRequestID = data.PermitRequestID;
                IndividualVehiclePermitVOList.VehicleMake = data.VehicleMake;
                IndividualVehiclePermitVOList.VehicleRegnNo = data.VehicleRegnNo;
                IndividualVehiclePermitVOList.Chassis_VinNo = data.Chassis_VinNo;
                IndividualVehiclePermitVOList.VehicleModel = data.VehicleModel;
                IndividualVehiclePermitVOList.Colour = data.Colour;
              
            }
            return IndividualVehiclePermitVOList;
        }


    }
}