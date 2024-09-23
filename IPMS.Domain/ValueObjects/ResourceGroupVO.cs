using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ResourceGroupVO
    {
        [DataMember]
        public int ResourceGroupID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ResourceGroupName { get; set; }
        [DataMember]
        public string Position { get; set; }
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
        public virtual PortVO Port { get; set; }
        [DataMember]
        public virtual List<ResourceEmployeeGroupVO> ResourceEmployeeGroups { get; set; }
        [DataMember]
        public virtual SubCategoryVO SubCategory { get; set; }
        [DataMember]
        public virtual ICollection<ResourceRosterVO> ResourceRosters { get; set; }
        [DataMember]
        public string ResourceGroupCode { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public int ResourceEmployeeGroupID { get; set; }
        [DataMember]
        public string DesignationCode { get; set; }
        List<int> EmpList { get; set; }


    }
}
