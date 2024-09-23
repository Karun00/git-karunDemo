using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class SlotPriorityConfigurationMapToExtension
    {
        public static SlotPriorityConfigurationVO MapToDTO(this SlotPriorityConfiguration data)
        {
            SlotPriorityConfigurationVO slotpriorityconfigvo = new SlotPriorityConfigurationVO();
            slotpriorityconfigvo.SlotCofiguratinid = data.SlotCofiguratinid;
            slotpriorityconfigvo.VesselType = data.VesselType;
            slotpriorityconfigvo.Priority = data.Priority;
            slotpriorityconfigvo.NoofVessels = data.NoofVessels;
            slotpriorityconfigvo.RecordStatus = data.RecordStatus;
            slotpriorityconfigvo.VesselTypeName = data.SubCategory != null ? data.SubCategory.SubCatName : null;

            return slotpriorityconfigvo;
        }
        public static SlotPriorityConfiguration MapToEntity(this SlotPriorityConfigurationVO vo)
        {
            SlotPriorityConfiguration slotpriorityconfig = new SlotPriorityConfiguration();
            slotpriorityconfig.SlotCofiguratinid = vo.SlotCofiguratinid;
            slotpriorityconfig.VesselType = vo.VesselType;
            slotpriorityconfig.Priority = vo.Priority;
            slotpriorityconfig.NoofVessels = vo.NoofVessels;
            slotpriorityconfig.RecordStatus = vo.RecordStatus;
            return slotpriorityconfig;
        }

        public static List<SlotPriorityConfigurationVO> MapToDTO(this IEnumerable<SlotPriorityConfiguration> SlotPriorityConfigurations)
        {
            var slotpriorityconfigvoList = new List<SlotPriorityConfigurationVO>();
            foreach (var item in SlotPriorityConfigurations)
            {
                slotpriorityconfigvoList.Add(item.MapToDTO());
            }
            return slotpriorityconfigvoList;
        }

        //public static List<SlotPriorityConfigurationVO> MapToDTOForPriority(this IEnumerable<SlotPriorityConfiguration> SlotPriorityConfigurations)
        //{

        // List<int> numberList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //    for (int i = 1; i <= 10; i++)
        //    {


        //        slotpriorityconfigvoList.Add(SlotPriorityConfigurations.);


        //    }

        //    //foreach (var item in SlotPriorityConfigurations)
        //    //{
        //    //    slotpriorityconfigvoList.Add(item.MapToDTO());
        //    //}
        //    return slotpriorityconfigvoList;
        //}

        public static List<SlotPriorityConfiguration> MapToEntity(this IEnumerable<SlotPriorityConfigurationVO> SlotPriorityConfigurationVO)
        {
            var slotpriorityconfigList = new List<SlotPriorityConfiguration>();
            foreach (var item in SlotPriorityConfigurationVO)
            {
                slotpriorityconfigList.Add(item.MapToEntity());
            }
            return slotpriorityconfigList;
        }
    }
}
