using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class PlannedMovementsVO
    {
        public string VesselName { get; set; }
        public string MovementType { get; set; }
        public string ArrDraft { get; set; }
        public string DepDraft { get; set; }
        public string Draft { get; set; }
        public string BerthName { get; set; }
        public string RegisteredName { get; set; }
        public string VeselType { get; set; }
        public string ReasonforvisitName { get; set; }
        public Nullable<decimal> GRT { get; set; }
        public Nullable<decimal> LOA { get; set; }
        public Nullable<DateTime> MovementDateTime { get; set; }
        public string Status { get; set; }
        public Nullable<DateTime> ScheduledTime  { get; set; }
    }
}
