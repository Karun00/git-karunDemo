using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ServiceTypeVO
    {
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string ServiceTypeName { get; set; }
        [DataMember]
        public string ServiceTypeCode { get; set; }
        [DataMember]
        public Nullable<bool> IsCraft { get; set; }
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
        public string IsServiceType { get; set; }
        [DataMember]
        public string ServiceUOM { get; set; }

        [DataMember]
        public List<ServiceTypeDesignationVO> ServiceTypeDesignations { get; set; }
    }
}
