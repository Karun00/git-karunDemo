using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    public partial class RosterDtl : EntityBase
    {
        public int ResourceGroupID { get; set; }
        public int ShiftID { get; set; }
        public System.DateTime RosterDate { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ResourceGroup ResourceGroup { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Shift Shift { get; set; }

        // -- Added by sandeep on 08-01-2015
        public Nullable<int> MainShiftID { get; set; }
        public virtual Shift Shift1 { get; set; }
        // -- end
    }
}
