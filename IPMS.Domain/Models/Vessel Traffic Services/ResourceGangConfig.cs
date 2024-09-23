using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    public partial class ResourceGangConfig:EntityBase
    {
        public int ResourceGangConfigID { get; set; }
        public int ResourceAllocationConfigRuleID { get; set; }
        public Nullable<decimal> FromMeter { get; set; }
        public Nullable<decimal> ToMeter { get; set; }
        public Nullable<int> NoOfGangs { get; set; }
        public virtual ResourceAllocationConfigRule ResourceAllocationConfigRule { get; set; }
    }
}
