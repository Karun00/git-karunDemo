using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    [DataContract]
    public class PortVO
    {
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string InternationalCharacter { get; set; }
        [DataMember]
        public string GeographicLocation { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string Website { get; set; }
        [DataMember]
        public string Description { get; set; }
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


    [DataContract]
    public class PortCodeNameVO
    {
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
    }
}
