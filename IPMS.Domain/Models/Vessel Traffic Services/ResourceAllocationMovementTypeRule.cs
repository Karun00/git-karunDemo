using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    public partial class ResourceAllocationMovementTypeRule:EntityBase
    {
        public int ResourceAllocationMovementTypeRuleID { get; set; }
        public int ResourceAllocationConfigRuleID { get; set; }
        public string PortCode { get; set; }
        public string MovementType { get; set; }
        public int ServiceTypeID { get; set; }
        public virtual Port Port { get; set; }
        public virtual ResourceAllocationConfigRule ResourceAllocationConfigRule { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
