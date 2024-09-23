using Core.Repository;
using System;
namespace IPMS.Domain.Models
{

    public partial class TerminalOperatorBerth : EntityBase
    {
        public int TerminalOperatorBerthID { get; set; }

        public int TerminalOperatorID { get; set; }

        public string PortCode { get; set; }

        public string QuayCode { get; set; }

        public string BerthCode { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public virtual Berth Berth { get; set; }

        public virtual TerminalOperator TerminalOperator { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
