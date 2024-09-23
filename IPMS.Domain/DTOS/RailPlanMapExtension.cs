using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class RailPlanMapExtension
    {
        public static List<RailPlan> MapToEntity(this List<RailPlanVO> data)
        {
            List<RailPlan> list = new List<RailPlan>();
            foreach (var item in data)
            {
                list.Add(item.MapToEntity());
            }
            return list;
        }

        public static List<RailPlanVO> MapToDTO(this List<RailPlan> data)
        {
            List<RailPlanVO> tdlist = new List<RailPlanVO>();
            foreach (var item in data)
            {
                tdlist.Add(item.MapToDTO());
            }
            return tdlist;
        }

        public static RailPlan MapToEntity(this RailPlanVO data)
        {
            RailPlan railPlanValues = new RailPlan
            {
                RailPlanNo = data.RailPlanID,
                Corridor=data.Corridor,
                PlannedDate = data.PlannedDate,
                Schedule=data.Schedule,
                TrainNo=data.TrainNo,
                Origin=data.Origin,
                Destination=data.Destination,
                BreakType=data.BreakType,
                PlannedETD=data.PlannedETD,
                PlannedETA=data.PlannedETA,
                Loco =data.Loco,
                LocoQty=data.LocoQty,
                NWBRef=data.NWBRef,
                PlannedTons=data.PlannedTons,
                Load=data.Load,
                Remark=data.Remark,
                YQ=data.YQ,
                ReasonForChange=data.ReasonForChange,
                NewETD=data.NewETD,
                NewETA=data.NewETA,
                TrainMovement=data.TrainMovement,
                ATD=data.ATD,
                ATA=data.ATA,               
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault() 
       
            };
            return railPlanValues;
        }

        public static RailPlanVO MapToDTO(this RailPlan data)
        {
            RailPlanVO railPlanvo = new RailPlanVO
            {
                RailPlanID = data.RailPlanNo,
                Corridor = data.Corridor,
                PlannedDate = data.PlannedDate,
                Schedule = data.Schedule,
                TrainNo = data.TrainNo,
                Origin=data.Origin,
                Destination =data.Destination,
                BreakType=data.BreakType,
                PlannedETD=data.PlannedETD, 
                PlannedETA=data.PlannedETA,
                Loco=data.Loco,
                LocoQty=data.LocoQty,
                NWBRef=data.NWBRef,
                PlannedTons=data.PlannedTons,
                Load=data.Load,
                Remark=data.Remark,
                YQ=data.YQ,
                TrainStatus=data.TrainStatus,
                ReasonForChange=data.ReasonForChange,
                NewETD=data.NewETD,
                NewETA=data.NewETA,
                TrainMovement=data.TrainMovement,
                ATD=data.ATD,
                ATA=data.ATA, 
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault() 
            };

            return railPlanvo;
        }
    }
}
