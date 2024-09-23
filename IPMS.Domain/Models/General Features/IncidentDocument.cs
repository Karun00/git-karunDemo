using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class IncidentDocument : EntityBase
    {
        [DataMember]
        public int IncidentDocument1 { get; set; }
        [DataMember]
        public int IncidentID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string FileName { get; set; }
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
        public virtual Incident Incident { get; set; }
    }
}
