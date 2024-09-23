using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class EntityVO
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string EntityCode { get; set; }
        [DataMember]
        public int ModuleID { get; set; }
        [DataMember]
        public string EntityName { get; set; }
        [DataMember]
        public string PageUrl { get; set; }
        [DataMember]
        public int OrderNo { get; set; }
        [DataMember]
        public string HasWorkflow { get; set; }
        [DataMember]
        public string HasMenuItem { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public List<WorkFlowTaskVO> WorkFlowTaskVO { get; set; }
        [DataMember]
        public List<WorkFlowTaskRoleVO> WorkFlowTaskRoleVO { get; set; }
    }

    public class EntityModulesVO
    {
        public int EntityID { get; set; }
        public string EntityCode { get; set; }
        public int ModuleID { get; set; }
        public string EntityName { get; set; }
        public string PageUrl { get; set; }
        public int OrderNo { get; set; }
        public string HasWorkflow { get; set; }
        public string HasMenuItem { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public bool HasWorkflowStatus { get; set; }
        public bool HasMenuItemStatus { get; set; }
        public ModuleVO Module { get; set; }
        public List<String> EntityPrivileges { get; set; }

        // Needed Properites   
        public string ModuleNameList { get; set; }
        public string ModuleName { get; set; }
        public Nullable<int> ParentModuleID { get; set; }
        public string ParentModuleName { get; set; }
    }
}
