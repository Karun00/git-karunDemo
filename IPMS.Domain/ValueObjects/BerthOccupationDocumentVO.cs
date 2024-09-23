using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class BerthOccupationDocumentVO
    {
        [DataMember]
        public int BerthOccupationDocumentID { get; set; }
        [DataMember]
        public int DredgingOperationID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string FileName { get; set; }
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
