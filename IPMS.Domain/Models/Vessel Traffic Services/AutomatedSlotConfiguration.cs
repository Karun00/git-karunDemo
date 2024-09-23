using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models    
{
   public partial class AutomatedSlotConfiguration:EntityBase
    {
        public AutomatedSlotConfiguration()
        {
            this.SlotPriorityConfigurations = new List<SlotPriorityConfiguration>();
        }

        public int SlotCofiguratinid { get; set; }
        public System.DateTime EffectiveFrm { get; set; }
        public int Duration { get; set; }
        public int NoofSlots { get; set; }
        public int ExtendableSlots { get; set; }
        public string OperationalPeriod { get; set; }
        public string PortCode { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModfiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string RecordStatus { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  Port Port { get; set; }
        public  ICollection<SlotPriorityConfiguration> SlotPriorityConfigurations { get; set; }
    }
}



