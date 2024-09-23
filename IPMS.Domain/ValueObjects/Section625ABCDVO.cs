using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public  class Section625ABCDVO
    {
        public int Section625ABCDID { get; set; }
        public string TOMSLogEntryNo { get; set; }
        public string OperatorName { get; set; }
        public string LincseNumber { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string SiteTerminal { get; set; }
        public Nullable<System.DateTime> ChangeControlDateTime { get; set; }
        public string CDName { get; set; }
        public string CDDesignation { get; set; }
        public string CDContactNumber { get; set; }
        public string CDMobileNumber { get; set; }
        public string CDEmailID { get; set; }
        public string CDAddress { get; set; }
        public string ChangeControlLicensedOperator { get; set; }
        public string AnticipatedImpactOnBBBEERating { get; set; }
        public Nullable<int> Hour24Report625ID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
