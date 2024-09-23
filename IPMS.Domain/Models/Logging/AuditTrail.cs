using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class AuditTrail : EntityBase
    {
        [DataMember]
        public int AuditTrailID { get; set; }

        [DataMember]
        public int AuditTrailConfigID { get; set; }

        [DataMember]
        public string EntryORExit { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> AuditDateTime { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public string UserIPAddress { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public System.DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        
        [DataMember]
        public virtual AuditTrailConfig AuditTrailConfig { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public string UserComputerName { get; set; }

        [DataMember]
        public string Parameters { get; set; }
    }
}
