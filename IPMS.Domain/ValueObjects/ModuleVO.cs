using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class ModuleVO
    {
        public int ModuleID { get; set; }       
        public Nullable<int> ParentModuleID { get; set; }       
        public string ModuleName { get; set; }      
        public Nullable<int> OrderNo { get; set; }       
        public string RecordStatus { get; set; }      
        public int CreatedBy { get; set; }      
        public Nullable<System.DateTime> CreatedDate { get; set; }     
        public Nullable<int> ModifiedBy { get; set; }      
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public ICollection<Module> Module { get; set; }
    }
}
