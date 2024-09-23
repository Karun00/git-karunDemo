using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppDryDockDocument : EntityBase
    {
        [DataMember]
        public int SuppDryDockDocumentID { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
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
        public virtual Document Document { get; set; }
        [DataMember]
        public virtual SuppDryDock SuppDryDock { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
