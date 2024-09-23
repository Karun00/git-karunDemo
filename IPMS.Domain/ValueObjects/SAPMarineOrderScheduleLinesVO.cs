using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderScheduleLinesVO
    {

        [DataMember]
        public string ITMNUMBER {get; set;}
        [DataMember]
        public string SCHEDLINE {get; set;}
        [DataMember]
        public string REQQTY { get; set; }
      
    }
}
