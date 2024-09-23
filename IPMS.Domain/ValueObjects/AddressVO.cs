using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class AddressVO
    {
        [DataMember]
        public int AddressID { get; set; }
        [DataMember]
        public string AddressType { get; set; }
        [DataMember]
        public string NumberStreet { get; set; }
        [DataMember]
        public string Suburb { get; set; }
        [DataMember]
        public string TownCity { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
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
