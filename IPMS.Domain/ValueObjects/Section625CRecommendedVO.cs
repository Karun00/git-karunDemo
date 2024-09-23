using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625CRecommendedVO
    {
        [DataMember]
        public int Section625CRecommendedID { get; set; }
        [DataMember]
        public int Section625CID { get; set; }
        [DataMember]
        public string RecommendedStep { get; set; }
        [DataMember]
        public System.DateTime TargetDateTime { get; set; }
        [DataMember]
        public System.DateTime ActionBy { get; set; }
        [DataMember]
        public System.DateTime CompletedDate { get; set; }
    }
}
