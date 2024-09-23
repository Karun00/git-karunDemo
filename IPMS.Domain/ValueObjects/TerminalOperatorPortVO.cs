using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for LicenseRequest Port
    /// </summary>
    public partial class TerminalOperatorPortVO
    {

        public int TerminalOperatorPortID { get; set; }

        public int TerminalOperatorID { get; set; }

        public string PortCode { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

      

    }
}
