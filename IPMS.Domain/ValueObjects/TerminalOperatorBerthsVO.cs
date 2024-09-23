using IPMS.Domain.Models;
using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for TerminalOperator Berths
    /// </summary>
    public class TerminalOperatorBerthsVO
    {
        public int TerminalOperatorID { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual Berth Berth { get; set; }


        //public string BerthKey { get; set; }
        //public string BerthName { get; set; }
    }
}
