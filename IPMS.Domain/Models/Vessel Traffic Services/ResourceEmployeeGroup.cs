using System;
using System.Collections.Generic;
using Core.Repository;
namespace IPMS.Domain.Models
{
    public partial class ResourceEmployeeGroup : EntityBase
    {
        public int ResourceEmployeeGroupID { get; set; }
        public int ResourceGroupID { get; set; }
        public int EmployeeID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ResourceGroup ResourceGroup { get; set; }
    }
}
