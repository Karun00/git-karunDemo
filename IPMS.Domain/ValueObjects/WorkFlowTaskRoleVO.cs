using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
     [DataContract]
    public class WorkFlowTaskRoleVO
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public int Step { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public List<WorkFlowTaskVO> WorkFlowTaskVO { get; set; }
        [DataMember]
        public string PortCode { get; set; }
         
    }
}
