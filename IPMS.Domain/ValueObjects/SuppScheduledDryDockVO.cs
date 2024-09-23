using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class SuppScheduledDryDockVO
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string title { get; set; }
        public string ScheduleStatus { get; set; }
        public string VesselName { get; set; }
        public DateTime ScheduleFromDate { get; set; }
        public DateTime ScheduleToDate { get; set; }
        public string DockBerthCode { get; set; }
        public DateTime ReqFromDate { get; set; }
        public DateTime ReqToDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }

    }
}
