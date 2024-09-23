using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PermitRequestContractorVO
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
    }
}
