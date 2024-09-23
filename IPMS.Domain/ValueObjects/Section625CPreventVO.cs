using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625CPreventVO
    {
        [DataMember]
        public int Section625CPreventID { get; set; }
        [DataMember]
        public int Section625CID { get; set; }
        [DataMember]
        public string PreventStep { get; set; }
        [DataMember]
        public System.DateTime TargetDateTime { get; set; }
        [DataMember]
        public System.DateTime ActionBy { get; set; }
        [DataMember]
        public System.DateTime CompletedDate { get; set; }
    }
}
