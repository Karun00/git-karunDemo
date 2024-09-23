using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPVesselCreateMESSTABVO
    {
        [DataMember]
        public string TCODE {get; set;}
        [DataMember]
        public string DYNAME {get; set;}
        [DataMember]
        public string DYNUMB {get; set;}
        [DataMember]
        public string MSGTYP {get; set;}
        [DataMember]
        public string MSGSPRA {get; set;} 
        [DataMember]
        public string MSGID {get; set;}
        [DataMember]
        public string MSGNR {get; set;} 
        [DataMember]
        public string MSGV2 {get; set;}
        [DataMember]
        public string MSGV3 {get; set;}
        [DataMember]
        public string MSGV4 {get; set;}
        [DataMember]
        public string ENV {get; set;}
        [DataMember]
        public string FLDNAME {get; set;}
        [DataMember]
        public string MSGV1 { get; set; }



    }
}
