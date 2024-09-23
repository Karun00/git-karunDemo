using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class OutTurnVolumeMapExtension
    {
        public static List<OutTurnVolume> MapToEntity(this List<OutTurnVolumesVO> data)
        {
            List<OutTurnVolume> list = new List<OutTurnVolume>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(item.MapToEntity());
                }
            }
            return list;
        }

        public static List<OutTurnVolumesVO> MapToDTO(this List<OutTurnVolume> data)
        {
            List<OutTurnVolumesVO> tdlist = new List<OutTurnVolumesVO>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    tdlist.Add(item.MapToDTO());
                }
            }
            return tdlist;
        }

        public static OutTurnVolume MapToEntity(this OutTurnVolumesVO data)
        {
            OutTurnVolume terminalDelayValues = new OutTurnVolume
            {
                OutTurnVolumeID = data.OutTurnVolumeID,
                IMONo = data.IMONo, 
                ArrivalDate= data.ArrivalDate,
                cargoType=data.CargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                OutturnVolume=data.OutTurnVolume,
                FirstCraneSwing=data.FirstCraneSwing,
                LastCraneSwing=data.LastCraneSwing,
                NoOfCranes=data.NoOfCranes,
                Comments=data.Comments,                 
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault(),
                PortCode = data.PortCode  
       
            };
            return terminalDelayValues;
        }

        public static OutTurnVolumesVO MapToDTO(this OutTurnVolume data)
        {
            OutTurnVolumesVO terminalDelaysvo = new OutTurnVolumesVO
            {
                OutTurnVolumeID=data.OutTurnVolumeID,
                IMONo = data.IMONo, 
                ArrivalDate= data.ArrivalDate,
                CargoType=data.cargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                OutTurnVolume=data.OutturnVolume,
                FirstCraneSwing=data.FirstCraneSwing,
                LastCraneSwing= data.LastCraneSwing,
                NoOfCranes=data.NoOfCranes,
                Comments=data.Comments,                 
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault(),
                PortCode = data.PortCode   
            };

            return terminalDelaysvo;
        }
    }
}
