using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models

{
    [DataContract]
    public partial class ArrivalAgent : EntityBase
    {
        [DataMember]
        public int ArrivalAgentID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public string IsPrimary { get; set; }
        [DataMember]
        public virtual Agent Agent { get; set; }
        [DataMember]
        public virtual ArrivalNotification ArrivalNotification { get; set; }
    }
}
