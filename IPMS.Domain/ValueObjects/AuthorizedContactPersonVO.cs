using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class AuthorizedContactPersonVO
    {
        [DataMember]
        public int AuthorizedContactPersonID { get; set; }
        [DataMember]
        public string AuthorizedContactPersonType { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SurName { get; set; }
        [DataMember]
        public string IdentityNo { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string CellularNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
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
    }
}
