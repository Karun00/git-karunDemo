using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for TerminalOperator CargoHandling
    /// </summary>
    public class TerminalOperatorCargoHandlingVO
    {        
        public int TerminalOperatorID { get; set; }
        public string CargoTypeCode { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
