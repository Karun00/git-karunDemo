using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class WorkflowTaskRole : EntityBase
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int Step { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public virtual Role Role { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual Entity Entity { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
    }
}
