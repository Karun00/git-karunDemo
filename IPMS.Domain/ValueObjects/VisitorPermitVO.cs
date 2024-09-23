using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class VisitorPermitVO
    {
        public int VisitorPermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string CompanyName { get; set; }
        public string Reason { get; set; }
        public string AuthorizedPersonName { get; set; }
        public string Division { get; set; }
        public string PositionHeld { get; set; }
        public string EscortName { get; set; }
        public string TelephoneNo { get; set; }
        public string PermitNo { get; set; }
        public string PermitCode { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
