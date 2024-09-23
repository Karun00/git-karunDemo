using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderScheduleLineSXVO
    {
        [DataMember]
        public string ITMNUMBER { get; set; }
        [DataMember]
        public string SCHEDLINE { get; set; }
        [DataMember]
        public string UPDATEFLAG { get; set; }
        [DataMember]
        public string REQDATE { get; set; }
        [DataMember]
        public string DATETYPE { get; set; }
        [DataMember]
        public string REQTIME { get; set; }
        [DataMember]
        public string REQQTY { get; set; }
        [DataMember]
        public string REQDLVBL { get; set; }
        [DataMember]
        public string SCHEDTYPE { get; set; }
        [DataMember]
        public string TPDATE { get; set; }
        [DataMember]
        public string MSDATE { get; set; }
        [DataMember]
        public string LOADDATE { get; set; }
        [DataMember]
        public string GIDATE { get; set; }
        [DataMember]
        public string MSTIME { get; set; }
        [DataMember]
        public string LOADTIME { get; set; }
        [DataMember]
        public string GITIME { get; set; }
        [DataMember]
        public string REFOBJTYPE { get; set; }
        [DataMember]
        public string REFOBJKEY { get; set; }
        [DataMember]
        public string DLVDATE { get; set; }
        [DataMember]
        public string DLVTIME { get; set; }
        [DataMember]
        public string RELTYPE { get; set; }
        [DataMember]
        public string PLANSCHEDTYPE { get; set; }
      
    }
}
