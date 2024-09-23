using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class IndividualPermitAppliactionDetailsVO
    {
        public int IndividualApplicationID { get; set; }
        public int PermitRequestID { get; set; }
        public string Initial { get; set; }
        public string SACitizen { get; set; }
        public string Gender { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryOfOrigin { get; set; }
        public string DepartmentManager { get; set; }
        public string JobTitle { get; set; }
        public string Current_Permit_Exists { get; set; }
        public string Reason_Reapplication { get; set; }
        public string Port_Induction_Training { get; set; }
        public System.DateTime? Training_Date { get; set; }
        public string Criminal_Bckground { get; set; }
        public string Signature { get; set; }
        public System.DateTime Date { get; set; }
        public string EmployeeNo { get; set; }
        public string EmailAddress { get; set; }
        
    }
}
