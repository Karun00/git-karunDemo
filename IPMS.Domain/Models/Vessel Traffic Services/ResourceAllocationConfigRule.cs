using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    public partial class ResourceAllocationConfigRule:EntityBase
    {
        public ResourceAllocationConfigRule()
        {
            this.ResourceAllocationMovementTypeRules = new List<ResourceAllocationMovementTypeRule>();
            this.ResourceGangConfigs = new List<ResourceGangConfig>();
        }

        public int ResourceAllocationConfigRuleID { get; set; }
        public string PortCode { get; set; }
        public string PilotCapacity { get; set; }
        public Nullable<int> TotalTugs { get; set; }
        public Nullable<System.DateTime> EffectedFrom { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  Port Port { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  ICollection<ResourceAllocationMovementTypeRule> ResourceAllocationMovementTypeRules { get; set; }
        public  ICollection<ResourceGangConfig> ResourceGangConfigs { get; set; }
    }
}
