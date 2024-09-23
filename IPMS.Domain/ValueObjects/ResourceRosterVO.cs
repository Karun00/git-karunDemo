using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ResourceRosterVO
    {
        [DataMember]
        public int ResourceRosterID { get; set; }
        [DataMember]
        public int ResourceGroupID { get; set; }
        [DataMember]
        public string Weekday { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

    }
}
