using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class AuditTrailConfig : EntityBase
    {
        public AuditTrailConfig()
        {
            this.AuditTrails = new List<AuditTrail>();
        }

        [DataMember]
        public int AuditTrailConfigID { get; set; }

        [DataMember]
        public string ControlerName { get; set; }

        [DataMember]
        public string ActionName { get; set; }

        [DataMember]
        public string UserFriendlyDescription { get; set; }

        [DataMember]
        public string IsAuditTrailRequired { get; set; }

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
        public  ICollection<AuditTrail> AuditTrails { get; set; }

        [DataMember]
        public  User User { get; set; }

        [DataMember]
        public string IsSecurityAuditTrail { get; set; }
    }
}
