using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
     public class ResourceGangConfigVO
    {
        public int ResourceGangConfigID { get; set; }
        public int ResourceAllocationConfigRuleID { get; set; }
        public Nullable<decimal> FromMeter { get; set; }
        public Nullable<decimal> ToMeter { get; set; }
        public Nullable<int> NoOfGangs { get; set; }
        //public  ResourceAllocationConfigRuleVO ResourceAllocationConfigRule { get; set; }
    }
}
