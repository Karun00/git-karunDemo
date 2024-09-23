using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    [DataContract]
    public class WorkFlowList_NotMappedVO
    {
        [DataMember]
        public string WorkFlowTaskCode { get; set; }
        [DataMember]
        public string Step { get; set; }
        [DataMember]
        public string NextStep { get; set; }
        [DataMember]
        public string ValidityPeriod { get; set; }
        [DataMember]
        public string HasNotification { get; set; }
        [DataMember]
        public string APIUrl { get; set; }

    }
}
