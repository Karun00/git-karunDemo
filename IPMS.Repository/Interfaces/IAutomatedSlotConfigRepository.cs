using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{   
    public interface IAutomatedSlotConfigRepository
    {
        List<AutomatedSlotConfiguration> GetAutomatedSlotConfigurationDetails(string portId);
        //List<SlotPriorityConfiguration> GetSlotPriorityConfigurationDetails(int SlotCofiguratinid);
        List<SlotPriorityConfiguration> GetSlotPriorityDetails();

        List<SubCategoryCodeNameVO> PrioprtySeqList();

    //    List<SlotPriorityConfiguration> PrioprtySeqList();
        

    }
}
