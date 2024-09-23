using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class ContractorPermitApplicationDetailsVO
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

    }
}
