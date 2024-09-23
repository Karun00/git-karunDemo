using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SuppHotColdWorkPermitDocumentVO
    {
        [DataMember]
        public int SuppHotColdWorkPermitDocumentID { get; set; }
        [DataMember]
        public int SuppHotColdWorkPermitID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string DocumentTypeName { get; set; }
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
