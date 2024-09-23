using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class ContractorPermitEmployeeDetailsVO
    {
        public int ContractorPermitEmployeeID { get; set; }
        public int PermitRequestID { get; set; }
        
        public string PermitRequestAreaCode { get; set; }
        public string EmployeeName { get; set; }
        public string IDNumber { get; set; }
        public string JobTitle { get; set; }
        public string CriminalRecord { get; set; }
        public string EmpSignature { get; set; }
        public string RecordStatus { get; set; }
    }
}
