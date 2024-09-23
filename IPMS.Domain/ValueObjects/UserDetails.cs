using System;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserDetails : EntityBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long SapNo { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public long CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<long> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
