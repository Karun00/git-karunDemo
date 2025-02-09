using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class UserRole : EntityBase
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
      
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public virtual Role Role { get; set; }
        [DataMember]
        public virtual User User { get; set; }
    }
}
