using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceCalendarSlotDetailsVO
    {
        public string VCN { get; set; }
        public string AttendanceStatus { get; set; }
        public int SlotNumber { get; set; }
        public string SlotPeriod { get; set; }
        public string SlotText { get; set; }
        public string MovementType { get; set; }
    }
}
