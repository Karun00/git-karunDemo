using System.Runtime.Serialization;
using Core.Repository;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppServiceResourceAlloc : EntityBase
    {
        [DataMember]
        public int SuppServiceResourceAllocID { get; set; }
        [DataMember]
        public int SuppWaterServiceID { get; set; }
        [DataMember]
        public System.DateTime AllocDate { get; set; }
        [DataMember]
        public string AllocSlot { get; set; }
        [DataMember]
        public int ResourceID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual Employee Employee { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
       
    }
}
