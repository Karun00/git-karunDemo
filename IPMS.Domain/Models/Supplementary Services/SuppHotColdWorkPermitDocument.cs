using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppHotColdWorkPermitDocument : EntityBase
    {
        [DataMember]
        public int SuppHotColdWorkPermitDocumentID { get; set; }
        [DataMember]
        public int SuppHotColdWorkPermitID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual Document Document { get; set; }
        [DataMember]
        public virtual SuppHotColdWorkPermit SuppHotColdWorkPermit { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
