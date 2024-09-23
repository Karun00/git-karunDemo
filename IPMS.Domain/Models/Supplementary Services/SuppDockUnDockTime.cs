using System;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppDockUnDockTime : EntityBase
    {
        [DataMember]
        public int SuppDockUnDockTimeID { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public string Chamber { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EnteredDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OnBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DryDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FinishedDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OffBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LeftDockDateTime { get; set; }
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
      //  [DataMember]
       // public virtual SuppDryDock SuppDryDock { get; set; }
    }
}
