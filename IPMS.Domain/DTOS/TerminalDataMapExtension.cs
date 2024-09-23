using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class TerminalDataMapExtension
    {
        public static List<TerminalData> MapToEntity(this List<TerminalWeeklyDataVO> data)
        {
            List<TerminalData> list = new List<TerminalData>();
            foreach (var item in data)
            {
                list.Add(item.MapToEntity());
            }
            return list;
        }

        public static List<TerminalWeeklyDataVO> MapToDTO(this List<TerminalData> data)
        {
            List<TerminalWeeklyDataVO> tdlist = new List<TerminalWeeklyDataVO>();
            foreach (var item in data)
            {
                tdlist.Add(item.MapToDTO());
            }
            return tdlist;
        }

        public static TerminalData MapToEntity(this TerminalWeeklyDataVO data)
        {
            TerminalData terminalDelayValues = new TerminalData
            {
                TerminalDataID = data.TerminalDataID,
                WeekNo=data.WeekNo,
                WeekEnding=data.WeekEnding,
                PerformanceArea=data.PerformanceArea,
                measure=data.Measure,
                CargoType=data.CargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                Planned_Qty=data.Planned,
                Actual_Qty=data.Actual,
                Comments=data.Comments,                 
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault(),
                PortCode = data.PortCode  
       
            };
            return terminalDelayValues;
        }

        public static TerminalWeeklyDataVO MapToDTO(this TerminalData data)
        {
            TerminalWeeklyDataVO terminalDelaysvo = new TerminalWeeklyDataVO
            {
                TerminalDataID=data.TerminalDataID,
                WeekNo = data.WeekNo, 
                WeekEnding=data.WeekEnding,
                PerformanceArea=data.PerformanceArea,
                Measure=data.measure,
                CargoType=data.CargoType,
                UnitOfMeasure =data.UnitOfMeasure,
                Planned=data.Planned_Qty,
                Actual=data.Actual_Qty, 
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
