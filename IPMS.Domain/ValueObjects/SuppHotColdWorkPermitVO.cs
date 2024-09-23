using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
   public class SuppHotColdWorkPermitVO
    {
        [DataMember]
        public int SuppHotColdWorkPermitID { get; set; }
        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public string GassFreeCertificateAvailable { get; set; }
        [DataMember]
        public string GassFreeCertificateValidity { get; set; }
        [DataMember]
        public string GassFreeIssuingAuthority { get; set; }
        [DataMember]
        public int LocationID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }       
        [DataMember]
        public virtual List<SuppHotColdWorkPermitDocumentVO> SuppHotColdWorkPermitDocumentsVO { get; set; }
        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string OtherLocation { get; set; }
    }
}
