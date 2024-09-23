using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class MarpolVO
    {
        [DataMember]
        public string ClassCode { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string MarpolCode { get; set; }
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
        [DataMember]
        public string MarpolName { get; set; }
        [DataMember]
        public ICollection<SubCategory> MarpolTypes { get; set; }
    }

    public class MarpolGroupVO
    {
        [DataMember]
        public string MarpolCode { get; set; }
        [DataMember]
        public string MarpolName { get; set; }
        [DataMember]
        public List<MarpolVO> MarpolDetails { get; set; }
    }

}
