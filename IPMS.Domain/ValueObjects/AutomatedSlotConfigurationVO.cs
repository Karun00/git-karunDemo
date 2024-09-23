using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class AutomatedSlotConfigurationVO
    {
        public int SlotCofiguratinid { get; set; }
        public String EffectiveFrm { get; set; }
        public int Duration { get; set; }
        public int NoofSlots { get; set; }
        public int ExtendableSlots { get; set; }
        public string OperationalPeriod { get; set; }
        public System.DateTime? OperationalPeriod1 { get; set; }        
        public string PortCode { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModfiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public List<SlotPriorityConfigurationVO> SlotPriorityConfigurations { get; set; }

        public string ExtendYn { get; set; }
    }
}
