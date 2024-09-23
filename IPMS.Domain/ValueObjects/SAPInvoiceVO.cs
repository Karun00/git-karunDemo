using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPInvoiceVO
    {

        [DataMember]
        public string VBELN { get; set; }
        [DataMember]
        public string BILLINGDOC { get; set; }
        [DataMember]
        public int ESUBRC { get; set; }
        [DataMember]
        public string NETVALUE { get; set; }
        [DataMember]
        public string MESSAGETYPE { get; set; }
        [DataMember]
        public int SAPPOSTINGID { get; set; }
        [DataMember]
        public string ERRMSG { get; set; }
        [DataMember]
        public List<SAPInvoiceItemVO> EINVOICE { get; set; }

    }
}
