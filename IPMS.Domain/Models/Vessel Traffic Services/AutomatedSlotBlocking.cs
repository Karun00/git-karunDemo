using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    [DataContract]
    public partial class AutomatedSlotBlocking : EntityBase
    {
        [DataMember]
        public int AutomatedSlotBlockingId { get; set; }
        [DataMember]
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public System.DateTime ToDate { get; set; }
        [DataMember]
        public string SlotFrom { get; set; }
        [DataMember]
        public string SlotTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Reason { get; set; }        
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }        
        [DataMember]
        public int TotalSlots { get; set; }
        [DataMember]
        public string Other { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public User User1 { get; set; }
        [DataMember]
        public Port Port { get; set; }
        [DataMember]
        public SubCategory SubCategory { get; set; }
    }
}








