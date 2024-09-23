using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationConfigVO
    {
        public string ServiceTypeCode { get; set; }

        public int TotalTugs { get; set; }

        public decimal ToMeter { get; set; }

        public decimal FromMeter { get; set; }

        public int NoOfGangs { get; set; }

        public string PilotCapacity { get; set; }

        public string CraftType { get; set; }

    }
}
