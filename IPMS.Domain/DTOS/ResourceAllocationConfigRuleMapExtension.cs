using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class ResourceAllocationConfigRuleMapExtension
    {
        public static ResourceAllocationConfigRuleVO MapToDTO(this ResourceAllocationConfigRule data)
        {
            ResourceAllocationConfigRuleVO RACRvo = new ResourceAllocationConfigRuleVO();
            if (data != null)
            {
                RACRvo.ResourceAllocationConfigRuleID = data.ResourceAllocationConfigRuleID;
                RACRvo.PortCode = data.PortCode;
                RACRvo.PilotCapacity = data.PilotCapacity;
                RACRvo.TotalTugs = data.TotalTugs;
                RACRvo.EffectedFrom = Convert.ToDateTime(data.EffectedFrom, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                RACRvo.RecordStatus = data.RecordStatus;
                RACRvo.CreatedBy = data.CreatedBy;
                RACRvo.CreatedDate = data.CreatedDate;
                RACRvo.ModifiedBy = data.ModifiedBy;
                RACRvo.ModifiedDate = data.ModifiedDate;

                RACRvo.arrivalservicetype = data.ResourceAllocationMovementTypeRules.ArrivalMapToDTO();
                RACRvo.shiftingservicetype = data.ResourceAllocationMovementTypeRules.ShiftingMapToDTO();
                RACRvo.sailingservicetype = data.ResourceAllocationMovementTypeRules.SailingMapToDTO();
                RACRvo.warpingservicetype = data.ResourceAllocationMovementTypeRules.WarpingMapToDTO();
                if (data.ResourceGangConfigs != null)
                {
                    RACRvo.ResourceGangConfigsVO = data.ResourceGangConfigs.MapToDTO();
                }
            }
            return RACRvo;

        }

        public static ResourceAllocationConfigRule MapToEntity(this ResourceAllocationConfigRuleVO vo)
        {
            ResourceAllocationConfigRule RACRentity = new ResourceAllocationConfigRule();
            if (vo != null)
            {
                RACRentity.ResourceAllocationConfigRuleID = vo.ResourceAllocationConfigRuleID;
                RACRentity.PortCode = vo.PortCode;
                RACRentity.PilotCapacity = vo.PilotCapacity;
                RACRentity.TotalTugs = vo.TotalTugs;
                RACRentity.EffectedFrom = DateTime.Parse(Convert.ToDateTime(vo.EffectedFrom, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                RACRentity.RecordStatus = vo.RecordStatus;
                RACRentity.CreatedBy = vo.CreatedBy;
                RACRentity.CreatedDate = vo.CreatedDate;
                RACRentity.ModifiedBy = vo.ModifiedBy;
                RACRentity.ModifiedDate = vo.ModifiedDate;

                RACRentity.ResourceGangConfigs = vo.ResourceGangConfigsVO.MapToEntity();
            }
            return RACRentity;
        }

        public static List<ResourceAllocationConfigRuleVO> MapToDTO(this IEnumerable<ResourceAllocationConfigRule> RACRentities)
        {
            var RACRvoList = new List<ResourceAllocationConfigRuleVO>();
            if (RACRentities != null)
            {
                foreach (var item in RACRentities)
                {
                    RACRvoList.Add(item.MapToDTO());
                }
            }
            return RACRvoList;
        }

        public static List<ResourceAllocationConfigRule> MapToEntity(this IEnumerable<ResourceAllocationConfigRuleVO> RACRvolist)
        {
            var RACRentitiesList = new List<ResourceAllocationConfigRule>();
            if (RACRvolist != null)
            {
                foreach (var item in RACRvolist)
                {
                    RACRentitiesList.Add(item.MapToEntity());
                }
            }
            return RACRentitiesList;
        }
    }
}
