using System;
using System.Collections.Generic;
using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class RosterGroup:EntityBase
    {
        public int RosterGroupID { get; set; }
        public int RosterID { get; set; }
        public int ResourceGroupID { get; set; }
        public Nullable<int> Mon { get; set; }
        public Nullable<int> Tue { get; set; }
        public Nullable<int> Wed { get; set; }
        public Nullable<int> Thu { get; set; }
        public Nullable<int> Fri { get; set; }
        public Nullable<int> Sat { get; set; }
        public Nullable<int> Sun { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ResourceGroup ResourceGroup { get; set; }
        public virtual Roster Roster { get; set; }
        public virtual User User { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual User User1 { get; set; }
        public virtual Shift Shift1 { get; set; }
        public virtual Shift Shift2 { get; set; }
        public virtual Shift Shift3 { get; set; }
        public virtual Shift Shift4 { get; set; }
        public virtual Shift Shift5 { get; set; }
        public virtual Shift Shift6 { get; set; }
    }
}
