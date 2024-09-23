using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class WorkFlowTaskVO
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string EntityName { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
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
        public int Step { get; set; }
        [DataMember]
        public Nullable<int> NextStep { get; set; }
        [DataMember]
        public Nullable<int> ValidityPeriod { get; set; }
        [DataMember]
        public string HasNotification { get; set; }
        [DataMember]
        public string APIUrl { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string HasRemarks { get; set; }

        //[DataMember]
        //public List<RoleVO> arrayRoles { get; set; }
        [DataMember]
        public string CommaSeperatedRoleIDs { get; set; }
        [DataMember]
        public List<string> arrayRoles { get; set; }
    }

    public class usp_GetWorkFlowTaskVO
    {        
        public int EntityID { get; set; }
        
        public string EntityName { get; set; }

        public string WorkflowTaskCode { get; set; }

        public Nullable<int> ValidityPeriod { get; set; }

        public Nullable<int> Step { get; set; }
        
        public Nullable<int> NextStep { get; set; }
        
        public string HasNotification { get; set; }

        public string APIUrl { get; set; }

        public string HasRemarks { get; set; }
        
        public string PortCode { get; set; }
        
        public string CommaSeperatedRoleIDs { get; set; }
        
        public List<string> arrayRoles { get; set; }
    }

    [DataContract]
    public class WorkFlowTaskUpdateVO
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public string HasNotification { get; set; }
        [DataMember]
        public Nullable<int> ValidityPeriod { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int Step { get; set; }
        [DataMember]
        public Nullable<int> NextStep { get; set; }
        [DataMember]
        public string APIUrl { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string HasRemarks { get; set; }

        [DataMember]
        public string CommaSeperatedRoleIDs { get; set; }
        [DataMember]
        public List<string> arrayRoles { get; set; }
    }
}
