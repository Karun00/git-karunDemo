using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    //class PortRegistryVO
    //{
    //}

    [DataContract]
    public class PortRegistryVO
    {
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string IsSA { get; set; }
        [DataMember]
        public string IsTNPA { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public string InternationalPortCode { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string PostalCode { get; set; }

    }

}
