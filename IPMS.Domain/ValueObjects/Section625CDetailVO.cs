using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625CDetailVO
    {
        [DataMember]
        public int Section625CDetailID { get; set; }
        [DataMember]
        public int Section625CID { get; set; }
        [DataMember]
        public string GroupCode { get; set; }
        [DataMember]
        public string DetailCode { get; set; }
    }
}
