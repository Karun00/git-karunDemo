using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceCalendarVO
    {
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public int ShiftID { get; set; }
        public string AllocSlot { get; set; }
        public DateTime AllocationDate { get; set; }
        public string AttendanceStatus { get; set; }
        public string ResourceWorkStatus { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int EmployeeID { get; set; }
        public List<ResourceCalendarSlotDetailsVO> ResourceCalendarSlotDetails  { get; set; }
        public string VCN { get; set; }
        public string Designation { get; set; }

        public int CraftID { get; set; }
        public string CraftName { get; set; }
    }
}
