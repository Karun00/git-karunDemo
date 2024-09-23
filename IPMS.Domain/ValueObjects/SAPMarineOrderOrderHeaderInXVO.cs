using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderOrderHeaderInXVO
    {
        [DataMember]
        public string UPDATEFLAG { get; set; }

    }
}
