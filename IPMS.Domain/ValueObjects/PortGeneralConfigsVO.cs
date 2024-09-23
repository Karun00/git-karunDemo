using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
     [DataContract]
    public partial class PortGeneralConfigsVO
    {
        [DataMember]
        public int PortGeneralConfigID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ConfigName { get; set; }
        [DataMember]
        public string ConfigValue { get; set; }
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
        public string ConfigLabelName { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public List<GroupNames> GroupNames { get; set; }

    }

     [DataContract]
     public partial class GroupNames  
     {
         [DataMember]
         public int PortGeneralConfigID { get; set; }
         [DataMember]
         public string ConfigValue { get; set; }
         [DataMember]
         public string ConfigLabelName { get; set; }
         [DataMember]
         public string GroupName { get; set; }
         [DataMember]
         public string ConfigName { get; set; }
         [DataMember]
         public string RecordStatus { get; set; }
         [DataMember]
         public string PortCode { get; set; }
     }
}
