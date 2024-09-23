using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class VisitorPermit:EntityBase
    {
        public int VisitorPermitID { get; set; }
        public int PermitRequestID { get; set; }
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
        public string CompanyName { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
