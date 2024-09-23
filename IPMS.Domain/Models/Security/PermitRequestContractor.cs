using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestContractor : EntityBase
    {
        public int PermitRequestContractorID { get; set; }
        public int PermitRequestID { get; set; }
        public string CompanyName { get; set; }
        public string ContractNo { get; set; }
        public string ContractManagerName { get; set; }
        public Nullable<int> ContractDuration { get; set; }
        public string ServiceCompanyName { get; set; }
        public string ResponsibleManager { get; set; }
        public string ContactNo { get; set; }
        public string MobileNo { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
