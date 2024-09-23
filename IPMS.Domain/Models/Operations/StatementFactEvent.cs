using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class StatementFactEvent : EntityBase
    {
        public int StatementFactEventID { get; set; }
        public int StatementFactID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string DelayType { get; set; }
        public Nullable<System.DateTime> StartOperational { get; set; }
        public Nullable<System.DateTime> EndOperational { get; set; } 
        public Nullable<decimal> Duration { get; set; }
        public string Remarks { get; set; }
        public virtual StatementFact StatementFact { get; set; }
        public virtual User User { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User1 { get; set; }
    }
}