using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceCalendarAttendanceVO
    {
        public int UserID { get; set; }
        public int ShiftID { get; set; }
        public string UserFullName { get; set; }
        public string AttendanceStatus { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Designation { get; set; }
        public int CraftID { get; set; }
        public string CraftName { get; set; }
        public string CraftCommissionStatus { get; set; }

        //-- Added by sandeep on 31-03-2015
        public Nullable<DateTime> OutOfCommissionDate { get; set; }
        public Nullable<DateTime> BackToCommisionDate { get; set; }
        public List<ResourceSlotVO> ResourceSlotVO { get; set; }
        //-- end
        
    }
}
