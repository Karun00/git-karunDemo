using System;
using System.Collections.Generic;
using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class ResourceRoster : EntityBase
    {
        public int ResourceRosterID { get; set; }
        public int ResourceGroupID { get; set; }
        public string Weekday { get; set; }
        public int ShiftID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ResourceGroup ResourceGroup { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual SubCategory SubCategory { get; set; }

    }
}
