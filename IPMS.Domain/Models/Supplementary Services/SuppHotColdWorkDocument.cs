using System.Runtime.Serialization;
using Core.Repository;


namespace IPMS.Domain.Models
{
    public partial class SuppHotColdWorkDocument : EntityBase
    {
        [DataMember]
        public int SuppHotColdWorkDocumentID { get; set; }
        [DataMember]
        public int SuppHotColdWorkID { get; set; }
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
        public virtual SuppHotColdWork SuppHotColdWork { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
