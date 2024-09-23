using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    public class AutomatedSlotConfigurationReferenceDataVO
    {    
            public ICollection<SlotPriorityConfigurationVO> SlotpriorityDetails { get; set; }
            public ICollection<SubCategoryVO> VesselType { get; set; }
            public ICollection<SubCategoryCodeNameVO> PrioprtySeqList { get; set; }
        
           
    }
}
