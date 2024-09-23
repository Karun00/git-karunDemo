using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class ContractorPermitApplicationDetails : EntityBase
     
    {
        public int ContractorPermitApplicationID { get; set; }
        public int PermitRequestID { get; set; }
        public string ContractorCompanyName { get; set; }
        public string ContractorCompanyManager { get; set; }
        public string Department { get; set; }
        public string TelephoneNumber { get; set; }
        public string SubContractorCompanyName { get; set; }
        public string SubContractorTelephoneNumber { get; set; }        
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
