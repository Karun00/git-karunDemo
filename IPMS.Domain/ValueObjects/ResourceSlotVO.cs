using System;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceSlotVO
    {
        public int SlotNumber { get; set; }
        public string ResourceName { get; set; }
        public Nullable<int> ResourceID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string Status { get; set; }
        public bool IsCraft { get; set; }
        public int ServiceReferenceID { get; set; }
        public int ResourceAllocationID { get; set; }
        public Nullable<int> CraftID { get; set; }
        public string CraftName { get; set; }
        public int ShiftID { get; set; }
        public string TugResourceName { get; set; }
        public string SlotPeriod { get; set; }
        public string TaskStatus { get; set; }
        public bool IsChanged { get; set; }
        public DateTime AllocationDate { get; set; }
        public string SlotDate { get; set; }
        public string SlotHeader { get; set; }
        public string ServiceTypeName { get; set; }
        
        public string Slot {get;set;}
        public string CraftStatus { get; set; }
        public int Duration { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StartMinutes { get; set; }

        public int EndMinutes { get; set; }
        
    }
}
