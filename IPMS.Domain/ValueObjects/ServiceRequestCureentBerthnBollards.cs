
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ServiceRequestCureentBerthnBollards 
    {        
        [DataMember]
        public string CurrentBerth { get; set; } 
        [DataMember]
        public string CurrentFromBollardName { get; set; }
        [DataMember]
        public string CurrentToBollardName { get; set; }
        [DataMember]
        public string CurrentBerthCode { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ATB { get; set; }
        [DataMember]
        public string AllocatedBerth { get; set; }
        [DataMember]
        public string AllocatedFromBollardName { get; set; }
        [DataMember]
        public string AllocatedToBollardName { get; set; }

    }
}
