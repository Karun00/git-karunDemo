using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderOrderItemsInXVO
    {
        [DataMember]
        public string ITMNUMBER { get; set; }
        [DataMember]
        public string UPDATEFLAG { get; set; }
        [DataMember]
        public string MATERIAL { get; set; }

    }
}
