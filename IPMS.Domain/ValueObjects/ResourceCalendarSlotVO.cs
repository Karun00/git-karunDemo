using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceCalendarSlotVO
    {
        public string VCN { get; set; }
        public string AllocSlot { get; set; }
        public int ResourceID { get; set; }
        public string OperationType { get; set; }
        public DateTime AllocationDate { get; set; }
        public string MovementType { get; set; }
        public string TaskStatus { get; set; }
        public int CraftID { get; set; }
        public string CraftName { get; set; }
    }
}
