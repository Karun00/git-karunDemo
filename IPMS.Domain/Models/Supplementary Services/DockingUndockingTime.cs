using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DockingUndockingTime : EntityBase
    {
        [DataMember]
        public int DockingUndockingTimeID { get; set; }
        [DataMember]
        public string Chamber { get; set; }
        [DataMember]
        public System.DateTime VesselEnteredDockAt { get; set; }
        [DataMember]
        public System.DateTime OnBlocks { get; set; }
        [DataMember]
        public System.DateTime DryDockAt { get; set; }
        [DataMember]
        public System.DateTime FinishedWithDockAt { get; set; }
        [DataMember]
        public System.DateTime OffBlocks { get; set; }
        [DataMember]
        public System.DateTime VesselLeftDockAt { get; set; }
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
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
