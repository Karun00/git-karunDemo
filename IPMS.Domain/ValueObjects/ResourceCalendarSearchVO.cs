using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceCalendarSearchVO
    {
        public int ShiftID { get; set; }
        public string OperationType { get; set; }
        public DateTime AllocationDate { get; set; }
        public string ServiceReferenceType { get; set; }
    }
}
