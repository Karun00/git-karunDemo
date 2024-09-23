using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class TerminalDelayMapExtension
    {
        public static List<TerminalDelay> MapToEntity(this List<TerminalDelaysVO> data)
        {
            List<TerminalDelay> list = new List<TerminalDelay>();
            foreach (var item in data)
            {
                list.Add(item.MapToEntity());
            }
            return list;
        }

        public static List<TerminalDelaysVO> MapToDTO(this List<TerminalDelay> data)
        {
            List<TerminalDelaysVO> tdlist = new List<TerminalDelaysVO>();
            foreach (var item in data)
            {
                tdlist.Add(item.MapToDTO());
            }
            return tdlist;
        }

        public static TerminalDelay MapToEntity(this TerminalDelaysVO data)
        {
            TerminalDelay terminalDelayValues = new TerminalDelay
            {
                TerminalDelayID = data.TerminalDelayID,
                IMONo = data.IMONo, 
                ArrivalDate= data.ArrivalDate,
                cargoType=data.CargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                DelayDuration=data.DelayDuration,
                ReasonForDelay=data.ReasonForDelay,
                Comments=data.Comments,                 
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault(),
                PortCode = data.PortCode  
       
            };
            return terminalDelayValues;
        }

        public static TerminalDelaysVO MapToDTO(this TerminalDelay data)
        {
            TerminalDelaysVO terminalDelaysvo = new TerminalDelaysVO
            {
                TerminalDelayID=data.TerminalDelayID,
                IMONo = data.IMONo, 
                ArrivalDate= data.ArrivalDate,
                CargoType=data.cargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                DelayDuration=data.DelayDuration,
                ReasonForDelay=data.ReasonForDelay,
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
