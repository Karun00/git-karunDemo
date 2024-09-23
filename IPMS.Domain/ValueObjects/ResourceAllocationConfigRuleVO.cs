using System;
using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationConfigRuleVO
    {
        public int ResourceAllocationConfigRuleID { get; set; }
        public string PortCode { get; set; }
        public string PilotCapacity { get; set; }
        public Nullable<int> TotalTugs { get; set; }
        public String EffectedFrom { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public List<int> arrivalservicetype { get; set; }
        public List<int> shiftingservicetype { get; set; }
        public List<int> sailingservicetype { get; set; }
        public List<int> warpingservicetype { get; set; }
        public ICollection<ResourceGangConfigVO> ResourceGangConfigsVO { get; set; }
    }
}
