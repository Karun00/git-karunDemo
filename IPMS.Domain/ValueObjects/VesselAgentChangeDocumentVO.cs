using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselAgentChangeDocumentVO
    {
        [DataMember]
        public int VesselAgentChangeID { get; set; }

        [DataMember]
        public int DocumentID { get; set; }

        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public System.DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
    }
}
