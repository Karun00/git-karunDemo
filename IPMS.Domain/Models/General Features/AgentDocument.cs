using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class AgentDocument : EntityBase
    {
            [DataMember]
        public int AgentID { get; set; }
            [DataMember]
        public int DocumentID { get; set; }
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
            [DataMember]
        public virtual Agent Agent { get; set; }
            [DataMember]
        public virtual Document Document { get; set; }
    }
}
