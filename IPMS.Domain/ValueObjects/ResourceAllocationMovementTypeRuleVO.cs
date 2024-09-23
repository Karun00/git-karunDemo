using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationMovementTypeRuleVO
    {
        public int ResourceAllocationMovementTypeRuleID { get; set; }
        public int ResourceAllocationConfigRuleID { get; set; }
        public string PortCode { get; set; }
        public string MovementType { get; set; }
        public int ServiceTypeID { get; set; }
    }
}
