using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ResourceEmployeeGroupVO
    {
        [DataMember]
        public int ResourceEmployeeGroupID { get; set; }
        [DataMember]
        public int ResourceGroupID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
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
