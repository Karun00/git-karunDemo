using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ServiceTypeDesignationVO
    {
        [DataMember]
        public int ServiceTypeDesignationID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ServiceTypeName { get; set; }
        [DataMember]
        public string ServiceTypeCode { get; set; }
        [DataMember]
        public string DesignationName { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string DesignationCode { get; set; }
        [DataMember]
        public Nullable<bool> IsCraft { get; set; }
        [DataMember]
        public string CraftType { get; set; }
        [DataMember]
        public string CraftName { get; set; }
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
