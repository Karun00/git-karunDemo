using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderReturnVO
    {
        [DataMember]
        public string TYPE { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string NUMBER { get; set; }
        [DataMember]
        public string MESSAGE { get; set; }
        [DataMember]
        public string LOGNO { get; set; }
        [DataMember]
        public string LOGMSGNO { get; set; }
        [DataMember]
        public string MESSAGEV1 { get; set; }
        [DataMember]
        public string MESSAGEV2 { get; set; }
        [DataMember]
        public string MESSAGEV3 { get; set; }
        [DataMember]
        public string MESSAGEV4 { get; set; }
        [DataMember]
        public string PARAMETER { get; set; }
        [DataMember]
        public string ROW { get; set; }
        [DataMember]
        public string FIELD { get; set; }
        [DataMember]
        public string SYSTEM { get; set; }

    }
}
