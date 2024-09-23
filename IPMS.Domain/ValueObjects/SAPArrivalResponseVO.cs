using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPArrivalResponseVO
    {
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public int ZZARRNO { get; set; }
        [DataMember]
        public string VCN { get; set; }

    }
}
