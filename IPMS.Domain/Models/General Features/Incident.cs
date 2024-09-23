using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Incident : EntityBase
    {
        public Incident()
        {
            this.IncidentDocuments = new List<IncidentDocument>();
            this.IncidentNatures = new List<IncidentNature>();
        }
        [DataMember]
        public int IncidentID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string IncidentLocation { get; set; }
        [DataMember]
        public string IncidentDescription { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public  ICollection<IncidentDocument> IncidentDocuments { get; set; }
        [DataMember]
        public  ICollection<IncidentNature> IncidentNatures { get; set; }
    }
}
