using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPVesselCreateVO
    {
        
        [DataMember]
        public string ICALLSIGN { get; set; }
        [DataMember]
        public string ICITY { get; set; }
        [DataMember]
        public string ICOUNTRY { get; set; }
        [DataMember]
        public System.DateTime IDATE { get; set; }
        [DataMember]
        public string IIMO { get; set; }
        [DataMember]
        public string ILENGTH { get; set; }
        [DataMember]
        public string IPOSTAL { get; set; }
        [DataMember]
        public string ITONNAGE { get; set; }
        [DataMember]
        public string IVESIND { get; set; }
        [DataMember]
        public string IVESNAME { get; set; }
        [DataMember]
        public string IVESTYPE { get; set; }
        [DataMember]
        public string ENUMBER { get; set; }
        [DataMember]
        public int SUBRC { get; set; }

        [DataMember]
        public string ERRMSG { get; set; }
        [DataMember]
        public int SAPPOSTINGID { get; set; }
        [DataMember]
        public string MESSAGETYPE { get; set; }
        [DataMember]
        public int VesselID { get; set; }

        [DataMember]
        public List<SAPVesselCreateMESSTABVO> MESSTAB { get; set; }

        [DataMember]
        public string VKORG { get; set; }

    }
}
