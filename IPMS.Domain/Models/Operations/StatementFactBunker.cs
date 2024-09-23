using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class StatementFactBunker : EntityBase
    {
        public int StatementFactBunkerID { get; set; }
        public int StatementFactID { get; set; }
        public decimal ArrivalFuel { get; set; }
        public decimal ArrivalDiesel { get; set; }
        public decimal SailingFuel { get; set; }
        public decimal SailingDiesel { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual StatementFact StatementFact { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
