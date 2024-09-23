using System;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DivingRequestDiver : EntityBase
    {
        [DataMember]
        public int DivingRequestDiverID { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string DiverName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeLeftSurface { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeArrivedSurface { get; set; }
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
        public virtual DivingRequest DivingRequest { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }

        [DataMember]
        public virtual string DiverType { get; set; }
    }
}
