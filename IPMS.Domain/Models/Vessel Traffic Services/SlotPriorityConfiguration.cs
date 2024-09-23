using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class SlotPriorityConfiguration:EntityBase
    {
        public int SlotCofiguratinid { get; set; }
        public string VesselType { get; set; }
        public int Priority { get; set; }
        public int NoofVessels { get; set; }
        public string RecordStatus { get; set; }
        public virtual AutomatedSlotConfiguration AutomatedSlotConfiguration { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
