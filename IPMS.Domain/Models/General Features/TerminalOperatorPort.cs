using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class TerminalOperatorPort : EntityBase
    {
        [DataMember]
        public int TerminalOperatorPortID { get; set; }

        [DataMember]
        public int TerminalOperatorID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
       
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual TerminalOperator TerminalOperator { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual User User2 { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }

    }
}
