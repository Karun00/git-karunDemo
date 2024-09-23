using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{

    public partial class TerminalOperatorCargoHandling : EntityBase
    {
        public int TerminalOperatorCargoHandlingID { get; set; }

        public int TerminalOperatorID { get; set; }
        
        public string CargoTypeCode { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual TerminalOperator TerminalOperator { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
