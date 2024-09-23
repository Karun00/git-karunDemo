using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SuppFloatingCraneVO
    {
        [DataMember]
        public int SuppFloatingCraneID { get; set; }
        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public decimal MassMT { get; set; }
        [DataMember]
        public long Quantity { get; set; }
        [DataMember]
        public string Description { get; set; }
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
    }
}
