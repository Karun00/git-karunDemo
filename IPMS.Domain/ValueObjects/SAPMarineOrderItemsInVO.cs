using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderItemsInVO
    {
        [DataMember]
        public string ITMNUMBER {get; set;}
	    [DataMember]
        public string MATERIAL {get; set;}
    }
}
