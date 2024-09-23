using System.Collections.Generic;
using System;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationSlotVO
    {
        public int ResourceAllocationID { get; set; }
        public string ServiceReferenceType { get; set; }
        public int ServiceReferenceID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string AllocSlot { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public int CraftId { get; set; }
        public string CraftName { get; set; }
        public string TaskStatus { get; set; }
        public bool IsServiceTypeDeleted { get; set; }
        public string AllocationDate { get; set; }
        public string BerthCode { get; set; }
        public string BerthName { get; set; }
        public long Quantity { get; set; }
        public bool IsCraft { get; set; }
        public int ResourceID { get; set; }
        public Nullable<System.DateTime> AcknowledgeDate { get; set; }

        public List<ResourceSlotVO> ResourceSlots { get; set; }

        public string AnyDangerousGoodsonBoard { get; set; }

        //-- Added by sandeep on 30-03-2015
        public string IsConfirm { get; set; }
        //-- end
    }
}
