using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class IncidentNature : EntityBase
    {
        [DataMember]
        public int IncidentNatureID { get; set; }
        [DataMember]
        public int IncidentID { get; set; }
        [DataMember]
        public string IncidentNature1 { get; set; }
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
        public virtual Incident Incident { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
