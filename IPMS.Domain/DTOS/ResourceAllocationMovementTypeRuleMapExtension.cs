using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ResourceAllocationMovementTypeRuleMapExtension
    {
        public static ResourceAllocationMovementTypeRuleVO MapToDTO(this ResourceAllocationMovementTypeRule data)
        {
            ResourceAllocationMovementTypeRuleVO RAMTRvo = new ResourceAllocationMovementTypeRuleVO();
            RAMTRvo.ResourceAllocationMovementTypeRuleID = data.ResourceAllocationMovementTypeRuleID;
            RAMTRvo.ResourceAllocationConfigRuleID = data.ResourceAllocationConfigRuleID;
            RAMTRvo.PortCode = data.PortCode;
            RAMTRvo.MovementType = data.MovementType;
            RAMTRvo.ServiceTypeID = data.ServiceTypeID;
            return RAMTRvo;

        }

        public static ResourceAllocationMovementTypeRule MapToEntity(this ResourceAllocationMovementTypeRuleVO VO)
        {
            ResourceAllocationMovementTypeRule RAMTRentity = new ResourceAllocationMovementTypeRule();
            RAMTRentity.ResourceAllocationMovementTypeRuleID = VO.ResourceAllocationMovementTypeRuleID;
            RAMTRentity.ResourceAllocationConfigRuleID = VO.ResourceAllocationConfigRuleID;
            RAMTRentity.PortCode = VO.PortCode;
            RAMTRentity.MovementType = VO.MovementType;
            RAMTRentity.ServiceTypeID = VO.ServiceTypeID;
            return RAMTRentity;
        }

        public static List<ResourceAllocationMovementTypeRuleVO> MapToDTO(this IEnumerable<ResourceAllocationMovementTypeRule> RAMTRentities)
        {
            var RAMTRvoList = new List<ResourceAllocationMovementTypeRuleVO>();
            foreach (var item in RAMTRentities)
            {
                RAMTRvoList.Add(item.MapToDTO());
            }
            return RAMTRvoList;
        }

        public static List<int> ArrivalMapToDTO(this IEnumerable<ResourceAllocationMovementTypeRule> RAMTRentities)
        {
            List<int> RAMTRvoList = new List<int>();
            if (RAMTRentities != null)
            {
                foreach (var item in RAMTRentities)
                {
                    // Srini -  ARRV , Super Category - MVTY
                    if (item.MovementType == "ARMV")
                    {
                        RAMTRvoList.Add(item.ServiceTypeID);
                    }
                }
            }
            return RAMTRvoList;
        }

        public static List<int> SailingMapToDTO(this IEnumerable<ResourceAllocationMovementTypeRule> RAMTRentities)
        {
            List<int> RAMTRvoList = new List<int>();
            if (RAMTRentities != null)
            {
                foreach (var item in RAMTRentities)
                {
                    // Srini -  SAIL  , Super Category - MVTY
                    if (item.MovementType == "SGMV")
                    {
                        RAMTRvoList.Add(item.ServiceTypeID);
                    }
                }
            }
            return RAMTRvoList;
        }

        public static List<int> ShiftingMapToDTO(this IEnumerable<ResourceAllocationMovementTypeRule> RAMTRentities)
        {
            List<int> RAMTRvoList = new List<int>();
            if (RAMTRentities != null)
            {
                foreach (var item in RAMTRentities)
                {
                    // Srini -  SHIF , Super Category - MVTY
                    if (item.MovementType == "SHMV")
                    {
                        RAMTRvoList.Add(item.ServiceTypeID);
                    }
                }
            }
            return RAMTRvoList;
        }

        public static List<int> WarpingMapToDTO(this IEnumerable<ResourceAllocationMovementTypeRule> RAMTRentities)
        {
            List<int> RAMTRvoList = new List<int>();
            if (RAMTRentities != null)
            {
                foreach (var item in RAMTRentities)
                {
                    // Srini -  WARP , Super Category - MVTY
                    if (item.MovementType == "WRMV")
                    {
                        RAMTRvoList.Add(item.ServiceTypeID);
                    }
                }
            }
            return RAMTRvoList;
        }
        
        public static List<ResourceAllocationMovementTypeRule> MapToEntity(this IEnumerable<ResourceAllocationMovementTypeRuleVO> RAMTRvolist)
        {
            var RAMTRentitiesList = new List<ResourceAllocationMovementTypeRule>();
            foreach (var item in RAMTRvolist)
            {
                RAMTRentitiesList.Add(item.MapToEntity());
            }
            return RAMTRentitiesList;
        }

    }
}
